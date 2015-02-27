using System.Xml.Serialization;

namespace Hqub.MusicBrainz.API.Entities
{
    [XmlRoot("label-info")]
    public class LabelInfo
    {
        [XmlElement("catalog-number")]
        public string CatalogNumber { get; set; }

        [XmlElement("label")]
        public Label Label { get; set; }
    }
}
