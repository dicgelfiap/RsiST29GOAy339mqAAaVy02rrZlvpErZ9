using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000AF5 RID: 2805
	[NullableContext(1)]
	[Nullable(0)]
	internal class JsonSerializerInternalWriter : JsonSerializerInternalBase
	{
		// Token: 0x06006FB9 RID: 28601 RVA: 0x0021E0D8 File Offset: 0x0021E0D8
		public JsonSerializerInternalWriter(JsonSerializer serializer) : base(serializer)
		{
		}

		// Token: 0x06006FBA RID: 28602 RVA: 0x0021E0EC File Offset: 0x0021E0EC
		[NullableContext(2)]
		public void Serialize([Nullable(1)] JsonWriter jsonWriter, object value, Type objectType)
		{
			if (jsonWriter == null)
			{
				throw new ArgumentNullException("jsonWriter");
			}
			this._rootType = objectType;
			this._rootLevel = this._serializeStack.Count + 1;
			JsonContract contractSafe = this.GetContractSafe(value);
			try
			{
				if (this.ShouldWriteReference(value, null, contractSafe, null, null))
				{
					this.WriteReference(jsonWriter, value);
				}
				else
				{
					this.SerializeValue(jsonWriter, value, contractSafe, null, null, null);
				}
			}
			catch (Exception ex)
			{
				if (!base.IsErrorHandled(null, contractSafe, null, null, jsonWriter.Path, ex))
				{
					base.ClearErrorContext();
					throw;
				}
				this.HandleError(jsonWriter, 0);
			}
			finally
			{
				this._rootType = null;
			}
		}

		// Token: 0x06006FBB RID: 28603 RVA: 0x0021E1B0 File Offset: 0x0021E1B0
		private JsonSerializerProxy GetInternalSerializer()
		{
			if (this.InternalSerializer == null)
			{
				this.InternalSerializer = new JsonSerializerProxy(this);
			}
			return this.InternalSerializer;
		}

		// Token: 0x06006FBC RID: 28604 RVA: 0x0021E1D0 File Offset: 0x0021E1D0
		[NullableContext(2)]
		private JsonContract GetContractSafe(object value)
		{
			if (value == null)
			{
				return null;
			}
			return this.GetContract(value);
		}

		// Token: 0x06006FBD RID: 28605 RVA: 0x0021E1E4 File Offset: 0x0021E1E4
		private JsonContract GetContract(object value)
		{
			return this.Serializer._contractResolver.ResolveContract(value.GetType());
		}

		// Token: 0x06006FBE RID: 28606 RVA: 0x0021E1FC File Offset: 0x0021E1FC
		private void SerializePrimitive(JsonWriter writer, object value, JsonPrimitiveContract contract, [Nullable(2)] JsonProperty member, [Nullable(2)] JsonContainerContract containerContract, [Nullable(2)] JsonProperty containerProperty)
		{
			if (contract.TypeCode == PrimitiveTypeCode.Bytes && this.ShouldWriteType(TypeNameHandling.Objects, contract, member, containerContract, containerProperty))
			{
				writer.WriteStartObject();
				this.WriteTypeProperty(writer, contract.CreatedType);
				writer.WritePropertyName("$value", false);
				JsonWriter.WriteValue(writer, contract.TypeCode, value);
				writer.WriteEndObject();
				return;
			}
			JsonWriter.WriteValue(writer, contract.TypeCode, value);
		}

		// Token: 0x06006FBF RID: 28607 RVA: 0x0021E270 File Offset: 0x0021E270
		[NullableContext(2)]
		private void SerializeValue([Nullable(1)] JsonWriter writer, object value, JsonContract valueContract, JsonProperty member, JsonContainerContract containerContract, JsonProperty containerProperty)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			JsonConverter jsonConverter;
			if ((jsonConverter = ((member != null) ? member.Converter : null)) == null && (jsonConverter = ((containerProperty != null) ? containerProperty.ItemConverter : null)) == null && (jsonConverter = ((containerContract != null) ? containerContract.ItemConverter : null)) == null && (jsonConverter = valueContract.Converter) == null)
			{
				jsonConverter = (this.Serializer.GetMatchingConverter(valueContract.UnderlyingType) ?? valueContract.InternalConverter);
			}
			JsonConverter jsonConverter2 = jsonConverter;
			if (jsonConverter2 != null && jsonConverter2.CanWrite)
			{
				this.SerializeConvertable(writer, jsonConverter2, value, valueContract, containerContract, containerProperty);
				return;
			}
			switch (valueContract.ContractType)
			{
			case JsonContractType.Object:
				this.SerializeObject(writer, value, (JsonObjectContract)valueContract, member, containerContract, containerProperty);
				return;
			case JsonContractType.Array:
			{
				JsonArrayContract jsonArrayContract = (JsonArrayContract)valueContract;
				if (!jsonArrayContract.IsMultidimensionalArray)
				{
					this.SerializeList(writer, (IEnumerable)value, jsonArrayContract, member, containerContract, containerProperty);
					return;
				}
				this.SerializeMultidimensionalArray(writer, (Array)value, jsonArrayContract, member, containerContract, containerProperty);
				return;
			}
			case JsonContractType.Primitive:
				this.SerializePrimitive(writer, value, (JsonPrimitiveContract)valueContract, member, containerContract, containerProperty);
				return;
			case JsonContractType.String:
				this.SerializeString(writer, value, (JsonStringContract)valueContract);
				return;
			case JsonContractType.Dictionary:
			{
				JsonDictionaryContract jsonDictionaryContract = (JsonDictionaryContract)valueContract;
				IDictionary dictionary = value as IDictionary;
				IDictionary values;
				if (dictionary == null)
				{
					IDictionary dictionary2 = jsonDictionaryContract.CreateWrapper(value);
					values = dictionary2;
				}
				else
				{
					values = dictionary;
				}
				this.SerializeDictionary(writer, values, jsonDictionaryContract, member, containerContract, containerProperty);
				return;
			}
			case JsonContractType.Dynamic:
				this.SerializeDynamic(writer, (IDynamicMetaObjectProvider)value, (JsonDynamicContract)valueContract, member, containerContract, containerProperty);
				return;
			case JsonContractType.Serializable:
				this.SerializeISerializable(writer, (ISerializable)value, (JsonISerializableContract)valueContract, member, containerContract, containerProperty);
				return;
			case JsonContractType.Linq:
				((JToken)value).WriteTo(writer, this.Serializer.Converters.ToArray<JsonConverter>());
				return;
			default:
				return;
			}
		}

		// Token: 0x06006FC0 RID: 28608 RVA: 0x0021E458 File Offset: 0x0021E458
		[NullableContext(2)]
		private bool? ResolveIsReference([Nullable(1)] JsonContract contract, JsonProperty property, JsonContainerContract collectionContract, JsonProperty containerProperty)
		{
			bool? result = null;
			if (property != null)
			{
				result = property.IsReference;
			}
			if (result == null && containerProperty != null)
			{
				result = containerProperty.ItemIsReference;
			}
			if (result == null && collectionContract != null)
			{
				result = collectionContract.ItemIsReference;
			}
			if (result == null)
			{
				result = contract.IsReference;
			}
			return result;
		}

		// Token: 0x06006FC1 RID: 28609 RVA: 0x0021E4C8 File Offset: 0x0021E4C8
		[NullableContext(2)]
		private bool ShouldWriteReference(object value, JsonProperty property, JsonContract valueContract, JsonContainerContract collectionContract, JsonProperty containerProperty)
		{
			if (value == null)
			{
				return false;
			}
			if (valueContract.ContractType == JsonContractType.Primitive || valueContract.ContractType == JsonContractType.String)
			{
				return false;
			}
			bool? flag = this.ResolveIsReference(valueContract, property, collectionContract, containerProperty);
			if (flag == null)
			{
				if (valueContract.ContractType == JsonContractType.Array)
				{
					flag = new bool?(this.HasFlag(this.Serializer._preserveReferencesHandling, PreserveReferencesHandling.Arrays));
				}
				else
				{
					flag = new bool?(this.HasFlag(this.Serializer._preserveReferencesHandling, PreserveReferencesHandling.Objects));
				}
			}
			return flag.GetValueOrDefault() && this.Serializer.GetReferenceResolver().IsReferenced(this, value);
		}

		// Token: 0x06006FC2 RID: 28610 RVA: 0x0021E578 File Offset: 0x0021E578
		[NullableContext(2)]
		private bool ShouldWriteProperty(object memberValue, JsonObjectContract containerContract, [Nullable(1)] JsonProperty property)
		{
			return (memberValue != null || base.ResolvedNullValueHandling(containerContract, property) != NullValueHandling.Ignore) && (!this.HasFlag(property.DefaultValueHandling.GetValueOrDefault(this.Serializer._defaultValueHandling), DefaultValueHandling.Ignore) || !MiscellaneousUtils.ValueEquals(memberValue, property.GetResolvedDefaultValue()));
		}

		// Token: 0x06006FC3 RID: 28611 RVA: 0x0021E5D8 File Offset: 0x0021E5D8
		[NullableContext(2)]
		private bool CheckForCircularReference([Nullable(1)] JsonWriter writer, object value, JsonProperty property, JsonContract contract, JsonContainerContract containerContract, JsonProperty containerProperty)
		{
			if (value == null)
			{
				return true;
			}
			if (contract.ContractType == JsonContractType.Primitive || contract.ContractType == JsonContractType.String)
			{
				return true;
			}
			ReferenceLoopHandling? referenceLoopHandling = null;
			if (property != null)
			{
				referenceLoopHandling = property.ReferenceLoopHandling;
			}
			if (referenceLoopHandling == null && containerProperty != null)
			{
				referenceLoopHandling = containerProperty.ItemReferenceLoopHandling;
			}
			if (referenceLoopHandling == null && containerContract != null)
			{
				referenceLoopHandling = containerContract.ItemReferenceLoopHandling;
			}
			if ((this.Serializer._equalityComparer != null) ? this._serializeStack.Contains(value, this.Serializer._equalityComparer) : this._serializeStack.Contains(value))
			{
				string text = "Self referencing loop detected";
				if (property != null)
				{
					text += " for property '{0}'".FormatWith(CultureInfo.InvariantCulture, property.PropertyName);
				}
				text += " with type '{0}'.".FormatWith(CultureInfo.InvariantCulture, value.GetType());
				switch (referenceLoopHandling.GetValueOrDefault(this.Serializer._referenceLoopHandling))
				{
				case ReferenceLoopHandling.Error:
					throw JsonSerializationException.Create(null, writer.ContainerPath, text, null);
				case ReferenceLoopHandling.Ignore:
					if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Verbose)
					{
						this.TraceWriter.Trace(TraceLevel.Verbose, JsonPosition.FormatMessage(null, writer.Path, text + ". Skipping serializing self referenced value."), null);
					}
					return false;
				case ReferenceLoopHandling.Serialize:
					if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Verbose)
					{
						this.TraceWriter.Trace(TraceLevel.Verbose, JsonPosition.FormatMessage(null, writer.Path, text + ". Serializing self referenced value."), null);
					}
					return true;
				}
			}
			return true;
		}

		// Token: 0x06006FC4 RID: 28612 RVA: 0x0021E798 File Offset: 0x0021E798
		private void WriteReference(JsonWriter writer, object value)
		{
			string reference = this.GetReference(writer, value);
			if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Info)
			{
				this.TraceWriter.Trace(TraceLevel.Info, JsonPosition.FormatMessage(null, writer.Path, "Writing object reference to Id '{0}' for {1}.".FormatWith(CultureInfo.InvariantCulture, reference, value.GetType())), null);
			}
			writer.WriteStartObject();
			writer.WritePropertyName("$ref", false);
			writer.WriteValue(reference);
			writer.WriteEndObject();
		}

		// Token: 0x06006FC5 RID: 28613 RVA: 0x0021E81C File Offset: 0x0021E81C
		private string GetReference(JsonWriter writer, object value)
		{
			string reference;
			try
			{
				reference = this.Serializer.GetReferenceResolver().GetReference(this, value);
			}
			catch (Exception ex)
			{
				throw JsonSerializationException.Create(null, writer.ContainerPath, "Error writing object reference for '{0}'.".FormatWith(CultureInfo.InvariantCulture, value.GetType()), ex);
			}
			return reference;
		}

		// Token: 0x06006FC6 RID: 28614 RVA: 0x0021E878 File Offset: 0x0021E878
		internal static bool TryConvertToString(object value, Type type, [Nullable(2)] [NotNullWhen(true)] out string s)
		{
			TypeConverter typeConverter;
			if (JsonTypeReflector.CanTypeDescriptorConvertString(type, out typeConverter))
			{
				s = typeConverter.ConvertToInvariantString(value);
				return true;
			}
			Type type2 = value as Type;
			if (type2 != null)
			{
				s = type2.AssemblyQualifiedName;
				return true;
			}
			s = null;
			return false;
		}

		// Token: 0x06006FC7 RID: 28615 RVA: 0x0021E8BC File Offset: 0x0021E8BC
		private void SerializeString(JsonWriter writer, object value, JsonStringContract contract)
		{
			this.OnSerializing(writer, contract, value);
			string value2;
			JsonSerializerInternalWriter.TryConvertToString(value, contract.UnderlyingType, out value2);
			writer.WriteValue(value2);
			this.OnSerialized(writer, contract, value);
		}

		// Token: 0x06006FC8 RID: 28616 RVA: 0x0021E8F8 File Offset: 0x0021E8F8
		private void OnSerializing(JsonWriter writer, JsonContract contract, object value)
		{
			if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Info)
			{
				this.TraceWriter.Trace(TraceLevel.Info, JsonPosition.FormatMessage(null, writer.Path, "Started serializing {0}".FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType)), null);
			}
			contract.InvokeOnSerializing(value, this.Serializer._context);
		}

		// Token: 0x06006FC9 RID: 28617 RVA: 0x0021E968 File Offset: 0x0021E968
		private void OnSerialized(JsonWriter writer, JsonContract contract, object value)
		{
			if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Info)
			{
				this.TraceWriter.Trace(TraceLevel.Info, JsonPosition.FormatMessage(null, writer.Path, "Finished serializing {0}".FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType)), null);
			}
			contract.InvokeOnSerialized(value, this.Serializer._context);
		}

		// Token: 0x06006FCA RID: 28618 RVA: 0x0021E9D8 File Offset: 0x0021E9D8
		private void SerializeObject(JsonWriter writer, object value, JsonObjectContract contract, [Nullable(2)] JsonProperty member, [Nullable(2)] JsonContainerContract collectionContract, [Nullable(2)] JsonProperty containerProperty)
		{
			this.OnSerializing(writer, contract, value);
			this._serializeStack.Add(value);
			this.WriteObjectStart(writer, value, contract, member, collectionContract, containerProperty);
			int top = writer.Top;
			for (int i = 0; i < contract.Properties.Count; i++)
			{
				JsonProperty jsonProperty = contract.Properties[i];
				try
				{
					JsonContract valueContract;
					object value2;
					if (this.CalculatePropertyValues(writer, value, contract, member, jsonProperty, out valueContract, out value2))
					{
						jsonProperty.WritePropertyName(writer);
						this.SerializeValue(writer, value2, valueContract, jsonProperty, contract, member);
					}
				}
				catch (Exception ex)
				{
					if (!base.IsErrorHandled(value, contract, jsonProperty.PropertyName, null, writer.ContainerPath, ex))
					{
						throw;
					}
					this.HandleError(writer, top);
				}
			}
			ExtensionDataGetter extensionDataGetter = contract.ExtensionDataGetter;
			IEnumerable<KeyValuePair<object, object>> enumerable = (extensionDataGetter != null) ? extensionDataGetter(value) : null;
			if (enumerable != null)
			{
				foreach (KeyValuePair<object, object> keyValuePair in enumerable)
				{
					JsonContract contract2 = this.GetContract(keyValuePair.Key);
					JsonContract contractSafe = this.GetContractSafe(keyValuePair.Value);
					bool flag;
					string text = this.GetPropertyName(writer, keyValuePair.Key, contract2, out flag);
					text = ((contract.ExtensionDataNameResolver != null) ? contract.ExtensionDataNameResolver(text) : text);
					if (this.ShouldWriteReference(keyValuePair.Value, null, contractSafe, contract, member))
					{
						writer.WritePropertyName(text);
						this.WriteReference(writer, keyValuePair.Value);
					}
					else if (this.CheckForCircularReference(writer, keyValuePair.Value, null, contractSafe, contract, member))
					{
						writer.WritePropertyName(text);
						this.SerializeValue(writer, keyValuePair.Value, contractSafe, null, contract, member);
					}
				}
			}
			writer.WriteEndObject();
			this._serializeStack.RemoveAt(this._serializeStack.Count - 1);
			this.OnSerialized(writer, contract, value);
		}

		// Token: 0x06006FCB RID: 28619 RVA: 0x0021EBF8 File Offset: 0x0021EBF8
		private bool CalculatePropertyValues(JsonWriter writer, object value, JsonContainerContract contract, [Nullable(2)] JsonProperty member, JsonProperty property, [Nullable(2)] [NotNullWhen(true)] out JsonContract memberContract, [Nullable(2)] [NotNullWhen(true)] out object memberValue)
		{
			if (!property.Ignored && property.Readable && this.ShouldSerialize(writer, property, value) && this.IsSpecified(writer, property, value))
			{
				if (property.PropertyContract == null)
				{
					property.PropertyContract = this.Serializer._contractResolver.ResolveContract(property.PropertyType);
				}
				memberValue = property.ValueProvider.GetValue(value);
				memberContract = (property.PropertyContract.IsSealed ? property.PropertyContract : this.GetContractSafe(memberValue));
				if (this.ShouldWriteProperty(memberValue, contract as JsonObjectContract, property))
				{
					if (this.ShouldWriteReference(memberValue, property, memberContract, contract, member))
					{
						property.WritePropertyName(writer);
						this.WriteReference(writer, memberValue);
						return false;
					}
					if (!this.CheckForCircularReference(writer, memberValue, property, memberContract, contract, member))
					{
						return false;
					}
					if (memberValue == null)
					{
						JsonObjectContract jsonObjectContract = contract as JsonObjectContract;
						Required required = property._required ?? ((jsonObjectContract != null) ? jsonObjectContract.ItemRequired : null).GetValueOrDefault();
						if (required == Required.Always)
						{
							throw JsonSerializationException.Create(null, writer.ContainerPath, "Cannot write a null value for property '{0}'. Property requires a value.".FormatWith(CultureInfo.InvariantCulture, property.PropertyName), null);
						}
						if (required == Required.DisallowNull)
						{
							throw JsonSerializationException.Create(null, writer.ContainerPath, "Cannot write a null value for property '{0}'. Property requires a non-null value.".FormatWith(CultureInfo.InvariantCulture, property.PropertyName), null);
						}
					}
					return true;
				}
			}
			memberContract = null;
			memberValue = null;
			return false;
		}

		// Token: 0x06006FCC RID: 28620 RVA: 0x0021EDAC File Offset: 0x0021EDAC
		private void WriteObjectStart(JsonWriter writer, object value, JsonContract contract, [Nullable(2)] JsonProperty member, [Nullable(2)] JsonContainerContract collectionContract, [Nullable(2)] JsonProperty containerProperty)
		{
			writer.WriteStartObject();
			if ((this.ResolveIsReference(contract, member, collectionContract, containerProperty) ?? this.HasFlag(this.Serializer._preserveReferencesHandling, PreserveReferencesHandling.Objects)) && (member == null || member.Writable || this.HasCreatorParameter(collectionContract, member)))
			{
				this.WriteReferenceIdProperty(writer, contract.UnderlyingType, value);
			}
			if (this.ShouldWriteType(TypeNameHandling.Objects, contract, member, collectionContract, containerProperty))
			{
				this.WriteTypeProperty(writer, contract.UnderlyingType);
			}
		}

		// Token: 0x06006FCD RID: 28621 RVA: 0x0021EE50 File Offset: 0x0021EE50
		private bool HasCreatorParameter([Nullable(2)] JsonContainerContract contract, JsonProperty property)
		{
			JsonObjectContract jsonObjectContract = contract as JsonObjectContract;
			return jsonObjectContract != null && jsonObjectContract.CreatorParameters.Contains(property.PropertyName);
		}

		// Token: 0x06006FCE RID: 28622 RVA: 0x0021EE84 File Offset: 0x0021EE84
		private void WriteReferenceIdProperty(JsonWriter writer, Type type, object value)
		{
			string reference = this.GetReference(writer, value);
			if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Verbose)
			{
				this.TraceWriter.Trace(TraceLevel.Verbose, JsonPosition.FormatMessage(null, writer.Path, "Writing object reference Id '{0}' for {1}.".FormatWith(CultureInfo.InvariantCulture, reference, type)), null);
			}
			writer.WritePropertyName("$id", false);
			writer.WriteValue(reference);
		}

		// Token: 0x06006FCF RID: 28623 RVA: 0x0021EEF8 File Offset: 0x0021EEF8
		private void WriteTypeProperty(JsonWriter writer, Type type)
		{
			string typeName = ReflectionUtils.GetTypeName(type, this.Serializer._typeNameAssemblyFormatHandling, this.Serializer._serializationBinder);
			if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Verbose)
			{
				this.TraceWriter.Trace(TraceLevel.Verbose, JsonPosition.FormatMessage(null, writer.Path, "Writing type name '{0}' for {1}.".FormatWith(CultureInfo.InvariantCulture, typeName, type)), null);
			}
			writer.WritePropertyName("$type", false);
			writer.WriteValue(typeName);
		}

		// Token: 0x06006FD0 RID: 28624 RVA: 0x0021EF80 File Offset: 0x0021EF80
		private bool HasFlag(DefaultValueHandling value, DefaultValueHandling flag)
		{
			return (value & flag) == flag;
		}

		// Token: 0x06006FD1 RID: 28625 RVA: 0x0021EF88 File Offset: 0x0021EF88
		private bool HasFlag(PreserveReferencesHandling value, PreserveReferencesHandling flag)
		{
			return (value & flag) == flag;
		}

		// Token: 0x06006FD2 RID: 28626 RVA: 0x0021EF90 File Offset: 0x0021EF90
		private bool HasFlag(TypeNameHandling value, TypeNameHandling flag)
		{
			return (value & flag) == flag;
		}

		// Token: 0x06006FD3 RID: 28627 RVA: 0x0021EF98 File Offset: 0x0021EF98
		private void SerializeConvertable(JsonWriter writer, JsonConverter converter, object value, JsonContract contract, [Nullable(2)] JsonContainerContract collectionContract, [Nullable(2)] JsonProperty containerProperty)
		{
			if (this.ShouldWriteReference(value, null, contract, collectionContract, containerProperty))
			{
				this.WriteReference(writer, value);
				return;
			}
			if (!this.CheckForCircularReference(writer, value, null, contract, collectionContract, containerProperty))
			{
				return;
			}
			this._serializeStack.Add(value);
			if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Info)
			{
				this.TraceWriter.Trace(TraceLevel.Info, JsonPosition.FormatMessage(null, writer.Path, "Started serializing {0} with converter {1}.".FormatWith(CultureInfo.InvariantCulture, value.GetType(), converter.GetType())), null);
			}
			converter.WriteJson(writer, value, this.GetInternalSerializer());
			if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Info)
			{
				this.TraceWriter.Trace(TraceLevel.Info, JsonPosition.FormatMessage(null, writer.Path, "Finished serializing {0} with converter {1}.".FormatWith(CultureInfo.InvariantCulture, value.GetType(), converter.GetType())), null);
			}
			this._serializeStack.RemoveAt(this._serializeStack.Count - 1);
		}

		// Token: 0x06006FD4 RID: 28628 RVA: 0x0021F0AC File Offset: 0x0021F0AC
		private void SerializeList(JsonWriter writer, IEnumerable values, JsonArrayContract contract, [Nullable(2)] JsonProperty member, [Nullable(2)] JsonContainerContract collectionContract, [Nullable(2)] JsonProperty containerProperty)
		{
			IWrappedCollection wrappedCollection = values as IWrappedCollection;
			object obj = (wrappedCollection != null) ? wrappedCollection.UnderlyingCollection : values;
			this.OnSerializing(writer, contract, obj);
			this._serializeStack.Add(obj);
			bool flag = this.WriteStartArray(writer, obj, contract, member, collectionContract, containerProperty);
			writer.WriteStartArray();
			int top = writer.Top;
			int num = 0;
			foreach (object value in values)
			{
				try
				{
					JsonContract jsonContract = contract.FinalItemContract ?? this.GetContractSafe(value);
					if (this.ShouldWriteReference(value, null, jsonContract, contract, member))
					{
						this.WriteReference(writer, value);
					}
					else if (this.CheckForCircularReference(writer, value, null, jsonContract, contract, member))
					{
						this.SerializeValue(writer, value, jsonContract, null, contract, member);
					}
				}
				catch (Exception ex)
				{
					if (!base.IsErrorHandled(obj, contract, num, null, writer.ContainerPath, ex))
					{
						throw;
					}
					this.HandleError(writer, top);
				}
				finally
				{
					num++;
				}
			}
			writer.WriteEndArray();
			if (flag)
			{
				writer.WriteEndObject();
			}
			this._serializeStack.RemoveAt(this._serializeStack.Count - 1);
			this.OnSerialized(writer, contract, obj);
		}

		// Token: 0x06006FD5 RID: 28629 RVA: 0x0021F23C File Offset: 0x0021F23C
		private void SerializeMultidimensionalArray(JsonWriter writer, Array values, JsonArrayContract contract, [Nullable(2)] JsonProperty member, [Nullable(2)] JsonContainerContract collectionContract, [Nullable(2)] JsonProperty containerProperty)
		{
			this.OnSerializing(writer, contract, values);
			this._serializeStack.Add(values);
			bool flag = this.WriteStartArray(writer, values, contract, member, collectionContract, containerProperty);
			this.SerializeMultidimensionalArray(writer, values, contract, member, writer.Top, CollectionUtils.ArrayEmpty<int>());
			if (flag)
			{
				writer.WriteEndObject();
			}
			this._serializeStack.RemoveAt(this._serializeStack.Count - 1);
			this.OnSerialized(writer, contract, values);
		}

		// Token: 0x06006FD6 RID: 28630 RVA: 0x0021F2B4 File Offset: 0x0021F2B4
		private void SerializeMultidimensionalArray(JsonWriter writer, Array values, JsonArrayContract contract, [Nullable(2)] JsonProperty member, int initialDepth, int[] indices)
		{
			int num = indices.Length;
			int[] array = new int[num + 1];
			for (int i = 0; i < num; i++)
			{
				array[i] = indices[i];
			}
			writer.WriteStartArray();
			int j = values.GetLowerBound(num);
			while (j <= values.GetUpperBound(num))
			{
				array[num] = j;
				if (array.Length == values.Rank)
				{
					object value = values.GetValue(array);
					try
					{
						JsonContract jsonContract = contract.FinalItemContract ?? this.GetContractSafe(value);
						if (this.ShouldWriteReference(value, null, jsonContract, contract, member))
						{
							this.WriteReference(writer, value);
						}
						else if (this.CheckForCircularReference(writer, value, null, jsonContract, contract, member))
						{
							this.SerializeValue(writer, value, jsonContract, null, contract, member);
						}
						goto IL_F9;
					}
					catch (Exception ex)
					{
						if (base.IsErrorHandled(values, contract, j, null, writer.ContainerPath, ex))
						{
							this.HandleError(writer, initialDepth + 1);
							goto IL_F9;
						}
						throw;
					}
					goto IL_E9;
				}
				goto IL_E9;
				IL_F9:
				j++;
				continue;
				IL_E9:
				this.SerializeMultidimensionalArray(writer, values, contract, member, initialDepth + 1, array);
				goto IL_F9;
			}
			writer.WriteEndArray();
		}

		// Token: 0x06006FD7 RID: 28631 RVA: 0x0021F3E4 File Offset: 0x0021F3E4
		private bool WriteStartArray(JsonWriter writer, object values, JsonArrayContract contract, [Nullable(2)] JsonProperty member, [Nullable(2)] JsonContainerContract containerContract, [Nullable(2)] JsonProperty containerProperty)
		{
			bool flag = this.ResolveIsReference(contract, member, containerContract, containerProperty) ?? this.HasFlag(this.Serializer._preserveReferencesHandling, PreserveReferencesHandling.Arrays);
			flag = (flag && (member == null || member.Writable || this.HasCreatorParameter(containerContract, member)));
			bool flag2 = this.ShouldWriteType(TypeNameHandling.Arrays, contract, member, containerContract, containerProperty);
			bool flag3 = flag || flag2;
			if (flag3)
			{
				writer.WriteStartObject();
				if (flag)
				{
					this.WriteReferenceIdProperty(writer, contract.UnderlyingType, values);
				}
				if (flag2)
				{
					this.WriteTypeProperty(writer, values.GetType());
				}
				writer.WritePropertyName("$values", false);
			}
			if (contract.ItemContract == null)
			{
				contract.ItemContract = this.Serializer._contractResolver.ResolveContract(contract.CollectionItemType ?? typeof(object));
			}
			return flag3;
		}

		// Token: 0x06006FD8 RID: 28632 RVA: 0x0021F4E8 File Offset: 0x0021F4E8
		[SecuritySafeCritical]
		private void SerializeISerializable(JsonWriter writer, ISerializable value, JsonISerializableContract contract, [Nullable(2)] JsonProperty member, [Nullable(2)] JsonContainerContract collectionContract, [Nullable(2)] JsonProperty containerProperty)
		{
			if (!JsonTypeReflector.FullyTrusted)
			{
				string text = "Type '{0}' implements ISerializable but cannot be serialized using the ISerializable interface because the current application is not fully trusted and ISerializable can expose secure data." + Environment.NewLine + "To fix this error either change the environment to be fully trusted, change the application to not deserialize the type, add JsonObjectAttribute to the type or change the JsonSerializer setting ContractResolver to use a new DefaultContractResolver with IgnoreSerializableInterface set to true." + Environment.NewLine;
				text = text.FormatWith(CultureInfo.InvariantCulture, value.GetType());
				throw JsonSerializationException.Create(null, writer.ContainerPath, text, null);
			}
			this.OnSerializing(writer, contract, value);
			this._serializeStack.Add(value);
			this.WriteObjectStart(writer, value, contract, member, collectionContract, containerProperty);
			SerializationInfo serializationInfo = new SerializationInfo(contract.UnderlyingType, new FormatterConverter());
			value.GetObjectData(serializationInfo, this.Serializer._context);
			foreach (SerializationEntry serializationEntry in serializationInfo)
			{
				JsonContract contractSafe = this.GetContractSafe(serializationEntry.Value);
				if (this.ShouldWriteReference(serializationEntry.Value, null, contractSafe, contract, member))
				{
					writer.WritePropertyName(serializationEntry.Name);
					this.WriteReference(writer, serializationEntry.Value);
				}
				else if (this.CheckForCircularReference(writer, serializationEntry.Value, null, contractSafe, contract, member))
				{
					writer.WritePropertyName(serializationEntry.Name);
					this.SerializeValue(writer, serializationEntry.Value, contractSafe, null, contract, member);
				}
			}
			writer.WriteEndObject();
			this._serializeStack.RemoveAt(this._serializeStack.Count - 1);
			this.OnSerialized(writer, contract, value);
		}

		// Token: 0x06006FD9 RID: 28633 RVA: 0x0021F64C File Offset: 0x0021F64C
		private void SerializeDynamic(JsonWriter writer, IDynamicMetaObjectProvider value, JsonDynamicContract contract, [Nullable(2)] JsonProperty member, [Nullable(2)] JsonContainerContract collectionContract, [Nullable(2)] JsonProperty containerProperty)
		{
			this.OnSerializing(writer, contract, value);
			this._serializeStack.Add(value);
			this.WriteObjectStart(writer, value, contract, member, collectionContract, containerProperty);
			int top = writer.Top;
			for (int i = 0; i < contract.Properties.Count; i++)
			{
				JsonProperty jsonProperty = contract.Properties[i];
				if (jsonProperty.HasMemberAttribute)
				{
					try
					{
						JsonContract valueContract;
						object value2;
						if (this.CalculatePropertyValues(writer, value, contract, member, jsonProperty, out valueContract, out value2))
						{
							jsonProperty.WritePropertyName(writer);
							this.SerializeValue(writer, value2, valueContract, jsonProperty, contract, member);
						}
					}
					catch (Exception ex)
					{
						if (!base.IsErrorHandled(value, contract, jsonProperty.PropertyName, null, writer.ContainerPath, ex))
						{
							throw;
						}
						this.HandleError(writer, top);
					}
				}
			}
			foreach (string text in value.GetDynamicMemberNames())
			{
				object obj;
				if (contract.TryGetMember(value, text, out obj))
				{
					try
					{
						JsonContract contractSafe = this.GetContractSafe(obj);
						if (this.ShouldWriteDynamicProperty(obj))
						{
							if (this.CheckForCircularReference(writer, obj, null, contractSafe, contract, member))
							{
								string name = (contract.PropertyNameResolver != null) ? contract.PropertyNameResolver(text) : text;
								writer.WritePropertyName(name);
								this.SerializeValue(writer, obj, contractSafe, null, contract, member);
							}
						}
					}
					catch (Exception ex2)
					{
						if (!base.IsErrorHandled(value, contract, text, null, writer.ContainerPath, ex2))
						{
							throw;
						}
						this.HandleError(writer, top);
					}
				}
			}
			writer.WriteEndObject();
			this._serializeStack.RemoveAt(this._serializeStack.Count - 1);
			this.OnSerialized(writer, contract, value);
		}

		// Token: 0x06006FDA RID: 28634 RVA: 0x0021F854 File Offset: 0x0021F854
		[NullableContext(2)]
		private bool ShouldWriteDynamicProperty(object memberValue)
		{
			return (this.Serializer._nullValueHandling != NullValueHandling.Ignore || memberValue != null) && (!this.HasFlag(this.Serializer._defaultValueHandling, DefaultValueHandling.Ignore) || (memberValue != null && !MiscellaneousUtils.ValueEquals(memberValue, ReflectionUtils.GetDefaultValue(memberValue.GetType()))));
		}

		// Token: 0x06006FDB RID: 28635 RVA: 0x0021F8B4 File Offset: 0x0021F8B4
		[NullableContext(2)]
		private bool ShouldWriteType(TypeNameHandling typeNameHandlingFlag, [Nullable(1)] JsonContract contract, JsonProperty member, JsonContainerContract containerContract, JsonProperty containerProperty)
		{
			TypeNameHandling value = ((member != null) ? member.TypeNameHandling : null) ?? (((containerProperty != null) ? containerProperty.ItemTypeNameHandling : null) ?? (((containerContract != null) ? containerContract.ItemTypeNameHandling : null) ?? this.Serializer._typeNameHandling));
			if (this.HasFlag(value, typeNameHandlingFlag))
			{
				return true;
			}
			if (this.HasFlag(value, TypeNameHandling.Auto))
			{
				if (member != null)
				{
					if (contract.NonNullableUnderlyingType != member.PropertyContract.CreatedType)
					{
						return true;
					}
				}
				else if (containerContract != null)
				{
					if (containerContract.ItemContract == null || contract.NonNullableUnderlyingType != containerContract.ItemContract.CreatedType)
					{
						return true;
					}
				}
				else if (this._rootType != null && this._serializeStack.Count == this._rootLevel)
				{
					JsonContract jsonContract = this.Serializer._contractResolver.ResolveContract(this._rootType);
					if (contract.NonNullableUnderlyingType != jsonContract.CreatedType)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06006FDC RID: 28636 RVA: 0x0021FA3C File Offset: 0x0021FA3C
		private void SerializeDictionary(JsonWriter writer, IDictionary values, JsonDictionaryContract contract, [Nullable(2)] JsonProperty member, [Nullable(2)] JsonContainerContract collectionContract, [Nullable(2)] JsonProperty containerProperty)
		{
			IWrappedDictionary wrappedDictionary = values as IWrappedDictionary;
			object obj = (wrappedDictionary != null) ? wrappedDictionary.UnderlyingDictionary : values;
			this.OnSerializing(writer, contract, obj);
			this._serializeStack.Add(obj);
			this.WriteObjectStart(writer, obj, contract, member, collectionContract, containerProperty);
			if (contract.ItemContract == null)
			{
				contract.ItemContract = this.Serializer._contractResolver.ResolveContract(contract.DictionaryValueType ?? typeof(object));
			}
			if (contract.KeyContract == null)
			{
				contract.KeyContract = this.Serializer._contractResolver.ResolveContract(contract.DictionaryKeyType ?? typeof(object));
			}
			int top = writer.Top;
			using (IDictionaryEnumerator enumerator = values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					DictionaryEntry entry = enumerator.Entry;
					bool escape;
					string text = this.GetPropertyName(writer, entry.Key, contract.KeyContract, out escape);
					text = ((contract.DictionaryKeyResolver != null) ? contract.DictionaryKeyResolver(text) : text);
					try
					{
						object value = entry.Value;
						JsonContract jsonContract = contract.FinalItemContract ?? this.GetContractSafe(value);
						if (this.ShouldWriteReference(value, null, jsonContract, contract, member))
						{
							writer.WritePropertyName(text, escape);
							this.WriteReference(writer, value);
						}
						else if (this.CheckForCircularReference(writer, value, null, jsonContract, contract, member))
						{
							writer.WritePropertyName(text, escape);
							this.SerializeValue(writer, value, jsonContract, null, contract, member);
						}
					}
					catch (Exception ex)
					{
						if (!base.IsErrorHandled(obj, contract, text, null, writer.ContainerPath, ex))
						{
							throw;
						}
						this.HandleError(writer, top);
					}
				}
			}
			writer.WriteEndObject();
			this._serializeStack.RemoveAt(this._serializeStack.Count - 1);
			this.OnSerialized(writer, contract, obj);
		}

		// Token: 0x06006FDD RID: 28637 RVA: 0x0021FC80 File Offset: 0x0021FC80
		private string GetPropertyName(JsonWriter writer, object name, JsonContract contract, out bool escape)
		{
			if (contract.ContractType == JsonContractType.Primitive)
			{
				JsonPrimitiveContract jsonPrimitiveContract = (JsonPrimitiveContract)contract;
				switch (jsonPrimitiveContract.TypeCode)
				{
				case PrimitiveTypeCode.Single:
				case PrimitiveTypeCode.SingleNullable:
				{
					float num = (float)name;
					escape = false;
					return num.ToString("R", CultureInfo.InvariantCulture);
				}
				case PrimitiveTypeCode.Double:
				case PrimitiveTypeCode.DoubleNullable:
				{
					double num2 = (double)name;
					escape = false;
					return num2.ToString("R", CultureInfo.InvariantCulture);
				}
				case PrimitiveTypeCode.DateTime:
				case PrimitiveTypeCode.DateTimeNullable:
				{
					DateTime value = DateTimeUtils.EnsureDateTime((DateTime)name, writer.DateTimeZoneHandling);
					escape = false;
					StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
					DateTimeUtils.WriteDateTimeString(stringWriter, value, writer.DateFormatHandling, writer.DateFormatString, writer.Culture);
					return stringWriter.ToString();
				}
				case PrimitiveTypeCode.DateTimeOffset:
				case PrimitiveTypeCode.DateTimeOffsetNullable:
				{
					escape = false;
					StringWriter stringWriter2 = new StringWriter(CultureInfo.InvariantCulture);
					DateTimeUtils.WriteDateTimeOffsetString(stringWriter2, (DateTimeOffset)name, writer.DateFormatHandling, writer.DateFormatString, writer.Culture);
					return stringWriter2.ToString();
				}
				default:
				{
					escape = true;
					string result;
					if (jsonPrimitiveContract.IsEnum && EnumUtils.TryToString(jsonPrimitiveContract.NonNullableUnderlyingType, name, null, out result))
					{
						return result;
					}
					return Convert.ToString(name, CultureInfo.InvariantCulture);
				}
				}
			}
			else
			{
				string result2;
				if (JsonSerializerInternalWriter.TryConvertToString(name, name.GetType(), out result2))
				{
					escape = true;
					return result2;
				}
				escape = true;
				return name.ToString();
			}
		}

		// Token: 0x06006FDE RID: 28638 RVA: 0x0021FDDC File Offset: 0x0021FDDC
		private void HandleError(JsonWriter writer, int initialDepth)
		{
			base.ClearErrorContext();
			if (writer.WriteState == WriteState.Property)
			{
				writer.WriteNull();
			}
			while (writer.Top > initialDepth)
			{
				writer.WriteEnd();
			}
		}

		// Token: 0x06006FDF RID: 28639 RVA: 0x0021FE1C File Offset: 0x0021FE1C
		private bool ShouldSerialize(JsonWriter writer, JsonProperty property, object target)
		{
			if (property.ShouldSerialize == null)
			{
				return true;
			}
			bool flag = property.ShouldSerialize(target);
			if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Verbose)
			{
				this.TraceWriter.Trace(TraceLevel.Verbose, JsonPosition.FormatMessage(null, writer.Path, "ShouldSerialize result for property '{0}' on {1}: {2}".FormatWith(CultureInfo.InvariantCulture, property.PropertyName, property.DeclaringType, flag)), null);
			}
			return flag;
		}

		// Token: 0x06006FE0 RID: 28640 RVA: 0x0021FEA0 File Offset: 0x0021FEA0
		private bool IsSpecified(JsonWriter writer, JsonProperty property, object target)
		{
			if (property.GetIsSpecified == null)
			{
				return true;
			}
			bool flag = property.GetIsSpecified(target);
			if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Verbose)
			{
				this.TraceWriter.Trace(TraceLevel.Verbose, JsonPosition.FormatMessage(null, writer.Path, "IsSpecified result for property '{0}' on {1}: {2}".FormatWith(CultureInfo.InvariantCulture, property.PropertyName, property.DeclaringType, flag)), null);
			}
			return flag;
		}

		// Token: 0x040037AE RID: 14254
		[Nullable(2)]
		private Type _rootType;

		// Token: 0x040037AF RID: 14255
		private int _rootLevel;

		// Token: 0x040037B0 RID: 14256
		private readonly List<object> _serializeStack = new List<object>();
	}
}
