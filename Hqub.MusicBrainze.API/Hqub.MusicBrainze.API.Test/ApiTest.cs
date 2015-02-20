using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Hqub.MusicBrainz.API.Test
{
    [TestClass]
    public class ApiTest
    {
        [TestMethod]
        public async Task CheckArtistGetAsync()
        {
            var artist = await Entities.Artist.GetAsync("c3cceeed-3332-4cf0-8c4c-bbde425147b6");

            Assert.IsNotNull(artist, "Artist not found.");

            Assert.AreEqual("scorpions", artist.Name.ToLower(),
                            string.Format("Expected 'Scorpions' instead '{0}'.", artist.Name));
        }

        [TestMethod]
        public async Task CheckArtistSearchAsync()
        {
            var artists = await Entities.Artist.SearchAsync("scorpions");

            Assert.AreNotEqual(0, artists.Count, "Results is Empty.");
        }

        [TestMethod]
        public async Task CheckReleaseGetAsync()
        {
            var release = await Entities.Release.GetAsync("ffad013a-4f64-44dd-bfb3-c6360fbd042d");

            Assert.IsNotNull(release, "Release not found.");

            Assert.AreEqual("comeblack", release.Title.ToLower(),
                               string.Format("Album title = {0}, expect Comeblack", release.Title));
        }

        [TestMethod]
        public async Task CheckReleaseSearchAsync()
        {
            var releases = await Entities.Release.SearchAsync("Comeblack");

            Assert.AreNotEqual(0, releases.Count, "Result is empty");
        }

        [TestMethod]
        public async Task CheckReleaseBrowseAsync()
        {
            var artists = await Entities.Artist.SearchAsync("The Scorpions"); 

            Assert.AreNotEqual(artists.Count, 0);

            var artist = artists.First();

            var releases = await Entities.Release.BrowseAsync("artist", artist.Id, 40);
            Assert.AreEqual(releases.Count, 40);
        }

        [TestMethod]
        public async Task CheckRecordingGetAsync()
        {
            var recording = await Entities.Recording.GetAsync("fc4d4d9c-58b7-4dba-a608-753ea752ccce");

            Assert.IsNotNull(recording, "Record not found");

            Assert.AreEqual("the wind of change", recording.Title.ToLower(),
                            string.Format("Expect 'The Wind Of Change' instead {0}", recording.Title));
        }

        [TestMethod]
        public async Task CheckRecordingSearchAsync()
        {
            var recordings = await Entities.Recording.SearchAsync("The Wind of Change");

            Assert.AreNotEqual(0, recordings.Count, "Result is empty");
        }

        [TestMethod]
        public async Task CheckRecordingBrowseAsync()
        {
            var artists = await Entities.Artist.SearchAsync("The Scorpions");

            Assert.AreNotEqual(artists.Count, 0);

            var artist = artists.First();

            var releases = await Entities.Recording.BrowseAsync("artist", artist.Id, 40);
            Assert.AreEqual(releases.Count, 40);
        }
    }
}
