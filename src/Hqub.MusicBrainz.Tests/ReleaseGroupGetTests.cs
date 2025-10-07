namespace Hqub.MusicBrainz.Tests
{
    using Hqub.MusicBrainz.Entities;
    using NUnit.Framework;
    using System.Threading.Tasks;

    // Resource: releasegroup-get.json
    // URL: https://musicbrainz.org/ws/2/release-group/fc325dd3-73ed-36aa-9c77-6b65a958e3cf?inc=artists+releases+ratings+url-rels&fmt=json

    public class ReleaseGroupGetTests
    {
        private ReleaseGroup group;

        [OneTimeSetUp]
        public async Task Init()
        {
            var client = new MusicBrainzClient()
            {
                Cache = EmbeddedResourceCache.Instance
            };

            string[] inc = ["artists", "releases", "ratings", "url-rels"];

            group = await client.ReleaseGroups.GetAsync("fc325dd3-73ed-36aa-9c77-6b65a958e3cf", inc);
        }

        [Test]
        public void TestReleaseGroupElements()
        {
            Assert.That(group, Is.Not.Null);

            Assert.That(group.Id, Is.EqualTo("fc325dd3-73ed-36aa-9c77-6b65a958e3cf"));
            Assert.That(group.Title, Is.EqualTo("Desire"));
            Assert.That(group.PrimaryType, Is.EqualTo("Album"));
            Assert.That(group.FirstReleaseDate, Is.EqualTo("1976-01-05"));
        }

        [Test]
        public void TestReleaseGroupCredits()
        {
            var credits = group.Credits;

            Assert.That(credits, Is.Not.Null);

            var artist = credits[0].Artist;

            Assert.That(artist, Is.Not.Null);
            Assert.That(artist.Id, Is.EqualTo("72c536dc-7137-4477-a521-567eeb840fa8"));
            Assert.That(artist.Name, Is.EqualTo("Bob Dylan"));
        }

        [Test]
        public void TestReleaseGroupReleases()
        {
            var releases = group.Releases;

            Assert.That(releases, Is.Not.Null);
            Assert.That(releases.Count, Is.GreaterThan(1));
        }

        [Test]
        public void TestReleaseGroupRating()
        {
            var rating = group.Rating;

            Assert.That(rating, Is.Not.Null);

            Assert.That(rating.Value, Is.GreaterThanOrEqualTo(1));
            Assert.That(rating.VotesCount, Is.GreaterThanOrEqualTo(1));
        }

        [Test]
        public void TestReleaseGroupRelations()
        {
            var list = group.Relations;

            Assert.That(list, Is.Not.Null);
            Assert.That(list.Count, Is.GreaterThan(1));
        }

        [Test]
        public void TestReleaseGroupGenres()
        {
            var genres = group.Genres;

            Assert.That(genres, Is.Not.Null);
            Assert.That(genres.Count, Is.GreaterThan(1));

            var genre = genres[0];

            Assert.That(genre.Count, Is.GreaterThan(1));
            Assert.That(genre.Name, Is.EqualTo("blues rock"));
        }
    }
}
