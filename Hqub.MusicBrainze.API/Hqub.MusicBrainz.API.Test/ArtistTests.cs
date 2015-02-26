using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hqub.MusicBrainz.API.Entities;

namespace Hqub.MusicBrainz.API.Test
{
    // Resource: artist-get.xml
    // Artist.Get("12195c41-6136-4dfd-acf1-9923dadc73e2", "release-groups", "tags", "works");
    //
    // http://musicbrainz.org/ws/2/artist/72c536dc-7137-4477-a521-567eeb840fa8/?inc=release-groups+tags+works

    [TestClass]
    public class ArtistTests
    {
        Artist artist;

        public ArtistTests()
        {
            this.artist = TestHelper.Get<Artist>("artist-get.xml");
        }

        [TestMethod]
        public void TestArtistAttributes()
        {
            Assert.IsNotNull(artist);
            Assert.AreEqual("72c536dc-7137-4477-a521-567eeb840fa8", artist.Id);
            Assert.AreEqual("Person", artist.Type);
        }

        [TestMethod]
        public void TestArtistElements()
        {
            Assert.AreEqual("72c536dc-7137-4477-a521-567eeb840fa8", artist.Id);
            Assert.AreEqual("Person", artist.Type);
            //Assert.AreEqual(artist, artist.Score);

            Assert.AreEqual("Bob Dylan", artist.Name);
            Assert.AreEqual("Dylan, Bob", artist.SortName);
            //Assert.AreEqual("male", artist.Gender);
            Assert.AreEqual("US", artist.Country);

            //Assert.IsNotNull(artist.Area);
            Assert.IsNotNull(artist.LifeSpan);
            Assert.IsNotNull(artist.Tags);
        }

        [TestMethod]
        public void TestArtistReleaseGroups()
        {
            var list = artist.ReleaseGroups.Items;

            Assert.IsNotNull(list);
            Assert.AreEqual(25, list.Count);
            //Assert.AreEqual(642, list.QueryCount);

            var group = list[3];

            Assert.IsNotNull(group);

            Assert.AreEqual("329fb554-2a81-3d8a-8e22-ec2c66810019", group.Id);
            Assert.AreEqual("Album", group.Type);

            Assert.AreEqual("Blonde on Blonde", group.Title);
            Assert.AreEqual("1966-05-16", group.FirstReleaseDate);
            Assert.AreEqual("Album", group.PrimaryType);
        }

        [TestMethod]
        public void TestArtistTags()
        {
            var list = artist.Tags.Items;

            Assert.IsNotNull(list);
            Assert.AreEqual(24, list.Count);

            var tag = list[1];

            Assert.IsNotNull(tag);

            Assert.AreEqual(1, tag.Count);
            Assert.AreEqual("1960s", tag.Name);
        }

        [TestMethod]
        public void TestArtistWorks()
        {
            var list = artist.Works.Items;

            Assert.IsNotNull(list);
            Assert.AreEqual(25, list.Count);
            //Assert.AreEqual(1465, list.QueryCount);

            var work = list[0];

            Assert.IsNotNull(work);

            Assert.AreEqual("0135740e-69dc-41a0-86d9-6a57664809a5", work.Id);
            Assert.AreEqual("(We're Living on) Borrowed Time", work.Title);
        }
    }
}
