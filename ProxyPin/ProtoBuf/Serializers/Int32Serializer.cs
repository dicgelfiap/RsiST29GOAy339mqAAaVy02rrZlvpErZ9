using System;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000C55 RID: 3157
	internal sealed class Int32Serializer : IProtoSerializer
	{
		// Token: 0x06007D6B RID: 32107 RVA: 0x0024E24C File Offset: 0x0024E24C
		public Int32Serializer(TypeModel model)
		{
		}

		// Token: 0x17001B2A RID: 6954
		// (get) Token: 0x06007D6C RID: 32108 RVA: 0x0024E254 File Offset: 0x0024E254
		public Type ExpectedType
		{
			get
			{
				return Int32Serializer.expectedType;
			}
		}

		// Token: 0x17001B2B RID: 6955
		// (get) Token: 0x06007D6D RID: 32109 RVA: 0x0024E25C File Offset: 0x0024E25C
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B2C RID: 6956
		// (get) Token: 0x06007D6E RID: 32110 RVA: 0x0024E260 File Offset: 0x0024E260
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007D6F RID: 32111 RVA: 0x0024E264 File Offset: 0x0024E264
		public object Read(object value, ProtoReader source)
		{
			return source.ReadInt32();
		}

		// Token: 0x06007D70 RID: 32112 RVA: 0x0024E274 File Offset: 0x0024E274
		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteInt32((int)value, dest);
		}

		// Token: 0x06007D71 RID: 32113 RVA: 0x0024E284 File Offset: 0x0024E284
		void IProtoSerializer.EmitWrite(CompilerContext ctx, Local valueFrom)
		{
			ctx.EmitBasicWrite("WriteInt32", valueFrom);
		}

		// Token: 0x06007D72 RID: 32114 RVA: 0x0024E294 File Offset: 0x0024E294
		void IProtoSerializer.EmitRead(CompilerContext ctx, Local valueFrom)
		{
			ctx.EmitBasicRead("ReadInt32", this.ExpectedType);
		}

		// Token: 0x04003C55 RID: 15445
		private static readonly Type expectedType = typeof(int);
	}
}
