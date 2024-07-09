using System;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000C4B RID: 3147
	internal sealed class CompiledSerializer : IProtoTypeSerializer, IProtoSerializer
	{
		// Token: 0x06007D09 RID: 32009 RVA: 0x0024C328 File Offset: 0x0024C328
		bool IProtoTypeSerializer.HasCallbacks(TypeModel.CallbackType callbackType)
		{
			return this.head.HasCallbacks(callbackType);
		}

		// Token: 0x06007D0A RID: 32010 RVA: 0x0024C338 File Offset: 0x0024C338
		bool IProtoTypeSerializer.CanCreateInstance()
		{
			return this.head.CanCreateInstance();
		}

		// Token: 0x06007D0B RID: 32011 RVA: 0x0024C348 File Offset: 0x0024C348
		object IProtoTypeSerializer.CreateInstance(ProtoReader source)
		{
			return this.head.CreateInstance(source);
		}

		// Token: 0x06007D0C RID: 32012 RVA: 0x0024C358 File Offset: 0x0024C358
		public void Callback(object value, TypeModel.CallbackType callbackType, SerializationContext context)
		{
			this.head.Callback(value, callbackType, context);
		}

		// Token: 0x06007D0D RID: 32013 RVA: 0x0024C368 File Offset: 0x0024C368
		public static CompiledSerializer Wrap(IProtoTypeSerializer head, TypeModel model)
		{
			CompiledSerializer compiledSerializer = head as CompiledSerializer;
			if (compiledSerializer == null)
			{
				compiledSerializer = new CompiledSerializer(head, model);
			}
			return compiledSerializer;
		}

		// Token: 0x06007D0E RID: 32014 RVA: 0x0024C390 File Offset: 0x0024C390
		private CompiledSerializer(IProtoTypeSerializer head, TypeModel model)
		{
			this.head = head;
			this.serializer = CompilerContext.BuildSerializer(head, model);
			this.deserializer = CompilerContext.BuildDeserializer(head, model);
		}

		// Token: 0x17001B0E RID: 6926
		// (get) Token: 0x06007D0F RID: 32015 RVA: 0x0024C3BC File Offset: 0x0024C3BC
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return this.head.RequiresOldValue;
			}
		}

		// Token: 0x17001B0F RID: 6927
		// (get) Token: 0x06007D10 RID: 32016 RVA: 0x0024C3CC File Offset: 0x0024C3CC
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return this.head.ReturnsValue;
			}
		}

		// Token: 0x17001B10 RID: 6928
		// (get) Token: 0x06007D11 RID: 32017 RVA: 0x0024C3DC File Offset: 0x0024C3DC
		Type IProtoSerializer.ExpectedType
		{
			get
			{
				return this.head.ExpectedType;
			}
		}

		// Token: 0x06007D12 RID: 32018 RVA: 0x0024C3EC File Offset: 0x0024C3EC
		void IProtoSerializer.Write(object value, ProtoWriter dest)
		{
			this.serializer(value, dest);
		}

		// Token: 0x06007D13 RID: 32019 RVA: 0x0024C3FC File Offset: 0x0024C3FC
		object IProtoSerializer.Read(object value, ProtoReader source)
		{
			return this.deserializer(value, source);
		}

		// Token: 0x06007D14 RID: 32020 RVA: 0x0024C40C File Offset: 0x0024C40C
		void IProtoSerializer.EmitWrite(CompilerContext ctx, Local valueFrom)
		{
			this.head.EmitWrite(ctx, valueFrom);
		}

		// Token: 0x06007D15 RID: 32021 RVA: 0x0024C41C File Offset: 0x0024C41C
		void IProtoSerializer.EmitRead(CompilerContext ctx, Local valueFrom)
		{
			this.head.EmitRead(ctx, valueFrom);
		}

		// Token: 0x06007D16 RID: 32022 RVA: 0x0024C42C File Offset: 0x0024C42C
		void IProtoTypeSerializer.EmitCallback(CompilerContext ctx, Local valueFrom, TypeModel.CallbackType callbackType)
		{
			this.head.EmitCallback(ctx, valueFrom, callbackType);
		}

		// Token: 0x06007D17 RID: 32023 RVA: 0x0024C43C File Offset: 0x0024C43C
		void IProtoTypeSerializer.EmitCreateInstance(CompilerContext ctx)
		{
			this.head.EmitCreateInstance(ctx);
		}

		// Token: 0x04003C40 RID: 15424
		private readonly IProtoTypeSerializer head;

		// Token: 0x04003C41 RID: 15425
		private readonly ProtoSerializer serializer;

		// Token: 0x04003C42 RID: 15426
		private readonly ProtoDeserializer deserializer;
	}
}
