using System;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000C68 RID: 3176
	internal sealed class TagDecorator : ProtoDecoratorBase, IProtoTypeSerializer, IProtoSerializer
	{
		// Token: 0x06007E27 RID: 32295 RVA: 0x002517D8 File Offset: 0x002517D8
		public bool HasCallbacks(TypeModel.CallbackType callbackType)
		{
			IProtoTypeSerializer protoTypeSerializer = this.Tail as IProtoTypeSerializer;
			return protoTypeSerializer != null && protoTypeSerializer.HasCallbacks(callbackType);
		}

		// Token: 0x06007E28 RID: 32296 RVA: 0x00251804 File Offset: 0x00251804
		public bool CanCreateInstance()
		{
			IProtoTypeSerializer protoTypeSerializer = this.Tail as IProtoTypeSerializer;
			return protoTypeSerializer != null && protoTypeSerializer.CanCreateInstance();
		}

		// Token: 0x06007E29 RID: 32297 RVA: 0x00251830 File Offset: 0x00251830
		public object CreateInstance(ProtoReader source)
		{
			return ((IProtoTypeSerializer)this.Tail).CreateInstance(source);
		}

		// Token: 0x06007E2A RID: 32298 RVA: 0x00251844 File Offset: 0x00251844
		public void Callback(object value, TypeModel.CallbackType callbackType, SerializationContext context)
		{
			IProtoTypeSerializer protoTypeSerializer = this.Tail as IProtoTypeSerializer;
			if (protoTypeSerializer != null)
			{
				protoTypeSerializer.Callback(value, callbackType, context);
			}
		}

		// Token: 0x06007E2B RID: 32299 RVA: 0x00251870 File Offset: 0x00251870
		public void EmitCallback(CompilerContext ctx, Local valueFrom, TypeModel.CallbackType callbackType)
		{
			((IProtoTypeSerializer)this.Tail).EmitCallback(ctx, valueFrom, callbackType);
		}

		// Token: 0x06007E2C RID: 32300 RVA: 0x00251888 File Offset: 0x00251888
		public void EmitCreateInstance(CompilerContext ctx)
		{
			((IProtoTypeSerializer)this.Tail).EmitCreateInstance(ctx);
		}

		// Token: 0x17001B66 RID: 7014
		// (get) Token: 0x06007E2D RID: 32301 RVA: 0x0025189C File Offset: 0x0025189C
		public override Type ExpectedType
		{
			get
			{
				return this.Tail.ExpectedType;
			}
		}

		// Token: 0x06007E2E RID: 32302 RVA: 0x002518AC File Offset: 0x002518AC
		public TagDecorator(int fieldNumber, WireType wireType, bool strict, IProtoSerializer tail) : base(tail)
		{
			this.fieldNumber = fieldNumber;
			this.wireType = wireType;
			this.strict = strict;
		}

		// Token: 0x17001B67 RID: 7015
		// (get) Token: 0x06007E2F RID: 32303 RVA: 0x002518CC File Offset: 0x002518CC
		public override bool RequiresOldValue
		{
			get
			{
				return this.Tail.RequiresOldValue;
			}
		}

		// Token: 0x17001B68 RID: 7016
		// (get) Token: 0x06007E30 RID: 32304 RVA: 0x002518DC File Offset: 0x002518DC
		public override bool ReturnsValue
		{
			get
			{
				return this.Tail.ReturnsValue;
			}
		}

		// Token: 0x17001B69 RID: 7017
		// (get) Token: 0x06007E31 RID: 32305 RVA: 0x002518EC File Offset: 0x002518EC
		private bool NeedsHint
		{
			get
			{
				return (this.wireType & (WireType)(-8)) > WireType.Variant;
			}
		}

		// Token: 0x06007E32 RID: 32306 RVA: 0x002518FC File Offset: 0x002518FC
		public override object Read(object value, ProtoReader source)
		{
			if (this.strict)
			{
				source.Assert(this.wireType);
			}
			else if (this.NeedsHint)
			{
				source.Hint(this.wireType);
			}
			return this.Tail.Read(value, source);
		}

		// Token: 0x06007E33 RID: 32307 RVA: 0x00251950 File Offset: 0x00251950
		public override void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteFieldHeader(this.fieldNumber, this.wireType, dest);
			this.Tail.Write(value, dest);
		}

		// Token: 0x06007E34 RID: 32308 RVA: 0x00251974 File Offset: 0x00251974
		protected override void EmitWrite(CompilerContext ctx, Local valueFrom)
		{
			ctx.LoadValue(this.fieldNumber);
			ctx.LoadValue((int)this.wireType);
			ctx.LoadReaderWriter();
			ctx.EmitCall(ctx.MapType(typeof(ProtoWriter)).GetMethod("WriteFieldHeader"));
			this.Tail.EmitWrite(ctx, valueFrom);
		}

		// Token: 0x06007E35 RID: 32309 RVA: 0x002519D0 File Offset: 0x002519D0
		protected override void EmitRead(CompilerContext ctx, Local valueFrom)
		{
			if (this.strict || this.NeedsHint)
			{
				ctx.LoadReaderWriter();
				ctx.LoadValue((int)this.wireType);
				ctx.EmitCall(ctx.MapType(typeof(ProtoReader)).GetMethod(this.strict ? "Assert" : "Hint"));
			}
			this.Tail.EmitRead(ctx, valueFrom);
		}

		// Token: 0x04003C87 RID: 15495
		private readonly bool strict;

		// Token: 0x04003C88 RID: 15496
		private readonly int fieldNumber;

		// Token: 0x04003C89 RID: 15497
		private readonly WireType wireType;
	}
}
