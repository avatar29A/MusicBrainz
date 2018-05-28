
namespace Hqub.MusicBrainz.API.Entities
{
    using System.Runtime.Serialization;

    /// <summary>
    /// In MusicBrainz terminology, a work is a distinct intellectual or artistic creation,
    /// which can be expressed in the form of one or more audio recordings.
    /// </summary>
    /// <see href="https://musicbrainz.org/doc/Work"/>
    [DataContract(Name = "work")]
    public class Work
    {
        /// <summary>
        /// Gets or sets the MusicBrainz id.
        /// </summary>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        [DataMember(Name = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the ISW code.
        /// </summary>
        [DataMember(Name = "iswc")]
        public string ISWC { get; set; }
    }
}
