
namespace Hqub.MusicBrainz.Tests
{
    using Hqub.MusicBrainz.Entities;
    using NUnit.Framework;
    using System.Linq;

    // Resource: label-get.json
    // Label.Get("82935ddb-a9d6-45a7-85e3-0b0add51fa1c", "releases", "artist-credits", "genres", "url-rels");
    //
    // https://musicbrainz.org/ws/2/label/82935ddb-a9d6-45a7-85e3-0b0add51fa1c?fmt=json&inc=releases+artist-credits+genres+url-rels

    public class LabelTests
    {
        Label label;

        public LabelTests()
        {
            this.label = TestHelper.GetJson<Label>("label-get.json");
        }

        [Test]
        public void TestLabelElements()
        {
            Assert.That(label.Id, Is.EqualTo("82935ddb-a9d6-45a7-85e3-0b0add51fa1c"));
            Assert.That(label.Type, Is.EqualTo("Original Production"));

            Assert.That(label.Name, Is.EqualTo("City Slang"));
            Assert.That(label.SortName, Is.EqualTo("City Slang"));
            Assert.That(label.Country, Is.EqualTo("DE"));
        }

        [Test]
        public void TestLabelReleases()
        {
            var list = label.Releases;

            Assert.That(list, Is.Not.Null);
            Assert.That(list.Count, Is.EqualTo(25));

            var release = list.Where(g => g.Id == "3f6ba929-ff4a-4af8-a896-2803ca3112c0").FirstOrDefault();

            Assert.That(release, Is.Not.Null);
            Assert.That(release.Title, Is.EqualTo("Fakebook"));

            var artist = release.Credits.FirstOrDefault();

            Assert.That(artist, Is.Not.Null);
            Assert.That(artist.Name, Is.EqualTo("Yo La Tengo"));
        }

        [Test]
        public void TestLabelArea()
        {
            var area = label.Area;

            Assert.That(area, Is.Not.Null);
            Assert.That(area.Id, Is.EqualTo("85752fda-13c4-31a3-bee5-0e5cb1f51dad"));
            Assert.That(area.Name, Is.EqualTo("Germany"));

            Assert.That(area.Iso1Codes, Is.Not.Null);
            Assert.That(area.Iso1Codes.Count, Is.EqualTo(1));
        }

        [Test]
        public void TestLabelLifeSpan()
        {
            var lifespan = label.LifeSpan;

            Assert.That(lifespan, Is.Not.Null);

            Assert.That(lifespan.Begin, Is.EqualTo("1991"));
            Assert.That(lifespan.End, Is.Null);
            Assert.That(lifespan.Ended, Is.False);
        }

        [Test]
        public void TestLabelGenres()
        {
            var genres = label.Genres;

            Assert.That(genres, Is.Not.Null);
            Assert.That(genres.Count, Is.EqualTo(3));

            var genre = genres[0];

            Assert.That(genre, Is.Not.Null);
            Assert.That(genre.Count, Is.EqualTo(1));
            Assert.That(genre.Name, Is.EqualTo("indie pop"));
        }

        [Test]
        public void TestLabelRelations()
        {
            var list = label.Relations;

            Assert.That(list, Is.Not.Null);
            Assert.That(list.Count, Is.GreaterThanOrEqualTo(12));

            Assert.That(list.Where(r => r.Type == "official site").Count(), Is.GreaterThanOrEqualTo(1));
        }
    }
}
