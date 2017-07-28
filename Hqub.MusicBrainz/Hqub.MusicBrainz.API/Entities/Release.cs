﻿
namespace Hqub.MusicBrainz.API.Entities
{
    using Hqub.MusicBrainz.API.Entities.Collections;
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Threading.Tasks;

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

        [DataMember(Name = "artist-credit")]
        public List<NameCredit> Credits { get; set; }

        [DataMember(Name = "label-info")]
        public List<LabelInfo> Labels { get; set; }

        [DataMember(Name = "media")]
        public List<Medium> Media { get; set; }

        #endregion

        #region Static Methods

        [Obsolete("Use GetAsync() method.")]
        public static Release Get(string id, params string[] inc)
        {
            return GetAsync<Release>(EntityName, id, inc).Result;
        }

        [Obsolete("Use SearchAsync() method.")]
        public static ReleaseList Search(string query, int limit = 25, int offset = 0)
        {
            return SearchAsync<ReleaseMetadata>(EntityName,
                query, limit, offset).Result.Collection;
        }

        /// <summary>
        /// Lookup a release in the MusicBrainz database.
        /// </summary>
        /// <param name="id">The release MusicBrainz id.</param>
        /// <param name="inc">A list of entities to include (subqueries).</param>
        /// <returns></returns>
        public async static Task<Release> GetAsync(string id, params string[] inc)
        {
            return await GetAsync<Release>(EntityName, id, inc);
        }

        /// <summary>
        /// Search for a release in the MusicBrainz database, matching the given query.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="limit">The maximum number of releases to return (default = 25).</param>
        /// <param name="offset">The offset to the releases list (enables paging, default = 0).</param>
        /// <returns></returns>
        public async static Task<ReleaseList> SearchAsync(string query, int limit = 25, int offset = 0)
        {
            return (await SearchAsync<ReleaseMetadata>(EntityName,
                query, limit, offset)).Collection;
        }

        /// <summary>
        /// Search for a release in the MusicBrainz database, matching the given query.
        /// </summary>
        /// <param name="query">The query parameters.</param>
        /// <param name="limit">The maximum number of releases to return (default = 25).</param>
        /// <param name="offset">The offset to the releases list (enables paging, default = 0).</param>
        /// <returns></returns>
        public async static Task<ReleaseList> SearchAsync(QueryParameters<Release> query, int limit = 25, int offset = 0)
        {
            return (await SearchAsync<ReleaseMetadata>(EntityName,
                query.ToString(), limit, offset)).Collection;
        }

        /// <summary>
        /// Browse all the releases in the MusicBrainz database, which are directly linked to the
        /// entity with given id.
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
            return (await BrowseAsync<ReleaseMetadata>(EntityName,
                entity, id, limit, offset, inc)).Collection;
        }

        #endregion
    }
}
