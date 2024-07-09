using System;
using System.Reflection.Emit;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000C65 RID: 3173
	internal sealed class SubItemSerializer : IProtoTypeSerializer, IProtoSerializer
	{
		// Token: 0x06007DFF RID: 32255 RVA: 0x00250FD4 File Offset: 0x00250FD4
		bool IProtoTypeSerializer.HasCallbacks(TypeModel.CallbackType callbackType)
		{
			return ((IProtoTypeSerializer)this.proxy.Serializer).HasCallbacks(callbackType);
		}

		// Token: 0x06007E00 RID: 32256 RVA: 0x00250FEC File Offset: 0x00250FEC
		bool IProtoTypeSerializer.CanCreateInstance()
		{
			return ((IProtoTypeSerializer)this.proxy.Serializer).CanCreateInstance();
		}

		// Token: 0x06007E01 RID: 32257 RVA: 0x00251004 File Offset: 0x00251004
		void IProtoTypeSerializer.EmitCallback(CompilerContext ctx, Local valueFrom, TypeModel.CallbackType callbackType)
		{
			((IProtoTypeSerializer)this.proxy.Serializer).EmitCallback(ctx, valueFrom, callbackType);
		}

		// Token: 0x06007E02 RID: 32258 RVA: 0x00251020 File Offset: 0x00251020
		void IProtoTypeSerializer.EmitCreateInstance(CompilerContext ctx)
		{
			((IProtoTypeSerializer)this.proxy.Serializer).EmitCreateInstance(ctx);
		}

		// Token: 0x06007E03 RID: 32259 RVA: 0x00251038 File Offset: 0x00251038
		void IProtoTypeSerializer.Callback(object value, TypeModel.CallbackType callbackType, SerializationContext context)
		{
			((IProtoTypeSerializer)this.proxy.Serializer).Callback(value, callbackType, context);
		}

		// Token: 0x06007E04 RID: 32260 RVA: 0x00251054 File Offset: 0x00251054
		object IProtoTypeSerializer.CreateInstance(ProtoReader source)
		{
			return ((IProtoTypeSerializer)this.proxy.Serializer).CreateInstance(source);
		}

		// Token: 0x06007E05 RID: 32261 RVA: 0x0025106C File Offset: 0x0025106C
		public SubItemSerializer(Type type, int key, ISerializerProxy proxy, bool recursionCheck)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.type = type;
			if (proxy == null)
			{
				throw new ArgumentNullException("proxy");
			}
			this.proxy = proxy;
			this.key = key;
			this.recursionCheck = recursionCheck;
		}

		// Token: 0x17001B5D RID: 7005
		// (get) Token: 0x06007E06 RID: 32262 RVA: 0x002510C4 File Offset: 0x002510C4
		Type IProtoSerializer.ExpectedType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17001B5E RID: 7006
		// (get) Token: 0x06007E07 RID: 32263 RVA: 0x002510CC File Offset: 0x002510CC
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001B5F RID: 7007
		// (get) Token: 0x06007E08 RID: 32264 RVA: 0x002510D0 File Offset: 0x002510D0
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007E09 RID: 32265 RVA: 0x002510D4 File Offset: 0x002510D4
		void IProtoSerializer.Write(object value, ProtoWriter dest)
		{
			if (this.recursionCheck)
			{
				ProtoWriter.WriteObject(value, this.key, dest);
				return;
			}
			ProtoWriter.WriteRecursionSafeObject(value, this.key, dest);
		}

		// Token: 0x06007E0A RID: 32266 RVA: 0x002510FC File Offset: 0x002510FC
		object IProtoSerializer.Read(object value, ProtoReader source)
		{
			return ProtoReader.ReadObject(value, this.key, source);
		}

		// Token: 0x06007E0B RID: 32267 RVA: 0x0025110C File Offset: 0x0025110C
		private bool EmitDedicatedMethod(CompilerContext ctx, Local valueFrom, bool read)
		{
			MethodBuilder dedicatedMethod = ctx.GetDedicatedMethod(this.key, read);
			if (dedicatedMethod == null)
			{
				return false;
			}
			using (Local local = new Local(ctx, ctx.MapType(typeof(SubItemToken))))
			{
				Type type = ctx.MapType(read ? typeof(ProtoReader) : typeof(ProtoWriter));
				ctx.LoadValue(valueFrom);
				if (!read)
				{
					if (Helpers.IsValueType(this.type) || !this.recursionCheck)
					{
						ctx.LoadNullRef();
					}
					else
					{
						ctx.CopyValue();
					}
				}
				ctx.LoadReaderWriter();
				Type declaringType = type;
				string name = "StartSubItem";
				Type[] parameterTypes;
				if (!read)
				{
					Type[] array = new Type[2];
					array[0] = ctx.MapType(typeof(object));
					parameterTypes = array;
					array[1] = type;
				}
				else
				{
					(parameterTypes = new Type[1])[0] = type;
				}
				ctx.EmitCall(Helpers.GetStaticMethod(declaringType, name, parameterTypes));
				ctx.StoreValue(local);
				ctx.LoadReaderWriter();
				ctx.EmitCall(dedicatedMethod);
				if (read && this.type != dedicatedMethod.ReturnType)
				{
					ctx.Cast(this.type);
				}
				ctx.LoadValue(local);
				ctx.LoadReaderWriter();
				ctx.EmitCall(Helpers.GetStaticMethod(type, "EndSubItem", new Type[]
				{
					ctx.MapType(typeof(SubItemToken)),
					type
				}));
			}
			return true;
		}

		// Token: 0x06007E0C RID: 32268 RVA: 0x0025129C File Offset: 0x0025129C
		void IProtoSerializer.EmitWrite(CompilerContext ctx, Local valueFrom)
		{
			if (!this.EmitDedicatedMethod(ctx, valueFrom, false))
			{
				ctx.LoadValue(valueFrom);
				if (Helpers.IsValueType(this.type))
				{
					ctx.CastToObject(this.type);
				}
				ctx.LoadValue(ctx.MapMetaKeyToCompiledKey(this.key));
				ctx.LoadReaderWriter();
				ctx.EmitCall(Helpers.GetStaticMethod(ctx.MapType(typeof(ProtoWriter)), this.recursionCheck ? "WriteObject" : "WriteRecursionSafeObject", new Type[]
				{
					ctx.MapType(typeof(object)),
					ctx.MapType(typeof(int)),
					ctx.MapType(typeof(ProtoWriter))
				}));
			}
		}

		// Token: 0x06007E0D RID: 32269 RVA: 0x0025136C File Offset: 0x0025136C
		void IProtoSerializer.EmitRead(CompilerContext ctx, Local valueFrom)
		{
			if (!this.EmitDedicatedMethod(ctx, valueFrom, true))
			{
				ctx.LoadValue(valueFrom);
				if (Helpers.IsValueType(this.type))
				{
					ctx.CastToObject(this.type);
				}
				ctx.LoadValue(ctx.MapMetaKeyToCompiledKey(this.key));
				ctx.LoadReaderWriter();
				ctx.EmitCall(Helpers.GetStaticMethod(ctx.MapType(typeof(ProtoReader)), "ReadObject"));
				ctx.CastFromObject(this.type);
			}
		}

		// Token: 0x04003C7D RID: 15485
		private readonly int key;

		// Token: 0x04003C7E RID: 15486
		private readonly Type type;

		// Token: 0x04003C7F RID: 15487
		private readonly ISerializerProxy proxy;

		// Token: 0x04003C80 RID: 15488
		private readonly bool recursionCheck;
	}
}
