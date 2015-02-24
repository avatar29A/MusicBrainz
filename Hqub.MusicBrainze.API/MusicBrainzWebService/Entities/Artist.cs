using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using MusicBrainzWebService.Entities.Collections;
using System.Threading.Tasks;

namespace MusicBrainzWebService.Entities
{
    [XmlType(Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    [XmlRoot("artist", Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    public class Artist : Entity
    {
        #region Properties

        // One of these:  person, group, character or other
        [XmlAttribute("type")]
        public string ArtistType { get; set; }

        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("sort-name")]
        public string SortName { get; set; }

        // male, female or neither. Groups do not have genders. 
        [XmlAttribute("gender")]        
        public string Gender { get; set; }

        [XmlElement("life-span")]
        public LifeSpanNode LifeSpan { get; set; }

        [XmlElement("country")]
        public string Country { get; set; }

        [XmlAttribute("score", Namespace = "http://musicbrainz.org/ns/ext#-2.0")]
        public int Score { get; set; }

        [XmlElement("disambiguation")]
        public string Disambiguation { get; set; }

        [XmlElement("rating")]
        public Rating Rating { get; set; }

        #endregion

        #region Subqueries

        [XmlElement("recording-list")]
        public RecordingList Recordings { get; set; }

        [XmlElement("release-group-list")]
        public ReleaseGroupList ReleaseGroups { get; set; }

        [XmlElement("release-list")]
        public ReleaseList ReleaseLists { get; set; }

        [XmlElement("work-list")]
        public WorkList Works { get; set; }

        [XmlElement("tag-list")]
        public TagList Tags { get; set; }

        #endregion

		#region Static Methods

        public const string ArtistConst = "artist";

		public static async Task<Artist> Get(string id, params string[] inc)
		{
			return await Get<Artist>(id, WebRequestHelper.CreateLookupUrl(ArtistConst, id, CreateIncludeQuery(inc)));
		}

		public static async Task<ArtistList> Search(string query, int limit = 25, int offset = 0, params string[] inc)
		{
			return (await Search<Metadata.ArtistMetadataWrapper>(ArtistConst, query, limit, offset, inc)).Collection;
		}

		#endregion
	}

    #region Include entities

    [XmlType(Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    [XmlRoot("life-span", Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    public class LifeSpanNode
    {
        [XmlElement("begin")]
        public string Begin { get; set; }

        [XmlElement("ended")]
        public bool Ended { get; set; }
    }

    #endregion

}

/*
<element name="artist">
    <optional>
        <attribute name="id">
            <data type="anyURI"/>
        </attribute>
    </optional>
    <optional>
        <attribute name="type">
            <data type="anyURI"/>
        </attribute>
    </optional>
    <ref name="def_artist-attribute_extension"/>

    <optional>
        <element name="name">
            <text/>
        </element>
    </optional>
    <optional>
        <element name="sort-name">
            <text/>
        </element>
    </optional>
    <optional>
        <element name="gender">
            <text/>
        </element>
    </optional>
    <optional>
        <element name="country">
            <ref name="def_iso-3166-1-code"/>
        </element>
    </optional>
    <optional>
        <ref name="def_area-element"/>
    </optional>
    <optional>
        <element name="begin-area">
            <ref name="def_area-element_inner"/>
        </element>
    </optional>
    <optional>
        <element name="end-area">
            <ref name="def_area-element_inner"/>
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
        <element name="ipi">
            <ref name="def_ipi"/>
        </element>
    </optional>
    <optional>
        <ref name="def_ipi-list" />
    </optional>
    <optional>
        <element name="life-span">
            <optional>
                <element name="begin">
                    <ref name="def_incomplete-date"/>
                </element>
            </optional>
            <optional>
                <element name="end">
                    <ref name="def_incomplete-date"/>
                </element>
            </optional>
            <optional>
                <ref name="def_ended" />
            </optional>
        </element>
    </optional>

    <optional>
        <ref name="def_alias-list"/>
    </optional>
    <optional>
        <ref name="def_recording-list"/>
    </optional>
    <optional>
        <ref name="def_release-list"/>
    </optional>
    <optional>
        <ref name="def_release-group-list"/>
    </optional>
    <optional>
        <ref name="def_label-list"/>
    </optional>
    <optional>
        <ref name="def_work-list"/>
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

    <ref name="def_artist-element_extension"/>
</element>
*/