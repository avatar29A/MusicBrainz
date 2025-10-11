
namespace Hqub.MusicBrainz.Entities.Collections
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

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

    [DataContract]
    class ReleaseListBrowse
    {
        [DataMember(Name = "releases")]
        public List<Release> Items { get; set; }

        [DataMember(Name = "release-count")]
        public int Count { get; set; }

        [DataMember(Name = "release-offset")]
        public int Offset { get; set; }
    }
}
