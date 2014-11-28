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

    using IdeaStorage.WebAPI.Models;

    /// <summary>
    /// Contains user authorization methods.
    /// </summary>
    public class LoginController : ApiController
    {
        #region Public Methods and Operators

        /// <summary>
        /// Validates if user exist in the system and creates user session.
        /// </summary>
        /// <param name="credentials">The credentials of the user to check.</param>
        /// <returns>
        /// The instance of <see cref="HttpResponseMessage" /> class, which represents response of Login operation.
        /// </returns>
        /// <exception cref="System.Security.Authentication.AuthenticationException">Invalid user name or password.</exception>
        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public HttpResponseMessage Login(LoginCredentials credentials)
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

                Thread.CurrentPrincipal = principal;
                if (HttpContext.Current != null)
                {
                    HttpContext.Current.User = principal;
                }

                if (FormsAuthentication.IsEnabled)
                {
                    FormsAuthentication.SetAuthCookie(principal.Identity.Name, false);
                }

                responseMessage = this.Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                responseMessage = this.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            return responseMessage;
        }

        /// <summary>
        /// Validates if user exist in the system and creates user session.
        /// </summary>
        /// <param name="email">The user's email.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>
        /// The instance of <see cref="HttpResponseMessage" /> class, which represents response of Login operation.
        /// </returns>
        /// <exception cref="System.Security.Authentication.AuthenticationException">Invalid user name or password.</exception>
        [AllowAnonymous]
        [Route("test")]
        [HttpPost]
        public HttpResponseMessage Test([FromBody] string email)
        {
            HttpResponseMessage responseMessage = this.Request.CreateResponse(HttpStatusCode.OK);

            return responseMessage;
        }

        /// <summary>
        /// Logout user from application.
        /// </summary>
        /// <returns>The instance of <see cref="HttpResponseMessage" /> class, which represents response of Logout operation.</returns>
        [HttpPost]
        [Route("logout")]
        public HttpResponseMessage Logout()
        {
            HttpResponseMessage responseMessage = this.Request.CreateResponse(HttpStatusCode.OK);

            if (FormsAuthentication.IsEnabled)
            {
                FormsAuthentication.SignOut();
            }

            return responseMessage;
        }

        #endregion
    }
}