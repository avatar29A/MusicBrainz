
namespace Hqub.MusicBrainz.API.Test
{
    using Hqub.MusicBrainz.API.Entities;
    using NUnit.Framework;

    // Resource: release-get.json
    // Release.Get("12195c41-6136-4dfd-acf1-9923dadc73e2", "artists", "labels", "recordings", "release-groups", "url-rels");
    //
    // https://musicbrainz.org/ws/2/release/12195c41-6136-4dfd-acf1-9923dadc73e2/?inc=artists+labels+recordings+release-groups+url-rels&fmt=json

    public class ReleaseTests
    {
        Release release;

        public ReleaseTests()
        {
            this.release = TestHelper.GetJson<Release>("release-get.json");
        }
        
        [Test]
        public void TestReleaseElements()
        {
            Assert.IsNotNull(release);

            Assert.AreEqual("12195c41-6136-4dfd-acf1-9923dadc73e2", release.Id);
            Assert.AreEqual("Tucson: A Country Rock Opera", release.Title);
            Assert.AreEqual("Official", release.Status);
            Assert.AreEqual("normal", release.Quality);
            Assert.AreEqual("2012-06-11", release.Date);
            Assert.AreEqual("US", release.Country);
            Assert.AreEqual("809236126221", release.Barcode);
        }

        [Test]
        public void TestReleaseTextRepresentation()
        {
            var text = release.TextRepresentation;

            Assert.IsNotNull(text);
            Assert.AreEqual("eng", text.Language);
            Assert.AreEqual("Latn", text.Script);
        }

        [Test]
        public void TestReleaseArtistCredit()
        {
            var credits = release.Credits;

            Assert.IsNotNull(credits);
            Assert.AreEqual(1, credits.Count);

            Assert.IsNotNull(credits[0].Artist);
            Assert.AreEqual("249eb550-505e-43ef-ac50-e8c605706ff1", credits[0].Artist.Id);
        }

        [Test]
        public void TestReleaseGroup()
        {
            var group = release.ReleaseGroup;

            Assert.IsNotNull(group);

            Assert.AreEqual("96daddd3-165b-4fdd-a422-e930ee6b3bc8", group.Id);
            //Assert.AreEqual("Album", group.Type);

            Assert.AreEqual("Tucson", group.Title);
            Assert.AreEqual("2012-06-11", group.FirstReleaseDate);
            Assert.AreEqual("Album", group.PrimaryType);
        }

        [Test]
        public void TestReleaseCoverArtArchive()
        {
            var coverart = release.CoverArtArchive;

            Assert.IsNotNull(coverart);

            Assert.AreEqual(true, coverart.Artwork);
            Assert.AreEqual(true, coverart.Front);
            Assert.AreEqual(false, coverart.Back);
            Assert.AreEqual(1, coverart.Count);
        }

        [Test]
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

        [Test]
        public void TestReleaseMediumList()
        {
            var mediums = release.Media;

            Assert.IsNotNull(mediums);
            Assert.AreEqual(1, mediums.Count);
        }

        [Test]
        public void TestReleaseMediaTracks()
        {
            var tracks = release.Media[0].Tracks;

            Assert.IsNotNull(tracks);
            Assert.AreEqual(19, tracks.Count);
            //Assert.AreEqual(19, tracks.TrackCount);

            var track = tracks[0];

            Assert.IsNotNull(track);
            Assert.AreEqual("7791b499-b680-3653-94df-60f76174137c", track.Id);
            Assert.AreEqual("1", track.Number);

            var recording = track.Recording;

            Assert.IsNotNull(recording);
            Assert.AreEqual("848f9f37-1a47-446b-b7f0-e09547738446", recording.Id);
            Assert.AreEqual(201000, recording.Length);
        }

        [Test]
        public void TestReleaseRelations()
        {
            var list = release.Relations;

            Assert.IsNotNull(list);
            Assert.AreEqual(1, list.Count);
        }
    }
}
