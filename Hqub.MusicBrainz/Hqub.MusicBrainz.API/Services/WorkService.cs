namespace Hqub.MusicBrainz.API.Services
{
    using Hqub.MusicBrainz.API.Entities;
    using Hqub.MusicBrainz.API.Entities.Collections;
    using System;
    using System.Threading.Tasks;

    public class WorkService
    {
        private const string EntityName = "work";

        private readonly MusicBrainzClient client;

        public WorkService(MusicBrainzClient client)
        {
            this.client = client;
        }

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

            string url = client.CreateLookupUrl(EntityName, id, inc);

            return await client.GetAsync<Work>(url);
        }
    }
}
