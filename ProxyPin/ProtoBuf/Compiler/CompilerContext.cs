using System;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Threading;
using ProtoBuf.Meta;
using ProtoBuf.Serializers;

namespace ProtoBuf.Compiler
{
	// Token: 0x02000C80 RID: 3200
	internal sealed class CompilerContext
	{
		// Token: 0x17001BCC RID: 7116
		// (get) Token: 0x06007FFF RID: 32767 RVA: 0x0025DFD8 File Offset: 0x0025DFD8
		public TypeModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x06008000 RID: 32768 RVA: 0x0025DFE0 File Offset: 0x0025DFE0
		internal CodeLabel DefineLabel()
		{
			Label value = this.il.DefineLabel();
			int num = this.nextLabel;
			this.nextLabel = num + 1;
			CodeLabel result = new CodeLabel(value, num);
			return result;
		}

		// Token: 0x06008001 RID: 32769 RVA: 0x0025E018 File Offset: 0x0025E018
		[Conditional("DEBUG_COMPILE")]
		private void TraceCompile(string value)
		{
		}

		// Token: 0x06008002 RID: 32770 RVA: 0x0025E01C File Offset: 0x0025E01C
		internal void MarkLabel(CodeLabel label)
		{
			this.il.MarkLabel(label.Value);
		}

		// Token: 0x06008003 RID: 32771 RVA: 0x0025E030 File Offset: 0x0025E030
		public static ProtoSerializer BuildSerializer(IProtoSerializer head, TypeModel model)
		{
			Type expectedType = head.ExpectedType;
			ProtoSerializer result;
			try
			{
				CompilerContext compilerContext = new CompilerContext(expectedType, true, true, model, typeof(object));
				compilerContext.LoadValue(compilerContext.InputValue);
				compilerContext.CastFromObject(expectedType);
				compilerContext.WriteNullCheckedTail(expectedType, head, null);
				compilerContext.Emit(OpCodes.Ret);
				result = (ProtoSerializer)compilerContext.method.CreateDelegate(typeof(ProtoSerializer));
			}
			catch (Exception innerException)
			{
				string text = expectedType.FullName;
				if (string.IsNullOrEmpty(text))
				{
					text = expectedType.Name;
				}
				throw new InvalidOperationException("It was not possible to prepare a serializer for: " + text, innerException);
			}
			return result;
		}

		// Token: 0x06008004 RID: 32772 RVA: 0x0025E0E0 File Offset: 0x0025E0E0
		public static ProtoDeserializer BuildDeserializer(IProtoSerializer head, TypeModel model)
		{
			Type expectedType = head.ExpectedType;
			CompilerContext compilerContext = new CompilerContext(expectedType, false, true, model, typeof(object));
			using (Local local = new Local(compilerContext, expectedType))
			{
				if (!Helpers.IsValueType(expectedType))
				{
					compilerContext.LoadValue(compilerContext.InputValue);
					compilerContext.CastFromObject(expectedType);
					compilerContext.StoreValue(local);
				}
				else
				{
					compilerContext.LoadValue(compilerContext.InputValue);
					CodeLabel label = compilerContext.DefineLabel();
					CodeLabel label2 = compilerContext.DefineLabel();
					compilerContext.BranchIfTrue(label, true);
					compilerContext.LoadAddress(local, expectedType, false);
					compilerContext.EmitCtor(expectedType);
					compilerContext.Branch(label2, true);
					compilerContext.MarkLabel(label);
					compilerContext.LoadValue(compilerContext.InputValue);
					compilerContext.CastFromObject(expectedType);
					compilerContext.StoreValue(local);
					compilerContext.MarkLabel(label2);
				}
				head.EmitRead(compilerContext, local);
				if (head.ReturnsValue)
				{
					compilerContext.StoreValue(local);
				}
				compilerContext.LoadValue(local);
				compilerContext.CastToObject(expectedType);
			}
			compilerContext.Emit(OpCodes.Ret);
			return (ProtoDeserializer)compilerContext.method.CreateDelegate(typeof(ProtoDeserializer));
		}

		// Token: 0x06008005 RID: 32773 RVA: 0x0025E210 File Offset: 0x0025E210
		internal void Return()
		{
			this.Emit(OpCodes.Ret);
		}

		// Token: 0x06008006 RID: 32774 RVA: 0x0025E220 File Offset: 0x0025E220
		private static bool IsObject(Type type)
		{
			return type == typeof(object);
		}

		// Token: 0x06008007 RID: 32775 RVA: 0x0025E234 File Offset: 0x0025E234
		internal void CastToObject(Type type)
		{
			if (!CompilerContext.IsObject(type))
			{
				if (Helpers.IsValueType(type))
				{
					this.il.Emit(OpCodes.Box, type);
					return;
				}
				this.il.Emit(OpCodes.Castclass, this.MapType(typeof(object)));
			}
		}

		// Token: 0x06008008 RID: 32776 RVA: 0x0025E290 File Offset: 0x0025E290
		internal void CastFromObject(Type type)
		{
			if (!CompilerContext.IsObject(type))
			{
				if (Helpers.IsValueType(type))
				{
					if (this.MetadataVersion == CompilerContext.ILVersion.Net1)
					{
						this.il.Emit(OpCodes.Unbox, type);
						this.il.Emit(OpCodes.Ldobj, type);
						return;
					}
					this.il.Emit(OpCodes.Unbox_Any, type);
					return;
				}
				else
				{
					this.il.Emit(OpCodes.Castclass, type);
				}
			}
		}

		// Token: 0x06008009 RID: 32777 RVA: 0x0025E30C File Offset: 0x0025E30C
		internal MethodBuilder GetDedicatedMethod(int metaKey, bool read)
		{
			if (this.methodPairs == null)
			{
				return null;
			}
			int i = 0;
			while (i < this.methodPairs.Length)
			{
				if (this.methodPairs[i].MetaKey == metaKey)
				{
					if (!read)
					{
						return this.methodPairs[i].Serialize;
					}
					return this.methodPairs[i].Deserialize;
				}
				else
				{
					i++;
				}
			}
			throw new ArgumentException("Meta-key not found", "metaKey");
		}

		// Token: 0x0600800A RID: 32778 RVA: 0x0025E390 File Offset: 0x0025E390
		internal int MapMetaKeyToCompiledKey(int metaKey)
		{
			if (metaKey < 0 || this.methodPairs == null)
			{
				return metaKey;
			}
			for (int i = 0; i < this.methodPairs.Length; i++)
			{
				if (this.methodPairs[i].MetaKey == metaKey)
				{
					return i;
				}
			}
			throw new ArgumentException("Key could not be mapped: " + metaKey.ToString(), "metaKey");
		}

