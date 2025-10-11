
namespace Hqub.MusicBrainz.Entities.Collections
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    class RecordingList
    {
        [DataMember(Name = "count")]
        public int Count { get; set; }

        [DataMember(Name = "offset")]
        public int Offset { get; set; }

        [DataMember(Name = "recordings")]
        public List<Recording> Items { get; set; }
    }

    // NOTE: for MusicBrainz ws/3 this additional class might no longer be necessary.
    //       See https://tickets.metabrainz.org/browse/MBS-9731

    [DataContract]
    class RecordingListBrowse
    {
        [DataMember(Name = "recordings")]
        public List<Recording> Items { get; set; }

        [DataMember(Name = "recording-count")]
        public int Count { get; set; }

        [DataMember(Name = "recording-offset")]
        public int Offset { get; set; }
    }
}
