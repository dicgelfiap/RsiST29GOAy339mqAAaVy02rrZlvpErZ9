using System;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000C47 RID: 3143
	internal sealed class BlobSerializer : IProtoSerializer
	{
		// Token: 0x17001B04 RID: 6916
		// (get) Token: 0x06007CE9 RID: 31977 RVA: 0x0024C130 File Offset: 0x0024C130
		public Type ExpectedType
		{
			get
			{
				return BlobSerializer.expectedType;
			}
		}

		// Token: 0x06007CEA RID: 31978 RVA: 0x0024C138 File Offset: 0x0024C138
		public BlobSerializer(TypeModel model, bool overwriteList)
		{
			this.overwriteList = overwriteList;
		}

		// Token: 0x06007CEB RID: 31979 RVA: 0x0024C148 File Offset: 0x0024C148
		public object Read(object value, ProtoReader source)
		{
			return ProtoReader.AppendBytes(this.overwriteList ? null : ((byte[])value), source);
		}

		// Token: 0x06007CEC RID: 31980 RVA: 0x0024C168 File Offset: 0x0024C168
		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteBytes((byte[])value, dest);
		}

		// Token: 0x17001B05 RID: 6917
		// (get) Token: 0x06007CED RID: 31981 RVA: 0x0024C178 File Offset: 0x0024C178
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return !this.overwriteList;
			}
		}

		// Token: 0x17001B06 RID: 6918
		// (get) Token: 0x06007CEE RID: 31982 RVA: 0x0024C184 File Offset: 0x0024C184
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007CEF RID: 31983 RVA: 0x0024C188 File Offset: 0x0024C188
		void IProtoSerializer.EmitWrite(CompilerContext ctx, Local valueFrom)
		{
			ctx.EmitBasicWrite("WriteBytes", valueFrom);
		}

		// Token: 0x06007CF0 RID: 31984 RVA: 0x0024C198 File Offset: 0x0024C198
		void IProtoSerializer.EmitRead(CompilerContext ctx, Local valueFrom)
		{
			if (this.overwriteList)
			{
				ctx.LoadNullRef();
			}
			else
			{
				ctx.LoadValue(valueFrom);
			}
			ctx.LoadReaderWriter();
			ctx.EmitCall(ctx.MapType(typeof(ProtoReader)).GetMethod("AppendBytes"));
		}

		// Token: 0x04003C3B RID: 15419
		private static readonly Type expectedType = typeof(byte[]);

		// Token: 0x04003C3C RID: 15420
		private readonly bool overwriteList;
	}
}
