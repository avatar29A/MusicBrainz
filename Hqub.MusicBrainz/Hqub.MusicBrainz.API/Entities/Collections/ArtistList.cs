
namespace Hqub.MusicBrainz.API.Entities.Collections
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// List of artists returned by MusicBrainz search requests.
    /// </summary>
    [DataContract]
    public class ArtistList : QueryResult
    {
        /// <summary>
        /// Gets or sets the list of artists.
        /// </summary>
        [DataMember(Name = "artists")]
        public List<Artist> Items { get; set; }

        // NOTE: hide members of the base class to make serialization work

        /// <summary>
        /// Gets or sets the total list items count.
        /// </summary>
        [DataMember(Name = "artist-count")]
        public new int Count { get; set; }

        /// <summary>
        /// Gets or sets the list offset.
        /// </summary>
        [DataMember(Name = "artist-offset")]
        public new int Offset { get; set; }
    }
}
