using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using ProtoBuf.Serializers;

namespace ProtoBuf.Meta
{
	// Token: 0x02000C74 RID: 3188
	[ComVisible(true)]
	public class MetaType : ISerializerProxy
	{
		// Token: 0x06007EB4 RID: 32436 RVA: 0x002542C8 File Offset: 0x002542C8
		public override string ToString()
		{
			return this.type.ToString();
		}

		// Token: 0x17001B8B RID: 7051
		// (get) Token: 0x06007EB5 RID: 32437 RVA: 0x002542D8 File Offset: 0x002542D8
		IProtoSerializer ISerializerProxy.Serializer
		{
			get
			{
				return this.Serializer;
			}
		}

		// Token: 0x17001B8C RID: 7052
		// (get) Token: 0x06007EB6 RID: 32438 RVA: 0x002542E0 File Offset: 0x002542E0
		public MetaType BaseType
		{
			get
			{
				return this.baseType;
			}
		}

		// Token: 0x17001B8D RID: 7053
		// (get) Token: 0x06007EB7 RID: 32439 RVA: 0x002542E8 File Offset: 0x002542E8
		internal TypeModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x17001B8E RID: 7054
		// (get) Token: 0x06007EB8 RID: 32440 RVA: 0x002542F0 File Offset: 0x002542F0
		// (set) Token: 0x06007EB9 RID: 32441 RVA: 0x002542FC File Offset: 0x002542FC
		public bool IncludeSerializerMethod
		{
			get
			{
				return !this.HasFlag(8);
			}
			set
			{
				this.SetFlag(8, !value, true);
			}
		}

		// Token: 0x17001B8F RID: 7055
		// (get) Token: 0x06007EBA RID: 32442 RVA: 0x0025430C File Offset: 0x0025430C
		// (set) Token: 0x06007EBB RID: 32443 RVA: 0x00254318 File Offset: 0x00254318
		public bool AsReferenceDefault
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

		// Token: 0x06007EBC RID: 32444 RVA: 0x00254324 File Offset: 0x00254324
		private bool IsValidSubType(Type subType)
		{
			return this.type.IsAssignableFrom(subType);
		}

		// Token: 0x06007EBD RID: 32445 RVA: 0x00254334 File Offset: 0x00254334
		public MetaType AddSubType(int fieldNumber, Type derivedType)
		{
			return this.AddSubType(fieldNumber, derivedType, DataFormat.Default);
		}

		// Token: 0x06007EBE RID: 32446 RVA: 0x00254340 File Offset: 0x00254340
		public MetaType AddSubType(int fieldNumber, Type derivedType, DataFormat dataFormat)
		{
			if (derivedType == null)
			{
				throw new ArgumentNullException("derivedType");
			}
			if (fieldNumber < 1)
			{
				throw new ArgumentOutOfRangeException("fieldNumber");
			}
			if ((!this.type.IsClass && !this.type.IsInterface) || this.type.IsSealed)
			{
				throw new InvalidOperationException("Sub-types can only be added to non-sealed classes");
			}
			if (!this.IsValidSubType(derivedType))
			{
				throw new ArgumentException(derivedType.Name + " is not a valid sub-type of " + this.type.Name, "derivedType");
			}
			MetaType metaType = this.model[derivedType];
			this.ThrowIfFrozen();
			metaType.ThrowIfFrozen();
			SubType value = new SubType(fieldNumber, metaType, dataFormat);
			this.ThrowIfFrozen();
			metaType.SetBaseType(this);
			if (this.subTypes == null)
			{
				this.subTypes = new BasicList();
			}
			this.subTypes.Add(value);
			this.model.ResetKeyCache();
			return this;
		}

		// Token: 0x06007EBF RID: 32447 RVA: 0x00254448 File Offset: 0x00254448
		private void SetBaseType(MetaType baseType)
		{
			if (baseType == null)
			{
				throw new ArgumentNullException("baseType");
			}
			if (this.baseType == baseType)
			{
				return;
			}
			if (this.baseType != null)
			{
				throw new InvalidOperationException("Type '" + this.baseType.Type.FullName + "' can only participate in one inheritance hierarchy");
			}
			for (MetaType metaType = baseType; metaType != null; metaType = metaType.baseType)
			{
				if (metaType == this)
				{
					throw new InvalidOperationException("Cyclic inheritance of '" + this.baseType.Type.FullName + "' is not allowed");
				}
			}
			this.baseType = baseType;
		}

		// Token: 0x17001B90 RID: 7056
		// (get) Token: 0x06007EC0 RID: 32448 RVA: 0x002544EC File Offset: 0x002544EC
		public bool HasCallbacks
		{
			get
			{
				return this.callbacks != null && this.callbacks.NonTrivial;
			}
		}

		// Token: 0x17001B91 RID: 7057
		// (get) Token: 0x06007EC1 RID: 32449 RVA: 0x00254508 File Offset: 0x00254508
		public bool HasSubtypes
		{
			get
			{
				return this.subTypes != null && this.subTypes.Count != 0;
			}
		}

		// Token: 0x17001B92 RID: 7058
		// (get) Token: 0x06007EC2 RID: 32450 RVA: 0x00254528 File Offset: 0x00254528
		public CallbackSet Callbacks
		{
			get
			{
				if (this.callbacks == null)
				{
					this.callbacks = new CallbackSet(this);
				}
				return this.callbacks;
			}
		}

		// Token: 0x17001B93 RID: 7059
		// (get) Token: 0x06007EC3 RID: 32451 RVA: 0x00254548 File Offset: 0x00254548
		private bool IsValueType
		{
			get
			{
				return this.type.IsValueType;
			}
		}

		// Token: 0x06007EC4 RID: 32452 RVA: 0x00254558 File Offset: 0x00254558
		public MetaType SetCallbacks(MethodInfo beforeSerialize, MethodInfo afterSerialize, MethodInfo beforeDeserialize, MethodInfo afterDeserialize)
		{
			CallbackSet callbackSet = this.Callbacks;
			callbackSet.BeforeSerialize = beforeSerialize;
			callbackSet.AfterSerialize = afterSerialize;
			callbackSet.BeforeDeserialize = beforeDeserialize;
			callbackSet.AfterDeserialize = afterDeserialize;
			return this;
		}

		// Token: 0x06007EC5 RID: 32453 RVA: 0x00254590 File Offset: 0x00254590
		public MetaType SetCallbacks(string beforeSerialize, string afterSerialize, string beforeDeserialize, string afterDeserialize)
		{
			if (this.IsValueType)
			{
				throw new InvalidOperationException();
			}
			CallbackSet callbackSet = this.Callbacks;
			callbackSet.BeforeSerialize = this.ResolveMethod(beforeSerialize, true);
			callbackSet.AfterSerialize = this.ResolveMethod(afterSerialize, true);
			callbackSet.BeforeDeserialize = this.ResolveMethod(beforeDeserialize, true);
			callbackSet.AfterDeserialize = this.ResolveMethod(afterDeserialize, true);
			return this;
		}

		// Token: 0x06007EC6 RID: 32454 RVA: 0x002545F4 File Offset: 0x002545F4
		internal string GetSchemaTypeName()
		{
			if (this.surrogate != null)
			{
				return this.model[this.surrogate].GetSchemaTypeName();
			}
			if (!string.IsNullOrEmpty(this.name))
			{
				return this.name;
			}
			string text = this.type.Name;
			if (this.type.IsGenericType)
			{
				StringBuilder stringBuilder = new StringBuilder(text);
				int num = text.IndexOf('`');
				if (num >= 0)
				{
					stringBuilder.Length = num;
				}
				foreach (Type type in this.type.GetGenericArguments())
				{
					stringBuilder.Append('_');
					Type type2 = type;
					int key = this.model.GetKey(ref type2);
					MetaType metaType;
					if (key >= 0 && (metaType = this.model[type2]) != null && metaType.surrogate == null)
					{
						stringBuilder.Append(metaType.GetSchemaTypeName());
					}
					else
					{
						stringBuilder.Append(type2.Name);
					}
				}
				return stringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x17001B94 RID: 7060
		// (get) Token: 0x06007EC7 RID: 32455 RVA: 0x00254720 File Offset: 0x00254720
		// (set) Token: 0x06007EC8 RID: 32456 RVA: 0x00254728 File Offset: 0x00254728
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.ThrowIfFrozen();
				this.name = value;
			}
		}

		// Token: 0x06007EC9 RID: 32457 RVA: 0x00254738 File Offset: 0x00254738
		public MetaType SetFactory(MethodInfo factory)
		{
			this.model.VerifyFactory(factory, this.type);
			this.ThrowIfFrozen();
			this.factory = factory;
			return this;
		}

		// Token: 0x06007ECA RID: 32458 RVA: 0x0025475C File Offset: 0x0025475C
		public MetaType SetFactory(string factory)
		{
			return this.SetFactory(this.ResolveMethod(factory, false));
		}

		// Token: 0x06007ECB RID: 32459 RVA: 0x0025476C File Offset: 0x0025476C
		private MethodInfo ResolveMethod(string name, bool instance)
		{
			if (string.IsNullOrEmpty(name))
			{
				return null;
			}
			if (!instance)
			{
				return Helpers.GetStaticMethod(this.type, name);
			}
			return Helpers.GetInstanceMethod(this.type, name);
		}

		// Token: 0x06007ECC RID: 32460 RVA: 0x0025479C File Offset: 0x0025479C
		internal static Exception InbuiltType(Type type)
		{
			return new ArgumentException("Data of this type has inbuilt behaviour, and cannot be added to a model in this way: " + type.FullName);
		}

		// Token: 0x06007ECD RID: 32461 RVA: 0x002547B4 File Offset: 0x002547B4
		internal MetaType(RuntimeTypeModel model, Type type, MethodInfo factory)
		{
			this.factory = factory;
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (type.IsArray)
			{
				throw MetaType.InbuiltType(type);
			}
			IProtoSerializer protoSerializer = model.TryGetBasicTypeSerializer(type);
			if (protoSerializer != null)
			{
				throw MetaType.InbuiltType(type);
			}
			this.type = type;
			this.model = model;
			if (Helpers.IsEnum(type))
			{
				this.EnumPassthru = type.IsDefined(model.MapType(typeof(FlagsAttribute)), false);
			}
		}

		// Token: 0x06007ECE RID: 32462 RVA: 0x00254864 File Offset: 0x00254864
		protected internal void ThrowIfFrozen()
		{
			if ((this.flags & 4) != 0)
			{
				throw new InvalidOperationException("The type cannot be changed once a serializer has been generated for " + this.type.FullName);
			}
		}

