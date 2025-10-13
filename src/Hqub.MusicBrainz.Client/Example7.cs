
namespace Hqub.MusicBrainz.Client
{
    using Hqub.MusicBrainz;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Search for a label using <c>client.Labels.SearchAsync()</c> and lookup label details
    /// like releases and related urls using <c>client.Labels.GetAsync()</c>.
    /// </summary>
    public class Example7
    {
        public static async Task Run(MusicBrainzClient client)
        {
            await Search(client, "City Slang");
        }

        public static async Task Search(MusicBrainzClient client, string name)
        {
            // Search for a label by name (limit to 20 matches).
            var labels = await client.Labels.SearchAsync(name.Quote(), 20);

            Console.WriteLine("Total matches for '{0}': {1}", name, labels.Count);

            // Count matches with score 100.
            int count = labels.Items.Count(a => a.Score == 100);

            Console.WriteLine("Exact matches for '{0}': {1}", name, count);

            var label = labels.First();

            // Get detailed information of the label, including genres and related urls.
            label = await client.Labels.GetAsync(label.Id, "genres", "url-rels");

            Console.WriteLine();
            Console.WriteLine("Genres tagged for '{0}':", label.Name);
            Console.WriteLine();

            foreach (var genre in label.Genres)
            {
                Console.WriteLine("     {0} ({1})", genre.Name, genre.Count);
            }

            var urls = label.Relations.Where(r => r.TargetType == "url" && r.Type == "official site");

            if (urls.Any())
            {
                Console.WriteLine();
                Console.WriteLine("Official web sites of '{0}':", label.Name);
                Console.WriteLine();

                foreach (var relation in urls)
                {
                    Console.WriteLine("     {0}", relation.Url.Resource);
                }
            }

            Console.WriteLine();
            Console.WriteLine("Releases published by '{0}':", label.Name);
            Console.WriteLine();

            int limit = 30;

            var releases = await client.Releases.BrowseAsync("label", label.Id, limit, 0, "artist-credits");

            foreach (var release in releases.OrderBy(r => r.Credits.FirstOrDefault()?.Name + r.Title))
            {
                Console.WriteLine("     {0} {1} - {2} / {3}", release.Date.ToShortDate(), release.Country ?? "  ",
                    release.Credits.FirstOrDefault()?.Name, release.Title);
            }

            Console.WriteLine();

            if (releases.Items.Count == limit)
            {
                Console.WriteLine("There are probably more items to browse ({0} releases total) ...", releases.Count);
            }
        }
    }
}
