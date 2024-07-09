using System;

namespace ProtoBuf.Meta
{
	// Token: 0x02000C71 RID: 3185
	internal sealed class MutableList : BasicList
	{
		// Token: 0x17001B82 RID: 7042
		public new object this[int index]
		{
			get
			{
				return this.head[index];
			}
			set
			{
				this.head[index] = value;
			}
		}

		// Token: 0x06007E95 RID: 32405 RVA: 0x00253E1C File Offset: 0x00253E1C
		public void RemoveLast()
		{
			this.head.RemoveLastWithMutate();
		}

		// Token: 0x06007E96 RID: 32406 RVA: 0x00253E2C File Offset: 0x00253E2C
		public void Clear()
		{
			this.head.Clear();
		}
	}
}
