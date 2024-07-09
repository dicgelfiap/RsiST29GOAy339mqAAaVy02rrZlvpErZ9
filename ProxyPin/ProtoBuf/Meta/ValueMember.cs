using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using ProtoBuf.Serializers;

namespace ProtoBuf.Meta
{
	// Token: 0x02000C7E RID: 3198
	[ComVisible(true)]
	public class ValueMember
	{
		// Token: 0x17001BB7 RID: 7095
		// (get) Token: 0x06007FCA RID: 32714 RVA: 0x0025CD30 File Offset: 0x0025CD30
		// (set) Token: 0x06007FCB RID: 32715 RVA: 0x0025CD38 File Offset: 0x0025CD38
		public int FieldNumber
		{
			get
			{
				return this._fieldNumber;
			}
			internal set
			{
				if (this._fieldNumber != value)
				{
					MetaType.AssertValidFieldNumber(value);
					this.ThrowIfFrozen();
					this._fieldNumber = value;
				}
			}
		}

		// Token: 0x17001BB8 RID: 7096
		// (get) Token: 0x06007FCC RID: 32716 RVA: 0x0025CD5C File Offset: 0x0025CD5C
		public MemberInfo Member
		{
			get
			{
				return this.originalMember;
			}
		}

		// Token: 0x17001BB9 RID: 7097
		// (get) Token: 0x06007FCD RID: 32717 RVA: 0x0025CD64 File Offset: 0x0025CD64
		// (set) Token: 0x06007FCE RID: 32718 RVA: 0x0025CD6C File Offset: 0x0025CD6C
		public MemberInfo BackingMember
		{
			get
			{
				return this.backingMember;
			}
			set
			{
				if (this.backingMember != value)
				{
					this.ThrowIfFrozen();
					this.backingMember = value;
				}
			}
		}

		// Token: 0x17001BBA RID: 7098
		// (get) Token: 0x06007FCF RID: 32719 RVA: 0x0025CD8C File Offset: 0x0025CD8C
		public Type ItemType
		{
			get
			{
				return this.itemType;
			}
		}

		// Token: 0x17001BBB RID: 7099
		// (get) Token: 0x06007FD0 RID: 32720 RVA: 0x0025CD94 File Offset: 0x0025CD94
		public Type MemberType
		{
			get
			{
				return this.memberType;
			}
		}

		// Token: 0x17001BBC RID: 7100
		// (get) Token: 0x06007FD1 RID: 32721 RVA: 0x0025CD9C File Offset: 0x0025CD9C
		public Type DefaultType
		{
			get
			{
				return this.defaultType;
			}
		}

		// Token: 0x17001BBD RID: 7101
		// (get) Token: 0x06007FD2 RID: 32722 RVA: 0x0025CDA4 File Offset: 0x0025CDA4
		public Type ParentType
		{
			get
			{
				return this.parentType;
			}
		}

		// Token: 0x17001BBE RID: 7102
		// (get) Token: 0x06007FD3 RID: 32723 RVA: 0x0025CDAC File Offset: 0x0025CDAC
		// (set) Token: 0x06007FD4 RID: 32724 RVA: 0x0025CDB4 File Offset: 0x0025CDB4
		public object DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
			set
			{
				if (this.defaultValue != value)
				{
					this.ThrowIfFrozen();
					this.defaultValue = value;
				}
			}
		}

		// Token: 0x06007FD5 RID: 32725 RVA: 0x0025CDD0 File Offset: 0x0025CDD0
		public ValueMember(RuntimeTypeModel model, Type parentType, int fieldNumber, MemberInfo member, Type memberType, Type itemType, Type defaultType, DataFormat dataFormat, object defaultValue) : this(model, fieldNumber, memberType, itemType, defaultType, dataFormat)
		{
			if (parentType == null)
			{
				throw new ArgumentNullException("parentType");
			}
			if (fieldNumber < 1 && !Helpers.IsEnum(parentType))
			{
				throw new ArgumentOutOfRangeException("fieldNumber");
			}
			if (member == null)
			{
				throw new ArgumentNullException("member");
			}
			this.originalMember = member;
			this.parentType = parentType;
			if (fieldNumber < 1 && !Helpers.IsEnum(parentType))
			{
				throw new ArgumentOutOfRangeException("fieldNumber");
			}
			if (defaultValue != null && model.MapType(defaultValue.GetType()) != memberType)
			{
				defaultValue = ValueMember.ParseDefaultValue(memberType, defaultValue);
			}
			this.defaultValue = defaultValue;
			MetaType metaType = model.FindWithoutAdd(memberType);
			if (metaType != null)
			{
				this.AsReference = metaType.AsReferenceDefault;
				return;
			}
			this.AsReference = MetaType.GetAsReferenceDefault(model, memberType);
		}

