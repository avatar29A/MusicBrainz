namespace Hqub.MusicBrainz.Services
{
    using Hqub.MusicBrainz.Entities;
    using System;
    using System.Threading.Tasks;

    class ArtistService : IArtistService
    {
        private const string EntityName = "artist";

        private readonly MusicBrainzClient client;
        private readonly UrlBuilder builder;

        public ArtistService(MusicBrainzClient client, UrlBuilder builder)
        {
            this.client = client;
            this.builder = builder;
        }

        #region Fluent API

        /// <inheritdoc />
        public LookupRequest<Artist> Get(string id, params string[] inc)
        {
            return new LookupRequest<Artist>(client, builder, id, EntityName).Include(inc);
        }

        /// <inheritdoc />
        public SearchRequest<Artist> Search(string query, int limit = 25, int offset = 0)
        {
            return new ArtistSearchRequest(client, builder, query, EntityName).Limit(limit).Offset(offset);
        }

        /// <inheritdoc />
        public SearchRequest<Artist> Search(QueryParameters<Artist> query, int limit = 25, int offset = 0)
        {
            return new ArtistSearchRequest(client, builder, query.ToString(), EntityName).Limit(limit).Offset(offset);
        }

        /// <inheritdoc />
        public BrowseRequest<Artist> Browse(string entity, string id, int limit = 25, int offset = 0, params string[] inc)
        {
            return new ArtistBrowseRequest(client, builder, id, entity, EntityName).Limit(limit).Offset(offset).Include(inc);
        }

        #endregion

        #region Direct API

        /// <inheritdoc />
        public async Task<Artist> GetAsync(string id, params string[] inc)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException(string.Format(Resources.Messages.MissingParameter, "id"));
            }

            string url = builder.CreateLookupUrl(EntityName, id, inc);

            return await client.GetAsync<Artist>(url);
        }

        /// <inheritdoc />
        public async Task<QueryResult<Artist>> SearchAsync(string query, int limit = 25, int offset = 0)
        {
            if (string.IsNullOrEmpty(query))
            {
                throw new ArgumentException(string.Format(Resources.Messages.MissingParameter, "query"));
            }

            string url = builder.CreateSearchUrl(EntityName, query, limit, offset);

            var list = await client.GetAsync<ArtistList>(url);

            return new QueryResult<Artist>(list.Count, list.Offset, list.Items);
        }

        /// <inheritdoc />
        public async Task<QueryResult<Artist>> SearchAsync(QueryParameters<Artist> query, int limit = 25, int offset = 0)
        {
            return await SearchAsync(query.ToString(), limit, offset);
        }

        /// <inheritdoc />
        public async Task<QueryResult<Artist>> BrowseAsync(string entity, string id, int limit = 25, int offset = 0, params string[] inc)
        {
            string url = builder.CreateBrowseUrl(EntityName, entity, id, limit, offset, inc);

            var list = await client.GetAsync<ArtistListBrowse>(url);

            return new QueryResult<Artist>(list.Count, list.Offset, list.Items);
        }

        #endregion
    }
}
