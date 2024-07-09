using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using ProtoBuf.Compiler;
using ProtoBuf.Serializers;

namespace ProtoBuf.Meta
{
	// Token: 0x02000C76 RID: 3190
	[ComVisible(true)]
	public sealed class RuntimeTypeModel : TypeModel
	{
		// Token: 0x06007F19 RID: 32537 RVA: 0x00257D98 File Offset: 0x00257D98
		private bool GetOption(ushort option)
		{
			return (this.options & option) == option;
		}

		// Token: 0x06007F1A RID: 32538 RVA: 0x00257DA8 File Offset: 0x00257DA8
		private void SetOption(ushort option, bool value)
		{
			if (value)
			{
				this.options |= option;
				return;
			}
			this.options &= ~option;
		}

		// Token: 0x17001BA2 RID: 7074
		// (get) Token: 0x06007F1B RID: 32539 RVA: 0x00257DD4 File Offset: 0x00257DD4
		// (set) Token: 0x06007F1C RID: 32540 RVA: 0x00257DE0 File Offset: 0x00257DE0
		public bool InferTagFromNameDefault
		{
			get
			{
				return this.GetOption(1);
			}
			set
			{
				this.SetOption(1, value);
			}
		}

		// Token: 0x17001BA3 RID: 7075
		// (get) Token: 0x06007F1D RID: 32541 RVA: 0x00257DEC File Offset: 0x00257DEC
		// (set) Token: 0x06007F1E RID: 32542 RVA: 0x00257DFC File Offset: 0x00257DFC
		public bool AutoAddProtoContractTypesOnly
		{
			get
			{
				return this.GetOption(128);
			}
			set
			{
				this.SetOption(128, value);
			}
		}

		// Token: 0x17001BA4 RID: 7076
		// (get) Token: 0x06007F1F RID: 32543 RVA: 0x00257E0C File Offset: 0x00257E0C
		// (set) Token: 0x06007F20 RID: 32544 RVA: 0x00257E18 File Offset: 0x00257E18
		public bool UseImplicitZeroDefaults
		{
			get
			{
				return this.GetOption(32);
			}
			set
			{
				if (!value && this.GetOption(2))
				{
					throw new InvalidOperationException("UseImplicitZeroDefaults cannot be disabled on the default model");
				}
				this.SetOption(32, value);
			}
		}

		// Token: 0x17001BA5 RID: 7077
		// (get) Token: 0x06007F21 RID: 32545 RVA: 0x00257E40 File Offset: 0x00257E40
		// (set) Token: 0x06007F22 RID: 32546 RVA: 0x00257E4C File Offset: 0x00257E4C
		public bool AllowParseableTypes
		{
			get
			{
				return this.GetOption(64);
			}
			set
			{
				this.SetOption(64, value);
			}
		}

		// Token: 0x17001BA6 RID: 7078
		// (get) Token: 0x06007F23 RID: 32547 RVA: 0x00257E58 File Offset: 0x00257E58
		// (set) Token: 0x06007F24 RID: 32548 RVA: 0x00257E68 File Offset: 0x00257E68
		public bool IncludeDateTimeKind
		{
			get
			{
				return this.GetOption(256);
			}
			set
			{
				this.SetOption(256, value);
			}
		}

		// Token: 0x17001BA7 RID: 7079
		// (get) Token: 0x06007F25 RID: 32549 RVA: 0x00257E78 File Offset: 0x00257E78
		// (set) Token: 0x06007F26 RID: 32550 RVA: 0x00257E88 File Offset: 0x00257E88
		public bool InternStrings
		{
			get
			{
				return !this.GetOption(512);
			}
			set
			{
				this.SetOption(512, !value);
			}
		}

		// Token: 0x06007F27 RID: 32551 RVA: 0x00257E9C File Offset: 0x00257E9C
		protected internal override bool SerializeDateTimeKind()
		{
			return this.GetOption(256);
		}

		// Token: 0x17001BA8 RID: 7080
		// (get) Token: 0x06007F28 RID: 32552 RVA: 0x00257EAC File Offset: 0x00257EAC
		public static RuntimeTypeModel Default
		{
			get
			{
				return RuntimeTypeModel.Singleton.Value;
			}
		}

		// Token: 0x06007F29 RID: 32553 RVA: 0x00257EB4 File Offset: 0x00257EB4
		public IEnumerable GetTypes()
		{
			return this.types;
		}

