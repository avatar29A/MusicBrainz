﻿
namespace Hqub.MusicBrainz.Entities
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The Tag entity.
    /// </summary>
    [DataContract(Name = "tag")]
    public class Tag
    {
        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        [DataMember(Name = "count")]
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}
