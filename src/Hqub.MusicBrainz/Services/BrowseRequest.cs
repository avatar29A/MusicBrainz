namespace Hqub.MusicBrainz.Services
{
    using Hqub.MusicBrainz.Entities;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Prepare a browse request to the MusicBrainz web service.
    /// </summary>
    /// <typeparam name="T">Any supported MusicBrainz entity.</typeparam>
    public abstract class BrowseRequest<T> where T : IEntity
    {
        private readonly MusicBrainzClient client;

        internal readonly UrlBuilder builder;

        /// <summary>Entity name.</summary>
        protected readonly string EntityName;

        /// <summary>Related entity name.</summary>
        protected string relatedEntity;

        /// <summary>MusicBrainz id.</summary>
        protected string id;

        /// <summary>Include entities.</summary>
        protected string[] include;

        /// <summary>Limit of fetched items.</summary>
        protected int limit;

        /// <summary>Offset to start browsing.</summary>
        protected int offset;

        /// <summary>Album type (only release or release-group).</summary>
        protected string type;

        /// <summary>Album type (only release).</summary>
        protected string status;

        internal BrowseRequest(MusicBrainzClient client, UrlBuilder builder, string id, string relatedEntity, string entity)
        {
            this.client = client;
            this.builder = builder;
            this.relatedEntity = relatedEntity;
            this.id = id;

            EntityName = entity;
        }

        /// <summary>
        /// Set a collection of entity names to include in the response.
        /// </summary>
        /// <param name="include">The entity names to include.</param>
        /// <returns></returns>
        public BrowseRequest<T> Include(params string[] include)
        {
            this.include = include;
            return this;
        }

        /// <summary>
        /// Set the maximum number of items to return.
        /// </summary>
        /// <param name="limit">The maximum number of items to return.</param>
        /// <returns></returns>
        public BrowseRequest<T> Limit(int limit)
        {
            this.limit = limit;
            return this;
        }

        /// <summary>
        /// Set the offset to the list of browsed items (enables paging).
        /// </summary>
        /// <param name="offset">The offset to the list of browsed items.</param>
        /// <returns></returns>
        public BrowseRequest<T> Offset(int offset)
        {
            this.offset = offset;
            return this;
        }

        /// <summary>
        /// Set the type of the release or release-group to browse.
        /// </summary>
        /// <param name="type">The release type (for example 'album').</param>
        /// <returns></returns>
        /// <remarks>
        /// See https://musicbrainz.org/doc/Development/XML_Web_Service/Version_2#Release_Type_and_Status for supported values.
        /// </remarks>
        public BrowseRequest<T> Type(string type)
        {
            this.type = type;
            return this;
        }

        /// <summary>
        /// Set the status of the release to browse.
        /// </summary>
        /// <param name="status">The release status (for example 'official').</param>
        /// <returns></returns>
        /// <remarks>
        /// See https://musicbrainz.org/doc/Development/XML_Web_Service/Version_2#Release_Type_and_Status for supported values.
        /// </remarks>
        public BrowseRequest<T> Status(string status)
        {
            this.status = status;
            return this;
        }

        /// <summary>
        /// Execute the browse request.
        /// </summary>
        /// <param name="ct">The cancellation token.</param>
        /// <returns></returns>
        public async Task<QueryResult<T>> GetAsync(CancellationToken ct = default)
        {
            return await BrowseAsync(client, ct);
        }

        /// <summary>
        /// Initiate the actual request.
        /// </summary>
        protected abstract Task<QueryResult<T>> BrowseAsync(MusicBrainzClient client, CancellationToken ct);

        /// <summary>
        /// Returns the request path.
        /// </summary>
        public abstract override string ToString();
    }

    #region Private implementation

    [DataContract]
    class ArtistListBrowse
    {
        [DataMember(Name = "artist-count")]
        public int Count { get; set; }

        [DataMember(Name = "artist-offset")]
        public int Offset { get; set; }

        [DataMember(Name = "artists")]
        public List<Artist> Items { get; set; }
    }

    class ArtistBrowseRequest : BrowseRequest<Artist>
    {
        public ArtistBrowseRequest(MusicBrainzClient client, UrlBuilder builder, string id, string relatedEntity, string entity)
            : base(client, builder, id, relatedEntity, entity)
        {
        }

        /// <inheritdoc />
        protected override async Task<QueryResult<Artist>> BrowseAsync(MusicBrainzClient client, CancellationToken ct)
        {
            string url = builder.CreateBrowseUrl(EntityName, relatedEntity, id, limit, offset, include);

            var list = await client.GetAsync<ArtistListBrowse>(url, ct);

            return new QueryResult<Artist>() { Items = list.Items, Count = list.Count, Offset = list.Offset };
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return builder.CreateBrowseUrl(EntityName, relatedEntity, id, limit, offset, include);
        }
    }

    [DataContract]
    class LabelListBrowse
    {
        [DataMember(Name = "label-count")]
        public int Count { get; set; }

        [DataMember(Name = "label-offset")]
        public int Offset { get; set; }

        [DataMember(Name = "labels")]
        public List<Label> Items { get; set; }
    }

    class LabelBrowseRequest : BrowseRequest<Label>
    {
        public LabelBrowseRequest(MusicBrainzClient client, UrlBuilder builder, string id, string relatedEntity, string entity)
            : base(client, builder, id, relatedEntity, entity)
        {
        }

        /// <inheritdoc />
        protected override async Task<QueryResult<Label>> BrowseAsync(MusicBrainzClient client, CancellationToken ct)
        {
            string url = builder.CreateBrowseUrl(EntityName, relatedEntity, id, limit, offset, include);

            var list = await client.GetAsync<LabelListBrowse>(url, ct);

            return new QueryResult<Label>() { Items = list.Items, Count = list.Count, Offset = list.Offset };
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return builder.CreateBrowseUrl(EntityName, relatedEntity, id, limit, offset, include);
        }
    }

    [DataContract]
    class RecordingListBrowse
    {
        [DataMember(Name = "recording-count")]
        public int Count { get; set; }

        [DataMember(Name = "recording-offset")]
        public int Offset { get; set; }

        [DataMember(Name = "recordings")]
        public List<Recording> Items { get; set; }
    }

    class RecordingBrowseRequest : BrowseRequest<Recording>
    {
        public RecordingBrowseRequest(MusicBrainzClient client, UrlBuilder builder, string id, string relatedEntity, string entity)
            : base(client, builder, id, relatedEntity, entity)
        {
        }

        /// <inheritdoc />
        protected override async Task<QueryResult<Recording>> BrowseAsync(MusicBrainzClient client, CancellationToken ct)
        {
            string url = builder.CreateBrowseUrl(EntityName, relatedEntity, id, limit, offset, include);

            var list = await client.GetAsync<RecordingListBrowse>(url, ct);

            return new QueryResult<Recording>() { Items = list.Items, Count = list.Count, Offset = list.Offset };
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return builder.CreateBrowseUrl(EntityName, relatedEntity, id, limit, offset, include);
        }
    }

    [DataContract]
    class ReleaseGroupListBrowse
    {
        [DataMember(Name = "release-group-count")]
        public int Count { get; set; }

        [DataMember(Name = "release-group-offset")]
        public int Offset { get; set; }

        [DataMember(Name = "release-groups")]
        public List<ReleaseGroup> Items { get; set; }
    }

    class ReleaseGroupBrowseRequest : BrowseRequest<ReleaseGroup>
    {
        public ReleaseGroupBrowseRequest(MusicBrainzClient client, UrlBuilder builder, string id, string relatedEntity, string entity)
            : base(client, builder, id, relatedEntity, entity)
        {
        }

        /// <inheritdoc />
        protected override async Task<QueryResult<ReleaseGroup>> BrowseAsync(MusicBrainzClient client, CancellationToken ct)
        {
            string url = builder.CreateBrowseUrl(EntityName, relatedEntity, id, type, null, limit, offset, include);

            var list = await client.GetAsync<ReleaseGroupListBrowse>(url, ct);

            return new QueryResult<ReleaseGroup>() { Items = list.Items, Count = list.Count, Offset = list.Offset };
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return builder.CreateBrowseUrl(EntityName, relatedEntity, id, type, null, limit, offset, include);
        }
    }

    [DataContract]
    class ReleaseListBrowse
    {
        [DataMember(Name = "release-count")]
        public int Count { get; set; }

        [DataMember(Name = "release-offset")]
        public int Offset { get; set; }

        [DataMember(Name = "releases")]
        public List<Release> Items { get; set; }
    }

    class ReleaseBrowseRequest : BrowseRequest<Release>
    {
        public ReleaseBrowseRequest(MusicBrainzClient client, UrlBuilder builder, string id, string relatedEntity, string entity)
            : base(client, builder, id, relatedEntity, entity)
        {
        }

        /// <inheritdoc />
        protected override async Task<QueryResult<Release>> BrowseAsync(MusicBrainzClient client, CancellationToken ct)
        {
            string url = builder.CreateBrowseUrl(EntityName, relatedEntity, id, type, status, limit, offset, include);

            var list = await client.GetAsync<ReleaseListBrowse>(url, ct);

            return new QueryResult<Release>() { Items = list.Items, Count = list.Count, Offset = list.Offset };
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return builder.CreateBrowseUrl(EntityName, relatedEntity, id, type, status, limit, offset, include);
        }
    }

    #endregion
}
