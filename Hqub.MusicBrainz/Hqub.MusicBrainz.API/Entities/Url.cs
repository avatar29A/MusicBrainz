
namespace Hqub.MusicBrainz.API.Entities
{
    using System.Runtime.Serialization;
    
    [DataContract(Name = "url")]
    public class Url
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the resource.
        /// </summary>
        [DataMember(Name = "resource")]
        public string Resource { get; set; }
    }
}
