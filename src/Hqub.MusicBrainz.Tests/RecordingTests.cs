
namespace Hqub.MusicBrainz.Tests
{
    using Hqub.MusicBrainz.Entities;
    using NUnit.Framework;
    using System.Linq;
    using System.Threading.Tasks;

    // Resource: recording-get.json
    // Recording.Get("12195c41-6136-4dfd-acf1-9923dadc73e2", "artists", "releases", "tags", "ratings", "url-rels");
    //
    // https://musicbrainz.org/ws/2/recording/9408b8ce-9b95-4fb0-ac70-595d054a15c6/?inc=artists+releases+tags+ratings+url-rels&fmt=json

    public class RecordingTests
    {
        private Recording recording;

        [OneTimeSetUp]
        public async Task Init()
        {
            var client = new MusicBrainzClient()
            {
                Cache = EmbeddedResourceCache.Instance
            };

            string[] inc = ["artists", "releases", "tags", "ratings", "url-rels"];

            recording = await client.Recordings.GetAsync("12195c41-6136-4dfd-acf1-9923dadc73e2", inc);
        }

        [Test]
        public void TestRecordingElements()
        {
            Assert.That(recording, Is.Not.Null);

            Assert.That(recording.Id, Is.EqualTo("9408b8ce-9b95-4fb0-ac70-595d054a15c6"));
            Assert.That(recording.Title, Is.EqualTo("Alone Again Or"));
            Assert.That(recording.Length, Is.EqualTo(204480));

            Assert.That(recording.Credits, Is.Not.Null);
            Assert.That(recording.Releases, Is.Not.Null);
            Assert.That(recording.Tags, Is.Not.Null);
        }

        [Test]
        public void TestRecordingArtistCredits()
        {
            var credits = recording.Credits;

            Assert.That(credits.Count, Is.EqualTo(1));

            var artist = credits[0].Artist;

            Assert.That(artist, Is.Not.Null);
            Assert.That(artist.Id, Is.EqualTo("5e372a49-5672-4fb8-ba14-18c90780c4f9"));
            Assert.That(artist.Name, Is.EqualTo("Calexico"));
        }

        [Test]
        public void TestRecordingReleases()
        {
            var releases = recording.Releases;

            Assert.That(releases.Count, Is.EqualTo(11));

            var release = releases[3];

            Assert.That(release, Is.Not.Null);
            Assert.That(release.Id, Is.EqualTo("8edf887c-f8ee-4663-af02-0a5117acc808"));
            Assert.That(release.Title, Is.EqualTo("Convict Pool"));
        }

        [Test]
        public void TestRecordingTags()
        {
            var tags = recording.Tags;

            Assert.That(tags.Count, Is.GreaterThanOrEqualTo(5));

            var tag = tags.OrderByDescending(i => i.Count).First();

            Assert.That(tag, Is.Not.Null);
            Assert.That(tag.Count, Is.GreaterThanOrEqualTo(1));
            Assert.That(tag.Name, Is.EqualTo("alternative"));
        }

        [Test]
        public void TestRecordingGenres()
        {
            var genres = recording.Genres;

            Assert.That(genres, Is.Not.Null);
            Assert.That(genres.Count, Is.GreaterThanOrEqualTo(5));

            var genre = genres.First();

            Assert.That(genre.Count, Is.GreaterThanOrEqualTo(1));
            Assert.That(genre.Name, Is.EqualTo("alternative country"));
        }

        [Test]
        public void TestRecordingRating()
        {
            var rating = recording.Rating;

            Assert.That(rating, Is.Not.Null);

            Assert.That(rating.Value, Is.GreaterThanOrEqualTo(1));
            Assert.That(rating.VotesCount, Is.GreaterThanOrEqualTo(1));
        }

        [Test]
        public void TestRecordingRelations()
        {
            var list = recording.Relations;

            Assert.That(list, Is.Not.Null);
        }
    }
}
