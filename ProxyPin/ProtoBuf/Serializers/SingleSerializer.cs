using System;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000C63 RID: 3171
	internal sealed class SingleSerializer : IProtoSerializer
	{
		// Token: 0x17001B57 RID: 6999
		// (get) Token: 0x06007DED RID: 32237 RVA: 0x00250EFC File Offset: 0x00250EFC
		public Type ExpectedType
		{
			get
			{
				return SingleSerializer.expectedType;
			}
		}

		// Token: 0x06007DEE RID: 32238 RVA: 0x00250F04 File Offset: 0x00250F04
		public SingleSerializer(TypeModel model)
		{
		}

		// Token: 0x17001B58 RID: 7000
		// (get) Token: 0x06007DEF RID: 32239 RVA: 0x00250F0C File Offset: 0x00250F0C
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B59 RID: 7001
		// (get) Token: 0x06007DF0 RID: 32240 RVA: 0x00250F10 File Offset: 0x00250F10
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007DF1 RID: 32241 RVA: 0x00250F14 File Offset: 0x00250F14
		public object Read(object value, ProtoReader source)
		{
			return source.ReadSingle();
		}

		// Token: 0x06007DF2 RID: 32242 RVA: 0x00250F24 File Offset: 0x00250F24
		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteSingle((float)value, dest);
		}

		// Token: 0x06007DF3 RID: 32243 RVA: 0x00250F34 File Offset: 0x00250F34
		void IProtoSerializer.EmitWrite(CompilerContext ctx, Local valueFrom)
		{
			ctx.EmitBasicWrite("WriteSingle", valueFrom);
		}

		// Token: 0x06007DF4 RID: 32244 RVA: 0x00250F44 File Offset: 0x00250F44
		void IProtoSerializer.EmitRead(CompilerContext ctx, Local valueFrom)
		{
			ctx.EmitBasicRead("ReadSingle", this.ExpectedType);
		}

		// Token: 0x04003C7B RID: 15483
		private static readonly Type expectedType = typeof(float);
	}
}
