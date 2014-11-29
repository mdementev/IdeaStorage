namespace IdeaStorage.WebAPI.Controllers
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Security.Authentication;
    using System.Security.Principal;
    using System.Web;
    using System.Web.Http;

    using BusinessLogic.Authorization;
    using BusinessLogic.EntityManagers;
    using BusinessLogic.Searches;

    using IdeaStorage.EntriesModel.Entries;
    using IdeaStorage.WebAPI.Models;

    public class NodeController : ApiController
    {
        #region Public Methods and Operators

        [AllowAnonymous]
        [Route("CreateNode")]
        [HttpPost]
        public HttpResponseMessage CreateNode(NodeModel nodeModel)
        {
            HttpResponseMessage responseMessage;
            try
            {
                IAuthorizationManager manager = new AuthorizationManager();

                IPrincipal principal = manager.ValidateUser(nodeModel.Credentials.Email, nodeModel.Credentials.Password);

                if (principal == null)
                {
                    throw new AuthenticationException("Invalid user name or password.");
                }

                INodeManager nodeManager = new NodeManager();
                nodeManager.CreateNode(nodeModel.Node);

                IUserManager userManager = new UserManager();
                User loggedInUser = userManager.FindUserByEmail(nodeModel.Credentials.Email);

                responseMessage = this.Request.CreateResponse(HttpStatusCode.OK, loggedInUser);
            }
            catch
            {
                responseMessage = this.Request.CreateErrorResponse(
                    HttpStatusCode.Unauthorized, 
                    "Invalid user name or password.");
            }

            return responseMessage;
        }

        [AllowAnonymous]
        [Route("DeleteNode")]
        [HttpPost]
        public HttpResponseMessage DeleteNode(NodeModel nodeModel)
        {
            HttpResponseMessage responseMessage;
            try
            {
                IAuthorizationManager manager = new AuthorizationManager();

                IPrincipal principal = manager.ValidateUser(nodeModel.Credentials.Email, nodeModel.Credentials.Password);

                if (principal == null)
                {
                    throw new AuthenticationException("Invalid user name or password.");
                }

                INodeManager nodeManager = new NodeManager();
                nodeManager.DeleteNode(nodeModel.Node.NodeId);

                IUserManager userManager = new UserManager();
                User loggedInUser = userManager.FindUserByEmail(nodeModel.Credentials.Email);

                responseMessage = this.Request.CreateResponse(HttpStatusCode.OK, loggedInUser);
            }
            catch
            {
                responseMessage = this.Request.CreateErrorResponse(
                    HttpStatusCode.Unauthorized, 
                    "Invalid user name or password.");
            }

            return responseMessage;
        }

        [AllowAnonymous]
        [Route("UpdateNode")]
        [HttpPost]
        public HttpResponseMessage UpdateNode(NodeModel nodeModel)
        {
            HttpResponseMessage responseMessage;
            try
            {
                IAuthorizationManager manager = new AuthorizationManager();

                IPrincipal principal = manager.ValidateUser(nodeModel.Credentials.Email, nodeModel.Credentials.Password);

                if (principal == null)
                {
                    throw new AuthenticationException("Invalid user name or password.");
                }

                INodeManager nodeManager = new NodeManager();
                nodeManager.UpdateNode(nodeModel.Node);

                IUserManager userManager = new UserManager();
                User loggedInUser = userManager.FindUserByEmail(nodeModel.Credentials.Email);

                responseMessage = this.Request.CreateResponse(HttpStatusCode.OK, loggedInUser);
            }
            catch
            {
                responseMessage = this.Request.CreateErrorResponse(
                    HttpStatusCode.Unauthorized, 
                    "Invalid user name or password.");
            }

            return responseMessage;
        }

        [AllowAnonymous]
        [Route("FindNodeByTag")]
        [HttpPost]
        public HttpResponseMessage FindNodeByTag(SearchModel search)
        {
            HttpResponseMessage responseMessage;
            try
            {
                IAuthorizationManager manager = new AuthorizationManager();

                IPrincipal principal = manager.ValidateUser(search.Credentials.Email, search.Credentials.Password);

                if (principal == null)
                {
                    throw new AuthenticationException("Invalid user name or password.");
                }

                IUserManager userManager = new UserManager();
                User loggedInUser = userManager.FindUserByEmail(search.Credentials.Email);

                SearchManager searchManager = new SearchManager();
                loggedInUser.Nodes = searchManager.FindNodesByTags(search.SearchTerm);

                responseMessage = this.Request.CreateResponse(HttpStatusCode.OK, loggedInUser);
            }
            catch
            {
                responseMessage = this.Request.CreateErrorResponse(
                    HttpStatusCode.Unauthorized,
                    "Invalid chegototam.");
            }

            return responseMessage;
        }

        #endregion
    }
}