		// Token: 0x17001B95 RID: 7061
		// (get) Token: 0x06007ECF RID: 32463 RVA: 0x00254890 File Offset: 0x00254890
		public Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17001B96 RID: 7062
		// (get) Token: 0x06007ED0 RID: 32464 RVA: 0x00254898 File Offset: 0x00254898
		internal IProtoTypeSerializer Serializer
		{
			get
			{
				if (this.serializer == null)
				{
					int opaqueToken = 0;
					try
					{
						this.model.TakeLock(ref opaqueToken);
						if (this.serializer == null)
						{
							this.SetFlag(4, true, false);
							this.serializer = this.BuildSerializer();
							if (this.model.AutoCompile)
							{
								this.CompileInPlace();
							}
						}
					}
					finally
					{
						this.model.ReleaseLock(opaqueToken);
					}
				}
				return this.serializer;
			}
		}

		// Token: 0x17001B97 RID: 7063
		// (get) Token: 0x06007ED1 RID: 32465 RVA: 0x00254920 File Offset: 0x00254920
		internal bool IsList
		{
			get
			{
				Type left = this.IgnoreListHandling ? null : TypeModel.GetListItemType(this.model, this.type);
				return left != null;
			}
		}

		// Token: 0x06007ED2 RID: 32466 RVA: 0x0025495C File Offset: 0x0025495C
		private IProtoTypeSerializer BuildSerializer()
		{
			if (Helpers.IsEnum(this.type))
			{
				return new TagDecorator(1, WireType.Variant, false, new EnumSerializer(this.type, this.GetEnumMap()));
			}
			Type type = this.IgnoreListHandling ? null : TypeModel.GetListItemType(this.model, this.type);
			if (type != null)
			{
				if (this.surrogate != null)
				{
					throw new ArgumentException("Repeated data (a list, collection, etc) has inbuilt behaviour and cannot use a surrogate");
				}
				if (this.subTypes != null && this.subTypes.Count != 0)
				{
					throw new ArgumentException("Repeated data (a list, collection, etc) has inbuilt behaviour and cannot be subclassed");
				}
				Type defaultType = null;
				MetaType.ResolveListTypes(this.model, this.type, ref type, ref defaultType);
				ValueMember valueMember = new ValueMember(this.model, 1, this.type, type, defaultType, DataFormat.Default);
				return new TypeSerializer(this.model, this.type, new int[]
				{
					1
				}, new IProtoSerializer[]
				{
					valueMember.Serializer
				}, null, true, true, null, this.constructType, this.factory);
			}
			else
			{
				if (this.surrogate != null)
				{
					MetaType metaType = this.model[this.surrogate];
					MetaType metaType2;
					while ((metaType2 = metaType.baseType) != null)
					{
						metaType = metaType2;
					}
					return new SurrogateSerializer(this.model, this.type, this.surrogate, metaType.Serializer);
				}
				if (!this.IsAutoTuple)
				{
					this.fields.Trim();
					int count = this.fields.Count;
					int num = (this.subTypes == null) ? 0 : this.subTypes.Count;
					int[] array = new int[count + num];
					IProtoSerializer[] array2 = new IProtoSerializer[count + num];
					int num2 = 0;
					if (num != 0)
					{
						foreach (object obj in this.subTypes)
						{
							SubType subType = (SubType)obj;
							if (!subType.DerivedType.IgnoreListHandling && this.model.MapType(MetaType.ienumerable).IsAssignableFrom(subType.DerivedType.Type))
							{
								throw new ArgumentException("Repeated data (a list, collection, etc) has inbuilt behaviour and cannot be used as a subclass");
							}
							array[num2] = subType.FieldNumber;
							array2[num2++] = subType.Serializer;
						}
					}
					if (count != 0)
					{
						foreach (object obj2 in this.fields)
						{
							ValueMember valueMember2 = (ValueMember)obj2;
							array[num2] = valueMember2.FieldNumber;
							array2[num2++] = valueMember2.Serializer;
						}
					}
					BasicList basicList = null;
					for (MetaType metaType3 = this.BaseType; metaType3 != null; metaType3 = metaType3.BaseType)
					{
						MethodInfo methodInfo = metaType3.HasCallbacks ? metaType3.Callbacks.BeforeDeserialize : null;
						if (methodInfo != null)
						{
							if (basicList == null)
							{
								basicList = new BasicList();
							}
							basicList.Add(methodInfo);
						}
					}
					MethodInfo[] array3 = null;
					if (basicList != null)
					{
						array3 = new MethodInfo[basicList.Count];
						basicList.CopyTo(array3, 0);
						Array.Reverse(array3);
					}
					return new TypeSerializer(this.model, this.type, array, array2, array3, this.baseType == null, this.UseConstructor, this.callbacks, this.constructType, this.factory);
				}
				MemberInfo[] members;
				ConstructorInfo constructorInfo = MetaType.ResolveTupleConstructor(this.type, out members);
				if (constructorInfo == null)
				{
					throw new InvalidOperationException();
				}
				return new TupleSerializer(this.model, constructorInfo, members);
			}
		}

		// Token: 0x06007ED3 RID: 32467 RVA: 0x00254CF4 File Offset: 0x00254CF4
		private static Type GetBaseType(MetaType type)
		{
			return type.type.BaseType;
		}

		// Token: 0x06007ED4 RID: 32468 RVA: 0x00254D04 File Offset: 0x00254D04
		internal static bool GetAsReferenceDefault(RuntimeTypeModel model, Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (Helpers.IsEnum(type))
			{
				return false;
			}
			AttributeMap[] array = AttributeMap.Create(model, type, false);
			for (int i = 0; i < array.Length; i++)
			{
				object obj;
				if (array[i].AttributeType.FullName == "ProtoBuf.ProtoContractAttribute" && array[i].TryGet("AsReferenceDefault", out obj))
				{
					return (bool)obj;
				}
			}
			return false;
		}

		// Token: 0x06007ED5 RID: 32469 RVA: 0x00254D94 File Offset: 0x00254D94
		internal void ApplyDefaultBehaviour()
		{
			TypeAddedEventArgs typeAddedEventArgs = null;
			RuntimeTypeModel.OnBeforeApplyDefaultBehaviour(this, ref typeAddedEventArgs);
			if (typeAddedEventArgs == null || typeAddedEventArgs.ApplyDefaultBehaviour)
			{
				this.ApplyDefaultBehaviourImpl();
			}
			RuntimeTypeModel.OnAfterApplyDefaultBehaviour(this, ref typeAddedEventArgs);
		}

