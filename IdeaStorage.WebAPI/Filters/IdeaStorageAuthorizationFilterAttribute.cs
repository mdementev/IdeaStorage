namespace IdeaStorage.WebAPI.Filters
{
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Security.Authentication;
    using System.Security.Principal;
    using System.Threading;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;
    using System.Web.Security;

    using BusinessLogic.Authorization;

    using IdeaStorage.WebAPI.Extensions;

    public class IdeaStorageAuthorizationFilterAttribute : AuthorizationFilterAttribute
    {
        #region Public Methods and Operators

        /// <summary>
        /// Calls when a process requests authorization.
        /// </summary>
        /// <param name="actionContext">The action context, which encapsulates information for 
        /// using <see cref="T:System.Web.Http.Filters.AuthorizationFilterAttribute" />.</param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            // Verifies if Allow Anonymous is set for this action.
            // If yes then do nothing.
            if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
                || actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>()
                       .Any())
            {
                base.OnAuthorization(actionContext);
                return;
            }

            try
            {
                // Verifies if user has been already authenticated by checking if authentication token exist.
                // Get the token value.
                string password = actionContext.Request.GetCookieValue("password");
                string email = actionContext.Request.GetCookieValue("email");

                IAuthorizationManager manager = new AuthorizationManager();

                IPrincipal principal = manager.ValidateUser(email, password);

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

                base.OnAuthorization(actionContext);
            }
            catch
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(
                    HttpStatusCode.Unauthorized, 
                    "Invalid login or password");
                return;
            }

            base.OnAuthorization(actionContext);
        }

        #endregion
    }
}