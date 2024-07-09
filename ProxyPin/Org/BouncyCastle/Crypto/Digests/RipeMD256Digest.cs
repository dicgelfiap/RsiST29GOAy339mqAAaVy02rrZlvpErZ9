using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x0200035F RID: 863
	public class RipeMD256Digest : GeneralDigest
	{
		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06001A44 RID: 6724 RVA: 0x0008BB5C File Offset: 0x0008BB5C
		public override string AlgorithmName
		{
			get
			{
				return "RIPEMD256";
			}
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x0008BB64 File Offset: 0x0008BB64
		public override int GetDigestSize()
		{
			return 32;
		}

		// Token: 0x06001A46 RID: 6726 RVA: 0x0008BB68 File Offset: 0x0008BB68
		public RipeMD256Digest()
		{
			this.Reset();
		}

		// Token: 0x06001A47 RID: 6727 RVA: 0x0008BB84 File Offset: 0x0008BB84
		public RipeMD256Digest(RipeMD256Digest t) : base(t)
		{
			this.CopyIn(t);
		}

		// Token: 0x06001A48 RID: 6728 RVA: 0x0008BBA4 File Offset: 0x0008BBA4
		private void CopyIn(RipeMD256Digest t)
		{
			base.CopyIn(t);
			this.H0 = t.H0;
			this.H1 = t.H1;
			this.H2 = t.H2;
			this.H3 = t.H3;
			this.H4 = t.H4;
			this.H5 = t.H5;
			this.H6 = t.H6;
			this.H7 = t.H7;
			Array.Copy(t.X, 0, this.X, 0, t.X.Length);
			this.xOff = t.xOff;
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x0008BC44 File Offset: 0x0008BC44
		internal override void ProcessWord(byte[] input, int inOff)
		{
			this.X[this.xOff++] = ((int)(input[inOff] & byte.MaxValue) | (int)(input[inOff + 1] & byte.MaxValue) << 8 | (int)(input[inOff + 2] & byte.MaxValue) << 16 | (int)(input[inOff + 3] & byte.MaxValue) << 24);
			if (this.xOff == 16)
			{
				this.ProcessBlock();
			}
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x0008BCB8 File Offset: 0x0008BCB8
		internal override void ProcessLength(long bitLength)
		{
			if (this.xOff > 14)
			{
				this.ProcessBlock();
			}
			this.X[14] = (int)(bitLength & (long)((ulong)-1));
			this.X[15] = (int)((ulong)bitLength >> 32);
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x0008BCEC File Offset: 0x0008BCEC
		private void UnpackWord(int word, byte[] outBytes, int outOff)
		{
			outBytes[outOff] = (byte)word;
			outBytes[outOff + 1] = (byte)((uint)word >> 8);
			outBytes[outOff + 2] = (byte)((uint)word >> 16);
			outBytes[outOff + 3] = (byte)((uint)word >> 24);
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x0008BD10 File Offset: 0x0008BD10
		public override int DoFinal(byte[] output, int outOff)
		{
			base.Finish();
			this.UnpackWord(this.H0, output, outOff);
			this.UnpackWord(this.H1, output, outOff + 4);
			this.UnpackWord(this.H2, output, outOff + 8);
			this.UnpackWord(this.H3, output, outOff + 12);
			this.UnpackWord(this.H4, output, outOff + 16);
			this.UnpackWord(this.H5, output, outOff + 20);
			this.UnpackWord(this.H6, output, outOff + 24);
			this.UnpackWord(this.H7, output, outOff + 28);
			this.Reset();
			return 32;
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x0008BDB4 File Offset: 0x0008BDB4
		public override void Reset()
		{
			base.Reset();
			this.H0 = 1732584193;
			this.H1 = -271733879;
			this.H2 = -1732584194;
			this.H3 = 271733878;
			this.H4 = 1985229328;
			this.H5 = -19088744;
			this.H6 = -1985229329;
			this.H7 = 19088743;
			this.xOff = 0;
			for (int num = 0; num != this.X.Length; num++)
			{
				this.X[num] = 0;
			}
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x0008BE4C File Offset: 0x0008BE4C
		private int RL(int x, int n)
		{
			return x << n | (int)((uint)x >> 32 - n);
		}

		// Token: 0x06001A4F RID: 6735 RVA: 0x0008BE60 File Offset: 0x0008BE60
		private int F1(int x, int y, int z)
		{
			return x ^ y ^ z;
		}

		// Token: 0x06001A50 RID: 6736 RVA: 0x0008BE68 File Offset: 0x0008BE68
		private int F2(int x, int y, int z)
		{
			return (x & y) | (~x & z);
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x0008BE74 File Offset: 0x0008BE74
		private int F3(int x, int y, int z)
		{
			return (x | ~y) ^ z;
		}

		// Token: 0x06001A52 RID: 6738 RVA: 0x0008BE7C File Offset: 0x0008BE7C
		private int F4(int x, int y, int z)
		{
			return (x & z) | (y & ~z);
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x0008BE88 File Offset: 0x0008BE88
		private int F1(int a, int b, int c, int d, int x, int s)
		{
			return this.RL(a + this.F1(b, c, d) + x, s);
		}

		// Token: 0x06001A54 RID: 6740 RVA: 0x0008BEA4 File Offset: 0x0008BEA4
		private int F2(int a, int b, int c, int d, int x, int s)
		{
			return this.RL(a + this.F2(b, c, d) + x + 1518500249, s);
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x0008BEC4 File Offset: 0x0008BEC4
		private int F3(int a, int b, int c, int d, int x, int s)
		{
			return this.RL(a + this.F3(b, c, d) + x + 1859775393, s);
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x0008BEE4 File Offset: 0x0008BEE4
		private int F4(int a, int b, int c, int d, int x, int s)
		{
			return this.RL(a + this.F4(b, c, d) + x + -1894007588, s);
		}

		// Token: 0x06001A57 RID: 6743 RVA: 0x0008BF04 File Offset: 0x0008BF04
		private int FF1(int a, int b, int c, int d, int x, int s)
		{
			return this.RL(a + this.F1(b, c, d) + x, s);
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x0008BF20 File Offset: 0x0008BF20
		private int FF2(int a, int b, int c, int d, int x, int s)
		{
			return this.RL(a + this.F2(b, c, d) + x + 1836072691, s);
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x0008BF40 File Offset: 0x0008BF40
		private int FF3(int a, int b, int c, int d, int x, int s)
		{
			return this.RL(a + this.F3(b, c, d) + x + 1548603684, s);
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x0008BF60 File Offset: 0x0008BF60
		private int FF4(int a, int b, int c, int d, int x, int s)
		{
			return this.RL(a + this.F4(b, c, d) + x + 1352829926, s);
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x0008BF80 File Offset: 0x0008BF80
		internal override void ProcessBlock()
		{
			int num = this.H0;
			int num2 = this.H1;
			int num3 = this.H2;
			int num4 = this.H3;
			int num5 = this.H4;
			int num6 = this.H5;
			int num7 = this.H6;
			int num8 = this.H7;
			num = this.F1(num, num2, num3, num4, this.X[0], 11);
			num4 = this.F1(num4, num, num2, num3, this.X[1], 14);
			num3 = this.F1(num3, num4, num, num2, this.X[2], 15);
			num2 = this.F1(num2, num3, num4, num, this.X[3], 12);
			num = this.F1(num, num2, num3, num4, this.X[4], 5);
			num4 = this.F1(num4, num, num2, num3, this.X[5], 8);
			num3 = this.F1(num3, num4, num, num2, this.X[6], 7);
			num2 = this.F1(num2, num3, num4, num, this.X[7], 9);
			num = this.F1(num, num2, num3, num4, this.X[8], 11);
			num4 = this.F1(num4, num, num2, num3, this.X[9], 13);
			num3 = this.F1(num3, num4, num, num2, this.X[10], 14);
			num2 = this.F1(num2, num3, num4, num, this.X[11], 15);
			num = this.F1(num, num2, num3, num4, this.X[12], 6);
			num4 = this.F1(num4, num, num2, num3, this.X[13], 7);
			num3 = this.F1(num3, num4, num, num2, this.X[14], 9);
			num2 = this.F1(num2, num3, num4, num, this.X[15], 8);
			num5 = this.FF4(num5, num6, num7, num8, this.X[5], 8);
			num8 = this.FF4(num8, num5, num6, num7, this.X[14], 9);
			num7 = this.FF4(num7, num8, num5, num6, this.X[7], 9);
			num6 = this.FF4(num6, num7, num8, num5, this.X[0], 11);
			num5 = this.FF4(num5, num6, num7, num8, this.X[9], 13);
			num8 = this.FF4(num8, num5, num6, num7, this.X[2], 15);
			num7 = this.FF4(num7, num8, num5, num6, this.X[11], 15);
			num6 = this.FF4(num6, num7, num8, num5, this.X[4], 5);
			num5 = this.FF4(num5, num6, num7, num8, this.X[13], 7);
			num8 = this.FF4(num8, num5, num6, num7, this.X[6], 7);
			num7 = this.FF4(num7, num8, num5, num6, this.X[15], 8);
			num6 = this.FF4(num6, num7, num8, num5, this.X[8], 11);
			num5 = this.FF4(num5, num6, num7, num8, this.X[1], 14);
			num8 = this.FF4(num8, num5, num6, num7, this.X[10], 14);
			num7 = this.FF4(num7, num8, num5, num6, this.X[3], 12);
			num6 = this.FF4(num6, num7, num8, num5, this.X[12], 6);
			int num9 = num;
			num = num5;
			num5 = num9;
			num = this.F2(num, num2, num3, num4, this.X[7], 7);
			num4 = this.F2(num4, num, num2, num3, this.X[4], 6);
			num3 = this.F2(num3, num4, num, num2, this.X[13], 8);
			num2 = this.F2(num2, num3, num4, num, this.X[1], 13);
			num = this.F2(num, num2, num3, num4, this.X[10], 11);
			num4 = this.F2(num4, num, num2, num3, this.X[6], 9);
			num3 = this.F2(num3, num4, num, num2, this.X[15], 7);
			num2 = this.F2(num2, num3, num4, num, this.X[3], 15);
			num = this.F2(num, num2, num3, num4, this.X[12], 7);
			num4 = this.F2(num4, num, num2, num3, this.X[0], 12);
			num3 = this.F2(num3, num4, num, num2, this.X[9], 15);
			num2 = this.F2(num2, num3, num4, num, this.X[5], 9);
			num = this.F2(num, num2, num3, num4, this.X[2], 11);
			num4 = this.F2(num4, num, num2, num3, this.X[14], 7);
			num3 = this.F2(num3, num4, num, num2, this.X[11], 13);
			num2 = this.F2(num2, num3, num4, num, this.X[8], 12);
			num5 = this.FF3(num5, num6, num7, num8, this.X[6], 9);
			num8 = this.FF3(num8, num5, num6, num7, this.X[11], 13);
			num7 = this.FF3(num7, num8, num5, num6, this.X[3], 15);
			num6 = this.FF3(num6, num7, num8, num5, this.X[7], 7);
			num5 = this.FF3(num5, num6, num7, num8, this.X[0], 12);
			num8 = this.FF3(num8, num5, num6, num7, this.X[13], 8);
			num7 = this.FF3(num7, num8, num5, num6, this.X[5], 9);
			num6 = this.FF3(num6, num7, num8, num5, this.X[10], 11);
			num5 = this.FF3(num5, num6, num7, num8, this.X[14], 7);
			num8 = this.FF3(num8, num5, num6, num7, this.X[15], 7);
			num7 = this.FF3(num7, num8, num5, num6, this.X[8], 12);
			num6 = this.FF3(num6, num7, num8, num5, this.X[12], 7);
			num5 = this.FF3(num5, num6, num7, num8, this.X[4], 6);
			num8 = this.FF3(num8, num5, num6, num7, this.X[9], 15);
			num7 = this.FF3(num7, num8, num5, num6, this.X[1], 13);
			num6 = this.FF3(num6, num7, num8, num5, this.X[2], 11);
			num9 = num2;
			num2 = num6;
			num6 = num9;
			num = this.F3(num, num2, num3, num4, this.X[3], 11);
			num4 = this.F3(num4, num, num2, num3, this.X[10], 13);
			num3 = this.F3(num3, num4, num, num2, this.X[14], 6);
			num2 = this.F3(num2, num3, num4, num, this.X[4], 7);
			num = this.F3(num, num2, num3, num4, this.X[9], 14);
			num4 = this.F3(num4, num, num2, num3, this.X[15], 9);
			num3 = this.F3(num3, num4, num, num2, this.X[8], 13);
			num2 = this.F3(num2, num3, num4, num, this.X[1], 15);
			num = this.F3(num, num2, num3, num4, this.X[2], 14);
			num4 = this.F3(num4, num, num2, num3, this.X[7], 8);
			num3 = this.F3(num3, num4, num, num2, this.X[0], 13);
			num2 = this.F3(num2, num3, num4, num, this.X[6], 6);
			num = this.F3(num, num2, num3, num4, this.X[13], 5);
			num4 = this.F3(num4, num, num2, num3, this.X[11], 12);
			num3 = this.F3(num3, num4, num, num2, this.X[5], 7);
			num2 = this.F3(num2, num3, num4, num, this.X[12], 5);
			num5 = this.FF2(num5, num6, num7, num8, this.X[15], 9);
			num8 = this.FF2(num8, num5, num6, num7, this.X[5], 7);
			num7 = this.FF2(num7, num8, num5, num6, this.X[1], 15);
			num6 = this.FF2(num6, num7, num8, num5, this.X[3], 11);
			num5 = this.FF2(num5, num6, num7, num8, this.X[7], 8);
			num8 = this.FF2(num8, num5, num6, num7, this.X[14], 6);
			num7 = this.FF2(num7, num8, num5, num6, this.X[6], 6);
			num6 = this.FF2(num6, num7, num8, num5, this.X[9], 14);
			num5 = this.FF2(num5, num6, num7, num8, this.X[11], 12);
			num8 = this.FF2(num8, num5, num6, num7, this.X[8], 13);
			num7 = this.FF2(num7, num8, num5, num6, this.X[12], 5);
			num6 = this.FF2(num6, num7, num8, num5, this.X[2], 14);
			num5 = this.FF2(num5, num6, num7, num8, this.X[10], 13);
			num8 = this.FF2(num8, num5, num6, num7, this.X[0], 13);
			num7 = this.FF2(num7, num8, num5, num6, this.X[4], 7);
			num6 = this.FF2(num6, num7, num8, num5, this.X[13], 5);
			num9 = num3;
			num3 = num7;
			num7 = num9;
			num = this.F4(num, num2, num3, num4, this.X[1], 11);
			num4 = this.F4(num4, num, num2, num3, this.X[9], 12);
			num3 = this.F4(num3, num4, num, num2, this.X[11], 14);
			num2 = this.F4(num2, num3, num4, num, this.X[10], 15);
			num = this.F4(num, num2, num3, num4, this.X[0], 14);
			num4 = this.F4(num4, num, num2, num3, this.X[8], 15);
			num3 = this.F4(num3, num4, num, num2, this.X[12], 9);
			num2 = this.F4(num2, num3, num4, num, this.X[4], 8);
			num = this.F4(num, num2, num3, num4, this.X[13], 9);
			num4 = this.F4(num4, num, num2, num3, this.X[3], 14);
			num3 = this.F4(num3, num4, num, num2, this.X[7], 5);
			num2 = this.F4(num2, num3, num4, num, this.X[15], 6);
			num = this.F4(num, num2, num3, num4, this.X[14], 8);
			num4 = this.F4(num4, num, num2, num3, this.X[5], 6);
			num3 = this.F4(num3, num4, num, num2, this.X[6], 5);
			num2 = this.F4(num2, num3, num4, num, this.X[2], 12);
			num5 = this.FF1(num5, num6, num7, num8, this.X[8], 15);
			num8 = this.FF1(num8, num5, num6, num7, this.X[6], 5);
			num7 = this.FF1(num7, num8, num5, num6, this.X[4], 8);
			num6 = this.FF1(num6, num7, num8, num5, this.X[1], 11);
			num5 = this.FF1(num5, num6, num7, num8, this.X[3], 14);
			num8 = this.FF1(num8, num5, num6, num7, this.X[11], 14);
			num7 = this.FF1(num7, num8, num5, num6, this.X[15], 6);
			num6 = this.FF1(num6, num7, num8, num5, this.X[0], 14);
			num5 = this.FF1(num5, num6, num7, num8, this.X[5], 6);
			num8 = this.FF1(num8, num5, num6, num7, this.X[12], 9);
			num7 = this.FF1(num7, num8, num5, num6, this.X[2], 12);
			num6 = this.FF1(num6, num7, num8, num5, this.X[13], 9);
			num5 = this.FF1(num5, num6, num7, num8, this.X[9], 12);
			num8 = this.FF1(num8, num5, num6, num7, this.X[7], 5);
			num7 = this.FF1(num7, num8, num5, num6, this.X[10], 15);
			num6 = this.FF1(num6, num7, num8, num5, this.X[14], 8);
			num9 = num4;
			num4 = num8;
			num8 = num9;
			this.H0 += num;
			this.H1 += num2;
			this.H2 += num3;
			this.H3 += num4;
			this.H4 += num5;
			this.H5 += num6;
			this.H6 += num7;
			this.H7 += num8;
			this.xOff = 0;
			for (int num10 = 0; num10 != this.X.Length; num10++)
			{
				this.X[num10] = 0;
			}
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x0008CC5C File Offset: 0x0008CC5C
		public override IMemoable Copy()
		{
			return new RipeMD256Digest(this);
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x0008CC64 File Offset: 0x0008CC64
		public override void Reset(IMemoable other)
		{
			RipeMD256Digest t = (RipeMD256Digest)other;
			this.CopyIn(t);
		}

		// Token: 0x0400118C RID: 4492
		private const int DigestLength = 32;

		// Token: 0x0400118D RID: 4493
		private int H0;

		// Token: 0x0400118E RID: 4494
		private int H1;

		// Token: 0x0400118F RID: 4495
		private int H2;

		// Token: 0x04001190 RID: 4496
		private int H3;

		// Token: 0x04001191 RID: 4497
		private int H4;

		// Token: 0x04001192 RID: 4498
		private int H5;

		// Token: 0x04001193 RID: 4499
		private int H6;

		// Token: 0x04001194 RID: 4500
		private int H7;

		// Token: 0x04001195 RID: 4501
		private int[] X = new int[16];

		// Token: 0x04001196 RID: 4502
		private int xOff;
	}
}
