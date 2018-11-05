
namespace Hqub.MusicBrainz.API.Entities
{
    using Hqub.MusicBrainz.API.Entities.Collections;
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    /// <summary>
    /// A MusicBrainz release represents the unique release (i.e. issuing) of a product on a specific
    /// date with specific release information such as the country, label, barcode and packaging.
    /// </summary>
    /// <see href="https://musicbrainz.org/doc/Release"/>
    [DataContract(Name = "release")]
    public class Release
    {
        public const string EntityName = "release";

        #region Properties

        /// <summary>
        /// Gets or sets the score (only available in search results).
        /// </summary>
        [DataMember(Name = "score")]
        public int Score { get; set; }

        /// <summary>
        /// Gets or sets the MusicBrainz id.
        /// </summary>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        [DataMember(Name = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        [DataMember(Name = "status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the quality.
        /// </summary>
        [DataMember(Name = "quality")]
        public string Quality { get; set; }

        /// <summary>
        /// Gets or sets the text-representation.
        /// </summary>
        [DataMember(Name = "text-representation")]
        public TextRepresentation TextRepresentation { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        [DataMember(Name = "date")]
        public string Date { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        [DataMember(Name = "country")]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the barcode.
        /// </summary>
        [DataMember(Name = "barcode")]
        public string Barcode { get; set; }

        /// <summary>
        /// Gets or sets the release-group.
        /// </summary>
        [DataMember(Name = "release-group")]
        public ReleaseGroup ReleaseGroup { get; set; }

        /// <summary>
        /// Gets or sets the cover-art-archive.
        /// </summary>
        [DataMember(Name = "cover-art-archive")]
        public CoverArtArchive CoverArtArchive { get; set; }

        #endregion

        #region Subqueries

        /// <summary>
        /// Gets or sets a list of artists associated to this release.
        /// </summary>
        /// <example>
        /// var e = await Release.GetAsync(mbid, "artists");
        /// </example>
        [DataMember(Name = "artist-credit")]
        public List<NameCredit> Credits { get; set; }

        /// <summary>
        /// Gets or sets a list of labels associated to this release.
        /// </summary>
        /// <example>
        /// var e = await Release.GetAsync(mbid, "labels");
        /// </example>
        [DataMember(Name = "label-info")]
        public List<LabelInfo> Labels { get; set; }

        /// <summary>
        /// Gets or sets a list of media/tracks associated to this release.
        /// </summary>
        /// <example>
        /// var e = await Release.GetAsync(mbid, "recordings");
        /// </example>
        [DataMember(Name = "media")]
        public List<Medium> Media { get; set; }

        /// <summary>
        /// Gets or sets a list of relations associated to this release.
        /// </summary>
        /// <example>
        /// var e = await Release.GetAsync(mbid, "url-rels");
        /// </example>
        [DataMember(Name = "relations")]
        public List<Relation> Relations { get; set; }

        #endregion

        #region Static Methods

        [Obsolete("Use GetAsync() method.")]
        public static Release Get(string id, params string[] inc)
        {
            return GetAsync(id, inc).Result;
        }

        [Obsolete("Use SearchAsync() method.")]
        public static ReleaseList Search(string query, int limit = 25, int offset = 0)
        {
            return SearchAsync(query, limit, offset).Result;
        }

        /// <summary>
        /// Lookup a release in the MusicBrainz database.
        /// </summary>
        /// <param name="id">The release MusicBrainz id.</param>
        /// <param name="inc">A list of entities to include (subqueries).</param>
        /// <returns></returns>
        public static async Task<Release> GetAsync(string id, params string[] inc)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException(string.Format(Resources.Messages.MissingParameter, "id"));
            }

            string url = WebServiceHelper.CreateLookupUrl(EntityName, id, inc);

            return await WebServiceHelper.GetAsync<Release>(url);
        }

        /// <summary>
        /// Search for a release in the MusicBrainz database, matching the given query.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="limit">The maximum number of releases to return (default = 25).</param>
        /// <param name="offset">The offset to the releases list (enables paging, default = 0).</param>
        /// <returns></returns>
        public static async Task<ReleaseList> SearchAsync(string query, int limit = 25, int offset = 0)
        {
            if (string.IsNullOrEmpty(query))
            {
                throw new ArgumentException(string.Format(Resources.Messages.MissingParameter, "query"));
            }

            string url = WebServiceHelper.CreateSearchTemplate(EntityName, query, limit, offset);

            return await WebServiceHelper.GetAsync<ReleaseList>(url);
        }

        /// <summary>
        /// Search for a release in the MusicBrainz database, matching the given query.
        /// </summary>
        /// <param name="query">The query parameters.</param>
        /// <param name="limit">The maximum number of releases to return (default = 25).</param>
        /// <param name="offset">The offset to the releases list (enables paging, default = 0).</param>
        /// <returns></returns>
        public static async Task<ReleaseList> SearchAsync(QueryParameters<Release> query, int limit = 25, int offset = 0)
        {
            return await SearchAsync(query.ToString(), limit, offset);
        }

        /// <summary>
        /// Browse all the releases in the MusicBrainz database, which are directly linked to the entity with given id.
        /// </summary>
        /// <param name="entity">The name of the related entity.</param>
        /// <param name="id">The id of the related entity.</param>
        /// <param name="limit">The maximum number of releases to return (default = 25).</param>
        /// <param name="offset">The offset to the releases list (enables paging, default = 0).</param>
        /// <param name="inc">A list of entities to include (subqueries).</param>
        /// <returns></returns>
        public static async Task<ReleaseList> BrowseAsync(string entity, string id, int limit = 25,
            int offset = 0, params string[] inc)
        {
            string url = WebServiceHelper.CreateBrowseTemplate(EntityName, entity, id, limit, offset, inc);

            return await WebServiceHelper.GetAsync<ReleaseList>(url);
        }

        /// <summary>
        /// Browse all the releases in the MusicBrainz database, which are directly linked to the entity with given id.
        /// </summary>
        /// <param name="entity">The name of the related entity.</param>
        /// <param name="id">The id of the related entity.</param>
        /// <param name="type">If releases or release-groups are included in the result, filter by type (for example 'album').</param>
        /// <param name="status">If releases are included in the result, filter by status (for example 'official', default = null).</param>
        /// <param name="limit">The maximum number of releases to return (default = 25).</param>
        /// <param name="offset">The offset to the releases list (enables paging, default = 0).</param>
        /// <param name="inc">A list of entities to include (subqueries).</param>
        /// <returns></returns>
        /// <remarks>
        /// See http://musicbrainz.org/doc/Development/XML_Web_Service/Version_2#Release_Type_and_Status for supported values of type and status.
        /// </remarks>
        public static async Task<ReleaseList> BrowseAsync(string entity, string id, string type, string status = null, int limit = 25, int offset = 0, params string[] inc)
        {
            string url = WebServiceHelper.CreateBrowseTemplate(EntityName, entity, id, type, status, limit, offset, inc);

            return await WebServiceHelper.GetAsync<ReleaseList>(url);
        }

        #endregion
    }
}
