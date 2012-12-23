using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Hqub.MusicBrainze.API.Entities.Collections;

namespace Hqub.MusicBrainze.API.Entities
{
    [XmlType(Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    [XmlRoot("recording", Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    public class Recording : Entity
    {
        #region Property

        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("length")]
        public int Length { get; set; }

        [XmlElement("disambiguation")]
        public string Disambiguation { get; set; }

        #endregion

		#region Static methods

		public static Recording Get(string id, params string[] inc)
		{
			return Get<Recording>(id, WebRequestHelper.CreatLookupUrl(Localization.Constants.Recording, id, CreateIncludeQuery(inc)));
		}

		public static RecordingList Search(string query, int limit = 25, int offset = 0, params string[] inc)
		{
			return
				Search<Metadata.RecordingMetadataWrapper>(Localization.Constants.Recording, query, limit, offset, inc).Collection;
		}

        public static RecordingList Browse(string relatedEntity, string value, int limit=25, int offset=0, params  string[] inc)
        {
            return
                Browse<Metadata.RecordingMetadataWrapper>(Localization.Constants.Recording, relatedEntity, value, limit,
                                                          offset, inc
                    ).Collection;
        }

		#endregion

		#region Include

		[XmlArray("tag-list")]
        [XmlArrayItem("tag")]
        public TagList Tags { get; set; }

        [XmlArray("artist-credit")]
        [XmlArrayItem("name-credit")]
        public List<NameCredit> Credits { get; set; }

        [XmlArray("release-list")]
        [XmlArrayItem("release")]
        public ReleaseList Releases { get; set; }

        #endregion
    }
}
