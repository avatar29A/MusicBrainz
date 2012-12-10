using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Hqub.MusicBrainze.API.Entities
{
    public abstract class Entity
    {
        public XElement Raw { get; set; }

        public virtual void SetSchema(XElement schema)
        {
            Raw = schema;
        }
    }
}
