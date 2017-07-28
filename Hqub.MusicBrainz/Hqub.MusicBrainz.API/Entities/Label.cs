
namespace Hqub.MusicBrainz.API.Entities
{
    using System.Runtime.Serialization;

    [DataContract(Name = "label")]
    public class Label
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
    }
}
