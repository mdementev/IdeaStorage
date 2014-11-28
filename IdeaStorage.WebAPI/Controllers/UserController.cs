namespace IdeaStorage.WebAPI.Controllers
{
    using System.Net;
    using System.Net.Http;
    using System.Security.Principal;
    using System.Threading;
    using System.Web.Http;

    using BusinessLogic.EntityManagers;

    using IdeaStorage.EntriesModel.Entries;

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
        [HttpPost]
        [Route("GetLoggedInUser")]
        public HttpResponseMessage GetLoggedInUser(HttpRequestMessage request)
        {
            IIdentity identity = Thread.CurrentPrincipal.Identity;
            IUserManager manager = new UserManager();
            User loggedInUser = manager.FindUserByEmail(identity.Name);

            return request.CreateResponse(HttpStatusCode.OK, loggedInUser);
        }

        #endregion
    }
}