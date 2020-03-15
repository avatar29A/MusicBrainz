﻿
namespace Hqub.MusicBrainz.API
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;
    using System.Threading.Tasks;

    internal static class WebServiceHelper
    {
        [DataContract]
        class ResponseError
        {
            [DataMember(Name = "error")]
            public string Message;
        }

        private const string WebServiceUrl = "https://musicbrainz.org/ws/2/";
        private const string LookupTemplate = "{0}/{1}/?inc={2}";
        private const string BrowseTemplate = "{0}?{1}={2}&limit={3}&offset={4}&inc={5}";
        private const string SearchTemplate = "{0}?query={1}&limit={2}&offset={3}";

        private const string JsonFormat = "&fmt=json";

        internal static async Task<T> GetAsync<T>(string url)
        {
            try
            {
                var cache = Configuration.Cache ?? Cache.NullCache.Default;

                var serializer = new DataContractJsonSerializer(typeof(T));

                if (await cache.TryGetCachedItem(url, out Stream stream))
                {
                    var result = (T)serializer.ReadObject(stream);

                    stream.Close();

                    return result;
                }

                using (var client = CreateHttpClient(true, Configuration.Proxy))
                using (var response = await client.GetAsync(new Uri(url)))
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
            catch (Exception)
            {
                // TODO: remove GenerateCommunicationThrow.

                if (Configuration.GenerateCommunicationThrow)
                {
                    throw;
                }
            }

            return default(T);
        }

        private static WebServiceException CreateWebserviceException(HttpStatusCode status, string url, Stream stream)
        {
            var serializer = new DataContractJsonSerializer(typeof(ResponseError));

            var error = (ResponseError)serializer.ReadObject(stream);

            return new WebServiceException(error.Message, status, url);
        }

        #region Generate urls

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
        internal static string CreateBrowseTemplate(string entity, string relatedEntity, string mbid, string type, string status,
            int limit, int offset, params string[] inc)
        {
            var url = CreateBrowseTemplate(entity, relatedEntity, mbid, limit, offset, CreateIncludeQuery(inc));
            
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
            query = WebUtility.UrlEncode(query);

            return string.Format("{0}{1}{2}", WebServiceUrl,
                string.Format(SearchTemplate, entity, query, limit, offset), JsonFormat);
        }

        private static bool ValidateBrowseParam(string availableParams, string value)
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
