using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hqub.MusicBrainze.API.Entities.Collections
{
    [XmlRoot("artist-list", Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    public class ArtistList : List<Artist>
    {
        [XmlAttribute("count")]
        public int QueryCount { get; set; }

        [XmlAttribute("offset")]
        public int QueryOffset { get; set; }

    }
}
