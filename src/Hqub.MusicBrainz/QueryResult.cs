using System.Collections;
using System.Collections.Generic;

namespace Hqub.MusicBrainz
{
    /// <summary>
    /// Abstract base class for MusicBrainz queries returning lists (with paging support).
    /// </summary>
    public class QueryResult<T> : IEnumerable<T>
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
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the list offset (only available in search requests).
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// Gets or sets the list of entities.
        /// </summary>
        public List<T> Items { get; set; }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return Items.GetEnumerator();
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
