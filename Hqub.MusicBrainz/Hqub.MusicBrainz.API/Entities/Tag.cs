using System.Xml.Serialization;

namespace Hqub.MusicBrainz.API.Entities
{
    [XmlRoot("tag", Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    public class Tag : Entity
    {
        [XmlAttribute("count")]
        public int Count { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }
    }
}
