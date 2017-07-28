
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
        /// Gets or sets the relation target.
        /// </summary>
        [DataMember(Name = "target")]
        public string Target { get; set; }
    }
}
