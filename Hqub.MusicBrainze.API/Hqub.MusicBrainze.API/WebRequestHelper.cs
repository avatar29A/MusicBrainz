using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Hqub.MusicBrainze.API
{
    internal static class WebRequestHelper
    {
        internal static T Get<T>(string url)
        {
            try
            {
                var request = System.Net.WebRequest.Create(url);

                var response = request.GetResponse();

                using (var stream = response.GetResponseStream())
                {
                    if (stream == null)
                        throw new NullReferenceException(Localization.Messages.StreamIsEmpty);

                    var xml = XDocument.Load(stream);

                    if(xml.Root == null || xml.Root.Name.LocalName != "metadata")
                        throw new NullReferenceException(Localization.Messages.WrongXmlFormat);

                    var serialize = new XmlSerializer(typeof (T));


                    return (T)serialize.Deserialize(xml.Root.FirstNode.CreateReader());
                }
            }
            catch (Exception ex)
            {
                if (APIGlobalSettings.GenerateCommunicationThrow)
                    throw new Exception("Unknow result. " + ex.Message);

                return default(T);
            }
        }

        /// <summary>
        /// Lookup url query
        /// </summary>
        internal static string CreatLookupUrl(string entity, string mbid, string inc)
        {
            return string.Format("{0}{1}", Properties.Settings.Default.WebServiceUrl,
                                 string.Format(Properties.Settings.Default.LookupTemplate, entity, mbid, inc));
        }

        /// <summary>
        /// Browse url query
        /// </summary>
        internal static string CreateBrowseTemplate(string entity, string mbid, int limit, int offset, string inc)
        {
            return string.Format("{0}{1}", Properties.Settings.Default.WebServiceUrl,
                                 string.Format(Properties.Settings.Default.BrowseTemplate, entity, mbid, limit, offset,
                                               inc));
        }

        /// <summary>
        /// SearchTemplate
        /// </summary>
        internal static string CreateSearchTemplate(string entity, string query, int limit, int offset, string inc)
        {
            return string.Format("{0}{1}", Properties.Settings.Default.WebServiceUrl,
                                 string.Format(Properties.Settings.Default.SearchTemplate, entity, query, limit, offset, inc));
        }
    }
}
