
namespace Hqub.MusicBrainz.API.Cache
{
    using System.IO;

    /// <summary>
    /// A simple cache interface.
    /// </summary>
    public interface IRequestCache
    {
        /// <summary>
        /// Add a request and its response to the cache.
        /// </summary>
        void Add(string request, Stream response);

        /// <summary>
        /// Add a request and its response to the cache.
        /// </summary>
        /// <returns>True, if a cache entry matching the request was found.</returns>
        bool TryGetCachedItem(string request, out Stream stream);

        /// <summary>
        /// Remove all expired entries.
        /// </summary>
        /// <returns>The number of removed entries.</returns>
        int Cleanup();

        /// <summary>
        /// Remove all entries from the cache.
        /// </summary>
        void Clear();
    }
}
