using System;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000C4C RID: 3148
	internal sealed class DateTimeSerializer : IProtoSerializer
	{
		// Token: 0x17001B11 RID: 6929
		// (get) Token: 0x06007D18 RID: 32024 RVA: 0x0024C44C File Offset: 0x0024C44C
		public Type ExpectedType
		{
			get
			{
				return DateTimeSerializer.expectedType;
			}
		}

		// Token: 0x17001B12 RID: 6930
		// (get) Token: 0x06007D19 RID: 32025 RVA: 0x0024C454 File Offset: 0x0024C454
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B13 RID: 6931
		// (get) Token: 0x06007D1A RID: 32026 RVA: 0x0024C458 File Offset: 0x0024C458
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007D1B RID: 32027 RVA: 0x0024C45C File Offset: 0x0024C45C
		public DateTimeSerializer(DataFormat dataFormat, TypeModel model)
		{
			this.wellKnown = (dataFormat == DataFormat.WellKnown);
			this.includeKind = (model != null && model.SerializeDateTimeKind());
		}

		// Token: 0x06007D1C RID: 32028 RVA: 0x0024C498 File Offset: 0x0024C498
		public object Read(object value, ProtoReader source)
		{
			if (this.wellKnown)
			{
				return BclHelpers.ReadTimestamp(source);
			}
			return BclHelpers.ReadDateTime(source);
		}

		// Token: 0x06007D1D RID: 32029 RVA: 0x0024C4BC File Offset: 0x0024C4BC
		public void Write(object value, ProtoWriter dest)
		{
			if (this.wellKnown)
			{
				BclHelpers.WriteTimestamp((DateTime)value, dest);
				return;
			}
			if (this.includeKind)
			{
				BclHelpers.WriteDateTimeWithKind((DateTime)value, dest);
				return;
			}
			BclHelpers.WriteDateTime((DateTime)value, dest);
		}

		// Token: 0x06007D1E RID: 32030 RVA: 0x0024C4FC File Offset: 0x0024C4FC
		void IProtoSerializer.EmitWrite(CompilerContext ctx, Local valueFrom)
		{
			ctx.EmitWrite(ctx.MapType(typeof(BclHelpers)), this.wellKnown ? "WriteTimestamp" : (this.includeKind ? "WriteDateTimeWithKind" : "WriteDateTime"), valueFrom);
		}

		// Token: 0x06007D1F RID: 32031 RVA: 0x0024C554 File Offset: 0x0024C554
		void IProtoSerializer.EmitRead(CompilerContext ctx, Local entity)
		{
			if (this.wellKnown)
			{
				ctx.LoadValue(entity);
			}
			ctx.EmitBasicRead(ctx.MapType(typeof(BclHelpers)), this.wellKnown ? "ReadTimestamp" : "ReadDateTime", this.ExpectedType);
		}

		// Token: 0x04003C43 RID: 15427
		private static readonly Type expectedType = typeof(DateTime);

		// Token: 0x04003C44 RID: 15428
		private readonly bool includeKind;

		// Token: 0x04003C45 RID: 15429
		private readonly bool wellKnown;
	}
}
