
namespace Hqub.MusicBrainz.API.Entities
{
    using System.Runtime.Serialization;

    [DataContract(Name = "name-credit")]
    public class NameCredit
    {
        /// <summary>
        /// Gets or sets the joinphrase.
        /// </summary>
        [DataMember(Name = "joinphrase")]
        public string JoinPhrase { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the artist.
        /// </summary>
        [DataMember(Name = "artist")]
        public Artist Artist { get; set; }
    }
}