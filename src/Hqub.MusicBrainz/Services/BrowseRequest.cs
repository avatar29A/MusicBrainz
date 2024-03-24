namespace Hqub.MusicBrainz.Services
{
    using Hqub.MusicBrainz.Entities.Collections;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Prepare a browse request to the MusicBrainz web service.
    /// </summary>
    /// <typeparam name="T">Any supported MusicBrainz entity.</typeparam>
    public abstract class BrowseRequest<T>
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
        public async Task<T> GetAsync(CancellationToken ct = default)
        {
            return await BrowseAsync(client, ct);
        }

        /// <summary>
        /// Initiate the actual request.
        /// </summary>
        protected abstract Task<T> BrowseAsync(MusicBrainzClient client, CancellationToken ct);

        /// <summary>
        /// Returns the request path.
        /// </summary>
        public abstract override string ToString();
    }

    #region Helper classes

    class ArtistBrowseRequest : BrowseRequest<ArtistList>
    {
        public ArtistBrowseRequest(MusicBrainzClient client, UrlBuilder builder, string id, string relatedEntity, string entity)
            : base(client, builder, id, relatedEntity, entity)
        {
        }

        /// <inheritdoc />
        protected override async Task<ArtistList> BrowseAsync(MusicBrainzClient client, CancellationToken ct)
        {
            string url = builder.CreateBrowseUrl(EntityName, relatedEntity, id, limit, offset, include);

            var list = await client.GetAsync<ArtistListBrowse>(url, ct);

            return new ArtistList() { Items = list.Items, Count = list.Count, Offset = list.Offset };
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return builder.CreateBrowseUrl(EntityName, relatedEntity, id, limit, offset, include);
        }
    }

    class RecordingBrowseRequest : BrowseRequest<RecordingList>
    {
        public RecordingBrowseRequest(MusicBrainzClient client, UrlBuilder builder, string id, string relatedEntity, string entity)
            : base(client, builder, id, relatedEntity, entity)
        {
        }

        /// <inheritdoc />
        protected override async Task<RecordingList> BrowseAsync(MusicBrainzClient client, CancellationToken ct)
        {
            string url = builder.CreateBrowseUrl(EntityName, relatedEntity, id, limit, offset, include);

            var list = await client.GetAsync<RecordingListBrowse>(url, ct);

            return new RecordingList() { Items = list.Items, Count = list.Count, Offset = list.Offset };
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return builder.CreateBrowseUrl(EntityName, relatedEntity, id, limit, offset, include);
        }
    }

    class ReleaseGroupBrowseRequest : BrowseRequest<ReleaseGroupList>
    {
        public ReleaseGroupBrowseRequest(MusicBrainzClient client, UrlBuilder builder, string id, string relatedEntity, string entity)
            : base(client, builder, id, relatedEntity, entity)
        {
        }

        /// <inheritdoc />
        protected override async Task<ReleaseGroupList> BrowseAsync(MusicBrainzClient client, CancellationToken ct)
        {
            string url = builder.CreateBrowseUrl(EntityName, relatedEntity, id, type, null, limit, offset, include);

            var list = await client.GetAsync<ReleaseGroupListBrowse>(url, ct);

            return new ReleaseGroupList() { Items = list.Items, Count = list.Count, Offset = list.Offset };
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return builder.CreateBrowseUrl(EntityName, relatedEntity, id, type, null, limit, offset, include);
        }
    }

    class ReleaseBrowseRequest : BrowseRequest<ReleaseList>
    {
        public ReleaseBrowseRequest(MusicBrainzClient client, UrlBuilder builder, string id, string relatedEntity, string entity)
            : base(client, builder, id, relatedEntity, entity)
        {
        }

        /// <inheritdoc />
        protected override async Task<ReleaseList> BrowseAsync(MusicBrainzClient client, CancellationToken ct)
        {
            string url = builder.CreateBrowseUrl(EntityName, relatedEntity, id, type, status, limit, offset, include);

            var list = await client.GetAsync<ReleaseListBrowse>(url, ct);

            return new ReleaseList() { Items = list.Items, Count = list.Count, Offset = list.Offset };
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return builder.CreateBrowseUrl(EntityName, relatedEntity, id, type, status, limit, offset, include);
        }
    }

    #endregion
}
