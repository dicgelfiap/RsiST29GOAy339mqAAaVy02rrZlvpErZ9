using System;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000C4F RID: 3151
	internal sealed class DoubleSerializer : IProtoSerializer
	{
		// Token: 0x06007D34 RID: 32052 RVA: 0x0024CBE4 File Offset: 0x0024CBE4
		public DoubleSerializer(TypeModel model)
		{
		}

		// Token: 0x17001B1A RID: 6938
		// (get) Token: 0x06007D35 RID: 32053 RVA: 0x0024CBEC File Offset: 0x0024CBEC
		public Type ExpectedType
		{
			get
			{
				return DoubleSerializer.expectedType;
			}
		}

		// Token: 0x17001B1B RID: 6939
		// (get) Token: 0x06007D36 RID: 32054 RVA: 0x0024CBF4 File Offset: 0x0024CBF4
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B1C RID: 6940
		// (get) Token: 0x06007D37 RID: 32055 RVA: 0x0024CBF8 File Offset: 0x0024CBF8
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007D38 RID: 32056 RVA: 0x0024CBFC File Offset: 0x0024CBFC
		public object Read(object value, ProtoReader source)
		{
			return source.ReadDouble();
		}

		// Token: 0x06007D39 RID: 32057 RVA: 0x0024CC0C File Offset: 0x0024CC0C
		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteDouble((double)value, dest);
		}

		// Token: 0x06007D3A RID: 32058 RVA: 0x0024CC1C File Offset: 0x0024CC1C
		void IProtoSerializer.EmitWrite(CompilerContext ctx, Local valueFrom)
		{
			ctx.EmitBasicWrite("WriteDouble", valueFrom);
		}

		// Token: 0x06007D3B RID: 32059 RVA: 0x0024CC2C File Offset: 0x0024CC2C
		void IProtoSerializer.EmitRead(CompilerContext ctx, Local valueFrom)
		{
			ctx.EmitBasicRead("ReadDouble", this.ExpectedType);
		}

		// Token: 0x04003C48 RID: 15432
		private static readonly Type expectedType = typeof(double);
	}
}
