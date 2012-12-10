using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hqub.MusicBrainze.API.Entities.Collections
{
    [XmlType(Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    [XmlRoot("work-list", Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    public class WorkList : List<Work>
    {
        [XmlAttribute("count")]
        public int QueryCount { get; set; }
    }
}
