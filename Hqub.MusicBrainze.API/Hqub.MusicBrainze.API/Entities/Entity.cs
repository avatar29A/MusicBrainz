using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Hqub.MusicBrainz.API.Entities.Collections;

namespace Hqub.MusicBrainz.API.Entities
{
    public abstract class Entity
    {
        // TODO: remove "Raw" property (no need to waste memory)
        public XElement Raw { get; set; }

        public virtual void SetSchema(XElement schema)
        {
            //Raw = schema;
        }

		public static string CreateIncludeQuery(string[] inc)
		{
			return string.Join("+", inc);
		}

		protected async static Task<T> GetAsync<T>(string id, string url) where T : Entity
		{
			if (id == null)
            {
                throw new ArgumentNullException(string.Format(ErrorMessages.RequiredAttributeException, "id"));
            }

			return await WebRequestHelper.GetAsync<T>(url);
		}

        protected async static Task<T> SearchAsync<T>(string entity, string query, int limit = 25, int offset = 0) where T : MetadataWrapper
		{
            if (query == null)
            {
                throw new ArgumentNullException(string.Format(ErrorMessages.RequiredAttributeException, "query"));
            }

            return await WebRequestHelper.GetAsync<T>(WebRequestHelper.CreateSearchTemplate(entity,
                query, limit, offset), withoutMetadata: false);
		}

        protected async static Task<T> BrowseAsync<T>(string entity, string relatedEntity, string relatedEntityId, int limit, int offset, params  string[] inc) where T : Entity
        {
            if (entity == null)
            {
                throw new ArgumentNullException(string.Format(ErrorMessages.RequiredAttributeException, "entity"));
            }

            return await WebRequestHelper.GetAsync<T>(WebRequestHelper.CreateBrowseTemplate(entity,
                relatedEntity, relatedEntityId, limit, offset, CreateIncludeQuery(inc)), withoutMetadata: false);
        }
    }
}
