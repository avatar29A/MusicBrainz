namespace Hqub.MusicBrainz.API.Services
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Prepare a get request to the MusicBrainz webservice.
    /// </summary>
    /// <typeparam name="T">Any supported MusicBrainz entity.</typeparam>
    public class GetRequest<T>
    {
        private readonly string EntityName;

        private readonly MusicBrainzClient client;
        private readonly UrlBuilder builder;

        private string id;
        private string[] include;

        internal GetRequest(MusicBrainzClient client, UrlBuilder builder, string id, string entity)
        {
            this.client = client;
            this.builder = builder;
            this.id = id;

            EntityName = entity;
        }

        /// <summary>
        /// Set a collection of entity names to include in the response.
        /// </summary>
        /// <param name="include">The entity names to include.</param>
        /// <returns></returns>
        public GetRequest<T> Include(params string[] include)
        {
            this.include = include;
            return this;
        }

        /// <summary>
        /// Execute the get request.
        /// </summary>
        /// <param name="ct">The cancellation token.</param>
        /// <returns></returns>
        public async Task<T> GetAsync(CancellationToken ct = default)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException(string.Format(Resources.Messages.MissingParameter, "id"));
            }

            string url = builder.CreateLookupUrl(EntityName, id, include);

            return await client.GetAsync<T>(url, ct);
        }
    }
}
