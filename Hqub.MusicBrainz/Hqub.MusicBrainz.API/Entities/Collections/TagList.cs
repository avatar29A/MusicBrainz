using System.Collections.Generic;
using System.Xml.Serialization;

namespace Hqub.MusicBrainz.API.Entities.Collections
{
    [XmlRoot("tag-list", Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    public class TagList : BaseList
    {
        [XmlElement("tag")]
        public List<Tag> Items { get; set; }
    }
}
