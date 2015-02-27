using System;
using System.Xml.Serialization;

namespace Hqub.MusicBrainz.API.Entities
{
    [XmlRoot("cover-art-archive", Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    public class CoverArtArchive : Entity
    {
        [XmlElement("artwork")]
        public bool Artwork { get; set; }

        [XmlElement("count")]
        public int Count { get; set; }

        [XmlElement("front")]
        public bool Front { get; set; }

        [XmlElement("back")]
        public bool Back { get; set; }

        public static Uri GetCoverArtUri(string releaseId)
        {
            string url = "http://coverartarchive.org/release/" + releaseId + "/front-250.jpg";
            return new Uri(url, UriKind.RelativeOrAbsolute);
        }
    }
}
