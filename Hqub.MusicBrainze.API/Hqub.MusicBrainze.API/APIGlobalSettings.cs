using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hqub.MusicBrainze.API
{
    public static class APIGlobalSettings
    {
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
