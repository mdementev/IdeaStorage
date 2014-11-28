namespace IdeaStorage.WebAPI
{
    using System.Web.Http;

    using IdeaStorage.WebAPI.Filters;

    public static class WebApiConfig
    {
        #region Public Methods and Operators

        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.EnableCors();

            config.Filters.Add(new IdeaStorageAuthorizationFilterAttribute());
        }

        #endregion
    }
}