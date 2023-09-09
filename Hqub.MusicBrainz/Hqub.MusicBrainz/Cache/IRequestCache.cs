
namespace Hqub.MusicBrainz.API.Cache
{
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// A simple cache interface.
    /// </summary>
    public interface IRequestCache
    {
        /// <summary>
        /// Add a request and its response to the cache.
        /// </summary>
        /// <param name="request">The request url used to identify the cache item.</param>
        /// <param name="response">The MusicbBrainz webservice response stream.</param>
        Task Add(string request, Stream response);

        /// <summary>
        /// Try to find the cached response for given request url.
        /// </summary>
        /// <param name="request">The request url used to identify the cache item.</param>
        /// <param name="stream">The response stream read form cache.</param>
        /// <returns>True, if a cache entry matching the request was found.</returns>
        Task<bool> TryGetCachedItem(string request, out Stream stream);
    }
}
