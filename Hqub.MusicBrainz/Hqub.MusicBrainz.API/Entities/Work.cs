using System.Xml.Serialization;

namespace Hqub.MusicBrainz.API.Entities
{
    [XmlRoot("work", Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    public class Work : Entity
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("iswc")]
        public string ISWC { get; set; }
    }
}
