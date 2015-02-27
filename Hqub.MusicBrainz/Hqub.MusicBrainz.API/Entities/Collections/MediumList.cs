using System.Collections.Generic;
using System.Xml.Serialization;

namespace Hqub.MusicBrainz.API.Entities.Collections
{
    [XmlRoot("medium-list", Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    public class MediumList : BaseList
    {
        [XmlElement(ElementName = "track-count")]
        public int TrackCount { get; set; }

        [XmlElement("medium")]
        public List<Medium> Items { get; set; }
    }
}
