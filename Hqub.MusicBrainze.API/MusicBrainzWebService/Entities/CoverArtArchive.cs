using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.UI.Xaml.Media.Imaging;

namespace MusicBrainzWebService.Entities
{
    [XmlType(Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
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
