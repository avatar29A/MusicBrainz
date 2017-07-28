
namespace Hqub.MusicBrainz.API.Entities.Collections
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class ArtistList : QueryResult
    {
        /// <summary>
        /// Gets or sets the list of artists.
        /// </summary>
        [DataMember(Name = "artists")]
        public List<Artist> Items { get; set; }
    }
}
