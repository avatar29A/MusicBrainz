namespace Hqub.MusicBrainz.API
{
    using Hqub.MusicBrainz.API.Cache;
    using Hqub.MusicBrainz.API.Services;
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;
    using System.Threading.Tasks;

    // TODO: implement IDisposable to dispose http client.

    /// <summary>
    /// MusicBrainz client.
    /// </summary>
    public class MusicBrainzClient
    {
        /// <summary>
        /// The default address of the MusicBrainz webservice.
        /// </summary>
        public const string ServiceBaseAddress = "https://musicbrainz.org/ws/2/";

        private const string LookupTemplate = "{0}/{1}/?inc={2}&fmt=json";
        private const string BrowseTemplate = "{0}?{1}={2}&limit={3}&offset={4}&inc={5}&fmt=json";
        private const string SearchTemplate = "{0}?query={1}&limit={2}&offset={3}&fmt=json";

        private const string UserAgent = "Hqub.MusicBrainz/3.0-beta";

        #region Public services

        /// <summary>
        /// Gets the artists entity service.
        /// </summary>
        public ArtistService Artists { get; }

        /// <summary>
        /// Gets the recordings entity service.
        /// </summary>
        public RecordingService Recordings { get; }

        /// <summary>
        /// Gets the releases entity service.
        /// </summary>
        public ReleaseService Releases { get; }

        /// <summary>
        /// Gets the release-groups entity service.
        /// </summary>
        public ReleaseGroupService ReleaseGroups { get; }

        /// <summary>
        /// Gets the work entity service.
        /// </summary>
        public WorkService Work { get; }

        #endregion

        /// <summary>
        /// Gets or sets the <see cref="IRequestCache"/> implementation.
        /// </summary>
        public IRequestCache Cache { get; set; }

        private HttpClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicBrainzClient"/> class.
        /// </summary>
        public MusicBrainzClient()
            : this(ServiceBaseAddress, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicBrainzClient"/> class.
        /// </summary>
        /// <param name="baseAddress">The base address of the webservice (default = <see cref="ServiceBaseAddress"/>).</param>
        public MusicBrainzClient(string baseAddress)
            : this(baseAddress, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicBrainzClient"/> class.
        /// </summary>
        /// <param name="baseAddress">The base address of the webservice (default = <see cref="ServiceBaseAddress"/>).</param>
        /// <param name="proxy">The <see cref="IWebProxy"/> used to connect to the webservice.</param>
        public MusicBrainzClient(string baseAddress, IWebProxy proxy)
        {
            Artists = new ArtistService(this);
            Recordings = new RecordingService(this);
            Releases = new ReleaseService(this);
            ReleaseGroups = new ReleaseGroupService(this);
            Work = new WorkService(this);

            client = CreateHttpClient(baseAddress, true, proxy);
        }

        [DataContract]
        class ResponseError
        {
            [DataMember(Name = "error")]
            public string Message;
        }

        internal async Task<T> GetAsync<T>(string url)
        {
            try
            {
                var cache = Cache ?? NullCache.Default;

                var serializer = new DataContractJsonSerializer(typeof(T));

                if (await cache.TryGetCachedItem(url, out Stream stream))
                {
                    var result = (T)serializer.ReadObject(stream);

                    stream.Close();

                    return result;
                }

                using (var response = await client.GetAsync(url))
                {
                    stream = await response.Content.ReadAsStreamAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        throw CreateWebserviceException(response.StatusCode, url, stream);
                    }

                    await cache.Add(url, stream);

                    return (T)serializer.ReadObject(stream);
                }

            }
            catch
            {
                throw;
            }
        }

        private WebServiceException CreateWebserviceException(HttpStatusCode status, string url, Stream stream)
        {
            var serializer = new DataContractJsonSerializer(typeof(ResponseError));

            var error = (ResponseError)serializer.ReadObject(stream);

            return new WebServiceException(error.Message, status, url);
        }

        #region Generate urls

        /// <summary>
        /// Creates a webservice lookup template.
        /// </summary>
        internal string CreateLookupUrl(string entity, string mbid, params string[] inc)
        {
            return CreateLookupUrl(entity, mbid, string.Join("+", inc));
        }

        /// <summary>
        /// Creates a webservice lookup template.
        /// </summary>
        internal string CreateLookupUrl(string entity, string mbid, string inc)
        {
            return string.Format(LookupTemplate, entity, mbid, inc);
        }

        /// <summary>
        /// Creates a webservice browse template.
        /// </summary>
        internal string CreateBrowseTemplate(string entity, string relatedEntity, string mbid, int limit, int offset, params string[] inc)
        {
            return CreateBrowseTemplate(entity, relatedEntity, mbid, limit, offset, string.Join("+", inc));
        }

        /// <summary>
        /// Creates a webservice browse template.
        /// </summary>
        internal string CreateBrowseTemplate(string entity, string relatedEntity, string mbid, string type, string status,
            int limit, int offset, params string[] inc)
        {
            var url = CreateBrowseTemplate(entity, relatedEntity, mbid, limit, offset, string.Join("+", inc));

            if (!ValidateBrowseParam(Resources.Constants.BrowseStatus, status))
            {
                throw new ArgumentException(string.Format(Resources.Messages.InvalidQueryValue, status, "status"));
            }

            if (!ValidateBrowseParam(Resources.Constants.BrowseType, type))
            {
                throw new ArgumentException(string.Format(Resources.Messages.InvalidQueryValue, type, "type"));
            }

            if (!string.IsNullOrEmpty(type))
            {
                url += "&type=" + type;
            }

            if (!string.IsNullOrEmpty(status))
            {
                url += "&status=" + status;
            }

            return url;
        }

        /// <summary>
        /// Creates a webservice browse template.
        /// </summary>
        internal string CreateBrowseTemplate(string entity, string relatedEntity, string mbid, int limit, int offset, string inc)
        {
            return string.Format(BrowseTemplate, entity, relatedEntity, mbid, limit, offset, inc);
        }

        /// <summary>
        /// Creates a webservice search template.
        /// </summary>
        internal string CreateSearchTemplate(string entity, string query, int limit, int offset)
        {
            query = Uri.EscapeUriString(query);

            return string.Format(SearchTemplate, entity, query, limit, offset);
        }

        private bool ValidateBrowseParam(string availableParams, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return true; // Irgnore, if no value specified.
            }

            if (value.IndexOf('|') > 0)
            {
                return value.Split('|').All(s => availableParams.IndexOf("+" + s + "+") >= 0);
            }

            return availableParams.IndexOf("+" + value + "+") >= 0;
        }

        #endregion

        // TODO: should HttpClient be re-used?

        // https://medium.com/@nuno.caneco/c-httpclient-should-not-be-disposed-or-should-it-45d2a8f568bc

        private HttpClient CreateHttpClient(string baseAddress, bool automaticDecompression, IWebProxy proxy)
        {
            var handler = new HttpClientHandler();

            if (proxy != null)
            {
                handler.Proxy = proxy;
                handler.UseProxy = true;
            }

            if (automaticDecompression)
            {
                handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            }

            var client = new HttpClient(handler);

            client.DefaultRequestHeaders.Add("User-Agent", UserAgent);
            client.BaseAddress = new Uri(baseAddress);

            return client;
        }
    }
}
