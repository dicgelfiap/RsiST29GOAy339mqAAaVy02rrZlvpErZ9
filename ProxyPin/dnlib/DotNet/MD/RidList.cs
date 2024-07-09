using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009D0 RID: 2512
	[DebuggerDisplay("Count = {Count}")]
	[ComVisible(true)]
	public readonly struct RidList : IEnumerable<uint>, IEnumerable
	{
		// Token: 0x06005FB9 RID: 24505 RVA: 0x001C9C9C File Offset: 0x001C9C9C
		public static RidList Create(uint startRid, uint length)
		{
			return new RidList(startRid, length);
		}

		// Token: 0x06005FBA RID: 24506 RVA: 0x001C9CA8 File Offset: 0x001C9CA8
		public static RidList Create(IList<uint> rids)
		{
			return new RidList(rids);
		}

		// Token: 0x06005FBB RID: 24507 RVA: 0x001C9CB0 File Offset: 0x001C9CB0
		private RidList(uint startRid, uint length)
		{
			this.startRid = startRid;
			this.length = length;
			this.rids = null;
		}

		// Token: 0x06005FBC RID: 24508 RVA: 0x001C9CC8 File Offset: 0x001C9CC8
		private RidList(IList<uint> rids)
		{
			if (rids == null)
			{
				throw new ArgumentNullException("rids");
			}
			this.rids = rids;
			this.startRid = 0U;
			this.length = (uint)rids.Count;
		}

		// Token: 0x17001413 RID: 5139
		public uint this[int index]
		{
			get
			{
				if (this.rids != null)
				{
					if (index >= this.rids.Count)
					{
						return 0U;
					}
					return this.rids[index];
				}
				else
				{
					if (index >= (int)this.length)
					{
						return 0U;
					}
					return this.startRid + (uint)index;
				}
			}
		}

		// Token: 0x17001414 RID: 5140
		// (get) Token: 0x06005FBE RID: 24510 RVA: 0x001C9D4C File Offset: 0x001C9D4C
		public int Count
		{
			get
			{
				return (int)this.length;
			}
		}

		// Token: 0x06005FBF RID: 24511 RVA: 0x001C9D54 File Offset: 0x001C9D54
		public RidList.Enumerator GetEnumerator()
		{
			return new RidList.Enumerator(ref this);
		}

		// Token: 0x06005FC0 RID: 24512 RVA: 0x001C9D5C File Offset: 0x001C9D5C
		IEnumerator<uint> IEnumerable<uint>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06005FC1 RID: 24513 RVA: 0x001C9D6C File Offset: 0x001C9D6C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04002EF3 RID: 12019
		private readonly uint startRid;

		// Token: 0x04002EF4 RID: 12020
		private readonly uint length;

		// Token: 0x04002EF5 RID: 12021
		private readonly IList<uint> rids;

		// Token: 0x04002EF6 RID: 12022
		public static readonly RidList Empty = RidList.Create(0U, 0U);

		// Token: 0x02001040 RID: 4160
		public struct Enumerator : IEnumerator<uint>, IDisposable, IEnumerator
		{
			// Token: 0x06008FC7 RID: 36807 RVA: 0x002AD644 File Offset: 0x002AD644
			internal Enumerator(in RidList list)
			{
				this.startRid = list.startRid;
				this.length = list.length;
				this.rids = list.rids;
				this.index = 0U;
				this.current = 0U;
			}

			// Token: 0x17001E11 RID: 7697
			// (get) Token: 0x06008FC8 RID: 36808 RVA: 0x002AD678 File Offset: 0x002AD678
			public uint Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x17001E12 RID: 7698
			// (get) Token: 0x06008FC9 RID: 36809 RVA: 0x002AD680 File Offset: 0x002AD680
			object IEnumerator.Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x06008FCA RID: 36810 RVA: 0x002AD690 File Offset: 0x002AD690
			public void Dispose()
			{
			}

			// Token: 0x06008FCB RID: 36811 RVA: 0x002AD694 File Offset: 0x002AD694
			public bool MoveNext()
			{
				if (this.rids == null && this.index < this.length)
				{
					this.current = this.startRid + this.index;
					this.index += 1U;
					return true;
				}
				return this.MoveNextOther();
			}

			// Token: 0x06008FCC RID: 36812 RVA: 0x002AD6EC File Offset: 0x002AD6EC
			private bool MoveNextOther()
			{
				if (this.index >= this.length)
				{
					this.current = 0U;
					return false;
				}
				if (this.rids != null)
				{
					this.current = this.rids[(int)this.index];
				}
				else
				{
					this.current = this.startRid + this.index;
				}
				this.index += 1U;
				return true;
			}

			// Token: 0x06008FCD RID: 36813 RVA: 0x002AD760 File Offset: 0x002AD760
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x04004530 RID: 17712
			private readonly uint startRid;

			// Token: 0x04004531 RID: 17713
			private readonly uint length;

			// Token: 0x04004532 RID: 17714
			private readonly IList<uint> rids;

			// Token: 0x04004533 RID: 17715
			private uint index;

			// Token: 0x04004534 RID: 17716
			private uint current;
		}
	}
}
