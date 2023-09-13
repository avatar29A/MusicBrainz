namespace Hqub.MusicBrainz.Services
{
    using Hqub.MusicBrainz.Entities;
    using Hqub.MusicBrainz.Entities.Collections;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface defining the recording service.
    /// </summary>
    public interface IRecordingService
    {
        /// <summary>
        /// Create a request to lookup a recording in the MusicBrainz database.
        /// </summary>
        /// <param name="id">The recording MusicBrainz id.</param>
        /// <param name="inc">A list of entities to include (subqueries).</param>
        /// <returns></returns>
        LookupRequest<Recording> Get(string id, params string[] inc);

        /// <summary>
        /// Create a request to search for a recording in the MusicBrainz database, matching the given query.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="limit">The maximum number of recordings to return (default = 25).</param>
        /// <param name="offset">The offset to the recordings list (enables paging, default = 0).</param>
        /// <returns></returns>
        SearchRequest<RecordingList> Search(string query, int limit = 25, int offset = 0);

        /// <summary>
        /// Create a request to search for a recording in the MusicBrainz database, matching the given query.
        /// </summary>
        /// <param name="query">The query parameters.</param>
        /// <param name="limit">The maximum number of recordings to return (default = 25).</param>
        /// <param name="offset">The offset to the recordings list (enables paging, default = 0).</param>
        /// <returns></returns>
        SearchRequest<RecordingList> Search(QueryParameters<Artist> query, int limit = 25, int offset = 0);

        /// <summary>
        /// Create a request to browse all the recordings in the MusicBrainz database, which are directly linked to the entity with given id.
        /// </summary>
        /// <param name="entity">The name of the related entity.</param>
        /// <param name="id">The id of the related entity.</param>
        /// <param name="limit">The maximum number of recordings to return (default = 25).</param>
        /// <param name="offset">The offset to the recordings list (enables paging, default = 0).</param>
        /// <param name="inc">A list of entities to include (subqueries).</param>
        /// <returns></returns>
        BrowseRequest<RecordingList> Browse(string entity, string id, int limit = 25, int offset = 0, params string[] inc);

        /// <summary>
        /// Lookup a recording in the MusicBrainz database.
        /// </summary>
        /// <param name="id">The recording MusicBrainz id.</param>
        /// <param name="inc">A list of entities to include (subqueries).</param>
        /// <returns></returns>
        Task<Recording> GetAsync(string id, params string[] inc);

        /// <summary>
        /// Search for a recording in the MusicBrainz database, matching the given query.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="limit">The maximum number of recordings to return (default = 25).</param>
        /// <param name="offset">The offset to the recordings list (enables paging, default = 0).</param>
        /// <returns></returns>
        Task<RecordingList> SearchAsync(string query, int limit = 25, int offset = 0);

        /// <summary>
        /// Search for a recording in the MusicBrainz database, matching the given query.
        /// </summary>
        /// <param name="query">The query parameters.</param>
        /// <param name="limit">The maximum number of recordings to return (default = 25).</param>
        /// <param name="offset">The offset to the recordings list (enables paging, default = 0).</param>
        /// <returns></returns>
        Task<RecordingList> SearchAsync(QueryParameters<Recording> query, int limit = 25, int offset = 0);

        /// <summary>
        /// Browse all the recordings in the MusicBrainz database, which are directly linked to the entity with given id.
        /// </summary>
        /// <param name="entity">The name of the related entity.</param>
        /// <param name="id">The id of the related entity.</param>
        /// <param name="limit">The maximum number of recordings to return (default = 25).</param>
        /// <param name="offset">The offset to the recordings list (enables paging, default = 0).</param>
        /// <param name="inc">A list of entities to include (subqueries).</param>
        /// <returns></returns>
        Task<RecordingList> BrowseAsync(string entity, string id, int limit = 25, int offset = 0, params string[] inc);
    }
}
