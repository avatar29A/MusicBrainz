
namespace Hqub.MusicBrainz.API
{
    using Hqub.MusicBrainz.API.Cache;
    using System;
    using System.Net;

    /// <summary>
    /// Hqub.MusicBrainz configuration.
    /// </summary>
    public static class Configuration
    {
        static Configuration()
        {
            Proxy = null;
            UserAgent = "Hqub.MusicBrainz/2.0";
        }

        /// <summary>
        /// If true, then all exceptions for http-requests to MusicBrainz (from class WebRequestHelper) will
        /// throw up. Otherwise they will be suppressed.
        /// </summary>
        [Obsolete("Will be removed. Use proper exception handling (catch WebServiceException).")]
        public static bool GenerateCommunicationThrow { get; set; } = true;

        /// <summary>
        /// Gets or sets a <see cref="System.Net.IWebProxy"/> used to query the webservice.
        /// </summary>
        public static IWebProxy Proxy { get; set; }

        /// <summary>
        /// Gets or sets a custom user agent string.
        /// </summary>
        public static string UserAgent { get; set; }

        /// <summary>
        /// Gets or sets the request cache.
        /// </summary>
        public static IRequestCache Cache { get; set; }
    }
}
