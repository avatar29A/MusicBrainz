using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hqub.MusicBrainz.API.Entities
{
    [XmlRoot("medium")]
    public class Medium
    {
        [XmlElement("format")]
        public string Format { get; set; }

        [XmlElement("disc-list")]
        public DiskList Disks { get; set; }

        [XmlElement("track-list")]
        public Collections.TrackList Tracks { get; set; }
    }

    [XmlRoot("disk-list")]
    public class DiskList 
    {
        [XmlAttribute("count")]
        public int Count { get; set; }
    }
}
/*
 * <define name="def_medium">
        <element name="medium">
            <optional>
                <element name="title">
                    <text/>
                </element>
            </optional>
            <optional>
                <element name="position">
                    <data type="nonNegativeInteger"/>
                </element>
            </optional>
            <optional>
                <element name="format">
                    <text/>
                </element>
            </optional>
            <optional>
                <ref name="def_disc-list"/>
            </optional>
            <optional>
                <ref name="def_pregap-track"/>
            </optional>
            <ref name="def_track-list"/>
            <optional>
                <ref name="def_data-track-list"/>
            </optional>
        </element>
    </define>
 * 
 * <define name="def_disc-list">
        <element name="disc-list">
            <ref name="def_list-attributes"/>
            <zeroOrMore>
                <ref name="def_disc-element"/>
            </zeroOrMore>
        </element>
    </define>
 */
