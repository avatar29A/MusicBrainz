
namespace Hqub.MusicBrainz.Tests
{
    using Hqub.MusicBrainz.Cache;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    class EmbeddedResourceCache : IRequestCache
    {
        #region Singleton

        private static readonly object creationLock = new();
        private static EmbeddedResourceCache _default;

        public static EmbeddedResourceCache Instance
        {
            get
            {
                if (_default == null)
                {
                    lock (creationLock)
                    {
                        _default ??= new EmbeddedResourceCache();
                    }
                }

                return _default;
            }
        }

        #endregion

        private static readonly Dictionary<string, string> Data = new()
        {
            { "artist?query", "artist-search" },
            { "artist/", "artist-get" },
            { "label?query", "label-search" },
            { "label/", "label-get" },
            { "recording?query", "recording-search" },
            { "recording/", "recording-get" },
            { "release?query", "release-search" },
            { "release/", "release-get" },
            { "release-group?query", "releasegroup-search" },
            { "release-group/", "releasegroup-get" },
        };

        private const string PATH_TEMPLATE = "Hqub.MusicBrainz.Tests.Data.{0}.json";

        public Task Add(string request, Stream response)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TryGetCachedItem(string request, out Stream stream)
        {
            foreach (var i in Data)
            {
                if (request.Contains(i.Key))
                {
                    var path = string.Format(PATH_TEMPLATE, i.Value);

                    stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);

                    return Task.FromResult(true);
                }
            }

            // Do not go on calling the web service, but throw exception to signal
            // that the test data is not setup correctly.
            throw new Exception("Item not in cache.");
        }

        public Task<bool> Contains(string request)
        {
            return Task.FromResult(Data.Any(i => request.Contains(i.Key)));
        }
    }
}
