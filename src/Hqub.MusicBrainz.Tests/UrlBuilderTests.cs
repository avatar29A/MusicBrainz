namespace Hqub.MusicBrainz.Tests
{
    using NUnit.Framework;

    public class UrlBuilder
    {
        private MusicBrainzClient client;

        [OneTimeSetUp]
        public void Init()
        {
            client = new MusicBrainzClient();
        }

        [Test]
        public void TestLookupRequest()
        {
            var request = client.Releases.Get("00000000-0000-0000-0000-000000000000");

            const string expected = "release/00000000-0000-0000-0000-000000000000?fmt=json";

            Assert.That(request.ToString(), Is.EqualTo(expected));

            request.Include("artists", "tags");

            Assert.That(request.ToString(), Is.EqualTo(expected + "&inc=artists+tags"));
        }

        [Test]
        public void TestSearchRequest()
        {
            var request = client.Releases.Search("search-string");

            const string expected = "release?query=search-string&fmt=json";

            Assert.That(request.ToString(), Is.EqualTo(expected));

            request.Limit(10);

            Assert.That(request.ToString(), Is.EqualTo(expected + "&limit=10"));

            request.Offset(20);

            Assert.That(request.ToString(), Is.EqualTo(expected + "&limit=10&offset=20"));
        }

        [Test]
        public void TestBrowseRequest()
        {
            var request = client.Releases.Browse("label", "00000000-0000-0000-0000-000000000000");

            const string expected = "release?label=00000000-0000-0000-0000-000000000000&fmt=json";

            Assert.That(request.ToString(), Is.EqualTo(expected));

            request.Limit(10);

            Assert.That(request.ToString(), Is.EqualTo(expected + "&limit=10"));

            request.Offset(20);

            Assert.That(request.ToString(), Is.EqualTo(expected + "&limit=10&offset=20"));

            request.Include("artist-credits", "tags");

            Assert.That(request.ToString(), Is.EqualTo(expected + "&limit=10&offset=20&inc=artist-credits+tags"));

            request.Type("album|ep");

            Assert.That(request.ToString(), Is.EqualTo(expected + "&limit=10&offset=20&inc=artist-credits+tags&type=album|ep"));

            request.Status("official");

            Assert.That(request.ToString(), Is.EqualTo(expected + "&limit=10&offset=20&inc=artist-credits+tags&type=album|ep&status=official"));
        }
    }
}
