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
//            var a = Artist.Get("1f36a3a2-9687-4819-ac55-54d7ff0b8b88");
//
//            Console.WriteLine(a.Name);

            var artists = Artist.Search("Scorpions");

            foreach (var artist in artists)
            {
                Console.WriteLine(artist.Name);
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

            Console.ReadKey();
        }
    }
}
