using System;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000C62 RID: 3170
	internal sealed class SByteSerializer : IProtoSerializer
	{
		// Token: 0x06007DE4 RID: 32228 RVA: 0x00250E8C File Offset: 0x00250E8C
		public SByteSerializer(TypeModel model)
		{
		}

		// Token: 0x17001B54 RID: 6996
		// (get) Token: 0x06007DE5 RID: 32229 RVA: 0x00250E94 File Offset: 0x00250E94
		public Type ExpectedType
		{
			get
			{
				return SByteSerializer.expectedType;
			}
		}

		// Token: 0x17001B55 RID: 6997
		// (get) Token: 0x06007DE6 RID: 32230 RVA: 0x00250E9C File Offset: 0x00250E9C
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B56 RID: 6998
		// (get) Token: 0x06007DE7 RID: 32231 RVA: 0x00250EA0 File Offset: 0x00250EA0
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007DE8 RID: 32232 RVA: 0x00250EA4 File Offset: 0x00250EA4
		public object Read(object value, ProtoReader source)
		{
			return source.ReadSByte();
		}

		// Token: 0x06007DE9 RID: 32233 RVA: 0x00250EB4 File Offset: 0x00250EB4
		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteSByte((sbyte)value, dest);
		}

		// Token: 0x06007DEA RID: 32234 RVA: 0x00250EC4 File Offset: 0x00250EC4
		void IProtoSerializer.EmitWrite(CompilerContext ctx, Local valueFrom)
		{
			ctx.EmitBasicWrite("WriteSByte", valueFrom);
		}

		// Token: 0x06007DEB RID: 32235 RVA: 0x00250ED4 File Offset: 0x00250ED4
		void IProtoSerializer.EmitRead(CompilerContext ctx, Local valueFrom)
		{
			ctx.EmitBasicRead("ReadSByte", this.ExpectedType);
		}

		// Token: 0x04003C7A RID: 15482
		private static readonly Type expectedType = typeof(sbyte);
	}
}
