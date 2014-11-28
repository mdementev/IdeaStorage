namespace BusinessLogic.EntityManagers
{
    using IdeaStorage.EntriesModel.Entries;

    /// <summary>
    /// Defines supported method to work with User Manager in Idea Storage project.
    /// </summary>
    public interface IUserManager
    {
        #region Public Methods and Operators

        /// <summary>
        /// Creates the user using model and password string.
        /// </summary>
        /// <param name="user">The user model.</param>
        /// <param name="password">The string with password for new user.</param>
        /// <returns>Id of created user.</returns>
        int CreateUser(User user, string password);

        /// <summary>
        /// Soft deletes the user. Change isDeleted flag to true.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteUser(int id);

        /// <summary>
        /// Finds existent user by email.
        /// </summary>
        /// <param name="email">The users's email.</param>
        /// <param name="includeDeleted">if set to <c>true</c> then deleted users will be included in search strategy.</param>
        /// <returns>
        /// An <see cref="User" /> instance if user can be found by email.
        /// If user doesn't exist or already deleted -- <c>null</c>.
        /// </returns>
        User FindUserByEmail(string email, bool includeDeleted = false);

        /// <summary>
        /// Gets the user from data base by id.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <param name="includeDeleted">if set to <c>true</c> then deleted users will be included in search strategy.</param>
        /// <returns>
        /// Users model.
        /// </returns>
        User GetUserById(int id, bool includeDeleted = false);

        /// <summary>
        /// Gets user credentials from database (hashed password and salt).
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="hash">The hashed password.</param>
        /// <param name="salt">The password's salt.</param>
        void GetUserCredentials(int userId, out string hash, out string salt);

        /// <summary>
        /// Restores the user which soft deleted. Change isDeleted flag to False.
        /// </summary>
        /// <param name="id">The identifier for user which should be restored.</param>
        void RestoreUser(int id);

        /// <summary>
        /// Updates the user in data base according the model.
        /// </summary>
        /// <param name="user">The user which should be updated.</param>
        void UpdateUser(User user);

        /// <summary>
        /// Updates the user password and salt.
        /// </summary>
        /// <param name="userId">The user's id.</param>
        /// <param name="oldPassword">The old password which should be updated.</param>
        /// <param name="newPassword">The new password for user.</param>
        /// <exception cref="System.Security.Authentication.AuthenticationException">Invalid email or password.</exception>
        void UpdateUserCredentials(int userId, string oldPassword, string newPassword);

        #endregion
    }
}