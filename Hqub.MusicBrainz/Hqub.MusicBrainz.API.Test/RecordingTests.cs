
namespace Hqub.MusicBrainz.API.Test
{
    using Hqub.MusicBrainz.API.Entities;
    using NUnit.Framework;

    // Resource: recording-get.json
    // Recording.Get("12195c41-6136-4dfd-acf1-9923dadc73e2"", "artists", "releases", "tags", "ratings", "url-rels");
    //
    // https://musicbrainz.org/ws/2/recording/9408b8ce-9b95-4fb0-ac70-595d054a15c6/?inc=artists+releases+tags+ratings+url-rels&fmt=json

    public class RecordingTests
    {
        Recording recording;

        public RecordingTests()
        {
            this.recording = TestHelper.GetJson<Recording>("recording-get.json");
        }

        [Test]
        public void TestRecordingElements()
        {
            Assert.IsNotNull(recording);

            Assert.AreEqual("9408b8ce-9b95-4fb0-ac70-595d054a15c6", recording.Id);
            Assert.AreEqual("Alone Again Or", recording.Title);
            Assert.AreEqual(204333, recording.Length);

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
            var releases = recording.Releases;

            Assert.AreEqual(7, releases.Count);

            var release = releases[6];

            Assert.IsNotNull(release);
            Assert.AreEqual("8edf887c-f8ee-4663-af02-0a5117acc808", release.Id);
            Assert.AreEqual("Convict Pool", release.Title);
        }

        [Test]
        public void TestRecordingTags()
        {
            var tags = recording.Tags;

            Assert.AreEqual(3, tags.Count);

            var tag = tags[0];

            Assert.IsNotNull(tag);
            Assert.AreEqual(1, tag.Count);
            Assert.AreEqual("alternative", tag.Name);
        }

        [Test]
        public void TestRecordingRating()
        {
            var rating = recording.Rating;

            Assert.IsNotNull(rating);

            Assert.AreEqual(5, rating.Value);
            Assert.AreEqual(1, rating.VotesCount);
        }

        [Test]
        public void TestRecordingRelations()
        {
            var list = recording.Relations;

            Assert.IsNotNull(list);
        }
    }
}
