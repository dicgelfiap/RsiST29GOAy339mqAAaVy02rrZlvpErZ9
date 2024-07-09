using System;
using System.Collections.Generic;
using System.Linq;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x02000B0E RID: 2830
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	public class JsonSchemaResolver
	{
		// Token: 0x170017AE RID: 6062
		// (get) Token: 0x06007182 RID: 29058 RVA: 0x00224AFC File Offset: 0x00224AFC
		// (set) Token: 0x06007183 RID: 29059 RVA: 0x00224B04 File Offset: 0x00224B04
		public IList<JsonSchema> LoadedSchemas { get; protected set; }

		// Token: 0x06007184 RID: 29060 RVA: 0x00224B10 File Offset: 0x00224B10
		public JsonSchemaResolver()
		{
			this.LoadedSchemas = new List<JsonSchema>();
		}

		// Token: 0x06007185 RID: 29061 RVA: 0x00224B24 File Offset: 0x00224B24
		public virtual JsonSchema GetSchema(string reference)
		{
			JsonSchema jsonSchema = this.LoadedSchemas.SingleOrDefault((JsonSchema s) => string.Equals(s.Id, reference, StringComparison.Ordinal));
			if (jsonSchema == null)
			{
				jsonSchema = this.LoadedSchemas.SingleOrDefault((JsonSchema s) => string.Equals(s.Location, reference, StringComparison.Ordinal));
			}
			return jsonSchema;
		}
	}
}
