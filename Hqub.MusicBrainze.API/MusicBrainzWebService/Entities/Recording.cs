using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Hqub.MusicBrainz.API.Entities.Collections;
using System.Threading.Tasks;

namespace Hqub.MusicBrainz.API.Entities
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

        public const string RecordingConst = "recording";

		public static async Task<Recording> Get(string id, params string[] inc)
		{
            string url = WebRequestHelper.CreateLookupUrl(RecordingConst, id, CreateIncludeQuery(inc));
            Recording recording = await Get<Recording>(id, url);
            return recording;
		}

		public static async Task<RecordingList> Search(string query, int limit = 25, int offset = 0, params string[] inc)
		{
			return (await Search<Metadata.RecordingMetadataWrapper>(RecordingConst, query, limit, offset, inc)).Collection;
		}

        public static async Task<RecordingList> Browse(string relatedEntity, string value, int limit=25, int offset=0, params  string[] inc)
        {
            return (await Browse<Metadata.RecordingMetadataWrapper>(RecordingConst, relatedEntity, value, limit, offset, inc)).Collection;
        }

		#endregion

		#region Include

        [XmlElement("tag-list")]
        public TagList Tags { get; set; }

        [XmlArray("artist-credit")]
        [XmlArrayItem("name-credit")]
        public List<NameCredit> Credits { get; set; }

        [XmlElement("release-list")]
        public ReleaseList Releases { get; set; }

        #endregion
    }
}

/*
<element name="recording">
    <optional>
        <attribute name="id">
            <data type="anyURI"/>
        </attribute>
    </optional>
    <ref name="def_recording-attribute_extension"/>

    <optional>
        <element name="title">
            <text/>
        </element>
    </optional>
    <optional>
        <element name="length">
            <data type="nonNegativeInteger"/>
        </element>
    </optional>
    <optional>
        <ref name="def_annotation" />
    </optional>
    <optional>
        <element name="disambiguation">
            <text/>
        </element>
    </optional>
    <optional>
        <ref name="def_video"/>
    </optional>
    <optional>
        <ref name="def_artist-credit"/>
    </optional>


    <optional>
        <ref name="def_release-list"/>
    </optional>
    <optional>
        <ref name="def_puid-list"/>
    </optional>
    <optional>
        <ref name="def_isrc-list"/>
    </optional>
    <zeroOrMore>
        <ref name="def_relation-list"/>
    </zeroOrMore>

    <optional>
        <ref name="def_tag-list"/>
    </optional>
    <optional>
        <ref name="def_user-tag-list"/>
    </optional>
    <optional>
        <ref name="def_rating"/>
    </optional>
    <optional>
        <ref name="def_user-rating"/>
    </optional>

    <ref name="def_recording-element_extension"/>
</element>
*/