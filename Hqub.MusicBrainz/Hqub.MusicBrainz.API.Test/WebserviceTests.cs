
namespace Hqub.MusicBrainz.API.Test
{
    using NUnit.Framework;
    using System.Linq;
    using System.Threading.Tasks;

    [Ignore("Ignore for offline testing.")]
    public class WebserviceTests
    {
        MusicBrainzClient client = new MusicBrainzClient();

        [Test]- //test 
        public async Task TestArtistGetAsync()
        {
            var artist = await client.Artists.GetAsync("c3cceeed-3332-4cf0-8c4c-bbde425147b6");

            Assert.IsNotNull(artist, "Artist not found.");

            Assert.AreEqual("scorpions", artist.Name.ToLower(),
                string.Format("Expected 'Scorpions' instead '{0}'.", artist.Name));
        }

        [Test]
        public async Task TestArtistSearchAsync()
        {
            var artists = (await client.Artists.SearchAsync("scorpions")).Items;

            Assert.AreNotEqual(0, artists.Count, "Results is Empty.");
        }

        [Test]
        public async Task TestReleaseGetAsync()
        {
            var release = await client.Releases.GetAsync("ffad013a-4f64-44dd-bfb3-c6360fbd042d");

            Assert.IsNotNull(release, "Release not found.");

            Assert.AreEqual("comeblack", release.Title.ToLower(),
                string.Format("Album title = {0}, expect Comeblack", release.Title));
        }

        [Test]
        public async Task TestReleaseSearchAsync()
        {
            var releases = (await client.Releases.SearchAsync("Comeblack")).Items;

            Assert.AreNotEqual(0, releases.Count, "Result is empty");
        }

        [Test]
        public async Task TestReleaseBrowseAsync()
        {
            var artists = (await client.Artists.SearchAsync("The Scorpions")).Items;

            Assert.AreNotEqual(artists.Count, 0);

            var artist = artists.First();

            var releases = (await client.Releases.BrowseAsync("artist", artist.Id, 40)).Items;
            Assert.AreEqual(releases.Count, 40);
        }

        [Test]
        public async Task TestRecordingGetAsync()
        {
            var recording = await client.Recordings.GetAsync("fc4d4d9c-58b7-4dba-a608-753ea752ccce");

            Assert.IsNotNull(recording, "Record not found");

            Assert.AreEqual("the wind of change", recording.Title.ToLower(),
                string.Format("Expect 'The Wind Of Change' instead {0}", recording.Title));
        }

        [Test]
        public async Task TestRecordingSearchAsync()
        {
            var recordings = (await client.Recordings.SearchAsync("The Wind of Change")).Items;

            Assert.AreNotEqual(0, recordings.Count, "Result is empty");
        }

        [Test]
        public async Task TestRecordingBrowseAsync()
        {
            var artists = (await client.Artists.SearchAsync("The Scorpions")).Items;

            Assert.AreNotEqual(artists.Count, 0);

            var artist = artists.First();

            var releases = (await client.Recordings.BrowseAsync("artist", artist.Id, 40)).Items;
            Assert.AreEqual(releases.Count, 40);
        }
    }
}
