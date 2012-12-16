using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hqub.MusicBrainze.API.Test
{
    [TestClass]
    public class ApiTest
    {
        [TestMethod]
        public void CheckArtistGet()
        {
            var artist = Entities.Artist.Get("c3cceeed-3332-4cf0-8c4c-bbde425147b6");

            Assert.IsNotNull(artist, "Artist not found.");

            Assert.AreEqual("scorpions", artist.Name.ToLower(),
                            string.Format("Expected 'Scorpions' instead '{0}'.", artist.Name));
        }

        [TestMethod]
        public void CheckArtistSearch()
        {
            var artists = Entities.Artist.Search("scorpions");

            Assert.AreNotEqual(0, artists.Count, "Results is Empty.");
        }

        [TestMethod]
        public void CheckReleaseGet()
        {
            var release = Entities.Release.Get("ffad013a-4f64-44dd-bfb3-c6360fbd042d");

            Assert.IsNotNull(release, "Release not found.");

            Assert.AreEqual("comeblack", release.Title.ToLower(),
                               string.Format("Album title = {0}, expect Comeblack", release.Title));
        }

        [TestMethod]
        public void CheckReleaseSearch()
        {
            var releases = Entities.Release.Search("Comeblack");

            Assert.AreNotEqual(0, releases.Count, "Result is empty");
        }

        [TestMethod]
        public void CheckRecordingGet()
        {
            var recording = Entities.Recording.Get("fc4d4d9c-58b7-4dba-a608-753ea752ccce");

            Assert.IsNotNull(recording, "Record not found");

            Assert.AreEqual("the wind of change", recording.Title.ToLower(),
                            string.Format("Expect 'The Wind Of Change' instead {0}", recording.Title));
        }

        [TestMethod]
        public void CheckRecordingSearch()
        {
            var recordings = Entities.Recording.Search("The Wind of Change");

            Assert.AreNotEqual(0, recordings.Count, "Result is empty");
        }
    }
}
