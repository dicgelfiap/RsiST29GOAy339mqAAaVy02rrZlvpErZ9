using System;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000C49 RID: 3145
	internal sealed class ByteSerializer : IProtoSerializer
	{
		// Token: 0x17001B0A RID: 6922
		// (get) Token: 0x06007CFB RID: 31995 RVA: 0x0024C270 File Offset: 0x0024C270
		public Type ExpectedType
		{
			get
			{
				return ByteSerializer.expectedType;
			}
		}

		// Token: 0x06007CFC RID: 31996 RVA: 0x0024C278 File Offset: 0x0024C278
		public ByteSerializer(TypeModel model)
		{
		}

		// Token: 0x17001B0B RID: 6923
		// (get) Token: 0x06007CFD RID: 31997 RVA: 0x0024C280 File Offset: 0x0024C280
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B0C RID: 6924
		// (get) Token: 0x06007CFE RID: 31998 RVA: 0x0024C284 File Offset: 0x0024C284
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007CFF RID: 31999 RVA: 0x0024C288 File Offset: 0x0024C288
		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteByte((byte)value, dest);
		}

		// Token: 0x06007D00 RID: 32000 RVA: 0x0024C298 File Offset: 0x0024C298
		public object Read(object value, ProtoReader source)
		{
			return source.ReadByte();
		}

		// Token: 0x06007D01 RID: 32001 RVA: 0x0024C2A8 File Offset: 0x0024C2A8
		void IProtoSerializer.EmitWrite(CompilerContext ctx, Local valueFrom)
		{
			ctx.EmitBasicWrite("WriteByte", valueFrom);
		}

		// Token: 0x06007D02 RID: 32002 RVA: 0x0024C2B8 File Offset: 0x0024C2B8
		void IProtoSerializer.EmitRead(CompilerContext ctx, Local valueFrom)
		{
			ctx.EmitBasicRead("ReadByte", this.ExpectedType);
		}

		// Token: 0x04003C3E RID: 15422
		private static readonly Type expectedType = typeof(byte);
	}
}
