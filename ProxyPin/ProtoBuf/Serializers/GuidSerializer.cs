using System;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000C52 RID: 3154
	internal sealed class GuidSerializer : IProtoSerializer
	{
		// Token: 0x06007D52 RID: 32082 RVA: 0x0024D74C File Offset: 0x0024D74C
		public GuidSerializer(TypeModel model)
		{
		}

		// Token: 0x17001B23 RID: 6947
		// (get) Token: 0x06007D53 RID: 32083 RVA: 0x0024D754 File Offset: 0x0024D754
		public Type ExpectedType
		{
			get
			{
				return GuidSerializer.expectedType;
			}
		}

		// Token: 0x17001B24 RID: 6948
		// (get) Token: 0x06007D54 RID: 32084 RVA: 0x0024D75C File Offset: 0x0024D75C
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B25 RID: 6949
		// (get) Token: 0x06007D55 RID: 32085 RVA: 0x0024D760 File Offset: 0x0024D760
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007D56 RID: 32086 RVA: 0x0024D764 File Offset: 0x0024D764
		public void Write(object value, ProtoWriter dest)
		{
			BclHelpers.WriteGuid((Guid)value, dest);
		}

		// Token: 0x06007D57 RID: 32087 RVA: 0x0024D774 File Offset: 0x0024D774
		public object Read(object value, ProtoReader source)
		{
			return BclHelpers.ReadGuid(source);
		}

		// Token: 0x06007D58 RID: 32088 RVA: 0x0024D784 File Offset: 0x0024D784
		void IProtoSerializer.EmitWrite(CompilerContext ctx, Local valueFrom)
		{
			ctx.EmitWrite(ctx.MapType(typeof(BclHelpers)), "WriteGuid", valueFrom);
		}

		// Token: 0x06007D59 RID: 32089 RVA: 0x0024D7A4 File Offset: 0x0024D7A4
		void IProtoSerializer.EmitRead(CompilerContext ctx, Local valueFrom)
		{
			ctx.EmitBasicRead(ctx.MapType(typeof(BclHelpers)), "ReadGuid", this.ExpectedType);
		}

		// Token: 0x04003C4D RID: 15437
		private static readonly Type expectedType = typeof(Guid);
	}
}
