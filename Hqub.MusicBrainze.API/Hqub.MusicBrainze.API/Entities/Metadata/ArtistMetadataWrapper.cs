using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using Hqub.MusicBrainze.API.Entities.Collections;

namespace Hqub.MusicBrainze.API.Entities.Metadata
{
    [XmlType(Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    [XmlRoot("metadata", Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    public class ArtistMetadataWrapper : MetadataWrapper
    {
        public ArtistMetadataWrapper()
        {
            Collection = new ArtistList();
        }

        [XmlArray("artist-list")]
        [XmlArrayItem("artist")]
        public ArtistList Collection { get; set; }

        public override void SetSchema(XElement schema)
        {
            base.SetSchema(schema);

            var firstNode = schema.Elements().FirstOrDefault();

            if (firstNode == null)
                return;

            var countAttribute = firstNode.Attribute("count");
            if(countAttribute != null)
            {
                int count;
                Collection.QueryCount = int.TryParse(countAttribute.Value, out count) ? count : 0;
            }

            var offsetAttribute = firstNode.Attribute("offset");
            if(offsetAttribute != null)
            {
                int offset;
                Collection.QueryOffset = int.TryParse(offsetAttribute.Value, out offset) ? offset : 0;
            }
        }
    }
}
