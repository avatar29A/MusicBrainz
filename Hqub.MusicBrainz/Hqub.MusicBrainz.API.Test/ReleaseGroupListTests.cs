
namespace Hqub.MusicBrainz.API.Test
{
    using Hqub.MusicBrainz.API.Entities.Collections;
    using NUnit.Framework;

    // Resource: releasegroup-search.json
    // ReleaseGroup.Search("artist:(bob dylan)", 10);
    //
    // https://musicbrainz.org/ws/2/release-group?query=artist:%28bob%20dylan%29&limit=10&fmt=json

    public class ReleaseGroupListTests
    {
        ReleaseGroupList data;

        public ReleaseGroupListTests()
        {
            this.data = TestHelper.GetJson<ReleaseGroupList>("releasegroup-search.json");
        }

        [Test]
        public void TestReleaseGroupListQueryCount()
        {
            Assert.AreEqual(0, data.Offset);
            Assert.GreaterOrEqual(data.Count, 1);
        }

        [Test]
        public void TestReleaseGroupListCount()
        {
            var group = data.Items;

            Assert.AreEqual(10, group.Count);
        }

        [Test]
        public void TestReleaseGroupListElements()
        {
            var group = data.Items[0];

            Assert.AreEqual("c2930468-8954-39e9-be89-39ca1477846f", group.Id);
            Assert.AreEqual(100, group.Score);

            Assert.AreEqual("The Genuine Bootleg Series, Take 2", group.Title);
            Assert.AreEqual("Album", group.PrimaryType);

            Assert.IsNotNull(group.SecondaryTypes);
            Assert.AreEqual(1, group.SecondaryTypes.Count);
            Assert.AreEqual("Compilation", group.SecondaryTypes[0]);

            Assert.IsNotNull(group.Releases);
            Assert.IsNotNull(group.Credits);
        }
    }
}
