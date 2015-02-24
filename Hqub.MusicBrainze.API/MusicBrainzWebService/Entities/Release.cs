using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hqub.MusicBrainz.API.Entities
{
    [XmlType(Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    [XmlRoot("release", Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    public class Release : Entity
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }

        [XmlElement("quality")]
        public string Quality { get; set; }

        [XmlElement("text-representation")]
        public TextRepresentation TextRepresentation { get; set; }

        [XmlElement("date")]
        public string Date { get; set; }

        [XmlElement("country")]
        public string Country { get; set; }

        [XmlElement("barcode")]
        public string Barcode { get; set; }

        [XmlElement("release-group")]
        public ReleaseGroup ReleaseGroup { get; set; }

        [XmlElement("cover-art-archive")]
        public CoverArtArchive CoverArtArchive { get; set; }

        #region Subqueries

        [XmlArray("artist-credit")]
        [XmlArrayItem("name-credit")]
        public List<NameCredit> Credits { get; set; }

        [XmlArray("label-info-list")]
        [XmlArrayItem("label-info")]
        public List<LabelInfo> Labels { get; set; }

        [XmlElement("medium-list")]
        public Collections.MediumList MediumList { get; set; }

        #endregion

        #region Static Methods

        public const string ReleaseConst = "release";

        public static async Task<Release> Get(string id, params string[] inc)
        {
            return await Get<Release>(id, WebRequestHelper.CreateLookupUrl(ReleaseConst, id, CreateIncludeQuery(inc)));
        }

        public static async Task<Collections.ReleaseList> Search(string query, int limit = 25, int offset = 0, params string[] inc)
        {
            return (await Search<Metadata.ReleaseMetadataWrapper>(ReleaseConst, query, limit, offset, inc)).Collection;
        }

        #endregion
    }

    [XmlType(Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    [XmlRoot("text-representation", Namespace = "http://musicbrainz.org/ns/mmd-2.0#")]
    public class TextRepresentation
    {
        [XmlElement("language")]
        public string Language { get; set; }

        [XmlElement("Latn")]
        public string Script { get; set; }
    }
}
