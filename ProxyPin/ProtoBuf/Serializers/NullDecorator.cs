using System;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000C5E RID: 3166
	internal sealed class NullDecorator : ProtoDecoratorBase
	{
		// Token: 0x06007DBD RID: 32189 RVA: 0x002502B8 File Offset: 0x002502B8
		public NullDecorator(TypeModel model, IProtoSerializer tail) : base(tail)
		{
			if (!tail.ReturnsValue)
			{
				throw new NotSupportedException("NullDecorator only supports implementations that return values");
			}
			Type type = tail.ExpectedType;
			if (Helpers.IsValueType(type))
			{
				this.expectedType = model.MapType(typeof(Nullable<>)).MakeGenericType(new Type[]
				{
					type
				});
				return;
			}
			this.expectedType = type;
		}

		// Token: 0x17001B48 RID: 6984
		// (get) Token: 0x06007DBE RID: 32190 RVA: 0x00250328 File Offset: 0x00250328
		public override Type ExpectedType
		{
			get
			{
				return this.expectedType;
			}
		}

		// Token: 0x17001B49 RID: 6985
		// (get) Token: 0x06007DBF RID: 32191 RVA: 0x00250330 File Offset: 0x00250330
		public override bool ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001B4A RID: 6986
		// (get) Token: 0x06007DC0 RID: 32192 RVA: 0x00250334 File Offset: 0x00250334
		public override bool RequiresOldValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007DC1 RID: 32193 RVA: 0x00250338 File Offset: 0x00250338
		protected override void EmitRead(CompilerContext ctx, Local valueFrom)
		{
			using (Local localWithValue = ctx.GetLocalWithValue(this.expectedType, valueFrom))
			{
				using (Local local = new Local(ctx, ctx.MapType(typeof(SubItemToken))))
				{
					using (Local local2 = new Local(ctx, ctx.MapType(typeof(int))))
					{
						ctx.LoadReaderWriter();
						ctx.EmitCall(ctx.MapType(typeof(ProtoReader)).GetMethod("StartSubItem"));
						ctx.StoreValue(local);
						CodeLabel label = ctx.DefineLabel();
						CodeLabel label2 = ctx.DefineLabel();
						CodeLabel label3 = ctx.DefineLabel();
						ctx.MarkLabel(label);
						ctx.EmitBasicRead("ReadFieldHeader", ctx.MapType(typeof(int)));
						ctx.CopyValue();
						ctx.StoreValue(local2);
						ctx.LoadValue(1);
						ctx.BranchIfEqual(label2, true);
						ctx.LoadValue(local2);
						ctx.LoadValue(1);
						ctx.BranchIfLess(label3, false);
						ctx.LoadReaderWriter();
						ctx.EmitCall(ctx.MapType(typeof(ProtoReader)).GetMethod("SkipField"));
						ctx.Branch(label, true);
						ctx.MarkLabel(label2);
						if (this.Tail.RequiresOldValue)
						{
							if (Helpers.IsValueType(this.expectedType))
							{
								ctx.LoadAddress(localWithValue, this.expectedType, false);
								ctx.EmitCall(this.expectedType.GetMethod("GetValueOrDefault", Helpers.EmptyTypes));
							}
							else
							{
								ctx.LoadValue(localWithValue);
							}
						}
						this.Tail.EmitRead(ctx, null);
						if (Helpers.IsValueType(this.expectedType))
						{
							ctx.EmitCtor(this.expectedType, new Type[]
							{
								this.Tail.ExpectedType
							});
						}
						ctx.StoreValue(localWithValue);
						ctx.Branch(label, false);
						ctx.MarkLabel(label3);
						ctx.LoadValue(local);
						ctx.LoadReaderWriter();
						ctx.EmitCall(ctx.MapType(typeof(ProtoReader)).GetMethod("EndSubItem"));
						ctx.LoadValue(localWithValue);
					}
				}
			}
		}

		// Token: 0x06007DC2 RID: 32194 RVA: 0x002505A8 File Offset: 0x002505A8
		protected override void EmitWrite(CompilerContext ctx, Local valueFrom)
		{
			using (Local localWithValue = ctx.GetLocalWithValue(this.expectedType, valueFrom))
			{
				using (Local local = new Local(ctx, ctx.MapType(typeof(SubItemToken))))
				{
					ctx.LoadNullRef();
					ctx.LoadReaderWriter();
					ctx.EmitCall(ctx.MapType(typeof(ProtoWriter)).GetMethod("StartSubItem"));
					ctx.StoreValue(local);
					if (Helpers.IsValueType(this.expectedType))
					{
						ctx.LoadAddress(localWithValue, this.expectedType, false);
						ctx.LoadValue(this.expectedType.GetProperty("HasValue"));
					}
					else
					{
						ctx.LoadValue(localWithValue);
					}
					CodeLabel label = ctx.DefineLabel();
					ctx.BranchIfFalse(label, false);
					if (Helpers.IsValueType(this.expectedType))
					{
						ctx.LoadAddress(localWithValue, this.expectedType, false);
						ctx.EmitCall(this.expectedType.GetMethod("GetValueOrDefault", Helpers.EmptyTypes));
					}
					else
					{
						ctx.LoadValue(localWithValue);
					}
					this.Tail.EmitWrite(ctx, null);
					ctx.MarkLabel(label);
					ctx.LoadValue(local);
					ctx.LoadReaderWriter();
					ctx.EmitCall(ctx.MapType(typeof(ProtoWriter)).GetMethod("EndSubItem"));
				}
			}
		}

		// Token: 0x06007DC3 RID: 32195 RVA: 0x00250738 File Offset: 0x00250738
		public override object Read(object value, ProtoReader source)
		{
			SubItemToken token = ProtoReader.StartSubItem(source);
			int num;
			while ((num = source.ReadFieldHeader()) > 0)
			{
				if (num == 1)
				{
					value = this.Tail.Read(value, source);
				}
				else
				{
					source.SkipField();
				}
			}
			ProtoReader.EndSubItem(token, source);
			return value;
		}

		// Token: 0x06007DC4 RID: 32196 RVA: 0x0025078C File Offset: 0x0025078C
		public override void Write(object value, ProtoWriter dest)
		{
			SubItemToken token = ProtoWriter.StartSubItem(null, dest);
			if (value != null)
			{
				this.Tail.Write(value, dest);
			}
			ProtoWriter.EndSubItem(token, dest);
		}

		// Token: 0x04003C72 RID: 15474
		private readonly Type expectedType;

		// Token: 0x04003C73 RID: 15475
		public const int Tag = 1;
	}
}
