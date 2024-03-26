
namespace Hqub.MusicBrainz
{
    using System;
    using System.Net;

    /// <summary>
    /// Exception containing error information returned by the MusicBrainz web service.
    /// </summary>
    public sealed class WebServiceException : Exception
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
        public WebServiceException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebServiceException"/> class.
        /// </summary>
        public WebServiceException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebServiceException"/> class.
        /// </summary>
        public WebServiceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

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