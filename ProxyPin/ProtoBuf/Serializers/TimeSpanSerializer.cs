using System;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000C69 RID: 3177
	internal sealed class TimeSpanSerializer : IProtoSerializer
	{
		// Token: 0x06007E36 RID: 32310 RVA: 0x00251A4C File Offset: 0x00251A4C
		public TimeSpanSerializer(DataFormat dataFormat, TypeModel model)
		{
			this.wellKnown = (dataFormat == DataFormat.WellKnown);
		}

		// Token: 0x17001B6A RID: 7018
		// (get) Token: 0x06007E37 RID: 32311 RVA: 0x00251A60 File Offset: 0x00251A60
		public Type ExpectedType
		{
			get
			{
				return TimeSpanSerializer.expectedType;
			}
		}

		// Token: 0x17001B6B RID: 7019
		// (get) Token: 0x06007E38 RID: 32312 RVA: 0x00251A68 File Offset: 0x00251A68
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B6C RID: 7020
		// (get) Token: 0x06007E39 RID: 32313 RVA: 0x00251A6C File Offset: 0x00251A6C
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007E3A RID: 32314 RVA: 0x00251A70 File Offset: 0x00251A70
		public object Read(object value, ProtoReader source)
		{
			if (this.wellKnown)
			{
				return BclHelpers.ReadDuration(source);
			}
			return BclHelpers.ReadTimeSpan(source);
		}

		// Token: 0x06007E3B RID: 32315 RVA: 0x00251A94 File Offset: 0x00251A94
		public void Write(object value, ProtoWriter dest)
		{
			if (this.wellKnown)
			{
				BclHelpers.WriteDuration((TimeSpan)value, dest);
				return;
			}
			BclHelpers.WriteTimeSpan((TimeSpan)value, dest);
		}

		// Token: 0x06007E3C RID: 32316 RVA: 0x00251ABC File Offset: 0x00251ABC
		void IProtoSerializer.EmitWrite(CompilerContext ctx, Local valueFrom)
		{
			ctx.EmitWrite(ctx.MapType(typeof(BclHelpers)), this.wellKnown ? "WriteDuration" : "WriteTimeSpan", valueFrom);
		}

		// Token: 0x06007E3D RID: 32317 RVA: 0x00251B00 File Offset: 0x00251B00
		void IProtoSerializer.EmitRead(CompilerContext ctx, Local valueFrom)
		{
			if (this.wellKnown)
			{
				ctx.LoadValue(valueFrom);
			}
			ctx.EmitBasicRead(ctx.MapType(typeof(BclHelpers)), this.wellKnown ? "ReadDuration" : "ReadTimeSpan", this.ExpectedType);
		}

		// Token: 0x04003C8A RID: 15498
		private static readonly Type expectedType = typeof(TimeSpan);

		// Token: 0x04003C8B RID: 15499
		private readonly bool wellKnown;
	}
}
