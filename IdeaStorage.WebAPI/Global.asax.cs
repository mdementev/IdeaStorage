namespace IdeaStorage.WebAPI
{
    using System;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;

    /// <summary>
    /// API Service Application File.
    /// </summary>
    public class WebApiApplication : HttpApplication
    {
        #region Methods

        /// <summary>
        /// Code that runs on application startup.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        protected void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        #endregion
    }
}