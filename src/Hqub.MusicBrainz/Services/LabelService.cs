namespace Hqub.MusicBrainz.Services
{
    using Hqub.MusicBrainz.Entities;
    using Hqub.MusicBrainz.Entities.Collections;
    using System;
    using System.Threading.Tasks;

    class LabelService : ILabelService
    {
        private const string EntityName = "label";

        private readonly MusicBrainzClient client;
        private readonly UrlBuilder builder;

        public LabelService(MusicBrainzClient client, UrlBuilder builder)
        {
            this.client = client;
            this.builder = builder;
        }

        #region Fluent API

        /// <inheritdoc />
        public LookupRequest<Label> Get(string id, params string[] inc)
        {
            return new LookupRequest<Label>(client, builder, id, EntityName).Include(inc);
        }

        /// <inheritdoc />
        public SearchRequest<LabelList> Search(string query, int limit = 25, int offset = 0)
        {
            return new SearchRequest<LabelList>(client, builder, query, EntityName).Limit(limit).Offset(offset);
        }

        /// <inheritdoc />
        public SearchRequest<LabelList> Search(QueryParameters<Label> query, int limit = 25, int offset = 0)
        {
            return new SearchRequest<LabelList>(client, builder, query.ToString(), EntityName).Limit(limit).Offset(offset);
        }

        /// <inheritdoc />
        public BrowseRequest<LabelList> Browse(string entity, string id, int limit = 25, int offset = 0, params string[] inc)
        {
            return new LabelBrowseRequest(client, builder, id, entity, EntityName).Limit(limit).Offset(offset).Include(inc);
        }

        #endregion

        #region Direct API

        /// <inheritdoc />
        public async Task<Label> GetAsync(string id, params string[] inc)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException(string.Format(Resources.Messages.MissingParameter, "id"));
            }

            string url = builder.CreateLookupUrl(EntityName, id, inc);

            return await client.GetAsync<Label>(url);
        }

        /// <inheritdoc />
        public async Task<LabelList> SearchAsync(string query, int limit = 25, int offset = 0)
        {
            if (string.IsNullOrEmpty(query))
            {
                throw new ArgumentException(string.Format(Resources.Messages.MissingParameter, "query"));
            }

            string url = builder.CreateSearchUrl(EntityName, query, limit, offset);

            return await client.GetAsync<LabelList>(url);
        }

        /// <inheritdoc />
        public async Task<LabelList> SearchAsync(QueryParameters<Label> query, int limit = 25, int offset = 0)
        {
            return await SearchAsync(query.ToString(), limit, offset);
        }

        /// <inheritdoc />
        public async Task<LabelList> BrowseAsync(string entity, string id, int limit = 25, int offset = 0, params string[] inc)
        {
            string url = builder.CreateBrowseUrl(EntityName, entity, id, limit, offset, inc);

            var list = await client.GetAsync<LabelListBrowse>(url);

            return new LabelList() { Items = list.Items, Count = list.Count, Offset = list.Offset };
        }

        #endregion
    }
}
