
namespace Hqub.MusicBrainz.API.Entities
{
    using Hqub.MusicBrainz.API.Entities.Collections;
    using System;
    using System.Threading.Tasks;

    partial class Recording
    {
        #region Static methods

        [Obsolete("Use async method instead.")]
        public static Recording Get(string id, params string[] inc)
        {
            return GetAsync(id, inc).Result;
        }

        [Obsolete("Use async method instead.")]
        public static RecordingList Search(string query, int limit = 25, int offset = 0)
        {
            return SearchAsync(query, limit, offset).Result;
        }

        [Obsolete("Use async method instead.")]
        public static RecordingListBrowse Browse(string relatedEntity, string value, int limit = 25, int offset = 0, params string[] inc)
        {
            return BrowseAsync(relatedEntity, value, limit, offset, inc).Result;
        }

        /// <summary>
        /// Lookup an recording in the MusicBrainz database.
        /// </summary>
        /// <param name="id">The recording MusicBrainz id.</param>
        /// <param name="inc">A list of entities to include (subqueries).</param>
        /// <returns></returns>
        [Obsolete("Use MusicBrainzClient instead of static API.")]
        public static async Task<Recording> GetAsync(string id, params string[] inc)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException(string.Format(Resources.Messages.MissingParameter, "id"));
            }

            var client = new MusicBrainzClient(Configuration.Proxy)
            {
                Cache = Configuration.Cache
            };

            return await client.Recordings.GetAsync(id, inc);
        }

        /// <summary>
        /// Search for an recording in the MusicBrainz database, matching the given query.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="limit">The maximum number of recordings to return (default = 25).</param>
        /// <param name="offset">The offset to the recordings list (enables paging, default = 0).</param>
        /// <returns></returns>
        [Obsolete("Use MusicBrainzClient instead of static API.")]
        public static async Task<RecordingList> SearchAsync(string query, int limit = 25, int offset = 0)
        {
            if (string.IsNullOrEmpty(query))
            {
                throw new ArgumentException(string.Format(Resources.Messages.MissingParameter, "query"));
            }

            var client = new MusicBrainzClient(Configuration.Proxy)
            {
                Cache = Configuration.Cache
            };

            return await client.Recordings.SearchAsync(query, limit, offset);
        }

        /// <summary>
        /// Search for an recording in the MusicBrainz database, matching the given query.
        /// </summary>
        /// <param name="query">The query parameters.</param>
        /// <param name="limit">The maximum number of recordings to return (default = 25).</param>
        /// <param name="offset">The offset to the recordings list (enables paging, default = 0).</param>
        /// <returns></returns>
        [Obsolete("Use MusicBrainzClient instead of static API.")]
        public static async Task<RecordingList> SearchAsync(QueryParameters<Recording> query, int limit = 25, int offset = 0)
        {
            return await SearchAsync(query.ToString(), limit, offset);
        }

        /// <summary>
        /// Browse all the recordings in the MusicBrainz database, which are directly linked to the entity with given id.
        /// </summary>
        /// <param name="entity">The name of the related entity.</param>
        /// <param name="id">The id of the related entity.</param>
        /// <param name="limit">The maximum number of recordings to return (default = 25).</param>
        /// <param name="offset">The offset to the recordings list (enables paging, default = 0).</param>
        /// <param name="inc">A list of entities to include (subqueries).</param>
        /// <returns></returns>
        [Obsolete("Use MusicBrainzClient instead of static API.")]
        public static async Task<RecordingListBrowse> BrowseAsync(string entity, string id, int limit = 25, int offset = 0, params string[] inc)
        {
            var client = new MusicBrainzClient(Configuration.Proxy)
            {
                Cache = Configuration.Cache
            };

            return await client.Recordings.BrowseAsync(entity, id, limit, offset, inc);
        }

        #endregion
    }
}