		// Token: 0x06007F2A RID: 32554 RVA: 0x00257EBC File Offset: 0x00257EBC
		public override string GetSchema(Type type, ProtoSyntax syntax)
		{
			BasicList basicList = new BasicList();
			MetaType metaType = null;
			bool flag = false;
			if (type == null)
			{
				foreach (object obj in this.types)
				{
					MetaType metaType2 = (MetaType)obj;
					MetaType surrogateOrBaseOrSelf = metaType2.GetSurrogateOrBaseOrSelf(false);
					if (!basicList.Contains(surrogateOrBaseOrSelf))
					{
						basicList.Add(surrogateOrBaseOrSelf);
						this.CascadeDependents(basicList, surrogateOrBaseOrSelf);
					}
				}
			}
			else
			{
				Type underlyingType = Helpers.GetUnderlyingType(type);
				if (underlyingType != null)
				{
					type = underlyingType;
				}
				WireType wireType;
				flag = (ValueMember.TryGetCoreSerializer(this, DataFormat.Default, type, out wireType, false, false, false, false) != null);
				if (!flag)
				{
					int num = this.FindOrAddAuto(type, false, false, false);
					if (num < 0)
					{
						throw new ArgumentException("The type specified is not a contract-type", "type");
					}
					metaType = ((MetaType)this.types[num]).GetSurrogateOrBaseOrSelf(false);
					basicList.Add(metaType);
					this.CascadeDependents(basicList, metaType);
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			string text = null;
			if (!flag)
			{
				IEnumerable enumerable = (metaType == null) ? this.types : basicList;
				foreach (object obj2 in enumerable)
				{
					MetaType metaType3 = (MetaType)obj2;
					if (!metaType3.IsList)
					{
						string @namespace = metaType3.Type.Namespace;
						if (!string.IsNullOrEmpty(@namespace) && !@namespace.StartsWith("System."))
						{
							if (text == null)
							{
								text = @namespace;
							}
							else if (!(text == @namespace))
							{
								text = null;
								break;
							}
						}
					}
				}
			}
			if (syntax != ProtoSyntax.Proto2)
			{
				if (syntax != ProtoSyntax.Proto3)
				{
					throw new ArgumentOutOfRangeException("syntax");
				}
				stringBuilder.AppendLine("syntax = \"proto3\";");
			}
			else
			{
				stringBuilder.AppendLine("syntax = \"proto2\";");
			}
			if (!string.IsNullOrEmpty(text))
			{
				stringBuilder.Append("package ").Append(text).Append(';');
				Helpers.AppendLine(stringBuilder);
			}
			RuntimeTypeModel.CommonImports commonImports = RuntimeTypeModel.CommonImports.None;
			StringBuilder stringBuilder2 = new StringBuilder();
			MetaType[] array = new MetaType[basicList.Count];
			basicList.CopyTo(array, 0);
			Array.Sort<MetaType>(array, MetaType.Comparer.Default);
			if (flag)
			{
				Helpers.AppendLine(stringBuilder2).Append("message ").Append(type.Name).Append(" {");
				MetaType.NewLine(stringBuilder2, 1).Append((syntax == ProtoSyntax.Proto2) ? "optional " : "").Append(this.GetSchemaTypeName(type, DataFormat.Default, false, false, ref commonImports)).Append(" value = 1;");
				Helpers.AppendLine(stringBuilder2).Append('}');
			}
			else
			{
				foreach (MetaType metaType4 in array)
				{
					if (!metaType4.IsList || metaType4 == metaType)
					{
						metaType4.WriteSchema(stringBuilder2, 0, ref commonImports, syntax);
					}
				}
			}
			if ((commonImports & RuntimeTypeModel.CommonImports.Bcl) != RuntimeTypeModel.CommonImports.None)
			{
				stringBuilder.Append("import \"protobuf-net/bcl.proto\"; // schema for protobuf-net's handling of core .NET types");
				Helpers.AppendLine(stringBuilder);
			}
			if ((commonImports & RuntimeTypeModel.CommonImports.Protogen) != RuntimeTypeModel.CommonImports.None)
			{
				stringBuilder.Append("import \"protobuf-net/protogen.proto\"; // custom protobuf-net options");
				Helpers.AppendLine(stringBuilder);
			}
			if ((commonImports & RuntimeTypeModel.CommonImports.Timestamp) != RuntimeTypeModel.CommonImports.None)
			{
				stringBuilder.Append("import \"google/protobuf/timestamp.proto\";");
				Helpers.AppendLine(stringBuilder);
			}
			if ((commonImports & RuntimeTypeModel.CommonImports.Duration) != RuntimeTypeModel.CommonImports.None)
			{
				stringBuilder.Append("import \"google/protobuf/duration.proto\";");
				Helpers.AppendLine(stringBuilder);
			}
			return Helpers.AppendLine(stringBuilder.Append(stringBuilder2)).ToString();
		}

		// Token: 0x06007F2B RID: 32555 RVA: 0x00258270 File Offset: 0x00258270
		private void CascadeDependents(BasicList list, MetaType metaType)
		{
			if (metaType.IsList)
			{
				Type listItemType = TypeModel.GetListItemType(this, metaType.Type);
				this.TryGetCoreSerializer(list, listItemType);
				return;
			}
			if (metaType.IsAutoTuple)
			{
				MemberInfo[] array;
				if (MetaType.ResolveTupleConstructor(metaType.Type, out array) != null)
				{
					for (int i = 0; i < array.Length; i++)
					{
						Type itemType = null;
						if (array[i] is PropertyInfo)
						{
							itemType = ((PropertyInfo)array[i]).PropertyType;
						}
						else if (array[i] is FieldInfo)
						{
							itemType = ((FieldInfo)array[i]).FieldType;
						}
						this.TryGetCoreSerializer(list, itemType);
					}
				}
			}
			else
			{
				foreach (object obj in metaType.Fields)
				{
					ValueMember valueMember = (ValueMember)obj;
					Type type = valueMember.ItemType;
					if (valueMember.IsMap)
					{
						Type type2;
						Type type3;
						valueMember.ResolveMapTypes(out type2, out type3, out type);
					}
					if (type == null)
					{
						type = valueMember.MemberType;
					}
					this.TryGetCoreSerializer(list, type);
				}
			}
			foreach (Type itemType2 in metaType.GetAllGenericArguments())
			{
				this.TryGetCoreSerializer(list, itemType2);
			}
			MetaType metaType2;
			if (metaType.HasSubtypes)
			{
				foreach (SubType subType in metaType.GetSubtypes())
				{
					metaType2 = subType.DerivedType.GetSurrogateOrSelf();
					if (!list.Contains(metaType2))
					{
						list.Add(metaType2);
						this.CascadeDependents(list, metaType2);
					}
				}
			}
			metaType2 = metaType.BaseType;
			if (metaType2 != null)
			{
				metaType2 = metaType2.GetSurrogateOrSelf();
			}
			if (metaType2 != null && !list.Contains(metaType2))
			{
				list.Add(metaType2);
				this.CascadeDependents(list, metaType2);
			}
		}

		// Token: 0x06007F2C RID: 32556 RVA: 0x002584B0 File Offset: 0x002584B0
		private void TryGetCoreSerializer(BasicList list, Type itemType)
		{
			WireType wireType;
			IProtoSerializer protoSerializer = ValueMember.TryGetCoreSerializer(this, DataFormat.Default, itemType, out wireType, false, false, false, false);
			if (protoSerializer != null)
			{
				return;
			}
			int num = this.FindOrAddAuto(itemType, false, false, false);
			if (num < 0)
			{
				return;
			}
			MetaType surrogateOrBaseOrSelf = ((MetaType)this.types[num]).GetSurrogateOrBaseOrSelf(false);
			if (list.Contains(surrogateOrBaseOrSelf))
			{
				return;
			}
			list.Add(surrogateOrBaseOrSelf);
			this.CascadeDependents(list, surrogateOrBaseOrSelf);
		}

		// Token: 0x06007F2D RID: 32557 RVA: 0x00258520 File Offset: 0x00258520
		public static RuntimeTypeModel Create(string name = null)
		{
			return new RuntimeTypeModel(false);
		}

		// Token: 0x06007F2E RID: 32558 RVA: 0x00258528 File Offset: 0x00258528
		private RuntimeTypeModel(bool isDefault)
		{
			this.AutoAddMissingTypes = true;
			this.UseImplicitZeroDefaults = true;
			this.SetOption(2, isDefault);
			try
			{
				this.AutoCompile = RuntimeTypeModel.EnableAutoCompile();
			}
			catch
			{
			}
		}

		// Token: 0x06007F2F RID: 32559 RVA: 0x002585A0 File Offset: 0x002585A0
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static bool EnableAutoCompile()
		{
			bool result;
			try
			{
				DynamicMethod dynamicMethod = new DynamicMethod("CheckCompilerAvailable", typeof(bool), new Type[]
				{
					typeof(int)
				});
				ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Ldc_I4, 42);
				ilgenerator.Emit(OpCodes.Ceq);
				ilgenerator.Emit(OpCodes.Ret);
				Predicate<int> predicate = (Predicate<int>)dynamicMethod.CreateDelegate(typeof(Predicate<int>));
				result = predicate(42);
			}
			catch (Exception ex)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x17001BA9 RID: 7081
		public MetaType this[Type type]
		{
			get
			{
				return (MetaType)this.types[this.FindOrAddAuto(type, true, false, false)];
			}
		}

		// Token: 0x06007F31 RID: 32561 RVA: 0x00258664 File Offset: 0x00258664
		internal MetaType FindWithoutAdd(Type type)
		{
			foreach (object obj in this.types)
			{
				MetaType metaType = (MetaType)obj;
				if (metaType.Type == type)
				{
					if (metaType.Pending)
					{
						this.WaitOnLock(metaType);
					}
					return metaType;
				}
			}
			Type type2 = TypeModel.ResolveProxies(type);
			if (!(type2 == null))
			{
				return this.FindWithoutAdd(type2);
			}
			return null;
		}

		// Token: 0x06007F32 RID: 32562 RVA: 0x002586E0 File Offset: 0x002586E0
		private static bool MetaTypeFinderImpl(object value, object ctx)
		{
			return ((MetaType)value).Type == (Type)ctx;
		}

		// Token: 0x06007F33 RID: 32563 RVA: 0x002586F8 File Offset: 0x002586F8
		private static bool BasicTypeFinderImpl(object value, object ctx)
		{
			return ((RuntimeTypeModel.BasicType)value).Type == (Type)ctx;
		}

		// Token: 0x06007F34 RID: 32564 RVA: 0x00258710 File Offset: 0x00258710
		private void WaitOnLock(MetaType type)
		{
			int opaqueToken = 0;
			try
			{
				this.TakeLock(ref opaqueToken);
			}
			finally
			{
				this.ReleaseLock(opaqueToken);
			}
		}

		// Token: 0x06007F35 RID: 32565 RVA: 0x00258744 File Offset: 0x00258744
		internal IProtoSerializer TryGetBasicTypeSerializer(Type type)
		{
			int num = this.basicTypes.IndexOf(RuntimeTypeModel.BasicTypeFinder, type);
			if (num >= 0)
			{
				return ((RuntimeTypeModel.BasicType)this.basicTypes[num]).Serializer;
			}
			BasicList obj = this.basicTypes;
			IProtoSerializer result;
			lock (obj)
			{
				num = this.basicTypes.IndexOf(RuntimeTypeModel.BasicTypeFinder, type);
				if (num >= 0)
				{
					result = ((RuntimeTypeModel.BasicType)this.basicTypes[num]).Serializer;
				}
				else
				{
					MetaType.AttributeFamily contractFamily = MetaType.GetContractFamily(this, type, null);
					WireType wireType;
					IProtoSerializer protoSerializer = (contractFamily == MetaType.AttributeFamily.None) ? ValueMember.TryGetCoreSerializer(this, DataFormat.Default, type, out wireType, false, false, false, false) : null;
					if (protoSerializer != null)
					{
						this.basicTypes.Add(new RuntimeTypeModel.BasicType(type, protoSerializer));
					}
					result = protoSerializer;
				}
			}
			return result;
		}

		// Token: 0x06007F36 RID: 32566 RVA: 0x00258830 File Offset: 0x00258830
		internal int FindOrAddAuto(Type type, bool demand, bool addWithContractOnly, bool addEvenIfAutoDisabled)
		{
			int num = this.types.IndexOf(RuntimeTypeModel.MetaTypeFinder, type);
			if (num >= 0)
			{
				MetaType metaType = (MetaType)this.types[num];
				if (metaType.Pending)
				{
					this.WaitOnLock(metaType);
				}
				return num;
			}
			bool flag = this.AutoAddMissingTypes || addEvenIfAutoDisabled;
			if (Helpers.IsEnum(type) || this.TryGetBasicTypeSerializer(type) == null)
			{
				Type type2 = TypeModel.ResolveProxies(type);
				if (type2 != null && type2 != type)
				{
					num = this.types.IndexOf(RuntimeTypeModel.MetaTypeFinder, type2);
					type = type2;
				}
				if (num < 0)
				{
					int opaqueToken = 0;
					bool flag2 = false;
					try
					{
						this.TakeLock(ref opaqueToken);
						MetaType metaType;
						if ((metaType = this.RecogniseCommonTypes(type)) == null)
						{
							MetaType.AttributeFamily contractFamily = MetaType.GetContractFamily(this, type, null);
							if (contractFamily == MetaType.AttributeFamily.AutoTuple)
							{
								addEvenIfAutoDisabled = (flag = true);
							}
							if (!flag || (!Helpers.IsEnum(type) && addWithContractOnly && contractFamily == MetaType.AttributeFamily.None))
							{
								if (demand)
								{
									TypeModel.ThrowUnexpectedType(type);
								}
								return num;
							}
							metaType = this.Create(type);
						}
						metaType.Pending = true;
						int num2 = this.types.IndexOf(RuntimeTypeModel.MetaTypeFinder, type);
						if (num2 < 0)
						{
							this.ThrowIfFrozen();
							num = this.types.Add(metaType);
							flag2 = true;
						}
						else
						{
							num = num2;
						}
						if (flag2)
						{
							metaType.ApplyDefaultBehaviour();
							metaType.Pending = false;
						}
					}
					finally
					{
						this.ReleaseLock(opaqueToken);
						if (flag2)
						{
							base.ResetKeyCache();
						}
					}
					return num;
				}
				return num;
			}
			if (flag && !addWithContractOnly)
			{
				throw MetaType.InbuiltType(type);
			}
			return -1;
		}

		// Token: 0x06007F37 RID: 32567 RVA: 0x002589E0 File Offset: 0x002589E0
		private MetaType RecogniseCommonTypes(Type type)
		{
			return null;
		}

		// Token: 0x06007F38 RID: 32568 RVA: 0x002589E4 File Offset: 0x002589E4
		private MetaType Create(Type type)
		{
			this.ThrowIfFrozen();
			return new MetaType(this, type, this.defaultFactory);
		}

		// Token: 0x06007F39 RID: 32569 RVA: 0x002589FC File Offset: 0x002589FC
		public MetaType Add(Type type, bool applyDefaultBehaviour)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			MetaType metaType = this.FindWithoutAdd(type);
			if (metaType != null)
			{
				return metaType;
			}
			int opaqueToken = 0;
			if (type.IsInterface && base.MapType(MetaType.ienumerable).IsAssignableFrom(type) && TypeModel.GetListItemType(this, type) == null)
			{
				throw new ArgumentException("IEnumerable[<T>] data cannot be used as a meta-type unless an Add method can be resolved");
			}
			try
			{
				metaType = this.RecogniseCommonTypes(type);
				if (metaType != null)
				{
					if (!applyDefaultBehaviour)
					{
						throw new ArgumentException("Default behaviour must be observed for certain types with special handling; " + type.FullName, "applyDefaultBehaviour");
					}
					applyDefaultBehaviour = false;
				}
				if (metaType == null)
				{
					metaType = this.Create(type);
				}
				metaType.Pending = true;
				this.TakeLock(ref opaqueToken);
				if (this.FindWithoutAdd(type) != null)
				{
					throw new ArgumentException("Duplicate type", "type");
				}
				this.ThrowIfFrozen();
				this.types.Add(metaType);
				if (applyDefaultBehaviour)
				{
					metaType.ApplyDefaultBehaviour();
				}
				metaType.Pending = false;
			}
			finally
			{
				this.ReleaseLock(opaqueToken);
				base.ResetKeyCache();
			}
			return metaType;
		}

		// Token: 0x17001BAA RID: 7082
		// (get) Token: 0x06007F3A RID: 32570 RVA: 0x00258B28 File Offset: 0x00258B28
		// (set) Token: 0x06007F3B RID: 32571 RVA: 0x00258B34 File Offset: 0x00258B34
		public bool AutoCompile
		{
			get
			{
				return this.GetOption(16);
			}
			set
			{
				this.SetOption(16, value);
			}
		}

		// Token: 0x17001BAB RID: 7083
		// (get) Token: 0x06007F3C RID: 32572 RVA: 0x00258B40 File Offset: 0x00258B40
		// (set) Token: 0x06007F3D RID: 32573 RVA: 0x00258B4C File Offset: 0x00258B4C
		public bool AutoAddMissingTypes
		{
			get
			{
				return this.GetOption(8);
			}
			set
			{
				if (!value && this.GetOption(2))
				{
					throw new InvalidOperationException("The default model must allow missing types");
				}
				this.ThrowIfFrozen();
				this.SetOption(8, value);
			}
		}

		// Token: 0x06007F3E RID: 32574 RVA: 0x00258B7C File Offset: 0x00258B7C
		private void ThrowIfFrozen()
		{
			if (this.GetOption(4))
			{
				throw new InvalidOperationException("The model cannot be changed once frozen");
			}
		}

		// Token: 0x06007F3F RID: 32575 RVA: 0x00258B98 File Offset: 0x00258B98
		public void Freeze()
		{
			if (this.GetOption(2))
			{
				throw new InvalidOperationException("The default model cannot be frozen");
			}
			this.SetOption(4, true);
		}

		// Token: 0x06007F40 RID: 32576 RVA: 0x00258BBC File Offset: 0x00258BBC
		protected override int GetKeyImpl(Type type)
		{
			return this.GetKey(type, false, true);
		}

		// Token: 0x06007F41 RID: 32577 RVA: 0x00258BC8 File Offset: 0x00258BC8
		internal int GetKey(Type type, bool demand, bool getBaseKey)
		{
			int result;
			try
			{
				int num = this.FindOrAddAuto(type, demand, true, false);
				if (num >= 0)
				{
					MetaType metaType = (MetaType)this.types[num];
					if (getBaseKey)
					{
						metaType = MetaType.GetRootType(metaType);
						num = this.FindOrAddAuto(metaType.Type, true, true, false);
					}
				}
				result = num;
			}
			catch (NotSupportedException)
			{
				throw;
			}
			catch (Exception ex)
			{
				if (ex.Message.IndexOf(type.FullName) >= 0)
				{
					throw;
				}
				throw new ProtoException(ex.Message + " (" + type.FullName + ")", ex);
			}
			return result;
		}

		// Token: 0x06007F42 RID: 32578 RVA: 0x00258C7C File Offset: 0x00258C7C
		protected internal override void Serialize(int key, object value, ProtoWriter dest)
		{
			((MetaType)this.types[key]).Serializer.Write(value, dest);
		}

		// Token: 0x06007F43 RID: 32579 RVA: 0x00258C9C File Offset: 0x00258C9C
		protected internal override object Deserialize(int key, object value, ProtoReader source)
		{
			IProtoSerializer serializer = ((MetaType)this.types[key]).Serializer;
			if (value == null && Helpers.IsValueType(serializer.ExpectedType))
			{
				if (serializer.RequiresOldValue)
				{
					value = Activator.CreateInstance(serializer.ExpectedType);
				}
				return serializer.Read(value, source);
			}
			return serializer.Read(value, source);
		}

		// Token: 0x06007F44 RID: 32580 RVA: 0x00258D04 File Offset: 0x00258D04
		internal ProtoSerializer GetSerializer(IProtoSerializer serializer, bool compiled)
		{
			if (serializer == null)
			{
				throw new ArgumentNullException("serializer");
			}
			if (compiled)
			{
				return CompilerContext.BuildSerializer(serializer, this);
			}
			return new ProtoSerializer(serializer.Write);
		}

		// Token: 0x06007F45 RID: 32581 RVA: 0x00258D34 File Offset: 0x00258D34
		public void CompileInPlace()
		{
			foreach (object obj in this.types)
			{
				MetaType metaType = (MetaType)obj;
				metaType.CompileInPlace();
			}
		}

		// Token: 0x06007F46 RID: 32582 RVA: 0x00258D74 File Offset: 0x00258D74
		private void BuildAllSerializers()
		{
			for (int i = 0; i < this.types.Count; i++)
			{
				MetaType metaType = (MetaType)this.types[i];
				if (metaType.Serializer == null)
				{
					throw new InvalidOperationException("No serializer available for " + metaType.Type.Name);
				}
			}
		}

		// Token: 0x06007F47 RID: 32583 RVA: 0x00258DD8 File Offset: 0x00258DD8
		public TypeModel Compile()
		{
			RuntimeTypeModel.CompilerOptions compilerOptions = new RuntimeTypeModel.CompilerOptions();
			return this.Compile(compilerOptions);
		}

		// Token: 0x06007F48 RID: 32584 RVA: 0x00258DF8 File Offset: 0x00258DF8
		private static ILGenerator Override(TypeBuilder type, string name)
		{
			MethodInfo method = type.BaseType.GetMethod(name, BindingFlags.Instance | BindingFlags.NonPublic);
			ParameterInfo[] parameters = method.GetParameters();
			Type[] array = new Type[parameters.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = parameters[i].ParameterType;
			}
			MethodBuilder methodBuilder = type.DefineMethod(method.Name, (method.Attributes & ~MethodAttributes.Abstract) | MethodAttributes.Final, method.CallingConvention, method.ReturnType, array);
			ILGenerator ilgenerator = methodBuilder.GetILGenerator();
			type.DefineMethodOverride(methodBuilder, method);
			return ilgenerator;
		}

		// Token: 0x06007F49 RID: 32585 RVA: 0x00258E8C File Offset: 0x00258E8C
		public TypeModel Compile(string name, string path)
		{
			return this.Compile(new RuntimeTypeModel.CompilerOptions
			{
				TypeName = name,
				OutputPath = path
			});
		}

		// Token: 0x06007F4A RID: 32586 RVA: 0x00258EB8 File Offset: 0x00258EB8
		public TypeModel Compile(RuntimeTypeModel.CompilerOptions options)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			string text = options.TypeName;
			string outputPath = options.OutputPath;
			this.BuildAllSerializers();
			this.Freeze();
			bool flag = !string.IsNullOrEmpty(outputPath);
			if (string.IsNullOrEmpty(text))
			{
				if (flag)
				{
					throw new ArgumentNullException("typeName");
				}
				text = Guid.NewGuid().ToString();
			}
			string text2;
			string name;
			if (outputPath == null)
			{
				text2 = text;
				name = text2 + ".dll";
			}
			else
			{
				text2 = new FileInfo(Path.GetFileNameWithoutExtension(outputPath)).Name;
				name = text2 + Path.GetExtension(outputPath);
			}
			AssemblyName assemblyName = new AssemblyName();
			assemblyName.Name = text2;
			AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, flag ? AssemblyBuilderAccess.RunAndSave : AssemblyBuilderAccess.Run);
			ModuleBuilder module = flag ? assemblyBuilder.DefineDynamicModule(name, outputPath) : assemblyBuilder.DefineDynamicModule(name);
			this.WriteAssemblyAttributes(options, text2, assemblyBuilder);
			TypeBuilder typeBuilder = this.WriteBasicTypeModel(options, text, module);
			int num;
			bool hasInheritance;
			RuntimeTypeModel.SerializerPair[] methodPairs;
			CompilerContext.ILVersion ilVersion;
			this.WriteSerializers(options, text2, typeBuilder, out num, out hasInheritance, out methodPairs, out ilVersion);
			ILGenerator ilgenerator;
			int knownTypesCategory;
			FieldBuilder knownTypes;
			Type knownTypesLookupType;
			this.WriteGetKeyImpl(typeBuilder, hasInheritance, methodPairs, ilVersion, text2, out ilgenerator, out knownTypesCategory, out knownTypes, out knownTypesLookupType);
			ilgenerator = RuntimeTypeModel.Override(typeBuilder, "SerializeDateTimeKind");
			ilgenerator.Emit(this.IncludeDateTimeKind ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0);
			ilgenerator.Emit(OpCodes.Ret);
			CompilerContext ctx = this.WriteSerializeDeserialize(text2, typeBuilder, methodPairs, ilVersion, ref ilgenerator);
			this.WriteConstructors(typeBuilder, ref num, methodPairs, ref ilgenerator, knownTypesCategory, knownTypes, knownTypesLookupType, ctx);
			Type type = typeBuilder.CreateType();
			if (!string.IsNullOrEmpty(outputPath))
			{
				try
				{
					assemblyBuilder.Save(outputPath);
				}
				catch (IOException ex)
				{
					throw new IOException(outputPath + ", " + ex.Message, ex);
				}
			}
			return (TypeModel)Activator.CreateInstance(type);
		}

