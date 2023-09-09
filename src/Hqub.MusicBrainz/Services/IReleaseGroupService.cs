namespace Hqub.MusicBrainz.Services
{
    using Hqub.MusicBrainz.Entities;
    using Hqub.MusicBrainz.Entities.Collections;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface defining the release-group service.
    /// </summary>
    public interface IReleaseGroupService
    {
        /// <summary>
        /// Create a request to lookup a release-group in the MusicBrainz database.
        /// </summary>
        /// <param name="id">The release-group MusicBrainz id.</param>
        /// <param name="inc">A list of entities to include (subqueries).</param>
        /// <returns></returns>
        LookupRequest<ReleaseGroup> Get(string id, params string[] inc);

        /// <summary>
        /// Create a request to search for a release-group in the MusicBrainz database, matching the given query.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="limit">The maximum number of release-groups to return (default = 25).</param>
        /// <param name="offset">The offset to the release-groups list (enables paging, default = 0).</param>
        /// <returns></returns>
        SearchRequest<ReleaseGroupList> Search(string query, int limit = 25, int offset = 0);

        /// <summary>
        /// Create a request to search for a release-group in the MusicBrainz database, matching the given query.
        /// </summary>
        /// <param name="query">The query parameters.</param>
        /// <param name="limit">The maximum number of release-groups to return (default = 25).</param>
        /// <param name="offset">The offset to the release-groups list (enables paging, default = 0).</param>
        /// <returns></returns>
        SearchRequest<ReleaseGroupList> Search(QueryParameters<Artist> query, int limit = 25, int offset = 0);

        /// <summary>
        /// Create a request to browse all the release-groups in the MusicBrainz database, which are directly linked to the entity with given id.
        /// </summary>
        /// <param name="entity">The name of the related entity.</param>
        /// <param name="id">The id of the related entity.</param>
        /// <param name="limit">The maximum number of release-groups to return (default = 25).</param>
        /// <param name="offset">The offset to the release-groups list (enables paging, default = 0).</param>
        /// <param name="inc">A list of entities to include (subqueries).</param>
        /// <returns></returns>
        BrowseRequest<ReleaseGroupList> Browse(string entity, string id, int limit = 25, int offset = 0, params string[] inc);

        /// <summary>
        /// Lookup a release-group in the MusicBrainz database.
        /// </summary>
        /// <param name="id">The release-group MusicBrainz id.</param>
        /// <param name="inc">A list of entities to include (subqueries).</param>
        /// <returns></returns>
        Task<ReleaseGroup> GetAsync(string id, params string[] inc);

        /// <summary>
        /// Search for a release-group in the MusicBrainz database, matching the given query.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="limit">The maximum number of release-groups to return (default = 25).</param>
        /// <param name="offset">The offset to the release-groups list (enables paging, default = 0).</param>
        /// <returns></returns>
        Task<ReleaseGroupList> SearchAsync(string query, int limit = 25, int offset = 0);

        /// <summary>
        /// Search for a release-group in the MusicBrainz database, matching the given query.
        /// </summary>
        /// <param name="query">The query parameters.</param>
        /// <param name="limit">The maximum number of release-groups to return (default = 25).</param>
        /// <param name="offset">The offset to the release-groups list (enables paging, default = 0).</param>
        /// <returns></returns>
        Task<ReleaseGroupList> SearchAsync(QueryParameters<ReleaseGroup> query, int limit = 25, int offset = 0);

        /// <summary>
        /// Browse all the release-groups in the MusicBrainz database, which are directly linked to the entity with given id.
        /// </summary>
        /// <param name="entity">The name of the related entity.</param>
        /// <param name="id">The id of the related entity.</param>
        /// <param name="limit">The maximum number of release-groups to return (default = 25).</param>
        /// <param name="offset">The offset to the release-groups list (enables paging, default = 0).</param>
        /// <param name="inc">A list of entities to include (subqueries).</param>
        /// <returns></returns>
        Task<ReleaseGroupList> BrowseAsync(string entity, string id, int limit = 25, int offset = 0, params string[] inc);

        /// <summary>
        /// Browse all the release-groups in the MusicBrainz database, which are directly linked to the entity with given id.
        /// </summary>
        /// <param name="entity">The name of the related entity.</param>
        /// <param name="id">The id of the related entity.</param>
        /// <param name="type">If releases or release-groups are included in the result, filter by type (for example 'album').</param>
        /// <param name="limit">The maximum number of release-groups to return (default = 25).</param>
        /// <param name="offset">The offset to the release-groups list (enables paging, default = 0).</param>
        /// <param name="inc">A list of entities to include (subqueries).</param>
        /// <returns></returns>
        /// <remarks>
        /// See https://musicbrainz.org/doc/Development/XML_Web_Service/Version_2#Release_Type_and_Status for supported values of type.
        /// </remarks>
        Task<ReleaseGroupList> BrowseAsync(string entity, string id, string type, int limit = 25, int offset = 0, params string[] inc);
    }
}
