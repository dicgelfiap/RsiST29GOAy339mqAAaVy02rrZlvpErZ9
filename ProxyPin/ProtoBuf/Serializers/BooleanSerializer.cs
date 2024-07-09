using System;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000C48 RID: 3144
	internal sealed class BooleanSerializer : IProtoSerializer
	{
		// Token: 0x06007CF2 RID: 31986 RVA: 0x0024C200 File Offset: 0x0024C200
		public BooleanSerializer(TypeModel model)
		{
		}

		// Token: 0x17001B07 RID: 6919
		// (get) Token: 0x06007CF3 RID: 31987 RVA: 0x0024C208 File Offset: 0x0024C208
		public Type ExpectedType
		{
			get
			{
				return BooleanSerializer.expectedType;
			}
		}

		// Token: 0x06007CF4 RID: 31988 RVA: 0x0024C210 File Offset: 0x0024C210
		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteBoolean((bool)value, dest);
		}

		// Token: 0x06007CF5 RID: 31989 RVA: 0x0024C220 File Offset: 0x0024C220
		public object Read(object value, ProtoReader source)
		{
			return source.ReadBoolean();
		}

		// Token: 0x17001B08 RID: 6920
		// (get) Token: 0x06007CF6 RID: 31990 RVA: 0x0024C230 File Offset: 0x0024C230
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B09 RID: 6921
		// (get) Token: 0x06007CF7 RID: 31991 RVA: 0x0024C234 File Offset: 0x0024C234
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007CF8 RID: 31992 RVA: 0x0024C238 File Offset: 0x0024C238
		void IProtoSerializer.EmitWrite(CompilerContext ctx, Local valueFrom)
		{
			ctx.EmitBasicWrite("WriteBoolean", valueFrom);
		}

		// Token: 0x06007CF9 RID: 31993 RVA: 0x0024C248 File Offset: 0x0024C248
		void IProtoSerializer.EmitRead(CompilerContext ctx, Local valueFrom)
		{
			ctx.EmitBasicRead("ReadBoolean", this.ExpectedType);
		}

		// Token: 0x04003C3D RID: 15421
		private static readonly Type expectedType = typeof(bool);
	}
}
