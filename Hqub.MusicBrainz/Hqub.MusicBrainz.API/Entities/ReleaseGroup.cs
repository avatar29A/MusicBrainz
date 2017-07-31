﻿
namespace Hqub.MusicBrainz.API.Entities
{
    using Hqub.MusicBrainz.API.Entities.Collections;
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    [DataContract(Name = "release-group")]
    public class ReleaseGroup
    {
        public const string EntityName = "release-group";

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
        /// Gets or sets the first release date.
        /// </summary>
        [DataMember(Name = "first-release-date")]
        public string FirstReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the primary type.
        /// </summary>
        [DataMember(Name = "primary-type")]
        public string PrimaryType { get; set; }

        /// <summary>
        /// Gets or sets the secondary types.
        /// </summary>
        [DataMember(Name = "secondary-types")]
        public List<string> SecondaryTypes { get; set; }

        /// <summary>
        /// Gets or sets the rating".
        /// </summary>
        [DataMember(Name = "rating")]
        public Rating Rating { get; set; }

        /// <summary>
        /// Gets or sets the tag-list.
        /// </summary>
        [DataMember(Name = "tags")]
        public List<Tag> Tags { get; set; }

        #endregion

        #region Subqueries

        [DataMember(Name = "artist-credit")]
        public List<NameCredit> Credits { get; set; }

        [DataMember(Name = "releases")]
        public List<Release> Releases { get; set; }

        #endregion

        #region Static Methods

        [Obsolete("Use GetAsync() method.")]
        public static ReleaseGroup Get(string id, params string[] inc)
        {
            return GetAsync(id, inc).Result;
        }

        [Obsolete("Use SearchAsync() method.")]
        public static ReleaseGroupList Search(string query, int limit = 25, int offset = 0)
        {
            return SearchAsync(query, limit, offset).Result;
        }

        [Obsolete("Use BrowseAsync() method.")]
        public static ReleaseGroupList Browse(string relatedEntity, string value, int limit = 25, int offset = 0, params  string[] inc)
        {
            return BrowseAsync(relatedEntity, value, limit, offset, inc).Result;
        }

        /// <summary>
        /// Lookup a release-group in the MusicBrainz database.
        /// </summary>
        /// <param name="id">The release-group MusicBrainz id.</param>
        /// <param name="inc">A list of entities to include (subqueries).</param>
        /// <returns></returns>
        public async static Task<ReleaseGroup> GetAsync(string id, params string[] inc)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException(string.Format(Resources.Messages.MissingParameter, "id"));
            }

            string url = WebRequestHelper.CreateLookupUrl(EntityName, id, inc);

            return await WebRequestHelper.GetAsync<ReleaseGroup>(url);
        }

        /// <summary>
        /// Search for a release-group in the MusicBrainz database, matching the given query.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="limit">The maximum number of release-groups to return (default = 25).</param>
        /// <param name="offset">The offset to the release-groups list (enables paging, default = 0).</param>
        /// <returns></returns>
        public async static Task<ReleaseGroupList> SearchAsync(string query, int limit = 25, int offset = 0)
        {
            if (string.IsNullOrEmpty(query))
            {
                throw new ArgumentException(string.Format(Resources.Messages.MissingParameter, "query"));
            }

            string url = WebRequestHelper.CreateSearchTemplate(EntityName, query, limit, offset);

            return await WebRequestHelper.GetAsync<ReleaseGroupList>(url);
        }

        /// <summary>
        /// Search for a release-group in the MusicBrainz database, matching the given query.
        /// </summary>
        /// <param name="query">The query parameters.</param>
        /// <param name="limit">The maximum number of release-groups to return (default = 25).</param>
        /// <param name="offset">The offset to the release-groups list (enables paging, default = 0).</param>
        /// <returns></returns>
        public async static Task<ReleaseGroupList> SearchAsync(QueryParameters<ReleaseGroup> query, int limit = 25, int offset = 0)
        {
            return await SearchAsync(query.ToString(), limit, offset);
        }

        /// <summary>
        /// Browse all the release-groups in the MusicBrainz database, which are directly linked to the
        /// entity with given id.
        /// </summary>
        /// <param name="entity">The name of the related entity.</param>
        /// <param name="id">The id of the related entity.</param>
        /// <param name="limit">The maximum number of release-groups to return (default = 25).</param>
        /// <param name="offset">The offset to the release-groups list (enables paging, default = 0).</param>
        /// <param name="inc">A list of entities to include (subqueries).</param>
        /// <returns></returns>
        public async static Task<ReleaseGroupList> BrowseAsync(string entity, string id, int limit = 25, int offset = 0, params  string[] inc)
        {
            string url = WebRequestHelper.CreateBrowseTemplate(EntityName, entity, id, limit, offset, inc);

            return await WebRequestHelper.GetAsync<ReleaseGroupList>(url);
        }

        // TODO: add string parameter 'type' and 'status' to browse methods
        // see http://musicbrainz.org/doc/Development/XML_Web_Service/Version_2#Release_Type_and_Status

        #endregion
    }
}
