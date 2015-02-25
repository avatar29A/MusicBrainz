using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hqub.MusicBrainz.Resources
{
    public sealed class Messages
    {
        public const string EmptyStream = "Query returned an empty result.";
        public const string MissingParameter = "Attribute '{0}' must be specified.";
        public const string InvalidQueryParameter = "Key not supported ({0}).";
        public const string WrongResponseFormat = "Webservice returned invalid response format.";
    }
}
