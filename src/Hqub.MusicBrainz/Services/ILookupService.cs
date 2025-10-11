namespace Hqub.MusicBrainz.Services
{
    using Hqub.MusicBrainz.Entities;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface defining the entity lookup service.
    /// </summary>
    public interface ILookupService<T> where T : IEntity
    {
        /// <summary>
        /// Create a request to lookup an entity in the MusicBrainz database.
        /// </summary>
        /// <param name="id">The id of the entity in the MusicBrainz database.</param>
        /// <param name="inc">A list of entities to include (sub-queries).</param>
        /// <returns></returns>
        LookupRequest<T> Get(string id, params string[] inc);

        /// <summary>
        /// Lookup an entity in the MusicBrainz database.
        /// </summary>
        /// <param name="id">The id of the entity in the MusicBrainz database.</param>
        /// <param name="inc">A list of entities to include (sub-queries).</param>
        /// <returns></returns>
        Task<T> GetAsync(string id, params string[] inc);
    }
}
