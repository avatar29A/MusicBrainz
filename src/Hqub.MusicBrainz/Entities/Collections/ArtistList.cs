
namespace Hqub.MusicBrainz.Entities.Collections
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    // NOTE: for MusicBrainz ws/3 these additional classes might no longer be necessary.
    //       See https://tickets.metabrainz.org/browse/MBS-9731

    [DataContract]
    class ArtistList
    {
        [DataMember(Name = "count")]
        public int Count { get; set; }

        [DataMember(Name = "offset")]
        public int Offset { get; set; }

        [DataMember(Name = "artists")]
        public List<Artist> Items { get; set; }
    }

    [DataContract]
    class ArtistListBrowse
    {
        [DataMember(Name = "artists")]
        public List<Artist> Items { get; set; }

        [DataMember(Name = "artist-count")]
        public int Count { get; set; }

        [DataMember(Name = "artist-offset")]
        public int Offset { get; set; }
    }
}
