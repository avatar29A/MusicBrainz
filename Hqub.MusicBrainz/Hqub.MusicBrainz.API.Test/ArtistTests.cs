
namespace Hqub.MusicBrainz.API.Test
{
    using Hqub.MusicBrainz.API.Entities;
    using NUnit.Framework;
    using System.Linq;

    // Resource: artist-get.json
    // Artist.Get("12195c41-6136-4dfd-acf1-9923dadc73e2", "release-groups", "tags", "works", "ratings", "artist-rels", "url-rels");
    //
    // https://musicbrainz.org/ws/2/artist/72c536dc-7137-4477-a521-567eeb840fa8/?inc=release-groups+tags+works+ratings+artist-rels+url-rels&fmt=json

    public class ArtistTests
    {
        Artist artist;

        public ArtistTests()
        {
            this.artist = TestHelper.GetJson<Artist>("artist-get.json");
        }

        [Test]
        public void TestArtistElements()
        {
            Assert.AreEqual("72c536dc-7137-4477-a521-567eeb840fa8", artist.Id);
            Assert.AreEqual("Person", artist.Type);

            Assert.AreEqual("Bob Dylan", artist.Name);
            Assert.AreEqual("Dylan, Bob", artist.SortName);
            Assert.AreEqual("Male", artist.Gender);
            Assert.AreEqual("US", artist.Country);
        }

        [Test]
        public void TestArtistReleaseGroups()
        {
            var list = artist.ReleaseGroups;

            Assert.IsNotNull(list);
            Assert.AreEqual(25, list.Count);

            var group = list[3];

            Assert.IsNotNull(group);

            Assert.AreEqual("329fb554-2a81-3d8a-8e22-ec2c66810019", group.Id);
            Assert.AreEqual("Album", group.PrimaryType);

            Assert.AreEqual("Blonde on Blonde", group.Title);
            Assert.AreEqual("1966-05-16", group.FirstReleaseDate);
            Assert.AreEqual("Album", group.PrimaryType);
        }

        [Test]
        public void TestArtistArea()
        {
            var area = artist.Area;

            Assert.IsNotNull(area);
            Assert.AreEqual("489ce91b-6658-3307-9877-795b68554c98", area.Id);
            Assert.AreEqual("United States", area.Name);

            Assert.IsNotNull(area.IsoCodes);
            Assert.AreEqual(1, area.IsoCodes.Count);
        }

        [Test]
        public void TestArtistLifeSpan()
        {
            var lifespan = artist.LifeSpan;

            Assert.IsNotNull(lifespan);

            Assert.AreEqual("1941-05-24", lifespan.Begin);
            Assert.IsNull(lifespan.End);
            Assert.False(lifespan.Ended);
        }

        [Test]
        public void TestArtistRating()
        {
            var rating = artist.Rating;
            
            Assert.IsNotNull(rating);

            Assert.GreaterOrEqual(rating.Value, 1.0);
            Assert.GreaterOrEqual(rating.VotesCount, 1);
        }

        [Test]
        public void TestArtistTags()
        {
            var list = artist.Tags;

            Assert.IsNotNull(list);
            Assert.GreaterOrEqual(list.Count, 1);

            var tag = list[7];

            Assert.IsNotNull(tag);

            Assert.GreaterOrEqual(tag.Count, 1);
            Assert.AreEqual("blues", tag.Name);
        }

        [Test]
        public void TestArtistWorks()
        {
            var list = artist.Works;

            Assert.IsNotNull(list);
            Assert.AreEqual(25, list.Count);

            var work = list[0];

            Assert.IsNotNull(work);

            Assert.AreEqual("0135740e-69dc-41a0-86d9-6a57664809a5", work.Id);
            Assert.AreEqual("(We're Living on) Borrowed Time", work.Title);
        }

        [Test]
        public void TestArtistRelations()
        {
            var list = artist.Relations;

            Assert.IsNotNull(list);
            Assert.GreaterOrEqual(list.Count, 1);
            
            Assert.GreaterOrEqual(list.Where(r => r.TargetType == "url").Count(), 1);
            Assert.GreaterOrEqual(list.Where(r => r.TargetType == "artist").Count(), 1);
        }
    }
}
