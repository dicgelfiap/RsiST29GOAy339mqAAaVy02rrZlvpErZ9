using System;
using System.Reflection;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000C4E RID: 3150
	internal sealed class DefaultValueDecorator : ProtoDecoratorBase
	{
		// Token: 0x17001B17 RID: 6935
		// (get) Token: 0x06007D2A RID: 32042 RVA: 0x0024C664 File Offset: 0x0024C664
		public override Type ExpectedType
		{
			get
			{
				return this.Tail.ExpectedType;
			}
		}

		// Token: 0x17001B18 RID: 6936
		// (get) Token: 0x06007D2B RID: 32043 RVA: 0x0024C674 File Offset: 0x0024C674
		public override bool RequiresOldValue
		{
			get
			{
				return this.Tail.RequiresOldValue;
			}
		}

		// Token: 0x17001B19 RID: 6937
		// (get) Token: 0x06007D2C RID: 32044 RVA: 0x0024C684 File Offset: 0x0024C684
		public override bool ReturnsValue
		{
			get
			{
				return this.Tail.ReturnsValue;
			}
		}

		// Token: 0x06007D2D RID: 32045 RVA: 0x0024C694 File Offset: 0x0024C694
		public DefaultValueDecorator(TypeModel model, object defaultValue, IProtoSerializer tail) : base(tail)
		{
			if (defaultValue == null)
			{
				throw new ArgumentNullException("defaultValue");
			}
			Type left = model.MapType(defaultValue.GetType());
			if (left != tail.ExpectedType)
			{
				throw new ArgumentException("Default value is of incorrect type", "defaultValue");
			}
			this.defaultValue = defaultValue;
		}

		// Token: 0x06007D2E RID: 32046 RVA: 0x0024C6F4 File Offset: 0x0024C6F4
		public override void Write(object value, ProtoWriter dest)
		{
			if (!object.Equals(value, this.defaultValue))
			{
				this.Tail.Write(value, dest);
			}
		}

		// Token: 0x06007D2F RID: 32047 RVA: 0x0024C714 File Offset: 0x0024C714
		public override object Read(object value, ProtoReader source)
		{
			return this.Tail.Read(value, source);
		}

		// Token: 0x06007D30 RID: 32048 RVA: 0x0024C724 File Offset: 0x0024C724
		protected override void EmitWrite(CompilerContext ctx, Local valueFrom)
		{
			CodeLabel label = ctx.DefineLabel();
			if (valueFrom == null)
			{
				ctx.CopyValue();
				CodeLabel label2 = ctx.DefineLabel();
				this.EmitBranchIfDefaultValue(ctx, label2);
				this.Tail.EmitWrite(ctx, null);
				ctx.Branch(label, true);
				ctx.MarkLabel(label2);
				ctx.DiscardValue();
			}
			else
			{
				ctx.LoadValue(valueFrom);
				this.EmitBranchIfDefaultValue(ctx, label);
				this.Tail.EmitWrite(ctx, valueFrom);
			}
			ctx.MarkLabel(label);
		}

		// Token: 0x06007D31 RID: 32049 RVA: 0x0024C7A4 File Offset: 0x0024C7A4
		private void EmitBeq(CompilerContext ctx, CodeLabel label, Type type)
		{
			ProtoTypeCode typeCode = Helpers.GetTypeCode(type);
			if (typeCode - ProtoTypeCode.Boolean <= 11)
			{
				ctx.BranchIfEqual(label, false);
				return;
			}
			MethodInfo method = type.GetMethod("op_Equality", BindingFlags.Static | BindingFlags.Public, null, new Type[]
			{
				type,
				type
			}, null);
			if (method == null || method.ReturnType != ctx.MapType(typeof(bool)))
			{
				throw new InvalidOperationException("No suitable equality operator found for default-values of type: " + type.FullName);
			}
			ctx.EmitCall(method);
			ctx.BranchIfTrue(label, false);
		}

		// Token: 0x06007D32 RID: 32050 RVA: 0x0024C840 File Offset: 0x0024C840
		private void EmitBranchIfDefaultValue(CompilerContext ctx, CodeLabel label)
		{
			Type expectedType = this.ExpectedType;
			ProtoTypeCode typeCode = Helpers.GetTypeCode(expectedType);
			switch (typeCode)
			{
			case ProtoTypeCode.Boolean:
				if ((bool)this.defaultValue)
				{
					ctx.BranchIfTrue(label, false);
					return;
				}
				ctx.BranchIfFalse(label, false);
				return;
			case ProtoTypeCode.Char:
				if ((char)this.defaultValue == '\0')
				{
					ctx.BranchIfFalse(label, false);
					return;
				}
				ctx.LoadValue((int)((char)this.defaultValue));
				this.EmitBeq(ctx, label, expectedType);
				return;
			case ProtoTypeCode.SByte:
				if ((sbyte)this.defaultValue == 0)
				{
					ctx.BranchIfFalse(label, false);
					return;
				}
				ctx.LoadValue((int)((sbyte)this.defaultValue));
				this.EmitBeq(ctx, label, expectedType);
				return;
			case ProtoTypeCode.Byte:
				if ((byte)this.defaultValue == 0)
				{
					ctx.BranchIfFalse(label, false);
					return;
				}
				ctx.LoadValue((int)((byte)this.defaultValue));
				this.EmitBeq(ctx, label, expectedType);
				return;
			case ProtoTypeCode.Int16:
				if ((short)this.defaultValue == 0)
				{
					ctx.BranchIfFalse(label, false);
					return;
				}
				ctx.LoadValue((int)((short)this.defaultValue));
				this.EmitBeq(ctx, label, expectedType);
				return;
			case ProtoTypeCode.UInt16:
				if ((ushort)this.defaultValue == 0)
				{
					ctx.BranchIfFalse(label, false);
					return;
				}
				ctx.LoadValue((int)((ushort)this.defaultValue));
				this.EmitBeq(ctx, label, expectedType);
				return;
			case ProtoTypeCode.Int32:
				if ((int)this.defaultValue == 0)
				{
					ctx.BranchIfFalse(label, false);
					return;
				}
				ctx.LoadValue((int)this.defaultValue);
				this.EmitBeq(ctx, label, expectedType);
				return;
			case ProtoTypeCode.UInt32:
				if ((uint)this.defaultValue == 0U)
				{
					ctx.BranchIfFalse(label, false);
					return;
				}
				ctx.LoadValue((int)((uint)this.defaultValue));
				this.EmitBeq(ctx, label, expectedType);
				return;
			case ProtoTypeCode.Int64:
				ctx.LoadValue((long)this.defaultValue);
				this.EmitBeq(ctx, label, expectedType);
				return;
			case ProtoTypeCode.UInt64:
				ctx.LoadValue((long)((ulong)this.defaultValue));
				this.EmitBeq(ctx, label, expectedType);
				return;
			case ProtoTypeCode.Single:
				ctx.LoadValue((float)this.defaultValue);
				this.EmitBeq(ctx, label, expectedType);
				return;
			case ProtoTypeCode.Double:
				ctx.LoadValue((double)this.defaultValue);
				this.EmitBeq(ctx, label, expectedType);
				return;
			case ProtoTypeCode.Decimal:
			{
				decimal value = (decimal)this.defaultValue;
				ctx.LoadValue(value);
				this.EmitBeq(ctx, label, expectedType);
				return;
			}
			case ProtoTypeCode.DateTime:
				ctx.LoadValue(((DateTime)this.defaultValue).ToBinary());
				ctx.EmitCall(ctx.MapType(typeof(DateTime)).GetMethod("FromBinary"));
				this.EmitBeq(ctx, label, expectedType);
				return;
			case (ProtoTypeCode)17:
				break;
			case ProtoTypeCode.String:
				ctx.LoadValue((string)this.defaultValue);
				this.EmitBeq(ctx, label, expectedType);
				return;
			default:
				if (typeCode == ProtoTypeCode.TimeSpan)
				{
					TimeSpan t = (TimeSpan)this.defaultValue;
					if (t == TimeSpan.Zero)
					{
						ctx.LoadValue(typeof(TimeSpan).GetField("Zero"));
					}
					else
					{
						ctx.LoadValue(t.Ticks);
						ctx.EmitCall(ctx.MapType(typeof(TimeSpan)).GetMethod("FromTicks"));
					}
					this.EmitBeq(ctx, label, expectedType);
					return;
				}
				if (typeCode == ProtoTypeCode.Guid)
				{
					ctx.LoadValue((Guid)this.defaultValue);
					this.EmitBeq(ctx, label, expectedType);
					return;
				}
				break;
			}
			throw new NotSupportedException("Type cannot be represented as a default value: " + expectedType.FullName);
		}

		// Token: 0x06007D33 RID: 32051 RVA: 0x0024CBD4 File Offset: 0x0024CBD4
		protected override void EmitRead(CompilerContext ctx, Local valueFrom)
		{
			this.Tail.EmitRead(ctx, valueFrom);
		}

		// Token: 0x04003C47 RID: 15431
		private readonly object defaultValue;
	}
}
