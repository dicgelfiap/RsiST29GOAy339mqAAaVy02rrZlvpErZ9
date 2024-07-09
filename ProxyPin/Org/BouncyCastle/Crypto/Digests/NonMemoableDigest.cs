using System;

namespace Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x0200035B RID: 859
	public class NonMemoableDigest : IDigest
	{
		// Token: 0x06001A07 RID: 6663 RVA: 0x000886C4 File Offset: 0x000886C4
		public NonMemoableDigest(IDigest baseDigest)
		{
			if (baseDigest == null)
			{
				throw new ArgumentNullException("baseDigest");
			}
			this.mBaseDigest = baseDigest;
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06001A08 RID: 6664 RVA: 0x000886E4 File Offset: 0x000886E4
		public virtual string AlgorithmName
		{
			get
			{
				return this.mBaseDigest.AlgorithmName;
			}
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x000886F4 File Offset: 0x000886F4
		public virtual int GetDigestSize()
		{
			return this.mBaseDigest.GetDigestSize();
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x00088704 File Offset: 0x00088704
		public virtual void Update(byte input)
		{
			this.mBaseDigest.Update(input);
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x00088714 File Offset: 0x00088714
		public virtual void BlockUpdate(byte[] input, int inOff, int len)
		{
			this.mBaseDigest.BlockUpdate(input, inOff, len);
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x00088724 File Offset: 0x00088724
		public virtual int DoFinal(byte[] output, int outOff)
		{
			return this.mBaseDigest.DoFinal(output, outOff);
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x00088734 File Offset: 0x00088734
		public virtual void Reset()
		{
			this.mBaseDigest.Reset();
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x00088744 File Offset: 0x00088744
		public virtual int GetByteLength()
		{
			return this.mBaseDigest.GetByteLength();
		}

		// Token: 0x0400117B RID: 4475
		protected readonly IDigest mBaseDigest;
	}
}
