using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Hqub.MusicBrainz.API.Entities.Collections;
using System.Threading.Tasks;

namespace Hqub.MusicBrainz.API.Entities
{
    [XmlType(Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    [XmlRoot("recording", Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    public class Recording : Entity
    {
        public const string EntityName = "recording";

        #region Property

        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("length")]
        public int Length { get; set; }

        [XmlElement("disambiguation")]
        public string Disambiguation { get; set; }

        #endregion

        #region Static methods

        [Obsolete("Use GetAsync() method.")]
        public static Recording Get(string id, params string[] inc)
        {
            return GetAsync<Recording>(EntityName, id, inc).Result;
        }

        [Obsolete("Use SearchAsync() method.")]
        public static RecordingList Search(string query, int limit = 25, int offset = 0)
        {
            return SearchAsync<Metadata.RecordingMetadataWrapper>(EntityName,
                query, limit, offset).Result.Collection;
        }

        [Obsolete("Use BrowseAsync() method.")]
        public static RecordingList Browse(string relatedEntity, string value, int limit = 25, int offset = 0, params  string[] inc)
        {
            return BrowseAsync<Metadata.RecordingMetadataWrapper>(EntityName,
                relatedEntity, value, limit, offset, inc).Result.Collection;
        }

        public async static Task<Recording> GetAsync(string id, params string[] inc)
        {
            return await GetAsync<Recording>(EntityName, id, inc);
        }

        public async static Task<RecordingList> SearchAsync(string query, int limit = 25, int offset = 0)
        {
            return (await SearchAsync<Metadata.RecordingMetadataWrapper>(EntityName,
                query, limit, offset)).Collection;
        }

        public async static Task<RecordingList> BrowseAsync(string relatedEntity, string value, int limit = 25, int offset = 0, params  string[] inc)
        {
            return (await BrowseAsync<Metadata.RecordingMetadataWrapper>(EntityName,
                relatedEntity, value, limit, offset, inc)).Collection;
        }

        #endregion

        #region Include

        [XmlArray("tag-list")]
        [XmlArrayItem("tag")]
        public TagList Tags { get; set; }

        [XmlArray("artist-credit")]
        [XmlArrayItem("name-credit")]
        public List<NameCredit> Credits { get; set; }

        [XmlArray("release-list")]
        [XmlArrayItem("release")]
        public ReleaseList Releases { get; set; }

        #endregion
    }
}
