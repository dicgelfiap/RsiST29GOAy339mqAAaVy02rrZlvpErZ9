using System;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000C5D RID: 3165
	internal sealed class NetObjectSerializer : IProtoSerializer
	{
		// Token: 0x06007DB5 RID: 32181 RVA: 0x002500D4 File Offset: 0x002500D4
		public NetObjectSerializer(TypeModel model, Type type, int key, BclHelpers.NetObjectOptions options)
		{
			bool flag = (options & BclHelpers.NetObjectOptions.DynamicType) > BclHelpers.NetObjectOptions.None;
			this.key = (flag ? -1 : key);
			this.type = (flag ? model.MapType(typeof(object)) : type);
			this.options = options;
		}

		// Token: 0x17001B45 RID: 6981
		// (get) Token: 0x06007DB6 RID: 32182 RVA: 0x00250130 File Offset: 0x00250130
		public Type ExpectedType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17001B46 RID: 6982
		// (get) Token: 0x06007DB7 RID: 32183 RVA: 0x00250138 File Offset: 0x00250138
		public bool ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001B47 RID: 6983
		// (get) Token: 0x06007DB8 RID: 32184 RVA: 0x0025013C File Offset: 0x0025013C
		public bool RequiresOldValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007DB9 RID: 32185 RVA: 0x00250140 File Offset: 0x00250140
		public object Read(object value, ProtoReader source)
		{
			return BclHelpers.ReadNetObject(value, source, this.key, (this.type == typeof(object)) ? null : this.type, this.options);
		}

		// Token: 0x06007DBA RID: 32186 RVA: 0x0025018C File Offset: 0x0025018C
		public void Write(object value, ProtoWriter dest)
		{
			BclHelpers.WriteNetObject(value, dest, this.key, this.options);
		}

		// Token: 0x06007DBB RID: 32187 RVA: 0x002501A4 File Offset: 0x002501A4
		public void EmitRead(CompilerContext ctx, Local valueFrom)
		{
			ctx.LoadValue(valueFrom);
			ctx.CastToObject(this.type);
			ctx.LoadReaderWriter();
			ctx.LoadValue(ctx.MapMetaKeyToCompiledKey(this.key));
			if (this.type == ctx.MapType(typeof(object)))
			{
				ctx.LoadNullRef();
			}
			else
			{
				ctx.LoadValue(this.type);
			}
			ctx.LoadValue((int)this.options);
			ctx.EmitCall(ctx.MapType(typeof(BclHelpers)).GetMethod("ReadNetObject"));
			ctx.CastFromObject(this.type);
		}

		// Token: 0x06007DBC RID: 32188 RVA: 0x00250250 File Offset: 0x00250250
		public void EmitWrite(CompilerContext ctx, Local valueFrom)
		{
			ctx.LoadValue(valueFrom);
			ctx.CastToObject(this.type);
			ctx.LoadReaderWriter();
			ctx.LoadValue(ctx.MapMetaKeyToCompiledKey(this.key));
			ctx.LoadValue((int)this.options);
			ctx.EmitCall(ctx.MapType(typeof(BclHelpers)).GetMethod("WriteNetObject"));
		}

		// Token: 0x04003C6F RID: 15471
		private readonly int key;

		// Token: 0x04003C70 RID: 15472
		private readonly Type type;

		// Token: 0x04003C71 RID: 15473
		private readonly BclHelpers.NetObjectOptions options;
	}
}
