using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hqub.MusicBrainz.API.Entities.Metadata;

namespace Hqub.MusicBrainz.API.Test
{
    [TestClass]
    public class ReleaseTest
    {
        [TestMethod]
        public void TestReleaseSearchResultSerialization()
        {
            var releases = TestHelper.Get<ReleaseMetadataWrapper>("release-search.xml", false).Collection;

            Assert.AreEqual(releases.Count, 10);

            // TODO: add a lot more tests.
        }
    }
}
