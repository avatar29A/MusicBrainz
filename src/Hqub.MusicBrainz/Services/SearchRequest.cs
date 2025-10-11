namespace Hqub.MusicBrainz.Services
{
    using Hqub.MusicBrainz.Entities;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Prepare a search request to the MusicBrainz web service.
    /// </summary>
    /// <typeparam name="T">Any supported MusicBrainz entity.</typeparam>
    public abstract class SearchRequest<T> where T : IEntity
    {
        private readonly MusicBrainzClient client;

        internal readonly UrlBuilder builder;

        /// <summary>Entity name.</summary>
        protected readonly string EntityName;

        /// <summary>The search query.</summary>
        protected string query;

        /// <summary>Limit of fetched items.</summary>
        protected int limit = 25;

        /// <summary>Offset to start browsing.</summary>
        protected int offset = 0;

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
        public async Task<QueryResult<T>> GetAsync(CancellationToken ct = default)
        {
            return await SearchAsync(client, ct);
        }

        /// <summary>
        /// Initiate the actual request.
        /// </summary>
        protected abstract Task<QueryResult<T>> SearchAsync(MusicBrainzClient client, CancellationToken ct);

        /// <summary>
        /// Returns the request path.
        /// </summary>
        public override string ToString()
        {
            return builder.CreateSearchUrl(EntityName, query, limit, offset);
        }
    }

    #region Private implementation

    [DataContract]
    class ArtistList
    {
        [DataMember(Name = "count")]
        public int Count { get; set; }

        [DataMember(Name = "offset")]
        public int Offset { get; set; }

        [DataMember(Name = "artists")]
        public List<Artist> Items { get; set; }
    }

    class ArtistSearchRequest : SearchRequest<Artist>
    {
        public ArtistSearchRequest(MusicBrainzClient client, UrlBuilder builder, string query, string entity)
            : base(client, builder, query, entity)
        {
        }

        /// <inheritdoc />
        protected override async Task<QueryResult<Artist>> SearchAsync(MusicBrainzClient client, CancellationToken ct)
        {
            string url = builder.CreateSearchUrl(EntityName, query, limit, offset);

            var list = await client.GetAsync<ArtistList>(url, ct);

            return new QueryResult<Artist>(list.Count, list.Offset, list.Items);
        }
    }

    [DataContract]
    class LabelList
    {
        [DataMember(Name = "count")]
        public int Count { get; set; }

        [DataMember(Name = "offset")]
        public int Offset { get; set; }

        [DataMember(Name = "labels")]
        public List<Label> Items { get; set; }
    }

    class LabelSearchRequest : SearchRequest<Label>
    {
        public LabelSearchRequest(MusicBrainzClient client, UrlBuilder builder, string query, string entity)
            : base(client, builder, query, entity)
        {
        }

        /// <inheritdoc />
        protected override async Task<QueryResult<Label>> SearchAsync(MusicBrainzClient client, CancellationToken ct)
        {
            string url = builder.CreateSearchUrl(EntityName, query, limit, offset);

            var list = await client.GetAsync<LabelList>(url, ct);

            return new QueryResult<Label>(list.Count, list.Offset, list.Items);
        }
    }

    [DataContract]
    class RecordingList
    {
        [DataMember(Name = "count")]
        public int Count { get; set; }

        [DataMember(Name = "offset")]
        public int Offset { get; set; }

        [DataMember(Name = "recordings")]
        public List<Recording> Items { get; set; }
    }

    class RecordingSearchRequest : SearchRequest<Recording>
    {
        public RecordingSearchRequest(MusicBrainzClient client, UrlBuilder builder, string query, string entity)
            : base(client, builder, query, entity)
        {
        }

        /// <inheritdoc />
        protected override async Task<QueryResult<Recording>> SearchAsync(MusicBrainzClient client, CancellationToken ct)
        {
            string url = builder.CreateSearchUrl(EntityName, query, limit, offset);

            var list = await client.GetAsync<RecordingList>(url, ct);

            return new QueryResult<Recording>(list.Count, list.Offset, list.Items);
        }
    }

    [DataContract]
    class ReleaseGroupList
    {
        [DataMember(Name = "count")]
        public int Count { get; set; }

        [DataMember(Name = "offset")]
        public int Offset { get; set; }

        [DataMember(Name = "release-groups")]
        public List<ReleaseGroup> Items { get; set; }
    }

    class ReleaseGroupSearchRequest : SearchRequest<ReleaseGroup>
    {
        public ReleaseGroupSearchRequest(MusicBrainzClient client, UrlBuilder builder, string query, string entity)
            : base(client, builder, query, entity)
        {
        }

        /// <inheritdoc />
        protected override async Task<QueryResult<ReleaseGroup>> SearchAsync(MusicBrainzClient client, CancellationToken ct)
        {
            string url = builder.CreateSearchUrl(EntityName, query, limit, offset);

            var list = await client.GetAsync<ReleaseGroupList>(url, ct);

            return new QueryResult<ReleaseGroup>(list.Count, list.Offset, list.Items);
        }
    }

    [DataContract]
    class ReleaseList
    {
        [DataMember(Name = "count")]
        public int Count { get; set; }

        [DataMember(Name = "offset")]
        public int Offset { get; set; }

        [DataMember(Name = "releases")]
        public List<Release> Items { get; set; }
    }

    class ReleaseSearchRequest : SearchRequest<Release>
    {
        public ReleaseSearchRequest(MusicBrainzClient client, UrlBuilder builder, string query, string entity)
            : base(client, builder, query, entity)
        {
        }

        /// <inheritdoc />
        protected override async Task<QueryResult<Release>> SearchAsync(MusicBrainzClient client, CancellationToken ct)
        {
            string url = builder.CreateSearchUrl(EntityName, query, limit, offset);

            var list = await client.GetAsync<ReleaseList>(url, ct);

            return new QueryResult<Release>(list.Count, list.Offset, list.Items);
        }
    }

    #endregion
}
