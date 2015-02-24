using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MusicBrainzWebService.Entities.Collections
{
    [XmlType(Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    [XmlRoot("recording-list", Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    public class ReleaseGroupList : BaseList
    {
        [XmlElement("release-group")]
        public List<ReleaseGroup> Items { get; set; }
    }
}

/*
<element name="release-group-list">
    <ref name="def_list-attributes"/>
    <zeroOrMore>
        <ref name="def_release-group-element"/>
    </zeroOrMore>
</element>
*/