		// Token: 0x06007FD6 RID: 32726 RVA: 0x0025CEC0 File Offset: 0x0025CEC0
		internal ValueMember(RuntimeTypeModel model, int fieldNumber, Type memberType, Type itemType, Type defaultType, DataFormat dataFormat)
		{
			this._fieldNumber = fieldNumber;
			if (memberType == null)
			{
				throw new ArgumentNullException("memberType");
			}
			this.memberType = memberType;
			this.itemType = itemType;
			this.defaultType = defaultType;
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}
			this.model = model;
			this.dataFormat = dataFormat;
		}

		// Token: 0x06007FD7 RID: 32727 RVA: 0x0025CF28 File Offset: 0x0025CF28
		internal object GetRawEnumValue()
		{
			return ((FieldInfo)this.originalMember).GetRawConstantValue();
		}

		// Token: 0x06007FD8 RID: 32728 RVA: 0x0025CF3C File Offset: 0x0025CF3C
		private static object ParseDefaultValue(Type type, object value)
		{
			Type underlyingType = Helpers.GetUnderlyingType(type);
			if (underlyingType != null)
			{
				type = underlyingType;
			}
			string text = value as string;
			if (text != null)
			{
				if (Helpers.IsEnum(type))
				{
					return Helpers.ParseEnum(type, text);
				}
				ProtoTypeCode typeCode = Helpers.GetTypeCode(type);
				switch (typeCode)
				{
				case ProtoTypeCode.Boolean:
					return bool.Parse(text);
				case ProtoTypeCode.Char:
					if (text.Length == 1)
					{
						return text[0];
					}
					throw new FormatException("Single character expected: \"" + text + "\"");
				case ProtoTypeCode.SByte:
					return sbyte.Parse(text, NumberStyles.Integer, CultureInfo.InvariantCulture);
				case ProtoTypeCode.Byte:
					return byte.Parse(text, NumberStyles.Integer, CultureInfo.InvariantCulture);
				case ProtoTypeCode.Int16:
					return short.Parse(text, NumberStyles.Any, CultureInfo.InvariantCulture);
				case ProtoTypeCode.UInt16:
					return ushort.Parse(text, NumberStyles.Any, CultureInfo.InvariantCulture);
				case ProtoTypeCode.Int32:
					return int.Parse(text, NumberStyles.Any, CultureInfo.InvariantCulture);
				case ProtoTypeCode.UInt32:
					return uint.Parse(text, NumberStyles.Any, CultureInfo.InvariantCulture);
				case ProtoTypeCode.Int64:
					return long.Parse(text, NumberStyles.Any, CultureInfo.InvariantCulture);
				case ProtoTypeCode.UInt64:
					return ulong.Parse(text, NumberStyles.Any, CultureInfo.InvariantCulture);
				case ProtoTypeCode.Single:
					return float.Parse(text, NumberStyles.Any, CultureInfo.InvariantCulture);
				case ProtoTypeCode.Double:
					return double.Parse(text, NumberStyles.Any, CultureInfo.InvariantCulture);
				case ProtoTypeCode.Decimal:
					return decimal.Parse(text, NumberStyles.Any, CultureInfo.InvariantCulture);
				case ProtoTypeCode.DateTime:
					return DateTime.Parse(text, CultureInfo.InvariantCulture);
				case (ProtoTypeCode)17:
					break;
				case ProtoTypeCode.String:
					return text;
				default:
					switch (typeCode)
					{
					case ProtoTypeCode.TimeSpan:
						return TimeSpan.Parse(text);
					case ProtoTypeCode.Guid:
						return new Guid(text);
					case ProtoTypeCode.Uri:
						return text;
					}
					break;
				}
			}
			if (Helpers.IsEnum(type))
			{
				return Enum.ToObject(type, value);
			}
			return Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
		}

		// Token: 0x17001BBF RID: 7103
		// (get) Token: 0x06007FD9 RID: 32729 RVA: 0x0025D164 File Offset: 0x0025D164
		internal IProtoSerializer Serializer
		{
			get
			{
				IProtoSerializer result;
				if ((result = this.serializer) == null)
				{
					result = (this.serializer = this.BuildSerializer());
				}
				return result;
			}
		}

		// Token: 0x17001BC0 RID: 7104
		// (get) Token: 0x06007FDA RID: 32730 RVA: 0x0025D194 File Offset: 0x0025D194
		// (set) Token: 0x06007FDB RID: 32731 RVA: 0x0025D19C File Offset: 0x0025D19C
		public DataFormat DataFormat
		{
			get
			{
				return this.dataFormat;
			}
			set
			{
				if (value != this.dataFormat)
				{
					this.ThrowIfFrozen();
					this.dataFormat = value;
				}
			}
		}

		// Token: 0x17001BC1 RID: 7105
		// (get) Token: 0x06007FDC RID: 32732 RVA: 0x0025D1B8 File Offset: 0x0025D1B8
		// (set) Token: 0x06007FDD RID: 32733 RVA: 0x0025D1C4 File Offset: 0x0025D1C4
		public bool IsStrict
		{
			get
			{
				return this.HasFlag(1);
			}
			set
			{
				this.SetFlag(1, value, true);
			}
		}

		// Token: 0x17001BC2 RID: 7106
		// (get) Token: 0x06007FDE RID: 32734 RVA: 0x0025D1D0 File Offset: 0x0025D1D0
		// (set) Token: 0x06007FDF RID: 32735 RVA: 0x0025D1DC File Offset: 0x0025D1DC
		public bool IsPacked
		{
			get
			{
				return this.HasFlag(2);
			}
			set
			{
				this.SetFlag(2, value, true);
			}
		}

		// Token: 0x17001BC3 RID: 7107
		// (get) Token: 0x06007FE0 RID: 32736 RVA: 0x0025D1E8 File Offset: 0x0025D1E8
		// (set) Token: 0x06007FE1 RID: 32737 RVA: 0x0025D1F4 File Offset: 0x0025D1F4
		public bool OverwriteList
		{
			get
			{
				return this.HasFlag(8);
			}
			set
			{
				this.SetFlag(8, value, true);
			}
		}

		// Token: 0x17001BC4 RID: 7108
		// (get) Token: 0x06007FE2 RID: 32738 RVA: 0x0025D200 File Offset: 0x0025D200
		// (set) Token: 0x06007FE3 RID: 32739 RVA: 0x0025D20C File Offset: 0x0025D20C
		public bool IsRequired
		{
			get
			{
				return this.HasFlag(4);
			}
			set
			{
				this.SetFlag(4, value, true);
			}
		}

		// Token: 0x17001BC5 RID: 7109
		// (get) Token: 0x06007FE4 RID: 32740 RVA: 0x0025D218 File Offset: 0x0025D218
		// (set) Token: 0x06007FE5 RID: 32741 RVA: 0x0025D224 File Offset: 0x0025D224
		public bool AsReference
		{
			get
			{
				return this.HasFlag(32);
			}
			set
			{
				this.SetFlag(32, value, true);
			}
		}

		// Token: 0x17001BC6 RID: 7110
		// (get) Token: 0x06007FE6 RID: 32742 RVA: 0x0025D230 File Offset: 0x0025D230
		// (set) Token: 0x06007FE7 RID: 32743 RVA: 0x0025D240 File Offset: 0x0025D240
		public bool DynamicType
		{
			get
			{
				return this.HasFlag(128);
			}
			set
			{
				this.SetFlag(128, value, true);
			}
		}

		// Token: 0x17001BC7 RID: 7111
		// (get) Token: 0x06007FE8 RID: 32744 RVA: 0x0025D250 File Offset: 0x0025D250
		// (set) Token: 0x06007FE9 RID: 32745 RVA: 0x0025D25C File Offset: 0x0025D25C
		public bool IsMap
		{
			get
			{
				return this.HasFlag(64);
			}
			set
			{
				this.SetFlag(64, value, true);
			}
		}

		// Token: 0x17001BC8 RID: 7112
		// (get) Token: 0x06007FEA RID: 32746 RVA: 0x0025D268 File Offset: 0x0025D268
		// (set) Token: 0x06007FEB RID: 32747 RVA: 0x0025D270 File Offset: 0x0025D270
		public DataFormat MapKeyFormat
		{
			get
			{
				return this.mapKeyFormat;
			}
			set
			{
				if (this.mapKeyFormat != value)
				{
					this.ThrowIfFrozen();
					this.mapKeyFormat = value;
				}
			}
		}

		// Token: 0x17001BC9 RID: 7113
		// (get) Token: 0x06007FEC RID: 32748 RVA: 0x0025D28C File Offset: 0x0025D28C
		// (set) Token: 0x06007FED RID: 32749 RVA: 0x0025D294 File Offset: 0x0025D294
		public DataFormat MapValueFormat
		{
			get
			{
				return this.mapValueFormat;
			}
			set
			{
				if (this.mapValueFormat != value)
				{
					this.ThrowIfFrozen();
					this.mapValueFormat = value;
				}
			}
		}

		// Token: 0x06007FEE RID: 32750 RVA: 0x0025D2B0 File Offset: 0x0025D2B0
		public void SetSpecified(MethodInfo getSpecified, MethodInfo setSpecified)
		{
			if (this.getSpecified != getSpecified || this.setSpecified != setSpecified)
			{
				if (getSpecified != null && (getSpecified.ReturnType != this.model.MapType(typeof(bool)) || getSpecified.IsStatic || getSpecified.GetParameters().Length != 0))
				{
					throw new ArgumentException("Invalid pattern for checking member-specified", "getSpecified");
				}
				ParameterInfo[] parameters;
				if (setSpecified != null && (setSpecified.ReturnType != this.model.MapType(typeof(void)) || setSpecified.IsStatic || (parameters = setSpecified.GetParameters()).Length != 1 || parameters[0].ParameterType != this.model.MapType(typeof(bool))))
				{
					throw new ArgumentException("Invalid pattern for setting member-specified", "setSpecified");
				}
				this.ThrowIfFrozen();
				this.getSpecified = getSpecified;
				this.setSpecified = setSpecified;
			}
		}

		// Token: 0x06007FEF RID: 32751 RVA: 0x0025D3D8 File Offset: 0x0025D3D8
		private void ThrowIfFrozen()
		{
			if (this.serializer != null)
			{
				throw new InvalidOperationException("The type cannot be changed once a serializer has been generated");
			}
		}

		// Token: 0x06007FF0 RID: 32752 RVA: 0x0025D3F0 File Offset: 0x0025D3F0
		internal bool ResolveMapTypes(out Type dictionaryType, out Type keyType, out Type valueType)
		{
			Type type;
			valueType = (type = null);
			keyType = (type = type);
			dictionaryType = type;
			bool result;
			try
			{
				Type type2 = this.memberType;
				MethodInfo methodInfo;
				PropertyInfo propertyInfo;
				PropertyInfo propertyInfo2;
				MethodInfo methodInfo2;
				MethodInfo methodInfo3;
				MethodInfo methodInfo4;
				if (ImmutableCollectionDecorator.IdentifyImmutable(this.model, this.MemberType, out methodInfo, out propertyInfo, out propertyInfo2, out methodInfo2, out methodInfo3, out methodInfo4))
				{
					result = false;
				}
				else if (type2.IsInterface && type2.IsGenericType && type2.GetGenericTypeDefinition() == typeof(IDictionary<, >))
				{
					Type[] genericArguments = this.memberType.GetGenericArguments();
					if (ValueMember.IsValidMapKeyType(genericArguments[0]))
					{
						keyType = genericArguments[0];
						valueType = genericArguments[1];
						dictionaryType = this.memberType;
					}
					result = false;
				}
				else
				{
					foreach (Type type3 in this.memberType.GetInterfaces())
					{
						type2 = type3;
						if (type2.IsGenericType && type2.GetGenericTypeDefinition() == typeof(IDictionary<, >))
						{
							if (dictionaryType != null)
							{
								throw new InvalidOperationException("Multiple dictionary interfaces implemented by type: " + this.memberType.FullName);
							}
							Type[] genericArguments2 = type3.GetGenericArguments();
							if (ValueMember.IsValidMapKeyType(genericArguments2[0]))
							{
								keyType = genericArguments2[0];
								valueType = genericArguments2[1];
								dictionaryType = this.memberType;
							}
						}
					}
					if (dictionaryType == null)
					{
						result = false;
					}
					else
					{
						Type left = null;
						Type type4 = null;
						this.model.ResolveListTypes(valueType, ref left, ref type4);
						if (left != null)
						{
							result = false;
						}
						else
						{
							result = (dictionaryType != null);
						}
					}
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06007FF1 RID: 32753 RVA: 0x0025D5E0 File Offset: 0x0025D5E0
		private static bool IsValidMapKeyType(Type type)
		{
			if (type == null || Helpers.IsEnum(type))
			{
				return false;
			}
			ProtoTypeCode typeCode = Helpers.GetTypeCode(type);
			return typeCode - ProtoTypeCode.Boolean <= 9 || typeCode == ProtoTypeCode.String;
		}

		// Token: 0x06007FF2 RID: 32754 RVA: 0x0025D628 File Offset: 0x0025D628
		private IProtoSerializer BuildSerializer()
		{
			int opaqueToken = 0;
			IProtoSerializer result;
			try
			{
				this.model.TakeLock(ref opaqueToken);
				MemberInfo memberInfo = this.backingMember ?? this.originalMember;
				IProtoSerializer protoSerializer3;
				if (this.IsMap)
				{
					Type type;
					Type type2;
					Type type3;
					this.ResolveMapTypes(out type, out type2, out type3);
					if (type == null)
					{
						throw new InvalidOperationException("Unable to resolve map type for type: " + this.memberType.FullName);
					}
					Type type4 = this.defaultType;
					if (type4 == null && Helpers.IsClass(this.memberType))
					{
						type4 = this.memberType;
					}
					WireType wireType;
					IProtoSerializer protoSerializer = ValueMember.TryGetCoreSerializer(this.model, this.MapKeyFormat, type2, out wireType, false, false, false, false);
					if (!this.AsReference)
					{
						this.AsReference = MetaType.GetAsReferenceDefault(this.model, type3);
					}
					WireType wireType2;
					IProtoSerializer protoSerializer2 = ValueMember.TryGetCoreSerializer(this.model, this.MapValueFormat, type3, out wireType2, this.AsReference, this.DynamicType, false, true);
					ConstructorInfo[] constructors = typeof(MapDecorator<, , >).MakeGenericType(new Type[]
					{
						type,
						type2,
						type3
					}).GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
					if (constructors.Length != 1)
					{
						throw new InvalidOperationException("Unable to resolve MapDecorator constructor");
					}
					protoSerializer3 = (IProtoSerializer)constructors[0].Invoke(new object[]
					{
						this.model,
						type4,
						protoSerializer,
						protoSerializer2,
						this._fieldNumber,
						(this.DataFormat == DataFormat.Group) ? WireType.StartGroup : WireType.String,
						wireType,
						wireType2,
						this.OverwriteList
					});
				}
				else
				{
					Type type5 = this.itemType ?? this.memberType;
					WireType wireType3;
					protoSerializer3 = ValueMember.TryGetCoreSerializer(this.model, this.dataFormat, type5, out wireType3, this.AsReference, this.DynamicType, this.OverwriteList, true);
					if (protoSerializer3 == null)
					{
						throw new InvalidOperationException("No serializer defined for type: " + type5.FullName);
					}
					if (this.itemType != null && this.SupportNull)
					{
						if (this.IsPacked)
						{
							throw new NotSupportedException("Packed encodings cannot support null values");
						}
						protoSerializer3 = new TagDecorator(1, wireType3, this.IsStrict, protoSerializer3);
						protoSerializer3 = new NullDecorator(this.model, protoSerializer3);
						protoSerializer3 = new TagDecorator(this._fieldNumber, WireType.StartGroup, false, protoSerializer3);
					}
					else
					{
						protoSerializer3 = new TagDecorator(this._fieldNumber, wireType3, this.IsStrict, protoSerializer3);
					}
					if (this.itemType != null)
					{
						Type type6 = this.SupportNull ? this.itemType : (Helpers.GetUnderlyingType(this.itemType) ?? this.itemType);
						if (this.memberType.IsArray)
						{
							protoSerializer3 = new ArrayDecorator(this.model, protoSerializer3, this._fieldNumber, this.IsPacked, wireType3, this.memberType, this.OverwriteList, this.SupportNull);
						}
						else
						{
							protoSerializer3 = ListDecorator.Create(this.model, this.memberType, this.defaultType, protoSerializer3, this._fieldNumber, this.IsPacked, wireType3, memberInfo != null && PropertyDecorator.CanWrite(this.model, memberInfo), this.OverwriteList, this.SupportNull);
						}
					}
					else if (this.defaultValue != null && !this.IsRequired && this.getSpecified == null)
					{
						protoSerializer3 = new DefaultValueDecorator(this.model, this.defaultValue, protoSerializer3);
					}
					if (this.memberType == this.model.MapType(typeof(Uri)))
					{
						protoSerializer3 = new UriDecorator(this.model, protoSerializer3);
					}
				}
				if (memberInfo != null)
				{
					PropertyInfo propertyInfo = memberInfo as PropertyInfo;
					if (propertyInfo != null)
					{
						protoSerializer3 = new PropertyDecorator(this.model, this.parentType, propertyInfo, protoSerializer3);
					}
					else
					{
						FieldInfo fieldInfo = memberInfo as FieldInfo;
						if (fieldInfo == null)
						{
							throw new InvalidOperationException();
						}
						protoSerializer3 = new FieldDecorator(this.parentType, fieldInfo, protoSerializer3);
					}
					if (this.getSpecified != null || this.setSpecified != null)
					{
						protoSerializer3 = new MemberSpecifiedDecorator(this.getSpecified, this.setSpecified, protoSerializer3);
					}
				}
				result = protoSerializer3;
			}
			finally
			{
				this.model.ReleaseLock(opaqueToken);
			}
			return result;
		}

		// Token: 0x06007FF3 RID: 32755 RVA: 0x0025DADC File Offset: 0x0025DADC
		private static WireType GetIntWireType(DataFormat format, int width)
		{
			switch (format)
			{
			case DataFormat.Default:
			case DataFormat.TwosComplement:
				return WireType.Variant;
			case DataFormat.ZigZag:
				return WireType.SignedVariant;
			case DataFormat.FixedSize:
				if (width != 32)
				{
					return WireType.Fixed64;
				}
				return WireType.Fixed32;
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06007FF4 RID: 32756 RVA: 0x0025DB10 File Offset: 0x0025DB10
		private static WireType GetDateTimeWireType(DataFormat format)
		{
			switch (format)
			{
			case DataFormat.Default:
			case DataFormat.WellKnown:
				return WireType.String;
			case DataFormat.FixedSize:
				return WireType.Fixed64;
			case DataFormat.Group:
				return WireType.StartGroup;
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06007FF5 RID: 32757 RVA: 0x0025DB40 File Offset: 0x0025DB40
		internal static IProtoSerializer TryGetCoreSerializer(RuntimeTypeModel model, DataFormat dataFormat, Type type, out WireType defaultWireType, bool asReference, bool dynamicType, bool overwriteList, bool allowComplexTypes)
		{
			Type underlyingType = Helpers.GetUnderlyingType(type);
			if (underlyingType != null)
			{
				type = underlyingType;
			}
			if (Helpers.IsEnum(type))
			{
				if (allowComplexTypes && model != null)
				{
					defaultWireType = WireType.Variant;
					return new EnumSerializer(type, model.GetEnumMap(type));
				}
				defaultWireType = WireType.None;
				return null;
			}
			else
			{
				ProtoTypeCode typeCode = Helpers.GetTypeCode(type);
				switch (typeCode)
				{
				case ProtoTypeCode.Boolean:
					defaultWireType = WireType.Variant;
					return new BooleanSerializer(model);
				case ProtoTypeCode.Char:
					defaultWireType = WireType.Variant;
					return new CharSerializer(model);
				case ProtoTypeCode.SByte:
					defaultWireType = ValueMember.GetIntWireType(dataFormat, 32);
					return new SByteSerializer(model);
				case ProtoTypeCode.Byte:
					defaultWireType = ValueMember.GetIntWireType(dataFormat, 32);
					return new ByteSerializer(model);
				case ProtoTypeCode.Int16:
					defaultWireType = ValueMember.GetIntWireType(dataFormat, 32);
					return new Int16Serializer(model);
				case ProtoTypeCode.UInt16:
					defaultWireType = ValueMember.GetIntWireType(dataFormat, 32);
					return new UInt16Serializer(model);
				case ProtoTypeCode.Int32:
					defaultWireType = ValueMember.GetIntWireType(dataFormat, 32);
					return new Int32Serializer(model);
				case ProtoTypeCode.UInt32:
					defaultWireType = ValueMember.GetIntWireType(dataFormat, 32);
					return new UInt32Serializer(model);
				case ProtoTypeCode.Int64:
					defaultWireType = ValueMember.GetIntWireType(dataFormat, 64);
					return new Int64Serializer(model);
				case ProtoTypeCode.UInt64:
					defaultWireType = ValueMember.GetIntWireType(dataFormat, 64);
					return new UInt64Serializer(model);
				case ProtoTypeCode.Single:
					defaultWireType = WireType.Fixed32;
					return new SingleSerializer(model);
				case ProtoTypeCode.Double:
					defaultWireType = WireType.Fixed64;
					return new DoubleSerializer(model);
				case ProtoTypeCode.Decimal:
					defaultWireType = WireType.String;
					return new DecimalSerializer(model);
				case ProtoTypeCode.DateTime:
					defaultWireType = ValueMember.GetDateTimeWireType(dataFormat);
					return new DateTimeSerializer(dataFormat, model);
				case (ProtoTypeCode)17:
					break;
				case ProtoTypeCode.String:
					defaultWireType = WireType.String;
					if (asReference)
					{
						return new NetObjectSerializer(model, model.MapType(typeof(string)), 0, BclHelpers.NetObjectOptions.AsReference);
					}
					return new StringSerializer(model);
				default:
					switch (typeCode)
					{
					case ProtoTypeCode.TimeSpan:
						defaultWireType = ValueMember.GetDateTimeWireType(dataFormat);
						return new TimeSpanSerializer(dataFormat, model);
					case ProtoTypeCode.ByteArray:
						defaultWireType = WireType.String;
						return new BlobSerializer(model, overwriteList);
					case ProtoTypeCode.Guid:
						defaultWireType = ((dataFormat == DataFormat.Group) ? WireType.StartGroup : WireType.String);
						return new GuidSerializer(model);
					case ProtoTypeCode.Uri:
						defaultWireType = WireType.String;
						return new StringSerializer(model);
					case ProtoTypeCode.Type:
						defaultWireType = WireType.String;
						return new SystemTypeSerializer(model);
					}
					break;
				}
				IProtoSerializer protoSerializer = model.AllowParseableTypes ? ParseableSerializer.TryCreate(type, model) : null;
				if (protoSerializer != null)
				{
					defaultWireType = WireType.String;
					return protoSerializer;
				}
				if (allowComplexTypes && model != null)
				{
					int key = model.GetKey(type, false, true);
					MetaType metaType = null;
					if (key >= 0)
					{
						metaType = model[type];
						if (dataFormat == DataFormat.Default && metaType.IsGroup)
						{
							dataFormat = DataFormat.Group;
						}
					}
					if (asReference || dynamicType)
					{
						BclHelpers.NetObjectOptions netObjectOptions = BclHelpers.NetObjectOptions.None;
						if (asReference)
						{
							netObjectOptions |= BclHelpers.NetObjectOptions.AsReference;
						}
						if (dynamicType)
						{
							netObjectOptions |= BclHelpers.NetObjectOptions.DynamicType;
						}
						if (metaType != null)
						{
							if (asReference && Helpers.IsValueType(type))
							{
								string text = "AsReference cannot be used with value-types";
								if (type.Name == "KeyValuePair`2")
								{
									text += "; please see https://stackoverflow.com/q/14436606/23354";
								}
								else
								{
									text = text + ": " + type.FullName;
								}
								throw new InvalidOperationException(text);
							}
							if (asReference && metaType.IsAutoTuple)
							{
								netObjectOptions |= BclHelpers.NetObjectOptions.LateSet;
							}
							if (metaType.UseConstructor)
							{
								netObjectOptions |= BclHelpers.NetObjectOptions.UseConstructor;
							}
						}
						defaultWireType = ((dataFormat == DataFormat.Group) ? WireType.StartGroup : WireType.String);
						return new NetObjectSerializer(model, type, key, netObjectOptions);
					}
					if (key >= 0)
					{
						defaultWireType = ((dataFormat == DataFormat.Group) ? WireType.StartGroup : WireType.String);
						return new SubItemSerializer(type, key, metaType, true);
					}
				}
				defaultWireType = WireType.None;
				return null;
			}
		}

		// Token: 0x06007FF6 RID: 32758 RVA: 0x0025DE9C File Offset: 0x0025DE9C
		internal void SetName(string name)
		{
			if (name != this.name)
			{
				this.ThrowIfFrozen();
				this.name = name;
			}
		}

		// Token: 0x17001BCA RID: 7114
		// (get) Token: 0x06007FF7 RID: 32759 RVA: 0x0025DEBC File Offset: 0x0025DEBC
		// (set) Token: 0x06007FF8 RID: 32760 RVA: 0x0025DEE0 File Offset: 0x0025DEE0
		public string Name
		{
			get
			{
				if (!string.IsNullOrEmpty(this.name))
				{
					return this.name;
				}
				return this.originalMember.Name;
			}
			set
			{
				this.SetName(value);
			}
		}

		// Token: 0x06007FF9 RID: 32761 RVA: 0x0025DEEC File Offset: 0x0025DEEC
		private bool HasFlag(byte flag)
		{
			return (this.flags & flag) == flag;
		}

		// Token: 0x06007FFA RID: 32762 RVA: 0x0025DEFC File Offset: 0x0025DEFC
		private void SetFlag(byte flag, bool value, bool throwIfFrozen)
		{
			if (throwIfFrozen && this.HasFlag(flag) != value)
			{
				this.ThrowIfFrozen();
			}
			if (value)
			{
				this.flags |= flag;
				return;
			}
			this.flags &= ~flag;
		}

		// Token: 0x17001BCB RID: 7115
		// (get) Token: 0x06007FFB RID: 32763 RVA: 0x0025DF4C File Offset: 0x0025DF4C
		// (set) Token: 0x06007FFC RID: 32764 RVA: 0x0025DF58 File Offset: 0x0025DF58
		public bool SupportNull
		{
			get
			{
				return this.HasFlag(16);
			}
			set
			{
				this.SetFlag(16, value, true);
			}
		}

		// Token: 0x06007FFD RID: 32765 RVA: 0x0025DF64 File Offset: 0x0025DF64
		internal string GetSchemaTypeName(bool applyNetObjectProxy, ref RuntimeTypeModel.CommonImports imports)
		{
			Type type = this.ItemType;
			if (type == null)
			{
				type = this.MemberType;
			}
			return this.model.GetSchemaTypeName(type, this.DataFormat, applyNetObjectProxy && this.AsReference, applyNetObjectProxy && this.DynamicType, ref imports);
		}

		// Token: 0x04003CE5 RID: 15589
		private int _fieldNumber;

		// Token: 0x04003CE6 RID: 15590
		private readonly MemberInfo originalMember;

		// Token: 0x04003CE7 RID: 15591
		private MemberInfo backingMember;

		// Token: 0x04003CE8 RID: 15592
		private readonly Type parentType;

		// Token: 0x04003CE9 RID: 15593
		private readonly Type itemType;

		// Token: 0x04003CEA RID: 15594
		private readonly Type defaultType;

		// Token: 0x04003CEB RID: 15595
		private readonly Type memberType;

		// Token: 0x04003CEC RID: 15596
		private object defaultValue;

		// Token: 0x04003CED RID: 15597
		private readonly RuntimeTypeModel model;

		// Token: 0x04003CEE RID: 15598
		private IProtoSerializer serializer;

		// Token: 0x04003CEF RID: 15599
		private DataFormat dataFormat;

		// Token: 0x04003CF0 RID: 15600
		private DataFormat mapKeyFormat;

		// Token: 0x04003CF1 RID: 15601
		private DataFormat mapValueFormat;

		// Token: 0x04003CF2 RID: 15602
		private MethodInfo getSpecified;

		// Token: 0x04003CF3 RID: 15603
		private MethodInfo setSpecified;

		// Token: 0x04003CF4 RID: 15604
		private string name;

		// Token: 0x04003CF5 RID: 15605
		private const byte OPTIONS_IsStrict = 1;

		// Token: 0x04003CF6 RID: 15606
		private const byte OPTIONS_IsPacked = 2;

		// Token: 0x04003CF7 RID: 15607
		private const byte OPTIONS_IsRequired = 4;

		// Token: 0x04003CF8 RID: 15608
		private const byte OPTIONS_OverwriteList = 8;

		// Token: 0x04003CF9 RID: 15609
		private const byte OPTIONS_SupportNull = 16;

		// Token: 0x04003CFA RID: 15610
		private const byte OPTIONS_AsReference = 32;

		// Token: 0x04003CFB RID: 15611
		private const byte OPTIONS_IsMap = 64;

		// Token: 0x04003CFC RID: 15612
		private const byte OPTIONS_DynamicType = 128;

		// Token: 0x04003CFD RID: 15613
		private byte flags;

		// Token: 0x02001188 RID: 4488
		internal sealed class Comparer : IComparer, IComparer<ValueMember>
		{
			// Token: 0x060093AD RID: 37805 RVA: 0x002C380C File Offset: 0x002C380C
			public int Compare(object x, object y)
			{
				return this.Compare(x as ValueMember, y as ValueMember);
			}

			// Token: 0x060093AE RID: 37806 RVA: 0x002C3820 File Offset: 0x002C3820
			public int Compare(ValueMember x, ValueMember y)
			{
				if (x == y)
				{
					return 0;
				}
				if (x == null)
				{
					return -1;
				}
				if (y == null)
				{
					return 1;
				}
				return x.FieldNumber.CompareTo(y.FieldNumber);
			}

			// Token: 0x04004B91 RID: 19345
			public static readonly ValueMember.Comparer Default = new ValueMember.Comparer();
		}
	}
}