		// Token: 0x17001BCD RID: 7117
		// (get) Token: 0x0600800B RID: 32779 RVA: 0x0025E400 File Offset: 0x0025E400
		internal bool NonPublic
		{
			get
			{
				return this.nonPublic;
			}
		}

		// Token: 0x17001BCE RID: 7118
		// (get) Token: 0x0600800C RID: 32780 RVA: 0x0025E408 File Offset: 0x0025E408
		public Local InputValue
		{
			get
			{
				return this.inputValue;
			}
		}

		// Token: 0x0600800D RID: 32781 RVA: 0x0025E410 File Offset: 0x0025E410
		internal CompilerContext(ILGenerator il, bool isStatic, bool isWriter, RuntimeTypeModel.SerializerPair[] methodPairs, TypeModel model, CompilerContext.ILVersion metadataVersion, string assemblyName, Type inputType, string traceName)
		{
			if (string.IsNullOrEmpty(assemblyName))
			{
				throw new ArgumentNullException("assemblyName");
			}
			this.assemblyName = assemblyName;
			this.isStatic = isStatic;
			if (methodPairs == null)
			{
				throw new ArgumentNullException("methodPairs");
			}
			this.methodPairs = methodPairs;
			if (il == null)
			{
				throw new ArgumentNullException("il");
			}
			this.il = il;
			this.isWriter = isWriter;
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}
			this.model = model;
			this.metadataVersion = metadataVersion;
			if (inputType != null)
			{
				this.inputValue = new Local(null, inputType);
			}
		}

