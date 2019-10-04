
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
    }
}
