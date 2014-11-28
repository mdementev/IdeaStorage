namespace IdeaStorage.WebAPI.Controllers
{
    using System.Net;
    using System.Net.Http;
    using System.Security.Authentication;
    using System.Security.Principal;
    using System.Threading;
    using System.Web;
    using System.Web.Http;
    using System.Web.Security;

    using BusinessLogic.Authorization;
    using BusinessLogic.EntityManagers;

    using IdeaStorage.EntriesModel.Entries;
    using IdeaStorage.WebAPI.Models;

    /// <summary>
    /// Contains logic for working with users in Idea Storage API.
    /// </summary>
    public class UserController : ApiController
    {
        #region Public Methods and Operators

        /// <summary>
        /// Gets the logged in user.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The instance of <see cref="User"/></returns>
        [AllowAnonymous]
        [Route("GetLoggedInUser")]
        [HttpPost]
        public HttpResponseMessage GetLoggedInUser(LoginCredentials credentials)
        {
            HttpResponseMessage responseMessage;
            try
            {
                IAuthorizationManager manager = new AuthorizationManager();

                IPrincipal principal = manager.ValidateUser(credentials.Email, credentials.Password);

                if (principal == null)
                {
                    throw new AuthenticationException("Invalid user name or password.");
                }

                IUserManager userManager = new UserManager();
                User loggedInUser = userManager.FindUserByEmail(credentials.Email);

                responseMessage = this.Request.CreateResponse(HttpStatusCode.OK, loggedInUser);
            }
            catch
            {
                responseMessage = this.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Invalid user name or password.");
            }

            return responseMessage;
        }

        #endregion
    }
}