		// Token: 0x0600800E RID: 32782 RVA: 0x0025E4D0 File Offset: 0x0025E4D0
		private CompilerContext(Type associatedType, bool isWriter, bool isStatic, TypeModel model, Type inputType)
		{
			this.metadataVersion = CompilerContext.ILVersion.Net2;
			this.isStatic = isStatic;
			this.isWriter = isWriter;
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}
			this.model = model;
			this.nonPublic = true;
			Type typeFromHandle;
			Type[] parameterTypes;
			if (isWriter)
			{
				typeFromHandle = typeof(void);
				parameterTypes = new Type[]
				{
					typeof(object),
					typeof(ProtoWriter)
				};
			}
			else
			{
				typeFromHandle = typeof(object);
				parameterTypes = new Type[]
				{
					typeof(object),
					typeof(ProtoReader)
				};
			}
			this.method = new DynamicMethod("proto_" + Interlocked.Increment(ref CompilerContext.next).ToString(), typeFromHandle, parameterTypes, associatedType.IsInterface ? typeof(object) : associatedType, true);
			this.il = this.method.GetILGenerator();
			if (inputType != null)
			{
				this.inputValue = new Local(null, inputType);
			}
		}

		// Token: 0x0600800F RID: 32783 RVA: 0x0025E5FC File Offset: 0x0025E5FC
		private void Emit(OpCode opcode)
		{
			this.il.Emit(opcode);
		}

		// Token: 0x06008010 RID: 32784 RVA: 0x0025E60C File Offset: 0x0025E60C
		public void LoadValue(string value)
		{
			if (value == null)
			{
				this.LoadNullRef();
				return;
			}
			this.il.Emit(OpCodes.Ldstr, value);
		}

		// Token: 0x06008011 RID: 32785 RVA: 0x0025E62C File Offset: 0x0025E62C
		public void LoadValue(float value)
		{
			this.il.Emit(OpCodes.Ldc_R4, value);
		}

		// Token: 0x06008012 RID: 32786 RVA: 0x0025E640 File Offset: 0x0025E640
		public void LoadValue(double value)
		{
			this.il.Emit(OpCodes.Ldc_R8, value);
		}

		// Token: 0x06008013 RID: 32787 RVA: 0x0025E654 File Offset: 0x0025E654
		public void LoadValue(long value)
		{
			this.il.Emit(OpCodes.Ldc_I8, value);
		}

		// Token: 0x06008014 RID: 32788 RVA: 0x0025E668 File Offset: 0x0025E668
		public void LoadValue(int value)
		{
			switch (value)
			{
			case -1:
				this.Emit(OpCodes.Ldc_I4_M1);
				return;
			case 0:
				this.Emit(OpCodes.Ldc_I4_0);
				return;
			case 1:
				this.Emit(OpCodes.Ldc_I4_1);
				return;
			case 2:
				this.Emit(OpCodes.Ldc_I4_2);
				return;
			case 3:
				this.Emit(OpCodes.Ldc_I4_3);
				return;
			case 4:
				this.Emit(OpCodes.Ldc_I4_4);
				return;
			case 5:
				this.Emit(OpCodes.Ldc_I4_5);
				return;
			case 6:
				this.Emit(OpCodes.Ldc_I4_6);
				return;
			case 7:
				this.Emit(OpCodes.Ldc_I4_7);
				return;
			case 8:
				this.Emit(OpCodes.Ldc_I4_8);
				return;
			default:
				if (value >= -128 && value <= 127)
				{
					this.il.Emit(OpCodes.Ldc_I4_S, (sbyte)value);
					return;
				}
				this.il.Emit(OpCodes.Ldc_I4, value);
				return;
			}
		}

		// Token: 0x06008015 RID: 32789 RVA: 0x0025E75C File Offset: 0x0025E75C
		internal LocalBuilder GetFromPool(Type type)
		{
			int count = this.locals.Count;
			for (int i = 0; i < count; i++)
			{
				LocalBuilder localBuilder = (LocalBuilder)this.locals[i];
				if (localBuilder != null && localBuilder.LocalType == type)
				{
					this.locals[i] = null;
					return localBuilder;
				}
			}
			return this.il.DeclareLocal(type);
		}

		// Token: 0x06008016 RID: 32790 RVA: 0x0025E7D0 File Offset: 0x0025E7D0
		internal void ReleaseToPool(LocalBuilder value)
		{
			int count = this.locals.Count;
			for (int i = 0; i < count; i++)
			{
				if (this.locals[i] == null)
				{
					this.locals[i] = value;
					return;
				}
			}
			this.locals.Add(value);
		}

		// Token: 0x06008017 RID: 32791 RVA: 0x0025E828 File Offset: 0x0025E828
		public void LoadReaderWriter()
		{
			this.Emit(this.isStatic ? OpCodes.Ldarg_1 : OpCodes.Ldarg_2);
		}

		// Token: 0x06008018 RID: 32792 RVA: 0x0025E84C File Offset: 0x0025E84C
		public void StoreValue(Local local)
		{
			if (local == this.InputValue)
			{
				byte arg = this.isStatic ? 0 : 1;
				this.il.Emit(OpCodes.Starg_S, arg);
				return;
			}
			switch (local.Value.LocalIndex)
			{
			case 0:
				this.Emit(OpCodes.Stloc_0);
				return;
			case 1:
				this.Emit(OpCodes.Stloc_1);
				return;
			case 2:
				this.Emit(OpCodes.Stloc_2);
				return;
			case 3:
				this.Emit(OpCodes.Stloc_3);
				return;
			default:
			{
				OpCode opcode = this.UseShortForm(local) ? OpCodes.Stloc_S : OpCodes.Stloc;
				this.il.Emit(opcode, local.Value);
				return;
			}
			}
		}

		// Token: 0x06008019 RID: 32793 RVA: 0x0025E914 File Offset: 0x0025E914
		public void LoadValue(Local local)
		{
			if (local != null)
			{
				if (local == this.InputValue)
				{
					this.Emit(this.isStatic ? OpCodes.Ldarg_0 : OpCodes.Ldarg_1);
					return;
				}
				switch (local.Value.LocalIndex)
				{
				case 0:
					this.Emit(OpCodes.Ldloc_0);
					return;
				case 1:
					this.Emit(OpCodes.Ldloc_1);
					return;
				case 2:
					this.Emit(OpCodes.Ldloc_2);
					return;
				case 3:
					this.Emit(OpCodes.Ldloc_3);
					return;
				default:
				{
					OpCode opcode = this.UseShortForm(local) ? OpCodes.Ldloc_S : OpCodes.Ldloc;
					this.il.Emit(opcode, local.Value);
					break;
				}
				}
			}
		}

		// Token: 0x0600801A RID: 32794 RVA: 0x0025E9E0 File Offset: 0x0025E9E0
		public Local GetLocalWithValue(Type type, Local fromValue)
		{
			if (fromValue != null)
			{
				if (fromValue.Type == type)
				{
					return fromValue.AsCopy();
				}
				this.LoadValue(fromValue);
				if (!Helpers.IsValueType(type) && (fromValue.Type == null || !type.IsAssignableFrom(fromValue.Type)))
				{
					this.Cast(type);
				}
			}
			Local local = new Local(this, type);
			this.StoreValue(local);
			return local;
		}

		// Token: 0x0600801B RID: 32795 RVA: 0x0025EA5C File Offset: 0x0025EA5C
		internal void EmitBasicRead(string methodName, Type expectedType)
		{
			MethodInfo methodInfo = this.MapType(typeof(ProtoReader)).GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (methodInfo == null || methodInfo.ReturnType != expectedType || methodInfo.GetParameters().Length != 0)
			{
				throw new ArgumentException("methodName");
			}
			this.LoadReaderWriter();
			this.EmitCall(methodInfo);
		}

		// Token: 0x0600801C RID: 32796 RVA: 0x0025EAC8 File Offset: 0x0025EAC8
		internal void EmitBasicRead(Type helperType, string methodName, Type expectedType)
		{
			MethodInfo methodInfo = helperType.GetMethod(methodName, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			if (methodInfo == null || methodInfo.ReturnType != expectedType || methodInfo.GetParameters().Length != 1)
			{
				throw new ArgumentException("methodName");
			}
			this.LoadReaderWriter();
			this.EmitCall(methodInfo);
		}

		// Token: 0x0600801D RID: 32797 RVA: 0x0025EB28 File Offset: 0x0025EB28
		internal void EmitBasicWrite(string methodName, Local fromValue)
		{
			if (string.IsNullOrEmpty(methodName))
			{
				throw new ArgumentNullException("methodName");
			}
			this.LoadValue(fromValue);
			this.LoadReaderWriter();
			this.EmitCall(this.GetWriterMethod(methodName));
		}

		// Token: 0x0600801E RID: 32798 RVA: 0x0025EB6C File Offset: 0x0025EB6C
		private MethodInfo GetWriterMethod(string methodName)
		{
			Type type = this.MapType(typeof(ProtoWriter));
			MethodInfo[] methods = type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			foreach (MethodInfo methodInfo in methods)
			{
				if (!(methodInfo.Name != methodName))
				{
					ParameterInfo[] parameters = methodInfo.GetParameters();
					if (parameters.Length == 2 && parameters[1].ParameterType == type)
					{
						return methodInfo;
					}
				}
			}
			throw new ArgumentException("No suitable method found for: " + methodName, "methodName");
		}

		// Token: 0x0600801F RID: 32799 RVA: 0x0025EC08 File Offset: 0x0025EC08
		internal void EmitWrite(Type helperType, string methodName, Local valueFrom)
		{
			if (string.IsNullOrEmpty(methodName))
			{
				throw new ArgumentNullException("methodName");
			}
			MethodInfo methodInfo = helperType.GetMethod(methodName, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			if (methodInfo == null || methodInfo.ReturnType != this.MapType(typeof(void)))
			{
				throw new ArgumentException("methodName");
			}
			this.LoadValue(valueFrom);
			this.LoadReaderWriter();
			this.EmitCall(methodInfo);
		}

		// Token: 0x06008020 RID: 32800 RVA: 0x0025EC84 File Offset: 0x0025EC84
		public void EmitCall(MethodInfo method)
		{
			this.EmitCall(method, null);
		}

		// Token: 0x06008021 RID: 32801 RVA: 0x0025EC90 File Offset: 0x0025EC90
		public void EmitCall(MethodInfo method, Type targetType)
		{
			MemberInfo memberInfo = method;
			this.CheckAccessibility(ref memberInfo);
			OpCode opcode;
			if (method.IsStatic || Helpers.IsValueType(method.DeclaringType))
			{
				opcode = OpCodes.Call;
			}
			else
			{
				opcode = OpCodes.Callvirt;
				if (targetType != null && Helpers.IsValueType(targetType) && !Helpers.IsValueType(method.DeclaringType))
				{
					this.Constrain(targetType);
				}
			}
			this.il.EmitCall(opcode, method, null);
		}

		// Token: 0x06008022 RID: 32802 RVA: 0x0025ED14 File Offset: 0x0025ED14
		public void LoadNullRef()
		{
			this.Emit(OpCodes.Ldnull);
		}

		// Token: 0x06008023 RID: 32803 RVA: 0x0025ED24 File Offset: 0x0025ED24
		internal void WriteNullCheckedTail(Type type, IProtoSerializer tail, Local valueFrom)
		{
			if (Helpers.IsValueType(type))
			{
				Type underlyingType = Helpers.GetUnderlyingType(type);
				if (underlyingType == null)
				{
					tail.EmitWrite(this, valueFrom);
					return;
				}
				using (Local localWithValue = this.GetLocalWithValue(type, valueFrom))
				{
					this.LoadAddress(localWithValue, type, false);
					this.LoadValue(type.GetProperty("HasValue"));
					CodeLabel label = this.DefineLabel();
					this.BranchIfFalse(label, false);
					this.LoadAddress(localWithValue, type, false);
					this.EmitCall(type.GetMethod("GetValueOrDefault", Helpers.EmptyTypes));
					tail.EmitWrite(this, null);
					this.MarkLabel(label);
					return;
				}
			}
			this.LoadValue(valueFrom);
			this.CopyValue();
			CodeLabel label2 = this.DefineLabel();
			CodeLabel label3 = this.DefineLabel();
			this.BranchIfTrue(label2, true);
			this.DiscardValue();
			this.Branch(label3, false);
			this.MarkLabel(label2);
			tail.EmitWrite(this, null);
			this.MarkLabel(label3);
		}

		// Token: 0x06008024 RID: 32804 RVA: 0x0025EE24 File Offset: 0x0025EE24
		internal void ReadNullCheckedTail(Type type, IProtoSerializer tail, Local valueFrom)
		{
			Type underlyingType;
			if (Helpers.IsValueType(type) && (underlyingType = Helpers.GetUnderlyingType(type)) != null)
			{
				if (tail.RequiresOldValue)
				{
					using (Local localWithValue = this.GetLocalWithValue(type, valueFrom))
					{
						this.LoadAddress(localWithValue, type, false);
						this.EmitCall(type.GetMethod("GetValueOrDefault", Helpers.EmptyTypes));
					}
				}
				tail.EmitRead(this, null);
				if (tail.ReturnsValue)
				{
					this.EmitCtor(type, new Type[]
					{
						underlyingType
					});
				}
				return;
			}
			tail.EmitRead(this, valueFrom);
		}

		// Token: 0x06008025 RID: 32805 RVA: 0x0025EED4 File Offset: 0x0025EED4
		public void EmitCtor(Type type)
		{
			this.EmitCtor(type, Helpers.EmptyTypes);
		}

		// Token: 0x06008026 RID: 32806 RVA: 0x0025EEE4 File Offset: 0x0025EEE4
		public void EmitCtor(ConstructorInfo ctor)
		{
			if (ctor == null)
			{
				throw new ArgumentNullException("ctor");
			}
			MemberInfo memberInfo = ctor;
			this.CheckAccessibility(ref memberInfo);
			this.il.Emit(OpCodes.Newobj, ctor);
		}

		// Token: 0x06008027 RID: 32807 RVA: 0x0025EF28 File Offset: 0x0025EF28
		public void InitLocal(Type type, Local target)
		{
			this.LoadAddress(target, type, true);
			this.il.Emit(OpCodes.Initobj, type);
		}

		// Token: 0x06008028 RID: 32808 RVA: 0x0025EF44 File Offset: 0x0025EF44
		public void EmitCtor(Type type, params Type[] parameterTypes)
		{
			if (Helpers.IsValueType(type) && parameterTypes.Length == 0)
			{
				this.il.Emit(OpCodes.Initobj, type);
				return;
			}
			ConstructorInfo constructor = Helpers.GetConstructor(type, parameterTypes, true);
			if (constructor == null)
			{
				throw new InvalidOperationException("No suitable constructor found for " + type.FullName);
			}
			this.EmitCtor(constructor);
		}

		// Token: 0x06008029 RID: 32809 RVA: 0x0025EFAC File Offset: 0x0025EFAC
		private bool InternalsVisible(Assembly assembly)
		{
			if (string.IsNullOrEmpty(this.assemblyName))
			{
				return false;
			}
			if (this.knownTrustedAssemblies != null && this.knownTrustedAssemblies.IndexOfReference(assembly) >= 0)
			{
				return true;
			}
			if (this.knownUntrustedAssemblies != null && this.knownUntrustedAssemblies.IndexOfReference(assembly) >= 0)
			{
				return false;
			}
			bool flag = false;
			Type type = this.MapType(typeof(InternalsVisibleToAttribute));
			if (type == null)
			{
				return false;
			}
			foreach (InternalsVisibleToAttribute internalsVisibleToAttribute in assembly.GetCustomAttributes(type, false))
			{
				if (internalsVisibleToAttribute.AssemblyName == this.assemblyName || internalsVisibleToAttribute.AssemblyName.StartsWith(this.assemblyName + ","))
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				if (this.knownTrustedAssemblies == null)
				{
					this.knownTrustedAssemblies = new BasicList();
				}
				this.knownTrustedAssemblies.Add(assembly);
			}
			else
			{
				if (this.knownUntrustedAssemblies == null)
				{
					this.knownUntrustedAssemblies = new BasicList();
				}
				this.knownUntrustedAssemblies.Add(assembly);
			}
			return flag;
		}

		// Token: 0x0600802A RID: 32810 RVA: 0x0025F0E4 File Offset: 0x0025F0E4
		internal void CheckAccessibility(ref MemberInfo member)
		{
			if (member == null)
			{
				throw new ArgumentNullException("member");
			}
			if (!this.NonPublic)
			{
				if (member is FieldInfo && (member.Name.StartsWith("<") & member.Name.EndsWith(">k__BackingField")))
				{
					string name = member.Name.Substring(1, member.Name.Length - 17);
					PropertyInfo property = member.DeclaringType.GetProperty(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
					if (property != null)
					{
						member = property;
					}
				}
				MemberTypes memberType = member.MemberType;
				bool flag;
				if (memberType <= MemberTypes.Method)
				{
					if (memberType == MemberTypes.Constructor)
					{
						ConstructorInfo constructorInfo = (ConstructorInfo)member;
						flag = (constructorInfo.IsPublic || ((constructorInfo.IsAssembly || constructorInfo.IsFamilyOrAssembly) && this.InternalsVisible(constructorInfo.DeclaringType.Assembly)));
						goto IL_2CB;
					}
					if (memberType == MemberTypes.Field)
					{
						FieldInfo fieldInfo = (FieldInfo)member;
						flag = (fieldInfo.IsPublic || ((fieldInfo.IsAssembly || fieldInfo.IsFamilyOrAssembly) && this.InternalsVisible(fieldInfo.DeclaringType.Assembly)));
						goto IL_2CB;
					}
					if (memberType == MemberTypes.Method)
					{
						MethodInfo methodInfo = (MethodInfo)member;
						flag = (methodInfo.IsPublic || ((methodInfo.IsAssembly || methodInfo.IsFamilyOrAssembly) && this.InternalsVisible(methodInfo.DeclaringType.Assembly)));
						if (!flag && (member is MethodBuilder || member.DeclaringType == this.MapType(typeof(TypeModel))))
						{
							flag = true;
							goto IL_2CB;
						}
						goto IL_2CB;
					}
				}
				else
				{
					if (memberType == MemberTypes.Property)
					{
						flag = true;
						goto IL_2CB;
					}
					if (memberType == MemberTypes.TypeInfo)
					{
						Type type = (Type)member;
						flag = (type.IsPublic || this.InternalsVisible(type.Assembly));
						goto IL_2CB;
					}
					if (memberType == MemberTypes.NestedType)
					{
						Type type = (Type)member;
						do
						{
							flag = (type.IsNestedPublic || type.IsPublic || ((type.DeclaringType == null || type.IsNestedAssembly || type.IsNestedFamORAssem) && this.InternalsVisible(type.Assembly)));
							if (!flag)
							{
								break;
							}
						}
						while ((type = type.DeclaringType) != null);
						goto IL_2CB;
					}
				}
				throw new NotSupportedException(memberType.ToString());
				IL_2CB:
				if (!flag)
				{
					if (memberType == MemberTypes.TypeInfo || memberType == MemberTypes.NestedType)
					{
						throw new InvalidOperationException("Non-public type cannot be used with full dll compilation: " + ((Type)member).FullName);
					}
					throw new InvalidOperationException("Non-public member cannot be used with full dll compilation: " + member.DeclaringType.FullName + "." + member.Name);
				}
			}
		}

		// Token: 0x0600802B RID: 32811 RVA: 0x0025F420 File Offset: 0x0025F420
		public void LoadValue(FieldInfo field)
		{
			MemberInfo memberInfo = field;
			this.CheckAccessibility(ref memberInfo);
			if (memberInfo is PropertyInfo)
			{
				this.LoadValue((PropertyInfo)memberInfo);
				return;
			}
			OpCode opcode = field.IsStatic ? OpCodes.Ldsfld : OpCodes.Ldfld;
			this.il.Emit(opcode, field);
		}

		// Token: 0x0600802C RID: 32812 RVA: 0x0025F47C File Offset: 0x0025F47C
		public void StoreValue(FieldInfo field)
		{
			MemberInfo memberInfo = field;
			this.CheckAccessibility(ref memberInfo);
			if (memberInfo is PropertyInfo)
			{
				this.StoreValue((PropertyInfo)memberInfo);
				return;
			}
			OpCode opcode = field.IsStatic ? OpCodes.Stsfld : OpCodes.Stfld;
			this.il.Emit(opcode, field);
		}

		// Token: 0x0600802D RID: 32813 RVA: 0x0025F4D8 File Offset: 0x0025F4D8
		public void LoadValue(PropertyInfo property)
		{
			MemberInfo memberInfo = property;
			this.CheckAccessibility(ref memberInfo);
			this.EmitCall(Helpers.GetGetMethod(property, true, true));
		}

		// Token: 0x0600802E RID: 32814 RVA: 0x0025F504 File Offset: 0x0025F504
		public void StoreValue(PropertyInfo property)
		{
			MemberInfo memberInfo = property;
			this.CheckAccessibility(ref memberInfo);
			this.EmitCall(Helpers.GetSetMethod(property, true, true));
		}

		// Token: 0x0600802F RID: 32815 RVA: 0x0025F530 File Offset: 0x0025F530
		internal static void LoadValue(ILGenerator il, int value)
		{
			switch (value)
			{
			case -1:
				il.Emit(OpCodes.Ldc_I4_M1);
				return;
			case 0:
				il.Emit(OpCodes.Ldc_I4_0);
				return;
			case 1:
				il.Emit(OpCodes.Ldc_I4_1);
				return;
			case 2:
				il.Emit(OpCodes.Ldc_I4_2);
				return;
			case 3:
				il.Emit(OpCodes.Ldc_I4_3);
				return;
			case 4:
				il.Emit(OpCodes.Ldc_I4_4);
				return;
			case 5:
				il.Emit(OpCodes.Ldc_I4_5);
				return;
			case 6:
				il.Emit(OpCodes.Ldc_I4_6);
				return;
			case 7:
				il.Emit(OpCodes.Ldc_I4_7);
				return;
			case 8:
				il.Emit(OpCodes.Ldc_I4_8);
				return;
			default:
				il.Emit(OpCodes.Ldc_I4, value);
				return;
			}
		}

		// Token: 0x06008030 RID: 32816 RVA: 0x0025F5FC File Offset: 0x0025F5FC
		private bool UseShortForm(Local local)
		{
			return local.Value.LocalIndex < 256;
		}

		// Token: 0x06008031 RID: 32817 RVA: 0x0025F610 File Offset: 0x0025F610
		internal void LoadAddress(Local local, Type type, bool evenIfClass = false)
		{
			if (!evenIfClass && !Helpers.IsValueType(type))
			{
				this.LoadValue(local);
				return;
			}
			if (local == null)
			{
				throw new InvalidOperationException("Cannot load the address of the head of the stack");
			}
			if (local == this.InputValue)
			{
				this.il.Emit(OpCodes.Ldarga_S, this.isStatic ? 0 : 1);
				return;
			}
			OpCode opcode = this.UseShortForm(local) ? OpCodes.Ldloca_S : OpCodes.Ldloca;
			this.il.Emit(opcode, local.Value);
		}

		// Token: 0x06008032 RID: 32818 RVA: 0x0025F6A8 File Offset: 0x0025F6A8
		internal void Branch(CodeLabel label, bool @short)
		{
			OpCode opcode = @short ? OpCodes.Br_S : OpCodes.Br;
			this.il.Emit(opcode, label.Value);
		}

		// Token: 0x06008033 RID: 32819 RVA: 0x0025F6E4 File Offset: 0x0025F6E4
		internal void BranchIfFalse(CodeLabel label, bool @short)
		{
			OpCode opcode = @short ? OpCodes.Brfalse_S : OpCodes.Brfalse;
			this.il.Emit(opcode, label.Value);
		}

		// Token: 0x06008034 RID: 32820 RVA: 0x0025F720 File Offset: 0x0025F720
		internal void BranchIfTrue(CodeLabel label, bool @short)
		{
			OpCode opcode = @short ? OpCodes.Brtrue_S : OpCodes.Brtrue;
			this.il.Emit(opcode, label.Value);
		}

		// Token: 0x06008035 RID: 32821 RVA: 0x0025F75C File Offset: 0x0025F75C
		internal void BranchIfEqual(CodeLabel label, bool @short)
		{
			OpCode opcode = @short ? OpCodes.Beq_S : OpCodes.Beq;
			this.il.Emit(opcode, label.Value);
		}

		// Token: 0x06008036 RID: 32822 RVA: 0x0025F798 File Offset: 0x0025F798
		internal void CopyValue()
		{
			this.Emit(OpCodes.Dup);
		}

		// Token: 0x06008037 RID: 32823 RVA: 0x0025F7A8 File Offset: 0x0025F7A8
		internal void BranchIfGreater(CodeLabel label, bool @short)
		{
			OpCode opcode = @short ? OpCodes.Bgt_S : OpCodes.Bgt;
			this.il.Emit(opcode, label.Value);
		}

		// Token: 0x06008038 RID: 32824 RVA: 0x0025F7E4 File Offset: 0x0025F7E4
		internal void BranchIfLess(CodeLabel label, bool @short)
		{
			OpCode opcode = @short ? OpCodes.Blt_S : OpCodes.Blt;
			this.il.Emit(opcode, label.Value);
		}

		// Token: 0x06008039 RID: 32825 RVA: 0x0025F820 File Offset: 0x0025F820
		internal void DiscardValue()
		{
			this.Emit(OpCodes.Pop);
		}

		// Token: 0x0600803A RID: 32826 RVA: 0x0025F830 File Offset: 0x0025F830
		public void Subtract()
		{
			this.Emit(OpCodes.Sub);
		}

		// Token: 0x0600803B RID: 32827 RVA: 0x0025F840 File Offset: 0x0025F840
		public void Switch(CodeLabel[] jumpTable)
		{
			if (jumpTable.Length <= 128)
			{
				Label[] array = new Label[jumpTable.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = jumpTable[i].Value;
				}
				this.il.Emit(OpCodes.Switch, array);
				return;
			}
			using (Local localWithValue = this.GetLocalWithValue(this.MapType(typeof(int)), null))
			{
				int num = jumpTable.Length;
				int num2 = 0;
				int num3 = num / 128;
				if (num % 128 != 0)
				{
					num3++;
				}
				Label[] array2 = new Label[num3];
				for (int j = 0; j < num3; j++)
				{
					array2[j] = this.il.DefineLabel();
				}
				CodeLabel label = this.DefineLabel();
				this.LoadValue(localWithValue);
				this.LoadValue(128);
				this.Emit(OpCodes.Div);
				this.il.Emit(OpCodes.Switch, array2);
				this.Branch(label, false);
				Label[] array3 = new Label[128];
				for (int k = 0; k < num3; k++)
				{
					this.il.MarkLabel(array2[k]);
					int num4 = Math.Min(128, num);
					num -= num4;
					if (array3.Length != num4)
					{
						array3 = new Label[num4];
					}
					int num5 = num2;
					for (int l = 0; l < num4; l++)
					{
						array3[l] = jumpTable[num2++].Value;
					}
					this.LoadValue(localWithValue);
					if (num5 != 0)
					{
						this.LoadValue(num5);
						this.Emit(OpCodes.Sub);
					}
					this.il.Emit(OpCodes.Switch, array3);
					if (num != 0)
					{
						this.Branch(label, false);
					}
				}
				this.MarkLabel(label);
			}
		}

		// Token: 0x0600803C RID: 32828 RVA: 0x0025FA4C File Offset: 0x0025FA4C
		internal void EndFinally()
		{
			this.il.EndExceptionBlock();
		}

		// Token: 0x0600803D RID: 32829 RVA: 0x0025FA5C File Offset: 0x0025FA5C
		internal void BeginFinally()
		{
			this.il.BeginFinallyBlock();
		}

		// Token: 0x0600803E RID: 32830 RVA: 0x0025FA6C File Offset: 0x0025FA6C
		internal void EndTry(CodeLabel label, bool @short)
		{
			OpCode opcode = @short ? OpCodes.Leave_S : OpCodes.Leave;
			this.il.Emit(opcode, label.Value);
		}

		// Token: 0x0600803F RID: 32831 RVA: 0x0025FAA8 File Offset: 0x0025FAA8
		internal CodeLabel BeginTry()
		{
			Label value = this.il.BeginExceptionBlock();
			int num = this.nextLabel;
			this.nextLabel = num + 1;
			CodeLabel result = new CodeLabel(value, num);
			return result;
		}

		// Token: 0x06008040 RID: 32832 RVA: 0x0025FAE0 File Offset: 0x0025FAE0
		internal void Constrain(Type type)
		{
			this.il.Emit(OpCodes.Constrained, type);
		}

		// Token: 0x06008041 RID: 32833 RVA: 0x0025FAF4 File Offset: 0x0025FAF4
		internal void TryCast(Type type)
		{
			this.il.Emit(OpCodes.Isinst, type);
		}

		// Token: 0x06008042 RID: 32834 RVA: 0x0025FB08 File Offset: 0x0025FB08
		internal void Cast(Type type)
		{
			this.il.Emit(OpCodes.Castclass, type);
		}

		// Token: 0x06008043 RID: 32835 RVA: 0x0025FB1C File Offset: 0x0025FB1C
		public IDisposable Using(Local local)
		{
			return new CompilerContext.UsingBlock(this, local);
		}

		// Token: 0x06008044 RID: 32836 RVA: 0x0025FB28 File Offset: 0x0025FB28
		internal void Add()
		{
			this.Emit(OpCodes.Add);
		}

		// Token: 0x06008045 RID: 32837 RVA: 0x0025FB38 File Offset: 0x0025FB38
		internal void LoadLength(Local arr, bool zeroIfNull)
		{
			if (zeroIfNull)
			{
				CodeLabel label = this.DefineLabel();
				CodeLabel label2 = this.DefineLabel();
				this.LoadValue(arr);
				this.CopyValue();
				this.BranchIfTrue(label, true);
				this.DiscardValue();
				this.LoadValue(0);
				this.Branch(label2, true);
				this.MarkLabel(label);
				this.Emit(OpCodes.Ldlen);
				this.Emit(OpCodes.Conv_I4);
				this.MarkLabel(label2);
				return;
			}
			this.LoadValue(arr);
			this.Emit(OpCodes.Ldlen);
			this.Emit(OpCodes.Conv_I4);
		}

		// Token: 0x06008046 RID: 32838 RVA: 0x0025FBCC File Offset: 0x0025FBCC
		internal void CreateArray(Type elementType, Local length)
		{
			this.LoadValue(length);
			this.il.Emit(OpCodes.Newarr, elementType);
		}

		// Token: 0x06008047 RID: 32839 RVA: 0x0025FBE8 File Offset: 0x0025FBE8
		internal void LoadArrayValue(Local arr, Local i)
		{
			Type type = arr.Type;
			type = type.GetElementType();
			this.LoadValue(arr);
			this.LoadValue(i);
			switch (Helpers.GetTypeCode(type))
			{
			case ProtoTypeCode.SByte:
				this.Emit(OpCodes.Ldelem_I1);
				return;
			case ProtoTypeCode.Byte:
				this.Emit(OpCodes.Ldelem_U1);
				return;
			case ProtoTypeCode.Int16:
				this.Emit(OpCodes.Ldelem_I2);
				return;
			case ProtoTypeCode.UInt16:
				this.Emit(OpCodes.Ldelem_U2);
				return;
			case ProtoTypeCode.Int32:
				this.Emit(OpCodes.Ldelem_I4);
				return;
			case ProtoTypeCode.UInt32:
				this.Emit(OpCodes.Ldelem_U4);
				return;
			case ProtoTypeCode.Int64:
				this.Emit(OpCodes.Ldelem_I8);
				return;
			case ProtoTypeCode.UInt64:
				this.Emit(OpCodes.Ldelem_I8);
				return;
			case ProtoTypeCode.Single:
				this.Emit(OpCodes.Ldelem_R4);
				return;
			case ProtoTypeCode.Double:
				this.Emit(OpCodes.Ldelem_R8);
				return;
			default:
				if (Helpers.IsValueType(type))
				{
					this.il.Emit(OpCodes.Ldelema, type);
					this.il.Emit(OpCodes.Ldobj, type);
					return;
				}
				this.Emit(OpCodes.Ldelem_Ref);
				return;
			}
		}

		// Token: 0x06008048 RID: 32840 RVA: 0x0025FD04 File Offset: 0x0025FD04
		internal void LoadValue(Type type)
		{
			this.il.Emit(OpCodes.Ldtoken, type);
			this.EmitCall(this.MapType(typeof(Type)).GetMethod("GetTypeFromHandle"));
		}

		// Token: 0x06008049 RID: 32841 RVA: 0x0025FD48 File Offset: 0x0025FD48
		internal void ConvertToInt32(ProtoTypeCode typeCode, bool uint32Overflow)
		{
			switch (typeCode)
			{
			case ProtoTypeCode.SByte:
			case ProtoTypeCode.Byte:
			case ProtoTypeCode.Int16:
			case ProtoTypeCode.UInt16:
				this.Emit(OpCodes.Conv_I4);
				return;
			case ProtoTypeCode.Int32:
				return;
			case ProtoTypeCode.UInt32:
				this.Emit(uint32Overflow ? OpCodes.Conv_Ovf_I4_Un : OpCodes.Conv_Ovf_I4);
				return;
			case ProtoTypeCode.Int64:
				this.Emit(OpCodes.Conv_Ovf_I4);
				return;
			case ProtoTypeCode.UInt64:
				this.Emit(OpCodes.Conv_Ovf_I4_Un);
				return;
			default:
				throw new InvalidOperationException("ConvertToInt32 not implemented for: " + typeCode.ToString());
			}
		}

		// Token: 0x0600804A RID: 32842 RVA: 0x0025FDE4 File Offset: 0x0025FDE4
		internal void ConvertFromInt32(ProtoTypeCode typeCode, bool uint32Overflow)
		{
			switch (typeCode)
			{
			case ProtoTypeCode.SByte:
				this.Emit(OpCodes.Conv_Ovf_I1);
				return;
			case ProtoTypeCode.Byte:
				this.Emit(OpCodes.Conv_Ovf_U1);
				return;
			case ProtoTypeCode.Int16:
				this.Emit(OpCodes.Conv_Ovf_I2);
				return;
			case ProtoTypeCode.UInt16:
				this.Emit(OpCodes.Conv_Ovf_U2);
				return;
			case ProtoTypeCode.Int32:
				return;
			case ProtoTypeCode.UInt32:
				this.Emit(uint32Overflow ? OpCodes.Conv_Ovf_U4 : OpCodes.Conv_U4);
				return;
			case ProtoTypeCode.Int64:
				this.Emit(OpCodes.Conv_I8);
				return;
			case ProtoTypeCode.UInt64:
				this.Emit(OpCodes.Conv_U8);
				return;
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0600804B RID: 32843 RVA: 0x0025FE8C File Offset: 0x0025FE8C
		internal void LoadValue(decimal value)
		{
			if (value == 0m)
			{
				this.LoadValue(typeof(decimal).GetField("Zero"));
				return;
			}
			int[] bits = decimal.GetBits(value);
			this.LoadValue(bits[0]);
			this.LoadValue(bits[1]);
			this.LoadValue(bits[2]);
			this.LoadValue((int)((uint)bits[3] >> 31));
			this.LoadValue(bits[3] >> 16 & 255);
			this.EmitCtor(this.MapType(typeof(decimal)), new Type[]
			{
				this.MapType(typeof(int)),
				this.MapType(typeof(int)),
				this.MapType(typeof(int)),
				this.MapType(typeof(bool)),
				this.MapType(typeof(byte))
			});
		}

		// Token: 0x0600804C RID: 32844 RVA: 0x0025FF84 File Offset: 0x0025FF84
		internal void LoadValue(Guid value)
		{
			if (value == Guid.Empty)
			{
				this.LoadValue(typeof(Guid).GetField("Empty"));
				return;
			}
			byte[] array = value.ToByteArray();
			int i = (int)array[0] | (int)array[1] << 8 | (int)array[2] << 16 | (int)array[3] << 24;
			this.LoadValue(i);
			short value2 = (short)((int)array[4] | (int)array[5] << 8);
			this.LoadValue((int)value2);
			value2 = (short)((int)array[6] | (int)array[7] << 8);
			this.LoadValue((int)value2);
			for (i = 8; i <= 15; i++)
			{
				this.LoadValue((int)array[i]);
			}
			this.EmitCtor(this.MapType(typeof(Guid)), new Type[]
			{
				this.MapType(typeof(int)),
				this.MapType(typeof(short)),
				this.MapType(typeof(short)),
				this.MapType(typeof(byte)),
				this.MapType(typeof(byte)),
				this.MapType(typeof(byte)),
				this.MapType(typeof(byte)),
				this.MapType(typeof(byte)),
				this.MapType(typeof(byte)),
				this.MapType(typeof(byte)),
				this.MapType(typeof(byte))
			});
		}

		// Token: 0x0600804D RID: 32845 RVA: 0x00260114 File Offset: 0x00260114
		internal void LoadSerializationContext()
		{
			this.LoadReaderWriter();
			this.LoadValue((this.isWriter ? typeof(ProtoWriter) : typeof(ProtoReader)).GetProperty("Context"));
		}

		// Token: 0x0600804E RID: 32846 RVA: 0x00260150 File Offset: 0x00260150
		internal Type MapType(Type type)
		{
			return this.model.MapType(type);
		}

		// Token: 0x17001BCF RID: 7119
		// (get) Token: 0x0600804F RID: 32847 RVA: 0x00260160 File Offset: 0x00260160
		public CompilerContext.ILVersion MetadataVersion
		{
			get
			{
				return this.metadataVersion;
			}
		}

		// Token: 0x06008050 RID: 32848 RVA: 0x00260168 File Offset: 0x00260168
		internal bool AllowInternal(PropertyInfo property)
		{
			return this.NonPublic || this.InternalsVisible(Helpers.GetAssembly(property.DeclaringType));
		}

		// Token: 0x04003D00 RID: 15616
		private readonly DynamicMethod method;

		// Token: 0x04003D01 RID: 15617
		private static int next;

		// Token: 0x04003D02 RID: 15618
		private readonly bool isStatic;

		// Token: 0x04003D03 RID: 15619
		private readonly RuntimeTypeModel.SerializerPair[] methodPairs;

		// Token: 0x04003D04 RID: 15620
		private readonly bool isWriter;

		// Token: 0x04003D05 RID: 15621
		private readonly bool nonPublic;

		// Token: 0x04003D06 RID: 15622
		private readonly Local inputValue;

		// Token: 0x04003D07 RID: 15623
		private readonly string assemblyName;

		// Token: 0x04003D08 RID: 15624
		private readonly ILGenerator il;

		// Token: 0x04003D09 RID: 15625
		private MutableList locals = new MutableList();

		// Token: 0x04003D0A RID: 15626
		private int nextLabel;

		// Token: 0x04003D0B RID: 15627
		private BasicList knownTrustedAssemblies;

		// Token: 0x04003D0C RID: 15628
		private BasicList knownUntrustedAssemblies;

		// Token: 0x04003D0D RID: 15629
		private readonly TypeModel model;

		// Token: 0x04003D0E RID: 15630
		private readonly CompilerContext.ILVersion metadataVersion;

		// Token: 0x02001189 RID: 4489
		private sealed class UsingBlock : IDisposable
		{
			// Token: 0x060093B1 RID: 37809 RVA: 0x002C3874 File Offset: 0x002C3874
			public UsingBlock(CompilerContext ctx, Local local)
			{
				if (ctx == null)
				{
					throw new ArgumentNullException("ctx");
				}
				if (local == null)
				{
					throw new ArgumentNullException("local");
				}
				Type type = local.Type;
				if ((Helpers.IsValueType(type) || Helpers.IsSealed(type)) && !ctx.MapType(typeof(IDisposable)).IsAssignableFrom(type))
				{
					return;
				}
				this.local = local;
				this.ctx = ctx;
				this.label = ctx.BeginTry();
			}

			// Token: 0x060093B2 RID: 37810 RVA: 0x002C3900 File Offset: 0x002C3900
			public void Dispose()
			{
				if (this.local == null || this.ctx == null)
				{
					return;
				}
				this.ctx.EndTry(this.label, false);
				this.ctx.BeginFinally();
				Type type = this.ctx.MapType(typeof(IDisposable));
				MethodInfo method = type.GetMethod("Dispose");
				Type type2 = this.local.Type;
				if (Helpers.IsValueType(type2))
				{
					this.ctx.LoadAddress(this.local, type2, false);
					if (this.ctx.MetadataVersion == CompilerContext.ILVersion.Net1)
					{
						this.ctx.LoadValue(this.local);
						this.ctx.CastToObject(type2);
					}
					else
					{
						this.ctx.Constrain(type2);
					}
					this.ctx.EmitCall(method);
				}
				else
				{
					CodeLabel codeLabel = this.ctx.DefineLabel();
					if (type.IsAssignableFrom(type2))
					{
						this.ctx.LoadValue(this.local);
						this.ctx.BranchIfFalse(codeLabel, true);
						this.ctx.LoadAddress(this.local, type2, false);
					}
					else
					{
						using (Local local = new Local(this.ctx, type))
						{
							this.ctx.LoadValue(this.local);
							this.ctx.TryCast(type);
							this.ctx.CopyValue();
							this.ctx.StoreValue(local);
							this.ctx.BranchIfFalse(codeLabel, true);
							this.ctx.LoadAddress(local, type, false);
						}
					}
					this.ctx.EmitCall(method);
					this.ctx.MarkLabel(codeLabel);
				}
				this.ctx.EndFinally();
				this.local = null;
				this.ctx = null;
				this.label = default(CodeLabel);
			}

			// Token: 0x04004B92 RID: 19346
			private Local local;

			// Token: 0x04004B93 RID: 19347
			private CompilerContext ctx;

			// Token: 0x04004B94 RID: 19348
			private CodeLabel label;
		}

		// Token: 0x0200118A RID: 4490
		public enum ILVersion
		{
			// Token: 0x04004B96 RID: 19350
			Net1,
			// Token: 0x04004B97 RID: 19351
			Net2
		}
	}
}
