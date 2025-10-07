
namespace Hqub.MusicBrainz.Tests
{
    using Hqub.MusicBrainz.Entities;
    using NUnit.Framework;
    using System.Linq;
    using System.Threading.Tasks;

    // Resource: artist-get.json
    // Artist.Get("12195c41-6136-4dfd-acf1-9923dadc73e2", "release-groups", "tags", "works", "ratings", "artist-rels", "url-rels");
    //
    // https://musicbrainz.org/ws/2/artist/72c536dc-7137-4477-a521-567eeb840fa8/?inc=release-groups+tags+works+ratings+artist-rels+url-rels&fmt=json

    public class ArtistTests
    {
        Artist artist;

        [OneTimeSetUp]
        public async Task Init()
        {
            var client = new MusicBrainzClient()
            {
                Cache = EmbeddedResourceCache.Instance
            };

            string[] inc = ["release-groups", "tags", "works", "ratings", "artist-rels", "url-rels"];

            artist = await client.Artists.GetAsync("12195c41-6136-4dfd-acf1-9923dadc73e2", inc);
        }

        [Test]
        public void TestArtistElements()
        {
            Assert.That(artist.Id, Is.EqualTo("72c536dc-7137-4477-a521-567eeb840fa8"));
            Assert.That(artist.Type, Is.EqualTo("Person"));

            Assert.That(artist.Name, Is.EqualTo("Bob Dylan"));
            Assert.That(artist.SortName, Is.EqualTo("Dylan, Bob"));
            Assert.That(artist.Gender, Is.EqualTo("Male"));
            Assert.That(artist.Country, Is.EqualTo("US"));
        }

        [Test]
        public void TestArtistReleaseGroups()
        {
            var list = artist.ReleaseGroups;

            Assert.That(list, Is.Not.Null);
            Assert.That(list.Count, Is.EqualTo(25));

            var group = list.Where(g => g.Id == "329fb554-2a81-3d8a-8e22-ec2c66810019").FirstOrDefault();

            Assert.That(group, Is.Not.Null);

            Assert.That(group.PrimaryType, Is.EqualTo("Album"));

            Assert.That(group.Title, Is.EqualTo("Blonde on Blonde"));
            Assert.That(group.FirstReleaseDate, Is.EqualTo("1966-05-16"));
            Assert.That(group.PrimaryType, Is.EqualTo("Album"));
        }

        [Test]
        public void TestArtistArea()
        {
            var area = artist.Area;

            Assert.That(area, Is.Not.Null);
            Assert.That(area.Id, Is.EqualTo("489ce91b-6658-3307-9877-795b68554c98"));
            Assert.That(area.Name, Is.EqualTo("United States"));

            Assert.That(area.IsoCodes, Is.Not.Null);
            Assert.That(area.IsoCodes.Count, Is.EqualTo(1));

            area = artist.BeginArea;

            Assert.That(area, Is.Not.Null);
            Assert.That(area.Id, Is.EqualTo("04e60741-b1ae-4078-80bb-ffe8ae643ea7"));
            Assert.That(area.Name, Is.EqualTo("Duluth"));
        }

        [Test]
        public void TestArtistAliases()
        {
            var aliases = artist.Aliases;

            Assert.That(aliases, Is.Not.Null);
            Assert.That(aliases.Any(a => a.Name.Equals("Boy Dylan")), Is.True);
        }

        [Test]
        public void TestArtistLifeSpan()
        {
            var lifespan = artist.LifeSpan;

            Assert.That(lifespan, Is.Not.Null);

            Assert.That(lifespan.Begin, Is.EqualTo("1941-05-24"));
            Assert.That(lifespan.End, Is.Null);
            Assert.That(lifespan.Ended, Is.False);
        }

        [Test]
        public void TestArtistRating()
        {
            var rating = artist.Rating;

            Assert.That(rating, Is.Not.Null);

            Assert.That(rating.Value, Is.GreaterThanOrEqualTo(1.0));
            Assert.That(rating.VotesCount, Is.GreaterThanOrEqualTo(1));
        }

        [Test]
        public void TestArtistTags()
        {
            var list = artist.Tags;

            Assert.That(list, Is.Not.Null);
            Assert.That(list.Count, Is.GreaterThanOrEqualTo(1));

            var tag = list.OrderByDescending(i => i.Count).First();

            Assert.That(tag.Count, Is.GreaterThanOrEqualTo(5));
            Assert.That(tag.Name, Is.EqualTo("folk rock"));
        }

        [Test]
        public void TestArtistGenres()
        {
            var genres = artist.Genres;

            Assert.That(genres, Is.Not.Null);
            Assert.That(genres.Count, Is.EqualTo(10));

            var genre = genres[0];

            Assert.That(genre, Is.Not.Null);
            Assert.That(genre.Count, Is.EqualTo(2));
            Assert.That(genre.Name, Is.EqualTo("blues"));
        }

        [Test]
        public void TestArtistWorks()
        {
            var list = artist.Works;

            Assert.That(list, Is.Not.Null);
            Assert.That(list.Count, Is.EqualTo(25));

            var work = list[0];

            Assert.That(work, Is.Not.Null);

            Assert.That(work.Id, Is.EqualTo("32bb68a2-90bd-4a1d-aeb9-295dfa7596b0"));
            Assert.That(work.Title, Is.EqualTo("’Cross the Green Mountain"));
        }

        [Test]
        public void TestArtistRelations()
        {
            var list = artist.Relations;

            Assert.That(list, Is.Not.Null);
            Assert.That(list.Count, Is.GreaterThanOrEqualTo(1));

            Assert.That(list.Where(r => r.TargetType == "url").Count(), Is.GreaterThanOrEqualTo(1));
            Assert.That(list.Where(r => r.TargetType == "artist").Count(), Is.GreaterThanOrEqualTo(1));
        }
    }
}
