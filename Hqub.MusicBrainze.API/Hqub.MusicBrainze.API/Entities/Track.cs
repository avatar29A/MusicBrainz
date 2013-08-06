using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hqub.MusicBrainze.API.Entities
{
    [XmlType(Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    [XmlRoot("track", Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    public class Track : Entity
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("position")]
        public int Position { get; set; }

        // number
        // length

        [XmlElement("recording")]
        public Recording Recordring { get; set; }

    }
}
