namespace Hqub.MusicBrainz.Services
{
    using Hqub.MusicBrainz.Entities;
    using Hqub.MusicBrainz.Entities.Collections;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface defining the label service.
    /// </summary>
    public interface ILabelService
    {
        /// <summary>
        /// Create a request to lookup a label in the MusicBrainz database.
        /// </summary>
        /// <param name="id">The id of the label in the MusicBrainz database.</param>
        /// <param name="inc">A list of entities to include (sub-queries).</param>
        /// <returns></returns>
        LookupRequest<Label> Get(string id, params string[] inc);

        /// <summary>
        /// Create a request to search for a label in the MusicBrainz database, matching the given query.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="limit">The number of entries to return (max. number of entries returned per page is 100, default = 25).</param>
        /// <param name="offset">Return search results starting at a given offset. Used for paging through more than one page of results (default = 0).</param>
        /// <returns></returns>
        SearchRequest<LabelList> Search(string query, int limit = 25, int offset = 0);

        /// <summary>
        /// Create a request to search for a label in the MusicBrainz database, matching the given query.
        /// </summary>
        /// <param name="query">The query parameters.</param>
        /// <param name="limit">The number of entries to return (max. number of entries returned per page is 100, default = 25).</param>
        /// <param name="offset">Return search results starting at a given offset. Used for paging through more than one page of results (default = 0).</param>
        /// <returns></returns>
        SearchRequest<LabelList> Search(QueryParameters<Label> query, int limit = 25, int offset = 0);

        /// <summary>
        /// Create a request to browse all labels in the MusicBrainz database, which are linked to the entity with given id.
        /// </summary>
        /// <param name="entity">The name of the related entity.</param>
        /// <param name="id">The id of the related entity.</param>
        /// <param name="limit">The number of entries to return (max. number of entries returned per page is 100, default = 25).</param>
        /// <param name="offset">Return results starting at a given offset. Used for paging through more than one page of results (default = 0).</param>
        /// <param name="inc">A list of entities to include (sub-queries).</param>
        /// <returns></returns>
        BrowseRequest<LabelList> Browse(string entity, string id, int limit = 25, int offset = 0, params string[] inc);

        /// <summary>
        /// Lookup a label in the MusicBrainz database.
        /// </summary>
        /// <param name="id">The id of the label in the MusicBrainz database.</param>
        /// <param name="inc">A list of entities to include (sub-queries).</param>
        /// <returns></returns>
        Task<Label> GetAsync(string id, params string[] inc);

        /// <summary>
        /// Search for a label in the MusicBrainz database, matching the given query.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="limit">The number of entries to return (max. number of entries returned per page is 100, default = 25).</param>
        /// <param name="offset">Return search results starting at a given offset. Used for paging through more than one page of results (default = 0).</param>
        /// <returns></returns>
        Task<LabelList> SearchAsync(string query, int limit = 25, int offset = 0);

        /// <summary>
        /// Search for a label in the MusicBrainz database, matching the given query.
        /// </summary>
        /// <param name="query">The query parameters.</param>
        /// <param name="limit">The number of entries to return (max. number of entries returned per page is 100, default = 25).</param>
        /// <param name="offset">Return search results starting at a given offset. Used for paging through more than one page of results (default = 0).</param>
        /// <returns></returns>
        Task<LabelList> SearchAsync(QueryParameters<Label> query, int limit = 25, int offset = 0);

        /// <summary>
        /// Browse all labels in the MusicBrainz database, which are linked to the entity with given id.
        /// </summary>
        /// <param name="entity">The name of the related entity.</param>
        /// <param name="id">The id of the related entity.</param>
        /// <param name="limit">The number of entries to return (max. number of entries returned per page is 100, default = 25).</param>
        /// <param name="offset">Return results starting at a given offset. Used for paging through more than one page of results (default = 0).</param>
        /// <param name="inc">A list of entities to include (sub-queries).</param>
        /// <returns></returns>
        Task<LabelList> BrowseAsync(string entity, string id, int limit = 25, int offset = 0, params string[] inc);
    }
}
