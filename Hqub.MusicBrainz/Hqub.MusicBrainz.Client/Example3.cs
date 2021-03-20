
namespace Hqub.MusicBrainz.Client
{
    using Hqub.MusicBrainz.API;
    using Hqub.MusicBrainz.API.Entities;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Setup an advanced search query to find a release using 'ReleaseGroup.SearchAsync' and get
    /// details including artists, releases and related urls using 'ReleaseGroup.GetAsync'.
    /// </summary>
    public class Example3
    {
        public static async Task Run(MusicBrainzClient client)
        {
            await Search(client, "Massive Attack", "Mezzanine");
        }

        public static async Task Search(MusicBrainzClient client, string band, string album)
        {
            // Build an advanced query to search for the release.
            var query = new QueryParameters<ReleaseGroup>()
            {
                { "artist", band },
                { "releasegroup", album },
                { "type", "album" },
                { "status", "official" }
            };

            // Search for an release-group by title.
            var groups = await client.ReleaseGroups.SearchAsync(query);

            Console.WriteLine("Total matches for '{0} - {1}': {2}", band, album, groups.Count);

            // Get best match.
            var group = groups.Items.First();

            // Get detailed information of the release-group, including artists, releases, genres and related urls.
            group = await client.ReleaseGroups.GetAsync(group.Id, "artists", "releases", "genres", "url-rels");

            var artist = group.Credits.First().Artist;

            Console.WriteLine();
            Console.WriteLine("Official album releases of '{0} - {1}':", artist.Name, group.Title);
            Console.WriteLine();

            foreach (var item in group.Releases.OrderBy(r => r.Date))
            {
                Console.WriteLine("     {0} - {1}  {2}", item.Date.ToShortDate(), item.Id, item.Country);
            }

            Console.WriteLine();
            Console.WriteLine("Top 3 (of {0}) genres for the release group:", group.Genres.Count);
            Console.WriteLine();

            foreach (var item in group.Genres.OrderByDescending(r => r.Count).Take(3))
            {
                Console.WriteLine("     {0} ({1})", item.Name, item.Count);
            }

            // Check if there are lyrcis available for the album.
            var lyrics = group.Relations.Where(r => r.Type == "lyrics");

            if (lyrics.Count() > 0)
            {
                Console.WriteLine();
                Console.WriteLine("You can find lyrics for '{0} - {1}' at", artist.Name, group.Title);
                Console.WriteLine();
                Console.WriteLine("     {0}", lyrics.First().Url.Resource);
                Console.WriteLine();
            }

            // Check if there's a wikipedia page for the album.
            var wiki = group.Relations.Where(r => r.Type == "wikipedia");

            if (wiki.Count() > 0)
            {
                Console.WriteLine();
                Console.WriteLine("More info for '{0} - {1}' at", artist.Name, group.Title);
                Console.WriteLine();
                Console.WriteLine("     {0}", wiki.First().Url.Resource);
                Console.WriteLine();
            }
        }
    }
}
