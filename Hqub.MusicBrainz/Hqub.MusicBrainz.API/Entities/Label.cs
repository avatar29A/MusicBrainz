using System.Xml.Serialization;

namespace Hqub.MusicBrainz.API.Entities
{
    [XmlRoot("label")]
    public class Label : Entity
    {
        /// <summary>
        /// Gets or sets the MusicBrainz id.
        /// </summary>
        [XmlAttribute("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [XmlElement("name")]
        public string Name { get; set; }
    }
}
