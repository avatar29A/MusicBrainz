
namespace Hqub.MusicBrainz.API.Entities
{
    using System.Runtime.Serialization;

    [DataContract(Name = "relation")]
    public class Relation
    {
        public const string EntityName = "relation";

        /// <summary>
        /// Gets or sets the relation type.
        /// </summary>
        [DataMember(Name = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the relation type ID.
        /// </summary>
        [DataMember(Name = "type-id")]
        public string TypeId { get; set; }

        /// <summary>
        /// Gets or sets the relation target type.
        /// </summary>
        [DataMember(Name = "target-type")]
        public string TargetType { get; set; }

        /// <summary>
        /// Gets or sets the relation direction.
        /// </summary>
        [DataMember(Name = "direction")]
        public string Direction { get; set; }

        // NOTE: using derived classes and the KnownTypes arttibute does not work,
        //       so we add the relations explicitly (at the moment, only url-rels
        //       are included).
        
        /// <summary>
        /// Gets or sets the url relationship (include url-rels).
        /// </summary>
        [DataMember(Name = "url")]
        public Url Url { get; set; }

        // Other relationships:
        //
        //   /// <summary>
        //   /// Gets or sets the artist relationship (include artist-rels).
        //   /// </summary>
        //   [DataMember(Name = "artist")]
        //   public Artist Artist { get; set; }
        //
        //   /// <summary>
        //   /// Gets or sets the work relationship (include work-rels).
        //   /// </summary>
        //   [DataMember(Name = "work")]
        //   public Work Work { get; set; }
        //
        //   /// <summary>
        //   /// Gets or sets the release relationship (include release-rels).
        //   /// </summary>
        //   [DataMember(Name = "release")]
        //   public Release Release { get; set; }
        //
        //   /// <summary>
        //   /// Gets or sets the release relationship (include release-rels).
        //   /// </summary>
        //   [DataMember(Name = "release")]
        //   public Release Release { get; set; }
        //
    }
}
