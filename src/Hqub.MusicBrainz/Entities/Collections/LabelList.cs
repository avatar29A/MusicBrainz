
namespace Hqub.MusicBrainz.Entities.Collections
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    class LabelList
    {
        [DataMember(Name = "count")]
        public int Count { get; set; }

        [DataMember(Name = "offset")]
        public int Offset { get; set; }

        [DataMember(Name = "labels")]
        public List<Label> Items { get; set; }
    }

    [DataContract]
    class LabelListBrowse
    {
        [DataMember(Name = "labels")]
        public List<Label> Items { get; set; }

        [DataMember(Name = "label-count")]
        public int Count { get; set; }

        [DataMember(Name = "label-offset")]
        public int Offset { get; set; }
    }
}
