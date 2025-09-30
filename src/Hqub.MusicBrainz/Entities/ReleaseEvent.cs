namespace Hqub.MusicBrainz.Entities
{
    using System.Runtime.Serialization;

    /// <summary>
    /// A release event represents a date on which a release was released in a specific country/region.
    /// </summary>
    /// <see href="https://wiki.musicbrainz.org/History:Release_Event"/>
    [DataContract(Name = "release-event")]
    public class ReleaseEvent
    {
        /// <summary>
        /// Gets or sets the area where the release event occurred.
        /// </summary>
        [DataMember(Name = "area")]
        public Area Area { get; set; }

        /// <summary>
        /// Gets or sets the date of the release event.
        /// </summary>
        [DataMember(Name = "date")]
        public string Date { get; set; }
    }
}
