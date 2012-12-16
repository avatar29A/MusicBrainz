using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hqub.MusicBrainze.API.Entities.Collections
{
    [XmlType(Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    [XmlRoot("medium-list", Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    public class MediumList : List<Medium>
    {
        [XmlAttribute("count")]
        public int QueryCount { get; set; }

        [XmlElement("track-count")]
        public int TrackCount { get; set; }
    }
}
