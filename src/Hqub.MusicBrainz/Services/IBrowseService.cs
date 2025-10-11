namespace Hqub.MusicBrainz.Services
{
    using System.Threading.Tasks;

    /// <summary>
    /// Interface defining the entity service.
    /// </summary>
    public interface IBrowseService<T>
    {
        /// <summary>
        /// Create a request to browse all entities in the MusicBrainz database, which are linked to the entity with given id.
        /// </summary>
        /// <param name="entity">The name of the related entity.</param>
        /// <param name="id">The id of the related entity.</param>
        /// <param name="limit">The number of entries to return (max. number of entries returned per page is 100, default = 25).</param>
        /// <param name="offset">Return results starting at a given offset. Used for paging through more than one page of results (default = 0).</param>
        /// <param name="inc">A list of entities to include (sub-queries).</param>
        /// <returns></returns>
        BrowseRequest<T> Browse(string entity, string id, int limit = 25, int offset = 0, params string[] inc);

        /// <summary>
        /// Browse all entities in the MusicBrainz database, which are linked to the entity with given id.
        /// </summary>
        /// <param name="entity">The name of the related entity.</param>
        /// <param name="id">The id of the related entity.</param>
        /// <param name="limit">The number of entries to return (max. number of entries returned per page is 100, default = 25).</param>
        /// <param name="offset">Return results starting at a given offset. Used for paging through more than one page of results (default = 0).</param>
        /// <param name="inc">A list of entities to include (sub-queries).</param>
        /// <returns></returns>
        Task<QueryResult<T>> BrowseAsync(string entity, string id, int limit = 25, int offset = 0, params string[] inc);
    }
}
