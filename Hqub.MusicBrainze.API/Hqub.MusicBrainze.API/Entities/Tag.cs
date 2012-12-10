using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Hqub.MusicBrainze.API.Entities
{
    [XmlType(Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    [XmlRoot("tag", Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    public class Tag : Entity
    {
        [XmlAttribute("count")]
        public int Count { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }
    }
}