		// Token: 0x06007ED6 RID: 32470 RVA: 0x00254DD0 File Offset: 0x00254DD0
		internal void ApplyDefaultBehaviourImpl()
		{
			Type left = MetaType.GetBaseType(this);
			if (left != null && this.model.FindWithoutAdd(left) == null && MetaType.GetContractFamily(this.model, left, null) != MetaType.AttributeFamily.None)
			{
				this.model.FindOrAddAuto(left, true, false, false);
			}
			AttributeMap[] array = AttributeMap.Create(this.model, this.type, false);
			MetaType.AttributeFamily attributeFamily = MetaType.GetContractFamily(this.model, this.type, array);
			if (attributeFamily == MetaType.AttributeFamily.AutoTuple)
			{
				this.SetFlag(64, true, true);
			}
			bool flag = !this.EnumPassthru && Helpers.IsEnum(this.type);
			if (attributeFamily == MetaType.AttributeFamily.None && !flag)
			{
				return;
			}
			bool flag2 = flag;
			BasicList basicList = null;
			BasicList basicList2 = null;
			int dataMemberOffset = 0;
			int num = 1;
			bool flag3 = this.model.InferTagFromNameDefault;
			ImplicitFields implicitFields = ImplicitFields.None;
			string text = null;
			foreach (AttributeMap attributeMap in array)
			{
				string fullName = attributeMap.AttributeType.FullName;
				object obj;
				if (!flag && fullName == "ProtoBuf.ProtoIncludeAttribute")
				{
					int fieldNumber = 0;
					if (attributeMap.TryGet("tag", out obj))
					{
						fieldNumber = (int)obj;
					}
					DataFormat dataFormat = DataFormat.Default;
					if (attributeMap.TryGet("DataFormat", out obj))
					{
						dataFormat = (DataFormat)((int)obj);
					}
					Type type = null;
					try
					{
						if (attributeMap.TryGet("knownTypeName", out obj))
						{
							type = this.model.GetType((string)obj, this.type.Assembly);
						}
						else if (attributeMap.TryGet("knownType", out obj))
						{
							type = (Type)obj;
						}
					}
					catch (Exception innerException)
					{
						throw new InvalidOperationException("Unable to resolve sub-type of: " + this.type.FullName, innerException);
					}
					if (type == null)
					{
						throw new InvalidOperationException("Unable to resolve sub-type of: " + this.type.FullName);
					}
					if (this.IsValidSubType(type))
					{
						this.AddSubType(fieldNumber, type, dataFormat);
					}
				}
				if (fullName == "ProtoBuf.ProtoPartialIgnoreAttribute" && attributeMap.TryGet("MemberName", out obj) && obj != null)
				{
					if (basicList == null)
					{
						basicList = new BasicList();
					}
					basicList.Add((string)obj);
				}
				if (!flag && fullName == "ProtoBuf.ProtoPartialMemberAttribute")
				{
					if (basicList2 == null)
					{
						basicList2 = new BasicList();
					}
					basicList2.Add(attributeMap);
				}
				if (fullName == "ProtoBuf.ProtoContractAttribute")
				{
					if (attributeMap.TryGet("Name", out obj))
					{
						text = (string)obj;
					}
					if (Helpers.IsEnum(this.type))
					{
						if (attributeMap.TryGet("EnumPassthruHasValue", false, out obj) && (bool)obj && attributeMap.TryGet("EnumPassthru", out obj))
						{
							this.EnumPassthru = (bool)obj;
							flag2 = false;
							if (this.EnumPassthru)
							{
								flag = false;
							}
						}
					}
					else
					{
						if (attributeMap.TryGet("DataMemberOffset", out obj))
						{
							dataMemberOffset = (int)obj;
						}
						if (attributeMap.TryGet("InferTagFromNameHasValue", false, out obj) && (bool)obj && attributeMap.TryGet("InferTagFromName", out obj))
						{
							flag3 = (bool)obj;
						}
						if (attributeMap.TryGet("ImplicitFields", out obj) && obj != null)
						{
							implicitFields = (ImplicitFields)((int)obj);
						}
						if (attributeMap.TryGet("SkipConstructor", out obj))
						{
							this.UseConstructor = !(bool)obj;
						}
						if (attributeMap.TryGet("IgnoreListHandling", out obj))
						{
							this.IgnoreListHandling = (bool)obj;
						}
						if (attributeMap.TryGet("AsReferenceDefault", out obj))
						{
							this.AsReferenceDefault = (bool)obj;
						}
						if (attributeMap.TryGet("ImplicitFirstTag", out obj) && (int)obj > 0)
						{
							num = (int)obj;
						}
						if (attributeMap.TryGet("IsGroup", out obj))
						{
							this.IsGroup = (bool)obj;
						}
						if (attributeMap.TryGet("Surrogate", out obj))
						{
							this.SetSurrogate((Type)obj);
						}
					}
				}
				if (fullName == "System.Runtime.Serialization.DataContractAttribute" && text == null && attributeMap.TryGet("Name", out obj))
				{
					text = (string)obj;
				}
				if (fullName == "System.Xml.Serialization.XmlTypeAttribute" && text == null && attributeMap.TryGet("TypeName", out obj))
				{
					text = (string)obj;
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				this.Name = text;
			}
			if (implicitFields != ImplicitFields.None)
			{
				attributeFamily &= MetaType.AttributeFamily.ProtoBuf;
			}
			MethodInfo[] array2 = null;
			BasicList basicList3 = new BasicList();
			MemberInfo[] members = this.type.GetMembers(flag ? (BindingFlags.Static | BindingFlags.Public) : (BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic));
			bool flag4 = false;
			foreach (MemberInfo memberInfo in members)
			{
				if (!(memberInfo.DeclaringType != this.type) && !memberInfo.IsDefined(this.model.MapType(typeof(ProtoIgnoreAttribute)), true) && (basicList == null || !basicList.Contains(memberInfo.Name)))
				{
					bool flag5 = false;
					PropertyInfo propertyInfo = memberInfo as PropertyInfo;
					if (propertyInfo != null)
					{
						if (!flag)
						{
							MemberInfo backingMember = null;
							if (!propertyInfo.CanWrite)
							{
								string b = "<" + propertyInfo.Name + ">k__BackingField";
								foreach (MemberInfo memberInfo2 in members)
								{
									if (memberInfo2 as FieldInfo != null && memberInfo2.Name == b)
									{
										backingMember = memberInfo2;
										break;
									}
								}
							}
							Type type2 = propertyInfo.PropertyType;
							bool isPublic = Helpers.GetGetMethod(propertyInfo, false, false) != null;
							bool isField = false;
							MetaType.ApplyDefaultBehaviour_AddMembers(this.model, attributeFamily, flag, basicList2, dataMemberOffset, flag3, implicitFields, basicList3, memberInfo, ref flag5, isPublic, isField, ref type2, ref flag4, backingMember);
						}
					}
					else
					{
						FieldInfo fieldInfo = memberInfo as FieldInfo;
						if (fieldInfo != null)
						{
							Type type2 = fieldInfo.FieldType;
							bool isPublic = fieldInfo.IsPublic;
							bool isField = true;
							if (!flag || fieldInfo.IsStatic)
							{
								MetaType.ApplyDefaultBehaviour_AddMembers(this.model, attributeFamily, flag, basicList2, dataMemberOffset, flag3, implicitFields, basicList3, memberInfo, ref flag5, isPublic, isField, ref type2, ref flag4, null);
							}
						}
						else
						{
							MethodInfo methodInfo = memberInfo as MethodInfo;
							if (methodInfo != null && !flag)
							{
								AttributeMap[] array5 = AttributeMap.Create(this.model, methodInfo, false);
								if (array5 != null && array5.Length != 0)
								{
									MetaType.CheckForCallback(methodInfo, array5, "ProtoBuf.ProtoBeforeSerializationAttribute", ref array2, 0);
									MetaType.CheckForCallback(methodInfo, array5, "ProtoBuf.ProtoAfterSerializationAttribute", ref array2, 1);
									MetaType.CheckForCallback(methodInfo, array5, "ProtoBuf.ProtoBeforeDeserializationAttribute", ref array2, 2);
									MetaType.CheckForCallback(methodInfo, array5, "ProtoBuf.ProtoAfterDeserializationAttribute", ref array2, 3);
									MetaType.CheckForCallback(methodInfo, array5, "System.Runtime.Serialization.OnSerializingAttribute", ref array2, 4);
									MetaType.CheckForCallback(methodInfo, array5, "System.Runtime.Serialization.OnSerializedAttribute", ref array2, 5);
									MetaType.CheckForCallback(methodInfo, array5, "System.Runtime.Serialization.OnDeserializingAttribute", ref array2, 6);
									MetaType.CheckForCallback(methodInfo, array5, "System.Runtime.Serialization.OnDeserializedAttribute", ref array2, 7);
								}
							}
						}
					}
				}
			}
			if (flag && flag2 && !flag4)
			{
				this.EnumPassthru = true;
			}
			ProtoMemberAttribute[] array6 = new ProtoMemberAttribute[basicList3.Count];
			basicList3.CopyTo(array6, 0);
			if (flag3 || implicitFields != ImplicitFields.None)
			{
				Array.Sort<ProtoMemberAttribute>(array6);
				int num2 = num;
				foreach (ProtoMemberAttribute protoMemberAttribute in array6)
				{
					if (!protoMemberAttribute.TagIsPinned)
					{
						protoMemberAttribute.Rebase(num2++);
					}
				}
			}
			foreach (ProtoMemberAttribute normalizedAttribute in array6)
			{
				ValueMember valueMember = this.ApplyDefaultBehaviour(flag, normalizedAttribute);
				if (valueMember != null)
				{
					this.Add(valueMember);
				}
			}
			if (array2 != null)
			{
				this.SetCallbacks(MetaType.Coalesce(array2, 0, 4), MetaType.Coalesce(array2, 1, 5), MetaType.Coalesce(array2, 2, 6), MetaType.Coalesce(array2, 3, 7));
			}
		}

		// Token: 0x06007ED7 RID: 32471 RVA: 0x00255664 File Offset: 0x00255664
		private static void ApplyDefaultBehaviour_AddMembers(TypeModel model, MetaType.AttributeFamily family, bool isEnum, BasicList partialMembers, int dataMemberOffset, bool inferTagByName, ImplicitFields implicitMode, BasicList members, MemberInfo member, ref bool forced, bool isPublic, bool isField, ref Type effectiveType, ref bool hasConflictingEnumValue, MemberInfo backingMember = null)
		{
			if (implicitMode != ImplicitFields.AllPublic)
			{
				if (implicitMode == ImplicitFields.AllFields && isField)
				{
					forced = true;
				}
			}
			else if (isPublic)
			{
				forced = true;
			}
			if (effectiveType.IsSubclassOf(model.MapType(typeof(Delegate))))
			{
				effectiveType = null;
			}
			if (effectiveType != null)
			{
				ProtoMemberAttribute protoMemberAttribute = MetaType.NormalizeProtoMember(model, member, family, forced, isEnum, partialMembers, dataMemberOffset, inferTagByName, ref hasConflictingEnumValue, backingMember);
				if (protoMemberAttribute != null)
				{
					members.Add(protoMemberAttribute);
				}
			}
		}

		// Token: 0x06007ED8 RID: 32472 RVA: 0x002556F8 File Offset: 0x002556F8
		private static MethodInfo Coalesce(MethodInfo[] arr, int x, int y)
		{
			MethodInfo methodInfo = arr[x];
			if (methodInfo == null)
			{
				methodInfo = arr[y];
			}
			return methodInfo;
		}

		// Token: 0x06007ED9 RID: 32473 RVA: 0x00255728 File Offset: 0x00255728
		internal static MetaType.AttributeFamily GetContractFamily(RuntimeTypeModel model, Type type, AttributeMap[] attributes)
		{
			MetaType.AttributeFamily attributeFamily = MetaType.AttributeFamily.None;
			if (attributes == null)
			{
				attributes = AttributeMap.Create(model, type, false);
			}
			for (int i = 0; i < attributes.Length; i++)
			{
				string fullName = attributes[i].AttributeType.FullName;
				if (fullName != null)
				{
					if (!(fullName == "ProtoBuf.ProtoContractAttribute"))
					{
						if (!(fullName == "System.Xml.Serialization.XmlTypeAttribute"))
						{
							if (fullName == "System.Runtime.Serialization.DataContractAttribute")
							{
								if (!model.AutoAddProtoContractTypesOnly)
								{
									attributeFamily |= MetaType.AttributeFamily.DataContractSerialier;
								}
							}
						}
						else if (!model.AutoAddProtoContractTypesOnly)
						{
							attributeFamily |= MetaType.AttributeFamily.XmlSerializer;
						}
					}
					else
					{
						bool flag = false;
						MetaType.GetFieldBoolean(ref flag, attributes[i], "UseProtoMembersOnly");
						if (flag)
						{
							return MetaType.AttributeFamily.ProtoBuf;
						}
						attributeFamily |= MetaType.AttributeFamily.ProtoBuf;
					}
				}
			}
			MemberInfo[] array;
			if (attributeFamily == MetaType.AttributeFamily.None && MetaType.ResolveTupleConstructor(type, out array) != null)
			{
				attributeFamily |= MetaType.AttributeFamily.AutoTuple;
			}
			return attributeFamily;
		}

		// Token: 0x06007EDA RID: 32474 RVA: 0x00255814 File Offset: 0x00255814
		internal static ConstructorInfo ResolveTupleConstructor(Type type, out MemberInfo[] mappedMembers)
		{
			mappedMembers = null;
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (type.IsAbstract)
			{
				return null;
			}
			ConstructorInfo[] constructors = Helpers.GetConstructors(type, false);
			if (constructors.Length == 0 || (constructors.Length == 1 && constructors[0].GetParameters().Length == 0))
			{
				return null;
			}
			MemberInfo[] instanceFieldsAndProperties = Helpers.GetInstanceFieldsAndProperties(type, true);
			BasicList basicList = new BasicList();
			bool flag = type.Name.IndexOf("Tuple", StringComparison.OrdinalIgnoreCase) < 0;
			for (int i = 0; i < instanceFieldsAndProperties.Length; i++)
			{
				PropertyInfo propertyInfo = instanceFieldsAndProperties[i] as PropertyInfo;
				if (propertyInfo != null)
				{
					if (!propertyInfo.CanRead)
					{
						return null;
					}
					if (flag && propertyInfo.CanWrite && Helpers.GetSetMethod(propertyInfo, false, false) != null)
					{
						return null;
					}
					basicList.Add(propertyInfo);
				}
				else
				{
					FieldInfo fieldInfo = instanceFieldsAndProperties[i] as FieldInfo;
					if (fieldInfo != null)
					{
						if (flag && !fieldInfo.IsInitOnly)
						{
							return null;
						}
						basicList.Add(fieldInfo);
					}
				}
			}
			if (basicList.Count == 0)
			{
				return null;
			}
			MemberInfo[] array = new MemberInfo[basicList.Count];
			basicList.CopyTo(array, 0);
			int[] array2 = new int[array.Length];
			int num = 0;
			ConstructorInfo result = null;
			mappedMembers = new MemberInfo[array2.Length];
			for (int j = 0; j < constructors.Length; j++)
			{
				ParameterInfo[] parameters = constructors[j].GetParameters();
				if (parameters.Length == array.Length)
				{
					for (int k = 0; k < array2.Length; k++)
					{
						array2[k] = -1;
					}
					for (int l = 0; l < parameters.Length; l++)
					{
						for (int m = 0; m < array.Length; m++)
						{
							if (string.Compare(parameters[l].Name, array[m].Name, StringComparison.OrdinalIgnoreCase) == 0)
							{
								Type memberType = Helpers.GetMemberType(array[m]);
								if (!(memberType != parameters[l].ParameterType))
								{
									array2[l] = m;
								}
							}
						}
					}
					bool flag2 = false;
					for (int n = 0; n < array2.Length; n++)
					{
						if (array2[n] < 0)
						{
							flag2 = true;
							break;
						}
						mappedMembers[n] = array[array2[n]];
					}
					if (!flag2)
					{
						num++;
						result = constructors[j];
					}
				}
			}
			if (num != 1)
			{
				return null;
			}
			return result;
		}

		// Token: 0x06007EDB RID: 32475 RVA: 0x00255AB4 File Offset: 0x00255AB4
		private static void CheckForCallback(MethodInfo method, AttributeMap[] attributes, string callbackTypeName, ref MethodInfo[] callbacks, int index)
		{
			for (int i = 0; i < attributes.Length; i++)
			{
				if (attributes[i].AttributeType.FullName == callbackTypeName)
				{
					if (callbacks == null)
					{
						callbacks = new MethodInfo[8];
					}
					else if (callbacks[index] != null)
					{
						Type reflectedType = method.ReflectedType;
						throw new ProtoException("Duplicate " + callbackTypeName + " callbacks on " + reflectedType.FullName);
					}
					callbacks[index] = method;
				}
			}
		}

		// Token: 0x06007EDC RID: 32476 RVA: 0x00255B40 File Offset: 0x00255B40
		private static bool HasFamily(MetaType.AttributeFamily value, MetaType.AttributeFamily required)
		{
			return (value & required) == required;
		}

		// Token: 0x06007EDD RID: 32477 RVA: 0x00255B48 File Offset: 0x00255B48
		private static ProtoMemberAttribute NormalizeProtoMember(TypeModel model, MemberInfo member, MetaType.AttributeFamily family, bool forced, bool isEnum, BasicList partialMembers, int dataMemberOffset, bool inferByTagName, ref bool hasConflictingEnumValue, MemberInfo backingMember = null)
		{
			if (member == null || (family == MetaType.AttributeFamily.None && !isEnum))
			{
				return null;
			}
			int num = int.MinValue;
			int num2 = inferByTagName ? -1 : 1;
			string text = null;
			bool isPacked = false;
			bool flag = false;
			bool flag2 = false;
			bool isRequired = false;
			bool asReference = false;
			bool flag3 = false;
			bool dynamicType = false;
			bool tagIsPinned = false;
			bool overwriteList = false;
			DataFormat dataFormat = DataFormat.Default;
			if (isEnum)
			{
				forced = true;
			}
			AttributeMap[] attribs = AttributeMap.Create(model, member, true);
			if (isEnum)
			{
				AttributeMap attribute = MetaType.GetAttribute(attribs, "ProtoBuf.ProtoIgnoreAttribute");
				if (attribute != null)
				{
					flag = true;
				}
				else
				{
					attribute = MetaType.GetAttribute(attribs, "ProtoBuf.ProtoEnumAttribute");
					num = Convert.ToInt32(((FieldInfo)member).GetRawConstantValue());
					if (attribute != null)
					{
						MetaType.GetFieldName(ref text, attribute, "Name");
						object obj;
						if ((bool)Helpers.GetInstanceMethod(attribute.AttributeType, "HasValue").Invoke(attribute.Target, null) && attribute.TryGet("Value", out obj))
						{
							if (num != (int)obj)
							{
								hasConflictingEnumValue = true;
							}
							num = (int)obj;
						}
					}
				}
				flag2 = true;
			}
			if (!flag && !flag2)
			{
				AttributeMap attribute = MetaType.GetAttribute(attribs, "ProtoBuf.ProtoMemberAttribute");
				MetaType.GetIgnore(ref flag, attribute, attribs, "ProtoBuf.ProtoIgnoreAttribute");
				if (!flag && attribute != null)
				{
					MetaType.GetFieldNumber(ref num, attribute, "Tag");
					MetaType.GetFieldName(ref text, attribute, "Name");
					MetaType.GetFieldBoolean(ref isRequired, attribute, "IsRequired");
					MetaType.GetFieldBoolean(ref isPacked, attribute, "IsPacked");
					MetaType.GetFieldBoolean(ref overwriteList, attribute, "OverwriteList");
					MetaType.GetDataFormat(ref dataFormat, attribute, "DataFormat");
					MetaType.GetFieldBoolean(ref flag3, attribute, "AsReferenceHasValue", false);
					if (flag3)
					{
						flag3 = MetaType.GetFieldBoolean(ref asReference, attribute, "AsReference", true);
					}
					MetaType.GetFieldBoolean(ref dynamicType, attribute, "DynamicType");
					tagIsPinned = (flag2 = (num > 0));
				}
				if (!flag2 && partialMembers != null)
				{
					foreach (object obj2 in partialMembers)
					{
						AttributeMap attributeMap = (AttributeMap)obj2;
						object obj3;
						if (attributeMap.TryGet("MemberName", out obj3) && (string)obj3 == member.Name)
						{
							MetaType.GetFieldNumber(ref num, attributeMap, "Tag");
							MetaType.GetFieldName(ref text, attributeMap, "Name");
							MetaType.GetFieldBoolean(ref isRequired, attributeMap, "IsRequired");
							MetaType.GetFieldBoolean(ref isPacked, attributeMap, "IsPacked");
							MetaType.GetFieldBoolean(ref overwriteList, attribute, "OverwriteList");
							MetaType.GetDataFormat(ref dataFormat, attributeMap, "DataFormat");
							MetaType.GetFieldBoolean(ref flag3, attribute, "AsReferenceHasValue", false);
							if (flag3)
							{
								flag3 = MetaType.GetFieldBoolean(ref asReference, attributeMap, "AsReference", true);
							}
							MetaType.GetFieldBoolean(ref dynamicType, attributeMap, "DynamicType");
							if (flag2 = (tagIsPinned = (num > 0)))
							{
								break;
							}
						}
					}
				}
			}
			if (!flag && !flag2 && MetaType.HasFamily(family, MetaType.AttributeFamily.DataContractSerialier))
			{
				AttributeMap attribute = MetaType.GetAttribute(attribs, "System.Runtime.Serialization.DataMemberAttribute");
				if (attribute != null)
				{
					MetaType.GetFieldNumber(ref num, attribute, "Order");
					MetaType.GetFieldName(ref text, attribute, "Name");
					MetaType.GetFieldBoolean(ref isRequired, attribute, "IsRequired");
					flag2 = (num >= num2);
					if (flag2)
					{
						num += dataMemberOffset;
					}
				}
			}
			if (!flag && !flag2 && MetaType.HasFamily(family, MetaType.AttributeFamily.XmlSerializer))
			{
				AttributeMap attribute = MetaType.GetAttribute(attribs, "System.Xml.Serialization.XmlElementAttribute");
				if (attribute == null)
				{
					attribute = MetaType.GetAttribute(attribs, "System.Xml.Serialization.XmlArrayAttribute");
				}
				MetaType.GetIgnore(ref flag, attribute, attribs, "System.Xml.Serialization.XmlIgnoreAttribute");
				if (attribute != null && !flag)
				{
					MetaType.GetFieldNumber(ref num, attribute, "Order");
					MetaType.GetFieldName(ref text, attribute, "ElementName");
					flag2 = (num >= num2);
				}
			}
			if (!flag && !flag2 && MetaType.GetAttribute(attribs, "System.NonSerializedAttribute") != null)
			{
				flag = true;
			}
			if (flag || (num < num2 && !forced))
			{
				return null;
			}
			return new ProtoMemberAttribute(num, forced || inferByTagName)
			{
				AsReference = asReference,
				AsReferenceHasValue = flag3,
				DataFormat = dataFormat,
				DynamicType = dynamicType,
				IsPacked = isPacked,
				OverwriteList = overwriteList,
				IsRequired = isRequired,
				Name = (string.IsNullOrEmpty(text) ? member.Name : text),
				Member = member,
				BackingMember = backingMember,
				TagIsPinned = tagIsPinned
			};
		}

		// Token: 0x06007EDE RID: 32478 RVA: 0x00255FD0 File Offset: 0x00255FD0
		private ValueMember ApplyDefaultBehaviour(bool isEnum, ProtoMemberAttribute normalizedAttribute)
		{
			MemberInfo member;
			if (normalizedAttribute == null || (member = normalizedAttribute.Member) == null)
			{
				return null;
			}
			Type memberType = Helpers.GetMemberType(member);
			Type type = null;
			Type defaultType = null;
			MetaType.ResolveListTypes(this.model, memberType, ref type, ref defaultType);
			bool flag = false;
			if (type != null)
			{
				int num = this.model.FindOrAddAuto(memberType, false, true, false);
				if (num >= 0 && (flag = this.model[memberType].IgnoreListHandling))
				{
					type = null;
					defaultType = null;
				}
			}
			AttributeMap[] attribs = AttributeMap.Create(this.model, member, true);
			object defaultValue = null;
			if (this.model.UseImplicitZeroDefaults)
			{
				ProtoTypeCode typeCode = Helpers.GetTypeCode(memberType);
				switch (typeCode)
				{
				case ProtoTypeCode.Boolean:
					defaultValue = false;
					break;
				case ProtoTypeCode.Char:
					defaultValue = '\0';
					break;
				case ProtoTypeCode.SByte:
					defaultValue = 0;
					break;
				case ProtoTypeCode.Byte:
					defaultValue = 0;
					break;
				case ProtoTypeCode.Int16:
					defaultValue = 0;
					break;
				case ProtoTypeCode.UInt16:
					defaultValue = 0;
					break;
				case ProtoTypeCode.Int32:
					defaultValue = 0;
					break;
				case ProtoTypeCode.UInt32:
					defaultValue = 0U;
					break;
				case ProtoTypeCode.Int64:
					defaultValue = 0L;
					break;
				case ProtoTypeCode.UInt64:
					defaultValue = 0UL;
					break;
				case ProtoTypeCode.Single:
					defaultValue = 0f;
					break;
				case ProtoTypeCode.Double:
					defaultValue = 0.0;
					break;
				case ProtoTypeCode.Decimal:
					defaultValue = 0m;
					break;
				default:
					if (typeCode != ProtoTypeCode.TimeSpan)
					{
						if (typeCode == ProtoTypeCode.Guid)
						{
							defaultValue = Guid.Empty;
						}
					}
					else
					{
						defaultValue = TimeSpan.Zero;
					}
					break;
				}
			}
			AttributeMap attribute;
			object obj;
			if ((attribute = MetaType.GetAttribute(attribs, "System.ComponentModel.DefaultValueAttribute")) != null && attribute.TryGet("Value", out obj))
			{
				defaultValue = obj;
			}
			ValueMember valueMember = (isEnum || normalizedAttribute.Tag > 0) ? new ValueMember(this.model, this.type, normalizedAttribute.Tag, member, memberType, type, defaultType, normalizedAttribute.DataFormat, defaultValue) : null;
			if (valueMember != null)
			{
				valueMember.BackingMember = normalizedAttribute.BackingMember;
				Type declaringType = this.type;
				PropertyInfo propertyInfo = Helpers.GetProperty(declaringType, member.Name + "Specified", true);
				MethodInfo getMethod = Helpers.GetGetMethod(propertyInfo, true, true);
				if (getMethod == null || getMethod.IsStatic)
				{
					propertyInfo = null;
				}
				if (propertyInfo != null)
				{
					valueMember.SetSpecified(getMethod, Helpers.GetSetMethod(propertyInfo, true, true));
				}
				else
				{
					MethodInfo instanceMethod = Helpers.GetInstanceMethod(declaringType, "ShouldSerialize" + member.Name, Helpers.EmptyTypes);
					if (instanceMethod != null && instanceMethod.ReturnType == this.model.MapType(typeof(bool)))
					{
						valueMember.SetSpecified(instanceMethod, null);
					}
				}
				if (!string.IsNullOrEmpty(normalizedAttribute.Name))
				{
					valueMember.SetName(normalizedAttribute.Name);
				}
				valueMember.IsPacked = normalizedAttribute.IsPacked;
				valueMember.IsRequired = normalizedAttribute.IsRequired;
				valueMember.OverwriteList = normalizedAttribute.OverwriteList;
				if (normalizedAttribute.AsReferenceHasValue)
				{
					valueMember.AsReference = normalizedAttribute.AsReference;
				}
				valueMember.DynamicType = normalizedAttribute.DynamicType;
				Type type2;
				Type type3;
				Type type4;
				valueMember.IsMap = (!flag && valueMember.ResolveMapTypes(out type2, out type3, out type4));
				if (valueMember.IsMap && (attribute = MetaType.GetAttribute(attribs, "ProtoBuf.ProtoMapAttribute")) != null)
				{
					object obj2;
					if (attribute.TryGet("DisableMap", out obj2) && (bool)obj2)
					{
						valueMember.IsMap = false;
					}
					else
					{
						if (attribute.TryGet("KeyFormat", out obj2))
						{
							valueMember.MapKeyFormat = (DataFormat)obj2;
						}
						if (attribute.TryGet("ValueFormat", out obj2))
						{
							valueMember.MapValueFormat = (DataFormat)obj2;
						}
					}
				}
			}
			return valueMember;
		}

		// Token: 0x06007EDF RID: 32479 RVA: 0x00256410 File Offset: 0x00256410
		private static void GetDataFormat(ref DataFormat value, AttributeMap attrib, string memberName)
		{
			if (attrib == null || value != DataFormat.Default)
			{
				return;
			}
			object obj;
			if (attrib.TryGet(memberName, out obj) && obj != null)
			{
				value = (DataFormat)obj;
			}
		}

		// Token: 0x06007EE0 RID: 32480 RVA: 0x0025644C File Offset: 0x0025644C
		private static void GetIgnore(ref bool ignore, AttributeMap attrib, AttributeMap[] attribs, string fullName)
		{
			if (ignore || attrib == null)
			{
				return;
			}
			ignore = (MetaType.GetAttribute(attribs, fullName) != null);
		}

		// Token: 0x06007EE1 RID: 32481 RVA: 0x00256468 File Offset: 0x00256468
		private static void GetFieldBoolean(ref bool value, AttributeMap attrib, string memberName)
		{
			MetaType.GetFieldBoolean(ref value, attrib, memberName, true);
		}

		// Token: 0x06007EE2 RID: 32482 RVA: 0x00256474 File Offset: 0x00256474
		private static bool GetFieldBoolean(ref bool value, AttributeMap attrib, string memberName, bool publicOnly)
		{
			if (attrib == null)
			{
				return false;
			}
			if (value)
			{
				return true;
			}
			object obj;
			if (attrib.TryGet(memberName, publicOnly, out obj) && obj != null)
			{
				value = (bool)obj;
				return true;
			}
			return false;
		}

		// Token: 0x06007EE3 RID: 32483 RVA: 0x002564B8 File Offset: 0x002564B8
		private static void GetFieldNumber(ref int value, AttributeMap attrib, string memberName)
		{
			if (attrib == null || value > 0)
			{
				return;
			}
			object obj;
			if (attrib.TryGet(memberName, out obj) && obj != null)
			{
				value = (int)obj;
			}
		}

		// Token: 0x06007EE4 RID: 32484 RVA: 0x002564F4 File Offset: 0x002564F4
		private static void GetFieldName(ref string name, AttributeMap attrib, string memberName)
		{
			if (attrib == null || !string.IsNullOrEmpty(name))
			{
				return;
			}
			object obj;
			if (attrib.TryGet(memberName, out obj) && obj != null)
			{
				name = (string)obj;
			}
		}

		// Token: 0x06007EE5 RID: 32485 RVA: 0x00256534 File Offset: 0x00256534
		private static AttributeMap GetAttribute(AttributeMap[] attribs, string fullName)
		{
			foreach (AttributeMap attributeMap in attribs)
			{
				if (attributeMap != null && attributeMap.AttributeType.FullName == fullName)
				{
					return attributeMap;
				}
			}
			return null;
		}

		// Token: 0x06007EE6 RID: 32486 RVA: 0x00256580 File Offset: 0x00256580
		public MetaType Add(int fieldNumber, string memberName)
		{
			this.AddField(fieldNumber, memberName, null, null, null);
			return this;
		}

		// Token: 0x06007EE7 RID: 32487 RVA: 0x00256590 File Offset: 0x00256590
		public ValueMember AddField(int fieldNumber, string memberName)
		{
			return this.AddField(fieldNumber, memberName, null, null, null);
		}

		// Token: 0x17001B98 RID: 7064
		// (get) Token: 0x06007EE8 RID: 32488 RVA: 0x002565A0 File Offset: 0x002565A0
		// (set) Token: 0x06007EE9 RID: 32489 RVA: 0x002565B0 File Offset: 0x002565B0
		public bool UseConstructor
		{
			get
			{
				return !this.HasFlag(16);
			}
			set
			{
				this.SetFlag(16, !value, true);
			}
		}

		// Token: 0x17001B99 RID: 7065
		// (get) Token: 0x06007EEA RID: 32490 RVA: 0x002565C0 File Offset: 0x002565C0
		// (set) Token: 0x06007EEB RID: 32491 RVA: 0x002565C8 File Offset: 0x002565C8
		public Type ConstructType
		{
			get
			{
				return this.constructType;
			}
			set
			{
				this.ThrowIfFrozen();
				this.constructType = value;
			}
		}

		// Token: 0x06007EEC RID: 32492 RVA: 0x002565D8 File Offset: 0x002565D8
		public MetaType Add(string memberName)
		{
			this.Add(this.GetNextFieldNumber(), memberName);
			return this;
		}

		// Token: 0x06007EED RID: 32493 RVA: 0x002565EC File Offset: 0x002565EC
		public void SetSurrogate(Type surrogateType)
		{
			if (surrogateType == this.type)
			{
				surrogateType = null;
			}
			if (surrogateType != null && surrogateType != null && Helpers.IsAssignableFrom(this.model.MapType(typeof(IEnumerable)), surrogateType))
			{
				throw new ArgumentException("Repeated data (a list, collection, etc) has inbuilt behaviour and cannot be used as a surrogate");
			}
			this.ThrowIfFrozen();
			this.surrogate = surrogateType;
		}

		// Token: 0x06007EEE RID: 32494 RVA: 0x00256664 File Offset: 0x00256664
		internal MetaType GetSurrogateOrSelf()
		{
			if (this.surrogate != null)
			{
				return this.model[this.surrogate];
			}
			return this;
		}

		// Token: 0x06007EEF RID: 32495 RVA: 0x0025668C File Offset: 0x0025668C
		internal MetaType GetSurrogateOrBaseOrSelf(bool deep)
		{
			if (this.surrogate != null)
			{
				return this.model[this.surrogate];
			}
			MetaType metaType = this.baseType;
			if (metaType == null)
			{
				return this;
			}
			if (deep)
			{
				MetaType result;
				do
				{
					result = metaType;
					metaType = metaType.baseType;
				}
				while (metaType != null);
				return result;
			}
			return metaType;
		}

		// Token: 0x06007EF0 RID: 32496 RVA: 0x002566E4 File Offset: 0x002566E4
		private int GetNextFieldNumber()
		{
			int num = 0;
			foreach (object obj in this.fields)
			{
				ValueMember valueMember = (ValueMember)obj;
				if (valueMember.FieldNumber > num)
				{
					num = valueMember.FieldNumber;
				}
			}
			if (this.subTypes != null)
			{
				foreach (object obj2 in this.subTypes)
				{
					SubType subType = (SubType)obj2;
					if (subType.FieldNumber > num)
					{
						num = subType.FieldNumber;
					}
				}
			}
			return num + 1;
		}

		// Token: 0x06007EF1 RID: 32497 RVA: 0x0025677C File Offset: 0x0025677C
		public MetaType Add(params string[] memberNames)
		{
			if (memberNames == null)
			{
				throw new ArgumentNullException("memberNames");
			}
			int nextFieldNumber = this.GetNextFieldNumber();
			for (int i = 0; i < memberNames.Length; i++)
			{
				this.Add(nextFieldNumber++, memberNames[i]);
			}
			return this;
		}

		// Token: 0x06007EF2 RID: 32498 RVA: 0x002567CC File Offset: 0x002567CC
		public MetaType Add(int fieldNumber, string memberName, object defaultValue)
		{
			this.AddField(fieldNumber, memberName, null, null, defaultValue);
			return this;
		}

		// Token: 0x06007EF3 RID: 32499 RVA: 0x002567DC File Offset: 0x002567DC
		public MetaType Add(int fieldNumber, string memberName, Type itemType, Type defaultType)
		{
			this.AddField(fieldNumber, memberName, itemType, defaultType, null);
			return this;
		}

		// Token: 0x06007EF4 RID: 32500 RVA: 0x002567EC File Offset: 0x002567EC
		public ValueMember AddField(int fieldNumber, string memberName, Type itemType, Type defaultType)
		{
			return this.AddField(fieldNumber, memberName, itemType, defaultType, null);
		}

		// Token: 0x06007EF5 RID: 32501 RVA: 0x002567FC File Offset: 0x002567FC
		private ValueMember AddField(int fieldNumber, string memberName, Type itemType, Type defaultType, object defaultValue)
		{
			MemberInfo memberInfo = null;
			MemberInfo[] member = this.type.GetMember(memberName, Helpers.IsEnum(this.type) ? (BindingFlags.Static | BindingFlags.Public) : (BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic));
			if (member != null && member.Length == 1)
			{
				memberInfo = member[0];
			}
			if (memberInfo == null)
			{
				throw new ArgumentException("Unable to determine member: " + memberName, "memberName");
			}
			PropertyInfo propertyInfo = null;
			MemberTypes memberType = memberInfo.MemberType;
			Type memberType2;
			if (memberType != MemberTypes.Field)
			{
				if (memberType != MemberTypes.Property)
				{
					throw new NotSupportedException(memberInfo.MemberType.ToString());
				}
				propertyInfo = (PropertyInfo)memberInfo;
				memberType2 = propertyInfo.PropertyType;
			}
			else
			{
				FieldInfo fieldInfo = (FieldInfo)memberInfo;
				memberType2 = fieldInfo.FieldType;
			}
			MetaType.ResolveListTypes(this.model, memberType2, ref itemType, ref defaultType);
			MemberInfo memberInfo2 = null;
			if (propertyInfo != null && !propertyInfo.CanWrite)
			{
				string text = "<" + ((PropertyInfo)memberInfo).Name + ">k__BackingField";
				MemberInfo[] member2 = this.type.GetMember("<" + ((PropertyInfo)memberInfo).Name + ">k__BackingField", Helpers.IsEnum(this.type) ? (BindingFlags.Static | BindingFlags.Public) : (BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic));
				if (member2 != null && member2.Length == 1 && member2[0] as FieldInfo != null)
				{
					memberInfo2 = member2[0];
				}
			}
			ValueMember valueMember = new ValueMember(this.model, this.type, fieldNumber, memberInfo2 ?? memberInfo, memberType2, itemType, defaultType, DataFormat.Default, defaultValue);
			if (memberInfo2 != null)
			{
				valueMember.SetName(memberInfo.Name);
			}
			this.Add(valueMember);
			return valueMember;
		}

		// Token: 0x06007EF6 RID: 32502 RVA: 0x002569D4 File Offset: 0x002569D4
		internal static void ResolveListTypes(TypeModel model, Type type, ref Type itemType, ref Type defaultType)
		{
			if (type == null)
			{
				return;
			}
			if (type.IsArray)
			{
				if (type.GetArrayRank() != 1)
				{
					throw new NotSupportedException("Multi-dimensional arrays are not supported");
				}
				itemType = type.GetElementType();
				if (itemType == model.MapType(typeof(byte)))
				{
					Type type2;
					itemType = (type2 = null);
					defaultType = type2;
				}
				else
				{
					defaultType = type;
				}
			}
			if (itemType == null)
			{
				itemType = TypeModel.GetListItemType(model, type);
			}
			if (itemType != null)
			{
				Type left = null;
				Type type3 = null;
				MetaType.ResolveListTypes(model, itemType, ref left, ref type3);
				if (left != null)
				{
					throw TypeModel.CreateNestedListsNotSupported(type);
				}
			}
			if (itemType != null && defaultType == null)
			{
				if (type.IsClass && !type.IsAbstract && Helpers.GetConstructor(type, Helpers.EmptyTypes, true) != null)
				{
					defaultType = type;
				}
				if (defaultType == null && type.IsInterface)
				{
					Type[] genericArguments;
					if (type.IsGenericType && type.GetGenericTypeDefinition() == model.MapType(typeof(IDictionary<, >)) && itemType == model.MapType(typeof(KeyValuePair<, >)).MakeGenericType(genericArguments = type.GetGenericArguments()))
					{
						defaultType = model.MapType(typeof(Dictionary<, >)).MakeGenericType(genericArguments);
					}
					else
					{
						defaultType = model.MapType(typeof(List<>)).MakeGenericType(new Type[]
						{
							itemType
						});
					}
				}
				if (defaultType != null && !Helpers.IsAssignableFrom(type, defaultType))
				{
					defaultType = null;
				}
			}
		}

		// Token: 0x06007EF7 RID: 32503 RVA: 0x00256BA0 File Offset: 0x00256BA0
		private void Add(ValueMember member)
		{
			int opaqueToken = 0;
			try
			{
				this.model.TakeLock(ref opaqueToken);
				this.ThrowIfFrozen();
				this.fields.Add(member);
			}
			finally
			{
				this.model.ReleaseLock(opaqueToken);
			}
		}

		// Token: 0x17001B9A RID: 7066
		public ValueMember this[int fieldNumber]
		{
			get
			{
				foreach (object obj in this.fields)
				{
					ValueMember valueMember = (ValueMember)obj;
					if (valueMember.FieldNumber == fieldNumber)
					{
						return valueMember;
					}
				}
				return null;
			}
		}

		// Token: 0x17001B9B RID: 7067
		public ValueMember this[MemberInfo member]
		{
			get
			{
				if (member == null)
				{
					return null;
				}
				foreach (object obj in this.fields)
				{
					ValueMember valueMember = (ValueMember)obj;
					if (valueMember.Member == member || valueMember.BackingMember == member)
					{
						return valueMember;
					}
				}
				return null;
			}
		}

		// Token: 0x06007EFA RID: 32506 RVA: 0x00256CA8 File Offset: 0x00256CA8
		public ValueMember[] GetFields()
		{
			ValueMember[] array = new ValueMember[this.fields.Count];
			this.fields.CopyTo(array, 0);
			Array.Sort<ValueMember>(array, ValueMember.Comparer.Default);
			return array;
		}

		// Token: 0x06007EFB RID: 32507 RVA: 0x00256CE4 File Offset: 0x00256CE4
		public SubType[] GetSubtypes()
		{
			if (this.subTypes == null || this.subTypes.Count == 0)
			{
				return new SubType[0];
			}
			SubType[] array = new SubType[this.subTypes.Count];
			this.subTypes.CopyTo(array, 0);
			Array.Sort<SubType>(array, SubType.Comparer.Default);
			return array;
		}

		// Token: 0x06007EFC RID: 32508 RVA: 0x00256D44 File Offset: 0x00256D44
		internal IEnumerable<Type> GetAllGenericArguments()
		{
			return MetaType.GetAllGenericArguments(this.type);
		}

		// Token: 0x06007EFD RID: 32509 RVA: 0x00256D54 File Offset: 0x00256D54
		private static IEnumerable<Type> GetAllGenericArguments(Type type)
		{
			Type[] genericArguments = type.GetGenericArguments();
			foreach (Type arg in genericArguments)
			{
				yield return arg;
				foreach (Type type2 in MetaType.GetAllGenericArguments(arg))
				{
					yield return type2;
				}
				IEnumerator<Type> enumerator = null;
				arg = null;
			}
			Type[] array = null;
			yield break;
			yield break;
		}

		// Token: 0x06007EFE RID: 32510 RVA: 0x00256D64 File Offset: 0x00256D64
		public void CompileInPlace()
		{
			this.serializer = CompiledSerializer.Wrap(this.Serializer, this.model);
		}

		// Token: 0x06007EFF RID: 32511 RVA: 0x00256D80 File Offset: 0x00256D80
		internal bool IsDefined(int fieldNumber)
		{
			foreach (object obj in this.fields)
			{
				ValueMember valueMember = (ValueMember)obj;
				if (valueMember.FieldNumber == fieldNumber)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06007F00 RID: 32512 RVA: 0x00256DC8 File Offset: 0x00256DC8
		internal int GetKey(bool demand, bool getBaseKey)
		{
			return this.model.GetKey(this.type, demand, getBaseKey);
		}

		// Token: 0x06007F01 RID: 32513 RVA: 0x00256DE0 File Offset: 0x00256DE0
		internal EnumSerializer.EnumPair[] GetEnumMap()
		{
			if (this.HasFlag(2))
			{
				return null;
			}
			EnumSerializer.EnumPair[] array = new EnumSerializer.EnumPair[this.fields.Count];
			for (int i = 0; i < array.Length; i++)
			{
				ValueMember valueMember = (ValueMember)this.fields[i];
				int fieldNumber = valueMember.FieldNumber;
				object rawEnumValue = valueMember.GetRawEnumValue();
				array[i] = new EnumSerializer.EnumPair(fieldNumber, rawEnumValue, valueMember.MemberType);
			}
			return array;
		}

		// Token: 0x17001B9C RID: 7068
		// (get) Token: 0x06007F02 RID: 32514 RVA: 0x00256E58 File Offset: 0x00256E58
		// (set) Token: 0x06007F03 RID: 32515 RVA: 0x00256E64 File Offset: 0x00256E64
		public bool EnumPassthru
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

		// Token: 0x17001B9D RID: 7069
		// (get) Token: 0x06007F04 RID: 32516 RVA: 0x00256E70 File Offset: 0x00256E70
		// (set) Token: 0x06007F05 RID: 32517 RVA: 0x00256E80 File Offset: 0x00256E80
		public bool IgnoreListHandling
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

		// Token: 0x17001B9E RID: 7070
		// (get) Token: 0x06007F06 RID: 32518 RVA: 0x00256E90 File Offset: 0x00256E90
		// (set) Token: 0x06007F07 RID: 32519 RVA: 0x00256E9C File Offset: 0x00256E9C
		internal bool Pending
		{
			get
			{
				return this.HasFlag(1);
			}
			set
			{
				this.SetFlag(1, value, false);
			}
		}

		// Token: 0x06007F08 RID: 32520 RVA: 0x00256EA8 File Offset: 0x00256EA8
		private bool HasFlag(ushort flag)
		{
			return (this.flags & flag) == flag;
		}

		// Token: 0x06007F09 RID: 32521 RVA: 0x00256EB8 File Offset: 0x00256EB8
		private void SetFlag(ushort flag, bool value, bool throwIfFrozen)
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

		// Token: 0x06007F0A RID: 32522 RVA: 0x00256F10 File Offset: 0x00256F10
		internal static MetaType GetRootType(MetaType source)
		{
			while (source.serializer != null)
			{
				MetaType metaType = source.baseType;
				if (metaType == null)
				{
					return source;
				}
				source = metaType;
			}
			RuntimeTypeModel runtimeTypeModel = source.model;
			int opaqueToken = 0;
			MetaType result;
			try
			{
				runtimeTypeModel.TakeLock(ref opaqueToken);
				MetaType metaType2;
				while ((metaType2 = source.baseType) != null)
				{
					source = metaType2;
				}
				result = source;
			}
			finally
			{
				runtimeTypeModel.ReleaseLock(opaqueToken);
			}
			return result;
		}

		// Token: 0x06007F0B RID: 32523 RVA: 0x00256F84 File Offset: 0x00256F84
		internal bool IsPrepared()
		{
			return this.serializer is CompiledSerializer;
		}

		// Token: 0x17001B9F RID: 7071
		// (get) Token: 0x06007F0C RID: 32524 RVA: 0x00256F94 File Offset: 0x00256F94
		internal IEnumerable Fields
		{
			get
			{
				return this.fields;
			}
		}

		// Token: 0x06007F0D RID: 32525 RVA: 0x00256F9C File Offset: 0x00256F9C
		internal static StringBuilder NewLine(StringBuilder builder, int indent)
		{
			return Helpers.AppendLine(builder).Append(' ', indent * 3);
		}

		// Token: 0x17001BA0 RID: 7072
		// (get) Token: 0x06007F0E RID: 32526 RVA: 0x00256FB0 File Offset: 0x00256FB0
		internal bool IsAutoTuple
		{
			get
			{
				return this.HasFlag(64);
			}
		}

		// Token: 0x17001BA1 RID: 7073
		// (get) Token: 0x06007F0F RID: 32527 RVA: 0x00256FBC File Offset: 0x00256FBC
		// (set) Token: 0x06007F10 RID: 32528 RVA: 0x00256FCC File Offset: 0x00256FCC
		public bool IsGroup
		{
			get
			{
				return this.HasFlag(256);
			}
			set
			{
				this.SetFlag(256, value, true);
			}
		}

		// Token: 0x06007F11 RID: 32529 RVA: 0x00256FDC File Offset: 0x00256FDC
		internal void WriteSchema(StringBuilder builder, int indent, ref RuntimeTypeModel.CommonImports imports, ProtoSyntax syntax)
		{
			if (this.surrogate != null)
			{
				return;
			}
			ValueMember[] array = new ValueMember[this.fields.Count];
			this.fields.CopyTo(array, 0);
			Array.Sort<ValueMember>(array, ValueMember.Comparer.Default);
			if (this.IsList)
			{
				string schemaTypeName = this.model.GetSchemaTypeName(TypeModel.GetListItemType(this.model, this.type), DataFormat.Default, false, false, ref imports);
				MetaType.NewLine(builder, indent).Append("message ").Append(this.GetSchemaTypeName()).Append(" {");
				MetaType.NewLine(builder, indent + 1).Append("repeated ").Append(schemaTypeName).Append(" items = 1;");
				MetaType.NewLine(builder, indent).Append('}');
				return;
			}
			if (this.IsAutoTuple)
			{
				MemberInfo[] array2;
				if (MetaType.ResolveTupleConstructor(this.type, out array2) != null)
				{
					MetaType.NewLine(builder, indent).Append("message ").Append(this.GetSchemaTypeName()).Append(" {");
					for (int i = 0; i < array2.Length; i++)
					{
						PropertyInfo propertyInfo = array2[i] as PropertyInfo;
						Type effectiveType;
						if (propertyInfo != null)
						{
							effectiveType = propertyInfo.PropertyType;
						}
						else
						{
							FieldInfo fieldInfo = array2[i] as FieldInfo;
							if (fieldInfo == null)
							{
								throw new NotSupportedException("Unknown member type: " + array2[i].GetType().Name);
							}
							effectiveType = fieldInfo.FieldType;
						}
						MetaType.NewLine(builder, indent + 1).Append((syntax == ProtoSyntax.Proto2) ? "optional " : "").Append(this.model.GetSchemaTypeName(effectiveType, DataFormat.Default, false, false, ref imports).Replace('.', '_')).Append(' ').Append(array2[i].Name).Append(" = ").Append(i + 1).Append(';');
					}
					MetaType.NewLine(builder, indent).Append('}');
					return;
				}
			}
			else
			{
				if (Helpers.IsEnum(this.type))
				{
					MetaType.NewLine(builder, indent).Append("enum ").Append(this.GetSchemaTypeName()).Append(" {");
					if (array.Length == 0 && this.EnumPassthru)
					{
						if (this.type.IsDefined(this.model.MapType(typeof(FlagsAttribute)), false))
						{
							MetaType.NewLine(builder, indent + 1).Append("// this is a composite/flags enumeration");
						}
						else
						{
							MetaType.NewLine(builder, indent + 1).Append("// this enumeration will be passed as a raw value");
						}
						foreach (FieldInfo fieldInfo2 in this.type.GetFields())
						{
							if (fieldInfo2.IsStatic && fieldInfo2.IsLiteral)
							{
								object rawConstantValue = fieldInfo2.GetRawConstantValue();
								MetaType.NewLine(builder, indent + 1).Append(fieldInfo2.Name).Append(" = ").Append(rawConstantValue).Append(";");
							}
						}
					}
					else
					{
						Dictionary<int, int> dictionary = new Dictionary<int, int>(array.Length);
						bool flag = false;
						foreach (ValueMember valueMember in array)
						{
							if (dictionary.ContainsKey(valueMember.FieldNumber))
							{
								flag = true;
								break;
							}
							dictionary.Add(valueMember.FieldNumber, 1);
						}
						if (flag)
						{
							MetaType.NewLine(builder, indent + 1).Append("option allow_alias = true;");
						}
						bool flag2 = false;
						foreach (ValueMember valueMember2 in array)
						{
							if (valueMember2.FieldNumber == 0)
							{
								MetaType.NewLine(builder, indent + 1).Append(valueMember2.Name).Append(" = ").Append(valueMember2.FieldNumber).Append(';');
								flag2 = true;
							}
						}
						if (syntax == ProtoSyntax.Proto3 && !flag2)
						{
							MetaType.NewLine(builder, indent + 1).Append("ZERO = 0; // proto3 requires a zero value as the first item (it can be named anything)");
						}
						foreach (ValueMember valueMember3 in array)
						{
							if (valueMember3.FieldNumber != 0)
							{
								MetaType.NewLine(builder, indent + 1).Append(valueMember3.Name).Append(" = ").Append(valueMember3.FieldNumber).Append(';');
							}
						}
					}
					MetaType.NewLine(builder, indent).Append('}');
					return;
				}
				MetaType.NewLine(builder, indent).Append("message ").Append(this.GetSchemaTypeName()).Append(" {");
				foreach (ValueMember valueMember4 in array)
				{
					bool flag3 = false;
					string schemaTypeName3;
					if (valueMember4.IsMap)
					{
						Type type;
						Type effectiveType2;
						Type effectiveType3;
						valueMember4.ResolveMapTypes(out type, out effectiveType2, out effectiveType3);
						string schemaTypeName2 = this.model.GetSchemaTypeName(effectiveType2, valueMember4.MapKeyFormat, false, false, ref imports);
						schemaTypeName3 = this.model.GetSchemaTypeName(effectiveType3, valueMember4.MapKeyFormat, valueMember4.AsReference, valueMember4.DynamicType, ref imports);
						MetaType.NewLine(builder, indent + 1).Append("map<").Append(schemaTypeName2).Append(",").Append(schemaTypeName3).Append("> ").Append(valueMember4.Name).Append(" = ").Append(valueMember4.FieldNumber).Append(";");
					}
					else
					{
						string value = (valueMember4.ItemType != null) ? "repeated " : ((syntax == ProtoSyntax.Proto2) ? (valueMember4.IsRequired ? "required " : "optional ") : "");
						MetaType.NewLine(builder, indent + 1).Append(value);
						if (valueMember4.DataFormat == DataFormat.Group)
						{
							builder.Append("group ");
						}
						schemaTypeName3 = valueMember4.GetSchemaTypeName(true, ref imports);
						builder.Append(schemaTypeName3).Append(" ").Append(valueMember4.Name).Append(" = ").Append(valueMember4.FieldNumber);
						if (syntax == ProtoSyntax.Proto2 && valueMember4.DefaultValue != null && !valueMember4.IsRequired)
						{
							if (valueMember4.DefaultValue is string)
							{
								MetaType.AddOption(builder, ref flag3).Append("default = \"").Append(valueMember4.DefaultValue).Append("\"");
							}
							else if (!(valueMember4.DefaultValue is TimeSpan))
							{
								if (valueMember4.DefaultValue is bool)
								{
									MetaType.AddOption(builder, ref flag3).Append(((bool)valueMember4.DefaultValue) ? "default = true" : "default = false");
								}
								else
								{
									MetaType.AddOption(builder, ref flag3).Append("default = ").Append(valueMember4.DefaultValue);
								}
							}
						}
						if (MetaType.CanPack(valueMember4.ItemType))
						{
							if (syntax == ProtoSyntax.Proto2)
							{
								if (valueMember4.IsPacked)
								{
									MetaType.AddOption(builder, ref flag3).Append("packed = true");
								}
							}
							else if (!valueMember4.IsPacked)
							{
								MetaType.AddOption(builder, ref flag3).Append("packed = false");
							}
						}
						if (valueMember4.AsReference)
						{
							imports |= RuntimeTypeModel.CommonImports.Protogen;
							MetaType.AddOption(builder, ref flag3).Append("(.protobuf_net.fieldopt).asRef = true");
						}
						if (valueMember4.DynamicType)
						{
							imports |= RuntimeTypeModel.CommonImports.Protogen;
							MetaType.AddOption(builder, ref flag3).Append("(.protobuf_net.fieldopt).dynamicType = true");
						}
						MetaType.CloseOption(builder, ref flag3).Append(';');
						if (syntax != ProtoSyntax.Proto2 && valueMember4.DefaultValue != null && !valueMember4.IsRequired && !MetaType.IsImplicitDefault(valueMember4.DefaultValue))
						{
							builder.Append(" // default value could not be applied: ").Append(valueMember4.DefaultValue);
						}
					}
					if (schemaTypeName3 == ".bcl.NetObjectProxy" && valueMember4.AsReference && !valueMember4.DynamicType)
					{
						builder.Append(" // reference-tracked ").Append(valueMember4.GetSchemaTypeName(false, ref imports));
					}
				}
				if (this.subTypes != null && this.subTypes.Count != 0)
				{
					SubType[] array8 = new SubType[this.subTypes.Count];
					this.subTypes.CopyTo(array8, 0);
					Array.Sort<SubType>(array8, SubType.Comparer.Default);
					string[] array9 = new string[array8.Length];
					for (int num = 0; num < array8.Length; num++)
					{
						array9[num] = array8[num].DerivedType.GetSchemaTypeName();
					}
					string text = "subtype";
					while (Array.IndexOf<string>(array9, text) >= 0)
					{
						text = "_" + text;
					}
					MetaType.NewLine(builder, indent + 1).Append("oneof ").Append(text).Append(" {");
					for (int num2 = 0; num2 < array8.Length; num2++)
					{
						string value2 = array9[num2];
						MetaType.NewLine(builder, indent + 2).Append(value2).Append(" ").Append(value2).Append(" = ").Append(array8[num2].FieldNumber).Append(';');
					}
					MetaType.NewLine(builder, indent + 1).Append("}");
				}
				MetaType.NewLine(builder, indent).Append('}');
			}
		}

		// Token: 0x06007F12 RID: 32530 RVA: 0x002579C0 File Offset: 0x002579C0
		private static StringBuilder AddOption(StringBuilder builder, ref bool hasOption)
		{
			if (hasOption)
			{
				return builder.Append(", ");
			}
			hasOption = true;
			return builder.Append(" [");
		}

		// Token: 0x06007F13 RID: 32531 RVA: 0x002579E4 File Offset: 0x002579E4
		private static StringBuilder CloseOption(StringBuilder builder, ref bool hasOption)
		{
			if (hasOption)
			{
				hasOption = false;
				return builder.Append("]");
			}
			return builder;
		}

		// Token: 0x06007F14 RID: 32532 RVA: 0x00257A00 File Offset: 0x00257A00
		private static bool IsImplicitDefault(object value)
		{
			try
			{
				if (value == null)
				{
					return false;
				}
				ProtoTypeCode typeCode = Helpers.GetTypeCode(value.GetType());
				switch (typeCode)
				{
				case ProtoTypeCode.Boolean:
					return !(bool)value;
				case ProtoTypeCode.Char:
					return (char)value == '\0';
				case ProtoTypeCode.SByte:
					return (sbyte)value == 0;
				case ProtoTypeCode.Byte:
					return (byte)value == 0;
				case ProtoTypeCode.Int16:
					return (short)value == 0;
				case ProtoTypeCode.UInt16:
					return (ushort)value == 0;
				case ProtoTypeCode.Int32:
					return (int)value == 0;
				case ProtoTypeCode.UInt32:
					return (uint)value == 0U;
				case ProtoTypeCode.Int64:
					return (long)value == 0L;
				case ProtoTypeCode.UInt64:
					return (ulong)value == 0UL;
				case ProtoTypeCode.Single:
					return (float)value == 0f;
				case ProtoTypeCode.Double:
					return (double)value == 0.0;
				case ProtoTypeCode.Decimal:
					return (decimal)value == 0m;
				case ProtoTypeCode.DateTime:
					return (DateTime)value == default(DateTime);
				case (ProtoTypeCode)17:
					break;
				case ProtoTypeCode.String:
					return (string)value == "";
				default:
					if (typeCode == ProtoTypeCode.TimeSpan)
					{
						return (TimeSpan)value == TimeSpan.Zero;
					}
					break;
				}
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x06007F15 RID: 32533 RVA: 0x00257BC4 File Offset: 0x00257BC4
		private static bool CanPack(Type type)
		{
			if (type == null)
			{
				return false;
			}
			ProtoTypeCode typeCode = Helpers.GetTypeCode(type);
			return typeCode - ProtoTypeCode.Boolean <= 11;
		}

		// Token: 0x06007F16 RID: 32534 RVA: 0x00257BF8 File Offset: 0x00257BF8
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void ApplyFieldOffset(int offset)
		{
			if (Helpers.IsEnum(this.type))
			{
				throw new InvalidOperationException("Cannot apply field-offset to an enum");
			}
			if (offset == 0)
			{
				return;
			}
			int opaqueToken = 0;
			try
			{
				this.model.TakeLock(ref opaqueToken);
				this.ThrowIfFrozen();
				if (this.fields != null)
				{
					foreach (object obj in this.fields)
					{
						ValueMember valueMember = (ValueMember)obj;
						MetaType.AssertValidFieldNumber(valueMember.FieldNumber + offset);
					}
				}
				if (this.subTypes != null)
				{
					foreach (object obj2 in this.subTypes)
					{
						SubType subType = (SubType)obj2;
						MetaType.AssertValidFieldNumber(subType.FieldNumber + offset);
					}
				}
				if (this.fields != null)
				{
					foreach (object obj3 in this.fields)
					{
						ValueMember valueMember2 = (ValueMember)obj3;
						valueMember2.FieldNumber += offset;
					}
				}
				if (this.subTypes != null)
				{
					foreach (object obj4 in this.subTypes)
					{
						SubType subType2 = (SubType)obj4;
						subType2.FieldNumber += offset;
					}
				}
			}
			finally
			{
				this.model.ReleaseLock(opaqueToken);
			}
		}

		// Token: 0x06007F17 RID: 32535 RVA: 0x00257D70 File Offset: 0x00257D70
		internal static void AssertValidFieldNumber(int fieldNumber)
		{
			if (fieldNumber < 1)
			{
				throw new ArgumentOutOfRangeException("fieldNumber");
			}
		}

		// Token: 0x04003CA6 RID: 15526
		private MetaType baseType;

		// Token: 0x04003CA7 RID: 15527
		private BasicList subTypes;

		// Token: 0x04003CA8 RID: 15528
		internal static readonly Type ienumerable = typeof(IEnumerable);

		// Token: 0x04003CA9 RID: 15529
		private CallbackSet callbacks;

		// Token: 0x04003CAA RID: 15530
		private string name;

		// Token: 0x04003CAB RID: 15531
		private MethodInfo factory;

		// Token: 0x04003CAC RID: 15532
		private readonly RuntimeTypeModel model;

		// Token: 0x04003CAD RID: 15533
		private readonly Type type;

		// Token: 0x04003CAE RID: 15534
		private IProtoTypeSerializer serializer;

		// Token: 0x04003CAF RID: 15535
		private Type constructType;

		// Token: 0x04003CB0 RID: 15536
		private Type surrogate;

		// Token: 0x04003CB1 RID: 15537
		private readonly BasicList fields = new BasicList();

		// Token: 0x04003CB2 RID: 15538
		private const ushort OPTIONS_Pending = 1;

		// Token: 0x04003CB3 RID: 15539
		private const ushort OPTIONS_EnumPassThru = 2;

		// Token: 0x04003CB4 RID: 15540
		private const ushort OPTIONS_Frozen = 4;

		// Token: 0x04003CB5 RID: 15541
		private const ushort OPTIONS_PrivateOnApi = 8;

		// Token: 0x04003CB6 RID: 15542
		private const ushort OPTIONS_SkipConstructor = 16;

		// Token: 0x04003CB7 RID: 15543
		private const ushort OPTIONS_AsReferenceDefault = 32;

		// Token: 0x04003CB8 RID: 15544
		private const ushort OPTIONS_AutoTuple = 64;

		// Token: 0x04003CB9 RID: 15545
		private const ushort OPTIONS_IgnoreListHandling = 128;

		// Token: 0x04003CBA RID: 15546
		private const ushort OPTIONS_IsGroup = 256;

		// Token: 0x04003CBB RID: 15547
		private volatile ushort flags;

		// Token: 0x02001179 RID: 4473
		internal sealed class Comparer : IComparer, IComparer<MetaType>
		{
			// Token: 0x06009370 RID: 37744 RVA: 0x002C30A4 File Offset: 0x002C30A4
			public int Compare(object x, object y)
			{
				return this.Compare(x as MetaType, y as MetaType);
			}

			// Token: 0x06009371 RID: 37745 RVA: 0x002C30B8 File Offset: 0x002C30B8
			public int Compare(MetaType x, MetaType y)
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
				return string.Compare(x.GetSchemaTypeName(), y.GetSchemaTypeName(), StringComparison.Ordinal);
			}

			// Token: 0x04004B51 RID: 19281
			public static readonly MetaType.Comparer Default = new MetaType.Comparer();
		}

		// Token: 0x0200117A RID: 4474
		[Flags]
		internal enum AttributeFamily
		{
			// Token: 0x04004B53 RID: 19283
			None = 0,
			// Token: 0x04004B54 RID: 19284
			ProtoBuf = 1,
			// Token: 0x04004B55 RID: 19285
			DataContractSerialier = 2,
			// Token: 0x04004B56 RID: 19286
			XmlSerializer = 4,
			// Token: 0x04004B57 RID: 19287
			AutoTuple = 8
		}
	}
}
