using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hqub.MusicBrainze.API.Entities.Collections
{
    [XmlType(Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    [XmlRoot("tag-list", Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    public class TagList : List<Tag>
    {

    }
}
