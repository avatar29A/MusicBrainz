using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Hqub.MusicBrainze.API.Entities
{
    [XmlType(Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    [XmlRoot("cover-art-archive", Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    public class CoverArtArchive : Entity
    {
        [XmlElement("artwork")]
        public bool Artwork { get; set; }

        [XmlElement("count")]
        public int Count { get; set; }

        [XmlElement("front")]
        public bool Front { get; set; }

        [XmlElement("back")]
        public bool Back { get; set; }
    }
}
