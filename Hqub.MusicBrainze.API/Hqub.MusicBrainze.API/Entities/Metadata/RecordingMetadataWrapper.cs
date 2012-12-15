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
    public class RecordingMetadataWrapper : MetadataWrapper
    {
        public RecordingMetadataWrapper()
        {
            Collection = new RecordingList();
        }

        [XmlArray("recording-list")]
        [XmlArrayItem("recording")]
        public RecordingList Collection { get; set; }

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
        }
    }
}
