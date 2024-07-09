using System;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000C4D RID: 3149
	internal sealed class DecimalSerializer : IProtoSerializer
	{
		// Token: 0x06007D21 RID: 32033 RVA: 0x0024C5C4 File Offset: 0x0024C5C4
		public DecimalSerializer(TypeModel model)
		{
		}

		// Token: 0x17001B14 RID: 6932
		// (get) Token: 0x06007D22 RID: 32034 RVA: 0x0024C5CC File Offset: 0x0024C5CC
		public Type ExpectedType
		{
			get
			{
				return DecimalSerializer.expectedType;
			}
		}

		// Token: 0x17001B15 RID: 6933
		// (get) Token: 0x06007D23 RID: 32035 RVA: 0x0024C5D4 File Offset: 0x0024C5D4
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B16 RID: 6934
		// (get) Token: 0x06007D24 RID: 32036 RVA: 0x0024C5D8 File Offset: 0x0024C5D8
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007D25 RID: 32037 RVA: 0x0024C5DC File Offset: 0x0024C5DC
		public object Read(object value, ProtoReader source)
		{
			return BclHelpers.ReadDecimal(source);
		}

		// Token: 0x06007D26 RID: 32038 RVA: 0x0024C5EC File Offset: 0x0024C5EC
		public void Write(object value, ProtoWriter dest)
		{
			BclHelpers.WriteDecimal((decimal)value, dest);
		}

		// Token: 0x06007D27 RID: 32039 RVA: 0x0024C5FC File Offset: 0x0024C5FC
		void IProtoSerializer.EmitWrite(CompilerContext ctx, Local valueFrom)
		{
			ctx.EmitWrite(ctx.MapType(typeof(BclHelpers)), "WriteDecimal", valueFrom);
		}

		// Token: 0x06007D28 RID: 32040 RVA: 0x0024C61C File Offset: 0x0024C61C
		void IProtoSerializer.EmitRead(CompilerContext ctx, Local valueFrom)
		{
			ctx.EmitBasicRead(ctx.MapType(typeof(BclHelpers)), "ReadDecimal", this.ExpectedType);
		}

		// Token: 0x04003C46 RID: 15430
		private static readonly Type expectedType = typeof(decimal);
	}
}
