namespace Hqub.MusicBrainz.Services
{
    using Hqub.MusicBrainz.Entities;
    using System;
    using System.Threading.Tasks;

    class ReleaseService : IReleaseService
    {
        private const string EntityName = "release";

        private readonly MusicBrainzClient client;
        private readonly UrlBuilder builder;

        public ReleaseService(MusicBrainzClient client, UrlBuilder builder)
        {
            this.client = client;
            this.builder = builder;
        }

        #region Fluent API

        /// <inheritdoc />
        public LookupRequest<Release> Get(string id, params string[] inc)
        {
            return new LookupRequest<Release>(client, builder, id, EntityName).Include(inc);
        }

        /// <inheritdoc />
        public SearchRequest<Release> Search(string query, int limit = 25, int offset = 0)
        {
            return new ReleaseSearchRequest(client, builder, query, EntityName).Limit(limit).Offset(offset);
        }

        /// <inheritdoc />
        public SearchRequest<Release> Search(QueryParameters<Release> query, int limit = 25, int offset = 0)
        {
            return new ReleaseSearchRequest(client, builder, query.ToString(), EntityName).Limit(limit).Offset(offset);
        }

        /// <inheritdoc />
        public BrowseRequest<Release> Browse(string entity, string id, int limit = 25, int offset = 0, params string[] inc)
        {
            return new ReleaseBrowseRequest(client, builder, id, entity, EntityName).Limit(limit).Offset(offset).Include(inc);
        }

        #endregion

        #region Direct API

        /// <inheritdoc />
        public async Task<Release> GetAsync(string id, params string[] inc)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException(string.Format(Resources.Messages.MissingParameter, "id"));
            }

            string url = builder.CreateLookupUrl(EntityName, id, inc);

            return await client.GetAsync<Release>(url);
        }

        /// <inheritdoc />
        public async Task<QueryResult<Release>> SearchAsync(string query, int limit = 25, int offset = 0)
        {
            if (string.IsNullOrEmpty(query))
            {
                throw new ArgumentException(string.Format(Resources.Messages.MissingParameter, "query"));
            }

            string url = builder.CreateSearchUrl(EntityName, query, limit, offset);

            var list = await client.GetAsync<ReleaseList>(url);

            return new QueryResult<Release>() { Items = list.Items, Count = list.Count, Offset = list.Offset };
        }

        /// <inheritdoc />
        public async Task<QueryResult<Release>> SearchAsync(QueryParameters<Release> query, int limit = 25, int offset = 0)
        {
            return await SearchAsync(query.ToString(), limit, offset);
        }

        /// <inheritdoc />
        public async Task<QueryResult<Release>> BrowseAsync(string entity, string id, int limit = 25,
            int offset = 0, params string[] inc)
        {
            string url = builder.CreateBrowseUrl(EntityName, entity, id, limit, offset, inc);

            var list = await client.GetAsync<ReleaseListBrowse>(url);

            return new QueryResult<Release>() { Items = list.Items, Count = list.Count, Offset = list.Offset };
        }

        /// <inheritdoc />
        public async Task<QueryResult<Release>> BrowseAsync(string entity, string id, string type, string status = null, int limit = 25, int offset = 0, params string[] inc)
        {
            string url = builder.CreateBrowseUrl(EntityName, entity, id, type, status, limit, offset, inc);

            var list = await client.GetAsync<ReleaseListBrowse>(url);

            return new QueryResult<Release>() { Items = list.Items, Count = list.Count, Offset = list.Offset };
        }

        #endregion
    }
}
