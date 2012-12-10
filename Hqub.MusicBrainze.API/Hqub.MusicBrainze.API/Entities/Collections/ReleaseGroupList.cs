using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hqub.MusicBrainze.API.Entities.Collections
{
    [XmlType(Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    [XmlRoot("recording-list", Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    public class ReleaseGroupList : List<ReleaseGroup>
    {
        [XmlAttribute("count")]
        public int QueryCount { get; set; }
    }
}
