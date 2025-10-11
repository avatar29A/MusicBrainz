namespace Hqub.MusicBrainz.Services
{
    using Hqub.MusicBrainz.Entities;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Prepare a lookup request to the MusicBrainz web service.
    /// </summary>
    /// <typeparam name="T">Any supported MusicBrainz entity.</typeparam>
    public class LookupRequest<T> where T : IEntity
    {
        private readonly string EntityName;

        private readonly MusicBrainzClient client;
        private readonly UrlBuilder builder;

        private readonly string id;
        private string[] include;

        internal LookupRequest(MusicBrainzClient client, UrlBuilder builder, string id, string entity)
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
        public LookupRequest<T> Include(params string[] include)
        {
            this.include = include;
            return this;
        }

        /// <summary>
        /// Execute the lookup request.
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

        /// <summary>
        /// Returns the request path.
        /// </summary>
        public override string ToString()
        {
            return builder.CreateLookupUrl(EntityName, id, include);
        }
    }
}
