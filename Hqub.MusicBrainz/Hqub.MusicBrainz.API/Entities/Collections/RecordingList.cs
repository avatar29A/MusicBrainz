
namespace Hqub.MusicBrainz.API.Entities.Collections
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class RecordingList : QueryResult
    {
        /// <summary>
        /// Gets or sets the list of artists.
        /// </summary>
        [DataMember(Name = "recordings")]
        public List<Recording> Items { get; set; }
    }
}
