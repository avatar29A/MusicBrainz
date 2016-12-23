using Hqub.MusicBrainz.API.Entities;
using NUnit.Framework;

namespace Hqub.MusicBrainz.API.Test
{
    // Resource: recording-get.xml
    // Recording.Get("12195c41-6136-4dfd-acf1-9923dadc73e2", "release-groups", "tags", "works");
    //
    // http://musicbrainz.org/ws/2/recording/9408b8ce-9b95-4fb0-ac70-595d054a15c6/?inc=artists+releases+tags
    
    public class RecordingTests
    {
        Recording recording;

        public RecordingTests()
        {
            this.recording = TestHelper.Get<Recording>("recording-get.xml");
        }

        [Test]
        public void TestRecordingAttributes()
        {
            Assert.IsNotNull(recording);
            Assert.AreEqual("9408b8ce-9b95-4fb0-ac70-595d054a15c6", recording.Id);
        }

        [Test]
        public void TestRecordingElements()
        {
            Assert.AreEqual("Alone Again Or", recording.Title);
            Assert.AreEqual(203293, recording.Length);

            Assert.IsNotNull(recording.Credits);
            Assert.IsNotNull(recording.Releases);
            Assert.IsNotNull(recording.Tags);
        }

        [Test]
        public void TestRecordingArtistCredits()
        {
            var credits = recording.Credits;

            Assert.AreEqual(1, credits.Count);

            var artist = credits[0].Artist;

            Assert.IsNotNull(artist);
            Assert.AreEqual("5e372a49-5672-4fb8-ba14-18c90780c4f9", artist.Id);
            Assert.AreEqual("Calexico", artist.Name);
        }

        [Test]
        public void TestRecordingReleases()
        {
            var releases = recording.Releases.Items;

            Assert.AreEqual(4, releases.Count);
            //Assert.AreEqual(4, releases.QueryCount);

            var release = releases[3];

            Assert.IsNotNull(release);
            Assert.AreEqual("8edf887c-f8ee-4663-af02-0a5117acc808", release.Id);
            Assert.AreEqual("Convict Pool", release.Title);
        }

        [Test]
        public void TestRecordingTags()
        {
            var tags = recording.Tags.Items;

            Assert.AreEqual(3, tags.Count);

            var tag = tags[0];

            Assert.IsNotNull(tag);
            Assert.AreEqual(1, tag.Count);
            Assert.AreEqual("alternative", tag.Name);
        }
    }
}
