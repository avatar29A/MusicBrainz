using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hqub.MusicBrainze.API.Entities
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
