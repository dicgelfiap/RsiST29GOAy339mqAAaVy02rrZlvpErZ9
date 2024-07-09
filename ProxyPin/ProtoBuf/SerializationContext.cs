using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace ProtoBuf
{
	// Token: 0x02000C3D RID: 3133
	[ComVisible(true)]
	public sealed class SerializationContext
	{
		// Token: 0x06007C9A RID: 31898 RVA: 0x0024AB5C File Offset: 0x0024AB5C
		internal void Freeze()
		{
			this.frozen = true;
		}

		// Token: 0x06007C9B RID: 31899 RVA: 0x0024AB68 File Offset: 0x0024AB68
		private void ThrowIfFrozen()
		{
			if (this.frozen)
			{
				throw new InvalidOperationException("The serialization-context cannot be changed once it is in use");
			}
		}

		// Token: 0x17001AFA RID: 6906
		// (get) Token: 0x06007C9C RID: 31900 RVA: 0x0024AB80 File Offset: 0x0024AB80
		// (set) Token: 0x06007C9D RID: 31901 RVA: 0x0024AB88 File Offset: 0x0024AB88
		public object Context
		{
			get
			{
				return this.context;
			}
			set
			{
				if (this.context != value)
				{
					this.ThrowIfFrozen();
					this.context = value;
				}
			}
		}

		// Token: 0x06007C9E RID: 31902 RVA: 0x0024ABA4 File Offset: 0x0024ABA4
		static SerializationContext()
		{
			SerializationContext.@default.Freeze();
		}

		// Token: 0x17001AFB RID: 6907
		// (get) Token: 0x06007C9F RID: 31903 RVA: 0x0024ABBC File Offset: 0x0024ABBC
		internal static SerializationContext Default
		{
			get
			{
				return SerializationContext.@default;
			}
		}

		// Token: 0x17001AFC RID: 6908
		// (get) Token: 0x06007CA0 RID: 31904 RVA: 0x0024ABC4 File Offset: 0x0024ABC4
		// (set) Token: 0x06007CA1 RID: 31905 RVA: 0x0024ABCC File Offset: 0x0024ABCC
		public StreamingContextStates State
		{
			get
			{
				return this.state;
			}
			set
			{
				if (this.state != value)
				{
					this.ThrowIfFrozen();
					this.state = value;
				}
			}
		}

		// Token: 0x06007CA2 RID: 31906 RVA: 0x0024ABE8 File Offset: 0x0024ABE8
		public static implicit operator StreamingContext(SerializationContext ctx)
		{
			if (ctx == null)
			{
				return new StreamingContext(StreamingContextStates.Persistence);
			}
			return new StreamingContext(ctx.state, ctx.context);
		}

		// Token: 0x06007CA3 RID: 31907 RVA: 0x0024AC08 File Offset: 0x0024AC08
		public static implicit operator SerializationContext(StreamingContext ctx)
		{
			return new SerializationContext
			{
				Context = ctx.Context,
				State = ctx.State
			};
		}

		// Token: 0x04003C1C RID: 15388
		private bool frozen;

		// Token: 0x04003C1D RID: 15389
		private object context;

		// Token: 0x04003C1E RID: 15390
		private static readonly SerializationContext @default = new SerializationContext();

		// Token: 0x04003C1F RID: 15391
		private StreamingContextStates state = StreamingContextStates.Persistence;
	}
}
