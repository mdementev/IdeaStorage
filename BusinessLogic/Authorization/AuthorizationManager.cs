namespace BusinessLogic.Authorization
{
    using System.Security.Principal;

    using BusinessLogic.Cryptography;
    using BusinessLogic.EntityManagers;

    using IdeaStorage.EntriesModel.Entries;

    /// <summary>
    /// Implements methods to work with Authorization in Idea Storage project.
    /// </summary>
    public class AuthorizationManager : IAuthorizationManager
    {
        #region Public Methods and Operators

        /// <summary>
        /// Validates the user by email as unique identifier and password.
        /// </summary>
        /// <param name="email">The user's email.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>An <see cref="User"/> instance if user's credentials are valid, otherwise <c>null</c>.</returns>
        public IPrincipal ValidateUser(string email, string password)
        {
            IUserManager userManager = new UserManager();

            User user = userManager.FindUserByEmail(email);
            if (user != null)
            {
                ICryptographyManager cryptographyManager = new CryptographyManager();

                string salt;
                string hash;
                userManager.GetUserCredentials(user.UserId, out hash, out salt);

                if (cryptographyManager.HashPassword(password, salt).Equals(hash))
                {
                    GenericIdentity identity = new GenericIdentity(email);
                    return new GenericPrincipal(identity, new[] { string.Empty });
                }
            }

            return null;
        }

        #endregion
    }
}