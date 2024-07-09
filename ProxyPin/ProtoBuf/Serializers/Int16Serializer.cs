using System;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000C54 RID: 3156
	internal sealed class Int16Serializer : IProtoSerializer
	{
		// Token: 0x06007D62 RID: 32098 RVA: 0x0024E1DC File Offset: 0x0024E1DC
		public Int16Serializer(TypeModel model)
		{
		}

		// Token: 0x17001B27 RID: 6951
		// (get) Token: 0x06007D63 RID: 32099 RVA: 0x0024E1E4 File Offset: 0x0024E1E4
		public Type ExpectedType
		{
			get
			{
				return Int16Serializer.expectedType;
			}
		}

		// Token: 0x17001B28 RID: 6952
		// (get) Token: 0x06007D64 RID: 32100 RVA: 0x0024E1EC File Offset: 0x0024E1EC
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B29 RID: 6953
		// (get) Token: 0x06007D65 RID: 32101 RVA: 0x0024E1F0 File Offset: 0x0024E1F0
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007D66 RID: 32102 RVA: 0x0024E1F4 File Offset: 0x0024E1F4
		public object Read(object value, ProtoReader source)
		{
			return source.ReadInt16();
		}

		// Token: 0x06007D67 RID: 32103 RVA: 0x0024E204 File Offset: 0x0024E204
		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteInt16((short)value, dest);
		}

		// Token: 0x06007D68 RID: 32104 RVA: 0x0024E214 File Offset: 0x0024E214
		void IProtoSerializer.EmitWrite(CompilerContext ctx, Local valueFrom)
		{
			ctx.EmitBasicWrite("WriteInt16", valueFrom);
		}

		// Token: 0x06007D69 RID: 32105 RVA: 0x0024E224 File Offset: 0x0024E224
		void IProtoSerializer.EmitRead(CompilerContext ctx, Local valueFrom)
		{
			ctx.EmitBasicRead("ReadInt16", this.ExpectedType);
		}

		// Token: 0x04003C54 RID: 15444
		private static readonly Type expectedType = typeof(short);
	}
}
