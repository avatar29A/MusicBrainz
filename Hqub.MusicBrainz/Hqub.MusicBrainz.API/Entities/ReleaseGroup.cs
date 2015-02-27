using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Hqub.MusicBrainz.API.Entities.Collections;
using Hqub.MusicBrainz.API.Entities.Metadata;

namespace Hqub.MusicBrainz.API.Entities
{
    [XmlRoot("release-group", Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    public class ReleaseGroup : Entity
    {
        public const string EntityName = "release-group";

        #region Properties

        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("first-release-date")]
        public string FirstReleaseDate { get; set; }

        [XmlElement("primary-type")]
        public string PrimaryType { get; set; }

        [XmlElement("rating")]
        public Rating Rating { get; set; }

        [XmlElement("tag-list")]
        public TagList Tags { get; set; }

        #endregion

        #region Subqueries

        [XmlArray("artist-credit")]
        [XmlArrayItem("name-credit")]
        public List<NameCredit> Credits { get; set; }

        [XmlArray("release-list")]
        [XmlArrayItem("release")]
        public List<Release> Releases { get; set; }

        #endregion

        #region Static Methods

        [Obsolete("Use GetAsync() method.")]
        public static ReleaseGroup Get(string id, params string[] inc)
        {
            return GetAsync<ReleaseGroup>(EntityName, id, inc).Result;
        }

        [Obsolete("Use SearchAsync() method.")]
        public static ReleaseGroupList Search(string query, int limit = 25, int offset = 0)
        {
            return SearchAsync<ReleaseGroupMetadata>(EntityName,
                query, limit, offset).Result.Collection;
        }

        public async static Task<ReleaseGroup> GetAsync(string id, params string[] inc)
        {
            return await GetAsync<ReleaseGroup>(EntityName, id, inc);
        }

        public async static Task<ReleaseGroupList> SearchAsync(string query, int limit = 25, int offset = 0)
        {
            return (await SearchAsync<ReleaseGroupMetadata>(EntityName,
                query, limit, offset)).Collection;
        }

        public async static Task<ReleaseGroupList> SearchAsync(QueryParameters<ReleaseGroup> query, int limit = 25, int offset = 0)
        {
            return (await SearchAsync<ReleaseGroupMetadata>(EntityName,
                query.ToString(), limit, offset)).Collection;
        }

        public async static Task<ReleaseGroupList> BrowseAsync(string entity, string id, int limit = 25, int offset = 0, params  string[] inc)
        {
            return (await BrowseAsync<ReleaseGroupMetadata>(EntityName, entity, id,
                limit, offset, inc)).Collection;
        }

        // TODO: add string parameter 'type' and 'status' to browse methods
        // see http://musicbrainz.org/doc/Development/XML_Web_Service/Version_2#Release_Type_and_Status

        #endregion
    }
}
