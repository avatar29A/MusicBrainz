using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using Windows.Web.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage.Streams;
using Hqub.MusicBrainz.API;

namespace Hqub.MusicBrainz.API
{
    internal static class WebRequestHelper
    {
        public const string WebServiceUrl = "http://musicbrainz.org/ws/2/";
        public const string LookupTemplate = "{0}/{1}/?inc={2}";
        public const string BrowseTemplate = "{0}?{1}={2}&limit={3}&offset={4}&inc={5}";
        public const string SearchTemplate = "{0}?query={1}&limit={2}&offset={3}";

        internal static async Task<T> GetAsync<T>(string url, bool withoutMetadata = true) where T : Entities.Entity
        {
            MyHttpClient httpClient = new MyHttpClient(url);
            HttpResponseMessage response = await httpClient.SendRequestAsync();

            IBuffer responseBuffer = await response.Content.ReadAsBufferAsync();
            using (var stream = responseBuffer.AsStream())
            {
                var xml = XDocument.Load(stream);
                var serializer = new XmlSerializer(typeof(T));

                //Add extension namespace:
                var ns = new XmlSerializerNamespaces();
                ns.Add("ext", "http://musicbrainz.org/ns/ext#-2.0");

                //check valid xml schema:
                if (xml.Root == null || xml.Root.Name.LocalName != "metadata")
                    throw new NullReferenceException("Wrong Xml Format");

                var node = withoutMetadata ? xml.Root.Elements().FirstOrDefault() : xml.Root;

                if (node == null)
                    return default(T);

                var obj = (T)serializer.Deserialize(node.CreateReader());

                obj.SetSchema(node);

                return obj;
            }
        }

        /// <summary>
        /// Lookup url query
        /// </summary>
        internal static string CreateLookupUrl(string entity, string mbid, string inc)
        {
            return string.Format("{0}{1}", WebServiceUrl, String.Format(LookupTemplate, entity, mbid, inc));
        }

        /// <summary>
        /// Browse url query
        /// </summary>
        internal static string CreateBrowseTemplate(string entity, string relatedEntity, string mbid, int limit, int offset, string inc)
        {
            return string.Format("{0}{1}", WebServiceUrl, String.Format(BrowseTemplate, entity, relatedEntity, mbid, limit, offset, inc));
        }

        /// <summary>
        /// SearchTemplate
        /// </summary>
        internal static string CreateSearchTemplate(string entity, string query, int limit, int offset)
        {
            return string.Format("{0}{1}", WebServiceUrl, string.Format(SearchTemplate, entity, query, limit, offset));
        }
    }
}
