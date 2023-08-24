
namespace Hqub.MusicBrainz.API.Cache
{
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// A cache that does not cache anything.
    /// </summary>
    public class NullCache : IRequestCache
    {
        /// <summary>
        ///
        /// Gets the default <see cref="NullCache"/> instance.
        /// </summary>
        public static NullCache Default { get; } = new NullCache();

        private NullCache()
        {
        }

        /// <inheritdoc />
        public Task Add(string request, Stream response)
        {
#if NET5_0_OR_GREATER
            return Task.CompletedTask;
#else
            return Task.FromResult(0);
#endif
        }

        /// <inheritdoc />
        public Task<bool> TryGetCachedItem(string request, out Stream stream)
        {
            stream = null;

            return Task.FromResult(false);
        }
    }
}
