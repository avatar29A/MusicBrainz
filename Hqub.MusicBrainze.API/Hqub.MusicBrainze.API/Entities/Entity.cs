using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Hqub.MusicBrainze.API.Entities.Collections;

namespace Hqub.MusicBrainze.API.Entities
{
    public abstract class Entity
    {
        public XElement Raw { get; set; }

        public virtual void SetSchema(XElement schema)
        {
            Raw = schema;
        }

		public static string CreateIncludeQuery(string[] inc)
		{
			//Build query for inc entiteis:
			var incBuilder = new StringBuilder();
			foreach (var entityName in inc)
			{
				incBuilder.AppendFormat("{0}+", entityName);
			}

			return incBuilder.ToString();
		}

		protected static T Get<T>(string id, string url) where T : Entity
		{
			if (id == null)
				throw new ArgumentNullException(string.Format(Localization.Messages.RequiredAttributeException, "id"));

			return
				WebRequestHelper.Get<T>(url);
		}

		protected static T Search<T>(string entity, string query, int limit = 25, int offset = 0, params  string[] inc) where T : MetadataWrapper
		{
			if (query == null)
				throw new ArgumentNullException(string.Format(Localization.Messages.RequiredAttributeException, "query"));

			return
				WebRequestHelper.Get<T>(
					WebRequestHelper.CreateSearchTemplate(entity, query, limit, offset,
					                                      CreateIncludeQuery(inc)), withoutMetadata: false);
		}

        protected static T Browse<T>(string entity, string relatedEntity, string relatedEntityId, int limit, int offset, params  string[] inc) where T : Entity
        {
            if (entity == null)
                throw new ArgumentNullException(string.Format(Localization.Messages.RequiredAttributeException, "entity"));

            return
                WebRequestHelper.Get<T>(
                    WebRequestHelper.CreateBrowseTemplate(entity, relatedEntity, relatedEntityId, limit, offset,
                                                          CreateIncludeQuery(inc)), withoutMetadata: false);
        }
    }
}
