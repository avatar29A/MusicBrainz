
namespace Hqub.MusicBrainz.API.Entities.Collections
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class ReleaseList : QueryResult
    {
        /// <summary>
        /// Gets or sets the list of artists.
        /// </summary>
        [DataMember(Name = "releases")]
        public List<Release> Items { get; set; }
    }
}
