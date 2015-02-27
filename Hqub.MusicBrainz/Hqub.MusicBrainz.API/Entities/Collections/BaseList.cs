using System.Xml.Serialization;

namespace Hqub.MusicBrainz.API.Entities.Collections
{
    public class BaseList
    {
        [XmlAttribute("count")]
        public int QueryCount { get; set; }

        [XmlAttribute("offset")]
        public int QueryOffset { get; set; }
    }
}