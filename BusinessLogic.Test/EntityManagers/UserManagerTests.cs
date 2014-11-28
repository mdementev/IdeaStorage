namespace BusinessLogic.Test.EntityManagers
{
    using System.Security.Authentication;

    using BusinessLogic.Authorization;
    using BusinessLogic.Cryptography;
    using BusinessLogic.EntityManagers;
    using BusinessLogic.Exceptions;

    using FluentValidation;

    using IdeaStorage.EntriesModel.Entries;

    using Infrastructure.Testing;

    using NUnit.Framework;

    /// <summary>
    /// Contains tests, which verifies <see cref="UserManager"/> logic.
    /// </summary>
    public class UserManagerTests : DatabaseTestFixture
    {
        #region Public Methods and Operators

        /// <summary>
        /// Test verified that email validation working correctly.
        /// </summary>
        [Test]
        public void CreateUser_InvalidEmail_ValidationException()
        {
            User user = new User
            {
                Email = "sample@sample", 
                FirstName = "Sample first name", 
                IsDeleted = false, 
                SecondName = "Sample second name"
            };

            UserManager userManager = new UserManager();

            ValidationException expectedException =
                Assert.Throws<ValidationException>(
                    () => userManager.CreateUser(user, "password"));

            //Assert.AreEqual("Validation failed: \r\n -- 'Email' is not a valid email address.", expectedException.Message);
        }

        /// <summary>
        /// Verifies, that user cannot be created successfully if user with duplicated email already exists in data base.
        /// </summary>
        [Test]
        public void CreateUser_UserWithDuplicatedEmail_DuplicateEntityException()
        {
            User user = new User
            {
                Email = "duplicated@mail.com", 
                FirstName = "First", 
                SecondName = "Second"
            };

            const string Password = "qwerty";

            IUserManager userManager = new UserManager();
            userManager.CreateUser(user, Password);

            DuplicateEntityException expectedException =
                Assert.Throws<DuplicateEntityException>(
                    () => userManager.CreateUser(user, Password));

            Assert.AreEqual("User with email:'duplicated@mail.com' already exists in data base.", expectedException.Message);
        }

        /// <summary>
        /// Verifies, that two users might be created successfully if they have duplicated names.
        /// </summary>
        [Test]
        public void CreateUser_UsersWithDuplicatedNames_CreatedWithoutExceptions()
        {
            User firstUser = new User
            {
                Email = "first@mail.com", 
                FirstName = "First duplicated name", 
                SecondName = "Second duplicated name"
            };

            User secondUser = new User
            {
                Email = "second@mail.com", 
                FirstName = "First duplicated name", 
                SecondName = "Second duplicated name"
            };

            const string Password = "qwerty";

            IUserManager userManager = new UserManager();
            userManager.CreateUser(firstUser, Password);
            userManager.CreateUser(secondUser, Password);
        }

        /// <summary>
        /// Verifies, that user might be created successfully for valid user data.
        /// </summary>
        [Test]
        public void CreateUser_ValidUserData_UserIsCreated()
        {
            User user = new User
            {
                Email = "user@mail.com", 
                FirstName = "First", 
                SecondName = "Second"
            };

            const string Password = "qwerty";

            IUserManager userManager = new UserManager();
            int userId = userManager.CreateUser(user, Password);

            User cretedUser = userManager.GetUserById(userId);

            Assert.AreEqual(user.Email, cretedUser.Email);
            Assert.AreEqual(user.FirstName, cretedUser.FirstName);
            Assert.AreEqual(user.SecondName, cretedUser.SecondName);
        }

        /// <summary>
        /// Tries to delete the user which not exists in data base.
        /// </summary>
        [Test]
        public void DeleteUser_InvalidId_EntityDoesNotExistException()
        {
            UserManager userManager = new UserManager();
            EntityDoesNotExistException expectedException =
                Assert.Throws<EntityDoesNotExistException>(
                    () => userManager.DeleteUser(-1));

            Assert.AreEqual("User with id:'-1' doesn't exist in data base.", expectedException.Message);
        }

        /// <summary>
        /// Test verify that user can be soft deleted.
        /// </summary>
        [Test]
        public void DeleteUser_ValidUser_UserDeleted()
        {
            User user = new User
            {
                Email = "sample@sample.com", 
                FirstName = "Sample first name", 
                IsDeleted = false, 
                SecondName = "Sample second name"
            };

            UserManager userManager = new UserManager();
            int newUserId = userManager.CreateUser(user, "SamplePassword");
            userManager.DeleteUser(newUserId);

            Assert.IsTrue(userManager.GetUserById(newUserId, true)
                .IsDeleted);
        }

        /// <summary>
        /// Finds the deleted user by email in non-deleted. Assert than Null returned.
        /// </summary>
        [Test]
        public void FindUserByEmail_DeletedUser_NullReturned()
        {
            User user = new User
            {
                Email = "sample@sample.com", 
                FirstName = "Sample first name", 
                IsDeleted = true, 
                SecondName = "Sample second name", 
            };

            UserManager userManager = new UserManager();
            userManager.CreateUser(user, "sample pass");

            Assert.Null(userManager.FindUserByEmail("sample@sample.com"));
        }

        /// <summary>
        /// Finds the deleted user by email.
        /// </summary>
        [Test]
        public void FindUserByEmail_DeletedUser_UserFound()
        {
            User user = new User
            {
                Email = "sample@sample.com", 
                FirstName = "Sample first name", 
                IsDeleted = true, 
                SecondName = "Sample second name", 
            };

            UserManager userManager = new UserManager();
            userManager.CreateUser(user, "sample pass");
            User newUser = userManager.FindUserByEmail("sample@sample.com", true);

            Assert.AreEqual(newUser.Email, "sample@sample.com");
            Assert.AreEqual(newUser.FirstName, "Sample first name");
            Assert.AreEqual(newUser.SecondName, "Sample second name");
            Assert.AreEqual(newUser.IsDeleted, true);
        }

        /// <summary>
        /// Test finds the not deleted user by email.
        /// </summary>
        [Test]
        public void FindUserByEmail_ValidUser_UserFound()
        {
            User user = new User
            {
                Email = "sample@sample.com", 
                FirstName = "Sample first name", 
                IsDeleted = false, 
                SecondName = "Sample second name", 
            };

            UserManager userManager = new UserManager();
            userManager.CreateUser(user, "sample pass");
            User newUser = userManager.FindUserByEmail("sample@sample.com");

            Assert.AreEqual(newUser.Email, "sample@sample.com");
            Assert.AreEqual(newUser.FirstName, "Sample first name");
            Assert.AreEqual(newUser.SecondName, "Sample second name");
            Assert.AreEqual(newUser.IsDeleted, false);
        }

        /// <summary>
        /// Gets the deleted user by Id. Include users which are deleted.
        /// </summary>
        [Test]
        public void GetUserById_DeletedUser_DeletedUserReturned()
        {
            User user = new User
            {
                Email = "sample@email.com", 
                FirstName = "First name", 
                SecondName = "Second name", 
                IsDeleted = true
            };

            const string Password = "qwerty";

            IUserManager userManager = new UserManager();
            int userId = userManager.CreateUser(user, Password);

            userManager.GetUserById(userId, true);
        }

        /// <summary>
        /// Tries to get deleted user by id. EntityDoesNotExistException expected.
        /// </summary>
        [Test]
        public void GetUserById_DeletedUser_EntityDoesNotExistException()
        {
            User user = new User
            {
                Email = "sample@email.com", 
                FirstName = "First name", 
                SecondName = "Second name", 
                IsDeleted = true
            };

            const string Password = "qwerty";

            IUserManager userManager = new UserManager();
            int userId = userManager.CreateUser(user, Password);

            EntityDoesNotExistException expectedException =
                Assert.Throws<EntityDoesNotExistException>(
                    () => userManager.GetUserById(userId));

            Assert.AreEqual(string.Format("User with id:'{0}' doesn't exist in data base.", userId), expectedException.Message);
        }

        /// <summary>
        /// Test verified that user can be got from data base.
        /// </summary>
        [Test]
        public void GetUserById_NewUser_AssertProperties()
        {
            User user = new User
            {
                Email = "sample@sample.com", 
                FirstName = "Sample first name", 
                IsDeleted = false, 
                SecondName = "Sample second name", 
            };

            UserManager userManager = new UserManager();
            User newUser = userManager.GetUserById(userManager.CreateUser(user, "SamplePass"));

            Assert.AreEqual(newUser.Email, "sample@sample.com");
            Assert.AreEqual(newUser.FirstName, "Sample first name");
            Assert.AreEqual(newUser.SecondName, "Sample second name");
            Assert.AreEqual(newUser.IsDeleted, false);
        }

        /// <summary>
        /// Tries to get user credentials for user which not exist in data base.
        /// </summary>
        [Test]
        public void GetUserCredentials_InvalidUser_EntityDoesNotExistException()
        {
            UserManager userManager = new UserManager();
            string salt;
            string password;

            EntityDoesNotExistException expectedException =
                Assert.Throws<EntityDoesNotExistException>(
                    () => userManager.GetUserCredentials(-1, out password, out salt));

            Assert.AreEqual("User with ID:'-1' doesn't exist in data base.", expectedException.Message);
        }

        /// <summary>
        /// Gets the user credentials. Assert salt and password.
        /// </summary>
        [Test]
        public void GetUserCredentials_ValidUser_AssertSaltAndPassword()
        {
            User user = new User
            {
                Email = "sample@sample.com", 
                FirstName = "Sample first name", 
                IsDeleted = false, 
                SecondName = "Sample second name"
            };

            ICryptographyManager cryptographyManager = new CryptographyManager();
            IUserManager userManager = new UserManager();

            const string Password = "SamplePass";

            int newUserId = userManager.CreateUser(user, Password);

            string newHashedPassword;
            string newSalt;

            userManager.GetUserCredentials(newUserId, out newHashedPassword, out newSalt);

            Assert.AreEqual(newHashedPassword, cryptographyManager.HashPassword(Password, newSalt));
        }

        /// <summary>
        /// Test verify that user can be restored after soft deleting.
        /// </summary>
        [Test]
        public void RestoreUser_DeletedUser_Restored()
        {
            User user = new User
            {
                Email = "sample@sample.com", 
                FirstName = "Sample first name", 
                IsDeleted = true, 
                SecondName = "Sample second name"
            };

            UserManager userManager = new UserManager();
            int newUserId = userManager.CreateUser(user, "SamplePassword");
            userManager.RestoreUser(newUserId);

            Assert.IsFalse(userManager.GetUserById(newUserId, true)
                .IsDeleted);
        }

        /// <summary>
        /// Tries to restore user which not exist in data base.
        /// </summary>
        [Test]
        public void RestoreUser_InvalidUser_EntityDoesNotExistException()
        {
            UserManager userManager = new UserManager();

            EntityDoesNotExistException expectedException =
                Assert.Throws<EntityDoesNotExistException>(
                    () => userManager.RestoreUser(-1));

            Assert.AreEqual("User with ID:'-1' doesn't exist in data base.", expectedException.Message);
        }

        /// <summary>
        /// Verified that user credentials cannot be updated with invalid password.
        /// </summary>
        [Test]
        public void UpdateUserCredentials_InvalidCredentials_AuthenticationException()
        {
            User user = new User
            {
                Email = "sample@sample.com", 
                FirstName = "Sample first name", 
                IsDeleted = false, 
                SecondName = "Sample second name"
            };

            UserManager userManager = new UserManager();
            int userId = userManager.CreateUser(user, "SamplePass");

            AuthenticationException expectedException =
                Assert.Throws<AuthenticationException>(
                    () => userManager.UpdateUserCredentials(userId, "2SamplePass", "NewPass"));

            Assert.AreEqual("Invalid email or password.", expectedException.Message);
        }

        /// <summary>
        /// Verified that EntityDoesNotExistException exception occurs if update user credentials with invalid id.
        /// </summary>
        [Test]
        public void UpdateUserCredentials_InvalidUserId_EntityDoesNotExistException()
        {
            User user = new User
            {
                Email = "sample@sample.com", 
                FirstName = "Sample first name", 
                IsDeleted = false, 
                SecondName = "Sample second name"
            };

            UserManager userManager = new UserManager();
            userManager.CreateUser(user, "SamplePass");

            EntityDoesNotExistException expectedException =
                Assert.Throws<EntityDoesNotExistException>(
                    () => userManager.UpdateUserCredentials(-1, "SamplePass", "NewPass"));

            Assert.AreEqual("User with ID:'-1' doesn't exist in data base.", expectedException.Message);
        }

        /// <summary>
        /// Verified that user credentials updated successful if old password is correct.
        /// </summary>
        [Test]
        public void UpdateUserCredentials_ValidCredentials_CredentialsUpdated()
        {
            User user = new User
            {
                Email = "sample@sample.com", 
                FirstName = "Sample first name", 
                IsDeleted = false, 
                SecondName = "Sample second name"
            };

            UserManager userManager = new UserManager();
            int userId = userManager.CreateUser(user, "SamplePass");

            userManager.UpdateUserCredentials(userId, "SamplePass", "NewPass");

            AuthorizationManager authorizationManager = new AuthorizationManager();
            Assert.IsNotNull(authorizationManager.ValidateUser(userManager.GetUserById(userId).Email, "NewPass"));
        }

        /// <summary>
        /// Tries to update user's email address to address which already user by another user.
        /// </summary>
        [Test]
        public void UpdateUser_DuplicateEmail_DuplicateEntityException()
        {
            User user = new User
            {
                Email = "sample@sample.com", 
                FirstName = "Sample first name", 
                IsDeleted = false, 
                SecondName = "Sample second name"
            };

            UserManager userManager = new UserManager();

            userManager.CreateUser(user, "password");

            user.Email = "new@sample.com";
            user.UserId = userManager.CreateUser(user, "password");
            user.Email = "sample@sample.com";

            DuplicateEntityException expectedException =
                Assert.Throws<DuplicateEntityException>(
                    () => userManager.UpdateUser(user));

            Assert.AreEqual(string.Format("User with email:'{0}' already exists in data base.", user.Email), expectedException.Message);
        }

        /// <summary>
        /// Tries to update user which not exist in data base.
        /// </summary>
        [Test]
        public void UpdateUser_InvalidUser_EntityDoesNotExistException()
        {
            User user = new User
            {
                Email = "sample@sample.com", 
                FirstName = "Sample first name", 
                IsDeleted = false, 
                SecondName = "Sample second name"
            };

            UserManager userManager = new UserManager();

            EntityDoesNotExistException expectedException =
                Assert.Throws<EntityDoesNotExistException>(
                    () => userManager.UpdateUser(user));

            Assert.AreEqual("User with ID:'0' doesn't exist in data base.", expectedException.Message);
        }

        /// <summary>
        /// Create and update the user, get it from data base and verify that it updated correctly.
        /// </summary>
        [Test]
        public void UpdateUser_ValidUserData_UserUpdated()
        {
            User user = new User
            {
                Email = "sample@sample.com", 
                FirstName = "Sample first name", 
                IsDeleted = false, 
                SecondName = "Sample second name"
            };

            UserManager userManager = new UserManager();
            User newUser = userManager.GetUserById(userManager.CreateUser(user, "SamplePassword"));
            newUser.Email = "New@sample.com";
            newUser.FirstName = "New first name";
            newUser.SecondName = "New second name";
            newUser.IsDeleted = false;
            userManager.UpdateUser(newUser);

            Assert.AreEqual(newUser.Email, "New@sample.com");
            Assert.AreEqual(newUser.FirstName, "New first name");
            Assert.AreEqual(newUser.IsDeleted, false);
            Assert.AreEqual(newUser.SecondName, "New second name");
        }

        #endregion
    }
}