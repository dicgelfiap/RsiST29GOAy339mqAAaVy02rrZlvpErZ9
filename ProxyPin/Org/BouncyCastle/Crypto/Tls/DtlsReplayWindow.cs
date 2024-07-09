using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004FA RID: 1274
	internal class DtlsReplayWindow
	{
		// Token: 0x06002730 RID: 10032 RVA: 0x000D4744 File Offset: 0x000D4744
		internal bool ShouldDiscard(long seq)
		{
			if ((seq & 281474976710655L) != seq)
			{
				return true;
			}
			if (seq <= this.mLatestConfirmedSeq)
			{
				long num = this.mLatestConfirmedSeq - seq;
				if (num >= 64L)
				{
					return true;
				}
				if ((this.mBitmap & 1L << (int)num) != 0L)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002731 RID: 10033 RVA: 0x000D47A4 File Offset: 0x000D47A4
		internal void ReportAuthenticated(long seq)
		{
			if ((seq & 281474976710655L) != seq)
			{
				throw new ArgumentException("out of range", "seq");
			}
			if (seq <= this.mLatestConfirmedSeq)
			{
				long num = this.mLatestConfirmedSeq - seq;
				if (num < 64L)
				{
					this.mBitmap |= 1L << (int)num;
					return;
				}
			}
			else
			{
				long num2 = seq - this.mLatestConfirmedSeq;
				if (num2 >= 64L)
				{
					this.mBitmap = 1L;
				}
				else
				{
					this.mBitmap <<= (int)num2;
					this.mBitmap |= 1L;
				}
				this.mLatestConfirmedSeq = seq;
			}
		}

		// Token: 0x06002732 RID: 10034 RVA: 0x000D4854 File Offset: 0x000D4854
		internal void Reset()
		{
			this.mLatestConfirmedSeq = -1L;
			this.mBitmap = 0L;
		}

		// Token: 0x0400194F RID: 6479
		private const long VALID_SEQ_MASK = 281474976710655L;

		// Token: 0x04001950 RID: 6480
		private const long WINDOW_SIZE = 64L;

		// Token: 0x04001951 RID: 6481
		private long mLatestConfirmedSeq = -1L;

		// Token: 0x04001952 RID: 6482
		private long mBitmap = 0L;
	}
}
