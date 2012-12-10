using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Hqub.MusicBrainze.API.Entities
{
    [XmlType(Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    [XmlRoot("release", Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    public class Release : Entity
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("Official")]
        public string Status { get; set; }

        [XmlElement("quality")]
        public string Quality { get; set; }

        [XmlElement("text-representation")]
        public TextRepresentation TextRepresentation { get; set; }

        [XmlElement("date")]
        public string Date { get; set; }

        [XmlElement("country")]
        public string Country { get; set; }
    }

    [XmlType(Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    [XmlRoot("text-representation", Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    public class TextRepresentation
    {
        [XmlElement("language")]
        public string Language { get; set; }

        [XmlElement("Latn")]
        public string Script { get; set; }
    }
}
