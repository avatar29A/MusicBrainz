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

        // The following dictionary maps the request url to the test data (see
        // FetchTestData.cs in Hqub.MusicBrainz.Client project).

        private static readonly Dictionary<string, string> Data = new()
        {
            { "artist/", "artist-get" },
            { "artist?query", "artist-search" },
            { "artist?area", "artist-browse" },
            { "label/", "label-get" },
            { "label?query", "label-search" },
            { "label?area", "label-browse" },
            { "recording/", "recording-get" },
            { "recording?query", "recording-search" },
            { "recording?release", "recording-browse" },
            { "release/", "release-get" },
            { "release?query", "release-search" },
            { "release?label", "release-browse" },
            { "release-group/", "releasegroup-get" },
            { "release-group?query", "releasegroup-search" },
            { "release-group?artist", "releasegroup-browse" },
            { "area/", "area-get" },
            { "work/", "work-get" }
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
