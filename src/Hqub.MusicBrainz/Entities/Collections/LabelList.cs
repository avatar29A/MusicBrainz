
namespace Hqub.MusicBrainz.Entities.Collections
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// List of labels returned by MusicBrainz search requests.
    /// </summary>
    [DataContract]
    public class LabelList : QueryResult<Label>
    {
        /// <summary>
        /// Gets or sets the list of labels.
        /// </summary>
        [DataMember(Name = "labels")]
        public List<Label> Items { get; set; }

        /// <inheritdoc />
        public override IEnumerator<Label> GetEnumerator()
        {
            return Items.GetEnumerator();
        }
    }

    // NOTE: for MusicBrainz ws/3 this additional class might no longer be necessary.
    //       See https://tickets.metabrainz.org/browse/MBS-9731

    /// <summary>
    /// List of labels returned by MusicBrainz browse requests.
    /// </summary>
    [DataContract]
    internal class LabelListBrowse
    {
        /// <summary>
        /// Gets or sets the list of labels.
        /// </summary>
        [DataMember(Name = "labels")]
        public List<Label> Items { get; set; }

        // NOTE: hide members of the base class to make serialization work

        /// <summary>
        /// Gets or sets the total list items count.
        /// </summary>
        [DataMember(Name = "label-count")]
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the list offset.
        /// </summary>
        [DataMember(Name = "label-offset")]
        public int Offset { get; set; }
    }
}
