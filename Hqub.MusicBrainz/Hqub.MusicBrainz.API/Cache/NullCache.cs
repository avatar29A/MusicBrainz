
namespace Hqub.MusicBrainz.API.Cache
{
    using System.IO;

    /// <summary>
    /// A cache that does not cache anything.
    /// </summary>
    class NullCache : IRequestCache
    {
        /// <summary>
        /// Gets the default <see cref="NullCache"/> instance.
        /// </summary>
        public static NullCache Default { get; } = new NullCache();

        private NullCache()
        {
        }

        /// <inheritdoc />
        public void Add(string request, Stream response)
        {
        }

        /// <inheritdoc />
        public bool TryGetCachedItem(string request, out Stream stream)
        {
            stream = null;
            return false;
        }
    }
}
