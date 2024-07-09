using System;
using System.Reflection;
using ProtoBuf.Compiler;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000C5C RID: 3164
	internal sealed class MemberSpecifiedDecorator : ProtoDecoratorBase
	{
		// Token: 0x17001B42 RID: 6978
		// (get) Token: 0x06007DAD RID: 32173 RVA: 0x0024FEBC File Offset: 0x0024FEBC
		public override Type ExpectedType
		{
			get
			{
				return this.Tail.ExpectedType;
			}
		}

		// Token: 0x17001B43 RID: 6979
		// (get) Token: 0x06007DAE RID: 32174 RVA: 0x0024FECC File Offset: 0x0024FECC
		public override bool RequiresOldValue
		{
			get
			{
				return this.Tail.RequiresOldValue;
			}
		}

		// Token: 0x17001B44 RID: 6980
		// (get) Token: 0x06007DAF RID: 32175 RVA: 0x0024FEDC File Offset: 0x0024FEDC
		public override bool ReturnsValue
		{
			get
			{
				return this.Tail.ReturnsValue;
			}
		}

		// Token: 0x06007DB0 RID: 32176 RVA: 0x0024FEEC File Offset: 0x0024FEEC
		public MemberSpecifiedDecorator(MethodInfo getSpecified, MethodInfo setSpecified, IProtoSerializer tail) : base(tail)
		{
			if (getSpecified == null && setSpecified == null)
			{
				throw new InvalidOperationException();
			}
			this.getSpecified = getSpecified;
			this.setSpecified = setSpecified;
		}

		// Token: 0x06007DB1 RID: 32177 RVA: 0x0024FF24 File Offset: 0x0024FF24
		public override void Write(object value, ProtoWriter dest)
		{
			if (this.getSpecified == null || (bool)this.getSpecified.Invoke(value, null))
			{
				this.Tail.Write(value, dest);
			}
		}

		// Token: 0x06007DB2 RID: 32178 RVA: 0x0024FF5C File Offset: 0x0024FF5C
		public override object Read(object value, ProtoReader source)
		{
			object result = this.Tail.Read(value, source);
			if (this.setSpecified != null)
			{
				this.setSpecified.Invoke(value, new object[]
				{
					true
				});
			}
			return result;
		}

		// Token: 0x06007DB3 RID: 32179 RVA: 0x0024FFAC File Offset: 0x0024FFAC
		protected override void EmitWrite(CompilerContext ctx, Local valueFrom)
		{
			if (this.getSpecified == null)
			{
				this.Tail.EmitWrite(ctx, valueFrom);
				return;
			}
			using (Local localWithValue = ctx.GetLocalWithValue(this.ExpectedType, valueFrom))
			{
				ctx.LoadAddress(localWithValue, this.ExpectedType, false);
				ctx.EmitCall(this.getSpecified);
				CodeLabel label = ctx.DefineLabel();
				ctx.BranchIfFalse(label, false);
				this.Tail.EmitWrite(ctx, localWithValue);
				ctx.MarkLabel(label);
			}
		}

		// Token: 0x06007DB4 RID: 32180 RVA: 0x00250048 File Offset: 0x00250048
		protected override void EmitRead(CompilerContext ctx, Local valueFrom)
		{
			if (this.setSpecified == null)
			{
				this.Tail.EmitRead(ctx, valueFrom);
				return;
			}
			using (Local localWithValue = ctx.GetLocalWithValue(this.ExpectedType, valueFrom))
			{
				this.Tail.EmitRead(ctx, localWithValue);
				ctx.LoadAddress(localWithValue, this.ExpectedType, false);
				ctx.LoadValue(1);
				ctx.EmitCall(this.setSpecified);
			}
		}

		// Token: 0x04003C6D RID: 15469
		private readonly MethodInfo getSpecified;

		// Token: 0x04003C6E RID: 15470
		private readonly MethodInfo setSpecified;
	}
}
