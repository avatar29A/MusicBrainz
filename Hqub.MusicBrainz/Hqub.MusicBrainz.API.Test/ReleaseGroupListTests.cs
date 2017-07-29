
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
            Assert.AreEqual(0, data.Offset);
            Assert.AreEqual(3283, data.Count);
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

            Assert.AreEqual("dae0c107-7db4-3fac-a027-5e47b6bf2e70", group.Id);
            //Assert.AreEqual("Album", group.Type);
            Assert.AreEqual(100, group.Score);

            Assert.AreEqual("Jokerman", group.Title);
            Assert.AreEqual("Album", group.PrimaryType);

            Assert.IsNotNull(group.Releases);
            Assert.IsNotNull(group.Credits);
        }
    }
}
