
namespace Hqub.MusicBrainz.API.Entities.Collections
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class ReleaseGroupList : QueryResult
    {
        /// <summary>
        /// Gets or sets the list of artists.
        /// </summary>
        [DataMember(Name = "release-groups")]
        public List<ReleaseGroup> Items { get; set; }
    }
}
