using System.Collections.Generic;
using System.Xml.Serialization;

namespace Hqub.MusicBrainz.API.Entities.Collections
{
    [XmlRoot("recording-list", Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    public class RecordingList : BaseList
    {
        [XmlElement("recording")]
        public List<Recording> Items { get; set; }
    }
}
