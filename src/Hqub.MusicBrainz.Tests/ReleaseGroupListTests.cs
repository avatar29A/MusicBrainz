﻿
namespace Hqub.MusicBrainz.Tests
{
    using Hqub.MusicBrainz.Entities.Collections;
    using NUnit.Framework;
    using System.Linq;
    using System.Threading.Tasks;

    // Resource: releasegroup-search.json
    // ReleaseGroup.Search("artist:(bob dylan)", 10);
    //
    // https://musicbrainz.org/ws/2/release-group?query=artist:%28bob%20dylan%29&limit=10&fmt=json

    public class ReleaseGroupListTests
    {
        private ReleaseGroupList data;

        [OneTimeSetUp]
        public async Task Init()
        {
            var client = new MusicBrainzClient()
            {
                Cache = EmbeddedResourceCache.Instance
            };

            data = await client.ReleaseGroups.SearchAsync("artist:(bob dylan)", 10);
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
            var group = data.Items.Where(g => g.Id == "699bebfe-b8f8-3e76-bf4d-0597910cd14d").FirstOrDefault();

            Assert.That(group, Is.Not.Null);
            Assert.That(group.Score, Is.EqualTo(100));

            Assert.That(group.Title, Is.EqualTo("Bob Dylan’s Greatest Hits, Vol. II"));
            Assert.That(group.PrimaryType, Is.EqualTo("Album"));

            Assert.That(group.SecondaryTypes, Is.Not.Null);
            Assert.That(group.SecondaryTypes.Count, Is.EqualTo(1));
            Assert.That(group.SecondaryTypes[0], Is.EqualTo("Compilation"));

            Assert.That(group.Releases, Is.Not.Null);
            Assert.That(group.Credits, Is.Not.Null);
        }
    }
}
