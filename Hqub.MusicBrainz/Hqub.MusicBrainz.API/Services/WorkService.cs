namespace Hqub.MusicBrainz.API.Services
{
    using Hqub.MusicBrainz.API.Entities;
    using Hqub.MusicBrainz.API.Entities.Collections;
    using System;
    using System.Threading.Tasks;

    class WorkService : IWorkService
    {
        private const string EntityName = "work";

        private readonly MusicBrainzClient client;
        private readonly UrlBuilder builder;

        public WorkService(MusicBrainzClient client, UrlBuilder builder)
        {
            this.client = client;
            this.builder = builder;
        }

        #region Fluent API

        /// <inheritdoc />
        public GetRequest<Work> Get(string id, params string[] inc)
        {
            return new GetRequest<Work>(client, builder, id, EntityName).Include(inc);
        }

        #endregion

        #region Direct API

        /// <summary>
        /// Lookup a work in the MusicBrainz database.
        /// </summary>
        /// <param name="id">The work MusicBrainz id.</param>
        /// <param name="inc">A list of entities to include (subqueries).</param>
        /// <returns></returns>
        public async Task<Work> GetAsync(string id, params string[] inc)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException(string.Format(Resources.Messages.MissingParameter, "id"));
            }

            string url = builder.CreateLookupUrl(EntityName, id, inc);

            return await client.GetAsync<Work>(url);
        }

        #endregion
    }
}
