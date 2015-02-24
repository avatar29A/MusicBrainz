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
    public class RecordingList : BaseList
    {
        [XmlElement("recording")]
        public List<Recording> Items { get; set; }
    }
}

/*
<element name="recording-list">
    <ref name="def_list-attributes"/>
    <zeroOrMore>
        <ref name="def_recording-element"/>
    </zeroOrMore>
</element>
*/