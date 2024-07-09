using System;
using System.Collections;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004F7 RID: 1271
	internal class DtlsReassembler
	{
		// Token: 0x06002700 RID: 9984 RVA: 0x000D344C File Offset: 0x000D344C
		internal DtlsReassembler(byte msg_type, int length)
		{
			this.mMsgType = msg_type;
			this.mBody = new byte[length];
			this.mMissing.Add(new DtlsReassembler.Range(0, length));
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06002701 RID: 9985 RVA: 0x000D3488 File Offset: 0x000D3488
		internal byte MsgType
		{
			get
			{
				return this.mMsgType;
			}
		}

		// Token: 0x06002702 RID: 9986 RVA: 0x000D3490 File Offset: 0x000D3490
		internal byte[] GetBodyIfComplete()
		{
			if (this.mMissing.Count != 0)
			{
				return null;
			}
			return this.mBody;
		}

		// Token: 0x06002703 RID: 9987 RVA: 0x000D34AC File Offset: 0x000D34AC
		internal void ContributeFragment(byte msg_type, int length, byte[] buf, int off, int fragment_offset, int fragment_length)
		{
			int num = fragment_offset + fragment_length;
			if (this.mMsgType != msg_type || this.mBody.Length != length || num > length)
			{
				return;
			}
			if (fragment_length == 0)
			{
				if (fragment_offset == 0 && this.mMissing.Count > 0)
				{
					DtlsReassembler.Range range = (DtlsReassembler.Range)this.mMissing[0];
					if (range.End == 0)
					{
						this.mMissing.RemoveAt(0);
					}
				}
				return;
			}
			for (int i = 0; i < this.mMissing.Count; i++)
			{
				DtlsReassembler.Range range2 = (DtlsReassembler.Range)this.mMissing[i];
				if (range2.Start >= num)
				{
					return;
				}
				if (range2.End > fragment_offset)
				{
					int num2 = Math.Max(range2.Start, fragment_offset);
					int num3 = Math.Min(range2.End, num);
					int length2 = num3 - num2;
					Array.Copy(buf, off + num2 - fragment_offset, this.mBody, num2, length2);
					if (num2 == range2.Start)
					{
						if (num3 == range2.End)
						{
							this.mMissing.RemoveAt(i--);
						}
						else
						{
							range2.Start = num3;
						}
					}
					else
					{
						if (num3 != range2.End)
						{
							this.mMissing.Insert(++i, new DtlsReassembler.Range(num3, range2.End));
						}
						range2.End = num2;
					}
				}
			}
		}

		// Token: 0x06002704 RID: 9988 RVA: 0x000D3620 File Offset: 0x000D3620
		internal void Reset()
		{
			this.mMissing.Clear();
			this.mMissing.Add(new DtlsReassembler.Range(0, this.mBody.Length));
		}

		// Token: 0x0400192D RID: 6445
		private readonly byte mMsgType;

		// Token: 0x0400192E RID: 6446
		private readonly byte[] mBody;

		// Token: 0x0400192F RID: 6447
		private readonly IList mMissing = Platform.CreateArrayList();

		// Token: 0x02000E1B RID: 3611
		private class Range
		{
			// Token: 0x06008C44 RID: 35908 RVA: 0x002A22AC File Offset: 0x002A22AC
			internal Range(int start, int end)
			{
				this.mStart = start;
				this.mEnd = end;
			}

			// Token: 0x17001D7B RID: 7547
			// (get) Token: 0x06008C45 RID: 35909 RVA: 0x002A22C4 File Offset: 0x002A22C4
			// (set) Token: 0x06008C46 RID: 35910 RVA: 0x002A22CC File Offset: 0x002A22CC
			public int Start
			{
				get
				{
					return this.mStart;
				}
				set
				{
					this.mStart = value;
				}
			}

			// Token: 0x17001D7C RID: 7548
			// (get) Token: 0x06008C47 RID: 35911 RVA: 0x002A22D8 File Offset: 0x002A22D8
			// (set) Token: 0x06008C48 RID: 35912 RVA: 0x002A22E0 File Offset: 0x002A22E0
			public int End
			{
				get
				{
					return this.mEnd;
				}
				set
				{
					this.mEnd = value;
				}
			}

			// Token: 0x0400416E RID: 16750
			private int mStart;

			// Token: 0x0400416F RID: 16751
			private int mEnd;
		}
	}
}
