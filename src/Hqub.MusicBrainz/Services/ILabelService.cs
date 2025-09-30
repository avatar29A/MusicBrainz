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
        /// Create a request to lookup an label in the MusicBrainz database.
        /// </summary>
        /// <param name="id">The label MusicBrainz id.</param>
        /// <param name="inc">A list of entities to include (sub-queries).</param>
        /// <returns></returns>
        LookupRequest<Label> Get(string id, params string[] inc);

        /// <summary>
        /// Create a request to search for an label in the MusicBrainz database, matching the given query.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="limit">The maximum number of labels to return (default = 25).</param>
        /// <param name="offset">The offset to the labels list (enables paging, default = 0).</param>
        /// <returns></returns>
        SearchRequest<LabelList> Search(string query, int limit = 25, int offset = 0);

        /// <summary>
        /// Create a request to search for an label in the MusicBrainz database, matching the given query.
        /// </summary>
        /// <param name="query">The query parameters.</param>
        /// <param name="limit">The maximum number of labels to return (default = 25).</param>
        /// <param name="offset">The offset to the labels list (enables paging, default = 0).</param>
        /// <returns></returns>
        SearchRequest<LabelList> Search(QueryParameters<Label> query, int limit = 25, int offset = 0);

        /// <summary>
        /// Create a request to browse all the labels in the MusicBrainz database, which are directly linked to the entity with given id.
        /// </summary>
        /// <param name="entity">The name of the related entity.</param>
        /// <param name="id">The id of the related entity.</param>
        /// <param name="limit">The maximum number of labels to return (default = 25).</param>
        /// <param name="offset">The offset to the labels list (enables paging, default = 0).</param>
        /// <param name="inc">A list of entities to include (sub-queries).</param>
        /// <returns></returns>
        BrowseRequest<LabelList> Browse(string entity, string id, int limit = 25, int offset = 0, params string[] inc);

        /// <summary>
        /// Lookup an label in the MusicBrainz database.
        /// </summary>
        /// <param name="id">The label MusicBrainz id.</param>
        /// <param name="inc">A list of entities to include (sub-queries).</param>
        /// <returns></returns>
        Task<Label> GetAsync(string id, params string[] inc);

        /// <summary>
        /// Search for an label in the MusicBrainz database, matching the given query.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="limit">The maximum number of labels to return (default = 25).</param>
        /// <param name="offset">The offset to the labels list (enables paging, default = 0).</param>
        /// <returns></returns>
        Task<LabelList> SearchAsync(string query, int limit = 25, int offset = 0);

        /// <summary>
        /// Search for an label in the MusicBrainz database, matching the given query.
        /// </summary>
        /// <param name="query">The query parameters.</param>
        /// <param name="limit">The maximum number of labels to return (default = 25).</param>
        /// <param name="offset">The offset to the labels list (enables paging, default = 0).</param>
        /// <returns></returns>
        Task<LabelList> SearchAsync(QueryParameters<Label> query, int limit = 25, int offset = 0);

        /// <summary>
        /// Browse all the labels in the MusicBrainz database, which are directly linked to the entity with given id.
        /// </summary>
        /// <param name="entity">The name of the related entity.</param>
        /// <param name="id">The id of the related entity.</param>
        /// <param name="limit">The maximum number of labels to return (default = 25).</param>
        /// <param name="offset">The offset to the labels list (enables paging, default = 0).</param>
        /// <param name="inc">A list of entities to include (sub-queries).</param>
        /// <returns></returns>
        Task<LabelList> BrowseAsync(string entity, string id, int limit = 25, int offset = 0, params string[] inc);
    }
}
