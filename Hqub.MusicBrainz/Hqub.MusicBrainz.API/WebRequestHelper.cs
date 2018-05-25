
namespace Hqub.MusicBrainz.API
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Runtime.Serialization.Json;
    using System.Threading.Tasks;

    internal static class WebRequestHelper
    {
        private const string WebServiceUrl = "http://musicbrainz.org/ws/2/";
        private const string LookupTemplate = "{0}/{1}/?inc={2}";
        private const string BrowseTemplate = "{0}?{1}={2}&limit={3}&offset={4}&inc={5}";
        private const string SearchTemplate = "{0}?query={1}&limit={2}&offset={3}";

        private const string JsonFormat = "&fmt=json";

        internal async static Task<T> GetAsync<T>(string url)
        {
            try
            {
                var client = CreateHttpClient(true, Configuration.Proxy);

                var stream = await client.GetStreamAsync(url);

                if (stream == null)
                {
                    throw new NullReferenceException(Resources.Messages.EmptyStream);
                }

                var serializer = new DataContractJsonSerializer(typeof(T));

                return (T)serializer.ReadObject(stream);
            }
            catch (Exception e)
            {
                if (Configuration.GenerateCommunicationThrow)
                {
                    throw e;
                }
            }

            return default(T);
        }

        internal static string CreateIncludeQuery(string[] inc)
        {
            return string.Join("+", inc);
        }

        /// <summary>
        /// Creates a webservice lookup template.
        /// </summary>
        internal static string CreateLookupUrl(string entity, string mbid, params string[] inc)
        {
            return CreateLookupUrl(entity, mbid, CreateIncludeQuery(inc));
        }

        /// <summary>
        /// Creates a webservice lookup template.
        /// </summary>
        internal static string CreateLookupUrl(string entity, string mbid, string inc)
        {
            return string.Format("{0}{1}{2}", WebServiceUrl,
                string.Format(LookupTemplate, entity, mbid, inc), JsonFormat);
        }

        /// <summary>
        /// Creates a webservice browse template.
        /// </summary>
        internal static string CreateBrowseTemplate(string entity, string relatedEntity, string mbid, int limit, int offset, params string[] inc)
        {
            return CreateBrowseTemplate(entity, relatedEntity, mbid, limit, offset, CreateIncludeQuery(inc));
        }

        /// <summary>
        /// Creates a webservice browse template.
        /// </summary>
        internal static string CreateBrowseTemplate(string entity, string relatedEntity, string mbid, int limit, int offset, string inc)
        {
            return string.Format("{0}{1}{2}", WebServiceUrl,
                string.Format(BrowseTemplate, entity, relatedEntity, mbid, limit, offset, inc), JsonFormat);
        }

        /// <summary>
        /// Creates a webservice search template.
        /// </summary>
        internal static string CreateSearchTemplate(string entity, string query, int limit, int offset)
        {
            query = Uri.EscapeUriString(query);

            return string.Format("{0}{1}{2}", WebServiceUrl,
                string.Format(SearchTemplate, entity, query, limit, offset), JsonFormat);
        }

        private static HttpClient CreateHttpClient(bool automaticDecompression = true, IWebProxy proxy = null)
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

            client.DefaultRequestHeaders.Add("User-Agent", Configuration.UserAgent);

            return client;
        }
    }
}