		// Token: 0x06007F4B RID: 32587 RVA: 0x002590B0 File Offset: 0x002590B0
		private void WriteConstructors(TypeBuilder type, ref int index, RuntimeTypeModel.SerializerPair[] methodPairs, ref ILGenerator il, int knownTypesCategory, FieldBuilder knownTypes, Type knownTypesLookupType, CompilerContext ctx)
		{
			type.DefineDefaultConstructor(MethodAttributes.Public);
			il = type.DefineTypeInitializer().GetILGenerator();
			switch (knownTypesCategory)
			{
			case 1:
				CompilerContext.LoadValue(il, this.types.Count);
				il.Emit(OpCodes.Newarr, ctx.MapType(typeof(Type)));
				index = 0;
				foreach (RuntimeTypeModel.SerializerPair serializerPair in methodPairs)
				{
					il.Emit(OpCodes.Dup);
					CompilerContext.LoadValue(il, index);
					il.Emit(OpCodes.Ldtoken, serializerPair.Type.Type);
					il.EmitCall(OpCodes.Call, ctx.MapType(typeof(Type)).GetMethod("GetTypeFromHandle"), null);
					il.Emit(OpCodes.Stelem_Ref);
					index++;
				}
				il.Emit(OpCodes.Stsfld, knownTypes);
				il.Emit(OpCodes.Ret);
				return;
			case 2:
			{
				CompilerContext.LoadValue(il, this.types.Count);
				il.Emit(OpCodes.Newobj, knownTypesLookupType.GetConstructor(new Type[]
				{
					base.MapType(typeof(int))
				}));
				il.Emit(OpCodes.Stsfld, knownTypes);
				int num = 0;
				foreach (RuntimeTypeModel.SerializerPair serializerPair2 in methodPairs)
				{
					il.Emit(OpCodes.Ldsfld, knownTypes);
					il.Emit(OpCodes.Ldtoken, serializerPair2.Type.Type);
					il.EmitCall(OpCodes.Call, ctx.MapType(typeof(Type)).GetMethod("GetTypeFromHandle"), null);
					int value = num++;
					int baseKey = serializerPair2.BaseKey;
					if (baseKey != serializerPair2.MetaKey)
					{
						value = -1;
						for (int k = 0; k < methodPairs.Length; k++)
						{
							if (methodPairs[k].BaseKey == baseKey && methodPairs[k].MetaKey == baseKey)
							{
								value = k;
								break;
							}
						}
					}
					CompilerContext.LoadValue(il, value);
					il.EmitCall(OpCodes.Callvirt, knownTypesLookupType.GetMethod("Add", new Type[]
					{
						base.MapType(typeof(Type)),
						base.MapType(typeof(int))
					}), null);
				}
				il.Emit(OpCodes.Ret);
				return;
			}
			case 3:
			{
				CompilerContext.LoadValue(il, this.types.Count);
				il.Emit(OpCodes.Newobj, knownTypesLookupType.GetConstructor(new Type[]
				{
					base.MapType(typeof(int))
				}));
				il.Emit(OpCodes.Stsfld, knownTypes);
				int num2 = 0;
				foreach (RuntimeTypeModel.SerializerPair serializerPair3 in methodPairs)
				{
					il.Emit(OpCodes.Ldsfld, knownTypes);
					il.Emit(OpCodes.Ldtoken, serializerPair3.Type.Type);
					il.EmitCall(OpCodes.Call, ctx.MapType(typeof(Type)).GetMethod("GetTypeFromHandle"), null);
					int value2 = num2++;
					int baseKey2 = serializerPair3.BaseKey;
					if (baseKey2 != serializerPair3.MetaKey)
					{
						value2 = -1;
						for (int m = 0; m < methodPairs.Length; m++)
						{
							if (methodPairs[m].BaseKey == baseKey2 && methodPairs[m].MetaKey == baseKey2)
							{
								value2 = m;
								break;
							}
						}
					}
					CompilerContext.LoadValue(il, value2);
					il.Emit(OpCodes.Box, base.MapType(typeof(int)));
					il.EmitCall(OpCodes.Callvirt, knownTypesLookupType.GetMethod("Add", new Type[]
					{
						base.MapType(typeof(object)),
						base.MapType(typeof(object))
					}), null);
				}
				il.Emit(OpCodes.Ret);
				return;
			}
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06007F4C RID: 32588 RVA: 0x0025950C File Offset: 0x0025950C
		private CompilerContext WriteSerializeDeserialize(string assemblyName, TypeBuilder type, RuntimeTypeModel.SerializerPair[] methodPairs, CompilerContext.ILVersion ilVersion, ref ILGenerator il)
		{
			il = RuntimeTypeModel.Override(type, "Serialize");
			CompilerContext compilerContext = new CompilerContext(il, false, true, methodPairs, this, ilVersion, assemblyName, base.MapType(typeof(object)), "Serialize " + type.Name);
			CodeLabel[] array = new CodeLabel[this.types.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = compilerContext.DefineLabel();
			}
			il.Emit(OpCodes.Ldarg_1);
			compilerContext.Switch(array);
			compilerContext.Return();
			for (int j = 0; j < array.Length; j++)
			{
				RuntimeTypeModel.SerializerPair serializerPair = methodPairs[j];
				compilerContext.MarkLabel(array[j]);
				il.Emit(OpCodes.Ldarg_2);
				compilerContext.CastFromObject(serializerPair.Type.Type);
				il.Emit(OpCodes.Ldarg_3);
				il.EmitCall(OpCodes.Call, serializerPair.Serialize, null);
				compilerContext.Return();
			}
			il = RuntimeTypeModel.Override(type, "Deserialize");
			compilerContext = new CompilerContext(il, false, false, methodPairs, this, ilVersion, assemblyName, base.MapType(typeof(object)), "Deserialize " + type.Name);
			for (int k = 0; k < array.Length; k++)
			{
				array[k] = compilerContext.DefineLabel();
			}
			il.Emit(OpCodes.Ldarg_1);
			compilerContext.Switch(array);
			compilerContext.LoadNullRef();
			compilerContext.Return();
			for (int l = 0; l < array.Length; l++)
			{
				RuntimeTypeModel.SerializerPair serializerPair2 = methodPairs[l];
				compilerContext.MarkLabel(array[l]);
				Type type2 = serializerPair2.Type.Type;
				if (Helpers.IsValueType(type2))
				{
					il.Emit(OpCodes.Ldarg_2);
					il.Emit(OpCodes.Ldarg_3);
					il.EmitCall(OpCodes.Call, RuntimeTypeModel.EmitBoxedSerializer(type, l, type2, methodPairs, this, ilVersion, assemblyName), null);
					compilerContext.Return();
				}
				else
				{
					il.Emit(OpCodes.Ldarg_2);
					compilerContext.CastFromObject(type2);
					il.Emit(OpCodes.Ldarg_3);
					il.EmitCall(OpCodes.Call, serializerPair2.Deserialize, null);
					compilerContext.Return();
				}
			}
			return compilerContext;
		}

		// Token: 0x06007F4D RID: 32589 RVA: 0x00259760 File Offset: 0x00259760
		private void WriteGetKeyImpl(TypeBuilder type, bool hasInheritance, RuntimeTypeModel.SerializerPair[] methodPairs, CompilerContext.ILVersion ilVersion, string assemblyName, out ILGenerator il, out int knownTypesCategory, out FieldBuilder knownTypes, out Type knownTypesLookupType)
		{
			il = RuntimeTypeModel.Override(type, "GetKeyImpl");
			CompilerContext compilerContext = new CompilerContext(il, false, false, methodPairs, this, ilVersion, assemblyName, this.MapType(typeof(Type), true), "GetKeyImpl");
			if (this.types.Count <= 20)
			{
				knownTypesCategory = 1;
				knownTypesLookupType = this.MapType(typeof(Type[]), true);
			}
			else
			{
				knownTypesLookupType = this.MapType(typeof(Dictionary<Type, int>), false);
				if (knownTypesLookupType == null)
				{
					knownTypesLookupType = this.MapType(typeof(Hashtable), true);
					knownTypesCategory = 3;
				}
				else
				{
					knownTypesCategory = 2;
				}
			}
			knownTypes = type.DefineField("knownTypes", knownTypesLookupType, FieldAttributes.Private | FieldAttributes.Static | FieldAttributes.InitOnly);
			switch (knownTypesCategory)
			{
			case 1:
				il.Emit(OpCodes.Ldsfld, knownTypes);
				il.Emit(OpCodes.Ldarg_1);
				il.EmitCall(OpCodes.Callvirt, base.MapType(typeof(IList)).GetMethod("IndexOf", new Type[]
				{
					base.MapType(typeof(object))
				}), null);
				if (hasInheritance)
				{
					il.DeclareLocal(base.MapType(typeof(int)));
					il.Emit(OpCodes.Dup);
					il.Emit(OpCodes.Stloc_0);
					BasicList basicList = new BasicList();
					int num = -1;
					int num2 = 0;
					while (num2 < methodPairs.Length && methodPairs[num2].MetaKey != methodPairs[num2].BaseKey)
					{
						if (num == methodPairs[num2].BaseKey)
						{
							basicList.Add(basicList[basicList.Count - 1]);
						}
						else
						{
							basicList.Add(compilerContext.DefineLabel());
							num = methodPairs[num2].BaseKey;
						}
						num2++;
					}
					CodeLabel[] array = new CodeLabel[basicList.Count];
					basicList.CopyTo(array, 0);
					compilerContext.Switch(array);
					il.Emit(OpCodes.Ldloc_0);
					il.Emit(OpCodes.Ret);
					num = -1;
					for (int i = array.Length - 1; i >= 0; i--)
					{
						if (num != methodPairs[i].BaseKey)
						{
							num = methodPairs[i].BaseKey;
							int value = -1;
							for (int j = array.Length; j < methodPairs.Length; j++)
							{
								if (methodPairs[j].BaseKey == num && methodPairs[j].MetaKey == num)
								{
									value = j;
									break;
								}
							}
							compilerContext.MarkLabel(array[i]);
							CompilerContext.LoadValue(il, value);
							il.Emit(OpCodes.Ret);
						}
					}
					return;
				}
				il.Emit(OpCodes.Ret);
				return;
			case 2:
			{
				LocalBuilder local = il.DeclareLocal(base.MapType(typeof(int)));
				Label label = il.DefineLabel();
				il.Emit(OpCodes.Ldsfld, knownTypes);
				il.Emit(OpCodes.Ldarg_1);
				il.Emit(OpCodes.Ldloca_S, local);
				il.EmitCall(OpCodes.Callvirt, knownTypesLookupType.GetMethod("TryGetValue", BindingFlags.Instance | BindingFlags.Public), null);
				il.Emit(OpCodes.Brfalse_S, label);
				il.Emit(OpCodes.Ldloc_S, local);
				il.Emit(OpCodes.Ret);
				il.MarkLabel(label);
				il.Emit(OpCodes.Ldc_I4_M1);
				il.Emit(OpCodes.Ret);
				return;
			}
			case 3:
			{
				Label label2 = il.DefineLabel();
				il.Emit(OpCodes.Ldsfld, knownTypes);
				il.Emit(OpCodes.Ldarg_1);
				il.EmitCall(OpCodes.Callvirt, knownTypesLookupType.GetProperty("Item").GetGetMethod(), null);
				il.Emit(OpCodes.Dup);
				il.Emit(OpCodes.Brfalse_S, label2);
				if (ilVersion == CompilerContext.ILVersion.Net1)
				{
					il.Emit(OpCodes.Unbox, base.MapType(typeof(int)));
					il.Emit(OpCodes.Ldobj, base.MapType(typeof(int)));
				}
				else
				{
					il.Emit(OpCodes.Unbox_Any, base.MapType(typeof(int)));
				}
				il.Emit(OpCodes.Ret);
				il.MarkLabel(label2);
				il.Emit(OpCodes.Pop);
				il.Emit(OpCodes.Ldc_I4_M1);
				il.Emit(OpCodes.Ret);
				return;
			}
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06007F4E RID: 32590 RVA: 0x00259C24 File Offset: 0x00259C24
		private void WriteSerializers(RuntimeTypeModel.CompilerOptions options, string assemblyName, TypeBuilder type, out int index, out bool hasInheritance, out RuntimeTypeModel.SerializerPair[] methodPairs, out CompilerContext.ILVersion ilVersion)
		{
			index = 0;
			hasInheritance = false;
			methodPairs = new RuntimeTypeModel.SerializerPair[this.types.Count];
			foreach (object obj in this.types)
			{
				MetaType metaType = (MetaType)obj;
				MethodBuilder methodBuilder = type.DefineMethod("Write", MethodAttributes.Private | MethodAttributes.Static, CallingConventions.Standard, base.MapType(typeof(void)), new Type[]
				{
					metaType.Type,
					base.MapType(typeof(ProtoWriter))
				});
				MethodBuilder methodBuilder2 = type.DefineMethod("Read", MethodAttributes.Private | MethodAttributes.Static, CallingConventions.Standard, metaType.Type, new Type[]
				{
					metaType.Type,
					base.MapType(typeof(ProtoReader))
				});
				RuntimeTypeModel.SerializerPair serializerPair = new RuntimeTypeModel.SerializerPair(this.GetKey(metaType.Type, true, false), this.GetKey(metaType.Type, true, true), metaType, methodBuilder, methodBuilder2, methodBuilder.GetILGenerator(), methodBuilder2.GetILGenerator());
				RuntimeTypeModel.SerializerPair[] array = methodPairs;
				int num = index;
				index = num + 1;
				array[num] = serializerPair;
				if (serializerPair.MetaKey != serializerPair.BaseKey)
				{
					hasInheritance = true;
				}
			}
			if (hasInheritance)
			{
				Array.Sort<RuntimeTypeModel.SerializerPair>(methodPairs);
			}
			ilVersion = CompilerContext.ILVersion.Net2;
			if (options.MetaDataVersion == 65536)
			{
				ilVersion = CompilerContext.ILVersion.Net1;
			}
			for (index = 0; index < methodPairs.Length; index++)
			{
				RuntimeTypeModel.SerializerPair serializerPair2 = methodPairs[index];
				CompilerContext compilerContext = new CompilerContext(serializerPair2.SerializeBody, true, true, methodPairs, this, ilVersion, assemblyName, serializerPair2.Type.Type, "SerializeImpl " + serializerPair2.Type.Type.Name);
				MemberInfo returnType = serializerPair2.Deserialize.ReturnType;
				compilerContext.CheckAccessibility(ref returnType);
				serializerPair2.Type.Serializer.EmitWrite(compilerContext, compilerContext.InputValue);
				compilerContext.Return();
				compilerContext = new CompilerContext(serializerPair2.DeserializeBody, true, false, methodPairs, this, ilVersion, assemblyName, serializerPair2.Type.Type, "DeserializeImpl " + serializerPair2.Type.Type.Name);
				serializerPair2.Type.Serializer.EmitRead(compilerContext, compilerContext.InputValue);
				if (!serializerPair2.Type.Serializer.ReturnsValue)
				{
					compilerContext.LoadValue(compilerContext.InputValue);
				}
				compilerContext.Return();
			}
		}

		// Token: 0x06007F4F RID: 32591 RVA: 0x00259E9C File Offset: 0x00259E9C
		private TypeBuilder WriteBasicTypeModel(RuntimeTypeModel.CompilerOptions options, string typeName, ModuleBuilder module)
		{
			Type type = base.MapType(typeof(TypeModel));
			TypeAttributes typeAttributes = (type.Attributes & ~TypeAttributes.Abstract) | TypeAttributes.Sealed;
			if (options.Accessibility == RuntimeTypeModel.Accessibility.Internal)
			{
				typeAttributes &= ~TypeAttributes.Public;
			}
			return module.DefineType(typeName, typeAttributes, type);
		}

		// Token: 0x06007F50 RID: 32592 RVA: 0x00259EF0 File Offset: 0x00259EF0
		private void WriteAssemblyAttributes(RuntimeTypeModel.CompilerOptions options, string assemblyName, AssemblyBuilder asm)
		{
			if (!string.IsNullOrEmpty(options.TargetFrameworkName))
			{
				Type type = null;
				try
				{
					type = this.GetType("System.Runtime.Versioning.TargetFrameworkAttribute", Helpers.GetAssembly(base.MapType(typeof(string))));
				}
				catch
				{
				}
				if (type != null)
				{
					PropertyInfo[] namedProperties;
					object[] propertyValues;
					if (string.IsNullOrEmpty(options.TargetFrameworkDisplayName))
					{
						namedProperties = new PropertyInfo[0];
						propertyValues = new object[0];
					}
					else
					{
						namedProperties = new PropertyInfo[]
						{
							type.GetProperty("FrameworkDisplayName")
						};
						propertyValues = new object[]
						{
							options.TargetFrameworkDisplayName
						};
					}
					CustomAttributeBuilder customAttribute = new CustomAttributeBuilder(type.GetConstructor(new Type[]
					{
						base.MapType(typeof(string))
					}), new object[]
					{
						options.TargetFrameworkName
					}, namedProperties, propertyValues);
					asm.SetCustomAttribute(customAttribute);
				}
			}
			Type type2 = null;
			try
			{
				type2 = base.MapType(typeof(InternalsVisibleToAttribute));
			}
			catch
			{
			}
			if (type2 != null)
			{
				BasicList basicList = new BasicList();
				BasicList basicList2 = new BasicList();
				foreach (object obj in this.types)
				{
					MetaType metaType = (MetaType)obj;
					Assembly assembly = Helpers.GetAssembly(metaType.Type);
					if (basicList2.IndexOfReference(assembly) < 0)
					{
						basicList2.Add(assembly);
						AttributeMap[] array = AttributeMap.Create(this, assembly);
						for (int i = 0; i < array.Length; i++)
						{
							if (!(array[i].AttributeType != type2))
							{
								object obj2;
								array[i].TryGet("AssemblyName", out obj2);
								string text = obj2 as string;
								if (!(text == assemblyName) && !string.IsNullOrEmpty(text) && basicList.IndexOfString(text) < 0)
								{
									basicList.Add(text);
									CustomAttributeBuilder customAttribute2 = new CustomAttributeBuilder(type2.GetConstructor(new Type[]
									{
										base.MapType(typeof(string))
									}), new object[]
									{
										text
									});
									asm.SetCustomAttribute(customAttribute2);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06007F51 RID: 32593 RVA: 0x0025A140 File Offset: 0x0025A140
		private static MethodBuilder EmitBoxedSerializer(TypeBuilder type, int i, Type valueType, RuntimeTypeModel.SerializerPair[] methodPairs, TypeModel model, CompilerContext.ILVersion ilVersion, string assemblyName)
		{
			MethodInfo deserialize = methodPairs[i].Deserialize;
			MethodBuilder methodBuilder = type.DefineMethod("_" + i.ToString(), MethodAttributes.Static, CallingConventions.Standard, model.MapType(typeof(object)), new Type[]
			{
				model.MapType(typeof(object)),
				model.MapType(typeof(ProtoReader))
			});
			CompilerContext compilerContext = new CompilerContext(methodBuilder.GetILGenerator(), true, false, methodPairs, model, ilVersion, assemblyName, model.MapType(typeof(object)), "BoxedSerializer " + valueType.Name);
			compilerContext.LoadValue(compilerContext.InputValue);
			CodeLabel label = compilerContext.DefineLabel();
			compilerContext.BranchIfFalse(label, true);
			compilerContext.LoadValue(compilerContext.InputValue);
			compilerContext.CastFromObject(valueType);
			compilerContext.LoadReaderWriter();
			compilerContext.EmitCall(deserialize);
			compilerContext.CastToObject(valueType);
			compilerContext.Return();
			compilerContext.MarkLabel(label);
			using (Local local = new Local(compilerContext, valueType))
			{
				compilerContext.LoadAddress(local, valueType, false);
				compilerContext.EmitCtor(valueType);
				compilerContext.LoadValue(local);
				compilerContext.LoadReaderWriter();
				compilerContext.EmitCall(deserialize);
				compilerContext.CastToObject(valueType);
				compilerContext.Return();
			}
			return methodBuilder;
		}

		// Token: 0x06007F52 RID: 32594 RVA: 0x0025A2A4 File Offset: 0x0025A2A4
		internal bool IsPrepared(Type type)
		{
			MetaType metaType = this.FindWithoutAdd(type);
			return metaType != null && metaType.IsPrepared();
		}

		// Token: 0x06007F53 RID: 32595 RVA: 0x0025A2CC File Offset: 0x0025A2CC
		internal EnumSerializer.EnumPair[] GetEnumMap(Type type)
		{
			int num = this.FindOrAddAuto(type, false, false, false);
			if (num >= 0)
			{
				return ((MetaType)this.types[num]).GetEnumMap();
			}
			return null;
		}

		// Token: 0x17001BAC RID: 7084
		// (get) Token: 0x06007F54 RID: 32596 RVA: 0x0025A308 File Offset: 0x0025A308
		// (set) Token: 0x06007F55 RID: 32597 RVA: 0x0025A310 File Offset: 0x0025A310
		public int MetadataTimeoutMilliseconds
		{
			get
			{
				return this.metadataTimeoutMilliseconds;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("MetadataTimeoutMilliseconds");
				}
				this.metadataTimeoutMilliseconds = value;
			}
		}

		// Token: 0x06007F56 RID: 32598 RVA: 0x0025A32C File Offset: 0x0025A32C
		internal void TakeLock(ref int opaqueToken)
		{
			opaqueToken = 0;
			if (Monitor.TryEnter(this.types, this.metadataTimeoutMilliseconds))
			{
				opaqueToken = this.GetContention();
				return;
			}
			this.AddContention();
			throw new TimeoutException("Timeout while inspecting metadata; this may indicate a deadlock. This can often be avoided by preparing necessary serializers during application initialization, rather than allowing multiple threads to perform the initial metadata inspection; please also see the LockContended event");
		}

		// Token: 0x06007F57 RID: 32599 RVA: 0x0025A360 File Offset: 0x0025A360
		private int GetContention()
		{
			return Interlocked.CompareExchange(ref this.contentionCounter, 0, 0);
		}

		// Token: 0x06007F58 RID: 32600 RVA: 0x0025A370 File Offset: 0x0025A370
		private void AddContention()
		{
			Interlocked.Increment(ref this.contentionCounter);
		}

		// Token: 0x06007F59 RID: 32601 RVA: 0x0025A380 File Offset: 0x0025A380
		internal void ReleaseLock(int opaqueToken)
		{
			if (opaqueToken != 0)
			{
				Monitor.Exit(this.types);
				if (opaqueToken != this.GetContention())
				{
					LockContentedEventHandler lockContended = this.LockContended;
					if (lockContended != null)
					{
						string stackTrace;
						try
						{
							throw new ProtoException();
						}
						catch (Exception ex)
						{
							stackTrace = ex.StackTrace;
						}
						lockContended(this, new LockContentedEventArgs(stackTrace));
					}
				}
			}
		}

		// Token: 0x1400005C RID: 92
		// (add) Token: 0x06007F5A RID: 32602 RVA: 0x0025A3E8 File Offset: 0x0025A3E8
		// (remove) Token: 0x06007F5B RID: 32603 RVA: 0x0025A424 File Offset: 0x0025A424
		public event LockContentedEventHandler LockContended;

		// Token: 0x06007F5C RID: 32604 RVA: 0x0025A460 File Offset: 0x0025A460
		internal void ResolveListTypes(Type type, ref Type itemType, ref Type defaultType)
		{
			if (type == null)
			{
				return;
			}
			if (Helpers.GetTypeCode(type) != ProtoTypeCode.Unknown)
			{
				return;
			}
			if (type.IsArray)
			{
				if (type.GetArrayRank() != 1)
				{
					throw new NotSupportedException("Multi-dimension arrays are supported");
				}
				itemType = type.GetElementType();
				if (itemType == base.MapType(typeof(byte)))
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
			else if (this[type].IgnoreListHandling)
			{
				return;
			}
			if (itemType == null)
			{
				itemType = TypeModel.GetListItemType(this, type);
			}
			if (itemType != null)
			{
				Type left = null;
				Type type3 = null;
				this.ResolveListTypes(itemType, ref left, ref type3);
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
					if (type.IsGenericType && type.GetGenericTypeDefinition() == base.MapType(typeof(IDictionary<, >)) && itemType == base.MapType(typeof(KeyValuePair<, >)).MakeGenericType(genericArguments = type.GetGenericArguments()))
					{
						defaultType = base.MapType(typeof(Dictionary<, >)).MakeGenericType(genericArguments);
					}
					else
					{
						defaultType = base.MapType(typeof(List<>)).MakeGenericType(new Type[]
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

		// Token: 0x06007F5D RID: 32605 RVA: 0x0025A650 File Offset: 0x0025A650
		internal string GetSchemaTypeName(Type effectiveType, DataFormat dataFormat, bool asReference, bool dynamicType, ref RuntimeTypeModel.CommonImports imports)
		{
			Type underlyingType = Helpers.GetUnderlyingType(effectiveType);
			if (underlyingType != null)
			{
				effectiveType = underlyingType;
			}
			if (effectiveType == base.MapType(typeof(byte[])))
			{
				return "bytes";
			}
			WireType wireType;
			IProtoSerializer protoSerializer = ValueMember.TryGetCoreSerializer(this, dataFormat, effectiveType, out wireType, false, false, false, false);
			if (protoSerializer == null)
			{
				if (asReference || dynamicType)
				{
					imports |= RuntimeTypeModel.CommonImports.Bcl;
					return ".bcl.NetObjectProxy";
				}
				return this[effectiveType].GetSurrogateOrBaseOrSelf(true).GetSchemaTypeName();
			}
			else
			{
				if (!(protoSerializer is ParseableSerializer))
				{
					ProtoTypeCode typeCode = Helpers.GetTypeCode(effectiveType);
					switch (typeCode)
					{
					case ProtoTypeCode.Boolean:
						return "bool";
					case ProtoTypeCode.Char:
					case ProtoTypeCode.Byte:
					case ProtoTypeCode.UInt16:
					case ProtoTypeCode.UInt32:
						if (dataFormat == DataFormat.FixedSize)
						{
							return "fixed32";
						}
						return "uint32";
					case ProtoTypeCode.SByte:
					case ProtoTypeCode.Int16:
					case ProtoTypeCode.Int32:
						if (dataFormat == DataFormat.ZigZag)
						{
							return "sint32";
						}
						if (dataFormat != DataFormat.FixedSize)
						{
							return "int32";
						}
						return "sfixed32";
					case ProtoTypeCode.Int64:
						if (dataFormat == DataFormat.ZigZag)
						{
							return "sint64";
						}
						if (dataFormat != DataFormat.FixedSize)
						{
							return "int64";
						}
						return "sfixed64";
					case ProtoTypeCode.UInt64:
						if (dataFormat == DataFormat.FixedSize)
						{
							return "fixed64";
						}
						return "uint64";
					case ProtoTypeCode.Single:
						return "float";
					case ProtoTypeCode.Double:
						return "double";
					case ProtoTypeCode.Decimal:
						imports |= RuntimeTypeModel.CommonImports.Bcl;
						return ".bcl.Decimal";
					case ProtoTypeCode.DateTime:
						if (dataFormat == DataFormat.FixedSize)
						{
							return "sint64";
						}
						if (dataFormat != DataFormat.WellKnown)
						{
							imports |= RuntimeTypeModel.CommonImports.Bcl;
							return ".bcl.DateTime";
						}
						imports |= RuntimeTypeModel.CommonImports.Timestamp;
						return ".google.protobuf.Timestamp";
					case (ProtoTypeCode)17:
						break;
					case ProtoTypeCode.String:
						if (asReference)
						{
							imports |= RuntimeTypeModel.CommonImports.Bcl;
						}
						if (!asReference)
						{
							return "string";
						}
						return ".bcl.NetObjectProxy";
					default:
						switch (typeCode)
						{
						case ProtoTypeCode.TimeSpan:
							if (dataFormat == DataFormat.FixedSize)
							{
								return "sint64";
							}
							if (dataFormat != DataFormat.WellKnown)
							{
								imports |= RuntimeTypeModel.CommonImports.Bcl;
								return ".bcl.TimeSpan";
							}
							imports |= RuntimeTypeModel.CommonImports.Duration;
							return ".google.protobuf.Duration";
						case ProtoTypeCode.Guid:
							imports |= RuntimeTypeModel.CommonImports.Bcl;
							return ".bcl.Guid";
						case ProtoTypeCode.Type:
							return "string";
						}
						break;
					}
					throw new NotSupportedException("No .proto map found for: " + effectiveType.FullName);
				}
				if (asReference)
				{
					imports |= RuntimeTypeModel.CommonImports.Bcl;
				}
				if (!asReference)
				{
					return "string";
				}
				return ".bcl.NetObjectProxy";
			}
		}

		// Token: 0x06007F5E RID: 32606 RVA: 0x0025A8B8 File Offset: 0x0025A8B8
		public void SetDefaultFactory(MethodInfo methodInfo)
		{
			this.VerifyFactory(methodInfo, null);
			this.defaultFactory = methodInfo;
		}

		// Token: 0x06007F5F RID: 32607 RVA: 0x0025A8CC File Offset: 0x0025A8CC
		internal void VerifyFactory(MethodInfo factory, Type type)
		{
			if (factory != null)
			{
				if (type != null && Helpers.IsValueType(type))
				{
					throw new InvalidOperationException();
				}
				if (!factory.IsStatic)
				{
					throw new ArgumentException("A factory-method must be static", "factory");
				}
				if (type != null && factory.ReturnType != type && factory.ReturnType != base.MapType(typeof(object)))
				{
					throw new ArgumentException("The factory-method must return object" + ((type == null) ? "" : (" or " + type.FullName)), "factory");
				}
				if (!CallbackSet.CheckCallbackParameters(this, factory))
				{
					throw new ArgumentException("Invalid factory signature in " + factory.DeclaringType.FullName + "." + factory.Name, "factory");
				}
			}
		}

		// Token: 0x1400005D RID: 93
		// (add) Token: 0x06007F60 RID: 32608 RVA: 0x0025A9D0 File Offset: 0x0025A9D0
		// (remove) Token: 0x06007F61 RID: 32609 RVA: 0x0025AA0C File Offset: 0x0025AA0C
		public event EventHandler<TypeAddedEventArgs> BeforeApplyDefaultBehaviour;

		// Token: 0x1400005E RID: 94
		// (add) Token: 0x06007F62 RID: 32610 RVA: 0x0025AA48 File Offset: 0x0025AA48
		// (remove) Token: 0x06007F63 RID: 32611 RVA: 0x0025AA84 File Offset: 0x0025AA84
		public event EventHandler<TypeAddedEventArgs> AfterApplyDefaultBehaviour;

		// Token: 0x06007F64 RID: 32612 RVA: 0x0025AAC0 File Offset: 0x0025AAC0
		internal static void OnBeforeApplyDefaultBehaviour(MetaType metaType, ref TypeAddedEventArgs args)
		{
			RuntimeTypeModel runtimeTypeModel = ((metaType != null) ? metaType.Model : null) as RuntimeTypeModel;
			RuntimeTypeModel.OnApplyDefaultBehaviour((runtimeTypeModel != null) ? runtimeTypeModel.BeforeApplyDefaultBehaviour : null, metaType, ref args);
		}

		// Token: 0x06007F65 RID: 32613 RVA: 0x0025AAF4 File Offset: 0x0025AAF4
		internal static void OnAfterApplyDefaultBehaviour(MetaType metaType, ref TypeAddedEventArgs args)
		{
			RuntimeTypeModel runtimeTypeModel = ((metaType != null) ? metaType.Model : null) as RuntimeTypeModel;
			RuntimeTypeModel.OnApplyDefaultBehaviour((runtimeTypeModel != null) ? runtimeTypeModel.AfterApplyDefaultBehaviour : null, metaType, ref args);
		}

		// Token: 0x06007F66 RID: 32614 RVA: 0x0025AB28 File Offset: 0x0025AB28
		private static void OnApplyDefaultBehaviour(EventHandler<TypeAddedEventArgs> handler, MetaType metaType, ref TypeAddedEventArgs args)
		{
			if (handler != null)
			{
				if (args == null)
				{
					args = new TypeAddedEventArgs(metaType);
				}
				handler(metaType.Model, args);
			}
		}

		// Token: 0x04003CBF RID: 15551
		private ushort options;

		// Token: 0x04003CC0 RID: 15552
		private const ushort OPTIONS_InferTagFromNameDefault = 1;

		// Token: 0x04003CC1 RID: 15553
		private const ushort OPTIONS_IsDefaultModel = 2;

		// Token: 0x04003CC2 RID: 15554
		private const ushort OPTIONS_Frozen = 4;

		// Token: 0x04003CC3 RID: 15555
		private const ushort OPTIONS_AutoAddMissingTypes = 8;

		// Token: 0x04003CC4 RID: 15556
		private const ushort OPTIONS_AutoCompile = 16;

		// Token: 0x04003CC5 RID: 15557
		private const ushort OPTIONS_UseImplicitZeroDefaults = 32;

		// Token: 0x04003CC6 RID: 15558
		private const ushort OPTIONS_AllowParseableTypes = 64;

		// Token: 0x04003CC7 RID: 15559
		private const ushort OPTIONS_AutoAddProtoContractTypesOnly = 128;

		// Token: 0x04003CC8 RID: 15560
		private const ushort OPTIONS_IncludeDateTimeKind = 256;

		// Token: 0x04003CC9 RID: 15561
		private const ushort OPTIONS_DoNotInternStrings = 512;

		// Token: 0x04003CCA RID: 15562
		private static readonly BasicList.MatchPredicate MetaTypeFinder = new BasicList.MatchPredicate(RuntimeTypeModel.MetaTypeFinderImpl);

		// Token: 0x04003CCB RID: 15563
		private static readonly BasicList.MatchPredicate BasicTypeFinder = new BasicList.MatchPredicate(RuntimeTypeModel.BasicTypeFinderImpl);

		// Token: 0x04003CCC RID: 15564
		private BasicList basicTypes = new BasicList();

		// Token: 0x04003CCD RID: 15565
		private readonly BasicList types = new BasicList();

		// Token: 0x04003CCE RID: 15566
		private const int KnownTypes_Array = 1;

		// Token: 0x04003CCF RID: 15567
		private const int KnownTypes_Dictionary = 2;

		// Token: 0x04003CD0 RID: 15568
		private const int KnownTypes_Hashtable = 3;

		// Token: 0x04003CD1 RID: 15569
		private const int KnownTypes_ArrayCutoff = 20;

		// Token: 0x04003CD2 RID: 15570
		private int metadataTimeoutMilliseconds = 5000;

		// Token: 0x04003CD3 RID: 15571
		private int contentionCounter = 1;

		// Token: 0x04003CD5 RID: 15573
		private MethodInfo defaultFactory;

		// Token: 0x0200117C RID: 4476
		private sealed class Singleton
		{
			// Token: 0x0600937D RID: 37757 RVA: 0x002C3340 File Offset: 0x002C3340
			private Singleton()
			{
			}

			// Token: 0x04004B61 RID: 19297
			internal static readonly RuntimeTypeModel Value = new RuntimeTypeModel(true);
		}

		// Token: 0x0200117D RID: 4477
		[Flags]
		internal enum CommonImports
		{
			// Token: 0x04004B63 RID: 19299
			None = 0,
			// Token: 0x04004B64 RID: 19300
			Bcl = 1,
			// Token: 0x04004B65 RID: 19301
			Timestamp = 2,
			// Token: 0x04004B66 RID: 19302
			Duration = 4,
			// Token: 0x04004B67 RID: 19303
			Protogen = 8
		}

		// Token: 0x0200117E RID: 4478
		private sealed class BasicType
		{
			// Token: 0x17001E88 RID: 7816
			// (get) Token: 0x0600937F RID: 37759 RVA: 0x002C3358 File Offset: 0x002C3358
			public Type Type
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x17001E89 RID: 7817
			// (get) Token: 0x06009380 RID: 37760 RVA: 0x002C3360 File Offset: 0x002C3360
			public IProtoSerializer Serializer
			{
				get
				{
					return this.serializer;
				}
			}

			// Token: 0x06009381 RID: 37761 RVA: 0x002C3368 File Offset: 0x002C3368
			public BasicType(Type type, IProtoSerializer serializer)
			{
				this.type = type;
				this.serializer = serializer;
			}

			// Token: 0x04004B68 RID: 19304
			private readonly Type type;

			// Token: 0x04004B69 RID: 19305
			private readonly IProtoSerializer serializer;
		}

		// Token: 0x0200117F RID: 4479
		internal sealed class SerializerPair : IComparable
		{
			// Token: 0x06009382 RID: 37762 RVA: 0x002C3380 File Offset: 0x002C3380
			int IComparable.CompareTo(object obj)
			{
				if (obj == null)
				{
					throw new ArgumentException("obj");
				}
				RuntimeTypeModel.SerializerPair serializerPair = (RuntimeTypeModel.SerializerPair)obj;
				if (this.BaseKey == this.MetaKey)
				{
					if (serializerPair.BaseKey == serializerPair.MetaKey)
					{
						return this.MetaKey.CompareTo(serializerPair.MetaKey);
					}
					return 1;
				}
				else
				{
					if (serializerPair.BaseKey == serializerPair.MetaKey)
					{
						return -1;
					}
					int num = this.BaseKey.CompareTo(serializerPair.BaseKey);
					if (num == 0)
					{
						num = this.MetaKey.CompareTo(serializerPair.MetaKey);
					}
					return num;
				}
			}

			// Token: 0x06009383 RID: 37763 RVA: 0x002C3428 File Offset: 0x002C3428
			public SerializerPair(int metaKey, int baseKey, MetaType type, MethodBuilder serialize, MethodBuilder deserialize, ILGenerator serializeBody, ILGenerator deserializeBody)
			{
				this.MetaKey = metaKey;
				this.BaseKey = baseKey;
				this.Serialize = serialize;
				this.Deserialize = deserialize;
				this.SerializeBody = serializeBody;
				this.DeserializeBody = deserializeBody;
				this.Type = type;
			}

			// Token: 0x04004B6A RID: 19306
			public readonly int MetaKey;

			// Token: 0x04004B6B RID: 19307
			public readonly int BaseKey;

			// Token: 0x04004B6C RID: 19308
			public readonly MetaType Type;

			// Token: 0x04004B6D RID: 19309
			public readonly MethodBuilder Serialize;

			// Token: 0x04004B6E RID: 19310
			public readonly MethodBuilder Deserialize;

			// Token: 0x04004B6F RID: 19311
			public readonly ILGenerator SerializeBody;

			// Token: 0x04004B70 RID: 19312
			public readonly ILGenerator DeserializeBody;
		}

		// Token: 0x02001180 RID: 4480
		public sealed class CompilerOptions
		{
			// Token: 0x06009384 RID: 37764 RVA: 0x002C3468 File Offset: 0x002C3468
			public void SetFrameworkOptions(MetaType from)
			{
				if (from == null)
				{
					throw new ArgumentNullException("from");
				}
				AttributeMap[] array = AttributeMap.Create(from.Model, Helpers.GetAssembly(from.Type));
				AttributeMap[] array2 = array;
				int i = 0;
				while (i < array2.Length)
				{
					AttributeMap attributeMap = array2[i];
					if (attributeMap.AttributeType.FullName == "System.Runtime.Versioning.TargetFrameworkAttribute")
					{
						object obj;
						if (attributeMap.TryGet("FrameworkName", out obj))
						{
							this.TargetFrameworkName = (string)obj;
						}
						if (attributeMap.TryGet("FrameworkDisplayName", out obj))
						{
							this.TargetFrameworkDisplayName = (string)obj;
							return;
						}
						break;
					}
					else
					{
						i++;
					}
				}
			}

			// Token: 0x17001E8A RID: 7818
			// (get) Token: 0x06009385 RID: 37765 RVA: 0x002C3518 File Offset: 0x002C3518
			// (set) Token: 0x06009386 RID: 37766 RVA: 0x002C3520 File Offset: 0x002C3520
			public string TargetFrameworkName
			{
				get
				{
					return this.targetFrameworkName;
				}
				set
				{
					this.targetFrameworkName = value;
				}
			}

			// Token: 0x17001E8B RID: 7819
			// (get) Token: 0x06009387 RID: 37767 RVA: 0x002C352C File Offset: 0x002C352C
			// (set) Token: 0x06009388 RID: 37768 RVA: 0x002C3534 File Offset: 0x002C3534
			public string TargetFrameworkDisplayName
			{
				get
				{
					return this.targetFrameworkDisplayName;
				}
				set
				{
					this.targetFrameworkDisplayName = value;
				}
			}

			// Token: 0x17001E8C RID: 7820
			// (get) Token: 0x06009389 RID: 37769 RVA: 0x002C3540 File Offset: 0x002C3540
			// (set) Token: 0x0600938A RID: 37770 RVA: 0x002C3548 File Offset: 0x002C3548
			public string TypeName
			{
				get
				{
					return this.typeName;
				}
				set
				{
					this.typeName = value;
				}
			}

			// Token: 0x17001E8D RID: 7821
			// (get) Token: 0x0600938B RID: 37771 RVA: 0x002C3554 File Offset: 0x002C3554
			// (set) Token: 0x0600938C RID: 37772 RVA: 0x002C355C File Offset: 0x002C355C
			public string OutputPath
			{
				get
				{
					return this.outputPath;
				}
				set
				{
					this.outputPath = value;
				}
			}

			// Token: 0x17001E8E RID: 7822
			// (get) Token: 0x0600938D RID: 37773 RVA: 0x002C3568 File Offset: 0x002C3568
			// (set) Token: 0x0600938E RID: 37774 RVA: 0x002C3570 File Offset: 0x002C3570
			public string ImageRuntimeVersion
			{
				get
				{
					return this.imageRuntimeVersion;
				}
				set
				{
					this.imageRuntimeVersion = value;
				}
			}

			// Token: 0x17001E8F RID: 7823
			// (get) Token: 0x0600938F RID: 37775 RVA: 0x002C357C File Offset: 0x002C357C
			// (set) Token: 0x06009390 RID: 37776 RVA: 0x002C3584 File Offset: 0x002C3584
			public int MetaDataVersion
			{
				get
				{
					return this.metaDataVersion;
				}
				set
				{
					this.metaDataVersion = value;
				}
			}

			// Token: 0x17001E90 RID: 7824
			// (get) Token: 0x06009391 RID: 37777 RVA: 0x002C3590 File Offset: 0x002C3590
			// (set) Token: 0x06009392 RID: 37778 RVA: 0x002C3598 File Offset: 0x002C3598
			public RuntimeTypeModel.Accessibility Accessibility
			{
				get
				{
					return this.accessibility;
				}
				set
				{
					this.accessibility = value;
				}
			}

			// Token: 0x04004B71 RID: 19313
			private string targetFrameworkName;

			// Token: 0x04004B72 RID: 19314
			private string targetFrameworkDisplayName;

			// Token: 0x04004B73 RID: 19315
			private string typeName;

			// Token: 0x04004B74 RID: 19316
			private string outputPath;

			// Token: 0x04004B75 RID: 19317
			private string imageRuntimeVersion;

			// Token: 0x04004B76 RID: 19318
			private int metaDataVersion;

			// Token: 0x04004B77 RID: 19319
			private RuntimeTypeModel.Accessibility accessibility;
		}

		// Token: 0x02001181 RID: 4481
		public enum Accessibility
		{
			// Token: 0x04004B79 RID: 19321
			Public,
			// Token: 0x04004B7A RID: 19322
			Internal
		}
	}
}
