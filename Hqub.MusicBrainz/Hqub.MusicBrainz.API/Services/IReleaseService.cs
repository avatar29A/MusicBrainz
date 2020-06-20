namespace Hqub.MusicBrainz.API.Services
{
    using Hqub.MusicBrainz.API.Entities;
    using Hqub.MusicBrainz.API.Entities.Collections;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface defining the release service.
    /// </summary>
    public interface IReleaseService
    {
        /// <summary>
        /// Create a request to lookup a release in the MusicBrainz database.
        /// </summary>
        /// <param name="id">The release MusicBrainz id.</param>
        /// <param name="inc">A list of entities to include (subqueries).</param>
        /// <returns></returns>
        LookupRequest<Release> Get(string id, params string[] inc);

        /// <summary>
        /// Create a request to search for a release in the MusicBrainz database, matching the given query.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="limit">The maximum number of releases to return (default = 25).</param>
        /// <param name="offset">The offset to the releases list (enables paging, default = 0).</param>
        /// <returns></returns>
        SearchRequest<ReleaseList> Search(string query, int limit = 25, int offset = 0);

        /// <summary>
        /// Create a request to search for a release in the MusicBrainz database, matching the given query.
        /// </summary>
        /// <param name="query">The query parameters.</param>
        /// <param name="limit">The maximum number of releases to return (default = 25).</param>
        /// <param name="offset">The offset to the releases list (enables paging, default = 0).</param>
        /// <returns></returns>
        SearchRequest<ReleaseList> Search(QueryParameters<Artist> query, int limit = 25, int offset = 0);

        /// <summary>
        /// Create a request to browse all the releases in the MusicBrainz database, which are directly linked to the entity with given id.
        /// </summary>
        /// <param name="entity">The name of the related entity.</param>
        /// <param name="id">The id of the related entity.</param>
        /// <param name="limit">The maximum number of releases to return (default = 25).</param>
        /// <param name="offset">The offset to the releases list (enables paging, default = 0).</param>
        /// <param name="inc">A list of entities to include (subqueries).</param>
        /// <returns></returns>
        BrowseRequest<ReleaseList> Browse(string entity, string id, int limit = 25, int offset = 0, params string[] inc);

        /// <summary>
        /// Lookup a release in the MusicBrainz database.
        /// </summary>
        /// <param name="id">The release MusicBrainz id.</param>
        /// <param name="inc">A list of entities to include (subqueries).</param>
        /// <returns></returns>
        Task<Release> GetAsync(string id, params string[] inc);

        /// <summary>
        /// Search for a release in the MusicBrainz database, matching the given query.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="limit">The maximum number of releases to return (default = 25).</param>
        /// <param name="offset">The offset to the releases list (enables paging, default = 0).</param>
        /// <returns></returns>
        Task<ReleaseList> SearchAsync(string query, int limit = 25, int offset = 0);

        /// <summary>
        /// Search for a release in the MusicBrainz database, matching the given query.
        /// </summary>
        /// <param name="query">The query parameters.</param>
        /// <param name="limit">The maximum number of releases to return (default = 25).</param>
        /// <param name="offset">The offset to the releases list (enables paging, default = 0).</param>
        /// <returns></returns>
        Task<ReleaseList> SearchAsync(QueryParameters<Release> query, int limit = 25, int offset = 0);

        /// <summary>
        /// Browse all the releases in the MusicBrainz database, which are directly linked to the entity with given id.
        /// </summary>
        /// <param name="entity">The name of the related entity.</param>
        /// <param name="id">The id of the related entity.</param>
        /// <param name="limit">The maximum number of releases to return (default = 25).</param>
        /// <param name="offset">The offset to the releases list (enables paging, default = 0).</param>
        /// <param name="inc">A list of entities to include (subqueries).</param>
        /// <returns></returns>
        Task<ReleaseList> BrowseAsync(string entity, string id, int limit = 25,
            int offset = 0, params string[] inc);

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
        /// See https://musicbrainz.org/doc/Development/XML_Web_Service/Version_2#Release_Type_and_Status for supported values of type and status.
        /// </remarks>
        Task<ReleaseList> BrowseAsync(string entity, string id, string type, string status = null, int limit = 25,
            int offset = 0, params string[] inc);
    }
}
