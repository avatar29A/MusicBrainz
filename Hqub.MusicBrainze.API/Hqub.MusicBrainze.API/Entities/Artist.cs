using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Hqub.MusicBrainze.API.Entities.Collections;

namespace Hqub.MusicBrainze.API.Entities
{
    [XmlType(Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    [XmlRoot("artist", Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    public class Artist
    {
        #region Properties

        [XmlAttribute("type")]
        public string ArtistType { get; set; }

        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("sort-name")]
        public string SortName { get; set; }

        [XmlElement("life-span")]
        public LifeSpanNode LifeSpan { get; set; }

        [XmlElement("ext:score")]
        public int Score { get; set; }

        [XmlElement("disambiguation")]
        public string Disambiguation { get; set; }

        #endregion

        #region Include

        [XmlArray("recording-list", ElementName = "recording-list")]
        [XmlArrayItem("recording")]
        public RecordingList Recordings { get; set; }

        [XmlArray("tag-list")]
        [XmlArrayItem("tag")]
        public TagList Tags { get; set; }

        #endregion

        #region Static Methods

        private static string CreateIncludeQuery(string[] inc)
        {
            //Build query for inc entiteis:
            var incBuilder = new StringBuilder();
            foreach (var entityName in inc.Where(Include.ArtistIncludeEntityHelper.Check))
            {
                incBuilder.AppendFormat("{0}+", entityName);
            }

            return incBuilder.ToString();
        }

        public static Artist Get(string id, params string[] inc)
        {
            if (id == null)
                throw new ArgumentNullException(string.Format(Localization.Messages.RequiredAttributeException, "id"));


            var incQuery = CreateIncludeQuery(inc);

            return
                WebRequestHelper.Get<Artist>(WebRequestHelper.CreatLookupUrl(Localization.Constants.Artist, id, incQuery));
        }

        public static ArtistList Search(string query, int limit=25, int offset=0, params  string[] inc)
        {
            if (query == null)
                throw new ArgumentNullException(string.Format(Localization.Messages.RequiredAttributeException, "query"));

            var result =
                WebRequestHelper.Get<Metadata.MetadataWrapper>(
                    WebRequestHelper.CreateSearchTemplate("artist", query, limit, offset,
                                                          CreateIncludeQuery(inc)), withoutMetadata: false);

            return result.Collection;
        }

        #endregion
    }

    #region Include entities

    [XmlType(Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    [XmlRoot("life-span", Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    public class LifeSpanNode
    {
        [XmlElement("begin")]
        public string Begin { get; set; }

        [XmlElement("ended")]
        public bool Ended { get; set; }
    }

    #endregion

}
