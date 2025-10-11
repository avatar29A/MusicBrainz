namespace Hqub.MusicBrainz.Services
{
    using System.Threading.Tasks;

    /// <summary>
    /// Interface defining the entity service.
    /// </summary>
    public interface ISearchService<T>
    {
        /// <summary>
        /// Create a request to search for an entity in the MusicBrainz database, matching the given query.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="limit">The number of entries to return (max. number of entries returned per page is 100, default = 25).</param>
        /// <param name="offset">Return search results starting at a given offset. Used for paging through more than one page of results (default = 0).</param>
        /// <returns></returns>
        SearchRequest<T> Search(string query, int limit = 25, int offset = 0);

        /// <summary>
        /// Create a request to search for an entity in the MusicBrainz database, matching the given query.
        /// </summary>
        /// <param name="query">The query parameters.</param>
        /// <param name="limit">The number of entries to return (max. number of entries returned per page is 100, default = 25).</param>
        /// <param name="offset">Return search results starting at a given offset. Used for paging through more than one page of results (default = 0).</param>
        /// <returns></returns>
        SearchRequest<T> Search(QueryParameters<T> query, int limit = 25, int offset = 0);

        /// <summary>
        /// Search for an entity in the MusicBrainz database, matching the given query.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="limit">The number of entries to return (max. number of entries returned per page is 100, default = 25).</param>
        /// <param name="offset">Return search results starting at a given offset. Used for paging through more than one page of results (default = 0).</param>
        /// <returns></returns>
        Task<QueryResult<T>> SearchAsync(string query, int limit = 25, int offset = 0);

        /// <summary>
        /// Search for an entity in the MusicBrainz database, matching the given query.
        /// </summary>
        /// <param name="query">The query parameters.</param>
        /// <param name="limit">The number of entries to return (max. number of entries returned per page is 100, default = 25).</param>
        /// <param name="offset">Return search results starting at a given offset. Used for paging through more than one page of results (default = 0).</param>
        /// <returns></returns>
        Task<QueryResult<T>> SearchAsync(QueryParameters<T> query, int limit = 25, int offset = 0);
    }
}
