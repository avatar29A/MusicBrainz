using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hqub.MusicBrainz.API.Entities.Collections
{
    [XmlRoot("artist-list", Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    public class ArtistList : BaseList
    {
        [XmlElement("artist")]
        public List<Artist> Items { get; set; }
    }
}

/*
<element name="artist-list">
    <ref name="def_list-attributes"/>
    <zeroOrMore>
        <ref name="def_artist-element"/>
    </zeroOrMore>
</element>
*/