
namespace Hqub.MusicBrainz.Entities
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Label information.
    /// </summary>
    /// <see href="https://musicbrainz.org/doc/Label"/>
    [DataContract(Name = "label")]
    public class Label
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
        /// Gets or sets the area.
        /// </summary>
        [DataMember(Name = "area")]
        public Area Area { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        [DataMember(Name = "country")]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the label code.
        /// </summary>
        [DataMember(Name = "label-code")]
        public string LabelCode { get; set; }

        /// <summary>
        /// Gets or sets the life-span.
        /// </summary>
        [DataMember(Name = "life-span")]
        public LifeSpan LifeSpan { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        [DataMember(Name = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the disambiguation.
        /// </summary>
        [DataMember(Name = "disambiguation")]
        public string Disambiguation { get; set; }

        #endregion

        #region Sub-queries

        /// <summary>
        /// Gets or sets a list of aliases associated to this label.
        /// </summary>
        /// <example>
        /// var e = await client.Labels.GetAsync(mbid, "aliases");
        /// </example>
        [DataMember(Name = "aliases")]
        public List<Alias> Aliases { get; set; }

        /// <summary>
        /// Gets or sets a list of releases associated to this release-group.
        /// </summary>
        /// <example>
        /// var e = await client.Labels.GetAsync(mbid, "releases");
        /// </example>
        [DataMember(Name = "releases")]
        public List<Release> Releases { get; set; }

        /// <summary>
        /// Gets or sets a list of tags associated to this release-group.
        /// </summary>
        /// <example>
        /// var e = await client.Labels.GetAsync(mbid, "tags");
        /// </example>
        [DataMember(Name = "tags")]
        public List<Tag> Tags { get; set; }

        /// <summary>
        /// Gets or sets a list of genres associated to this release-group.
        /// </summary>
        /// <example>
        /// var e = await client.Labels.GetAsync(mbid, "genres");
        /// </example>
        [DataMember(Name = "genres")]
        public List<Genre> Genres { get; set; }

        /// <summary>
        /// Gets or sets a list of relations associated to this artist.
        /// </summary>
        /// <example>
        /// var e = await client.Labels.GetAsync(mbid, "url-rels");
        /// </example>
        [DataMember(Name = "relations")]
        public List<Relation> Relations { get; set; }

        #endregion
    }
}
