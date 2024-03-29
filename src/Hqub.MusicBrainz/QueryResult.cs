﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Hqub.MusicBrainz
{
    /// <summary>
    /// Abstract base class for MusicBrainz queries returning lists (with paging support).
    /// </summary>
    [DataContract]
    public abstract class QueryResult<T> : IEnumerable<T>
    {
        /// <summary>
        /// Gets or sets the total list items count.
        /// </summary>
        /// <remarks>
        /// This might be different form the actual list items count. If the list was
        /// generated from a search request, this property will return the total number
        /// of available items (on the server), while the number of returned items is
        /// limited by the requests 'limit' parameter (default = 25).
        /// </remarks>
        [DataMember(Name = "count")]
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the list offset (only available in search requests).
        /// </summary>
        [DataMember(Name = "offset")]
        public int Offset { get; set; }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public abstract IEnumerator<T> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
