using System;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000C64 RID: 3172
	internal sealed class StringSerializer : IProtoSerializer
	{
		// Token: 0x06007DF6 RID: 32246 RVA: 0x00250F6C File Offset: 0x00250F6C
		public StringSerializer(TypeModel model)
		{
		}

		// Token: 0x17001B5A RID: 7002
		// (get) Token: 0x06007DF7 RID: 32247 RVA: 0x00250F74 File Offset: 0x00250F74
		public Type ExpectedType
		{
			get
			{
				return StringSerializer.expectedType;
			}
		}

		// Token: 0x06007DF8 RID: 32248 RVA: 0x00250F7C File Offset: 0x00250F7C
		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteString((string)value, dest);
		}

		// Token: 0x17001B5B RID: 7003
		// (get) Token: 0x06007DF9 RID: 32249 RVA: 0x00250F8C File Offset: 0x00250F8C
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B5C RID: 7004
		// (get) Token: 0x06007DFA RID: 32250 RVA: 0x00250F90 File Offset: 0x00250F90
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007DFB RID: 32251 RVA: 0x00250F94 File Offset: 0x00250F94
		public object Read(object value, ProtoReader source)
		{
			return source.ReadString();
		}

		// Token: 0x06007DFC RID: 32252 RVA: 0x00250F9C File Offset: 0x00250F9C
		void IProtoSerializer.EmitWrite(CompilerContext ctx, Local valueFrom)
		{
			ctx.EmitBasicWrite("WriteString", valueFrom);
		}

		// Token: 0x06007DFD RID: 32253 RVA: 0x00250FAC File Offset: 0x00250FAC
		void IProtoSerializer.EmitRead(CompilerContext ctx, Local valueFrom)
		{
			ctx.EmitBasicRead("ReadString", this.ExpectedType);
		}

		// Token: 0x04003C7C RID: 15484
		private static readonly Type expectedType = typeof(string);
	}
}
