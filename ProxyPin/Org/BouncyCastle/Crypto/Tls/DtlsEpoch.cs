using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004F5 RID: 1269
	internal class DtlsEpoch
	{
		// Token: 0x060026F9 RID: 9977 RVA: 0x000D3334 File Offset: 0x000D3334
		internal DtlsEpoch(int epoch, TlsCipher cipher)
		{
			if (epoch < 0)
			{
				throw new ArgumentException("must be >= 0", "epoch");
			}
			if (cipher == null)
			{
				throw new ArgumentNullException("cipher");
			}
			this.mEpoch = epoch;
			this.mCipher = cipher;
		}

		// Token: 0x060026FA RID: 9978 RVA: 0x000D3394 File Offset: 0x000D3394
		internal long AllocateSequenceNumber()
		{
			long result;
			lock (this)
			{
				if (this.mSequenceNumber >= 281474976710656L)
				{
					throw new TlsFatalAlert(80);
				}
				long num;
				this.mSequenceNumber = (num = this.mSequenceNumber) + 1L;
				result = num;
			}
			return result;
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x060026FB RID: 9979 RVA: 0x000D33F8 File Offset: 0x000D33F8
		internal TlsCipher Cipher
		{
			get
			{
				return this.mCipher;
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x060026FC RID: 9980 RVA: 0x000D3400 File Offset: 0x000D3400
		internal int Epoch
		{
			get
			{
				return this.mEpoch;
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x060026FD RID: 9981 RVA: 0x000D3408 File Offset: 0x000D3408
		internal DtlsReplayWindow ReplayWindow
		{
			get
			{
				return this.mReplayWindow;
			}
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x060026FE RID: 9982 RVA: 0x000D3410 File Offset: 0x000D3410
		internal long SequenceNumber
		{
			get
			{
				long result;
				lock (this)
				{
					result = this.mSequenceNumber;
				}
				return result;
			}
		}

		// Token: 0x04001929 RID: 6441
		private readonly DtlsReplayWindow mReplayWindow = new DtlsReplayWindow();

		// Token: 0x0400192A RID: 6442
		private readonly int mEpoch;

		// Token: 0x0400192B RID: 6443
		private readonly TlsCipher mCipher;

		// Token: 0x0400192C RID: 6444
		private long mSequenceNumber = 0L;
	}
}
