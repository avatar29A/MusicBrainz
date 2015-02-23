using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Hqub.MusicBrainz.API.Entities.Collections;
using Hqub.MusicBrainz.API.Entities.Metadata;

namespace Hqub.MusicBrainz.API.Entities
{
    [XmlType(Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    [XmlRoot("release", Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    public class Release : Entity
    {
        public const string EntityName = "release";

        #region Properties

        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }

        [XmlElement("quality")]
        public string Quality { get; set; }

        [XmlElement("text-representation")]
        public TextRepresentation TextRepresentation { get; set; }

        [XmlElement("date")]
        public string Date { get; set; }

        [XmlElement("country")]
        public string Country { get; set; }

        [XmlElement("barcode")]
        public string Barcode { get; set; }

        [XmlElement("release-group")]
        public ReleaseGroup ReleaseGroup { get; set; }

        [XmlElement("cover-art-archive")]
        public CoverArtArchive CoverArtArchive { get; set; }

        #endregion

        #region Subqueries

        [XmlArray("artist-credit")]
        [XmlArrayItem("name-credit")]
        public List<NameCredit> Credits { get; set; }

        [XmlArray("label-info-list")]
        [XmlArrayItem("label-info")]
        public List<LabelInfo> Labels { get; set; }

        [XmlArray("medium-list")]
        [XmlArrayItem("medium")]
        public Collections.MediumList MediumList { get; set; }

        #endregion

        #region Static Methods

        [Obsolete("Use GetAsync() method.")]
        public static Release Get(string id, params string[] inc)
        {
            return GetAsync<Release>(EntityName, id, inc).Result;
        }

        [Obsolete("Use SearchAsync() method.")]
        public static ReleaseList Search(string query, int limit = 25, int offset = 0)
        {
            return SearchAsync<ReleaseMetadataWrapper>(EntityName,
                query, limit, offset).Result.Collection;
        }

        public async static Task<Release> GetAsync(string id, params string[] inc)
        {
            return await GetAsync<Release>(EntityName, id, inc);
        }

        public async static Task<ReleaseList> SearchAsync(string query, int limit = 25, int offset = 0)
        {
            return (await SearchAsync<ReleaseMetadataWrapper>(EntityName,
                query, limit, offset)).Collection;
        }

        public async static Task<ReleaseList> SearchAsync(QueryParameters<Release> query, int limit = 25, int offset = 0)
        {
            return (await SearchAsync<ReleaseMetadataWrapper>(EntityName,
                query.ToString(), limit, offset)).Collection;
        }

        public static async Task<ReleaseList> BrowseAsync(string relatedEntity, string value, int limit = 25,
            int offset = 0, params string[] inc)
        {
            return (await BrowseAsync<ReleaseMetadataWrapper>(EntityName,
                relatedEntity, value, limit, offset, inc)).Collection;
        }

        #endregion
    }

    [XmlType(Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    [XmlRoot("text-representation", Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    public class TextRepresentation
    {
        [XmlElement("language")]
        public string Language { get; set; }

        [XmlElement("Latn")]
        public string Script { get; set; }
    }
}
