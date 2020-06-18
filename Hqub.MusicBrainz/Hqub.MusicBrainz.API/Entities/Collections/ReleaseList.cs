
namespace Hqub.MusicBrainz.API.Entities.Collections
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// List of releases returned by MusicBrainz search requests.
    /// </summary>
    [DataContract]
    public class ReleaseList : QueryResult
    {
        /// <summary>
        /// Gets or sets the list of artists.
        /// </summary>
        [DataMember(Name = "releases")]
        public List<Release> Items { get; set; }

        // NOTE: hide members of the base class to make serialization work

        /// <summary>
        /// Gets or sets the total list items count.
        /// </summary>
        [DataMember(Name = "release-count")]
        public new int Count { get; set; }

        /// <summary>
        /// Gets or sets the list offset.
        /// </summary>
        [DataMember(Name = "release-offset")]
        public new int Offset { get; set; }
    }
}
