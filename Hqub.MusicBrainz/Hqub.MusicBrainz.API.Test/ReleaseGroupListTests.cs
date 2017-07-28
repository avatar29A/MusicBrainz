
namespace Hqub.MusicBrainz.API.Test
{
    using Hqub.MusicBrainz.API.Entities.Collections;
    using NUnit.Framework;

    // Resource: release-group-search.json
    // ReleaseGroup.Search("artist:(bob dylan)", 10);
    //
    // http://musicbrainz.org/ws/2/release-group?query=artist:%28bob%20dylan%29&limit=10&fmt=json

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
            var group = data;

            Assert.AreEqual(2787, group.Count);
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

            Assert.AreEqual("fc325dd3-73ed-36aa-9c77-6b65a958e3cf", group.Id);
            //Assert.AreEqual("Album", group.Type);
            Assert.AreEqual(100, group.Score);

            Assert.AreEqual("Desire", group.Title);
            Assert.AreEqual("Album", group.PrimaryType);

            Assert.IsNotNull(group.Releases);
            Assert.IsNotNull(group.Credits);
            Assert.IsNotNull(group.Tags);
        }
    }
}
