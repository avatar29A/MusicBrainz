using System;
using System.Collections.Generic;

namespace Hqub.MusicBrainz.API.Entities
{
    using System.Runtime.Serialization;

    [DataContract(Name = "area")]
    public class Area
    {
        /// <summary>
        /// Gets or sets the MusicBrainz id.
        /// </summary>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Disambiguation.
        /// </summary>
        [DataMember(Name = "disambiguation")]
        public string Disambiguation { get; set; }

        /// <summary>
        /// Gets or sets the iso-3166-1 codes.
        /// </summary>
        [DataMember(Name = "iso-3166-1-codes")]
        public List<string> IsoCodes { get; set; }
    }
}
