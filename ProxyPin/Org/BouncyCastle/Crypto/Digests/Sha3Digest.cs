using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x02000365 RID: 869
	public class Sha3Digest : KeccakDigest
	{
		// Token: 0x06001AAD RID: 6829 RVA: 0x000907B4 File Offset: 0x000907B4
		private static int CheckBitLength(int bitLength)
		{
			if (bitLength <= 256)
			{
				if (bitLength != 224 && bitLength != 256)
				{
					goto IL_40;
				}
			}
			else if (bitLength != 384 && bitLength != 512)
			{
				goto IL_40;
			}
			return bitLength;
			IL_40:
			throw new ArgumentException(bitLength + " not supported for SHA-3", "bitLength");
		}

		// Token: 0x06001AAE RID: 6830 RVA: 0x00090820 File Offset: 0x00090820
		public Sha3Digest() : this(256)
		{
		}

		// Token: 0x06001AAF RID: 6831 RVA: 0x00090830 File Offset: 0x00090830
		public Sha3Digest(int bitLength) : base(Sha3Digest.CheckBitLength(bitLength))
		{
		}

		// Token: 0x06001AB0 RID: 6832 RVA: 0x00090840 File Offset: 0x00090840
		public Sha3Digest(Sha3Digest source) : base(source)
		{
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06001AB1 RID: 6833 RVA: 0x0009084C File Offset: 0x0009084C
		public override string AlgorithmName
		{
			get
			{
				return "SHA3-" + this.fixedOutputLength;
			}
		}

		// Token: 0x06001AB2 RID: 6834 RVA: 0x00090864 File Offset: 0x00090864
		public override int DoFinal(byte[] output, int outOff)
		{
			base.AbsorbBits(2, 2);
			return base.DoFinal(output, outOff);
		}

		// Token: 0x06001AB3 RID: 6835 RVA: 0x00090878 File Offset: 0x00090878
		protected override int DoFinal(byte[] output, int outOff, byte partialByte, int partialBits)
		{
			if (partialBits < 0 || partialBits > 7)
			{
				throw new ArgumentException("must be in the range [0,7]", "partialBits");
			}
			int num = ((int)partialByte & (1 << partialBits) - 1) | 2 << partialBits;
			int num2 = partialBits + 2;
			if (num2 >= 8)
			{
				base.Absorb(new byte[]
				{
					(byte)num
				}, 0, 1);
				num2 -= 8;
				num >>= 8;
			}
			return base.DoFinal(output, outOff, (byte)num, num2);
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x000908F4 File Offset: 0x000908F4
		public override IMemoable Copy()
		{
			return new Sha3Digest(this);
		}
	}
}
