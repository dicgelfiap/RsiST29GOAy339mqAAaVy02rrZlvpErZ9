using System;
using System.Reflection;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000C66 RID: 3174
	internal sealed class SurrogateSerializer : IProtoTypeSerializer, IProtoSerializer
	{
		// Token: 0x06007E0E RID: 32270 RVA: 0x002513F4 File Offset: 0x002513F4
		bool IProtoTypeSerializer.HasCallbacks(TypeModel.CallbackType callbackType)
		{
			return false;
		}

		// Token: 0x06007E0F RID: 32271 RVA: 0x002513F8 File Offset: 0x002513F8
		void IProtoTypeSerializer.EmitCallback(CompilerContext ctx, Local valueFrom, TypeModel.CallbackType callbackType)
		{
		}

		// Token: 0x06007E10 RID: 32272 RVA: 0x002513FC File Offset: 0x002513FC
		void IProtoTypeSerializer.EmitCreateInstance(CompilerContext ctx)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06007E11 RID: 32273 RVA: 0x00251404 File Offset: 0x00251404
		bool IProtoTypeSerializer.CanCreateInstance()
		{
			return false;
		}

		// Token: 0x06007E12 RID: 32274 RVA: 0x00251408 File Offset: 0x00251408
		object IProtoTypeSerializer.CreateInstance(ProtoReader source)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06007E13 RID: 32275 RVA: 0x00251410 File Offset: 0x00251410
		void IProtoTypeSerializer.Callback(object value, TypeModel.CallbackType callbackType, SerializationContext context)
		{
		}

		// Token: 0x17001B60 RID: 7008
		// (get) Token: 0x06007E14 RID: 32276 RVA: 0x00251414 File Offset: 0x00251414
		public bool ReturnsValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B61 RID: 7009
		// (get) Token: 0x06007E15 RID: 32277 RVA: 0x00251418 File Offset: 0x00251418
		public bool RequiresOldValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001B62 RID: 7010
		// (get) Token: 0x06007E16 RID: 32278 RVA: 0x0025141C File Offset: 0x0025141C
		public Type ExpectedType
		{
			get
			{
				return this.forType;
			}
		}

		// Token: 0x06007E17 RID: 32279 RVA: 0x00251424 File Offset: 0x00251424
		public SurrogateSerializer(TypeModel model, Type forType, Type declaredType, IProtoTypeSerializer rootTail)
		{
			this.forType = forType;
			this.declaredType = declaredType;
			this.rootTail = rootTail;
			this.toTail = this.GetConversion(model, true);
			this.fromTail = this.GetConversion(model, false);
		}

		// Token: 0x06007E18 RID: 32280 RVA: 0x00251470 File Offset: 0x00251470
		private static bool HasCast(TypeModel model, Type type, Type from, Type to, out MethodInfo op)
		{
			MethodInfo[] methods = type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			Type type2 = null;
			foreach (MethodInfo methodInfo in methods)
			{
				if (!(methodInfo.ReturnType != to))
				{
					ParameterInfo[] parameters = methodInfo.GetParameters();
					if (parameters.Length == 1 && parameters[0].ParameterType == from)
					{
						if (type2 == null)
						{
							type2 = model.MapType(typeof(ProtoConverterAttribute), false);
							if (type2 == null)
							{
								break;
							}
						}
						if (methodInfo.IsDefined(type2, true))
						{
							op = methodInfo;
							return true;
						}
					}
				}
			}
			foreach (MethodInfo methodInfo2 in methods)
			{
				if ((!(methodInfo2.Name != "op_Implicit") || !(methodInfo2.Name != "op_Explicit")) && !(methodInfo2.ReturnType != to))
				{
					ParameterInfo[] parameters = methodInfo2.GetParameters();
					if (parameters.Length == 1 && parameters[0].ParameterType == from)
					{
						op = methodInfo2;
						return true;
					}
				}
			}
			op = null;
			return false;
		}

		// Token: 0x06007E19 RID: 32281 RVA: 0x002515B8 File Offset: 0x002515B8
		public MethodInfo GetConversion(TypeModel model, bool toTail)
		{
			Type to = toTail ? this.declaredType : this.forType;
			Type from = toTail ? this.forType : this.declaredType;
			MethodInfo result;
			if (SurrogateSerializer.HasCast(model, this.declaredType, from, to, out result) || SurrogateSerializer.HasCast(model, this.forType, from, to, out result))
			{
				return result;
			}
			throw new InvalidOperationException("No suitable conversion operator found for surrogate: " + this.forType.FullName + " / " + this.declaredType.FullName);
		}

		// Token: 0x06007E1A RID: 32282 RVA: 0x00251650 File Offset: 0x00251650
		public void Write(object value, ProtoWriter writer)
		{
			this.rootTail.Write(this.toTail.Invoke(null, new object[]
			{
				value
			}), writer);
		}

		// Token: 0x06007E1B RID: 32283 RVA: 0x00251684 File Offset: 0x00251684
		public object Read(object value, ProtoReader source)
		{
			object[] array = new object[]
			{
				value
			};
			value = this.toTail.Invoke(null, array);
			array[0] = this.rootTail.Read(value, source);
			return this.fromTail.Invoke(null, array);
		}

		// Token: 0x06007E1C RID: 32284 RVA: 0x002516CC File Offset: 0x002516CC
		void IProtoSerializer.EmitRead(CompilerContext ctx, Local valueFrom)
		{
			using (Local local = new Local(ctx, this.declaredType))
			{
				ctx.LoadValue(valueFrom);
				ctx.EmitCall(this.toTail);
				ctx.StoreValue(local);
				this.rootTail.EmitRead(ctx, local);
				ctx.LoadValue(local);
				ctx.EmitCall(this.fromTail);
				ctx.StoreValue(valueFrom);
			}
		}

		// Token: 0x06007E1D RID: 32285 RVA: 0x0025174C File Offset: 0x0025174C
		void IProtoSerializer.EmitWrite(CompilerContext ctx, Local valueFrom)
		{
			ctx.LoadValue(valueFrom);
			ctx.EmitCall(this.toTail);
			this.rootTail.EmitWrite(ctx, null);
		}

		// Token: 0x04003C81 RID: 15489
		private readonly Type forType;

		// Token: 0x04003C82 RID: 15490
		private readonly Type declaredType;

		// Token: 0x04003C83 RID: 15491
		private readonly MethodInfo toTail;

		// Token: 0x04003C84 RID: 15492
		private readonly MethodInfo fromTail;

		// Token: 0x04003C85 RID: 15493
		private IProtoTypeSerializer rootTail;
	}
}
