using System.Xml.Serialization;

namespace MusicBrainzWebService.Entities
{
    [XmlRoot("name-credit")]
    public class NameCredit
    {
        [XmlAttribute("joinphrase")]
        public string JoinPhrase { get; set; }

        [XmlElement("artist")]
        public Artist Artist { get; set; }
    }
}