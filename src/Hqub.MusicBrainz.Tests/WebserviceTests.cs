
namespace Hqub.MusicBrainz.Tests
{
    using NUnit.Framework;
    using System.Linq;
    using System.Threading.Tasks;

    [Ignore("Ignore for offline testing.")]
    public class WebserviceTests
    {
        private readonly MusicBrainzClient client = new MusicBrainzClient();

        [OneTimeTearDown]
        public void Dispose()
        {
            client.Dispose();
        }

        [Test]
        public async Task TestArtistGetAsync()
        {
            var artist = await client.Artists.GetAsync("c3cceeed-3332-4cf0-8c4c-bbde425147b6");

            Assert.That(artist, Is.Not.Null, "Artist not found.");

            Assert.That(artist.Name.ToLower(), Is.EqualTo("scorpions"),
                string.Format("Expected 'Scorpions' instead '{0}'.", artist.Name));
        }

        [Test]
        public async Task TestArtistSearchAsync()
        {
            var artists = (await client.Artists.SearchAsync("scorpions")).Items;

            Assert.That(artists.Count, Is.Not.EqualTo(0), "Results is Empty.");
        }

        [Test]
        public async Task TestReleaseGetAsync()
        {
            var release = await client.Releases.GetAsync("ffad013a-4f64-44dd-bfb3-c6360fbd042d");

            Assert.That(release, Is.Not.Null, "Release not found.");

            Assert.That(release.Title.ToLower(), Is.EqualTo("comeblack"),
                string.Format("Album title = {0}, expect Comeblack", release.Title));
        }

        [Test]
        public async Task TestReleaseSearchAsync()
        {
            var releases = (await client.Releases.SearchAsync("Comeblack")).Items;

            Assert.That(releases.Count, Is.Not.EqualTo(0), "Result is empty");
        }

        [Test]
        public async Task TestReleaseBrowseAsync()
        {
            var artists = (await client.Artists.SearchAsync("The Scorpions")).Items;

            Assert.That(artists.Count, Is.Not.EqualTo(0));

            var artist = artists.First();

            var releases = (await client.Releases.BrowseAsync("artist", artist.Id, 40)).Items;
            Assert.That(releases.Count, Is.EqualTo(40));
        }

        [Test]
        public async Task TestRecordingGetAsync()
        {
            var recording = await client.Recordings.GetAsync("fc4d4d9c-58b7-4dba-a608-753ea752ccce");

            Assert.That(recording, Is.Not.Null, "Record not found");

            Assert.That(recording.Title.ToLower(), Is.EqualTo("the wind of change"),
                string.Format("Expect 'The Wind Of Change' instead {0}", recording.Title));
        }

        [Test]
        public async Task TestRecordingSearchAsync()
        {
            var recordings = (await client.Recordings.SearchAsync("The Wind of Change")).Items;

            Assert.That(recordings.Count, Is.Not.EqualTo(0), "Result is empty");
        }

        [Test]
        public async Task TestRecordingBrowseAsync()
        {
            var artists = (await client.Artists.SearchAsync("The Scorpions")).Items;

            Assert.That(artists.Count, Is.Not.EqualTo(0));

            var artist = artists.First();

            var releases = (await client.Recordings.BrowseAsync("artist", artist.Id, 40)).Items;
            Assert.That(releases.Count, Is.EqualTo(40));
        }
    }
}
