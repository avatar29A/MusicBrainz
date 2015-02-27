using System.Collections.Generic;
using System.Xml.Serialization;

namespace Hqub.MusicBrainz.API.Entities.Collections
{
    [XmlRoot("track-list", Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    public class TrackList : BaseList
    {
        [XmlElement("track")]
        public List<Track> Items { get; set; }
    }
}
