using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace ProtoBuf.Meta
{
	// Token: 0x02000C7D RID: 3197
	[ComVisible(true)]
	public abstract class TypeModel : IProtoInput<Stream>, IProtoInput<ArraySegment<byte>>, IProtoInput<byte[]>, IProtoOutput<Stream>
	{
		// Token: 0x06007F85 RID: 32645 RVA: 0x0025ADB4 File Offset: 0x0025ADB4
		protected internal virtual bool SerializeDateTimeKind()
		{
			return false;
		}

		// Token: 0x06007F86 RID: 32646 RVA: 0x0025ADB8 File Offset: 0x0025ADB8
		protected internal Type MapType(Type type)
		{
			return this.MapType(type, true);
		}

		// Token: 0x06007F87 RID: 32647 RVA: 0x0025ADC4 File Offset: 0x0025ADC4
		protected internal virtual Type MapType(Type type, bool demand)
		{
			return type;
		}

		// Token: 0x06007F88 RID: 32648 RVA: 0x0025ADC8 File Offset: 0x0025ADC8
		private WireType GetWireType(ProtoTypeCode code, DataFormat format, ref Type type, out int modelKey)
		{
			modelKey = -1;
			if (Helpers.IsEnum(type))
			{
				modelKey = this.GetKey(ref type);
				return WireType.Variant;
			}
			switch (code)
			{
			case ProtoTypeCode.Boolean:
			case ProtoTypeCode.Char:
			case ProtoTypeCode.SByte:
			case ProtoTypeCode.Byte:
			case ProtoTypeCode.Int16:
			case ProtoTypeCode.UInt16:
			case ProtoTypeCode.Int32:
			case ProtoTypeCode.UInt32:
				if (format != DataFormat.FixedSize)
				{
					return WireType.Variant;
				}
				return WireType.Fixed32;
			case ProtoTypeCode.Int64:
			case ProtoTypeCode.UInt64:
				if (format != DataFormat.FixedSize)
				{
					return WireType.Variant;
				}
				return WireType.Fixed64;
			case ProtoTypeCode.Single:
				return WireType.Fixed32;
			case ProtoTypeCode.Double:
				return WireType.Fixed64;
			case ProtoTypeCode.Decimal:
			case ProtoTypeCode.DateTime:
			case ProtoTypeCode.String:
				break;
			case (ProtoTypeCode)17:
				goto IL_8F;
			default:
				if (code - ProtoTypeCode.TimeSpan > 3)
				{
					goto IL_8F;
				}
				break;
			}
			return WireType.String;
			IL_8F:
			if ((modelKey = this.GetKey(ref type)) >= 0)
			{
				return WireType.String;
			}
			return WireType.None;
		}

		// Token: 0x06007F89 RID: 32649 RVA: 0x0025AE80 File Offset: 0x0025AE80
		internal bool TrySerializeAuxiliaryType(ProtoWriter writer, Type type, DataFormat format, int tag, object value, bool isInsideList, object parentList)
		{
			if (type == null)
			{
				type = value.GetType();
			}
			ProtoTypeCode typeCode = Helpers.GetTypeCode(type);
			int num;
			WireType wireType = this.GetWireType(typeCode, format, ref type, out num);
			if (num >= 0)
			{
				if (Helpers.IsEnum(type))
				{
					this.Serialize(num, value, writer);
					return true;
				}
				ProtoWriter.WriteFieldHeader(tag, wireType, writer);
				if (wireType == WireType.None)
				{
					throw ProtoWriter.CreateException(writer);
				}
				if (wireType - WireType.String > 1)
				{
					this.Serialize(num, value, writer);
					return true;
				}
				SubItemToken token = ProtoWriter.StartSubItem(value, writer);
				this.Serialize(num, value, writer);
				ProtoWriter.EndSubItem(token, writer);
				return true;
			}
			else
			{
				if (wireType != WireType.None)
				{
					ProtoWriter.WriteFieldHeader(tag, wireType, writer);
				}
				switch (typeCode)
				{
				case ProtoTypeCode.Boolean:
					ProtoWriter.WriteBoolean((bool)value, writer);
					return true;
				case ProtoTypeCode.Char:
					ProtoWriter.WriteUInt16((ushort)((char)value), writer);
					return true;
				case ProtoTypeCode.SByte:
					ProtoWriter.WriteSByte((sbyte)value, writer);
					return true;
				case ProtoTypeCode.Byte:
					ProtoWriter.WriteByte((byte)value, writer);
					return true;
				case ProtoTypeCode.Int16:
					ProtoWriter.WriteInt16((short)value, writer);
					return true;
				case ProtoTypeCode.UInt16:
					ProtoWriter.WriteUInt16((ushort)value, writer);
					return true;
				case ProtoTypeCode.Int32:
					ProtoWriter.WriteInt32((int)value, writer);
					return true;
				case ProtoTypeCode.UInt32:
					ProtoWriter.WriteUInt32((uint)value, writer);
					return true;
				case ProtoTypeCode.Int64:
					ProtoWriter.WriteInt64((long)value, writer);
					return true;
				case ProtoTypeCode.UInt64:
					ProtoWriter.WriteUInt64((ulong)value, writer);
					return true;
				case ProtoTypeCode.Single:
					ProtoWriter.WriteSingle((float)value, writer);
					return true;
				case ProtoTypeCode.Double:
					ProtoWriter.WriteDouble((double)value, writer);
					return true;
				case ProtoTypeCode.Decimal:
					BclHelpers.WriteDecimal((decimal)value, writer);
					return true;
				case ProtoTypeCode.DateTime:
					if (this.SerializeDateTimeKind())
					{
						BclHelpers.WriteDateTimeWithKind((DateTime)value, writer);
					}
					else
					{
						BclHelpers.WriteDateTime((DateTime)value, writer);
					}
					return true;
				case (ProtoTypeCode)17:
					break;
				case ProtoTypeCode.String:
					ProtoWriter.WriteString((string)value, writer);
					return true;
				default:
					switch (typeCode)
					{
					case ProtoTypeCode.TimeSpan:
						BclHelpers.WriteTimeSpan((TimeSpan)value, writer);
						return true;
					case ProtoTypeCode.ByteArray:
						ProtoWriter.WriteBytes((byte[])value, writer);
						return true;
					case ProtoTypeCode.Guid:
						BclHelpers.WriteGuid((Guid)value, writer);
						return true;
					case ProtoTypeCode.Uri:
						ProtoWriter.WriteString(((Uri)value).OriginalString, writer);
						return true;
					}
					break;
				}
				IEnumerable enumerable = value as IEnumerable;
				if (enumerable == null)
				{
					return false;
				}
				if (isInsideList)
				{
					throw TypeModel.CreateNestedListsNotSupported((parentList != null) ? parentList.GetType() : null);
				}
				foreach (object obj in enumerable)
				{
					if (obj == null)
					{
						throw new NullReferenceException();
					}
					if (!this.TrySerializeAuxiliaryType(writer, null, format, tag, obj, true, enumerable))
					{
						TypeModel.ThrowUnexpectedType(obj.GetType());
					}
				}
				return true;
			}
		}

		// Token: 0x06007F8A RID: 32650 RVA: 0x0025B188 File Offset: 0x0025B188
		private void SerializeCore(ProtoWriter writer, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			int key = this.GetKey(ref type);
			if (key >= 0)
			{
				this.Serialize(key, value, writer);
				return;
			}
			if (!this.TrySerializeAuxiliaryType(writer, type, DataFormat.Default, 1, value, false, null))
			{
				TypeModel.ThrowUnexpectedType(type);
			}
		}

		// Token: 0x06007F8B RID: 32651 RVA: 0x0025B1E4 File Offset: 0x0025B1E4
		public void Serialize(Stream dest, object value)
		{
			this.Serialize(dest, value, null);
		}

		// Token: 0x06007F8C RID: 32652 RVA: 0x0025B1F0 File Offset: 0x0025B1F0
		public void Serialize(Stream dest, object value, SerializationContext context)
		{
			using (ProtoWriter protoWriter = ProtoWriter.Create(dest, this, context))
			{
				protoWriter.SetRootObject(value);
				this.SerializeCore(protoWriter, value);
				protoWriter.Close();
			}
		}

		// Token: 0x06007F8D RID: 32653 RVA: 0x0025B240 File Offset: 0x0025B240
		public void Serialize(ProtoWriter dest, object value)
		{
			if (dest == null)
			{
				throw new ArgumentNullException("dest");
			}
			dest.CheckDepthFlushlock();
			dest.SetRootObject(value);
			this.SerializeCore(dest, value);
			dest.CheckDepthFlushlock();
			ProtoWriter.Flush(dest);
		}

		// Token: 0x06007F8E RID: 32654 RVA: 0x0025B284 File Offset: 0x0025B284
		public object DeserializeWithLengthPrefix(Stream source, object value, Type type, PrefixStyle style, int fieldNumber)
		{
			long num;
			return this.DeserializeWithLengthPrefix(source, value, type, style, fieldNumber, null, out num);
		}

		// Token: 0x06007F8F RID: 32655 RVA: 0x0025B2A8 File Offset: 0x0025B2A8
		public object DeserializeWithLengthPrefix(Stream source, object value, Type type, PrefixStyle style, int expectedField, Serializer.TypeResolver resolver)
		{
			long num;
			return this.DeserializeWithLengthPrefix(source, value, type, style, expectedField, resolver, out num);
		}

		// Token: 0x06007F90 RID: 32656 RVA: 0x0025B2CC File Offset: 0x0025B2CC
		public object DeserializeWithLengthPrefix(Stream source, object value, Type type, PrefixStyle style, int expectedField, Serializer.TypeResolver resolver, out int bytesRead)
		{
			long num;
			bool flag;
			object result = this.DeserializeWithLengthPrefix(source, value, type, style, expectedField, resolver, out num, out flag, null);
			bytesRead = checked((int)num);
			return result;
		}

		// Token: 0x06007F91 RID: 32657 RVA: 0x0025B2F8 File Offset: 0x0025B2F8
		public object DeserializeWithLengthPrefix(Stream source, object value, Type type, PrefixStyle style, int expectedField, Serializer.TypeResolver resolver, out long bytesRead)
		{
			bool flag;
			return this.DeserializeWithLengthPrefix(source, value, type, style, expectedField, resolver, out bytesRead, out flag, null);
		}

		// Token: 0x06007F92 RID: 32658 RVA: 0x0025B320 File Offset: 0x0025B320
		private object DeserializeWithLengthPrefix(Stream source, object value, Type type, PrefixStyle style, int expectedField, Serializer.TypeResolver resolver, out long bytesRead, out bool haveObject, SerializationContext context)
		{
			haveObject = false;
			bytesRead = 0L;
			if (type == null && (style != PrefixStyle.Base128 || resolver == null))
			{
				throw new InvalidOperationException("A type must be provided unless base-128 prefixing is being used in combination with a resolver");
			}
			for (;;)
			{
				bool flag = expectedField > 0 || resolver != null;
				int num2;
				int num3;
				long num = ProtoReader.ReadLongLengthPrefix(source, flag, style, out num2, out num3);
				if (num3 == 0)
				{
					break;
				}
				bytesRead += (long)num3;
				if (num < 0L)
				{
					return value;
				}
				bool flag2;
				if (style == PrefixStyle.Base128)
				{
					if (flag && expectedField == 0 && type == null && resolver != null)
					{
						type = resolver(num2);
						flag2 = (type == null);
					}
					else
					{
						flag2 = (expectedField != num2);
					}
				}
				else
				{
					flag2 = false;
				}
				if (flag2)
				{
					if (num == 9223372036854775807L)
					{
						goto Block_12;
					}
					ProtoReader.Seek(source, num, null);
					bytesRead += num;
				}
				if (!flag2)
				{
					goto Block_13;
				}
			}
			return value;
			Block_12:
			throw new InvalidOperationException();
			Block_13:
			ProtoReader protoReader = null;
			object result;
			try
			{
				long num;
				protoReader = ProtoReader.Create(source, this, context, num);
				int key = this.GetKey(ref type);
				if (key >= 0 && !Helpers.IsEnum(type))
				{
					value = this.Deserialize(key, value, protoReader);
				}
				else if (!this.TryDeserializeAuxiliaryType(protoReader, DataFormat.Default, 1, type, ref value, true, false, true, false, null) && num != 0L)
				{
					TypeModel.ThrowUnexpectedType(type);
				}
				bytesRead += protoReader.LongPosition;
				haveObject = true;
				result = value;
			}
			finally
			{
				ProtoReader.Recycle(protoReader);
			}
			return result;
		}

		// Token: 0x06007F93 RID: 32659 RVA: 0x0025B4B4 File Offset: 0x0025B4B4
		public IEnumerable DeserializeItems(Stream source, Type type, PrefixStyle style, int expectedField, Serializer.TypeResolver resolver)
		{
			return this.DeserializeItems(source, type, style, expectedField, resolver, null);
		}

		// Token: 0x06007F94 RID: 32660 RVA: 0x0025B4C4 File Offset: 0x0025B4C4
		public IEnumerable DeserializeItems(Stream source, Type type, PrefixStyle style, int expectedField, Serializer.TypeResolver resolver, SerializationContext context)
		{
			return new TypeModel.DeserializeItemsIterator(this, source, type, style, expectedField, resolver, context);
		}

		// Token: 0x06007F95 RID: 32661 RVA: 0x0025B4D8 File Offset: 0x0025B4D8
		public IEnumerable<T> DeserializeItems<T>(Stream source, PrefixStyle style, int expectedField)
		{
			return this.DeserializeItems<T>(source, style, expectedField, null);
		}

		// Token: 0x06007F96 RID: 32662 RVA: 0x0025B4E4 File Offset: 0x0025B4E4
		public IEnumerable<T> DeserializeItems<T>(Stream source, PrefixStyle style, int expectedField, SerializationContext context)
		{
			return new TypeModel.DeserializeItemsIterator<T>(this, source, style, expectedField, context);
		}

		// Token: 0x06007F97 RID: 32663 RVA: 0x0025B4F4 File Offset: 0x0025B4F4
		public void SerializeWithLengthPrefix(Stream dest, object value, Type type, PrefixStyle style, int fieldNumber)
		{
			this.SerializeWithLengthPrefix(dest, value, type, style, fieldNumber, null);
		}

		// Token: 0x06007F98 RID: 32664 RVA: 0x0025B504 File Offset: 0x0025B504
		public void SerializeWithLengthPrefix(Stream dest, object value, Type type, PrefixStyle style, int fieldNumber, SerializationContext context)
		{
			if (type == null)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				type = this.MapType(value.GetType());
			}
			int key = this.GetKey(ref type);
			using (ProtoWriter protoWriter = ProtoWriter.Create(dest, this, context))
			{
				if (style != PrefixStyle.None)
				{
					if (style - PrefixStyle.Base128 > 2)
					{
						throw new ArgumentOutOfRangeException("style");
					}
					ProtoWriter.WriteObject(value, key, protoWriter, style, fieldNumber);
				}
				else
				{
					this.Serialize(key, value, protoWriter);
				}
				protoWriter.Close();
			}
		}

		// Token: 0x06007F99 RID: 32665 RVA: 0x0025B5B8 File Offset: 0x0025B5B8
		public object Deserialize(Stream source, object value, Type type)
		{
			return this.Deserialize(source, value, type, null);
		}

		// Token: 0x06007F9A RID: 32666 RVA: 0x0025B5C4 File Offset: 0x0025B5C4
		public object Deserialize(Stream source, object value, Type type, SerializationContext context)
		{
			bool noAutoCreate = this.PrepareDeserialize(value, ref type);
			ProtoReader protoReader = null;
			object result;
			try
			{
				protoReader = ProtoReader.Create(source, this, context, -1L);
				if (value != null)
				{
					protoReader.SetRootObject(value);
				}
				object obj = this.DeserializeCore(protoReader, type, value, noAutoCreate);
				protoReader.CheckFullyConsumed();
				result = obj;
			}
			finally
			{
				ProtoReader.Recycle(protoReader);
			}
			return result;
		}

		// Token: 0x06007F9B RID: 32667 RVA: 0x0025B628 File Offset: 0x0025B628
		private bool PrepareDeserialize(object value, ref Type type)
		{
			if (type == null)
			{
				if (value == null)
				{
					throw new ArgumentNullException("type");
				}
				type = this.MapType(value.GetType());
			}
			bool result = true;
			Type underlyingType = Helpers.GetUnderlyingType(type);
			if (underlyingType != null)
			{
				type = underlyingType;
				result = false;
			}
			return result;
		}

		// Token: 0x06007F9C RID: 32668 RVA: 0x0025B684 File Offset: 0x0025B684
		public object Deserialize(Stream source, object value, Type type, int length)
		{
			return this.Deserialize(source, value, type, length, null);
		}

		// Token: 0x06007F9D RID: 32669 RVA: 0x0025B694 File Offset: 0x0025B694
		public object Deserialize(Stream source, object value, Type type, long length)
		{
			return this.Deserialize(source, value, type, length, null);
		}

		// Token: 0x06007F9E RID: 32670 RVA: 0x0025B6A4 File Offset: 0x0025B6A4
		public object Deserialize(Stream source, object value, Type type, int length, SerializationContext context)
		{
			return this.Deserialize(source, value, type, (length == int.MaxValue) ? long.MaxValue : ((long)length), context);
		}

		// Token: 0x06007F9F RID: 32671 RVA: 0x0025B6E0 File Offset: 0x0025B6E0
		public object Deserialize(Stream source, object value, Type type, long length, SerializationContext context)
		{
			bool noAutoCreate = this.PrepareDeserialize(value, ref type);
			ProtoReader protoReader = null;
			object result;
			try
			{
				protoReader = ProtoReader.Create(source, this, context, length);
				if (value != null)
				{
					protoReader.SetRootObject(value);
				}
				object obj = this.DeserializeCore(protoReader, type, value, noAutoCreate);
				protoReader.CheckFullyConsumed();
				result = obj;
			}
			finally
			{
				ProtoReader.Recycle(protoReader);
			}
			return result;
		}

		// Token: 0x06007FA0 RID: 32672 RVA: 0x0025B744 File Offset: 0x0025B744
		public object Deserialize(ProtoReader source, object value, Type type)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			bool noAutoCreate = this.PrepareDeserialize(value, ref type);
			if (value != null)
			{
				source.SetRootObject(value);
			}
			object result = this.DeserializeCore(source, type, value, noAutoCreate);
			source.CheckFullyConsumed();
			return result;
		}

		// Token: 0x06007FA1 RID: 32673 RVA: 0x0025B790 File Offset: 0x0025B790
		private object DeserializeCore(ProtoReader reader, Type type, object value, bool noAutoCreate)
		{
			int key = this.GetKey(ref type);
			if (key >= 0 && !Helpers.IsEnum(type))
			{
				return this.Deserialize(key, value, reader);
			}
			this.TryDeserializeAuxiliaryType(reader, DataFormat.Default, 1, type, ref value, true, false, noAutoCreate, false, null);
			return value;
		}

		// Token: 0x06007FA2 RID: 32674 RVA: 0x0025B7DC File Offset: 0x0025B7DC
		internal static MethodInfo ResolveListAdd(TypeModel model, Type listType, Type itemType, out bool isList)
		{
			isList = model.MapType(TypeModel.ilist).IsAssignableFrom(listType);
			Type[] array = new Type[]
			{
				itemType
			};
			MethodInfo instanceMethod = Helpers.GetInstanceMethod(listType, "Add", array);
			if (instanceMethod == null)
			{
				bool flag = listType.IsInterface && model.MapType(typeof(IEnumerable<>)).MakeGenericType(array).IsAssignableFrom(listType);
				Type type = model.MapType(typeof(ICollection<>)).MakeGenericType(array);
				if (flag || type.IsAssignableFrom(listType))
				{
					instanceMethod = Helpers.GetInstanceMethod(type, "Add", array);
				}
			}
			if (instanceMethod == null)
			{
				foreach (Type type2 in listType.GetInterfaces())
				{
					if (type2.Name == "IProducerConsumerCollection`1" && type2.IsGenericType && type2.GetGenericTypeDefinition().FullName == "System.Collections.Concurrent.IProducerConsumerCollection`1")
					{
						instanceMethod = Helpers.GetInstanceMethod(type2, "TryAdd", array);
						if (instanceMethod != null)
						{
							break;
						}
					}
				}
			}
			if (instanceMethod == null)
			{
				array[0] = model.MapType(typeof(object));
				instanceMethod = Helpers.GetInstanceMethod(listType, "Add", array);
			}
			if (instanceMethod == null & isList)
			{
				instanceMethod = Helpers.GetInstanceMethod(model.MapType(TypeModel.ilist), "Add", array);
			}
			return instanceMethod;
		}

		// Token: 0x06007FA3 RID: 32675 RVA: 0x0025B970 File Offset: 0x0025B970
		internal static Type GetListItemType(TypeModel model, Type listType)
		{
			if (listType == model.MapType(typeof(string)) || listType.IsArray || !model.MapType(typeof(IEnumerable)).IsAssignableFrom(listType))
			{
				return null;
			}
			BasicList basicList = new BasicList();
			foreach (MethodInfo methodInfo in listType.GetMethods())
			{
				if (!methodInfo.IsStatic && !(methodInfo.Name != "Add"))
				{
					ParameterInfo[] parameters = methodInfo.GetParameters();
					Type parameterType;
					if (parameters.Length == 1 && !basicList.Contains(parameterType = parameters[0].ParameterType))
					{
						basicList.Add(parameterType);
					}
				}
			}
			string name = listType.Name;
			if (name == null || (name.IndexOf("Queue") < 0 && name.IndexOf("Stack") < 0))
			{
				TypeModel.TestEnumerableListPatterns(model, basicList, listType);
				foreach (Type iType in listType.GetInterfaces())
				{
					TypeModel.TestEnumerableListPatterns(model, basicList, iType);
				}
			}
			foreach (PropertyInfo propertyInfo in listType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				if (!(propertyInfo.Name != "Item") && !basicList.Contains(propertyInfo.PropertyType))
				{
					ParameterInfo[] indexParameters = propertyInfo.GetIndexParameters();
					if (indexParameters.Length == 1 && !(indexParameters[0].ParameterType != model.MapType(typeof(int))))
					{
						basicList.Add(propertyInfo.PropertyType);
					}
				}
			}
			switch (basicList.Count)
			{
			case 0:
				return null;
			case 1:
				if ((Type)basicList[0] == listType)
				{
					return null;
				}
				return (Type)basicList[0];
			case 2:
				if ((Type)basicList[0] != listType && TypeModel.CheckDictionaryAccessors(model, (Type)basicList[0], (Type)basicList[1]))
				{
					return (Type)basicList[0];
				}
				if ((Type)basicList[1] != listType && TypeModel.CheckDictionaryAccessors(model, (Type)basicList[1], (Type)basicList[0]))
				{
					return (Type)basicList[1];
				}
				break;
			}
			return null;
		}

		// Token: 0x06007FA4 RID: 32676 RVA: 0x0025BC38 File Offset: 0x0025BC38
		private static void TestEnumerableListPatterns(TypeModel model, BasicList candidates, Type iType)
		{
			if (iType.IsGenericType)
			{
				Type genericTypeDefinition = iType.GetGenericTypeDefinition();
				if (genericTypeDefinition == model.MapType(typeof(IEnumerable<>)) || genericTypeDefinition == model.MapType(typeof(ICollection<>)) || genericTypeDefinition.FullName == "System.Collections.Concurrent.IProducerConsumerCollection`1")
				{
					Type[] genericArguments = iType.GetGenericArguments();
					if (!candidates.Contains(genericArguments[0]))
					{
						candidates.Add(genericArguments[0]);
					}
				}
			}
		}

		// Token: 0x06007FA5 RID: 32677 RVA: 0x0025BCD0 File Offset: 0x0025BCD0
		private static bool CheckDictionaryAccessors(TypeModel model, Type pair, Type value)
		{
			return pair.IsGenericType && pair.GetGenericTypeDefinition() == model.MapType(typeof(KeyValuePair<, >)) && pair.GetGenericArguments()[1] == value;
		}

		// Token: 0x06007FA6 RID: 32678 RVA: 0x0025BD20 File Offset: 0x0025BD20
		private bool TryDeserializeList(TypeModel model, ProtoReader reader, DataFormat format, int tag, Type listType, Type itemType, ref object value)
		{
			bool flag;
			MethodInfo methodInfo = TypeModel.ResolveListAdd(model, listType, itemType, out flag);
			if (methodInfo == null)
			{
				throw new NotSupportedException("Unknown list variant: " + listType.FullName);
			}
			bool result = false;
			object obj = null;
			IList list = value as IList;
			object[] array = flag ? null : new object[1];
			BasicList basicList = listType.IsArray ? new BasicList() : null;
			while (this.TryDeserializeAuxiliaryType(reader, format, tag, itemType, ref obj, true, true, true, true, value ?? listType))
			{
				result = true;
				if (value == null && basicList == null)
				{
					value = TypeModel.CreateListInstance(listType, itemType);
					list = (value as IList);
				}
				if (list != null)
				{
					list.Add(obj);
				}
				else if (basicList != null)
				{
					basicList.Add(obj);
				}
				else
				{
					array[0] = obj;
					methodInfo.Invoke(value, array);
				}
				obj = null;
			}
			if (basicList != null)
			{
				if (value != null)
				{
					if (basicList.Count != 0)
					{
						Array array2 = (Array)value;
						Array array3 = Array.CreateInstance(itemType, array2.Length + basicList.Count);
						Array.Copy(array2, array3, array2.Length);
						basicList.CopyTo(array3, array2.Length);
						value = array3;
					}
				}
				else
				{
					Array array3 = Array.CreateInstance(itemType, basicList.Count);
					basicList.CopyTo(array3, 0);
					value = array3;
				}
			}
			return result;
		}

		// Token: 0x06007FA7 RID: 32679 RVA: 0x0025BEB0 File Offset: 0x0025BEB0
		private static object CreateListInstance(Type listType, Type itemType)
		{
			Type type = listType;
			if (listType.IsArray)
			{
				return Array.CreateInstance(itemType, 0);
			}
			if (!listType.IsClass || listType.IsAbstract || Helpers.GetConstructor(listType, Helpers.EmptyTypes, true) == null)
			{
				bool flag = false;
				string fullName;
				if (listType.IsInterface && (fullName = listType.FullName) != null && fullName.IndexOf("Dictionary") >= 0)
				{
					if (listType.IsGenericType && listType.GetGenericTypeDefinition() == typeof(IDictionary<, >))
					{
						Type[] genericArguments = listType.GetGenericArguments();
						type = typeof(Dictionary<, >).MakeGenericType(genericArguments);
						flag = true;
					}
					if (!flag && listType == typeof(IDictionary))
					{
						type = typeof(Hashtable);
						flag = true;
					}
				}
				if (!flag)
				{
					type = typeof(List<>).MakeGenericType(new Type[]
					{
						itemType
					});
					flag = true;
				}
				if (!flag)
				{
					type = typeof(ArrayList);
				}
			}
			return Activator.CreateInstance(type);
		}

		// Token: 0x06007FA8 RID: 32680 RVA: 0x0025BFD0 File Offset: 0x0025BFD0
		internal bool TryDeserializeAuxiliaryType(ProtoReader reader, DataFormat format, int tag, Type type, ref object value, bool skipOtherFields, bool asListItem, bool autoCreate, bool insideList, object parentListOrType)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			ProtoTypeCode typeCode = Helpers.GetTypeCode(type);
			int num;
			WireType wireType = this.GetWireType(typeCode, format, ref type, out num);
			bool flag = false;
			if (wireType == WireType.None)
			{
				Type type2 = TypeModel.GetListItemType(this, type);
				if (type2 == null && type.IsArray && type.GetArrayRank() == 1 && type != typeof(byte[]))
				{
					type2 = type.GetElementType();
				}
				if (type2 != null)
				{
					if (insideList)
					{
						throw TypeModel.CreateNestedListsNotSupported((parentListOrType as Type) ?? ((parentListOrType != null) ? parentListOrType.GetType() : null));
					}
					flag = this.TryDeserializeList(this, reader, format, tag, type, type2, ref value);
					if (!flag && autoCreate)
					{
						value = TypeModel.CreateListInstance(type, type2);
					}
					return flag;
				}
				else
				{
					TypeModel.ThrowUnexpectedType(type);
				}
			}
			while (!flag || !asListItem)
			{
				int num2 = reader.ReadFieldHeader();
				if (num2 <= 0)
				{
					break;
				}
				if (num2 != tag)
				{
					if (!skipOtherFields)
					{
						throw ProtoReader.AddErrorData(new InvalidOperationException("Expected field " + tag.ToString() + ", but found " + num2.ToString()), reader);
					}
					reader.SkipField();
				}
				else
				{
					flag = true;
					reader.Hint(wireType);
					if (num >= 0)
					{
						if (wireType - WireType.String <= 1)
						{
							SubItemToken token = ProtoReader.StartSubItem(reader);
							value = this.Deserialize(num, value, reader);
							ProtoReader.EndSubItem(token, reader);
						}
						else
						{
							value = this.Deserialize(num, value, reader);
						}
					}
					else
					{
						switch (typeCode)
						{
						case ProtoTypeCode.Boolean:
							value = reader.ReadBoolean();
							break;
						case ProtoTypeCode.Char:
							value = (char)reader.ReadUInt16();
							break;
						case ProtoTypeCode.SByte:
							value = reader.ReadSByte();
							break;
						case ProtoTypeCode.Byte:
							value = reader.ReadByte();
							break;
						case ProtoTypeCode.Int16:
							value = reader.ReadInt16();
							break;
						case ProtoTypeCode.UInt16:
							value = reader.ReadUInt16();
							break;
						case ProtoTypeCode.Int32:
							value = reader.ReadInt32();
							break;
						case ProtoTypeCode.UInt32:
							value = reader.ReadUInt32();
							break;
						case ProtoTypeCode.Int64:
							value = reader.ReadInt64();
							break;
						case ProtoTypeCode.UInt64:
							value = reader.ReadUInt64();
							break;
						case ProtoTypeCode.Single:
							value = reader.ReadSingle();
							break;
						case ProtoTypeCode.Double:
							value = reader.ReadDouble();
							break;
						case ProtoTypeCode.Decimal:
							value = BclHelpers.ReadDecimal(reader);
							break;
						case ProtoTypeCode.DateTime:
							value = BclHelpers.ReadDateTime(reader);
							break;
						case (ProtoTypeCode)17:
							break;
						case ProtoTypeCode.String:
							value = reader.ReadString();
							break;
						default:
							switch (typeCode)
							{
							case ProtoTypeCode.TimeSpan:
								value = BclHelpers.ReadTimeSpan(reader);
								break;
							case ProtoTypeCode.ByteArray:
								value = ProtoReader.AppendBytes((byte[])value, reader);
								break;
							case ProtoTypeCode.Guid:
								value = BclHelpers.ReadGuid(reader);
								break;
							case ProtoTypeCode.Uri:
								value = new Uri(reader.ReadString(), UriKind.RelativeOrAbsolute);
								break;
							}
							break;
						}
					}
				}
			}
			if (!flag && !asListItem && autoCreate && type != typeof(string))
			{
				value = Activator.CreateInstance(type);
			}
			return flag;
		}

		// Token: 0x06007FA9 RID: 32681 RVA: 0x0025C388 File Offset: 0x0025C388
		[Obsolete("Please use RuntimeTypeModel.Create", false)]
		public static RuntimeTypeModel Create()
		{
			return RuntimeTypeModel.Create(null);
		}

		// Token: 0x06007FAA RID: 32682 RVA: 0x0025C390 File Offset: 0x0025C390
		protected internal static Type ResolveProxies(Type type)
		{
			if (type == null)
			{
				return null;
			}
			if (type.IsGenericParameter)
			{
				return null;
			}
			Type underlyingType = Helpers.GetUnderlyingType(type);
			if (underlyingType != null)
			{
				return underlyingType;
			}
			string fullName = type.FullName;
			if (fullName != null && fullName.StartsWith("System.Data.Entity.DynamicProxies."))
			{
				return type.BaseType;
			}
			Type[] interfaces = type.GetInterfaces();
			foreach (Type type2 in interfaces)
			{
				string fullName2 = type2.FullName;
				if (fullName2 != null && (fullName2 == "NHibernate.Proxy.INHibernateProxy" || fullName2 == "NHibernate.Proxy.DynamicProxy.IProxy" || fullName2 == "NHibernate.Intercept.IFieldInterceptorAccessor"))
				{
					return type.BaseType;
				}
			}
			return null;
		}

		// Token: 0x06007FAB RID: 32683 RVA: 0x0025C468 File Offset: 0x0025C468
		public bool IsDefined(Type type)
		{
			return this.GetKey(ref type) >= 0;
		}

		// Token: 0x06007FAC RID: 32684 RVA: 0x0025C478 File Offset: 0x0025C478
		protected internal int GetKey(ref Type type)
		{
			if (type == null)
			{
				return -1;
			}
			Dictionary<Type, TypeModel.KnownTypeKey> obj = this.knownKeys;
			lock (obj)
			{
				TypeModel.KnownTypeKey knownTypeKey;
				if (this.knownKeys.TryGetValue(type, out knownTypeKey))
				{
					type = knownTypeKey.Type;
					return knownTypeKey.Key;
				}
			}
			int keyImpl = this.GetKeyImpl(type);
			Type key = type;
			if (keyImpl < 0)
			{
				Type type2 = TypeModel.ResolveProxies(type);
				if (type2 != null && type2 != type)
				{
					type = type2;
					keyImpl = this.GetKeyImpl(type);
				}
			}
			Dictionary<Type, TypeModel.KnownTypeKey> obj2 = this.knownKeys;
			lock (obj2)
			{
				this.knownKeys[key] = new TypeModel.KnownTypeKey(type, keyImpl);
			}
			return keyImpl;
		}

		// Token: 0x06007FAD RID: 32685 RVA: 0x0025C588 File Offset: 0x0025C588
		internal void ResetKeyCache()
		{
			Dictionary<Type, TypeModel.KnownTypeKey> obj = this.knownKeys;
			lock (obj)
			{
				this.knownKeys.Clear();
			}
		}

		// Token: 0x06007FAE RID: 32686
		protected abstract int GetKeyImpl(Type type);

		// Token: 0x06007FAF RID: 32687
		protected internal abstract void Serialize(int key, object value, ProtoWriter dest);

		// Token: 0x06007FB0 RID: 32688
		protected internal abstract object Deserialize(int key, object value, ProtoReader source);

		// Token: 0x06007FB1 RID: 32689 RVA: 0x0025C5D4 File Offset: 0x0025C5D4
		public object DeepClone(object value)
		{
			if (value == null)
			{
				return null;
			}
			Type type = value.GetType();
			int key = this.GetKey(ref type);
			if (key >= 0 && !Helpers.IsEnum(type))
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (ProtoWriter protoWriter = ProtoWriter.Create(memoryStream, this, null))
					{
						protoWriter.SetRootObject(value);
						this.Serialize(key, value, protoWriter);
						protoWriter.Close();
					}
					memoryStream.Position = 0L;
					ProtoReader protoReader = null;
					try
					{
						protoReader = ProtoReader.Create(memoryStream, this, null, -1L);
						return this.Deserialize(key, null, protoReader);
					}
					finally
					{
						ProtoReader.Recycle(protoReader);
					}
				}
			}
			if (type == typeof(byte[]))
			{
				byte[] array = (byte[])value;
				byte[] array2 = new byte[array.Length];
				Buffer.BlockCopy(array, 0, array2, 0, array.Length);
				return array2;
			}
			int num;
			if (this.GetWireType(Helpers.GetTypeCode(type), DataFormat.Default, ref type, out num) != WireType.None && num < 0)
			{
				return value;
			}
			object result;
			using (MemoryStream memoryStream2 = new MemoryStream())
			{
				using (ProtoWriter protoWriter2 = ProtoWriter.Create(memoryStream2, this, null))
				{
					if (!this.TrySerializeAuxiliaryType(protoWriter2, type, DataFormat.Default, 1, value, false, null))
					{
						TypeModel.ThrowUnexpectedType(type);
					}
					protoWriter2.Close();
				}
				memoryStream2.Position = 0L;
				ProtoReader reader = null;
				try
				{
					reader = ProtoReader.Create(memoryStream2, this, null, -1L);
					value = null;
					this.TryDeserializeAuxiliaryType(reader, DataFormat.Default, 1, type, ref value, true, false, true, false, null);
					result = value;
				}
				finally
				{
					ProtoReader.Recycle(reader);
				}
			}
			return result;
		}

		// Token: 0x06007FB2 RID: 32690 RVA: 0x0025C7BC File Offset: 0x0025C7BC
		protected internal static void ThrowUnexpectedSubtype(Type expected, Type actual)
		{
			if (expected != TypeModel.ResolveProxies(actual))
			{
				throw new InvalidOperationException("Unexpected sub-type: " + actual.FullName);
			}
		}

		// Token: 0x06007FB3 RID: 32691 RVA: 0x0025C7E8 File Offset: 0x0025C7E8
		protected internal static void ThrowUnexpectedType(Type type)
		{
			string str = (type == null) ? "(unknown)" : type.FullName;
			if (type != null)
			{
				Type baseType = type.BaseType;
				if (baseType != null && baseType.IsGenericType && baseType.GetGenericTypeDefinition().Name == "GeneratedMessage`2")
				{
					throw new InvalidOperationException("Are you mixing protobuf-net and protobuf-csharp-port? See https://stackoverflow.com/q/11564914/23354; type: " + str);
				}
			}
			throw new InvalidOperationException("Type is not expected, and no contract can be inferred: " + str);
		}

		// Token: 0x06007FB4 RID: 32692 RVA: 0x0025C87C File Offset: 0x0025C87C
		internal static Exception CreateNestedListsNotSupported(Type type)
		{
			return new NotSupportedException("Nested or jagged lists and arrays are not supported: " + (((type != null) ? type.FullName : null) ?? "(null)"));
		}

		// Token: 0x06007FB5 RID: 32693 RVA: 0x0025C8AC File Offset: 0x0025C8AC
		public static void ThrowCannotCreateInstance(Type type)
		{
			throw new ProtoException("No parameterless constructor found for " + (((type != null) ? type.FullName : null) ?? "(null)"));
		}

		// Token: 0x06007FB6 RID: 32694 RVA: 0x0025C8DC File Offset: 0x0025C8DC
		internal static string SerializeType(TypeModel model, Type type)
		{
			if (model != null)
			{
				TypeFormatEventHandler dynamicTypeFormatting = model.DynamicTypeFormatting;
				if (dynamicTypeFormatting != null)
				{
					TypeFormatEventArgs typeFormatEventArgs = new TypeFormatEventArgs(type);
					dynamicTypeFormatting(model, typeFormatEventArgs);
					if (!string.IsNullOrEmpty(typeFormatEventArgs.FormattedName))
					{
						return typeFormatEventArgs.FormattedName;
					}
				}
			}
			return type.AssemblyQualifiedName;
		}

		// Token: 0x06007FB7 RID: 32695 RVA: 0x0025C92C File Offset: 0x0025C92C
		internal static Type DeserializeType(TypeModel model, string value)
		{
			if (model != null)
			{
				TypeFormatEventHandler dynamicTypeFormatting = model.DynamicTypeFormatting;
				if (dynamicTypeFormatting != null)
				{
					TypeFormatEventArgs typeFormatEventArgs = new TypeFormatEventArgs(value);
					dynamicTypeFormatting(model, typeFormatEventArgs);
					if (typeFormatEventArgs.Type != null)
					{
						return typeFormatEventArgs.Type;
					}
				}
			}
			return Type.GetType(value);
		}

		// Token: 0x06007FB8 RID: 32696 RVA: 0x0025C980 File Offset: 0x0025C980
		public bool CanSerializeContractType(Type type)
		{
			return this.CanSerialize(type, false, true, true);
		}

		// Token: 0x06007FB9 RID: 32697 RVA: 0x0025C98C File Offset: 0x0025C98C
		public bool CanSerialize(Type type)
		{
			return this.CanSerialize(type, true, true, true);
		}

		// Token: 0x06007FBA RID: 32698 RVA: 0x0025C998 File Offset: 0x0025C998
		public bool CanSerializeBasicType(Type type)
		{
			return this.CanSerialize(type, true, false, true);
		}

		// Token: 0x06007FBB RID: 32699 RVA: 0x0025C9A4 File Offset: 0x0025C9A4
		private bool CanSerialize(Type type, bool allowBasic, bool allowContract, bool allowLists)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			Type underlyingType = Helpers.GetUnderlyingType(type);
			if (underlyingType != null)
			{
				type = underlyingType;
			}
			ProtoTypeCode typeCode = Helpers.GetTypeCode(type);
			if (typeCode > ProtoTypeCode.Unknown)
			{
				return allowBasic;
			}
			int key = this.GetKey(ref type);
			if (key >= 0)
			{
				return allowContract;
			}
			if (allowLists)
			{
				Type type2 = null;
				if (type.IsArray)
				{
					if (type.GetArrayRank() == 1)
					{
						type2 = type.GetElementType();
					}
				}
				else
				{
					type2 = TypeModel.GetListItemType(this, type);
				}
				if (type2 != null)
				{
					return this.CanSerialize(type2, allowBasic, allowContract, false);
				}
			}
			return false;
		}

		// Token: 0x06007FBC RID: 32700 RVA: 0x0025CA50 File Offset: 0x0025CA50
		public virtual string GetSchema(Type type)
		{
			return this.GetSchema(type, ProtoSyntax.Proto2);
		}

		// Token: 0x06007FBD RID: 32701 RVA: 0x0025CA5C File Offset: 0x0025CA5C
		public virtual string GetSchema(Type type, ProtoSyntax syntax)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1400005F RID: 95
		// (add) Token: 0x06007FBE RID: 32702 RVA: 0x0025CA64 File Offset: 0x0025CA64
		// (remove) Token: 0x06007FBF RID: 32703 RVA: 0x0025CAA0 File Offset: 0x0025CAA0
		public event TypeFormatEventHandler DynamicTypeFormatting;

		// Token: 0x06007FC0 RID: 32704 RVA: 0x0025CADC File Offset: 0x0025CADC
		public IFormatter CreateFormatter(Type type)
		{
			return new TypeModel.Formatter(this, type);
		}

		// Token: 0x06007FC1 RID: 32705 RVA: 0x0025CAE8 File Offset: 0x0025CAE8
		internal virtual Type GetType(string fullName, Assembly context)
		{
			return TypeModel.ResolveKnownType(fullName, this, context);
		}

		// Token: 0x06007FC2 RID: 32706 RVA: 0x0025CAF4 File Offset: 0x0025CAF4
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static Type ResolveKnownType(string name, TypeModel model, Assembly assembly)
		{
			if (string.IsNullOrEmpty(name))
			{
				return null;
			}
			try
			{
				Type type = Type.GetType(name);
				if (type != null)
				{
					return type;
				}
			}
			catch
			{
			}
			try
			{
				int num = name.IndexOf(',');
				string name2 = ((num > 0) ? name.Substring(0, num) : name).Trim();
				if (assembly == null)
				{
					assembly = Assembly.GetCallingAssembly();
				}
				Type type2 = (assembly != null) ? assembly.GetType(name2) : null;
				if (type2 != null)
				{
					return type2;
				}
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x06007FC3 RID: 32707 RVA: 0x0025CBC0 File Offset: 0x0025CBC0
		private static SerializationContext CreateContext(object userState)
		{
			if (userState == null)
			{
				return SerializationContext.Default;
			}
			SerializationContext serializationContext = userState as SerializationContext;
			if (serializationContext != null)
			{
				return serializationContext;
			}
			SerializationContext serializationContext2 = new SerializationContext
			{
				Context = userState
			};
			serializationContext2.Freeze();
			return serializationContext2;
		}

		// Token: 0x06007FC4 RID: 32708 RVA: 0x0025CC00 File Offset: 0x0025CC00
		T IProtoInput<Stream>.Deserialize<T>(Stream source, T value, object userState)
		{
			return (T)((object)this.Deserialize(source, value, typeof(T), TypeModel.CreateContext(userState)));
		}

		// Token: 0x06007FC5 RID: 32709 RVA: 0x0025CC24 File Offset: 0x0025CC24
		T IProtoInput<ArraySegment<byte>>.Deserialize<T>(ArraySegment<byte> source, T value, object userState)
		{
			T result;
			using (MemoryStream memoryStream = new MemoryStream(source.Array, source.Offset, source.Count))
			{
				result = (T)((object)this.Deserialize(memoryStream, value, typeof(T), TypeModel.CreateContext(userState)));
			}
			return result;
		}

		// Token: 0x06007FC6 RID: 32710 RVA: 0x0025CC94 File Offset: 0x0025CC94
		T IProtoInput<byte[]>.Deserialize<T>(byte[] source, T value, object userState)
		{
			T result;
			using (MemoryStream memoryStream = new MemoryStream(source))
			{
				result = (T)((object)this.Deserialize(memoryStream, value, typeof(T), TypeModel.CreateContext(userState)));
			}
			return result;
		}

		// Token: 0x06007FC7 RID: 32711 RVA: 0x0025CCF0 File Offset: 0x0025CCF0
		void IProtoOutput<Stream>.Serialize<T>(Stream destination, T value, object userState)
		{
			this.Serialize(destination, value, TypeModel.CreateContext(userState));
		}

		// Token: 0x04003CE2 RID: 15586
		private static readonly Type ilist = typeof(IList);

		// Token: 0x04003CE3 RID: 15587
		private readonly Dictionary<Type, TypeModel.KnownTypeKey> knownKeys = new Dictionary<Type, TypeModel.KnownTypeKey>();

		// Token: 0x02001183 RID: 4483
		private sealed class DeserializeItemsIterator<T> : TypeModel.DeserializeItemsIterator, IEnumerator<!0>, IDisposable, IEnumerator, IEnumerable<!0>, IEnumerable
		{
			// Token: 0x06009398 RID: 37784 RVA: 0x002C3614 File Offset: 0x002C3614
			IEnumerator<T> IEnumerable<!0>.GetEnumerator()
			{
				return this;
			}

			// Token: 0x17001E91 RID: 7825
			// (get) Token: 0x06009399 RID: 37785 RVA: 0x002C3618 File Offset: 0x002C3618
			public new T Current
			{
				get
				{
					return (T)((object)base.Current);
				}
			}

			// Token: 0x0600939A RID: 37786 RVA: 0x002C3628 File Offset: 0x002C3628
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0600939B RID: 37787 RVA: 0x002C362C File Offset: 0x002C362C
			public DeserializeItemsIterator(TypeModel model, Stream source, PrefixStyle style, int expectedField, SerializationContext context) : base(model, source, model.MapType(typeof(T)), style, expectedField, null, context)
			{
			}
		}

		// Token: 0x02001184 RID: 4484
		private class DeserializeItemsIterator : IEnumerator, IEnumerable
		{
			// Token: 0x0600939C RID: 37788 RVA: 0x002C365C File Offset: 0x002C365C
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this;
			}

			// Token: 0x0600939D RID: 37789 RVA: 0x002C3660 File Offset: 0x002C3660
			public bool MoveNext()
			{
				if (this.haveObject)
				{
					long num;
					this.current = this.model.DeserializeWithLengthPrefix(this.source, null, this.type, this.style, this.expectedField, this.resolver, out num, out this.haveObject, this.context);
				}
				return this.haveObject;
			}

			// Token: 0x0600939E RID: 37790 RVA: 0x002C36C0 File Offset: 0x002C36C0
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17001E92 RID: 7826
			// (get) Token: 0x0600939F RID: 37791 RVA: 0x002C36C8 File Offset: 0x002C36C8
			public object Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x060093A0 RID: 37792 RVA: 0x002C36D0 File Offset: 0x002C36D0
			public DeserializeItemsIterator(TypeModel model, Stream source, Type type, PrefixStyle style, int expectedField, Serializer.TypeResolver resolver, SerializationContext context)
			{
				this.haveObject = true;
				this.source = source;
				this.type = type;
				this.style = style;
				this.expectedField = expectedField;
				this.resolver = resolver;
				this.model = model;
				this.context = context;
			}

			// Token: 0x04004B7C RID: 19324
			private bool haveObject;

			// Token: 0x04004B7D RID: 19325
			private object current;

			// Token: 0x04004B7E RID: 19326
			private readonly Stream source;

			// Token: 0x04004B7F RID: 19327
			private readonly Type type;

			// Token: 0x04004B80 RID: 19328
			private readonly PrefixStyle style;

			// Token: 0x04004B81 RID: 19329
			private readonly int expectedField;

			// Token: 0x04004B82 RID: 19330
			private readonly Serializer.TypeResolver resolver;

			// Token: 0x04004B83 RID: 19331
			private readonly TypeModel model;

			// Token: 0x04004B84 RID: 19332
			private readonly SerializationContext context;
		}

		// Token: 0x02001185 RID: 4485
		[protobuf-net.IsReadOnly]
		private struct KnownTypeKey
		{
			// Token: 0x060093A1 RID: 37793 RVA: 0x002C3724 File Offset: 0x002C3724
			public KnownTypeKey(Type type, int key)
			{
				this.Type = type;
				this.Key = key;
			}

			// Token: 0x17001E93 RID: 7827
			// (get) Token: 0x060093A2 RID: 37794 RVA: 0x002C3734 File Offset: 0x002C3734
			public int Key { get; }

			// Token: 0x17001E94 RID: 7828
			// (get) Token: 0x060093A3 RID: 37795 RVA: 0x002C373C File Offset: 0x002C373C
			public Type Type { get; }
		}

		// Token: 0x02001186 RID: 4486
		protected internal enum CallbackType
		{
			// Token: 0x04004B88 RID: 19336
			BeforeSerialize,
			// Token: 0x04004B89 RID: 19337
			AfterSerialize,
			// Token: 0x04004B8A RID: 19338
			BeforeDeserialize,
			// Token: 0x04004B8B RID: 19339
			AfterDeserialize
		}

		// Token: 0x02001187 RID: 4487
		internal sealed class Formatter : IFormatter
		{
			// Token: 0x060093A4 RID: 37796 RVA: 0x002C3744 File Offset: 0x002C3744
			internal Formatter(TypeModel model, Type type)
			{
				if (model == null)
				{
					throw new ArgumentNullException("model");
				}
				this.model = model;
				if (type == null)
				{
					throw new ArgumentNullException("type");
				}
				this.type = type;
			}

			// Token: 0x17001E95 RID: 7829
			// (get) Token: 0x060093A5 RID: 37797 RVA: 0x002C3780 File Offset: 0x002C3780
			// (set) Token: 0x060093A6 RID: 37798 RVA: 0x002C3788 File Offset: 0x002C3788
			public SerializationBinder Binder
			{
				get
				{
					return this.binder;
				}
				set
				{
					this.binder = value;
				}
			}

			// Token: 0x17001E96 RID: 7830
			// (get) Token: 0x060093A7 RID: 37799 RVA: 0x002C3794 File Offset: 0x002C3794
			// (set) Token: 0x060093A8 RID: 37800 RVA: 0x002C379C File Offset: 0x002C379C
			public StreamingContext Context
			{
				get
				{
					return this.context;
				}
				set
				{
					this.context = value;
				}
			}

			// Token: 0x060093A9 RID: 37801 RVA: 0x002C37A8 File Offset: 0x002C37A8
			public object Deserialize(Stream source)
			{
				return this.model.Deserialize(source, null, this.type, -1L, this.Context);
			}

			// Token: 0x060093AA RID: 37802 RVA: 0x002C37DC File Offset: 0x002C37DC
			public void Serialize(Stream destination, object graph)
			{
				this.model.Serialize(destination, graph, this.Context);
			}

			// Token: 0x17001E97 RID: 7831
			// (get) Token: 0x060093AB RID: 37803 RVA: 0x002C37F8 File Offset: 0x002C37F8
			// (set) Token: 0x060093AC RID: 37804 RVA: 0x002C3800 File Offset: 0x002C3800
			public ISurrogateSelector SurrogateSelector
			{
				get
				{
					return this.surrogateSelector;
				}
				set
				{
					this.surrogateSelector = value;
				}
			}

			// Token: 0x04004B8C RID: 19340
			private readonly TypeModel model;

			// Token: 0x04004B8D RID: 19341
			private readonly Type type;

			// Token: 0x04004B8E RID: 19342
			private SerializationBinder binder;

			// Token: 0x04004B8F RID: 19343
			private StreamingContext context;

			// Token: 0x04004B90 RID: 19344
			private ISurrogateSelector surrogateSelector;
		}
	}
}
