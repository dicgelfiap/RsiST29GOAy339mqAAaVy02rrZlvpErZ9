using System;
using System.Reflection;
using ProtoBuf.Compiler;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000C51 RID: 3153
	internal sealed class FieldDecorator : ProtoDecoratorBase
	{
		// Token: 0x17001B20 RID: 6944
		// (get) Token: 0x06007D4A RID: 32074 RVA: 0x0024D4D8 File Offset: 0x0024D4D8
		public override Type ExpectedType
		{
			get
			{
				return this.forType;
			}
		}

		// Token: 0x17001B21 RID: 6945
		// (get) Token: 0x06007D4B RID: 32075 RVA: 0x0024D4E0 File Offset: 0x0024D4E0
		public override bool RequiresOldValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001B22 RID: 6946
		// (get) Token: 0x06007D4C RID: 32076 RVA: 0x0024D4E4 File Offset: 0x0024D4E4
		public override bool ReturnsValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007D4D RID: 32077 RVA: 0x0024D4E8 File Offset: 0x0024D4E8
		public FieldDecorator(Type forType, FieldInfo field, IProtoSerializer tail) : base(tail)
		{
			this.forType = forType;
			this.field = field;
		}

		// Token: 0x06007D4E RID: 32078 RVA: 0x0024D500 File Offset: 0x0024D500
		public override void Write(object value, ProtoWriter dest)
		{
			value = this.field.GetValue(value);
			if (value != null)
			{
				this.Tail.Write(value, dest);
			}
		}

		// Token: 0x06007D4F RID: 32079 RVA: 0x0024D524 File Offset: 0x0024D524
		public override object Read(object value, ProtoReader source)
		{
			object obj = this.Tail.Read(this.Tail.RequiresOldValue ? this.field.GetValue(value) : null, source);
			if (obj != null)
			{
				this.field.SetValue(value, obj);
			}
			return null;
		}

		// Token: 0x06007D50 RID: 32080 RVA: 0x0024D578 File Offset: 0x0024D578
		protected override void EmitWrite(CompilerContext ctx, Local valueFrom)
		{
			ctx.LoadAddress(valueFrom, this.ExpectedType, false);
			ctx.LoadValue(this.field);
			ctx.WriteNullCheckedTail(this.field.FieldType, this.Tail, null);
		}

		// Token: 0x06007D51 RID: 32081 RVA: 0x0024D5BC File Offset: 0x0024D5BC
		protected override void EmitRead(CompilerContext ctx, Local valueFrom)
		{
			using (Local localWithValue = ctx.GetLocalWithValue(this.ExpectedType, valueFrom))
			{
				if (this.Tail.RequiresOldValue)
				{
					ctx.LoadAddress(localWithValue, this.ExpectedType, false);
					ctx.LoadValue(this.field);
				}
				ctx.ReadNullCheckedTail(this.field.FieldType, this.Tail, null);
				MemberInfo memberInfo = this.field;
				ctx.CheckAccessibility(ref memberInfo);
				bool flag = memberInfo is FieldInfo;
				if (flag)
				{
					if (!this.Tail.ReturnsValue)
					{
						goto IL_13C;
					}
					using (Local local = new Local(ctx, this.field.FieldType))
					{
						ctx.StoreValue(local);
						if (Helpers.IsValueType(this.field.FieldType))
						{
							ctx.LoadAddress(localWithValue, this.ExpectedType, false);
							ctx.LoadValue(local);
							ctx.StoreValue(this.field);
							return;
						}
						CodeLabel label = ctx.DefineLabel();
						ctx.LoadValue(local);
						ctx.BranchIfFalse(label, true);
						ctx.LoadAddress(localWithValue, this.ExpectedType, false);
						ctx.LoadValue(local);
						ctx.StoreValue(this.field);
						ctx.MarkLabel(label);
						return;
					}
				}
				if (this.Tail.ReturnsValue)
				{
					ctx.DiscardValue();
				}
				IL_13C:;
			}
		}

		// Token: 0x04003C4B RID: 15435
		private readonly FieldInfo field;

		// Token: 0x04003C4C RID: 15436
		private readonly Type forType;
	}
}
