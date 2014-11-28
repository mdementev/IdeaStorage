namespace BusinessLogic.Authorization
{
    using System.Security.Principal;

    using IdeaStorage.EntriesModel.Entries;

    /// <summary>
    /// Defines supported methods to work with Authorization in Idea Storage project.
    /// </summary>
    public interface IAuthorizationManager
    {
        #region Public Methods and Operators

        /// <summary>
        /// Validates the user by email as unique identifier and password.
        /// </summary>
        /// <param name="email">The user's email.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>An <see cref="User"/> instance if user's credentials are valid, otherwise <c>null</c>.</returns>
        IPrincipal ValidateUser(string email, string password);

        #endregion
    }
}