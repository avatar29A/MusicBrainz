namespace Hqub.MusicBrainz.Services
{
    using Hqub.MusicBrainz.Entities;
    using System;
    using System.Threading.Tasks;

    class RecordingService : IRecordingService
    {
        private const string EntityName = "recording";

        private readonly MusicBrainzClient client;
        private readonly UrlBuilder builder;

        public RecordingService(MusicBrainzClient client, UrlBuilder builder)
        {
            this.client = client;
            this.builder = builder;
        }

        #region Fluent API

        /// <inheritdoc />
        public LookupRequest<Recording> Get(string id, params string[] inc)
        {
            return new LookupRequest<Recording>(client, builder, id, EntityName).Include(inc);
        }

        /// <inheritdoc />
        public SearchRequest<Recording> Search(string query, int limit = 25, int offset = 0)
        {
            return new RecordingSearchRequest(client, builder, query, EntityName).Limit(limit).Offset(offset);
        }

        /// <inheritdoc />
        public SearchRequest<Recording> Search(QueryParameters<Recording> query, int limit = 25, int offset = 0)
        {
            return new RecordingSearchRequest(client, builder, query.ToString(), EntityName).Limit(limit).Offset(offset);
        }

        /// <inheritdoc />
        public BrowseRequest<Recording> Browse(string entity, string id, int limit = 25, int offset = 0, params string[] inc)
        {
            return new RecordingBrowseRequest(client, builder, id, entity, EntityName).Limit(limit).Offset(offset).Include(inc);
        }

        #endregion

        #region Direct API

        /// <inheritdoc />
        public async Task<Recording> GetAsync(string id, params string[] inc)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException(string.Format(Resources.Messages.MissingParameter, "id"));
            }

            string url = builder.CreateLookupUrl(EntityName, id, inc);

            return await client.GetAsync<Recording>(url);
        }

        /// <inheritdoc />
        public async Task<QueryResult<Recording>> SearchAsync(string query, int limit = 25, int offset = 0)
        {
            if (string.IsNullOrEmpty(query))
            {
                throw new ArgumentException(string.Format(Resources.Messages.MissingParameter, "query"));
            }

            string url = builder.CreateSearchUrl(EntityName, query, limit, offset);

            var list = await client.GetAsync<RecordingList>(url);

            return new QueryResult<Recording>(list.Count, list.Offset, list.Items);
        }

        /// <inheritdoc />
        public async Task<QueryResult<Recording>> SearchAsync(QueryParameters<Recording> query, int limit = 25, int offset = 0)
        {
            return await SearchAsync(query.ToString(), limit, offset);
        }

        /// <inheritdoc />
        public async Task<QueryResult<Recording>> BrowseAsync(string entity, string id, int limit = 25, int offset = 0, params string[] inc)
        {
            string url = builder.CreateBrowseUrl(EntityName, entity, id, limit, offset, inc);

            var list = await client.GetAsync<RecordingListBrowse>(url);

            return new QueryResult<Recording>(list.Count, list.Offset, list.Items);
        }

        #endregion
    }
}
