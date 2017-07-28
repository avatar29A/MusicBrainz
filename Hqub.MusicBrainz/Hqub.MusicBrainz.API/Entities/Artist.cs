
namespace Hqub.MusicBrainz.API.Entities
{
    using Hqub.MusicBrainz.API.Entities.Collections;
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Threading.Tasks;

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

        [DataMember(Name = "recordings")]
        public List<Recording> Recordings { get; set; }

        [DataMember(Name = "release-groups")]
        public List<ReleaseGroup> ReleaseGroups { get; set; }

        [DataMember(Name = "releases")]
        public List<Release> Releases { get; set; }

        [DataMember(Name = "relations")]
        public List<Relation> Relations { get; set; }

        [DataMember(Name = "works")]
        public List<Work> Works { get; set; }

        [DataMember(Name = "tags")]
        public List<Tag> Tags { get; set; }

        #endregion

        #region Static Methods

        [Obsolete("Use GetAsync() method.")]
        public static Artist Get(string id, params string[] inc)
        {
            return GetAsync<Artist>(EntityName, id, inc).Result;
        }

        [Obsolete("Use SearchAsync() method.")]
        public static ArtistList Search(string query, int limit = 25, int offset = 0)
        {
            return SearchAsync<ArtistMetadata>(EntityName,
                query, limit, offset).Result.Collection;
        }

        [Obsolete("Use BrowseAsync() method.")]
        public static ArtistList Browse(string relatedEntity, string value, int limit = 25, int offset = 0, params  string[] inc)
        {
            return BrowseAsync<ArtistMetadata>(EntityName,
                relatedEntity, value, limit, offset, inc).Result.Collection;
        }

        /// <summary>
        /// Lookup an artist in the MusicBrainz database.
        /// </summary>
        /// <param name="id">The artist MusicBrainz id.</param>
        /// <param name="inc">A list of entities to include (subqueries).</param>
        /// <returns></returns>
        public async static Task<Artist> GetAsync(string id, params string[] inc)
        {
            return await GetAsync<Artist>(EntityName, id, inc);
        }

        /// <summary>
        /// Search for an artist in the MusicBrainz database, matching the given query.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="limit">The maximum number of artists to return (default = 25).</param>
        /// <param name="offset">The offset to the artists list (enables paging, default = 0).</param>
        /// <returns></returns>
        public async static Task<ArtistList> SearchAsync(string query, int limit = 25, int offset = 0)
        {
            return (await SearchAsync<ArtistMetadata>(EntityName,
                query, limit, offset)).Collection;
        }

        /// <summary>
        /// Search for an artist in the MusicBrainz database, matching the given query.
        /// </summary>
        /// <param name="query">The query parameters.</param>
        /// <param name="limit">The maximum number of artists to return (default = 25).</param>
        /// <param name="offset">The offset to the artists list (enables paging, default = 0).</param>
        /// <returns></returns>
        public async static Task<ArtistList> SearchAsync(QueryParameters<Artist> query, int limit = 25, int offset = 0)
        {
            return (await SearchAsync<ArtistMetadata>(EntityName,
                query.ToString(), limit, offset)).Collection;
        }

        /// <summary>
        /// Browse all the artists in the MusicBrainz database, which are directly linked to the
        /// entity with given id.
        /// </summary>
        /// <param name="entity">The name of the related entity.</param>
        /// <param name="id">The id of the related entity.</param>
        /// <param name="limit">The maximum number of artists to return (default = 25).</param>
        /// <param name="offset">The offset to the artists list (enables paging, default = 0).</param>
        /// <param name="inc">A list of entities to include (subqueries).</param>
        /// <returns></returns>
        public async static Task<ArtistList> BrowseAsync(string entity, string id, int limit = 25, int offset = 0, params  string[] inc)
        {
            return (await BrowseAsync<ArtistMetadata>(EntityName, entity, id,
                limit, offset, inc)).Collection;
        }

        #endregion
    }
}
