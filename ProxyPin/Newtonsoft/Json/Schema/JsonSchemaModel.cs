using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x02000B0A RID: 2826
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	internal class JsonSchemaModel
	{
		// Token: 0x17001790 RID: 6032
		// (get) Token: 0x06007139 RID: 28985 RVA: 0x00223FAC File Offset: 0x00223FAC
		// (set) Token: 0x0600713A RID: 28986 RVA: 0x00223FB4 File Offset: 0x00223FB4
		public bool Required { get; set; }

		// Token: 0x17001791 RID: 6033
		// (get) Token: 0x0600713B RID: 28987 RVA: 0x00223FC0 File Offset: 0x00223FC0
		// (set) Token: 0x0600713C RID: 28988 RVA: 0x00223FC8 File Offset: 0x00223FC8
		public JsonSchemaType Type { get; set; }

		// Token: 0x17001792 RID: 6034
		// (get) Token: 0x0600713D RID: 28989 RVA: 0x00223FD4 File Offset: 0x00223FD4
		// (set) Token: 0x0600713E RID: 28990 RVA: 0x00223FDC File Offset: 0x00223FDC
		public int? MinimumLength { get; set; }

		// Token: 0x17001793 RID: 6035
		// (get) Token: 0x0600713F RID: 28991 RVA: 0x00223FE8 File Offset: 0x00223FE8
		// (set) Token: 0x06007140 RID: 28992 RVA: 0x00223FF0 File Offset: 0x00223FF0
		public int? MaximumLength { get; set; }

		// Token: 0x17001794 RID: 6036
		// (get) Token: 0x06007141 RID: 28993 RVA: 0x00223FFC File Offset: 0x00223FFC
		// (set) Token: 0x06007142 RID: 28994 RVA: 0x00224004 File Offset: 0x00224004
		public double? DivisibleBy { get; set; }

		// Token: 0x17001795 RID: 6037
		// (get) Token: 0x06007143 RID: 28995 RVA: 0x00224010 File Offset: 0x00224010
		// (set) Token: 0x06007144 RID: 28996 RVA: 0x00224018 File Offset: 0x00224018
		public double? Minimum { get; set; }

		// Token: 0x17001796 RID: 6038
		// (get) Token: 0x06007145 RID: 28997 RVA: 0x00224024 File Offset: 0x00224024
		// (set) Token: 0x06007146 RID: 28998 RVA: 0x0022402C File Offset: 0x0022402C
		public double? Maximum { get; set; }

		// Token: 0x17001797 RID: 6039
		// (get) Token: 0x06007147 RID: 28999 RVA: 0x00224038 File Offset: 0x00224038
		// (set) Token: 0x06007148 RID: 29000 RVA: 0x00224040 File Offset: 0x00224040
		public bool ExclusiveMinimum { get; set; }

		// Token: 0x17001798 RID: 6040
		// (get) Token: 0x06007149 RID: 29001 RVA: 0x0022404C File Offset: 0x0022404C
		// (set) Token: 0x0600714A RID: 29002 RVA: 0x00224054 File Offset: 0x00224054
		public bool ExclusiveMaximum { get; set; }

		// Token: 0x17001799 RID: 6041
		// (get) Token: 0x0600714B RID: 29003 RVA: 0x00224060 File Offset: 0x00224060
		// (set) Token: 0x0600714C RID: 29004 RVA: 0x00224068 File Offset: 0x00224068
		public int? MinimumItems { get; set; }

		// Token: 0x1700179A RID: 6042
		// (get) Token: 0x0600714D RID: 29005 RVA: 0x00224074 File Offset: 0x00224074
		// (set) Token: 0x0600714E RID: 29006 RVA: 0x0022407C File Offset: 0x0022407C
		public int? MaximumItems { get; set; }

		// Token: 0x1700179B RID: 6043
		// (get) Token: 0x0600714F RID: 29007 RVA: 0x00224088 File Offset: 0x00224088
		// (set) Token: 0x06007150 RID: 29008 RVA: 0x00224090 File Offset: 0x00224090
		public IList<string> Patterns { get; set; }

		// Token: 0x1700179C RID: 6044
		// (get) Token: 0x06007151 RID: 29009 RVA: 0x0022409C File Offset: 0x0022409C
		// (set) Token: 0x06007152 RID: 29010 RVA: 0x002240A4 File Offset: 0x002240A4
		public IList<JsonSchemaModel> Items { get; set; }

		// Token: 0x1700179D RID: 6045
		// (get) Token: 0x06007153 RID: 29011 RVA: 0x002240B0 File Offset: 0x002240B0
		// (set) Token: 0x06007154 RID: 29012 RVA: 0x002240B8 File Offset: 0x002240B8
		public IDictionary<string, JsonSchemaModel> Properties { get; set; }

		// Token: 0x1700179E RID: 6046
		// (get) Token: 0x06007155 RID: 29013 RVA: 0x002240C4 File Offset: 0x002240C4
		// (set) Token: 0x06007156 RID: 29014 RVA: 0x002240CC File Offset: 0x002240CC
		public IDictionary<string, JsonSchemaModel> PatternProperties { get; set; }

		// Token: 0x1700179F RID: 6047
		// (get) Token: 0x06007157 RID: 29015 RVA: 0x002240D8 File Offset: 0x002240D8
		// (set) Token: 0x06007158 RID: 29016 RVA: 0x002240E0 File Offset: 0x002240E0
		public JsonSchemaModel AdditionalProperties { get; set; }

		// Token: 0x170017A0 RID: 6048
		// (get) Token: 0x06007159 RID: 29017 RVA: 0x002240EC File Offset: 0x002240EC
		// (set) Token: 0x0600715A RID: 29018 RVA: 0x002240F4 File Offset: 0x002240F4
		public JsonSchemaModel AdditionalItems { get; set; }

		// Token: 0x170017A1 RID: 6049
		// (get) Token: 0x0600715B RID: 29019 RVA: 0x00224100 File Offset: 0x00224100
		// (set) Token: 0x0600715C RID: 29020 RVA: 0x00224108 File Offset: 0x00224108
		public bool PositionalItemsValidation { get; set; }

		// Token: 0x170017A2 RID: 6050
		// (get) Token: 0x0600715D RID: 29021 RVA: 0x00224114 File Offset: 0x00224114
		// (set) Token: 0x0600715E RID: 29022 RVA: 0x0022411C File Offset: 0x0022411C
		public bool AllowAdditionalProperties { get; set; }

		// Token: 0x170017A3 RID: 6051
		// (get) Token: 0x0600715F RID: 29023 RVA: 0x00224128 File Offset: 0x00224128
		// (set) Token: 0x06007160 RID: 29024 RVA: 0x00224130 File Offset: 0x00224130
		public bool AllowAdditionalItems { get; set; }

		// Token: 0x170017A4 RID: 6052
		// (get) Token: 0x06007161 RID: 29025 RVA: 0x0022413C File Offset: 0x0022413C
		// (set) Token: 0x06007162 RID: 29026 RVA: 0x00224144 File Offset: 0x00224144
		public bool UniqueItems { get; set; }

		// Token: 0x170017A5 RID: 6053
		// (get) Token: 0x06007163 RID: 29027 RVA: 0x00224150 File Offset: 0x00224150
		// (set) Token: 0x06007164 RID: 29028 RVA: 0x00224158 File Offset: 0x00224158
		public IList<JToken> Enum { get; set; }

		// Token: 0x170017A6 RID: 6054
		// (get) Token: 0x06007165 RID: 29029 RVA: 0x00224164 File Offset: 0x00224164
		// (set) Token: 0x06007166 RID: 29030 RVA: 0x0022416C File Offset: 0x0022416C
		public JsonSchemaType Disallow { get; set; }

		// Token: 0x06007167 RID: 29031 RVA: 0x00224178 File Offset: 0x00224178
		public JsonSchemaModel()
		{
			this.Type = JsonSchemaType.Any;
			this.AllowAdditionalProperties = true;
			this.AllowAdditionalItems = true;
			this.Required = false;
		}

		// Token: 0x06007168 RID: 29032 RVA: 0x002241AC File Offset: 0x002241AC
		public static JsonSchemaModel Create(IList<JsonSchema> schemata)
		{
			JsonSchemaModel jsonSchemaModel = new JsonSchemaModel();
			foreach (JsonSchema schema in schemata)
			{
				JsonSchemaModel.Combine(jsonSchemaModel, schema);
			}
			return jsonSchemaModel;
		}

		// Token: 0x06007169 RID: 29033 RVA: 0x00224204 File Offset: 0x00224204
		private static void Combine(JsonSchemaModel model, JsonSchema schema)
		{
			model.Required = (model.Required || schema.Required.GetValueOrDefault());
			model.Type &= (schema.Type ?? JsonSchemaType.Any);
			model.MinimumLength = MathUtils.Max(model.MinimumLength, schema.MinimumLength);
			model.MaximumLength = MathUtils.Min(model.MaximumLength, schema.MaximumLength);
			model.DivisibleBy = MathUtils.Max(model.DivisibleBy, schema.DivisibleBy);
			model.Minimum = MathUtils.Max(model.Minimum, schema.Minimum);
			model.Maximum = MathUtils.Max(model.Maximum, schema.Maximum);
			model.ExclusiveMinimum = (model.ExclusiveMinimum || schema.ExclusiveMinimum.GetValueOrDefault());
			model.ExclusiveMaximum = (model.ExclusiveMaximum || schema.ExclusiveMaximum.GetValueOrDefault());
			model.MinimumItems = MathUtils.Max(model.MinimumItems, schema.MinimumItems);
			model.MaximumItems = MathUtils.Min(model.MaximumItems, schema.MaximumItems);
			model.PositionalItemsValidation = (model.PositionalItemsValidation || schema.PositionalItemsValidation);
			model.AllowAdditionalProperties = (model.AllowAdditionalProperties && schema.AllowAdditionalProperties);
			model.AllowAdditionalItems = (model.AllowAdditionalItems && schema.AllowAdditionalItems);
			model.UniqueItems = (model.UniqueItems || schema.UniqueItems);
			if (schema.Enum != null)
			{
				if (model.Enum == null)
				{
					model.Enum = new List<JToken>();
				}
				model.Enum.AddRangeDistinct(schema.Enum, JToken.EqualityComparer);
			}
			model.Disallow |= schema.Disallow.GetValueOrDefault();
			if (schema.Pattern != null)
			{
				if (model.Patterns == null)
				{
					model.Patterns = new List<string>();
				}
				model.Patterns.AddDistinct(schema.Pattern);
			}
		}
	}
}
