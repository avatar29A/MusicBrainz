
namespace Hqub.MusicBrainz.Client
{
    using Hqub.MusicBrainz.API;
    using Hqub.MusicBrainz.API.Entities;
    using Hqub.MusicBrainz.API.Entities.Collections;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Example using the fluent API.
    /// </summary>
    public class Example6
    {
        public static async Task Run(MusicBrainzClient client)
        {
            await Search(client, "alternative rock", 10);
        }

        public static async Task Search(MusicBrainzClient client, string gernre, int limit)
        {
            Console.WriteLine("Getting bands from the UK matching tag '{0}'", gernre);

            var query = new QueryParameters<Artist>()
            {
                { "tag", gernre },
                { "type", "group" },
                { "country", "GB" }
            };

            // Search for artist.
            var artists = await client.Artists.Search(query).Limit(limit).GetAsync();

            Console.WriteLine();
            Console.WriteLine("The first {0} of {1} bands:", limit, artists.Count);
            Console.WriteLine();

            DisplayArtists(artists);

            var artist = artists.Items[DateTime.Now.Second % artists.Items.Count];

            Console.WriteLine();
            Console.WriteLine("Choose random artist '{0}' and browse official album releases:", artist.Name);

            // Create request.
            var request = client.ReleaseGroups.Browse("artist", artist.Id).Limit(limit).Type("album");

            // Browse release-groups.
            var groups = await request.GetAsync();

            int pages = Math.Max(1, groups.Count / limit);

            // Print the first page
            DisplayReleases(groups, 1, pages);

            const int MAX_PAGES = 5;

            int i = 1;

            for (; i < pages && i < MAX_PAGES; i++)
            {
                // Advance browse offset and fetch results.
                groups = await request.Offset(i * limit).GetAsync();

                DisplayReleases(groups, i + 1, pages);

                // Try to avoid rate limit ...
                await Task.Delay(500);
            }

            if (i == MAX_PAGES)
            {
                Console.WriteLine();
                Console.WriteLine("Stopping at page {0} ...", i);
            }
        }

        private static void DisplayArtists(ArtistList artists)
        {
            foreach (var a in artists)
            {
                var s = a.LifeSpan;
                Console.WriteLine("     {0} - {1}  {2}", s.Begin.ToShortDate(), s.End.ToShortDate(), a.Name);
            }
        }

        private static void DisplayReleases(ReleaseGroupList groups, int i, int pages)
        {
            var selection = groups.Where(g => IsOfficial(g));

            Console.WriteLine();
            Console.WriteLine("Page {0} of {1}", i, pages);
            Console.WriteLine();

            var color = Console.ForegroundColor;

            foreach (var g in groups)
            {
                Console.ForegroundColor = IsOfficial(g) ? color : ConsoleColor.DarkGray;

                Console.WriteLine("     {0}  {1}", g.FirstReleaseDate.ToShortDate(), g.Title);
            }

            Console.ForegroundColor = color;
        }

        static bool IsOfficial(ReleaseGroup g)
        {
            return g.PrimaryType.Equals("album", StringComparison.OrdinalIgnoreCase)
                && g.SecondaryTypes.Count == 0
                && !string.IsNullOrEmpty(g.FirstReleaseDate);
        }
    }
}
