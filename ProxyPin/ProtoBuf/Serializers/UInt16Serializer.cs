using System;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000C6C RID: 3180
	internal class UInt16Serializer : IProtoSerializer
	{
		// Token: 0x06007E66 RID: 32358 RVA: 0x002539FC File Offset: 0x002539FC
		public UInt16Serializer(TypeModel model)
		{
		}

		// Token: 0x17001B74 RID: 7028
		// (get) Token: 0x06007E67 RID: 32359 RVA: 0x00253A04 File Offset: 0x00253A04
		public virtual Type ExpectedType
		{
			get
			{
				return UInt16Serializer.expectedType;
			}
		}

		// Token: 0x17001B75 RID: 7029
		// (get) Token: 0x06007E68 RID: 32360 RVA: 0x00253A0C File Offset: 0x00253A0C
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B76 RID: 7030
		// (get) Token: 0x06007E69 RID: 32361 RVA: 0x00253A10 File Offset: 0x00253A10
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007E6A RID: 32362 RVA: 0x00253A14 File Offset: 0x00253A14
		public virtual object Read(object value, ProtoReader source)
		{
			return source.ReadUInt16();
		}

		// Token: 0x06007E6B RID: 32363 RVA: 0x00253A24 File Offset: 0x00253A24
		public virtual void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteUInt16((ushort)value, dest);
		}

		// Token: 0x06007E6C RID: 32364 RVA: 0x00253A34 File Offset: 0x00253A34
		void IProtoSerializer.EmitWrite(CompilerContext ctx, Local valueFrom)
		{
			ctx.EmitBasicWrite("WriteUInt16", valueFrom);
		}

		// Token: 0x06007E6D RID: 32365 RVA: 0x00253A44 File Offset: 0x00253A44
		void IProtoSerializer.EmitRead(CompilerContext ctx, Local valueFrom)
		{
			ctx.EmitBasicRead("ReadUInt16", ctx.MapType(typeof(ushort)));
		}

		// Token: 0x04003C9B RID: 15515
		private static readonly Type expectedType = typeof(ushort);
	}
}
