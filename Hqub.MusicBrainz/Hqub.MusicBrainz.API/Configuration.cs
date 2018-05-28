
namespace Hqub.MusicBrainz.API
{
    using System.Net;

    /// <summary>
    /// Hqub.MusicBrainz configuration.
    /// </summary>
    public static class Configuration
    {
        static Configuration()
        {
            GenerateCommunicationThrow = true;
            Proxy = null;
            UserAgent = "Hqub.MusicBrainz/2.0";
        }

        /// <summary>
        /// If true, then all exceptions for http-requests to MusicBrainz (from class WebRequestHelper) will
        /// throw up. Otherwise they will be suppressed.
        /// </summary>
        public static bool GenerateCommunicationThrow { get; set; }


        /// <summary>
        /// Gets or sets a <see cref="System.Net.IWebProxy"/> used to query the webservice.
        /// </summary>
        public static IWebProxy Proxy { get; set; }

        /// <summary>
        /// Allow set custom user agent string.
        /// </summary>
        public static string UserAgent { get; set; }
    }
}
