using System;
using Org.BouncyCastle.Crypto.Utilities;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x0200036D RID: 877
	public class SM3Digest : GeneralDigest
	{
		// Token: 0x06001B00 RID: 6912 RVA: 0x00091AF4 File Offset: 0x00091AF4
		static SM3Digest()
		{
			for (int i = 0; i < 16; i++)
			{
				uint num = 2043430169U;
				SM3Digest.T[i] = (num << i | num >> 32 - i);
			}
			for (int j = 16; j < 64; j++)
			{
				int num2 = j % 32;
				uint num3 = 2055708042U;
				SM3Digest.T[j] = (num3 << num2 | num3 >> 32 - num2);
			}
		}

		// Token: 0x06001B01 RID: 6913 RVA: 0x00091B74 File Offset: 0x00091B74
		public SM3Digest()
		{
			this.Reset();
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x00091BA8 File Offset: 0x00091BA8
		public SM3Digest(SM3Digest t) : base(t)
		{
			this.CopyIn(t);
		}

		// Token: 0x06001B03 RID: 6915 RVA: 0x00091BE0 File Offset: 0x00091BE0
		private void CopyIn(SM3Digest t)
		{
			Array.Copy(t.V, 0, this.V, 0, this.V.Length);
			Array.Copy(t.inwords, 0, this.inwords, 0, this.inwords.Length);
			this.xOff = t.xOff;
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06001B04 RID: 6916 RVA: 0x00091C34 File Offset: 0x00091C34
		public override string AlgorithmName
		{
			get
			{
				return "SM3";
			}
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x00091C3C File Offset: 0x00091C3C
		public override int GetDigestSize()
		{
			return 32;
		}

		// Token: 0x06001B06 RID: 6918 RVA: 0x00091C40 File Offset: 0x00091C40
		public override IMemoable Copy()
		{
			return new SM3Digest(this);
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x00091C48 File Offset: 0x00091C48
		public override void Reset(IMemoable other)
		{
			SM3Digest t = (SM3Digest)other;
			base.CopyIn(t);
			this.CopyIn(t);
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x00091C70 File Offset: 0x00091C70
		public override void Reset()
		{
			base.Reset();
			this.V[0] = 1937774191U;
			this.V[1] = 1226093241U;
			this.V[2] = 388252375U;
			this.V[3] = 3666478592U;
			this.V[4] = 2842636476U;
			this.V[5] = 372324522U;
			this.V[6] = 3817729613U;
			this.V[7] = 2969243214U;
			this.xOff = 0;
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x00091CF8 File Offset: 0x00091CF8
		public override int DoFinal(byte[] output, int outOff)
		{
			base.Finish();
			Pack.UInt32_To_BE(this.V, output, outOff);
			this.Reset();
			return 32;
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x00091D18 File Offset: 0x00091D18
		internal override void ProcessWord(byte[] input, int inOff)
		{
			uint num = Pack.BE_To_UInt32(input, inOff);
			this.inwords[this.xOff] = num;
			this.xOff++;
			if (this.xOff >= 16)
			{
				this.ProcessBlock();
			}
		}

		// Token: 0x06001B0B RID: 6923 RVA: 0x00091D60 File Offset: 0x00091D60
		internal override void ProcessLength(long bitLength)
		{
			if (this.xOff > 14)
			{
				this.inwords[this.xOff] = 0U;
				this.xOff++;
				this.ProcessBlock();
			}
			while (this.xOff < 14)
			{
				this.inwords[this.xOff] = 0U;
				this.xOff++;
			}
			this.inwords[this.xOff++] = (uint)(bitLength >> 32);
			this.inwords[this.xOff++] = (uint)bitLength;
		}

		// Token: 0x06001B0C RID: 6924 RVA: 0x00091E04 File Offset: 0x00091E04
		private uint P0(uint x)
		{
			uint num = x << 9 | x >> 23;
			uint num2 = x << 17 | x >> 15;
			return x ^ num ^ num2;
		}

		// Token: 0x06001B0D RID: 6925 RVA: 0x00091E30 File Offset: 0x00091E30
		private uint P1(uint x)
		{
			uint num = x << 15 | x >> 17;
			uint num2 = x << 23 | x >> 9;
			return x ^ num ^ num2;
		}

		// Token: 0x06001B0E RID: 6926 RVA: 0x00091E5C File Offset: 0x00091E5C
		private uint FF0(uint x, uint y, uint z)
		{
			return x ^ y ^ z;
		}

		// Token: 0x06001B0F RID: 6927 RVA: 0x00091E64 File Offset: 0x00091E64
		private uint FF1(uint x, uint y, uint z)
		{
			return (x & y) | (x & z) | (y & z);
		}

		// Token: 0x06001B10 RID: 6928 RVA: 0x00091E74 File Offset: 0x00091E74
		private uint GG0(uint x, uint y, uint z)
		{
			return x ^ y ^ z;
		}

		// Token: 0x06001B11 RID: 6929 RVA: 0x00091E7C File Offset: 0x00091E7C
		private uint GG1(uint x, uint y, uint z)
		{
			return (x & y) | (~x & z);
		}

		// Token: 0x06001B12 RID: 6930 RVA: 0x00091E88 File Offset: 0x00091E88
		internal override void ProcessBlock()
		{
			for (int i = 0; i < 16; i++)
			{
				this.W[i] = this.inwords[i];
			}
			for (int j = 16; j < 68; j++)
			{
				uint num = this.W[j - 3];
				uint num2 = num << 15 | num >> 17;
				uint num3 = this.W[j - 13];
				uint num4 = num3 << 7 | num3 >> 25;
				this.W[j] = (this.P1(this.W[j - 16] ^ this.W[j - 9] ^ num2) ^ num4 ^ this.W[j - 6]);
			}
			uint num5 = this.V[0];
			uint num6 = this.V[1];
			uint num7 = this.V[2];
			uint num8 = this.V[3];
			uint num9 = this.V[4];
			uint num10 = this.V[5];
			uint num11 = this.V[6];
			uint num12 = this.V[7];
			for (int k = 0; k < 16; k++)
			{
				uint num13 = num5 << 12 | num5 >> 20;
				uint num14 = num13 + num9 + SM3Digest.T[k];
				uint num15 = num14 << 7 | num14 >> 25;
				uint num16 = num15 ^ num13;
				uint num17 = this.W[k];
				uint num18 = num17 ^ this.W[k + 4];
				uint num19 = this.FF0(num5, num6, num7) + num8 + num16 + num18;
				uint x = this.GG0(num9, num10, num11) + num12 + num15 + num17;
				num8 = num7;
				num7 = (num6 << 9 | num6 >> 23);
				num6 = num5;
				num5 = num19;
				num12 = num11;
				num11 = (num10 << 19 | num10 >> 13);
				num10 = num9;
				num9 = this.P0(x);
			}
			for (int l = 16; l < 64; l++)
			{
				uint num20 = num5 << 12 | num5 >> 20;
				uint num21 = num20 + num9 + SM3Digest.T[l];
				uint num22 = num21 << 7 | num21 >> 25;
				uint num23 = num22 ^ num20;
				uint num24 = this.W[l];
				uint num25 = num24 ^ this.W[l + 4];
				uint num26 = this.FF1(num5, num6, num7) + num8 + num23 + num25;
				uint x2 = this.GG1(num9, num10, num11) + num12 + num22 + num24;
				num8 = num7;
				num7 = (num6 << 9 | num6 >> 23);
				num6 = num5;
				num5 = num26;
				num12 = num11;
				num11 = (num10 << 19 | num10 >> 13);
				num10 = num9;
				num9 = this.P0(x2);
			}
			uint[] v;
			(v = this.V)[0] = (v[0] ^ num5);
			(v = this.V)[1] = (v[1] ^ num6);
			(v = this.V)[2] = (v[2] ^ num7);
			(v = this.V)[3] = (v[3] ^ num8);
			(v = this.V)[4] = (v[4] ^ num9);
			(v = this.V)[5] = (v[5] ^ num10);
			(v = this.V)[6] = (v[6] ^ num11);
			(v = this.V)[7] = (v[7] ^ num12);
			this.xOff = 0;
		}

		// Token: 0x040011EB RID: 4587
		private const int DIGEST_LENGTH = 32;

		// Token: 0x040011EC RID: 4588
		private const int BLOCK_SIZE = 16;

		// Token: 0x040011ED RID: 4589
		private uint[] V = new uint[8];

		// Token: 0x040011EE RID: 4590
		private uint[] inwords = new uint[16];

		// Token: 0x040011EF RID: 4591
		private int xOff;

		// Token: 0x040011F0 RID: 4592
		private uint[] W = new uint[68];

		// Token: 0x040011F1 RID: 4593
		private static readonly uint[] T = new uint[64];
	}
}
