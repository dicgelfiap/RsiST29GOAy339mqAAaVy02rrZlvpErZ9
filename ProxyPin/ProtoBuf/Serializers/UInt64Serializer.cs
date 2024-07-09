using System;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000C6E RID: 3182
	internal sealed class UInt64Serializer : IProtoSerializer
	{
		// Token: 0x06007E78 RID: 32376 RVA: 0x00253AF4 File Offset: 0x00253AF4
		public UInt64Serializer(TypeModel model)
		{
		}

		// Token: 0x17001B7A RID: 7034
		// (get) Token: 0x06007E79 RID: 32377 RVA: 0x00253AFC File Offset: 0x00253AFC
		public Type ExpectedType
		{
			get
			{
				return UInt64Serializer.expectedType;
			}
		}

		// Token: 0x17001B7B RID: 7035
		// (get) Token: 0x06007E7A RID: 32378 RVA: 0x00253B04 File Offset: 0x00253B04
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B7C RID: 7036
		// (get) Token: 0x06007E7B RID: 32379 RVA: 0x00253B08 File Offset: 0x00253B08
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007E7C RID: 32380 RVA: 0x00253B0C File Offset: 0x00253B0C
		public object Read(object value, ProtoReader source)
		{
			return source.ReadUInt64();
		}

		// Token: 0x06007E7D RID: 32381 RVA: 0x00253B1C File Offset: 0x00253B1C
		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteUInt64((ulong)value, dest);
		}

		// Token: 0x06007E7E RID: 32382 RVA: 0x00253B2C File Offset: 0x00253B2C
		void IProtoSerializer.EmitWrite(CompilerContext ctx, Local valueFrom)
		{
			ctx.EmitBasicWrite("WriteUInt64", valueFrom);
		}

		// Token: 0x06007E7F RID: 32383 RVA: 0x00253B3C File Offset: 0x00253B3C
		void IProtoSerializer.EmitRead(CompilerContext ctx, Local valueFrom)
		{
			ctx.EmitBasicRead("ReadUInt64", this.ExpectedType);
		}

		// Token: 0x04003C9D RID: 15517
		private static readonly Type expectedType = typeof(ulong);
	}
}
