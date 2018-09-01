
namespace Hqub.MusicBrainz.API.Entities
{
    using Hqub.MusicBrainz.API.Entities.Collections;
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    /// <summary>
    /// A recording is an entity in MusicBrainz which can be linked to tracks on releases. Each track must always
    /// be associated with a single recording, but a recording can be linked to any number of tracks. 
    /// </summary>
    /// <see href="https://musicbrainz.org/doc/Recording"/>
    [DataContract(Name = "recording")]
    public class Recording
    {
        public const string EntityName = "recording";

        #region Properties

        /// <summary>
        /// Gets or sets the score (only available in search results).
        /// </summary>
        [DataMember(Name = "score")]
        public int Score { get; set; }

        /// <summary>
        /// Gets or sets the MusicBrainz id.
        /// </summary>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        [DataMember(Name = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        [DataMember(Name = "length")]
        public int? Length { get; set; }

        /// <summary>
        /// Gets or sets the disambiguation.
        /// </summary>
        [DataMember(Name = "disambiguation")]
        public string Disambiguation { get; set; }

        /// <summary>
        /// Gets or sets the rating.
        /// </summary>
        [DataMember(Name = "rating")]
        public Rating Rating { get; set; }

        #endregion

        #region Subqueries

        /// <summary>
        /// Gets or sets a list of artists associated to this recording.
        /// </summary>
        /// <example>
        /// var e = await Recording.GetAsync(mbid, "artists");
        /// </example>
        [DataMember(Name = "artist-credit")]
        public List<NameCredit> Credits { get; set; }

        /// <summary>
        /// Gets or sets a list of releases associated to this recording.
        /// </summary>
        /// <example>
        /// var e = await Recording.GetAsync(mbid, "releases");
        /// </example>
        [DataMember(Name = "releases")]
        public List<Release> Releases { get; set; }

        /// <summary>
        /// Gets or sets a list of tags associated to this recording.
        /// </summary>
        /// <example>
        /// var e = await Recording.GetAsync(mbid, "tags");
        /// </example>
        [DataMember(Name = "tags")]
        public List<Tag> Tags { get; set; }

        /// <summary>
        /// Gets or sets a list of relations associated to this recording.
        /// </summary>
        /// <example>
        /// var e = await Recording.GetAsync(mbid, "url-rels");
        /// </example>
        [DataMember(Name = "relations")]
        public List<Relation> Relations { get; set; }

        #endregion

        #region Static methods

        [Obsolete("Use GetAsync() method.")]
        public static Recording Get(string id, params string[] inc)
        {
            return GetAsync(id, inc).Result;
        }

        [Obsolete("Use SearchAsync() method.")]
        public static RecordingList Search(string query, int limit = 25, int offset = 0)
        {
            return SearchAsync(query, limit, offset).Result;
        }

        [Obsolete("Use BrowseAsync() method.")]
        public static RecordingList Browse(string relatedEntity, string value, int limit = 25, int offset = 0, params string[] inc)
        {
            return BrowseAsync(relatedEntity, value, limit, offset, inc).Result;
        }

        /// <summary>
        /// Lookup an recording in the MusicBrainz database.
        /// </summary>
        /// <param name="id">The recording MusicBrainz id.</param>
        /// <param name="inc">A list of entities to include (subqueries).</param>
        /// <returns></returns>
        public static async Task<Recording> GetAsync(string id, params string[] inc)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException(string.Format(Resources.Messages.MissingParameter, "id"));
            }

            string url = WebServiceHelper.CreateLookupUrl(EntityName, id, inc);

            return await WebServiceHelper.GetAsync<Recording>(url);
        }

        /// <summary>
        /// Search for an recording in the MusicBrainz database, matching the given query.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="limit">The maximum number of recordings to return (default = 25).</param>
        /// <param name="offset">The offset to the recordings list (enables paging, default = 0).</param>
        /// <returns></returns>
        public static async Task<RecordingList> SearchAsync(string query, int limit = 25, int offset = 0)
        {
            if (string.IsNullOrEmpty(query))
            {
                throw new ArgumentException(string.Format(Resources.Messages.MissingParameter, "query"));
            }

            string url = WebServiceHelper.CreateSearchTemplate(EntityName, query, limit, offset);

            return await WebServiceHelper.GetAsync<RecordingList>(url);
        }

        /// <summary>
        /// Search for an recording in the MusicBrainz database, matching the given query.
        /// </summary>
        /// <param name="query">The query parameters.</param>
        /// <param name="limit">The maximum number of recordings to return (default = 25).</param>
        /// <param name="offset">The offset to the recordings list (enables paging, default = 0).</param>
        /// <returns></returns>
        public static async Task<RecordingList> SearchAsync(QueryParameters<Recording> query, int limit = 25, int offset = 0)
        {
            return await SearchAsync(query.ToString(), limit, offset);
        }

        /// <summary>
        /// Browse all the recordings in the MusicBrainz database, which are directly linked to the entity with given id.
        /// </summary>
        /// <param name="entity">The name of the related entity.</param>
        /// <param name="id">The id of the related entity.</param>
        /// <param name="limit">The maximum number of recordings to return (default = 25).</param>
        /// <param name="offset">The offset to the recordings list (enables paging, default = 0).</param>
        /// <param name="inc">A list of entities to include (subqueries).</param>
        /// <returns></returns>
        public static async Task<RecordingList> BrowseAsync(string entity, string id, int limit = 25, int offset = 0, params string[] inc)
        {
            string url = WebServiceHelper.CreateBrowseTemplate(EntityName, entity, id, limit, offset, inc);

            return await WebServiceHelper.GetAsync<RecordingList>(url);
        }

        #endregion
    }
}
