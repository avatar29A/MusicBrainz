namespace Hqub.MusicBrainz.Tests
{
    using Hqub.MusicBrainz.Entities;
    using NUnit.Framework;
    using System.Threading.Tasks;

    // Resource: release-get.json
    // URL: https://musicbrainz.org/ws/2/release/12195c41-6136-4dfd-acf1-9923dadc73e2/?inc=artists+labels+recordings+release-groups+url-rels&fmt=json

    public class ReleaseGetTests
    {
        private Release release;

        [OneTimeSetUp]
        public async Task Init()
        {
            var client = new MusicBrainzClient()
            {
                Cache = EmbeddedResourceCache.Instance
            };

            string[] inc = ["artists", "labels", "recordings", "release-groups", "url-rels"];

            release = await client.Releases.GetAsync("12195c41-6136-4dfd-acf1-9923dadc73e2", inc);
        }
        
        [Test]
        public void TestReleaseElements()
        {
            Assert.That(release, Is.Not.Null);

            Assert.That(release.Id, Is.EqualTo("12195c41-6136-4dfd-acf1-9923dadc73e2"));
            Assert.That(release.Title, Is.EqualTo("Tucson: A Country Rock Opera"));
            Assert.That(release.Status, Is.EqualTo("Official"));
            Assert.That(release.Quality, Is.EqualTo("normal"));
            Assert.That(release.Date, Is.EqualTo("2012-06-11"));
            Assert.That(release.Country, Is.EqualTo("US"));
            Assert.That(release.Barcode, Is.EqualTo("809236126221"));
            Assert.That(release.Packaging, Is.EqualTo("Cardboard/Paper Sleeve"));
        }

        [Test]
        public void TestReleaseTextRepresentation()
        {
            var text = release.TextRepresentation;

            Assert.That(text, Is.Not.Null);
            Assert.That(text.Language, Is.EqualTo("eng"));
            Assert.That(text.Script, Is.EqualTo("Latn"));
        }

        [Test]
        public void TestReleaseArtistCredit()
        {
            var credits = release.Credits;

            Assert.That(credits, Is.Not.Null);
            Assert.That(credits.Count, Is.EqualTo(1));

            Assert.That(credits[0].Artist, Is.Not.Null);
            Assert.That(credits[0].Artist.Id, Is.EqualTo("249eb550-505e-43ef-ac50-e8c605706ff1"));
        }

        [Test]
        public void TestReleaseGroup()
        {
            var group = release.ReleaseGroup;

            Assert.That(group, Is.Not.Null);

            Assert.That(group.Id, Is.EqualTo("96daddd3-165b-4fdd-a422-e930ee6b3bc8"));
            //Assert.AreEqual("Album", group.Type);

            Assert.That(group.Title, Is.EqualTo("Tucson"));
            Assert.That(group.FirstReleaseDate, Is.EqualTo("2012-06-11"));
            Assert.That(group.PrimaryType, Is.EqualTo("Album"));
        }

        [Test]
        public void TestReleaseCoverArtArchive()
        {
            var coverart = release.CoverArtArchive;

            Assert.That(coverart, Is.Not.Null);

            Assert.That(coverart.Artwork, Is.EqualTo(true));
            Assert.That(coverart.Front, Is.EqualTo(true));
            Assert.That(coverart.Back, Is.EqualTo(false));
            Assert.That(coverart.Count, Is.EqualTo(1));
        }

        [Test]
        public void TestReleaseLabels()
        {
            var labels = release.Labels;

            Assert.That(labels, Is.Not.Null);
            Assert.That(labels.Count, Is.EqualTo(1));
            Assert.That(labels[0].CatalogNumber, Is.EqualTo("FireCD262"));

            var label = labels[0].Label;

            Assert.That(label.Id, Is.EqualTo("659008fb-d1e7-4eca-865c-0d0344a721ed"));
            Assert.That(label.Name, Is.EqualTo("Fire Records"));
            Assert.That(label.Disambiguation, Is.EqualTo("UK independent label"));
        }

        [Test]
        public void TestReleaseMediumList()
        {
            var mediums = release.Media;

            Assert.That(mediums, Is.Not.Null);
            Assert.That(mediums.Count, Is.EqualTo(1));
        }

        [Test]
        public void TestReleaseMediaTracks()
        {
            var tracks = release.Media[0].Tracks;

            Assert.That(tracks, Is.Not.Null);
            Assert.That(tracks.Count, Is.EqualTo(19));
            //Assert.AreEqual(19, tracks.TrackCount);

            var track = tracks[0];

            Assert.That(track, Is.Not.Null);
            Assert.That(track.Id, Is.EqualTo("7791b499-b680-3653-94df-60f76174137c"));
            Assert.That(track.Number, Is.EqualTo("1"));

            var recording = track.Recording;

            Assert.That(recording, Is.Not.Null);
            Assert.That(recording.Id, Is.EqualTo("848f9f37-1a47-446b-b7f0-e09547738446"));
            Assert.That(recording.Length, Is.EqualTo(201000));
        }

        [Test]
        public void TestReleaseGenres()
        {
            var genres = release.Genres;

            Assert.That(genres, Is.Not.Null);
            Assert.That(genres.Count, Is.EqualTo(0));
        }

        [Test]
        public void TestReleaseRelations()
        {
            var list = release.Relations;

            Assert.That(list, Is.Not.Null);
            Assert.That(list.Count, Is.EqualTo(1));
        }

        [Test]
        public void TestReleaseEvents()
        {
            var events = release.ReleaseEvents;

            Assert.That(events, Is.Not.Null);
            Assert.That(events.Count, Is.EqualTo(1));

            var releaseEvent = events[0];

            Assert.That(releaseEvent, Is.Not.Null);
            Assert.That(releaseEvent.Date, Is.EqualTo("2012-06-11"));

            var area = releaseEvent.Area;

            Assert.That(area, Is.Not.Null);
            Assert.That(area.Id, Is.EqualTo("489ce91b-6658-3307-9877-795b68554c98"));
            Assert.That(area.Name, Is.EqualTo("United States"));
            Assert.That(area.SortName, Is.EqualTo("United States"));
            Assert.That(area.Iso1Codes, Is.Not.Null);
            Assert.That(area.Iso1Codes.Count, Is.EqualTo(1));
            Assert.That(area.Iso1Codes[0], Is.EqualTo("US"));
        }
    }
}
