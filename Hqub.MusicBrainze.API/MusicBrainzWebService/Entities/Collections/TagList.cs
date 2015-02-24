using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MusicBrainzWebService.Entities.Collections
{
    [XmlType(Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    [XmlRoot("tag-list", Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    public class TagList : BaseList
    {
        [XmlElement("tag")]
        public List<Tag> Items { get; set; }
    }
}

/*
<element name="tag-list">
    <ref name="def_list-attributes"/>
    <zeroOrMore>
        <ref name="def_tag"/>
    </zeroOrMore>
</element>
*/