
namespace Hqub.MusicBrainz.Tests
{
    using Hqub.MusicBrainz.Entities.Collections;
    using NUnit.Framework;
    using System.Linq;

    // Resource: releasegroup-search.json
    // ReleaseGroup.Search("artist:(bob dylan)", 10);
    //
    // https://musicbrainz.org/ws/2/release-group?query=artist:%28bob%20dylan%29&limit=10&fmt=json

    public class ReleaseGroupListTests
    {
        private readonly ReleaseGroupList data;

        public ReleaseGroupListTests()
        {
            data = TestHelper.GetJson<ReleaseGroupList>("releasegroup-search.json");
        }

        [Test]
        public void TestReleaseGroupListQueryCount()
        {
            Assert.That(data.Offset, Is.EqualTo(0));
            Assert.That(data.Count, Is.GreaterThanOrEqualTo(1));
        }

        [Test]
        public void TestReleaseGroupListCount()
        {
            var group = data.Items;

            Assert.That(group.Count, Is.EqualTo(10));
        }

        [Test]
        public void TestReleaseGroupListElements()
        {
            var group = data.Items.Where(g => g.Id == "b5cbeb61-6e9c-38a8-9cb7-caf456782361").FirstOrDefault();

            Assert.That(group, Is.Not.Null);
            Assert.That(group.Score, Is.EqualTo(100));

            Assert.That(group.Title, Is.EqualTo("Biograph"));
            Assert.That(group.PrimaryType, Is.EqualTo("Album"));

            Assert.That(group.SecondaryTypes, Is.Not.Null);
            Assert.That(group.SecondaryTypes.Count, Is.EqualTo(1));
            Assert.That(group.SecondaryTypes[0], Is.EqualTo("Compilation"));

            Assert.That(group.Releases, Is.Not.Null);
            Assert.That(group.Credits, Is.Not.Null);
        }
    }
}
