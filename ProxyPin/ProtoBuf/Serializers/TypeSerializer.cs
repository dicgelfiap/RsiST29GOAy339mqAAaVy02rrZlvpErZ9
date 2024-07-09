using System;
using System.Reflection;
using System.Runtime.Serialization;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000C6B RID: 3179
	internal sealed class TypeSerializer : IProtoTypeSerializer, IProtoSerializer
	{
		// Token: 0x06007E4F RID: 32335 RVA: 0x00252520 File Offset: 0x00252520
		public bool HasCallbacks(TypeModel.CallbackType callbackType)
		{
			if (this.callbacks != null && this.callbacks[callbackType] != null)
			{
				return true;
			}
			for (int i = 0; i < this.serializers.Length; i++)
			{
				if (this.serializers[i].ExpectedType != this.forType && ((IProtoTypeSerializer)this.serializers[i]).HasCallbacks(callbackType))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17001B70 RID: 7024
		// (get) Token: 0x06007E50 RID: 32336 RVA: 0x002525AC File Offset: 0x002525AC
		public Type ExpectedType
		{
			get
			{
				return this.forType;
			}
		}

		// Token: 0x06007E51 RID: 32337 RVA: 0x002525B4 File Offset: 0x002525B4
		public TypeSerializer(TypeModel model, Type forType, int[] fieldNumbers, IProtoSerializer[] serializers, MethodInfo[] baseCtorCallbacks, bool isRootType, bool useConstructor, CallbackSet callbacks, Type constructType, MethodInfo factory)
		{
			Helpers.Sort(fieldNumbers, serializers);
			bool flag = false;
			for (int i = 0; i < fieldNumbers.Length; i++)
			{
				if (i != 0 && fieldNumbers[i] == fieldNumbers[i - 1])
				{
					throw new InvalidOperationException("Duplicate field-number detected; " + fieldNumbers[i].ToString() + " on: " + forType.FullName);
				}
				if (!flag && serializers[i].ExpectedType != forType)
				{
					flag = true;
				}
			}
			this.forType = forType;
			this.factory = factory;
			if (constructType == null)
			{
				constructType = forType;
			}
			else if (!forType.IsAssignableFrom(constructType))
			{
				throw new InvalidOperationException(forType.FullName + " cannot be assigned from " + constructType.FullName);
			}
			this.constructType = constructType;
			this.serializers = serializers;
			this.fieldNumbers = fieldNumbers;
			this.callbacks = callbacks;
			this.isRootType = isRootType;
			this.useConstructor = useConstructor;
			if (baseCtorCallbacks != null && baseCtorCallbacks.Length == 0)
			{
				baseCtorCallbacks = null;
			}
			this.baseCtorCallbacks = baseCtorCallbacks;
			if (Helpers.GetUnderlyingType(forType) != null)
			{
				throw new ArgumentException("Cannot create a TypeSerializer for nullable types", "forType");
			}
			if (model.MapType(TypeSerializer.iextensible).IsAssignableFrom(forType))
			{
				if (forType.IsValueType || !isRootType || flag)
				{
					throw new NotSupportedException("IExtensible is not supported in structs or classes with inheritance");
				}
				this.isExtensible = true;
			}
			this.hasConstructor = (!constructType.IsAbstract && Helpers.GetConstructor(constructType, Helpers.EmptyTypes, true) != null);
			if (constructType != forType && useConstructor && !this.hasConstructor)
			{
				throw new ArgumentException("The supplied default implementation cannot be created: " + constructType.FullName, "constructType");
			}
		}

		// Token: 0x17001B71 RID: 7025
		// (get) Token: 0x06007E52 RID: 32338 RVA: 0x002527A4 File Offset: 0x002527A4
		private bool CanHaveInheritance
		{
			get
			{
				return (this.forType.IsClass || this.forType.IsInterface) && !this.forType.IsSealed;
			}
		}

		// Token: 0x06007E53 RID: 32339 RVA: 0x002527D8 File Offset: 0x002527D8
		bool IProtoTypeSerializer.CanCreateInstance()
		{
			return true;
		}

		// Token: 0x06007E54 RID: 32340 RVA: 0x002527DC File Offset: 0x002527DC
		object IProtoTypeSerializer.CreateInstance(ProtoReader source)
		{
			return this.CreateInstance(source, false);
		}

		// Token: 0x06007E55 RID: 32341 RVA: 0x002527E8 File Offset: 0x002527E8
		public void Callback(object value, TypeModel.CallbackType callbackType, SerializationContext context)
		{
			if (this.callbacks != null)
			{
				this.InvokeCallback(this.callbacks[callbackType], value, context);
			}
			IProtoTypeSerializer protoTypeSerializer = (IProtoTypeSerializer)this.GetMoreSpecificSerializer(value);
			if (protoTypeSerializer != null)
			{
				protoTypeSerializer.Callback(value, callbackType, context);
			}
		}

		// Token: 0x06007E56 RID: 32342 RVA: 0x00252838 File Offset: 0x00252838
		private IProtoSerializer GetMoreSpecificSerializer(object value)
		{
			if (!this.CanHaveInheritance)
			{
				return null;
			}
			Type type = value.GetType();
			if (type == this.forType)
			{
				return null;
			}
			for (int i = 0; i < this.serializers.Length; i++)
			{
				IProtoSerializer protoSerializer = this.serializers[i];
				if (protoSerializer.ExpectedType != this.forType && Helpers.IsAssignableFrom(protoSerializer.ExpectedType, type))
				{
					return protoSerializer;
				}
			}
			if (type == this.constructType)
			{
				return null;
			}
			TypeModel.ThrowUnexpectedSubtype(this.forType, type);
			return null;
		}

		// Token: 0x06007E57 RID: 32343 RVA: 0x002528DC File Offset: 0x002528DC
		public void Write(object value, ProtoWriter dest)
		{
			if (this.isRootType)
			{
				this.Callback(value, TypeModel.CallbackType.BeforeSerialize, dest.Context);
			}
			IProtoSerializer moreSpecificSerializer = this.GetMoreSpecificSerializer(value);
			if (moreSpecificSerializer != null)
			{
				moreSpecificSerializer.Write(value, dest);
			}
			for (int i = 0; i < this.serializers.Length; i++)
			{
				IProtoSerializer protoSerializer = this.serializers[i];
				if (protoSerializer.ExpectedType == this.forType)
				{
					protoSerializer.Write(value, dest);
				}
			}
			if (this.isExtensible)
			{
				ProtoWriter.AppendExtensionData((IExtensible)value, dest);
			}
			if (this.isRootType)
			{
				this.Callback(value, TypeModel.CallbackType.AfterSerialize, dest.Context);
			}
		}

		// Token: 0x06007E58 RID: 32344 RVA: 0x00252990 File Offset: 0x00252990
		public object Read(object value, ProtoReader source)
		{
			if (this.isRootType && value != null)
			{
				this.Callback(value, TypeModel.CallbackType.BeforeDeserialize, source.Context);
			}
			int num = 0;
			int num2 = 0;
			int num3;
			while ((num3 = source.ReadFieldHeader()) > 0)
			{
				bool flag = false;
				if (num3 < num)
				{
					num2 = (num = 0);
				}
				for (int i = num2; i < this.fieldNumbers.Length; i++)
				{
					if (this.fieldNumbers[i] == num3)
					{
						IProtoSerializer protoSerializer = this.serializers[i];
						Type expectedType = protoSerializer.ExpectedType;
						if (value == null)
						{
							if (expectedType == this.forType)
							{
								value = this.CreateInstance(source, true);
							}
						}
						else if (expectedType != this.forType && ((IProtoTypeSerializer)protoSerializer).CanCreateInstance() && expectedType.IsSubclassOf(value.GetType()))
						{
							value = ProtoReader.Merge(source, value, ((IProtoTypeSerializer)protoSerializer).CreateInstance(source));
						}
						if (protoSerializer.ReturnsValue)
						{
							value = protoSerializer.Read(value, source);
						}
						else
						{
							protoSerializer.Read(value, source);
						}
						num2 = i;
						num = num3;
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					if (value == null)
					{
						value = this.CreateInstance(source, true);
					}
					if (this.isExtensible)
					{
						source.AppendExtensionData((IExtensible)value);
					}
					else
					{
						source.SkipField();
					}
				}
			}
			if (value == null)
			{
				value = this.CreateInstance(source, true);
			}
			if (this.isRootType)
			{
				this.Callback(value, TypeModel.CallbackType.AfterDeserialize, source.Context);
			}
			return value;
		}

		// Token: 0x06007E59 RID: 32345 RVA: 0x00252B30 File Offset: 0x00252B30
		private object InvokeCallback(MethodInfo method, object obj, SerializationContext context)
		{
			object result = null;
			if (method != null)
			{
				ParameterInfo[] parameters = method.GetParameters();
				object[] array;
				bool flag;
				if (parameters.Length == 0)
				{
					array = null;
					flag = true;
				}
				else
				{
					array = new object[parameters.Length];
					flag = true;
					for (int i = 0; i < array.Length; i++)
					{
						Type parameterType = parameters[i].ParameterType;
						object obj2;
						if (parameterType == typeof(SerializationContext))
						{
							obj2 = context;
						}
						else if (parameterType == typeof(Type))
						{
							obj2 = this.constructType;
						}
						else if (parameterType == typeof(StreamingContext))
						{
							obj2 = context;
						}
						else
						{
							obj2 = null;
							flag = false;
						}
						array[i] = obj2;
					}
				}
				if (!flag)
				{
					throw CallbackSet.CreateInvalidCallbackSignature(method);
				}
				result = method.Invoke(obj, array);
			}
			return result;
		}

		// Token: 0x06007E5A RID: 32346 RVA: 0x00252C30 File Offset: 0x00252C30
		private object CreateInstance(ProtoReader source, bool includeLocalCallback)
		{
			object obj;
			if (this.factory != null)
			{
				obj = this.InvokeCallback(this.factory, null, source.Context);
			}
			else if (this.useConstructor)
			{
				if (!this.hasConstructor)
				{
					TypeModel.ThrowCannotCreateInstance(this.constructType);
				}
				obj = Activator.CreateInstance(this.constructType, true);
			}
			else
			{
				obj = BclHelpers.GetUninitializedObject(this.constructType);
			}
			ProtoReader.NoteObject(obj, source);
			if (this.baseCtorCallbacks != null)
			{
				for (int i = 0; i < this.baseCtorCallbacks.Length; i++)
				{
					this.InvokeCallback(this.baseCtorCallbacks[i], obj, source.Context);
				}
			}
			if (includeLocalCallback && this.callbacks != null)
			{
				this.InvokeCallback(this.callbacks.BeforeDeserialize, obj, source.Context);
			}
			return obj;
		}

		// Token: 0x17001B72 RID: 7026
		// (get) Token: 0x06007E5B RID: 32347 RVA: 0x00252D18 File Offset: 0x00252D18
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001B73 RID: 7027
		// (get) Token: 0x06007E5C RID: 32348 RVA: 0x00252D1C File Offset: 0x00252D1C
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007E5D RID: 32349 RVA: 0x00252D20 File Offset: 0x00252D20
		void IProtoSerializer.EmitWrite(CompilerContext ctx, Local valueFrom)
		{
			Type expectedType = this.ExpectedType;
			using (Local localWithValue = ctx.GetLocalWithValue(expectedType, valueFrom))
			{
				this.EmitCallbackIfNeeded(ctx, localWithValue, TypeModel.CallbackType.BeforeSerialize);
				CodeLabel label = ctx.DefineLabel();
				if (this.CanHaveInheritance)
				{
					for (int i = 0; i < this.serializers.Length; i++)
					{
						IProtoSerializer protoSerializer = this.serializers[i];
						Type expectedType2 = protoSerializer.ExpectedType;
						if (expectedType2 != this.forType)
						{
							CodeLabel label2 = ctx.DefineLabel();
							CodeLabel label3 = ctx.DefineLabel();
							ctx.LoadValue(localWithValue);
							ctx.TryCast(expectedType2);
							ctx.CopyValue();
							ctx.BranchIfTrue(label2, true);
							ctx.DiscardValue();
							ctx.Branch(label3, true);
							ctx.MarkLabel(label2);
							if (Helpers.IsValueType(expectedType2))
							{
								ctx.DiscardValue();
								ctx.LoadValue(localWithValue);
								ctx.CastFromObject(expectedType2);
							}
							protoSerializer.EmitWrite(ctx, null);
							ctx.Branch(label, false);
							ctx.MarkLabel(label3);
						}
					}
					if (this.constructType != null && this.constructType != this.forType)
					{
						using (Local local = new Local(ctx, ctx.MapType(typeof(Type))))
						{
							ctx.LoadValue(localWithValue);
							ctx.EmitCall(ctx.MapType(typeof(object)).GetMethod("GetType"));
							ctx.CopyValue();
							ctx.StoreValue(local);
							ctx.LoadValue(this.forType);
							ctx.BranchIfEqual(label, true);
							ctx.LoadValue(local);
							ctx.LoadValue(this.constructType);
							ctx.BranchIfEqual(label, true);
							goto IL_1DF;
						}
					}
					ctx.LoadValue(localWithValue);
					ctx.EmitCall(ctx.MapType(typeof(object)).GetMethod("GetType"));
					ctx.LoadValue(this.forType);
					ctx.BranchIfEqual(label, true);
					IL_1DF:
					ctx.LoadValue(this.forType);
					ctx.LoadValue(localWithValue);
					ctx.EmitCall(ctx.MapType(typeof(object)).GetMethod("GetType"));
					ctx.EmitCall(ctx.MapType(typeof(TypeModel)).GetMethod("ThrowUnexpectedSubtype", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic));
				}
				ctx.MarkLabel(label);
				for (int j = 0; j < this.serializers.Length; j++)
				{
					IProtoSerializer protoSerializer2 = this.serializers[j];
					if (protoSerializer2.ExpectedType == this.forType)
					{
						protoSerializer2.EmitWrite(ctx, localWithValue);
					}
				}
				if (this.isExtensible)
				{
					ctx.LoadValue(localWithValue);
					ctx.LoadReaderWriter();
					ctx.EmitCall(ctx.MapType(typeof(ProtoWriter)).GetMethod("AppendExtensionData"));
				}
				this.EmitCallbackIfNeeded(ctx, localWithValue, TypeModel.CallbackType.AfterSerialize);
			}
		}

		// Token: 0x06007E5E RID: 32350 RVA: 0x00253038 File Offset: 0x00253038
		private static void EmitInvokeCallback(CompilerContext ctx, MethodInfo method, bool copyValue, Type constructType, Type type)
		{
			if (method != null)
			{
				if (copyValue)
				{
					ctx.CopyValue();
				}
				ParameterInfo[] parameters = method.GetParameters();
				bool flag = true;
				for (int i = 0; i < parameters.Length; i++)
				{
					Type parameterType = parameters[i].ParameterType;
					if (parameterType == ctx.MapType(typeof(SerializationContext)))
					{
						ctx.LoadSerializationContext();
					}
					else if (parameterType == ctx.MapType(typeof(Type)))
					{
						Type type2 = constructType;
						if (type2 == null)
						{
							type2 = type;
						}
						ctx.LoadValue(type2);
					}
					else if (parameterType == ctx.MapType(typeof(StreamingContext)))
					{
						ctx.LoadSerializationContext();
						MethodInfo method2 = ctx.MapType(typeof(SerializationContext)).GetMethod("op_Implicit", new Type[]
						{
							ctx.MapType(typeof(SerializationContext))
						});
						if (method2 != null)
						{
							ctx.EmitCall(method2);
							flag = true;
						}
					}
					else
					{
						flag = false;
					}
				}
				if (!flag)
				{
					throw CallbackSet.CreateInvalidCallbackSignature(method);
				}
				ctx.EmitCall(method);
				if (constructType != null && method.ReturnType == ctx.MapType(typeof(object)))
				{
					ctx.CastFromObject(type);
					return;
				}
			}
		}

		// Token: 0x06007E5F RID: 32351 RVA: 0x002531AC File Offset: 0x002531AC
		private void EmitCallbackIfNeeded(CompilerContext ctx, Local valueFrom, TypeModel.CallbackType callbackType)
		{
			if (this.isRootType && ((IProtoTypeSerializer)this).HasCallbacks(callbackType))
			{
				((IProtoTypeSerializer)this).EmitCallback(ctx, valueFrom, callbackType);
			}
		}

		// Token: 0x06007E60 RID: 32352 RVA: 0x002531D0 File Offset: 0x002531D0
		void IProtoTypeSerializer.EmitCallback(CompilerContext ctx, Local valueFrom, TypeModel.CallbackType callbackType)
		{
			bool flag = false;
			if (this.CanHaveInheritance)
			{
				for (int i = 0; i < this.serializers.Length; i++)
				{
					IProtoSerializer protoSerializer = this.serializers[i];
					if (protoSerializer.ExpectedType != this.forType && ((IProtoTypeSerializer)protoSerializer).HasCallbacks(callbackType))
					{
						flag = true;
					}
				}
			}
			CallbackSet callbackSet = this.callbacks;
			MethodInfo methodInfo = (callbackSet != null) ? callbackSet[callbackType] : null;
			if (methodInfo == null && !flag)
			{
				return;
			}
			ctx.LoadAddress(valueFrom, this.ExpectedType, false);
			TypeSerializer.EmitInvokeCallback(ctx, methodInfo, flag, null, this.forType);
			if (flag)
			{
				CodeLabel label = ctx.DefineLabel();
				for (int j = 0; j < this.serializers.Length; j++)
				{
					IProtoSerializer protoSerializer2 = this.serializers[j];
					Type expectedType = protoSerializer2.ExpectedType;
					IProtoTypeSerializer protoTypeSerializer;
					if (expectedType != this.forType && (protoTypeSerializer = (IProtoTypeSerializer)protoSerializer2).HasCallbacks(callbackType))
					{
						CodeLabel label2 = ctx.DefineLabel();
						CodeLabel label3 = ctx.DefineLabel();
						ctx.CopyValue();
						ctx.TryCast(expectedType);
						ctx.CopyValue();
						ctx.BranchIfTrue(label2, true);
						ctx.DiscardValue();
						ctx.Branch(label3, false);
						ctx.MarkLabel(label2);
						protoTypeSerializer.EmitCallback(ctx, null, callbackType);
						ctx.Branch(label, false);
						ctx.MarkLabel(label3);
					}
				}
				ctx.MarkLabel(label);
				ctx.DiscardValue();
			}
		}

		// Token: 0x06007E61 RID: 32353 RVA: 0x0025335C File Offset: 0x0025335C
		void IProtoSerializer.EmitRead(CompilerContext ctx, Local valueFrom)
		{
			Type expectedType = this.ExpectedType;
			using (Local localWithValue = ctx.GetLocalWithValue(expectedType, valueFrom))
			{
				using (Local local = new Local(ctx, ctx.MapType(typeof(int))))
				{
					if (this.HasCallbacks(TypeModel.CallbackType.BeforeDeserialize))
					{
						if (Helpers.IsValueType(this.ExpectedType))
						{
							this.EmitCallbackIfNeeded(ctx, localWithValue, TypeModel.CallbackType.BeforeDeserialize);
						}
						else
						{
							CodeLabel label = ctx.DefineLabel();
							ctx.LoadValue(localWithValue);
							ctx.BranchIfFalse(label, false);
							this.EmitCallbackIfNeeded(ctx, localWithValue, TypeModel.CallbackType.BeforeDeserialize);
							ctx.MarkLabel(label);
						}
					}
					CodeLabel codeLabel = ctx.DefineLabel();
					CodeLabel label2 = ctx.DefineLabel();
					ctx.Branch(codeLabel, false);
					ctx.MarkLabel(label2);
					int[] keys = this.fieldNumbers;
					object[] values = this.serializers;
					foreach (object obj in BasicList.GetContiguousGroups(keys, values))
					{
						BasicList.Group group = (BasicList.Group)obj;
						CodeLabel label3 = ctx.DefineLabel();
						int count = group.Items.Count;
						if (count == 1)
						{
							ctx.LoadValue(local);
							ctx.LoadValue(group.First);
							CodeLabel codeLabel2 = ctx.DefineLabel();
							ctx.BranchIfEqual(codeLabel2, true);
							ctx.Branch(label3, false);
							this.WriteFieldHandler(ctx, expectedType, localWithValue, codeLabel2, codeLabel, (IProtoSerializer)group.Items[0]);
						}
						else
						{
							ctx.LoadValue(local);
							ctx.LoadValue(group.First);
							ctx.Subtract();
							CodeLabel[] array = new CodeLabel[count];
							for (int i = 0; i < count; i++)
							{
								array[i] = ctx.DefineLabel();
							}
							ctx.Switch(array);
							ctx.Branch(label3, false);
							for (int j = 0; j < count; j++)
							{
								this.WriteFieldHandler(ctx, expectedType, localWithValue, array[j], codeLabel, (IProtoSerializer)group.Items[j]);
							}
						}
						ctx.MarkLabel(label3);
					}
					this.EmitCreateIfNull(ctx, localWithValue);
					ctx.LoadReaderWriter();
					if (this.isExtensible)
					{
						ctx.LoadValue(localWithValue);
						ctx.EmitCall(ctx.MapType(typeof(ProtoReader)).GetMethod("AppendExtensionData"));
					}
					else
					{
						ctx.EmitCall(ctx.MapType(typeof(ProtoReader)).GetMethod("SkipField"));
					}
					ctx.MarkLabel(codeLabel);
					ctx.EmitBasicRead("ReadFieldHeader", ctx.MapType(typeof(int)));
					ctx.CopyValue();
					ctx.StoreValue(local);
					ctx.LoadValue(0);
					ctx.BranchIfGreater(label2, false);
					this.EmitCreateIfNull(ctx, localWithValue);
					this.EmitCallbackIfNeeded(ctx, localWithValue, TypeModel.CallbackType.AfterDeserialize);
					if (valueFrom != null && !localWithValue.IsSame(valueFrom))
					{
						ctx.LoadValue(localWithValue);
						ctx.Cast(valueFrom.Type);
						ctx.StoreValue(valueFrom);
					}
				}
			}
		}

		// Token: 0x06007E62 RID: 32354 RVA: 0x0025367C File Offset: 0x0025367C
		private void WriteFieldHandler(CompilerContext ctx, Type expected, Local loc, CodeLabel handler, CodeLabel @continue, IProtoSerializer serializer)
		{
			ctx.MarkLabel(handler);
			Type expectedType = serializer.ExpectedType;
			if (expectedType == this.forType)
			{
				this.EmitCreateIfNull(ctx, loc);
				serializer.EmitRead(ctx, loc);
			}
			else
			{
				if (((IProtoTypeSerializer)serializer).CanCreateInstance())
				{
					CodeLabel label = ctx.DefineLabel();
					ctx.LoadValue(loc);
					ctx.BranchIfFalse(label, false);
					ctx.LoadValue(loc);
					ctx.TryCast(expectedType);
					ctx.BranchIfTrue(label, false);
					ctx.LoadReaderWriter();
					ctx.LoadValue(loc);
					((IProtoTypeSerializer)serializer).EmitCreateInstance(ctx);
					ctx.EmitCall(ctx.MapType(typeof(ProtoReader)).GetMethod("Merge"));
					ctx.Cast(expected);
					ctx.StoreValue(loc);
					ctx.MarkLabel(label);
				}
				if (Helpers.IsValueType(expectedType))
				{
					CodeLabel label2 = ctx.DefineLabel();
					CodeLabel label3 = ctx.DefineLabel();
					using (Local local = new Local(ctx, expectedType))
					{
						ctx.LoadValue(loc);
						ctx.BranchIfFalse(label2, false);
						ctx.LoadValue(loc);
						ctx.CastFromObject(expectedType);
						ctx.Branch(label3, false);
						ctx.MarkLabel(label2);
						ctx.InitLocal(expectedType, local);
						ctx.LoadValue(local);
						ctx.MarkLabel(label3);
						goto IL_14B;
					}
				}
				ctx.LoadValue(loc);
				ctx.Cast(expectedType);
				IL_14B:
				serializer.EmitRead(ctx, null);
			}
			if (serializer.ReturnsValue)
			{
				if (Helpers.IsValueType(expectedType))
				{
					ctx.CastToObject(expectedType);
				}
				ctx.StoreValue(loc);
			}
			ctx.Branch(@continue, false);
		}

		// Token: 0x06007E63 RID: 32355 RVA: 0x0025381C File Offset: 0x0025381C
		void IProtoTypeSerializer.EmitCreateInstance(CompilerContext ctx)
		{
			bool flag = true;
			if (this.factory != null)
			{
				TypeSerializer.EmitInvokeCallback(ctx, this.factory, false, this.constructType, this.forType);
			}
			else if (!this.useConstructor)
			{
				ctx.LoadValue(this.constructType);
				ctx.EmitCall(ctx.MapType(typeof(BclHelpers)).GetMethod("GetUninitializedObject"));
				ctx.Cast(this.forType);
			}
			else if (Helpers.IsClass(this.constructType) && this.hasConstructor)
			{
				ctx.EmitCtor(this.constructType);
			}
			else
			{
				ctx.LoadValue(this.ExpectedType);
				ctx.EmitCall(ctx.MapType(typeof(TypeModel)).GetMethod("ThrowCannotCreateInstance", BindingFlags.Static | BindingFlags.Public));
				ctx.LoadNullRef();
				flag = false;
			}
			if (flag)
			{
				ctx.CopyValue();
				ctx.LoadReaderWriter();
				ctx.EmitCall(ctx.MapType(typeof(ProtoReader)).GetMethod("NoteObject", BindingFlags.Static | BindingFlags.Public));
			}
			if (this.baseCtorCallbacks != null)
			{
				for (int i = 0; i < this.baseCtorCallbacks.Length; i++)
				{
					TypeSerializer.EmitInvokeCallback(ctx, this.baseCtorCallbacks[i], true, null, this.forType);
				}
			}
		}

		// Token: 0x06007E64 RID: 32356 RVA: 0x00253978 File Offset: 0x00253978
		private void EmitCreateIfNull(CompilerContext ctx, Local storage)
		{
			if (!Helpers.IsValueType(this.ExpectedType))
			{
				CodeLabel label = ctx.DefineLabel();
				ctx.LoadValue(storage);
				ctx.BranchIfTrue(label, false);
				((IProtoTypeSerializer)this).EmitCreateInstance(ctx);
				if (this.callbacks != null)
				{
					TypeSerializer.EmitInvokeCallback(ctx, this.callbacks.BeforeDeserialize, true, null, this.forType);
				}
				ctx.StoreValue(storage);
				ctx.MarkLabel(label);
			}
		}

		// Token: 0x04003C8F RID: 15503
		private readonly Type forType;

		// Token: 0x04003C90 RID: 15504
		private readonly Type constructType;

		// Token: 0x04003C91 RID: 15505
		private readonly IProtoSerializer[] serializers;

		// Token: 0x04003C92 RID: 15506
		private readonly int[] fieldNumbers;

		// Token: 0x04003C93 RID: 15507
		private readonly bool isRootType;

		// Token: 0x04003C94 RID: 15508
		private readonly bool useConstructor;

		// Token: 0x04003C95 RID: 15509
		private readonly bool isExtensible;

		// Token: 0x04003C96 RID: 15510
		private readonly bool hasConstructor;

		// Token: 0x04003C97 RID: 15511
		private readonly CallbackSet callbacks;

		// Token: 0x04003C98 RID: 15512
		private readonly MethodInfo[] baseCtorCallbacks;

		// Token: 0x04003C99 RID: 15513
		private readonly MethodInfo factory;

		// Token: 0x04003C9A RID: 15514
		private static readonly Type iextensible = typeof(IExtensible);
	}
}
