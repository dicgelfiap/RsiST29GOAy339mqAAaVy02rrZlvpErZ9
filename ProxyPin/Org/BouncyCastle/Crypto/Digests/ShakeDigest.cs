using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x02000369 RID: 873
	public class ShakeDigest : KeccakDigest, IXof, IDigest
	{
		// Token: 0x06001ACA RID: 6858 RVA: 0x00090F40 File Offset: 0x00090F40
		private static int CheckBitLength(int bitLength)
		{
			if (bitLength == 128 || bitLength == 256)
			{
				return bitLength;
			}
			throw new ArgumentException(bitLength + " not supported for SHAKE", "bitLength");
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x00090F88 File Offset: 0x00090F88
		public ShakeDigest() : this(128)
		{
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x00090F98 File Offset: 0x00090F98
		public ShakeDigest(int bitLength) : base(ShakeDigest.CheckBitLength(bitLength))
		{
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x00090FA8 File Offset: 0x00090FA8
		public ShakeDigest(ShakeDigest source) : base(source)
		{
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06001ACE RID: 6862 RVA: 0x00090FB4 File Offset: 0x00090FB4
		public override string AlgorithmName
		{
			get
			{
				return "SHAKE" + this.fixedOutputLength;
			}
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x00090FCC File Offset: 0x00090FCC
		public override int DoFinal(byte[] output, int outOff)
		{
			return this.DoFinal(output, outOff, this.GetDigestSize());
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x00090FDC File Offset: 0x00090FDC
		public virtual int DoFinal(byte[] output, int outOff, int outLen)
		{
			this.DoOutput(output, outOff, outLen);
			this.Reset();
			return outLen;
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x00090FF0 File Offset: 0x00090FF0
		public virtual int DoOutput(byte[] output, int outOff, int outLen)
		{
			if (!this.squeezing)
			{
				base.AbsorbBits(15, 4);
			}
			base.Squeeze(output, outOff, (long)outLen << 3);
			return outLen;
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x00091014 File Offset: 0x00091014
		protected override int DoFinal(byte[] output, int outOff, byte partialByte, int partialBits)
		{
			return this.DoFinal(output, outOff, this.GetDigestSize(), partialByte, partialBits);
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x00091038 File Offset: 0x00091038
		protected virtual int DoFinal(byte[] output, int outOff, int outLen, byte partialByte, int partialBits)
		{
			if (partialBits < 0 || partialBits > 7)
			{
				throw new ArgumentException("must be in the range [0,7]", "partialBits");
			}
			int num = ((int)partialByte & (1 << partialBits) - 1) | 15 << partialBits;
			int num2 = partialBits + 4;
			if (num2 >= 8)
			{
				base.Absorb(new byte[]
				{
					(byte)num
				}, 0, 1);
				num2 -= 8;
				num >>= 8;
			}
			if (num2 > 0)
			{
				base.AbsorbBits(num, num2);
			}
			base.Squeeze(output, outOff, (long)outLen << 3);
			this.Reset();
			return outLen;
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x000910CC File Offset: 0x000910CC
		public override IMemoable Copy()
		{
			return new ShakeDigest(this);
		}
	}
}
