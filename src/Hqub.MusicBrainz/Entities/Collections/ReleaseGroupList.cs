
namespace Hqub.MusicBrainz.Entities.Collections
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

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

    [DataContract]
    class ReleaseGroupListBrowse
    {
        [DataMember(Name = "release-groups")]
        public List<ReleaseGroup> Items { get; set; }

        [DataMember(Name = "release-group-count")]
        public int Count { get; set; }

        [DataMember(Name = "release-group-offset")]
        public int Offset { get; set; }
    }
}
