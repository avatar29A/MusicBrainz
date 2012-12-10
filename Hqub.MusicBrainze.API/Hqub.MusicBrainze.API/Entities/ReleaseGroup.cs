using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Hqub.MusicBrainze.API.Entities.Collections;

namespace Hqub.MusicBrainze.API.Entities
{
    [XmlType(Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    [XmlRoot("release-group", Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    public class ReleaseGroup : Entity
    {
        [XmlAttribute("type")]
        public string ReleaseGroupType { get; set; }

        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("first-release-date")]
        public string FirstReleaseDate { get; set; }

        [XmlElement("primary-type")]
        public string PrimaryType { get; set; }

        [XmlElement("rating")]
        public Rating Rating { get; set; }

        [XmlArray("tag-list")]
        [XmlArrayItem("tag")]
        public TagList Tags { get; set; }
    }
}
