//﻿
namespace Hqub.MusicBrainz.API.Test
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Runtime.Serialization.Json;

    static class TestHelper
    {
        private const string prefix = "Hqub.MusicBrainz.API.Test.Data.";

        private static Stream LoadResource(string name)
        {
            if (!name.StartsWith(prefix))
            {
                name = prefix + name;
            }

            return Assembly.GetExecutingAssembly().GetManifestResourceStream(name);
        }

        public static T GetJson<T>(string resource)
        {
            try
            {
                var stream = LoadResource(resource);
                
                var serializer = new DataContractJsonSerializer(typeof(T));

                return (T)serializer.ReadObject(stream);
            }
            catch
            {
                throw;
            }
        }
    }
}
