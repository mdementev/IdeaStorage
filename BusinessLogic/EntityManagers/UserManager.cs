namespace BusinessLogic.EntityManagers
{
    using System.Linq;
    using System.Reflection;
    using System.Security.Authentication;

    using BusinessLogic.Cryptography;
    using BusinessLogic.Exceptions;
    using BusinessLogic.Mappers;
    using BusinessLogic.Validators;

    using FluentValidation;

    using IdeaSorage.DataModel;

    using IdeaStorage.EntriesModel.Entries;

    using log4net;

    /// <summary>
    /// Implements methods to work with UserManager in Idea Storage project.
    /// </summary>
    public sealed class UserManager : IUserManager
    {
        #region Constants and Fields

        /// <summary>
        /// The log.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Creates the user using model and password string.
        /// </summary>
        /// <param name="newUser">Model of the new user.</param>
        /// <param name="password">The string with password for new user.</param>
        /// <returns>Id of newly created user.</returns>
        public int CreateUser(User newUser, string password)
        {
            Log.DebugFormat("Begin CreateUser('{0}')", newUser);
            UserValidator userValidator = new UserValidator();
            userValidator.ValidateAndThrow(newUser);

            if (this.FindUserByEmail(newUser.Email) != null)
            {
                string message = string.Format("User with email:'{0}' already exists in data base.", newUser.Email);
                Log.Debug(message);
                throw new DuplicateEntityException(message);
            }

            using (IdeaStorageEntities context = new IdeaStorageEntities())
            {
                ICryptographyManager cryptography = new CryptographyManager();
                string salt = cryptography.CreateSalt(12);
                USER user = new USER
                {
                    FirstName = newUser.FirstName, 
                    SecondName = newUser.SecondName, 
                    IsDeleted = newUser.IsDeleted, 
                    Email = newUser.Email.ToLower(), 
                    Salt = salt, 
                    Password = cryptography.HashPassword(password, salt)
                };

                context.USERS.Add(user);
                context.SaveChanges();
                Log.DebugFormat("Created user's ID:'{0}'", user.UserId);
                return user.UserId;
            }
        }

        /// <summary>
        /// Soft deletes the user. Change isDeleted flag to true.
        /// </summary>
        /// <param name="id">The identifier for user which should be deleted.</param>
        public void DeleteUser(int id)
        {
            Log.DebugFormat("Begin DeleteUser(id:'{0}')", id);

            using (IdeaStorageEntities context = new IdeaStorageEntities())
            {
                USER user = context.USERS.SingleOrDefault(u => u.UserId == id);

                if (user == null)
                {
                    string message = string.Format("User with id:'{0}' doesn't exist in data base.", id);
                    Log.Debug(message);
                    throw new EntityDoesNotExistException(message);
                }

                user.IsDeleted = true;
                context.SaveChanges();
            }

            Log.DebugFormat("User with ID:'{0}' deleted", id);
        }

        /// <summary>
        /// Finds existent user by email.
        /// </summary>
        /// <param name="email">The users's email.</param>
        /// <param name="includeDeleted">if set to <c>true</c> then deleted users will be included in search strategy.</param>
        /// <returns>
        /// An <see cref="User" /> instance if user can be found by email.
        /// </returns>
        public User FindUserByEmail(string email, bool includeDeleted = false)
        {
            Log.DebugFormat("Begin FindUserByEmail(email:'{0}', includeDeleted:'{1}')", email, includeDeleted);

            using (IdeaStorageEntities context = new IdeaStorageEntities())
            {
                User foundUser = context.USERS.SingleOrDefault(u => u.Email.Equals(email.ToLower()) && (!u.IsDeleted || includeDeleted))
                    .ToModel();
                Log.DebugFormat("Found user:'{0}'", foundUser);
                return foundUser;
            }
        }

        /// <summary>
        /// Gets the user from data base by id.
        /// </summary>
        /// <param name="id">The UserId.</param>
        /// <param name="includeDeleted">if set to <c>true</c> then deleted users will be included in search strategy.</param>
        /// <returns>
        /// An <see cref="User" /> instance if user can be found by identifier.
        /// </returns>
        public User GetUserById(int id, bool includeDeleted = false)
        {
            Log.DebugFormat("Begin GetUserById(id:'{0}', includeDeleted:'{1}')", id, includeDeleted);

            using (IdeaStorageEntities context = new IdeaStorageEntities())
            {
                User user = context.USERS.SingleOrDefault(u => u.UserId == id && (!u.IsDeleted || includeDeleted))
                    .ToModel();

                if (user == null)
                {
                    string message = string.Format("User with id:'{0}' doesn't exist in data base.", id);
                    Log.Debug(message);
                    throw new EntityDoesNotExistException(message);
                }

                Log.DebugFormat("Returned: '{0}'", user);

                return user;
            }
        }

        /// <summary>
        /// Gets user credentials from database (hashed password and salt).
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="hash">The hashed password.</param>
        /// <param name="salt">The password's salt.</param>
        public void GetUserCredentials(int userId, out string hash, out string salt)
        {
            Log.DebugFormat("Begin GetUserCredentials(userId:'{0}')", userId);

            using (IdeaStorageEntities context = new IdeaStorageEntities())
            {
                USER user = context.USERS.SingleOrDefault(u => u.UserId == userId);

                if (user == null)
                {
                    string message = string.Format("User with ID:'{0}' doesn't exist in data base.", userId);
                    Log.Debug(message);
                    throw new EntityDoesNotExistException(message);
                }

                hash = user.Password;
                salt = user.Salt;
            }

            Log.DebugFormat("Credentials returned for user with ID:'{0}'", userId);
        }

        /// <summary>
        /// Restores the user which soft deleted. Change isDeleted flag to False.
        /// </summary>
        /// <param name="id">The identifier for user which should be restored.</param>
        public void RestoreUser(int id)
        {
            Log.DebugFormat("Begin RestoreUser(id:'{0}')", id);

            using (IdeaStorageEntities context = new IdeaStorageEntities())
            {
                USER user = context.USERS.SingleOrDefault(u => u.UserId == id);

                if (user == null)
                {
                    string message = string.Format("User with ID:'{0}' doesn't exist in data base.", id);
                    Log.Debug(message);
                    throw new EntityDoesNotExistException(message);
                }

                user.IsDeleted = false;
                context.SaveChanges();
            }

            Log.DebugFormat("User with ID:'{0}' restored", id);
        }

        /// <summary>
        /// Updates the user in data base according the model.
        /// </summary>
        /// <param name="user">An <see cref="User" /> instance.</param>
        public void UpdateUser(User user)
        {
            Log.DebugFormat("Begin UpdateUser('{0}')", user);

            UserValidator userValidator = new UserValidator();
            userValidator.ValidateAndThrow(user);

            using (IdeaStorageEntities context = new IdeaStorageEntities())
            {
                USER userFromDataBase = context.USERS.SingleOrDefault(u => u.UserId == user.UserId);

                if (userFromDataBase == null)
                {
                    string message = string.Format("User with ID:'{0}' doesn't exist in data base.", user.UserId);
                    Log.Debug(message);
                    throw new EntityDoesNotExistException(message);
                }

                if (context.USERS.SingleOrDefault(u => u.Email == user.Email.ToLower() && u.UserId != user.UserId) != null)
                {
                    string message = string.Format("User with email:'{0}' already exists in data base.", user.Email);
                    Log.Debug(message);
                    throw new DuplicateEntityException(message);
                }

                userFromDataBase.Email = user.Email.ToLower();
                userFromDataBase.FirstName = user.FirstName;
                userFromDataBase.IsDeleted = user.IsDeleted;
                userFromDataBase.SecondName = user.SecondName;
                context.SaveChanges();
            }

            Log.DebugFormat("User updated '{0}'", user);
        }

        /// <summary>
        /// Updates the user password and salt.
        /// </summary>
        /// <param name="userId">The user's id.</param>
        /// <param name="oldPassword">The old password which should be updated.</param>
        /// <param name="newPassword">The new password for user.</param>
        /// <exception cref="System.Security.Authentication.AuthenticationException">Invalid email or password.</exception>
        public void UpdateUserCredentials(int userId, string oldPassword, string newPassword)
        {
            Log.DebugFormat("Begin UpdateUserCredentials(userId:'{0}')", userId);

            string passwordFromDataBase;
            string oldSalt;
            this.GetUserCredentials(userId, out passwordFromDataBase, out oldSalt);

            ICryptographyManager cryptographyManager = new CryptographyManager();
            oldPassword = cryptographyManager.HashPassword(oldPassword, oldSalt);

            if (oldPassword != passwordFromDataBase)
            {
                const string Message = "Invalid email or password.";
                Log.Debug(Message);
                throw new AuthenticationException(Message);
            }

            using (IdeaStorageEntities context = new IdeaStorageEntities())
            {
                USER user = context.USERS.Single(u => u.UserId == userId);
                user.Salt = cryptographyManager.CreateSalt(12);
                user.Password = cryptographyManager.HashPassword(newPassword, user.Salt);
                context.SaveChanges();
            }

            Log.DebugFormat("Credentials updated for user with ID:'{0}'", userId);
        }

        #endregion
    }
}