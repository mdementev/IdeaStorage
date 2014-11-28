namespace IdeaStorage.WebAPI.Controllers
{
    using System.Net;
    using System.Net.Http;
    using System.Security.Authentication;
    using System.Security.Principal;
    using System.Web.Http;

    using BusinessLogic.Authorization;
    using BusinessLogic.EntityManagers;

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

        #endregion
    }
}