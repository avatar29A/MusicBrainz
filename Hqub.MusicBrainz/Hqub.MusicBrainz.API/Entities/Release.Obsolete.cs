
namespace Hqub.MusicBrainz.API.Entities
{
    using Hqub.MusicBrainz.API.Entities.Collections;
    using System;
    using System.Threading.Tasks;

    partial class Release
    {
        #region Static Methods

        [Obsolete("Use async method instead.")]
        public static Release Get(string id, params string[] inc)
        {
            return GetAsync(id, inc).Result;
        }

        [Obsolete("Use async method instead.")]
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
        [Obsolete("Use MusicBrainzClient instead of static API.")]
        public static async Task<Release> GetAsync(string id, params string[] inc)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException(string.Format(Resources.Messages.MissingParameter, "id"));
            }

            var client = new MusicBrainzClient(Configuration.Proxy)
            {
                Cache = Configuration.Cache
            };

            return await client.Releases.GetAsync(id, inc);
        }

        /// <summary>
        /// Search for a release in the MusicBrainz database, matching the given query.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="limit">The maximum number of releases to return (default = 25).</param>
        /// <param name="offset">The offset to the releases list (enables paging, default = 0).</param>
        /// <returns></returns>
        [Obsolete("Use MusicBrainzClient instead of static API.")]
        public static async Task<ReleaseList> SearchAsync(string query, int limit = 25, int offset = 0)
        {
            if (string.IsNullOrEmpty(query))
            {
                throw new ArgumentException(string.Format(Resources.Messages.MissingParameter, "query"));
            }

            var client = new MusicBrainzClient(Configuration.Proxy)
            {
                Cache = Configuration.Cache
            };

            return await client.Releases.SearchAsync(query, limit, offset);
        }

        /// <summary>
        /// Search for a release in the MusicBrainz database, matching the given query.
        /// </summary>
        /// <param name="query">The query parameters.</param>
        /// <param name="limit">The maximum number of releases to return (default = 25).</param>
        /// <param name="offset">The offset to the releases list (enables paging, default = 0).</param>
        /// <returns></returns>
        [Obsolete("Use MusicBrainzClient instead of static API.")]
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
        [Obsolete("Use MusicBrainzClient instead of static API.")]
        public static async Task<ReleaseList> BrowseAsync(string entity, string id, int limit = 25,
            int offset = 0, params string[] inc)
        {
            var client = new MusicBrainzClient(Configuration.Proxy)
            {
                Cache = Configuration.Cache
            };

            return await client.Releases.BrowseAsync(entity, id, limit, offset, inc);
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
        [Obsolete("Use MusicBrainzClient instead of static API.")]
        public static async Task<ReleaseList> BrowseAsync(string entity, string id, string type, string status = null, int limit = 25, int offset = 0, params string[] inc)
        {
            var client = new MusicBrainzClient(Configuration.Proxy)
            {
                Cache = Configuration.Cache
            };

            return await client.Releases.BrowseAsync(entity, id, type, status, limit, offset, inc);
        }

        #endregion
    }
}