using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Hqub.MusicBrainze.API.Entities;

namespace Hqub.MusicBrainze.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var include = API.Entities.Include.ArtistIncludeEntityHelper.Releases;
            var a = Artist.Get("c3cceeed-3332-4cf0-8c4c-bbde425147b6",
                               API.Entities.Include.ArtistIncludeEntityHelper.Releases,
                               API.Entities.Include.ArtistIncludeEntityHelper.Raitings,
                               API.Entities.Include.ArtistIncludeEntityHelper.Recordings,
                               API.Entities.Include.ArtistIncludeEntityHelper.ReleaseGroups,
                               API.Entities.Include.ArtistIncludeEntityHelper.Tags,
                               API.Entities.Include.ArtistIncludeEntityHelper.Works);
//
            Console.WriteLine("{0} - {1}.", include, a.ReleaseLists.Count);

//            var artists = Artist.Search("Scorpions");
//
//            foreach (var artist in artists)
//            {
//                Console.WriteLine(artist.Name);
//
//                foreach (var tag in artist.Tags)
//                {
//                    Console.WriteLine("\t{0}", tag.Name);
//                }
//            }
//
//            var serilize = new XmlSerializer(typeof (API.Entities.Metadata.MetadataWrapper));
//
//            var entity = new API.Entities.Metadata.MetadataWrapper();
//            entity.Collection.Add(new Artist
//            {
//                Id = "123-234-234-234",
//                Name = "Lyapis"
//            });
//
//            var memory = new MemoryStream();
//            serilize.Serialize(memory, entity);
//
//            Console.WriteLine(UTF8Encoding.UTF8.GetString(memory.ToArray()));

            Console.ReadKey();
        }
    }
}
