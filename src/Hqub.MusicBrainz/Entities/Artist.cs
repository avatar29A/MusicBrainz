﻿
namespace Hqub.MusicBrainz.Entities
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// An artist is generally a musician (or musician persona), group of musicians
    /// or other music professional (like a producer or engineer).
    /// </summary>
    /// <see href="https://musicbrainz.org/doc/Artist"/>
    [DataContract(Name = "artist")]
    public class Artist : IEntity
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
        /// Gets or sets the type.
        /// </summary>
        [DataMember(Name = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the sort name.
        /// </summary>
        [DataMember(Name = "sort-name")]
        public string SortName { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        [DataMember(Name = "gender")]
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the life-span.
        /// </summary>
        [DataMember(Name = "life-span")]
        public LifeSpan LifeSpan { get; set; }

        /// <summary>
        /// Gets or sets the area.
        /// </summary>
        [DataMember(Name = "area")]
        public Area Area { get; set; }

        /// <summary>
        /// Gets or sets the begin area.
        /// </summary>
        [DataMember(Name = "begin-area")]
        public Area BeginArea { get; set; }

        /// <summary>
        /// Gets or sets the end area.
        /// </summary>
        [DataMember(Name = "end-area")]
        public Area EndArea { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        [DataMember(Name = "country")]
        public string Country { get; set; }

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
        /// Gets or sets a list of recordings associated to this artist.
        /// </summary>
        /// <example>
        /// var e = await client.Artists.GetAsync(mbid, "recordings");
        /// </example>
        [DataMember(Name = "recordings")]
        public List<Recording> Recordings { get; set; }

        /// <summary>
        /// Gets or sets a list of release-groups associated to this artist.
        /// </summary>
        /// <example>
        /// var e = await client.Artists.GetAsync(mbid, "release-groups");
        /// </example>
        [DataMember(Name = "release-groups")]
        public List<ReleaseGroup> ReleaseGroups { get; set; }

        /// <summary>
        /// Gets or sets a list of releases associated to this artist.
        /// </summary>
        /// <example>
        /// var e = await client.Artists.GetAsync(mbid, "releases");
        /// </example>
        [DataMember(Name = "releases")]
        public List<Release> Releases { get; set; }

        /// <summary>
        /// Gets or sets a list of works associated to this artist.
        /// </summary>
        /// <example>
        /// var e = await client.Artists.GetAsync(mbid, "works");
        /// </example>
        [DataMember(Name = "works")]
        public List<Work> Works { get; set; }

        /// <summary>
        /// Gets or sets a list of tags associated to this artist.
        /// </summary>
        /// <example>
        /// var e = await client.Artists.GetAsync(mbid, "tags");
        /// </example>
        [DataMember(Name = "tags")]
        public List<Tag> Tags { get; set; }

        /// <summary>
        /// Gets or sets a list of genres associated to this artist.
        /// </summary>
        /// <example>
        /// var e = await client.Artists.GetAsync(mbid, "genres");
        /// </example>
        [DataMember(Name = "genres")]
        public List<Genre> Genres { get; set; }

        /// <summary>
        /// Gets or sets a list of relations associated to this artist.
        /// </summary>
        /// <example>
        /// var e = await client.Artists.GetAsync(mbid, "url-rels", "artist-rels");
        /// </example>
        [DataMember(Name = "relations")]
        public List<Relation> Relations { get; set; }

        /// <summary>
        /// Gets or sets a list of aliases associated to this artist.
        /// </summary>
        /// <example>
        /// var e = await client.Artists.GetAsync(mbid, "aliases");
        /// </example>
        [DataMember(Name = "aliases")]
        public List<Alias> Aliases { get; set; }

        #endregion
    }
}
