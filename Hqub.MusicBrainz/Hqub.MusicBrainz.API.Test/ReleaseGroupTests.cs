
namespace Hqub.MusicBrainz.API.Test
{
    using Hqub.MusicBrainz.API.Entities;
    using NUnit.Framework;

    // Resource: releasegroup-get.json
    // ReleaseGroup.Get("fc325dd3-73ed-36aa-9c77-6b65a958e3cf", "artists", "releases", "ratings", "url-rels");
    // release group
    // https://musicbrainz.org/ws/2/release-group/fc325dd3-73ed-36aa-9c77-6b65a958e3cf?inc=artists+releases+ratings+url-rels&fmt=json

    public class ReleaseGroupTests
    {
        ReleaseGroup group;

        public ReleaseGroupTests()
        {
            this.group = TestHelper.GetJson<ReleaseGroup>("releasegroup-get.json");
        }

        [Test]
        public void TestReleaseGroupElements()
        {
            Assert.IsNotNull(group);

            Assert.AreEqual("fc325dd3-73ed-36aa-9c77-6b65a958e3cf", group.Id);
            Assert.AreEqual("Desire", group.Title);
            Assert.AreEqual("Album", group.PrimaryType);
            Assert.AreEqual("1976-01-05", group.FirstReleaseDate);
        }

        [Test]
        public void TestReleaseGroupCredits()
        {
            var credits = group.Credits;

            Assert.IsNotNull(credits);

            var artist = credits[0].Artist;

            Assert.IsNotNull(artist);
            Assert.AreEqual("72c536dc-7137-4477-a521-567eeb840fa8", artist.Id);
            Assert.AreEqual("Bob Dylan", artist.Name);
        }

        [Test]
        public void TestReleaseGroupReleases()
        {
            var releases = group.Releases;

            Assert.IsNotNull(releases);
            Assert.IsTrue(releases.Count > 1);
        }

        [Test]
        public void TestReleaseGroupRating()
        {
            var rating = group.Rating;

            Assert.IsNotNull(rating);

            Assert.GreaterOrEqual(rating.Value, 1);
            Assert.GreaterOrEqual(rating.VotesCount, 1);
        }

        [Test]
        public void TestReleaseGroupRelations()
        {
            var list = group.Relations;

            Assert.IsNotNull(list);
            Assert.IsTrue(list.Count > 1);
        }

        [Test]
        public void TestReleaseGroupGenres()
        {
            var genres = group.Genres;

            Assert.IsNotNull(genres);
            Assert.IsTrue(genres.Count > 1);

            var genre = genres[0];

            Assert.IsTrue(genre.Count > 1);
            Assert.AreEqual("blues rock", genre.Name);
        }
    }
}
