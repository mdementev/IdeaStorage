namespace IdeaStorage.WebAPI.Extensions
{
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;

    /// <summary>
    /// Contains extension methods for <see cref="HttpRequestMessage"/> class.
    /// </summary>
    public static class HttpRequestMessageExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// Retrieves an individual cookie from the cookies collection.
        /// </summary>
        /// <param name="request">The request to check for cookie.</param>
        /// <param name="cookieName">Name of the cookie.</param>
        /// <returns>Value of the cookie as string if cookie exists, otherwise <c>null</c>.</returns>
        public static string GetCookieValue(this HttpRequestMessage request, string cookieName)
        {
            CookieHeaderValue cookie = request.Headers.GetCookies(cookieName).FirstOrDefault();
            return cookie != null ? cookie[cookieName].Value : null;
        }

        #endregion
    }
}