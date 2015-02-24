using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hqub.MusicBrainz.API.Entities.Collections
{
    [XmlType(Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    [XmlRoot("work-list", Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    public class WorkList : BaseList
    {
        [XmlElement("work")]
        public List<Work> Items { get; set; }
    }
}

/*
<element name="work-list">
    <ref name="def_list-attributes"/>
    <zeroOrMore>
        <ref name="def_work-element"/>
    </zeroOrMore>
</element>
*/