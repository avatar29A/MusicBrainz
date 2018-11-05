
namespace Hqub.MusicBrainz.API.Entities
{
    using Hqub.MusicBrainz.API.Entities.Collections;
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    /// <summary>
    /// An artist is generally a musician (or musician persona), group of musicians
    /// or other music professional (like a producer or engineer).
    /// </summary>
    /// <see href="https://musicbrainz.org/doc/Artist"/>
    [DataContract(Name = "artist")]
    public class Artist
    {
        public const string EntityName = "artist";

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
        /// Gets or sets the type.
        /// </summary>
        [DataMember(Name = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the sort name.
        /// </summary>
        [DataMember(Name = "sort-name")]
        public string SortName { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        [DataMember(Name = "gender")]
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the life-span.
        /// </summary>
        [DataMember(Name = "life-span")]
        public LifeSpan LifeSpan { get; set; }

        /// <summary>
        /// Gets or sets the area.
        /// </summary>
        [DataMember(Name = "area")]
        public Area Area { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        [DataMember(Name = "country")]
        public string Country { get; set; }

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
        /// Gets or sets a list of recordings associated to this artist.
        /// </summary>
        /// <example>
        /// var e = await Artist.GetAsync(mbid, "recordings");
        /// </example>
        [DataMember(Name = "recordings")]
        public List<Recording> Recordings { get; set; }

        /// <summary>
        /// Gets or sets a list of release-groups associated to this artist.
        /// </summary>
        /// <example>
        /// var e = await Artist.GetAsync(mbid, "release-groups");
        /// </example>
        [DataMember(Name = "release-groups")]
        public List<ReleaseGroup> ReleaseGroups { get; set; }

        /// <summary>
        /// Gets or sets a list of releases associated to this artist.
        /// </summary>
        /// <example>
        /// var e = await Artist.GetAsync(mbid, "releases");
        /// </example>
        [DataMember(Name = "releases")]
        public List<Release> Releases { get; set; }

        /// <summary>
        /// Gets or sets a list of works associated to this artist.
        /// </summary>
        /// <example>
        /// var e = await Artist.GetAsync(mbid, "works");
        /// </example>
        [DataMember(Name = "works")]
        public List<Work> Works { get; set; }

        /// <summary>
        /// Gets or sets a list of tags associated to this artist.
        /// </summary>
        /// <example>
        /// var e = await Artist.GetAsync(mbid, "tags");
        /// </example>
        [DataMember(Name = "tags")]
        public List<Tag> Tags { get; set; }

        /// <summary>
        /// Gets or sets a list of relations associated to this artist.
        /// </summary>
        /// <example>
        /// var e = await Artist.GetAsync(mbid, "url-rels", "artist-rels");
        /// </example>
        [DataMember(Name = "relations")]
        public List<Relation> Relations { get; set; }

        #endregion

        #region Static Methods

        [Obsolete("Use GetAsync() method.")]
        public static Artist Get(string id, params string[] inc)
        {
            return GetAsync(id, inc).Result;
        }

        [Obsolete("Use SearchAsync() method.")]
        public static ArtistList Search(string query, int limit = 25, int offset = 0)
        {
            return SearchAsync(query, limit, offset).Result;
        }

        [Obsolete("Use BrowseAsync() method.")]
        public static ArtistList Browse(string relatedEntity, string value, int limit = 25, int offset = 0, params string[] inc)
        {
            return BrowseAsync(relatedEntity, value, limit, offset, inc).Result;
        }

        /// <summary>
        /// Lookup an artist in the MusicBrainz database.
        /// </summary>
        /// <param name="id">The artist MusicBrainz id.</param>
        /// <param name="inc">A list of entities to include (subqueries).</param>
        /// <returns></returns>
        public static async Task<Artist> GetAsync(string id, params string[] inc)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException(string.Format(Resources.Messages.MissingParameter, "id"));
            }

            string url = WebServiceHelper.CreateLookupUrl(EntityName, id, inc);

            return await WebServiceHelper.GetAsync<Artist>(url);
        }

        /// <summary>
        /// Search for an artist in the MusicBrainz database, matching the given query.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="limit">The maximum number of artists to return (default = 25).</param>
        /// <param name="offset">The offset to the artists list (enables paging, default = 0).</param>
        /// <returns></returns>
        public static async Task<ArtistList> SearchAsync(string query, int limit = 25, int offset = 0)
        {
            if (string.IsNullOrEmpty(query))
            {
                throw new ArgumentException(string.Format(Resources.Messages.MissingParameter, "query"));
            }

            string url = WebServiceHelper.CreateSearchTemplate(EntityName, query, limit, offset);

            return await WebServiceHelper.GetAsync<ArtistList>(url);
        }

        /// <summary>
        /// Search for an artist in the MusicBrainz database, matching the given query.
        /// </summary>
        /// <param name="query">The query parameters.</param>
        /// <param name="limit">The maximum number of artists to return (default = 25).</param>
        /// <param name="offset">The offset to the artists list (enables paging, default = 0).</param>
        /// <returns></returns>
        public static async Task<ArtistList> SearchAsync(QueryParameters<Artist> query, int limit = 25, int offset = 0)
        {
            return await SearchAsync(query.ToString(), limit, offset);
        }

        /// <summary>
        /// Browse all the artists in the MusicBrainz database, which are directly linked to the entity with given id.
        /// </summary>
        /// <param name="entity">The name of the related entity.</param>
        /// <param name="id">The id of the related entity.</param>
        /// <param name="limit">The maximum number of artists to return (default = 25).</param>
        /// <param name="offset">The offset to the artists list (enables paging, default = 0).</param>
        /// <param name="inc">A list of entities to include (subqueries).</param>
        /// <returns></returns>
        public static async Task<ArtistList> BrowseAsync(string entity, string id, int limit = 25, int offset = 0, params string[] inc)
        {
            string url = WebServiceHelper.CreateBrowseTemplate(EntityName, entity, id, limit, offset, inc);

            return await WebServiceHelper.GetAsync<ArtistList>(url);
        }

        #endregion
    }
}
