using System.Xml.Serialization;

namespace Hqub.MusicBrainz.API.Entities
{
    [XmlRoot("rating", Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    public class Rating
    {
        [XmlAttribute("votes-count")]
        public int VotesCount { get; set; }

        [XmlText]
        public double Value { get; set; }
    }
}
