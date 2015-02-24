using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MusicBrainzWebService.Entities
{
    [XmlType(Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    [XmlRoot("track", Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    public class Track : Entity
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlElement("position")]
        public int Position { get; set; }

        // <number> is almost always same as <position>, so leaving it

        [XmlElement("length")]
        public int Length { get; set; }

        [XmlElement("recording")]
        public Recording Recording { get; set; }
    }
}

/*
<element name="track">
    <ref name="def_track-data"/>
</element>

<define name="def_track-data">
    <optional>
        <attribute name="id">
            <data type="anyURI"/>
        </attribute>
    </optional>
    <optional>
        <element name="position">
            <data type="nonNegativeInteger"/>
        </element>
    </optional>
    <optional>
        <element name="number">
            <text/>
        </element>
    </optional>
    <optional>
        <element name="title">
            <text/>
        </element>
    </optional>
    <optional>
        <element name="length">
            <data type="nonNegativeInteger"/>
        </element>
    </optional>
    <optional>
        <ref name="def_artist-credit"/>
    </optional>
    <optional>
        <ref name="def_recording-element"/>
    </optional>
</define>
*/