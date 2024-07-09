using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x02000B0C RID: 2828
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	internal class JsonSchemaNode
	{
		// Token: 0x170017A7 RID: 6055
		// (get) Token: 0x06007173 RID: 29043 RVA: 0x00224928 File Offset: 0x00224928
		public string Id { get; }

		// Token: 0x170017A8 RID: 6056
		// (get) Token: 0x06007174 RID: 29044 RVA: 0x00224930 File Offset: 0x00224930
		public ReadOnlyCollection<JsonSchema> Schemas { get; }

		// Token: 0x170017A9 RID: 6057
		// (get) Token: 0x06007175 RID: 29045 RVA: 0x00224938 File Offset: 0x00224938
		public Dictionary<string, JsonSchemaNode> Properties { get; }

		// Token: 0x170017AA RID: 6058
		// (get) Token: 0x06007176 RID: 29046 RVA: 0x00224940 File Offset: 0x00224940
		public Dictionary<string, JsonSchemaNode> PatternProperties { get; }

		// Token: 0x170017AB RID: 6059
		// (get) Token: 0x06007177 RID: 29047 RVA: 0x00224948 File Offset: 0x00224948
		public List<JsonSchemaNode> Items { get; }

		// Token: 0x170017AC RID: 6060
		// (get) Token: 0x06007178 RID: 29048 RVA: 0x00224950 File Offset: 0x00224950
		// (set) Token: 0x06007179 RID: 29049 RVA: 0x00224958 File Offset: 0x00224958
		public JsonSchemaNode AdditionalProperties { get; set; }

		// Token: 0x170017AD RID: 6061
		// (get) Token: 0x0600717A RID: 29050 RVA: 0x00224964 File Offset: 0x00224964
		// (set) Token: 0x0600717B RID: 29051 RVA: 0x0022496C File Offset: 0x0022496C
		public JsonSchemaNode AdditionalItems { get; set; }

		// Token: 0x0600717C RID: 29052 RVA: 0x00224978 File Offset: 0x00224978
		public JsonSchemaNode(JsonSchema schema)
		{
			this.Schemas = new ReadOnlyCollection<JsonSchema>(new JsonSchema[]
			{
				schema
			});
			this.Properties = new Dictionary<string, JsonSchemaNode>();
			this.PatternProperties = new Dictionary<string, JsonSchemaNode>();
			this.Items = new List<JsonSchemaNode>();
			this.Id = JsonSchemaNode.GetId(this.Schemas);
		}

		// Token: 0x0600717D RID: 29053 RVA: 0x002249D8 File Offset: 0x002249D8
		private JsonSchemaNode(JsonSchemaNode source, JsonSchema schema)
		{
			this.Schemas = new ReadOnlyCollection<JsonSchema>(source.Schemas.Union(new JsonSchema[]
			{
				schema
			}).ToList<JsonSchema>());
			this.Properties = new Dictionary<string, JsonSchemaNode>(source.Properties);
			this.PatternProperties = new Dictionary<string, JsonSchemaNode>(source.PatternProperties);
			this.Items = new List<JsonSchemaNode>(source.Items);
			this.AdditionalProperties = source.AdditionalProperties;
			this.AdditionalItems = source.AdditionalItems;
			this.Id = JsonSchemaNode.GetId(this.Schemas);
		}

		// Token: 0x0600717E RID: 29054 RVA: 0x00224A70 File Offset: 0x00224A70
		public JsonSchemaNode Combine(JsonSchema schema)
		{
			return new JsonSchemaNode(this, schema);
		}

		// Token: 0x0600717F RID: 29055 RVA: 0x00224A7C File Offset: 0x00224A7C
		public static string GetId(IEnumerable<JsonSchema> schemata)
		{
			return string.Join("-", (from s in schemata
			select s.InternalId).OrderBy((string id) => id, StringComparer.Ordinal));
		}
	}
}
