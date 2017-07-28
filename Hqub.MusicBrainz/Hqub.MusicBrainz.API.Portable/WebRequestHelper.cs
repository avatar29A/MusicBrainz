using System;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace Hqub.MusicBrainz.API
{
    internal static class WebRequestHelper
    {
        private const string WebServiceUrl = "http://musicbrainz.org/ws/2/";
        private const string LookupTemplate = "{0}/{1}/?inc={2}";
        private const string BrowseTemplate = "{0}?{1}={2}&limit={3}&offset={4}&inc={5}";
        private const string SearchTemplate = "{0}?query={1}&limit={2}&offset={3}";

        private const string JsonFormat = "&fmt=json";


        internal static async Task<T> GetAsync<T>(string url)
        {
            var client = new MyHttpClient(url);
            HttpResponseMessage response = await client.SendRequestAsync();

            var responseBuffer = await response.Content.ReadAsStreamAsync();
            using (var stream = responseBuffer)
            {
                var serializer = new DataContractJsonSerializer(typeof(T));

                return (T)serializer.ReadObject(stream);
            }
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
    }
}
