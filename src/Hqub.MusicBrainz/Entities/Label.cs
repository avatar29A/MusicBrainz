
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
        /// Gets or sets the disambiguation.
        /// </summary>
        [DataMember(Name = "disambiguation")]
        public string Disambiguation { get; set; }

        #endregion

        #region Subqueries

        /// <summary>
        /// Gets or sets a list of aliases associated to this label.
        /// </summary>
        /// <example>
        /// var e = await Label.GetAsync(mbid, "aliases");
        /// </example>
        [DataMember(Name = "aliases")]
        public List<Alias> Aliases { get; set; }

        #endregion
    }
}
