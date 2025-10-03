namespace Hqub.MusicBrainz.Services
{
    using Hqub.MusicBrainz.Entities;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface defining the work service.
    /// </summary>
    public interface IWorkService
    {
        /// <summary>
        /// Create a request to lookup a work in the MusicBrainz database.
        /// </summary>
        /// <param name="id">The id of the work in the MusicBrainz database.</param>
        /// <param name="inc">A list of entities to include (sub-queries).</param>
        /// <returns></returns>
        LookupRequest<Work> Get(string id, params string[] inc);

        /// <summary>
        /// Lookup a work in the MusicBrainz database.
        /// </summary>
        /// <param name="id">The id of the work in the MusicBrainz database.</param>
        /// <param name="inc">A list of entities to include (sub-queries).</param>
        /// <returns></returns>
        Task<Work> GetAsync(string id, params string[] inc);
    }
}
