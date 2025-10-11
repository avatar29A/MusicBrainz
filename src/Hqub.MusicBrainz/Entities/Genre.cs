
namespace Hqub.MusicBrainz.Entities
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Genre information.
    /// </summary>
    /// <see href="https://musicbrainz.org/doc/Genre"/>
    [DataContract(Name = "genre")]
    public class Genre : IEntity
    {
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
        /// Gets or sets the count.
        /// </summary>
        [DataMember(Name = "count")]
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the disambiguation.
        /// </summary>
        [DataMember(Name = "disambiguation")]
        public string Disambiguation { get; set; }
    }
}
