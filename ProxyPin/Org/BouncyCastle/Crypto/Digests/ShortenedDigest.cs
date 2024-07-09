using System;

namespace Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x0200036A RID: 874
	public class ShortenedDigest : IDigest
	{
		// Token: 0x06001AD5 RID: 6869 RVA: 0x000910D4 File Offset: 0x000910D4
		public ShortenedDigest(IDigest baseDigest, int length)
		{
			if (baseDigest == null)
			{
				throw new ArgumentNullException("baseDigest");
			}
			if (length > baseDigest.GetDigestSize())
			{
				throw new ArgumentException("baseDigest output not large enough to support length");
			}
			this.baseDigest = baseDigest;
			this.length = length;
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06001AD6 RID: 6870 RVA: 0x00091114 File Offset: 0x00091114
		public string AlgorithmName
		{
			get
			{
				return string.Concat(new object[]
				{
					this.baseDigest.AlgorithmName,
					"(",
					this.length * 8,
					")"
				});
			}
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x00091160 File Offset: 0x00091160
		public int GetDigestSize()
		{
			return this.length;
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x00091168 File Offset: 0x00091168
		public void Update(byte input)
		{
			this.baseDigest.Update(input);
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x00091178 File Offset: 0x00091178
		public void BlockUpdate(byte[] input, int inOff, int length)
		{
			this.baseDigest.BlockUpdate(input, inOff, length);
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x00091188 File Offset: 0x00091188
		public int DoFinal(byte[] output, int outOff)
		{
			byte[] array = new byte[this.baseDigest.GetDigestSize()];
			this.baseDigest.DoFinal(array, 0);
			Array.Copy(array, 0, output, outOff, this.length);
			return this.length;
		}

		// Token: 0x06001ADB RID: 6875 RVA: 0x000911D0 File Offset: 0x000911D0
		public void Reset()
		{
			this.baseDigest.Reset();
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x000911E0 File Offset: 0x000911E0
		public int GetByteLength()
		{
			return this.baseDigest.GetByteLength();
		}

		// Token: 0x040011D4 RID: 4564
		private IDigest baseDigest;

		// Token: 0x040011D5 RID: 4565
		private int length;
	}
}
