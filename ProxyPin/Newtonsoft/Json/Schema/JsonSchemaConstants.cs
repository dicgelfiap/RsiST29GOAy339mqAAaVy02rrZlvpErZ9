using System;
using System.Collections.Generic;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x02000B07 RID: 2823
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	internal static class JsonSchemaConstants
	{
		// Token: 0x040037FA RID: 14330
		public const string TypePropertyName = "type";

		// Token: 0x040037FB RID: 14331
		public const string PropertiesPropertyName = "properties";

		// Token: 0x040037FC RID: 14332
		public const string ItemsPropertyName = "items";

		// Token: 0x040037FD RID: 14333
		public const string AdditionalItemsPropertyName = "additionalItems";

		// Token: 0x040037FE RID: 14334
		public const string RequiredPropertyName = "required";

		// Token: 0x040037FF RID: 14335
		public const string PatternPropertiesPropertyName = "patternProperties";

		// Token: 0x04003800 RID: 14336
		public const string AdditionalPropertiesPropertyName = "additionalProperties";

		// Token: 0x04003801 RID: 14337
		public const string RequiresPropertyName = "requires";

		// Token: 0x04003802 RID: 14338
		public const string MinimumPropertyName = "minimum";

		// Token: 0x04003803 RID: 14339
		public const string MaximumPropertyName = "maximum";

		// Token: 0x04003804 RID: 14340
		public const string ExclusiveMinimumPropertyName = "exclusiveMinimum";

		// Token: 0x04003805 RID: 14341
		public const string ExclusiveMaximumPropertyName = "exclusiveMaximum";

		// Token: 0x04003806 RID: 14342
		public const string MinimumItemsPropertyName = "minItems";

		// Token: 0x04003807 RID: 14343
		public const string MaximumItemsPropertyName = "maxItems";

		// Token: 0x04003808 RID: 14344
		public const string PatternPropertyName = "pattern";

		// Token: 0x04003809 RID: 14345
		public const string MaximumLengthPropertyName = "maxLength";

		// Token: 0x0400380A RID: 14346
		public const string MinimumLengthPropertyName = "minLength";

		// Token: 0x0400380B RID: 14347
		public const string EnumPropertyName = "enum";

		// Token: 0x0400380C RID: 14348
		public const string ReadOnlyPropertyName = "readonly";

		// Token: 0x0400380D RID: 14349
		public const string TitlePropertyName = "title";

		// Token: 0x0400380E RID: 14350
		public const string DescriptionPropertyName = "description";

		// Token: 0x0400380F RID: 14351
		public const string FormatPropertyName = "format";

		// Token: 0x04003810 RID: 14352
		public const string DefaultPropertyName = "default";

		// Token: 0x04003811 RID: 14353
		public const string TransientPropertyName = "transient";

		// Token: 0x04003812 RID: 14354
		public const string DivisibleByPropertyName = "divisibleBy";

		// Token: 0x04003813 RID: 14355
		public const string HiddenPropertyName = "hidden";

		// Token: 0x04003814 RID: 14356
		public const string DisallowPropertyName = "disallow";

		// Token: 0x04003815 RID: 14357
		public const string ExtendsPropertyName = "extends";

		// Token: 0x04003816 RID: 14358
		public const string IdPropertyName = "id";

		// Token: 0x04003817 RID: 14359
		public const string UniqueItemsPropertyName = "uniqueItems";

		// Token: 0x04003818 RID: 14360
		public const string OptionValuePropertyName = "value";

		// Token: 0x04003819 RID: 14361
		public const string OptionLabelPropertyName = "label";

		// Token: 0x0400381A RID: 14362
		public static readonly IDictionary<string, JsonSchemaType> JsonSchemaTypeMapping = new Dictionary<string, JsonSchemaType>
		{
			{
				"string",
				JsonSchemaType.String
			},
			{
				"object",
				JsonSchemaType.Object
			},
			{
				"integer",
				JsonSchemaType.Integer
			},
			{
				"number",
				JsonSchemaType.Float
			},
			{
				"null",
				JsonSchemaType.Null
			},
			{
				"boolean",
				JsonSchemaType.Boolean
			},
			{
				"array",
				JsonSchemaType.Array
			},
			{
				"any",
				JsonSchemaType.Any
			}
		};
	}
}
