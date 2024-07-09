using System;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000C6D RID: 3181
	internal sealed class UInt32Serializer : IProtoSerializer
	{
		// Token: 0x06007E6F RID: 32367 RVA: 0x00253A78 File Offset: 0x00253A78
		public UInt32Serializer(TypeModel model)
		{
		}

		// Token: 0x17001B77 RID: 7031
		// (get) Token: 0x06007E70 RID: 32368 RVA: 0x00253A80 File Offset: 0x00253A80
		public Type ExpectedType
		{
			get
			{
				return UInt32Serializer.expectedType;
			}
		}

		// Token: 0x17001B78 RID: 7032
		// (get) Token: 0x06007E71 RID: 32369 RVA: 0x00253A88 File Offset: 0x00253A88
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B79 RID: 7033
		// (get) Token: 0x06007E72 RID: 32370 RVA: 0x00253A8C File Offset: 0x00253A8C
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007E73 RID: 32371 RVA: 0x00253A90 File Offset: 0x00253A90
		public object Read(object value, ProtoReader source)
		{
			return source.ReadUInt32();
		}

		// Token: 0x06007E74 RID: 32372 RVA: 0x00253AA0 File Offset: 0x00253AA0
		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteUInt32((uint)value, dest);
		}

		// Token: 0x06007E75 RID: 32373 RVA: 0x00253AB0 File Offset: 0x00253AB0
		void IProtoSerializer.EmitWrite(CompilerContext ctx, Local valueFrom)
		{
			ctx.EmitBasicWrite("WriteUInt32", valueFrom);
		}

		// Token: 0x06007E76 RID: 32374 RVA: 0x00253AC0 File Offset: 0x00253AC0
		void IProtoSerializer.EmitRead(CompilerContext ctx, Local valueFrom)
		{
			ctx.EmitBasicRead("ReadUInt32", ctx.MapType(typeof(uint)));
		}

		// Token: 0x04003C9C RID: 15516
		private static readonly Type expectedType = typeof(uint);
	}
}
