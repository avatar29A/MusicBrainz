namespace Hqub.MusicBrainz.Services
{
    using Hqub.MusicBrainz.Entities;
    using System;
    using System.Threading.Tasks;

    class ReleaseGroupService : IReleaseGroupService
    {
        private const string EntityName = "release-group";

        private readonly MusicBrainzClient client;
        private readonly UrlBuilder builder;

        public ReleaseGroupService(MusicBrainzClient client, UrlBuilder builder)
        {
            this.client = client;
            this.builder = builder;
        }

        #region Fluent API

        /// <inheritdoc />
        public LookupRequest<ReleaseGroup> Get(string id, params string[] inc)
        {
            return new LookupRequest<ReleaseGroup>(client, builder, id, EntityName).Include(inc);
        }

        /// <inheritdoc />
        public SearchRequest<ReleaseGroup> Search(string query, int limit = 25, int offset = 0)
        {
            return new ReleaseGroupSearchRequest(client, builder, query, EntityName).Limit(limit).Offset(offset);
        }

        /// <inheritdoc />
        public SearchRequest<ReleaseGroup> Search(QueryParameters<ReleaseGroup> query, int limit = 25, int offset = 0)
        {
            return new ReleaseGroupSearchRequest(client, builder, query.ToString(), EntityName).Limit(limit).Offset(offset);
        }

        /// <inheritdoc />
        public BrowseRequest<ReleaseGroup> Browse(string entity, string id, int limit = 25, int offset = 0, params string[] inc)
        {
            return new ReleaseGroupBrowseRequest(client, builder, id, entity, EntityName).Limit(limit).Offset(offset).Include(inc);
        }

        #endregion

        #region Direct API

        /// <inheritdoc />
        public async Task<ReleaseGroup> GetAsync(string id, params string[] inc)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException(string.Format(Resources.Messages.MissingParameter, "id"));
            }

            string url = builder.CreateLookupUrl(EntityName, id, inc);

            return await client.GetAsync<ReleaseGroup>(url);
        }

        /// <inheritdoc />
        public async Task<QueryResult<ReleaseGroup>> SearchAsync(string query, int limit = 25, int offset = 0)
        {
            if (string.IsNullOrEmpty(query))
            {
                throw new ArgumentException(string.Format(Resources.Messages.MissingParameter, "query"));
            }

            string url = builder.CreateSearchUrl(EntityName, query, limit, offset);

            var list = await client.GetAsync<ReleaseGroupList>(url);

            return new QueryResult<ReleaseGroup>() { Items = list.Items, Count = list.Count, Offset = list.Offset };
        }

        /// <inheritdoc />
        public async Task<QueryResult<ReleaseGroup>> SearchAsync(QueryParameters<ReleaseGroup> query, int limit = 25, int offset = 0)
        {
            return await SearchAsync(query.ToString(), limit, offset);
        }

        /// <inheritdoc />
        public async Task<QueryResult<ReleaseGroup>> BrowseAsync(string entity, string id, int limit = 25, int offset = 0, params string[] inc)
        {
            string url = builder.CreateBrowseUrl(EntityName, entity, id, limit, offset, inc);

            var list = await client.GetAsync<ReleaseGroupListBrowse>(url);

            return new QueryResult<ReleaseGroup>() { Items = list.Items, Count = list.Count, Offset = list.Offset };
        }

        /// <inheritdoc />
        public async Task<QueryResult<ReleaseGroup>> BrowseAsync(string entity, string id, string type, int limit = 25, int offset = 0, params string[] inc)
        {
            string url = builder.CreateBrowseUrl(EntityName, entity, id, type, null, limit, offset, inc);

            var list = await client.GetAsync<ReleaseGroupListBrowse>(url);

            return new QueryResult<ReleaseGroup>() { Items = list.Items, Count = list.Count, Offset = list.Offset };
        }

        #endregion
    }
}
