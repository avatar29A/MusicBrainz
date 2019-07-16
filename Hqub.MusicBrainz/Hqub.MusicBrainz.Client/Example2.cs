
using Hqub.MusicBrainz.API.Extensions;

namespace Hqub.MusicBrainz.Client
{
    using Hqub.MusicBrainz.API;
    using Hqub.MusicBrainz.API.Entities;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Setup an advanced search query to find a release using 'Release.SearchAsync'
    /// and get details including recordings using 'Release.GetAsync'.
    /// </summary>
    public class Example2
    {
        public static async Task Run()
        {
            await Search("Massive Attack", "Mezzanine");
        }

        public static async Task Search(string band, string album)
        {
            // Search for an artist by name.
            var artists = await Artist.SearchAsync(band.Quote());

            var artist = artists.Items.First();

            // Build an advanced query to search for the release.
            var query = new QueryParameters<Release>()
            {
                { "arid", artist.Id },
                { "release", album },
                { "type", "album" },
                { "status", "official" }
            };

            // Search for a release by title.
            var releases = await Release.SearchAsync(query);

            Console.WriteLine("Total matches for '{0}': {1}", album, releases.Count);

            // Get the oldest release (remember to sort out items with no date set).
            var release = releases.Items.Where(r => r.Date != null && IsCompactDisc(r)).OrderBy(r => r.Date).First();

            // Get detailed information of the release, including recordings.
            release = await Release.GetAsync(release.Id, "recordings", "url-rels");

            Console.WriteLine();
            Console.WriteLine("Details for {0} - {1} ({2})", artist.Name, release.Title, release.Date);
            Console.WriteLine();

            // Get the medium associated with the release.
            var medium = release.Media.First();

            foreach (var track in medium.Tracks)
            {
                // The song length.
                var length = TimeSpan.FromMilliseconds((int)track.Length);

                Console.WriteLine("{0,3}  {1}  ({2:m\\:ss})", track.Number, track.Recording.Title, length);
            }
        }

        private static bool IsCompactDisc(Release r)
        {
            if (r.Media == null || r.Media.Count == 0)
            {
                return false;
            }

            return r.Media[0].Format == "CD";
        }
    }
}
