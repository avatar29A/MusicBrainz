using System;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Hqub.MusicBrainz.API.Entities.Collections;
using Hqub.MusicBrainz.API.Entities.Metadata;

namespace Hqub.MusicBrainz.API.Entities
{
    [XmlRoot("artist", Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    public class Artist : Entity
    {
        public const string EntityName = "artist";

        #region Properties

        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("sort-name")]
        public string SortName { get; set; }

        [XmlAttribute("gender")]
        public string Gender { get; set; }

        [XmlElement("life-span")]
        public LifeSpanNode LifeSpan { get; set; }

        [XmlElement("country")]
        public string Country { get; set; }

        [XmlAttribute("score", Namespace = "http://musicbrainz.org/ns/ext#-2.0")]
        public int Score { get; set; }

        [XmlElement("disambiguation")]
        public string Disambiguation { get; set; }

        [XmlElement("rating")]
        public Rating Rating { get; set; }

        #endregion

        #region Subqueries

        [XmlElement("recording-list")]
        public RecordingList Recordings { get; set; }

        [XmlElement("release-group-list")]
        public ReleaseGroupList ReleaseGroups { get; set; }

        [XmlElement("release-list")]
        public ReleaseList ReleaseLists { get; set; }

        [XmlElement("work-list")]
        public WorkList Works { get; set; }

        [XmlElement("tag-list")]
        public TagList Tags { get; set; }

        #endregion

        #region Static Methods

        [Obsolete("Use GetAsync() method.")]
        public static Artist Get(string id, params string[] inc)
        {
            return GetAsync<Artist>(EntityName, id, inc).Result;
        }

        [Obsolete("Use SearchAsync() method.")]
        public static ArtistList Search(string query, int limit = 25, int offset = 0)
        {
            return SearchAsync<ArtistMetadata>(EntityName,
                query, limit, offset).Result.Collection;
        }

        [Obsolete("Use BrowseAsync() method.")]
        public static ArtistList Browse(string relatedEntity, string value, int limit = 25, int offset = 0, params  string[] inc)
        {
            return BrowseAsync<ArtistMetadata>(EntityName,
                relatedEntity, value, limit, offset, inc).Result.Collection;
        }

        public async static Task<Artist> GetAsync(string id, params string[] inc)
        {
            return await GetAsync<Artist>(EntityName, id, inc);
        }

        public async static Task<ArtistList> SearchAsync(string query, int limit = 25, int offset = 0)
        {
            return (await SearchAsync<ArtistMetadata>(EntityName,
                query, limit, offset)).Collection;
        }

        public async static Task<ArtistList> SearchAsync(QueryParameters<Artist> query, int limit = 25, int offset = 0)
        {
            return (await SearchAsync<ArtistMetadata>(EntityName,
                query.ToString(), limit, offset)).Collection;
        }

        public async static Task<ArtistList> BrowseAsync(string relatedEntity, string value, int limit = 25, int offset = 0, params  string[] inc)
        {
            return (await BrowseAsync<ArtistMetadata>(EntityName, relatedEntity, value,
                limit, offset, inc)).Collection;
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
