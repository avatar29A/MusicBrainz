namespace Hqub.MusicBrainz.Services
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Prepare a search request to the MusicBrainz webservice.
    /// </summary>
    /// <typeparam name="T">Any supported MusicBrainz entity.</typeparam>
    public class SearchRequest<T>
    {
        private readonly string EntityName;

        private readonly MusicBrainzClient client;
        private readonly UrlBuilder builder;

        private string query;
        private int limit = 25;
        private int offset = 0;

        internal SearchRequest(MusicBrainzClient client, UrlBuilder builder, string query, string entity)
        {
            this.client = client;
            this.builder = builder;
            this.query = query;

            EntityName = entity;
        }

        /// <summary>
        /// Set the search query string.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <returns></returns>
        public SearchRequest<T> Query(string query)
        {
            this.query = query;
            return this;
        }

        /// <summary>
        /// Set the maximum number of matches to return.
        /// </summary>
        /// <param name="limit">The maximum number of matches to return.</param>
        /// <returns></returns>
        public SearchRequest<T> Limit(int limit)
        {
            this.limit = limit;
            return this;
        }

        /// <summary>
        /// Set the offset to the list of matches (enables paging).
        /// </summary>
        /// <param name="offset">The offset to the list of matches.</param>
        /// <returns></returns>
        public SearchRequest<T> Offset(int offset)
        {
            this.offset = offset;
            return this;
        }

        /// <summary>
        /// Execute the search request.
        /// </summary>
        /// <param name="ct">The cancellation token.</param>
        /// <returns></returns>
        public async Task<T> GetAsync(CancellationToken ct = default)
        {
            if (string.IsNullOrEmpty(query))
            {
                throw new ArgumentException(string.Format(Resources.Messages.MissingParameter, "query"));
            }

            string url = builder.CreateSearchUrl(EntityName, query, limit, offset);

            return await client.GetAsync<T>(url, ct);
        }
    }
}
