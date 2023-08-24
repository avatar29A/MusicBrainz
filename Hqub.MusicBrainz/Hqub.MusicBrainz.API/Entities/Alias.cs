
namespace Hqub.MusicBrainz.API.Entities
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    ///
    /// Aliases are alternative names for all types of MusicBrainz entities.
    /// </summary>
    /// <see href="https://musicbrainz.org/doc/Aliases"/>
    [DataContract(Name = "alias")]
    public class Alias
    {
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
        /// Gets or sets the type.
        /// </summary>
        [DataMember(Name = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the locale.
        /// </summary>
        [DataMember(Name = "locale")]
        public string Locale { get; set; }

        /// <summary>
        /// Gets or sets the begin date.
        /// </summary>
        [DataMember(Name = "begin")]
        public string Begin { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        [DataMember(Name = "end")]
        public string End { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the alias ended or not.
        /// </summary>
        [DataMember(Name = "ended")]
        public bool? Ended { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this is a primary alias.
        /// </summary>
        [DataMember(Name = "primary")]
        public bool? Primary { get; set; }
    }
}
