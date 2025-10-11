namespace Hqub.MusicBrainz.Services
{
    using Hqub.MusicBrainz.Entities;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface defining the release service.
    /// </summary>
    public interface IReleaseService : ILookupService<Release>, ISearchService<Release>, IBrowseService<Release>
    {
        /// <summary>
        /// Browse all releases in the MusicBrainz database, which are linked to the entity with given id.
        /// </summary>
        /// <param name="entity">The name of the related entity.</param>
        /// <param name="id">The id of the related entity.</param>
        /// <param name="type">If releases or release-groups are included in the result, filter by type (for example 'album').</param>
        /// <param name="status">If releases are included in the result, filter by status (for example 'official', default = null).</param>
        /// <param name="limit">The number of entries to return (max. number of entries returned per page is 100, default = 25).</param>
        /// <param name="offset">Return results starting at a given offset. Used for paging through more than one page of results (default = 0).</param>
        /// <param name="inc">A list of entities to include (sub-queries).</param>
        /// <returns></returns>
        /// <remarks>
        /// See https://musicbrainz.org/doc/Development/XML_Web_Service/Version_2#Release_Type_and_Status for supported values of type and status.
        /// </remarks>
        Task<QueryResult<Release>> BrowseAsync(string entity, string id, string type, string status = null, int limit = 25,
            int offset = 0, params string[] inc);
    }
}
