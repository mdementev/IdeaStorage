namespace BusinessLogic.Test.Authorization
{
    using BusinessLogic.Authorization;
    using BusinessLogic.EntityManagers;

    using IdeaStorage.EntriesModel.Entries;

    using Infrastructure.Testing;

    using NUnit.Framework;

    /// <summary>
    /// Contains tests, which verifies <see cref="AuthorizationManager"/> logic.
    /// </summary>
    public class AuthorizationManagerTests : DatabaseTestFixture
    {
        #region Public Methods and Operators

        /// <summary>
        /// Verified that null returned if valid user with invalid password tries to authorize.
        /// </summary>
        [Test]
        public void Authorize_InvalidPassword_NullReturned()
        {
            User user = new User
            {
                Email = "sample@sample.com", 
                FirstName = "Sample first name", 
                IsDeleted = false, 
                SecondName = "Sample second name"
            };

            UserManager userManager = new UserManager();
            userManager.CreateUser(user, "SamplePassword");

            AuthorizationManager authorizationManager = new AuthorizationManager();
            Assert.IsNull(authorizationManager.ValidateUser("sample@sample.com", "Password"));
        }

        /// <summary>
        /// Verified that null returned if invalid user tries to authorize.
        /// </summary>
        [Test]
        public void Authorize_InvalidUser_NullReturned()
        {
            AuthorizationManager authorizationManager = new AuthorizationManager();
            Assert.IsNull(authorizationManager.ValidateUser("email@email.com", "IncorrectPassword"));
        }

        /// <summary>
        /// Verified that <see cref="User"/> returned when authorize with valid credentials.
        /// </summary>
        [Test]
        public void Authorize_ValidUser_UserReturned()
        {
            User user = new User
            {
                Email = "sample@sample.com", 
                FirstName = "Sample first name", 
                IsDeleted = false, 
                SecondName = "Sample second name"
            };

            UserManager userManager = new UserManager();
            userManager.CreateUser(user, "SamplePassword");

            AuthorizationManager authorizationManager = new AuthorizationManager();
            Assert.IsNotNull(authorizationManager.ValidateUser("sample@sample.com", "SamplePassword"));
        }

        #endregion
    }
}