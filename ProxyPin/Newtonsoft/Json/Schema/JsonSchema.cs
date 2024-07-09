using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x02000B05 RID: 2821
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	public class JsonSchema
	{
		// Token: 0x17001764 RID: 5988
		// (get) Token: 0x060070B7 RID: 28855 RVA: 0x00221DDC File Offset: 0x00221DDC
		// (set) Token: 0x060070B8 RID: 28856 RVA: 0x00221DE4 File Offset: 0x00221DE4
		public string Id { get; set; }

		// Token: 0x17001765 RID: 5989
		// (get) Token: 0x060070B9 RID: 28857 RVA: 0x00221DF0 File Offset: 0x00221DF0
		// (set) Token: 0x060070BA RID: 28858 RVA: 0x00221DF8 File Offset: 0x00221DF8
		public string Title { get; set; }

		// Token: 0x17001766 RID: 5990
		// (get) Token: 0x060070BB RID: 28859 RVA: 0x00221E04 File Offset: 0x00221E04
		// (set) Token: 0x060070BC RID: 28860 RVA: 0x00221E0C File Offset: 0x00221E0C
		public bool? Required { get; set; }

		// Token: 0x17001767 RID: 5991
		// (get) Token: 0x060070BD RID: 28861 RVA: 0x00221E18 File Offset: 0x00221E18
		// (set) Token: 0x060070BE RID: 28862 RVA: 0x00221E20 File Offset: 0x00221E20
		public bool? ReadOnly { get; set; }

		// Token: 0x17001768 RID: 5992
		// (get) Token: 0x060070BF RID: 28863 RVA: 0x00221E2C File Offset: 0x00221E2C
		// (set) Token: 0x060070C0 RID: 28864 RVA: 0x00221E34 File Offset: 0x00221E34
		public bool? Hidden { get; set; }

		// Token: 0x17001769 RID: 5993
		// (get) Token: 0x060070C1 RID: 28865 RVA: 0x00221E40 File Offset: 0x00221E40
		// (set) Token: 0x060070C2 RID: 28866 RVA: 0x00221E48 File Offset: 0x00221E48
		public bool? Transient { get; set; }

		// Token: 0x1700176A RID: 5994
		// (get) Token: 0x060070C3 RID: 28867 RVA: 0x00221E54 File Offset: 0x00221E54
		// (set) Token: 0x060070C4 RID: 28868 RVA: 0x00221E5C File Offset: 0x00221E5C
		public string Description { get; set; }

		// Token: 0x1700176B RID: 5995
		// (get) Token: 0x060070C5 RID: 28869 RVA: 0x00221E68 File Offset: 0x00221E68
		// (set) Token: 0x060070C6 RID: 28870 RVA: 0x00221E70 File Offset: 0x00221E70
		public JsonSchemaType? Type { get; set; }

		// Token: 0x1700176C RID: 5996
		// (get) Token: 0x060070C7 RID: 28871 RVA: 0x00221E7C File Offset: 0x00221E7C
		// (set) Token: 0x060070C8 RID: 28872 RVA: 0x00221E84 File Offset: 0x00221E84
		public string Pattern { get; set; }

		// Token: 0x1700176D RID: 5997
		// (get) Token: 0x060070C9 RID: 28873 RVA: 0x00221E90 File Offset: 0x00221E90
		// (set) Token: 0x060070CA RID: 28874 RVA: 0x00221E98 File Offset: 0x00221E98
		public int? MinimumLength { get; set; }

		// Token: 0x1700176E RID: 5998
		// (get) Token: 0x060070CB RID: 28875 RVA: 0x00221EA4 File Offset: 0x00221EA4
		// (set) Token: 0x060070CC RID: 28876 RVA: 0x00221EAC File Offset: 0x00221EAC
		public int? MaximumLength { get; set; }

		// Token: 0x1700176F RID: 5999
		// (get) Token: 0x060070CD RID: 28877 RVA: 0x00221EB8 File Offset: 0x00221EB8
		// (set) Token: 0x060070CE RID: 28878 RVA: 0x00221EC0 File Offset: 0x00221EC0
		public double? DivisibleBy { get; set; }

		// Token: 0x17001770 RID: 6000
		// (get) Token: 0x060070CF RID: 28879 RVA: 0x00221ECC File Offset: 0x00221ECC
		// (set) Token: 0x060070D0 RID: 28880 RVA: 0x00221ED4 File Offset: 0x00221ED4
		public double? Minimum { get; set; }

		// Token: 0x17001771 RID: 6001
		// (get) Token: 0x060070D1 RID: 28881 RVA: 0x00221EE0 File Offset: 0x00221EE0
		// (set) Token: 0x060070D2 RID: 28882 RVA: 0x00221EE8 File Offset: 0x00221EE8
		public double? Maximum { get; set; }

		// Token: 0x17001772 RID: 6002
		// (get) Token: 0x060070D3 RID: 28883 RVA: 0x00221EF4 File Offset: 0x00221EF4
		// (set) Token: 0x060070D4 RID: 28884 RVA: 0x00221EFC File Offset: 0x00221EFC
		public bool? ExclusiveMinimum { get; set; }

		// Token: 0x17001773 RID: 6003
		// (get) Token: 0x060070D5 RID: 28885 RVA: 0x00221F08 File Offset: 0x00221F08
		// (set) Token: 0x060070D6 RID: 28886 RVA: 0x00221F10 File Offset: 0x00221F10
		public bool? ExclusiveMaximum { get; set; }

		// Token: 0x17001774 RID: 6004
		// (get) Token: 0x060070D7 RID: 28887 RVA: 0x00221F1C File Offset: 0x00221F1C
		// (set) Token: 0x060070D8 RID: 28888 RVA: 0x00221F24 File Offset: 0x00221F24
		public int? MinimumItems { get; set; }

		// Token: 0x17001775 RID: 6005
		// (get) Token: 0x060070D9 RID: 28889 RVA: 0x00221F30 File Offset: 0x00221F30
		// (set) Token: 0x060070DA RID: 28890 RVA: 0x00221F38 File Offset: 0x00221F38
		public int? MaximumItems { get; set; }

		// Token: 0x17001776 RID: 6006
		// (get) Token: 0x060070DB RID: 28891 RVA: 0x00221F44 File Offset: 0x00221F44
		// (set) Token: 0x060070DC RID: 28892 RVA: 0x00221F4C File Offset: 0x00221F4C
		public IList<JsonSchema> Items { get; set; }

		// Token: 0x17001777 RID: 6007
		// (get) Token: 0x060070DD RID: 28893 RVA: 0x00221F58 File Offset: 0x00221F58
		// (set) Token: 0x060070DE RID: 28894 RVA: 0x00221F60 File Offset: 0x00221F60
		public bool PositionalItemsValidation { get; set; }

		// Token: 0x17001778 RID: 6008
		// (get) Token: 0x060070DF RID: 28895 RVA: 0x00221F6C File Offset: 0x00221F6C
		// (set) Token: 0x060070E0 RID: 28896 RVA: 0x00221F74 File Offset: 0x00221F74
		public JsonSchema AdditionalItems { get; set; }

		// Token: 0x17001779 RID: 6009
		// (get) Token: 0x060070E1 RID: 28897 RVA: 0x00221F80 File Offset: 0x00221F80
		// (set) Token: 0x060070E2 RID: 28898 RVA: 0x00221F88 File Offset: 0x00221F88
		public bool AllowAdditionalItems { get; set; }

		// Token: 0x1700177A RID: 6010
		// (get) Token: 0x060070E3 RID: 28899 RVA: 0x00221F94 File Offset: 0x00221F94
		// (set) Token: 0x060070E4 RID: 28900 RVA: 0x00221F9C File Offset: 0x00221F9C
		public bool UniqueItems { get; set; }

		// Token: 0x1700177B RID: 6011
		// (get) Token: 0x060070E5 RID: 28901 RVA: 0x00221FA8 File Offset: 0x00221FA8
		// (set) Token: 0x060070E6 RID: 28902 RVA: 0x00221FB0 File Offset: 0x00221FB0
		public IDictionary<string, JsonSchema> Properties { get; set; }

		// Token: 0x1700177C RID: 6012
		// (get) Token: 0x060070E7 RID: 28903 RVA: 0x00221FBC File Offset: 0x00221FBC
		// (set) Token: 0x060070E8 RID: 28904 RVA: 0x00221FC4 File Offset: 0x00221FC4
		public JsonSchema AdditionalProperties { get; set; }

		// Token: 0x1700177D RID: 6013
		// (get) Token: 0x060070E9 RID: 28905 RVA: 0x00221FD0 File Offset: 0x00221FD0
		// (set) Token: 0x060070EA RID: 28906 RVA: 0x00221FD8 File Offset: 0x00221FD8
		public IDictionary<string, JsonSchema> PatternProperties { get; set; }

		// Token: 0x1700177E RID: 6014
		// (get) Token: 0x060070EB RID: 28907 RVA: 0x00221FE4 File Offset: 0x00221FE4
		// (set) Token: 0x060070EC RID: 28908 RVA: 0x00221FEC File Offset: 0x00221FEC
		public bool AllowAdditionalProperties { get; set; }

		// Token: 0x1700177F RID: 6015
		// (get) Token: 0x060070ED RID: 28909 RVA: 0x00221FF8 File Offset: 0x00221FF8
		// (set) Token: 0x060070EE RID: 28910 RVA: 0x00222000 File Offset: 0x00222000
		public string Requires { get; set; }

		// Token: 0x17001780 RID: 6016
		// (get) Token: 0x060070EF RID: 28911 RVA: 0x0022200C File Offset: 0x0022200C
		// (set) Token: 0x060070F0 RID: 28912 RVA: 0x00222014 File Offset: 0x00222014
		public IList<JToken> Enum { get; set; }

		// Token: 0x17001781 RID: 6017
		// (get) Token: 0x060070F1 RID: 28913 RVA: 0x00222020 File Offset: 0x00222020
		// (set) Token: 0x060070F2 RID: 28914 RVA: 0x00222028 File Offset: 0x00222028
		public JsonSchemaType? Disallow { get; set; }

		// Token: 0x17001782 RID: 6018
		// (get) Token: 0x060070F3 RID: 28915 RVA: 0x00222034 File Offset: 0x00222034
		// (set) Token: 0x060070F4 RID: 28916 RVA: 0x0022203C File Offset: 0x0022203C
		public JToken Default { get; set; }

		// Token: 0x17001783 RID: 6019
		// (get) Token: 0x060070F5 RID: 28917 RVA: 0x00222048 File Offset: 0x00222048
		// (set) Token: 0x060070F6 RID: 28918 RVA: 0x00222050 File Offset: 0x00222050
		public IList<JsonSchema> Extends { get; set; }

		// Token: 0x17001784 RID: 6020
		// (get) Token: 0x060070F7 RID: 28919 RVA: 0x0022205C File Offset: 0x0022205C
		// (set) Token: 0x060070F8 RID: 28920 RVA: 0x00222064 File Offset: 0x00222064
		public string Format { get; set; }

		// Token: 0x17001785 RID: 6021
		// (get) Token: 0x060070F9 RID: 28921 RVA: 0x00222070 File Offset: 0x00222070
		// (set) Token: 0x060070FA RID: 28922 RVA: 0x00222078 File Offset: 0x00222078
		internal string Location { get; set; }

		// Token: 0x17001786 RID: 6022
		// (get) Token: 0x060070FB RID: 28923 RVA: 0x00222084 File Offset: 0x00222084
		internal string InternalId
		{
			get
			{
				return this._internalId;
			}
		}

		// Token: 0x17001787 RID: 6023
		// (get) Token: 0x060070FC RID: 28924 RVA: 0x0022208C File Offset: 0x0022208C
		// (set) Token: 0x060070FD RID: 28925 RVA: 0x00222094 File Offset: 0x00222094
		internal string DeferredReference { get; set; }

		// Token: 0x17001788 RID: 6024
		// (get) Token: 0x060070FE RID: 28926 RVA: 0x002220A0 File Offset: 0x002220A0
		// (set) Token: 0x060070FF RID: 28927 RVA: 0x002220A8 File Offset: 0x002220A8
		internal bool ReferencesResolved { get; set; }

		// Token: 0x06007100 RID: 28928 RVA: 0x002220B4 File Offset: 0x002220B4
		public JsonSchema()
		{
			this.AllowAdditionalProperties = true;
			this.AllowAdditionalItems = true;
		}

		// Token: 0x06007101 RID: 28929 RVA: 0x002220F4 File Offset: 0x002220F4
		public static JsonSchema Read(JsonReader reader)
		{
			return JsonSchema.Read(reader, new JsonSchemaResolver());
		}

		// Token: 0x06007102 RID: 28930 RVA: 0x00222104 File Offset: 0x00222104
		public static JsonSchema Read(JsonReader reader, JsonSchemaResolver resolver)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			ValidationUtils.ArgumentNotNull(resolver, "resolver");
			return new JsonSchemaBuilder(resolver).Read(reader);
		}

		// Token: 0x06007103 RID: 28931 RVA: 0x00222128 File Offset: 0x00222128
		public static JsonSchema Parse(string json)
		{
			return JsonSchema.Parse(json, new JsonSchemaResolver());
		}

		// Token: 0x06007104 RID: 28932 RVA: 0x00222138 File Offset: 0x00222138
		public static JsonSchema Parse(string json, JsonSchemaResolver resolver)
		{
			ValidationUtils.ArgumentNotNull(json, "json");
			JsonSchema result;
			using (JsonReader jsonReader = new JsonTextReader(new StringReader(json)))
			{
				result = JsonSchema.Read(jsonReader, resolver);
			}
			return result;
		}

		// Token: 0x06007105 RID: 28933 RVA: 0x00222188 File Offset: 0x00222188
		public void WriteTo(JsonWriter writer)
		{
			this.WriteTo(writer, new JsonSchemaResolver());
		}

		// Token: 0x06007106 RID: 28934 RVA: 0x00222198 File Offset: 0x00222198
		public void WriteTo(JsonWriter writer, JsonSchemaResolver resolver)
		{
			ValidationUtils.ArgumentNotNull(writer, "writer");
			ValidationUtils.ArgumentNotNull(resolver, "resolver");
			new JsonSchemaWriter(writer, resolver).WriteSchema(this);
		}

		// Token: 0x06007107 RID: 28935 RVA: 0x002221C0 File Offset: 0x002221C0
		public override string ToString()
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			this.WriteTo(new JsonTextWriter(stringWriter)
			{
				Formatting = Formatting.Indented
			});
			return stringWriter.ToString();
		}

		// Token: 0x040037F2 RID: 14322
		private readonly string _internalId = Guid.NewGuid().ToString("N");
	}
}
