using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MusicBrainzWebService.Entities.Collections
{
    public class BaseList
    {
        [XmlAttribute("count")]
        public int QueryCount { get; set; }

        [XmlAttribute("offset")]
        public int QueryOffset { get; set; }
    }
}

/*
 * <!-- the attributes which can be used on a -list element. -->
    <define name="def_list-attributes">
        <optional>
            <attribute name="count">
                <data type="nonNegativeInteger"/>
            </attribute>
        </optional>
        <optional>
            <attribute name="offset">
                <data type="nonNegativeInteger"/>
            </attribute>
        </optional>
    </define>
*/