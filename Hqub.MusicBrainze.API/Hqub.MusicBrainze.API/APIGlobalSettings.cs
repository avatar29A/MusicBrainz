using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hqub.MusicBrainz.API
{
    public static class APIGlobalSettings
    {
        public const string WebServiceUrl = "http://musicbrainz.org/ws/2/";
        public const string LookupTemplate = "{0}/{1}/?inc={2}";
        public const string BrowseTemplate = "{0}?{1}={2}&limit={3}&offset={4}&inc={5}";
        public const string SearchTemplate = "{0}?query={1}&limit={2}&offset={3}";

        static APIGlobalSettings()
        {
            GenerateCommunicationThrow = true;
        }

        //[Russian]
        // Если true, то все исключения (exceptions) при http-запросах к MusicBrainze (из класса WebRequestHelper) будут 
        // пробрасоваться вверх. Иначе будут подавляться.
        //[Translate to Eng]
        // If true, then all exceptions for http-requests to MusicBrainze (from class WebRequestHelper) will
        // throw up. Otherwise there will be suppressed.
        public static bool GenerateCommunicationThrow { get; set; }
    }
}
