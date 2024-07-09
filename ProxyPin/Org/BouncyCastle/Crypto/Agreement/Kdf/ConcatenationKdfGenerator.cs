using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Utilities;

namespace Org.BouncyCastle.Crypto.Agreement.Kdf
{
	// Token: 0x02000335 RID: 821
	public class ConcatenationKdfGenerator : IDerivationFunction
	{
		// Token: 0x060018A2 RID: 6306 RVA: 0x0007ED18 File Offset: 0x0007ED18
		public ConcatenationKdfGenerator(IDigest digest)
		{
			this.mDigest = digest;
			this.mHLen = digest.GetDigestSize();
		}

		// Token: 0x060018A3 RID: 6307 RVA: 0x0007ED34 File Offset: 0x0007ED34
		public virtual void Init(IDerivationParameters param)
		{
			if (!(param is KdfParameters))
			{
				throw new ArgumentException("KDF parameters required for ConcatenationKdfGenerator");
			}
			KdfParameters kdfParameters = (KdfParameters)param;
			this.mShared = kdfParameters.GetSharedSecret();
			this.mOtherInfo = kdfParameters.GetIV();
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x060018A4 RID: 6308 RVA: 0x0007ED7C File Offset: 0x0007ED7C
		public virtual IDigest Digest
		{
			get
			{
				return this.mDigest;
			}
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x0007ED84 File Offset: 0x0007ED84
		public virtual int GenerateBytes(byte[] outBytes, int outOff, int len)
		{
			if (outBytes.Length - len < outOff)
			{
				throw new DataLengthException("output buffer too small");
			}
			byte[] array = new byte[this.mHLen];
			byte[] array2 = new byte[4];
			uint n = 1U;
			int num = 0;
			this.mDigest.Reset();
			if (len > this.mHLen)
			{
				do
				{
					Pack.UInt32_To_BE(n, array2);
					this.mDigest.BlockUpdate(array2, 0, array2.Length);
					this.mDigest.BlockUpdate(this.mShared, 0, this.mShared.Length);
					this.mDigest.BlockUpdate(this.mOtherInfo, 0, this.mOtherInfo.Length);
					this.mDigest.DoFinal(array, 0);
					Array.Copy(array, 0, outBytes, outOff + num, this.mHLen);
					num += this.mHLen;
				}
				while ((ulong)n++ < (ulong)((long)(len / this.mHLen)));
			}
			if (num < len)
			{
				Pack.UInt32_To_BE(n, array2);
				this.mDigest.BlockUpdate(array2, 0, array2.Length);
				this.mDigest.BlockUpdate(this.mShared, 0, this.mShared.Length);
				this.mDigest.BlockUpdate(this.mOtherInfo, 0, this.mOtherInfo.Length);
				this.mDigest.DoFinal(array, 0);
				Array.Copy(array, 0, outBytes, outOff + num, len - num);
			}
			return len;
		}

		// Token: 0x0400104C RID: 4172
		private readonly IDigest mDigest;

		// Token: 0x0400104D RID: 4173
		private byte[] mShared;

		// Token: 0x0400104E RID: 4174
		private byte[] mOtherInfo;

		// Token: 0x0400104F RID: 4175
		private int mHLen;
	}
}
