using System;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000C56 RID: 3158
	internal sealed class Int64Serializer : IProtoSerializer
	{
		// Token: 0x06007D74 RID: 32116 RVA: 0x0024E2BC File Offset: 0x0024E2BC
		public Int64Serializer(TypeModel model)
		{
		}

		// Token: 0x17001B2D RID: 6957
		// (get) Token: 0x06007D75 RID: 32117 RVA: 0x0024E2C4 File Offset: 0x0024E2C4
		public Type ExpectedType
		{
			get
			{
				return Int64Serializer.expectedType;
			}
		}

		// Token: 0x17001B2E RID: 6958
		// (get) Token: 0x06007D76 RID: 32118 RVA: 0x0024E2CC File Offset: 0x0024E2CC
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B2F RID: 6959
		// (get) Token: 0x06007D77 RID: 32119 RVA: 0x0024E2D0 File Offset: 0x0024E2D0
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007D78 RID: 32120 RVA: 0x0024E2D4 File Offset: 0x0024E2D4
		public object Read(object value, ProtoReader source)
		{
			return source.ReadInt64();
		}

		// Token: 0x06007D79 RID: 32121 RVA: 0x0024E2E4 File Offset: 0x0024E2E4
		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteInt64((long)value, dest);
		}

		// Token: 0x06007D7A RID: 32122 RVA: 0x0024E2F4 File Offset: 0x0024E2F4
		void IProtoSerializer.EmitWrite(CompilerContext ctx, Local valueFrom)
		{
			ctx.EmitBasicWrite("WriteInt64", valueFrom);
		}

		// Token: 0x06007D7B RID: 32123 RVA: 0x0024E304 File Offset: 0x0024E304
		void IProtoSerializer.EmitRead(CompilerContext ctx, Local valueFrom)
		{
			ctx.EmitBasicRead("ReadInt64", this.ExpectedType);
		}

		// Token: 0x04003C56 RID: 15446
		private static readonly Type expectedType = typeof(long);
	}
}
