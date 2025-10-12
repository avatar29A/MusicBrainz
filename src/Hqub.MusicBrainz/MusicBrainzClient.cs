namespace Hqub.MusicBrainz
{
    using Hqub.MusicBrainz.Cache;
    using Hqub.MusicBrainz.Entities;
    using Hqub.MusicBrainz.Services;
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// MusicBrainz client.
    /// </summary>
    public sealed class MusicBrainzClient : IDisposable
    {
        /// <summary>
        /// The default address of the MusicBrainz web service.
        /// </summary>
        public const string ServiceBaseAddress = "https://musicbrainz.org/ws/2/";

        private const string UserAgent = "Hqub.MusicBrainz/3.0 (https://github.com/avatar29A/MusicBrainz)";

        #region Public services

        /// <summary>
        /// Gets the artists entity service.
        /// </summary>
        public IArtistService Artists { get; }

        /// <summary>
        /// Gets the labels entity service.
        /// </summary>
        public ILabelService Labels { get; }

        /// <summary>
        /// Gets the recordings entity service.
        /// </summary>
        public IRecordingService Recordings { get; }

        /// <summary>
        /// Gets the releases entity service.
        /// </summary>
        public IReleaseService Releases { get; }

        /// <summary>
        /// Gets the release-groups entity service.
        /// </summary>
        public IReleaseGroupService ReleaseGroups { get; }

        /// <summary>
        /// Gets the work entity lookup service.
        /// </summary>
        public ILookupService<Work> Work { get; }

        /// <summary>
        /// Gets the area entity lookup service.
        /// </summary>
        public ILookupService<Area> Area { get; }

        #endregion

        /// <summary>
        /// Gets or sets the <see cref="IRequestCache"/> implementation.
        /// </summary>
        public IRequestCache Cache { get; set; }

        private readonly HttpClient client;

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
        /// <param name="proxy">The <see cref="IWebProxy"/> used to connect to the web service.</param>
        public MusicBrainzClient(IWebProxy proxy)
            : this(ServiceBaseAddress, proxy)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicBrainzClient"/> class.
        /// </summary>
        /// <param name="baseAddress">The base address of the web service (default = <see cref="ServiceBaseAddress"/>).</param>
        public MusicBrainzClient(string baseAddress)
            : this(baseAddress, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicBrainzClient"/> class.
        /// </summary>
        /// <param name="baseAddress">The base address of the web service (default = <see cref="ServiceBaseAddress"/>).</param>
        /// <param name="proxy">The <see cref="IWebProxy"/> used to connect to the web service.</param>
        public MusicBrainzClient(string baseAddress, IWebProxy proxy)
            : this(CreateHttpClient(new Uri(baseAddress), true, proxy))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicBrainzClient"/> class.
        /// </summary>
        /// <param name="httpClient">The <see cref="HttpClient"/> used for request to the web service.</param>
        public MusicBrainzClient(HttpClient httpClient)
        {
            var urlBuilder = new UrlBuilder(true);

            Artists = new ArtistService(this, urlBuilder);
            Labels = new LabelService(this, urlBuilder);
            Recordings = new RecordingService(this, urlBuilder);
            Releases = new ReleaseService(this, urlBuilder);
            ReleaseGroups = new ReleaseGroupService(this, urlBuilder);

            Work = new LookupService<Work>(this, urlBuilder, "work");
            Area = new LookupService<Area>(this, urlBuilder, "area");

            client = httpClient;
        }

        /// <summary>
        /// Disposes the HTTP client.
        /// </summary>
        public void Dispose()
        {
            client.Dispose();
        }

        [DataContract]
        class ResponseError
        {
            /// <summary>The error message.</summary>
            [DataMember(Name = "error")]
            public string Message;
        }

        internal async Task<T> GetAsync<T>(string url, CancellationToken ct = default)
        {
            try
            {
                var cache = Cache ?? NullCache.Default;

                var serializer = new DataContractJsonSerializer(typeof(T));

                if (await cache.TryGetCachedItem(url, out Stream stream).ConfigureAwait(false))
                {
                    var result = (T)serializer.ReadObject(stream);

                    // TODO: if de-serialization of the cache file fails, we shouldn't throw 
                    //       but delete the file and go on with calling the web service!

                    stream.Close();

                    return result;
                }

                using var response = await client.GetAsync(url, ct).ConfigureAwait(false);

                using var content = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    throw CreateWebserviceException(response.StatusCode, url, content);
                }

                await cache.Add(url, content).ConfigureAwait(false);

                return (T)serializer.ReadObject(content);
            }
            catch
            {
                throw;
            }
        }

        private WebServiceException CreateWebserviceException(HttpStatusCode status, string url, Stream stream)
        {
            var serializer = new DataContractJsonSerializer(typeof(ResponseError));

            try
            {
                var error = (ResponseError)serializer.ReadObject(stream);
                return new WebServiceException(error.Message, status, url);
            }
            catch
            {
                return new WebServiceException(status.ToString(), status, url);
            }
        }

        private static HttpClient CreateHttpClient(Uri baseAddress, bool automaticDecompression, IWebProxy proxy)
        {
#if NET5_0_OR_GREATER
            var handler = new SocketsHttpHandler();
#else
            var handler = new HttpClientHandler();
#endif

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
            client.BaseAddress = baseAddress;

            return client;
        }
    }
}
