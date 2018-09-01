
namespace Hqub.MusicBrainz.API
{
    using System;
    using System.Net;

    /// <summary>
    /// Exception containing error information returned by the MusicBrainz webservice.
    /// </summary>
    [Serializable]
    public class WebServiceException : Exception
    {
        /// <summary>
        /// Gets the response HTTP status code.
        /// </summary>
        public readonly HttpStatusCode StatusCode;

        /// <summary>
        /// Gets the requested uri.
        /// </summary>
        public readonly string Uri;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebServiceException"/> class.
        /// </summary>
        /// <param name="statusCode">The response HTTP status code.</param>
        /// <param name="uri">The requested uri.</param>
        /// <param name="message">The error message.</param>
        public WebServiceException(string message, HttpStatusCode statusCode, string uri)
            : base(message)
        {
            StatusCode = statusCode;
            Uri = uri;
        }
    }
}