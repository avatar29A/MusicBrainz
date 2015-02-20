using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hqub.MusicBrainze.API
{
    public class ErrorMessages
    {
        public const string RequiredAttributeException = "Attribute '{0}' must be specified.";
        public const string RequiredAttributesException = "Attributes '{0}' must be specified.";
        public const string StreamIsEmpty = "Query returned an empty result.";
        public const string WrongXmlFormat = "Wrong answer format";
    }
}
