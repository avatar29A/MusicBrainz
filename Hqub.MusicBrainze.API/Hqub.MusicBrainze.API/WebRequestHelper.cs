using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Hqub.MusicBrainze.API
{
    internal static class WebRequestHelper
    {
        internal async static Task<T> GetAsync<T>(string url, bool withoutMetadata = true) where T : Entities.Entity
        {
            try
            {
                var client = CreateHttpClient();

                return DeserializeStream<T>(await client.GetStreamAsync(url), withoutMetadata);
            }
            catch (Exception e)
            {
                if (APIGlobalSettings.GenerateCommunicationThrow)
                {
                    throw e;
                }
            }

            return default(T);
        }

        /// <summary>
        /// Lookup url query
        /// </summary>
        internal static string CreateLookupUrl(string entity, string mbid, string inc)
        {
            var settings = Properties.Settings.Default;

            return string.Format("{0}{1}", settings.WebServiceUrl,
                string.Format(settings.LookupTemplate, entity, mbid, inc));
        }

        /// <summary>
        /// Browse url query
        /// </summary>
        internal static string CreateBrowseTemplate(string entity, string relatedEntity, string mbid, int limit, int offset, string inc)
        {
            var settings = Properties.Settings.Default;

            return string.Format("{0}{1}", settings.WebServiceUrl,
                string.Format(settings.BrowseTemplate, entity, relatedEntity, mbid, limit, offset, inc));
        }

        /// <summary>
        /// SearchTemplate
        /// </summary>
        internal static string CreateSearchTemplate(string entity, string query, int limit, int offset, string inc)
        {
            // TODO: inc parameter not used - remove!
            var settings = Properties.Settings.Default;

            return string.Format("{0}{1}", settings.WebServiceUrl,
                string.Format(settings.SearchTemplate, entity, query, limit, offset, inc));
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

            client.DefaultRequestHeaders.Add("User-Agent", "Hqub.MusicBrainze.API/2.0");

            return client;
        }

        private static T DeserializeStream<T>(Stream stream, bool withoutMetadata) where T : Entities.Entity
        {
            if (stream == null)
            {
                throw new NullReferenceException(Localization.Messages.StreamIsEmpty);
            }

            var xml = XDocument.Load(stream);
            var serialize = new XmlSerializer(typeof(T));

            //Add extension namespace:
            var ns = new XmlSerializerNamespaces();
            ns.Add("ext", "http://musicbrainz.org/ns/ext#-2.0");

            //check valid xml schema:
            if (xml.Root == null || xml.Root.Name.LocalName != "metadata")
            {
                throw new NullReferenceException(Localization.Messages.WrongXmlFormat);
            }

            var node = withoutMetadata ? xml.Root.Elements().FirstOrDefault() : xml.Root;

            if (node == null)
            {
                return default(T);
            }

            var obj = (T)serialize.Deserialize(node.CreateReader());

            obj.SetSchema(node);

            return obj;
        }
    }
}
