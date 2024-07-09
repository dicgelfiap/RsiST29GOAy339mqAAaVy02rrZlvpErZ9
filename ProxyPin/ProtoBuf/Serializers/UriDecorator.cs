using System;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000C6F RID: 3183
	internal sealed class UriDecorator : ProtoDecoratorBase
	{
		// Token: 0x06007E81 RID: 32385 RVA: 0x00253B64 File Offset: 0x00253B64
		public UriDecorator(TypeModel model, IProtoSerializer tail) : base(tail)
		{
		}

		// Token: 0x17001B7D RID: 7037
		// (get) Token: 0x06007E82 RID: 32386 RVA: 0x00253B70 File Offset: 0x00253B70
		public override Type ExpectedType
		{
			get
			{
				return UriDecorator.expectedType;
			}
		}

		// Token: 0x17001B7E RID: 7038
		// (get) Token: 0x06007E83 RID: 32387 RVA: 0x00253B78 File Offset: 0x00253B78
		public override bool RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B7F RID: 7039
		// (get) Token: 0x06007E84 RID: 32388 RVA: 0x00253B7C File Offset: 0x00253B7C
		public override bool ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007E85 RID: 32389 RVA: 0x00253B80 File Offset: 0x00253B80
		public override void Write(object value, ProtoWriter dest)
		{
			this.Tail.Write(((Uri)value).OriginalString, dest);
		}

		// Token: 0x06007E86 RID: 32390 RVA: 0x00253B9C File Offset: 0x00253B9C
		public override object Read(object value, ProtoReader source)
		{
			string text = (string)this.Tail.Read(null, source);
			if (text.Length != 0)
			{
				return new Uri(text, UriKind.RelativeOrAbsolute);
			}
			return null;
		}

		// Token: 0x06007E87 RID: 32391 RVA: 0x00253BD4 File Offset: 0x00253BD4
		protected override void EmitWrite(CompilerContext ctx, Local valueFrom)
		{
			ctx.LoadValue(valueFrom);
			ctx.LoadValue(typeof(Uri).GetProperty("OriginalString"));
			this.Tail.EmitWrite(ctx, null);
		}

		// Token: 0x06007E88 RID: 32392 RVA: 0x00253C14 File Offset: 0x00253C14
		protected override void EmitRead(CompilerContext ctx, Local valueFrom)
		{
			this.Tail.EmitRead(ctx, valueFrom);
			ctx.CopyValue();
			CodeLabel label = ctx.DefineLabel();
			CodeLabel label2 = ctx.DefineLabel();
			ctx.LoadValue(typeof(string).GetProperty("Length"));
			ctx.BranchIfTrue(label, true);
			ctx.DiscardValue();
			ctx.LoadNullRef();
			ctx.Branch(label2, true);
			ctx.MarkLabel(label);
			ctx.LoadValue(0);
			ctx.EmitCtor(ctx.MapType(typeof(Uri)), new Type[]
			{
				ctx.MapType(typeof(string)),
				ctx.MapType(typeof(UriKind))
			});
			ctx.MarkLabel(label2);
		}

		// Token: 0x04003C9E RID: 15518
		private static readonly Type expectedType = typeof(Uri);
	}
}
