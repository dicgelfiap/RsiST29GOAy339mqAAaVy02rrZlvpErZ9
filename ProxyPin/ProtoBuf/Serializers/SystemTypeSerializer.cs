using System;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000C67 RID: 3175
	internal sealed class SystemTypeSerializer : IProtoSerializer
	{
		// Token: 0x06007E1E RID: 32286 RVA: 0x00251770 File Offset: 0x00251770
		public SystemTypeSerializer(TypeModel model)
		{
		}

		// Token: 0x17001B63 RID: 7011
		// (get) Token: 0x06007E1F RID: 32287 RVA: 0x00251778 File Offset: 0x00251778
		public Type ExpectedType
		{
			get
			{
				return SystemTypeSerializer.expectedType;
			}
		}

		// Token: 0x06007E20 RID: 32288 RVA: 0x00251780 File Offset: 0x00251780
		void IProtoSerializer.Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteType((Type)value, dest);
		}

		// Token: 0x06007E21 RID: 32289 RVA: 0x00251790 File Offset: 0x00251790
		object IProtoSerializer.Read(object value, ProtoReader source)
		{
			return source.ReadType();
		}

		// Token: 0x17001B64 RID: 7012
		// (get) Token: 0x06007E22 RID: 32290 RVA: 0x00251798 File Offset: 0x00251798
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B65 RID: 7013
		// (get) Token: 0x06007E23 RID: 32291 RVA: 0x0025179C File Offset: 0x0025179C
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007E24 RID: 32292 RVA: 0x002517A0 File Offset: 0x002517A0
		void IProtoSerializer.EmitWrite(CompilerContext ctx, Local valueFrom)
		{
			ctx.EmitBasicWrite("WriteType", valueFrom);
		}

		// Token: 0x06007E25 RID: 32293 RVA: 0x002517B0 File Offset: 0x002517B0
		void IProtoSerializer.EmitRead(CompilerContext ctx, Local valueFrom)
		{
			ctx.EmitBasicRead("ReadType", this.ExpectedType);
		}

		// Token: 0x04003C86 RID: 15494
		private static readonly Type expectedType = typeof(Type);
	}
}
