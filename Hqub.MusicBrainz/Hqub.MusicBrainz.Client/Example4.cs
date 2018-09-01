
namespace Hqub.MusicBrainz.Client
{
    using Hqub.MusicBrainz.API;
    using Hqub.MusicBrainz.API.Entities;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Setup an advanced search query to find a recording using 'Recording.SearchAsync'
    /// and get details including related work using 'Recording.GetAsync'. Then get info
    /// for the related work (like lyrics) using 'Work.GetAsync'.
    /// </summary>
    class Example4
    {
        public static async Task Run()
        {
            await Search("Massive Attack", "Mezzanine", "Teardrop");
        }

        public static async Task Search(string artist, string album, string song)
        {
            // Build an advanced query to search for the recording.
            var query = new QueryParameters<Recording>()
            {
                { "artist", artist },
                { "release", album },
                { "recording", song }
            };

            // Search for a recording by title.
            var recordings = await Recording.SearchAsync(query);

            Console.WriteLine("Total matches for '{0} ({1}) {2}': {3}", artist, album, song, recordings.Count);

            // Get exact match for given song.
            var recording = recordings.Items.Where(r => r.Title == song).First();

            var release = recording.Releases.Where(r => r.Title == album).First();

            // Get detailed information of the recording, including related works.
            recording = await Recording.GetAsync(recording.Id, "work-rels");
            
            // Expect only a single work related to recording.
            var work = recording.Relations.Single().Work;

            // Get detailed information of the work, including related urls.
            work = await Work.GetAsync(work.Id, "url-rels");

            // Check if there are lyrcis available for the recording.
            var lyrics = work.Relations.Where(r => r.Type == "lyrics");

            if (lyrics.Count() > 0)
            {
                Console.WriteLine();
                Console.WriteLine("You can find lyrics for '{0} - {1} ({2})' at", artist, recording.Title, release.Date.ToShortDate());
                Console.WriteLine();
                Console.WriteLine("     {0}", lyrics.First().Url.Resource);
                Console.WriteLine();
            }
        }
    }
}
