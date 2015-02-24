using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hqub.MusicBrainz.API.Entities;

namespace Hqub.MusicBrainz.API.Test
{
    // Resource: release-get.xml
    // Release.Get("12195c41-6136-4dfd-acf1-9923dadc73e2", "artists", "labels", "recordings", "release-groups");
    //
    // http://musicbrainz.org/ws/2/release/12195c41-6136-4dfd-acf1-9923dadc73e2/?inc=artists+labels+recordings+release-groups
    
    [TestClass]
    public class ReleaseTests
    {
        Release release;

        public ReleaseTests()
        {
            this.release = TestHelper.Get<Release>("release-get.xml");
        }

        [TestMethod]
        public void TestReleaseAttributes()
        {
            Assert.IsNotNull(release);
            Assert.AreEqual(release.Id, "12195c41-6136-4dfd-acf1-9923dadc73e2");
        }

        [TestMethod]
        public void TestReleaseElements()
        {
            Assert.AreEqual("Tucson: A Country Rock Opera", release.Title);
            Assert.AreEqual("Official", release.Status);
            Assert.AreEqual("normal", release.Quality);
            Assert.AreEqual("2012-06-11", release.Date);
            Assert.AreEqual("US", release.Country);
            Assert.AreEqual("809236126221", release.Barcode);
        }

        [TestMethod]
        public void TestReleaseTextRepresentation()
        {
            var text = release.TextRepresentation;

            Assert.IsNotNull(text);
            Assert.AreEqual("eng", text.Language);
            Assert.AreEqual("Latn", text.Script);
        }

        [TestMethod]
        public void TestReleaseArtistCredit()
        {
            var credits = release.Credits;

            Assert.IsNotNull(credits);
            Assert.AreEqual(1, credits.Count);

            Assert.IsNotNull(credits[0].Artist);
            Assert.AreEqual("249eb550-505e-43ef-ac50-e8c605706ff1", credits[0].Artist.Id);
        }

        [TestMethod]
        public void TestReleaseGroup()
        {
            var group = release.ReleaseGroup;

            Assert.IsNotNull(group);

            Assert.AreEqual("c0fa897e-17ea-42c9-9f42-04057c07d96b", group.Id);
            Assert.AreEqual("Album", group.Type);

            Assert.AreEqual("Tucson: A Country Rock Opera", group.Title);
            Assert.AreEqual("2012-06-11", group.FirstReleaseDate);
            Assert.AreEqual("Album", group.PrimaryType);
        }

        [TestMethod]
        public void TestReleaseCoverArtArchive()
        {
            var coverart = release.CoverArtArchive;

            Assert.IsNotNull(coverart);

            Assert.AreEqual(true, coverart.Artwork);
            Assert.AreEqual(true, coverart.Front);
            Assert.AreEqual(false, coverart.Back);
            Assert.AreEqual(1, coverart.Count);
        }

        [TestMethod]
        public void TestReleaseLabels()
        {
            var labels = release.Labels;

            Assert.IsNotNull(labels);
            Assert.AreEqual(1, labels.Count);
            Assert.AreEqual("FireCD262", labels[0].CatalogNumber);

            var label = labels[0].Label;

            Assert.AreEqual("659008fb-d1e7-4eca-865c-0d0344a721ed", label.Id);
            Assert.AreEqual("Fire Records", label.Name);
        }

        [TestMethod]
        public void TestReleaseMediumList()
        {
            var mediums = release.MediumList;

            Assert.IsNotNull(mediums);
            Assert.AreEqual(1, mediums.Count);
        }

        [TestMethod]
        public void TestReleaseMediumListTracks()
        {
            var tracks = release.MediumList[0].Tracks;

            Assert.IsNotNull(tracks);
            Assert.AreEqual(19, tracks.Count);
            //Assert.AreEqual(19, tracks.TrackCount);

            var track = tracks[0];

            Assert.IsNotNull(track);
            Assert.AreEqual("7791b499-b680-3653-94df-60f76174137c", track.Id);
            Assert.AreEqual(1, track.Position);

            var recording = track.Recordring;

            Assert.IsNotNull(recording);
            Assert.AreEqual("848f9f37-1a47-446b-b7f0-e09547738446", recording.Id);
            Assert.AreEqual(201066, recording.Length);
        }
    }
}
