using System;
using ProtoBuf.Compiler;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000C61 RID: 3169
	internal abstract class ProtoDecoratorBase : IProtoSerializer
	{
		// Token: 0x17001B51 RID: 6993
		// (get) Token: 0x06007DDA RID: 32218
		public abstract Type ExpectedType { get; }

		// Token: 0x06007DDB RID: 32219 RVA: 0x00250E64 File Offset: 0x00250E64
		protected ProtoDecoratorBase(IProtoSerializer tail)
		{
			this.Tail = tail;
		}

		// Token: 0x17001B52 RID: 6994
		// (get) Token: 0x06007DDC RID: 32220
		public abstract bool ReturnsValue { get; }

		// Token: 0x17001B53 RID: 6995
		// (get) Token: 0x06007DDD RID: 32221
		public abstract bool RequiresOldValue { get; }

		// Token: 0x06007DDE RID: 32222
		public abstract void Write(object value, ProtoWriter dest);

		// Token: 0x06007DDF RID: 32223
		public abstract object Read(object value, ProtoReader source);

		// Token: 0x06007DE0 RID: 32224 RVA: 0x00250E74 File Offset: 0x00250E74
		void IProtoSerializer.EmitWrite(CompilerContext ctx, Local valueFrom)
		{
			this.EmitWrite(ctx, valueFrom);
		}

		// Token: 0x06007DE1 RID: 32225
		protected abstract void EmitWrite(CompilerContext ctx, Local valueFrom);

		// Token: 0x06007DE2 RID: 32226 RVA: 0x00250E80 File Offset: 0x00250E80
		void IProtoSerializer.EmitRead(CompilerContext ctx, Local valueFrom)
		{
			this.EmitRead(ctx, valueFrom);
		}

		// Token: 0x06007DE3 RID: 32227
		protected abstract void EmitRead(CompilerContext ctx, Local valueFrom);

		// Token: 0x04003C79 RID: 15481
		protected readonly IProtoSerializer Tail;
	}
}
