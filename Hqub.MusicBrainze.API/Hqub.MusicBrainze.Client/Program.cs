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
//            var track = Recording.Get("4a397477-4819-41c8-b6dd-cc7ee55a341f", "artists");

//            foreach (var s in Recording.Browse(API.Localization.Constants.Release, "0af70df8-92f3-358f-8a9f-07c0ae1d8f73", 50))
//	        {
//		        Console.WriteLine(s.Title);
//	        }
//

            var artists = Artist.Search("Scorpions");

            foreach (var artist in artists)
            {
                Console.WriteLine(artist.Name);

                foreach (var tag in artist.Tags)
                {
                    Console.WriteLine("\t{0}", tag.Name);
                }
            }
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

			Console.WriteLine("OK");
            Console.ReadKey();
        }
    }
}
