
namespace Hqub.MusicBrainz.API.Entities
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract(Name = "medium")]
    public class Medium
    {
        /// <summary>
        /// Gets or sets the number of tracks.
        /// </summary>
        [DataMember(Name = "track-count")]
        public int TrackCount { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        [DataMember(Name = "position")]
        public int Position { get; set; }

        /// Gets or sets the format.
        /// </summary>
        [DataMember(Name = "format")]
        public string Format { get; set; }

        /// <summary>
        /// Gets or sets the disc list.
        /// </summary>
        [DataMember(Name = "discs")]
        public List<Disc> Discs { get; set; }

        /// <summary>
        /// Gets or sets the track list.
        /// </summary>
        [DataMember(Name = "tracks")]
        public List<Track> Tracks { get; set; }
    }
}
