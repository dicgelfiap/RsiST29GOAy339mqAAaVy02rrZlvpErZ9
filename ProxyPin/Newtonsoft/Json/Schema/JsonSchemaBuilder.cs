﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x02000B06 RID: 2822
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	internal class JsonSchemaBuilder
	{
		// Token: 0x06007108 RID: 28936 RVA: 0x002221F8 File Offset: 0x002221F8
		public JsonSchemaBuilder(JsonSchemaResolver resolver)
		{
			this._stack = new List<JsonSchema>();
			this._documentSchemas = new Dictionary<string, JsonSchema>();
			this._resolver = resolver;
		}

		// Token: 0x06007109 RID: 28937 RVA: 0x00222220 File Offset: 0x00222220
		private void Push(JsonSchema value)
		{
			this._currentSchema = value;
			this._stack.Add(value);
			this._resolver.LoadedSchemas.Add(value);
			this._documentSchemas.Add(value.Location, value);
		}

		// Token: 0x0600710A RID: 28938 RVA: 0x00222268 File Offset: 0x00222268
		private JsonSchema Pop()
		{
			JsonSchema currentSchema = this._currentSchema;
			this._stack.RemoveAt(this._stack.Count - 1);
			this._currentSchema = this._stack.LastOrDefault<JsonSchema>();
			return currentSchema;
		}

		// Token: 0x17001789 RID: 6025
		// (get) Token: 0x0600710B RID: 28939 RVA: 0x0022229C File Offset: 0x0022229C
		private JsonSchema CurrentSchema
		{
			get
			{
				return this._currentSchema;
			}
		}

		// Token: 0x0600710C RID: 28940 RVA: 0x002222A4 File Offset: 0x002222A4
		internal JsonSchema Read(JsonReader reader)
		{
			JToken jtoken = JToken.ReadFrom(reader);
			this._rootSchema = (jtoken as JObject);
			JsonSchema jsonSchema = this.BuildSchema(jtoken);
			this.ResolveReferences(jsonSchema);
			return jsonSchema;
		}

		// Token: 0x0600710D RID: 28941 RVA: 0x002222DC File Offset: 0x002222DC
		private string UnescapeReference(string reference)
		{
			return Uri.UnescapeDataString(reference).Replace("~1", "/").Replace("~0", "~");
		}

		// Token: 0x0600710E RID: 28942 RVA: 0x00222304 File Offset: 0x00222304
		private JsonSchema ResolveReferences(JsonSchema schema)
		{
			if (schema.DeferredReference != null)
			{
				string text = schema.DeferredReference;
				bool flag = text.StartsWith("#", StringComparison.Ordinal);
				if (flag)
				{
					text = this.UnescapeReference(text);
				}
				JsonSchema jsonSchema = this._resolver.GetSchema(text);
				if (jsonSchema == null)
				{
					if (flag)
					{
						string[] array = schema.DeferredReference.TrimStart(new char[]
						{
							'#'
						}).Split(new char[]
						{
							'/'
						}, StringSplitOptions.RemoveEmptyEntries);
						JToken jtoken = this._rootSchema;
						foreach (string reference in array)
						{
							string text2 = this.UnescapeReference(reference);
							if (jtoken.Type == JTokenType.Object)
							{
								jtoken = jtoken[text2];
							}
							else if (jtoken.Type == JTokenType.Array || jtoken.Type == JTokenType.Constructor)
							{
								int num;
								if (int.TryParse(text2, out num) && num >= 0 && num < jtoken.Count<JToken>())
								{
									jtoken = jtoken[num];
								}
								else
								{
									jtoken = null;
								}
							}
							if (jtoken == null)
							{
								break;
							}
						}
						if (jtoken != null)
						{
							jsonSchema = this.BuildSchema(jtoken);
						}
					}
					if (jsonSchema == null)
					{
						throw new JsonException("Could not resolve schema reference '{0}'.".FormatWith(CultureInfo.InvariantCulture, schema.DeferredReference));
					}
				}
				schema = jsonSchema;
			}
			if (schema.ReferencesResolved)
			{
				return schema;
			}
			schema.ReferencesResolved = true;
			if (schema.Extends != null)
			{
				for (int j = 0; j < schema.Extends.Count; j++)
				{
					schema.Extends[j] = this.ResolveReferences(schema.Extends[j]);
				}
			}
			if (schema.Items != null)
			{
				for (int k = 0; k < schema.Items.Count; k++)
				{
					schema.Items[k] = this.ResolveReferences(schema.Items[k]);
				}
			}
			if (schema.AdditionalItems != null)
			{
				schema.AdditionalItems = this.ResolveReferences(schema.AdditionalItems);
			}
			if (schema.PatternProperties != null)
			{
				foreach (KeyValuePair<string, JsonSchema> keyValuePair in schema.PatternProperties.ToList<KeyValuePair<string, JsonSchema>>())
				{
					schema.PatternProperties[keyValuePair.Key] = this.ResolveReferences(keyValuePair.Value);
				}
			}
			if (schema.Properties != null)
			{
				foreach (KeyValuePair<string, JsonSchema> keyValuePair2 in schema.Properties.ToList<KeyValuePair<string, JsonSchema>>())
				{
					schema.Properties[keyValuePair2.Key] = this.ResolveReferences(keyValuePair2.Value);
				}
			}
			if (schema.AdditionalProperties != null)
			{
				schema.AdditionalProperties = this.ResolveReferences(schema.AdditionalProperties);
			}
			return schema;
		}

		// Token: 0x0600710F RID: 28943 RVA: 0x00222620 File Offset: 0x00222620
		private JsonSchema BuildSchema(JToken token)
		{
			JObject jobject = token as JObject;
			if (jobject == null)
			{
				throw JsonException.Create(token, token.Path, "Expected object while parsing schema object, got {0}.".FormatWith(CultureInfo.InvariantCulture, token.Type));
			}
			JToken value;
			if (jobject.TryGetValue("$ref", out value))
			{
				return new JsonSchema
				{
					DeferredReference = (string)value
				};
			}
			string text = token.Path.Replace(".", "/").Replace("[", "/").Replace("]", string.Empty);
			if (!StringUtils.IsNullOrEmpty(text))
			{
				text = "/" + text;
			}
			text = "#" + text;
			JsonSchema result;
			if (this._documentSchemas.TryGetValue(text, out result))
			{
				return result;
			}
			this.Push(new JsonSchema
			{
				Location = text
			});
			this.ProcessSchemaProperties(jobject);
			return this.Pop();
		}

		// Token: 0x06007110 RID: 28944 RVA: 0x00222714 File Offset: 0x00222714
		private void ProcessSchemaProperties(JObject schemaObject)
		{
			foreach (KeyValuePair<string, JToken> keyValuePair in schemaObject)
			{
				string key = keyValuePair.Key;
				if (key != null)
				{
					uint num = Newtonsoft.Json.<PrivateImplementationDetails>.ComputeStringHash(key);
					if (num <= 2223801888U)
					{
						if (num <= 981021583U)
						{
							if (num <= 353304077U)
							{
								if (num != 299789532U)
								{
									if (num != 334560121U)
									{
										if (num == 353304077U)
										{
											if (key == "divisibleBy")
											{
												this.CurrentSchema.DivisibleBy = new double?((double)keyValuePair.Value);
											}
										}
									}
									else if (key == "minItems")
									{
										this.CurrentSchema.MinimumItems = new int?((int)keyValuePair.Value);
									}
								}
								else if (key == "properties")
								{
									this.CurrentSchema.Properties = this.ProcessProperties(keyValuePair.Value);
								}
							}
							else if (num <= 879704937U)
							{
								if (num != 479998177U)
								{
									if (num == 879704937U)
									{
										if (key == "description")
										{
											this.CurrentSchema.Description = (string)keyValuePair.Value;
										}
									}
								}
								else if (key == "additionalProperties")
								{
									this.ProcessAdditionalProperties(keyValuePair.Value);
								}
							}
							else if (num != 926444256U)
							{
								if (num == 981021583U)
								{
									if (key == "items")
									{
										this.ProcessItems(keyValuePair.Value);
									}
								}
							}
							else if (key == "id")
							{
								this.CurrentSchema.Id = (string)keyValuePair.Value;
							}
						}
						else if (num <= 1693958795U)
						{
							if (num != 1361572173U)
							{
								if (num != 1542649473U)
								{
									if (num == 1693958795U)
									{
										if (key == "exclusiveMaximum")
										{
											this.CurrentSchema.ExclusiveMaximum = new bool?((bool)keyValuePair.Value);
										}
									}
								}
								else if (key == "maximum")
								{
									this.CurrentSchema.Maximum = new double?((double)keyValuePair.Value);
								}
							}
							else if (key == "type")
							{
								this.CurrentSchema.Type = this.ProcessType(keyValuePair.Value);
							}
						}
						else if (num <= 2051482624U)
						{
							if (num != 1913005517U)
							{
								if (num == 2051482624U)
								{
									if (key == "additionalItems")
									{
										this.ProcessAdditionalItems(keyValuePair.Value);
									}
								}
							}
							else if (key == "exclusiveMinimum")
							{
								this.CurrentSchema.ExclusiveMinimum = new bool?((bool)keyValuePair.Value);
							}
						}
						else if (num != 2171383808U)
						{
							if (num == 2223801888U)
							{
								if (key == "required")
								{
									this.CurrentSchema.Required = new bool?((bool)keyValuePair.Value);
								}
							}
						}
						else if (key == "enum")
						{
							this.ProcessEnum(keyValuePair.Value);
						}
					}
					else if (num <= 2692244416U)
					{
						if (num <= 2474713847U)
						{
							if (num != 2268922153U)
							{
								if (num != 2470140894U)
								{
									if (num == 2474713847U)
									{
										if (key == "minimum")
										{
											this.CurrentSchema.Minimum = new double?((double)keyValuePair.Value);
										}
									}
								}
								else if (key == "default")
								{
									this.CurrentSchema.Default = keyValuePair.Value.DeepClone();
								}
							}
							else if (key == "pattern")
							{
								this.CurrentSchema.Pattern = (string)keyValuePair.Value;
							}
						}
						else if (num <= 2609687125U)
						{
							if (num != 2556802313U)
							{
								if (num == 2609687125U)
								{
									if (key == "requires")
									{
										this.CurrentSchema.Requires = (string)keyValuePair.Value;
									}
								}
							}
							else if (key == "title")
							{
								this.CurrentSchema.Title = (string)keyValuePair.Value;
							}
						}
						else if (num != 2642794062U)
						{
							if (num == 2692244416U)
							{
								if (key == "disallow")
								{
									this.CurrentSchema.Disallow = this.ProcessType(keyValuePair.Value);
								}
							}
						}
						else if (key == "extends")
						{
							this.ProcessExtends(keyValuePair.Value);
						}
					}
					else if (num <= 3522602594U)
					{
						if (num <= 3114108242U)
						{
							if (num != 2957261815U)
							{
								if (num == 3114108242U)
								{
									if (key == "format")
									{
										this.CurrentSchema.Format = (string)keyValuePair.Value;
									}
								}
							}
							else if (key == "minLength")
							{
								this.CurrentSchema.MinimumLength = new int?((int)keyValuePair.Value);
							}
						}
						else if (num != 3456888823U)
						{
							if (num == 3522602594U)
							{
								if (key == "uniqueItems")
								{
									this.CurrentSchema.UniqueItems = (bool)keyValuePair.Value;
								}
							}
						}
						else if (key == "readonly")
						{
							this.CurrentSchema.ReadOnly = new bool?((bool)keyValuePair.Value);
						}
					}
					else if (num <= 3947606640U)
					{
						if (num != 3526559937U)
						{
							if (num == 3947606640U)
							{
								if (key == "patternProperties")
								{
									this.CurrentSchema.PatternProperties = this.ProcessProperties(keyValuePair.Value);
								}
							}
						}
						else if (key == "maxLength")
						{
							this.CurrentSchema.MaximumLength = new int?((int)keyValuePair.Value);
						}
					}
					else if (num != 4128829753U)
					{
						if (num == 4244322099U)
						{
							if (key == "maxItems")
							{
								this.CurrentSchema.MaximumItems = new int?((int)keyValuePair.Value);
							}
						}
					}
					else if (key == "hidden")
					{
						this.CurrentSchema.Hidden = new bool?((bool)keyValuePair.Value);
					}
				}
			}
		}

		// Token: 0x06007111 RID: 28945 RVA: 0x00222F20 File Offset: 0x00222F20
		private void ProcessExtends(JToken token)
		{
			IList<JsonSchema> list = new List<JsonSchema>();
			if (token.Type == JTokenType.Array)
			{
				using (IEnumerator<JToken> enumerator = ((IEnumerable<JToken>)token).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						JToken token2 = enumerator.Current;
						list.Add(this.BuildSchema(token2));
					}
					goto IL_61;
				}
			}
			JsonSchema jsonSchema = this.BuildSchema(token);
			if (jsonSchema != null)
			{
				list.Add(jsonSchema);
			}
			IL_61:
			if (list.Count > 0)
			{
				this.CurrentSchema.Extends = list;
			}
		}

		// Token: 0x06007112 RID: 28946 RVA: 0x00222FB8 File Offset: 0x00222FB8
		private void ProcessEnum(JToken token)
		{
			if (token.Type != JTokenType.Array)
			{
				throw JsonException.Create(token, token.Path, "Expected Array token while parsing enum values, got {0}.".FormatWith(CultureInfo.InvariantCulture, token.Type));
			}
			this.CurrentSchema.Enum = new List<JToken>();
			foreach (JToken jtoken in ((IEnumerable<JToken>)token))
			{
				this.CurrentSchema.Enum.Add(jtoken.DeepClone());
			}
		}

		// Token: 0x06007113 RID: 28947 RVA: 0x0022305C File Offset: 0x0022305C
		private void ProcessAdditionalProperties(JToken token)
		{
			if (token.Type == JTokenType.Boolean)
			{
				this.CurrentSchema.AllowAdditionalProperties = (bool)token;
				return;
			}
			this.CurrentSchema.AdditionalProperties = this.BuildSchema(token);
		}

		// Token: 0x06007114 RID: 28948 RVA: 0x002230A0 File Offset: 0x002230A0
		private void ProcessAdditionalItems(JToken token)
		{
			if (token.Type == JTokenType.Boolean)
			{
				this.CurrentSchema.AllowAdditionalItems = (bool)token;
				return;
			}
			this.CurrentSchema.AdditionalItems = this.BuildSchema(token);
		}

		// Token: 0x06007115 RID: 28949 RVA: 0x002230E4 File Offset: 0x002230E4
		private IDictionary<string, JsonSchema> ProcessProperties(JToken token)
		{
			IDictionary<string, JsonSchema> dictionary = new Dictionary<string, JsonSchema>();
			if (token.Type != JTokenType.Object)
			{
				throw JsonException.Create(token, token.Path, "Expected Object token while parsing schema properties, got {0}.".FormatWith(CultureInfo.InvariantCulture, token.Type));
			}
			foreach (JToken jtoken in ((IEnumerable<JToken>)token))
			{
				JProperty jproperty = (JProperty)jtoken;
				if (dictionary.ContainsKey(jproperty.Name))
				{
					throw new JsonException("Property {0} has already been defined in schema.".FormatWith(CultureInfo.InvariantCulture, jproperty.Name));
				}
				dictionary.Add(jproperty.Name, this.BuildSchema(jproperty.Value));
			}
			return dictionary;
		}

		// Token: 0x06007116 RID: 28950 RVA: 0x002231B4 File Offset: 0x002231B4
		private void ProcessItems(JToken token)
		{
			this.CurrentSchema.Items = new List<JsonSchema>();
			JTokenType type = token.Type;
			if (type != JTokenType.Object)
			{
				if (type == JTokenType.Array)
				{
					this.CurrentSchema.PositionalItemsValidation = true;
					using (IEnumerator<JToken> enumerator = ((IEnumerable<JToken>)token).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							JToken token2 = enumerator.Current;
							this.CurrentSchema.Items.Add(this.BuildSchema(token2));
						}
						return;
					}
				}
				throw JsonException.Create(token, token.Path, "Expected array or JSON schema object, got {0}.".FormatWith(CultureInfo.InvariantCulture, token.Type));
			}
			this.CurrentSchema.Items.Add(this.BuildSchema(token));
			this.CurrentSchema.PositionalItemsValidation = false;
		}

		// Token: 0x06007117 RID: 28951 RVA: 0x00223298 File Offset: 0x00223298
		private JsonSchemaType? ProcessType(JToken token)
		{
			JTokenType type = token.Type;
			if (type == JTokenType.Array)
			{
				JsonSchemaType? jsonSchemaType = new JsonSchemaType?(JsonSchemaType.None);
				foreach (JToken jtoken in ((IEnumerable<JToken>)token))
				{
					if (jtoken.Type != JTokenType.String)
					{
						throw JsonException.Create(jtoken, jtoken.Path, "Expected JSON schema type string token, got {0}.".FormatWith(CultureInfo.InvariantCulture, token.Type));
					}
					jsonSchemaType |= JsonSchemaBuilder.MapType((string)jtoken);
				}
				return jsonSchemaType;
			}
			if (type != JTokenType.String)
			{
				throw JsonException.Create(token, token.Path, "Expected array or JSON schema type string token, got {0}.".FormatWith(CultureInfo.InvariantCulture, token.Type));
			}
			return new JsonSchemaType?(JsonSchemaBuilder.MapType((string)token));
		}

		// Token: 0x06007118 RID: 28952 RVA: 0x002233AC File Offset: 0x002233AC
		internal static JsonSchemaType MapType(string type)
		{
			JsonSchemaType result;
			if (!JsonSchemaConstants.JsonSchemaTypeMapping.TryGetValue(type, out result))
			{
				throw new JsonException("Invalid JSON schema type: {0}".FormatWith(CultureInfo.InvariantCulture, type));
			}
			return result;
		}

		// Token: 0x06007119 RID: 28953 RVA: 0x002233E8 File Offset: 0x002233E8
		internal static string MapType(JsonSchemaType type)
		{
			return JsonSchemaConstants.JsonSchemaTypeMapping.Single((KeyValuePair<string, JsonSchemaType> kv) => kv.Value == type).Key;
		}

		// Token: 0x040037F5 RID: 14325
		private readonly IList<JsonSchema> _stack;

		// Token: 0x040037F6 RID: 14326
		private readonly JsonSchemaResolver _resolver;

		// Token: 0x040037F7 RID: 14327
		private readonly IDictionary<string, JsonSchema> _documentSchemas;

		// Token: 0x040037F8 RID: 14328
		private JsonSchema _currentSchema;

		// Token: 0x040037F9 RID: 14329
		private JObject _rootSchema;
	}
}
