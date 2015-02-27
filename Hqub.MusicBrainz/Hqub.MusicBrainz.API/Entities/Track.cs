using System.Xml.Serialization;

namespace Hqub.MusicBrainz.API.Entities
{
    [XmlRoot("track", Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    public class Track : Entity
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlElement("position")]
        public int Position { get; set; }

        // <number> is almost always same as <position>, so leaving it

        [XmlElement("length")]
        public int Length { get; set; }

        [XmlElement("recording")]
        public Recording Recording { get; set; }

    }
}
