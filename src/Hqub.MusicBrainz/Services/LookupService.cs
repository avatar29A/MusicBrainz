namespace Hqub.MusicBrainz.Services
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface defining the entity lookup service.
    /// </summary>
    class LookupService<T> : ILookupService<T>
    {

        private readonly MusicBrainzClient client;
        private readonly UrlBuilder builder;
        private readonly string entityName;

        public LookupService(MusicBrainzClient client, UrlBuilder builder, string entityName)
        {
            this.client = client;
            this.builder = builder;
            this.entityName = entityName;
        }

        /// <summary>
        /// Create a request to lookup an entity in the MusicBrainz database.
        /// </summary>
        /// <param name="id">The id of the entity in the MusicBrainz database.</param>
        /// <param name="inc">A list of entities to include (sub-queries).</param>
        /// <returns></returns>
        public LookupRequest<T> Get(string id, params string[] inc)
        {
            return new LookupRequest<T>(client, builder, id, entityName).Include(inc);
        }

        /// <summary>
        /// Lookup an entity in the MusicBrainz database.
        /// </summary>
        /// <param name="id">The id of the entity in the MusicBrainz database.</param>
        /// <param name="inc">A list of entities to include (sub-queries).</param>
        /// <returns></returns>
        public async Task<T> GetAsync(string id, params string[] inc)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException(string.Format(Resources.Messages.MissingParameter, "id"));
            }

            string url = builder.CreateLookupUrl(entityName, id, inc);

            return await client.GetAsync<T>(url);
        }
    }
}
