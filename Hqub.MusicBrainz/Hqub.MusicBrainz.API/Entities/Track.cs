
namespace Hqub.MusicBrainz.API.Entities
{
    using System.Runtime.Serialization;

    [DataContract(Name = "track")]
    public class Track
    {
        /// <summary>
        /// Gets or sets the MusicBrainz id.
        /// </summary>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        [DataMember(Name = "number")]
        public int Number { get; set; }

        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        [DataMember(Name = "length")]
        public int Length { get; set; }

        /// <summary>
        /// Gets or sets the recording.
        /// </summary>
        [DataMember(Name = "recording")]
        public Recording Recording { get; set; }

    }
}
