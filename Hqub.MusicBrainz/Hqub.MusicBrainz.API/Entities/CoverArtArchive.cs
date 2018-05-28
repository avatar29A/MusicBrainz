﻿using System;
using System.Runtime.Serialization;

namespace Hqub.MusicBrainz.API.Entities
{
    [DataContract(Name = "cover-art-archive")]
    public class CoverArtArchive
    {
        /// <summary>
        /// Gets or sets a value indicating whether artwork is available or not.
        /// </summary>
        [DataMember(Name = "artwork")]
        public bool Artwork { get; set; }

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        [DataMember(Name = "count")]
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a front crover is available or not.
        /// </summary>
        [DataMember(Name = "front")]
        public bool Front { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a back crover is available or not.
        /// </summary>
        [DataMember(Name = "back")]
        public bool Back { get; set; }

        /// <summary>
        /// Gets or sets a value to the CoverArtUri
        /// </summary>
        public string CoverArtUri { get; set; }

        public static Uri GetCoverArtUri(string releaseId)
        {
            string url = "http://coverartarchive.org/release/" + releaseId + "/front-250.jpg";
            return new Uri(url, UriKind.RelativeOrAbsolute);
        }
    }
}
