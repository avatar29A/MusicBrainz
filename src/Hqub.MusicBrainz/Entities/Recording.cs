
namespace Hqub.MusicBrainz.Entities
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// A recording is an entity in MusicBrainz which can be linked to tracks on releases. Each track must always
    /// be associated with a single recording, but a recording can be linked to any number of tracks. 
    /// </summary>
    /// <see href="https://musicbrainz.org/doc/Recording"/>
    [DataContract(Name = "recording")]
    public class Recording : IEntity
    {
        #region Properties

        /// <summary>
        /// Gets or sets the score (only available in search results).
        /// </summary>
        [DataMember(Name = "score")]
        public int Score { get; set; }

        /// <summary>
        /// Gets or sets the MusicBrainz id.
        /// </summary>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        [DataMember(Name = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        [DataMember(Name = "length")]
        public int? Length { get; set; }

        /// <summary>
        /// Gets or sets the disambiguation.
        /// </summary>
        [DataMember(Name = "disambiguation")]
        public string Disambiguation { get; set; }

        /// <summary>
        /// Gets or sets the rating.
        /// </summary>
        [DataMember(Name = "rating")]
        public Rating Rating { get; set; }

        #endregion

        #region Sub-queries

        /// <summary>
        /// Gets or sets a list of artists associated to this recording.
        /// </summary>
        /// <example>
        /// var e = await client.Recordings.GetAsync(mbid, "artists");
        /// </example>
        [DataMember(Name = "artist-credit")]
        public List<NameCredit> Credits { get; set; }

        /// <summary>
        /// Gets or sets a list of releases associated to this recording.
        /// </summary>
        /// <example>
        /// var e = await client.Recordings.GetAsync(mbid, "releases");
        /// </example>
        [DataMember(Name = "releases")]
        public List<Release> Releases { get; set; }

        /// <summary>
        /// Gets or sets a list of tags associated to this recording.
        /// </summary>
        /// <example>
        /// var e = await client.Recordings.GetAsync(mbid, "tags");
        /// </example>
        [DataMember(Name = "tags")]
        public List<Tag> Tags { get; set; }

        /// <summary>
        /// Gets or sets a list of genres associated to this recording.
        /// </summary>
        /// <example>
        /// var e = await client.Recordings.GetAsync(mbid, "genres");
        /// </example>
        [DataMember(Name = "genres")]
        public List<Genre> Genres { get; set; }

        /// <summary>
        /// Gets or sets a list of relations associated to this recording.
        /// </summary>
        /// <example>
        /// var e = await client.Recordings.GetAsync(mbid, "url-rels");
        /// </example>
        [DataMember(Name = "relations")]
        public List<Relation> Relations { get; set; }

        /// <summary>
        /// Gets or sets a list of aliases associated to this recording.
        /// </summary>
        /// <example>
        /// var e = await client.Recordings.GetAsync(mbid, "aliases");
        /// </example>
        [DataMember(Name = "aliases")]
        public List<Alias> Aliases { get; set; }

        #endregion
    }
}
