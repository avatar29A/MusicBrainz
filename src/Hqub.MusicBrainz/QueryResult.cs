using Hqub.MusicBrainz.Entities;
using System.Collections;
using System.Collections.Generic;

namespace Hqub.MusicBrainz
{
    /// <summary>
    /// Base class for MusicBrainz queries returning lists (with paging support).
    /// </summary>
    public sealed class QueryResult<T> : IEnumerable<T> where T : IEntity
    {
        /// <summary>
        /// Gets the total list items count.
        /// </summary>
        /// <remarks>
        /// This might be different form the actual list items count. If the list was
        /// generated from a search request, this property will return the total number
        /// of available items (on the server), while the number of returned items is
        /// limited by the requests 'limit' parameter (default = 25).
        /// </remarks>
        public int Count { get; }

        /// <summary>
        /// Gets the list offset (only available in search requests).
        /// </summary>
        public int Offset { get; }

        /// <summary>
        /// Gets the list of entities.
        /// </summary>
        public IReadOnlyList<T> Items { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryResult{T}"/> class.
        /// </summary>
        public QueryResult(int count, int offset, List<T> list)
        {
            Count = count;
            Offset = offset;
            Items = list;
        }

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
