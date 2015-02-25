using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hqub.MusicBrainz.API.Entities.Collections
{
    [XmlType(Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    [XmlRoot("medium-list", Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    public class MediumList : BaseList
    {
        [XmlElement(ElementName = "track-count")]
        public int TrackCount { get; set; }

        [XmlElement("medium")]
        public List<Medium> Items { get; set; }
    }
}
