using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x0200035D RID: 861
	public class RipeMD128Digest : GeneralDigest
	{
		// Token: 0x06001A17 RID: 6679 RVA: 0x000887EC File Offset: 0x000887EC
		public RipeMD128Digest()
		{
			this.Reset();
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x00088808 File Offset: 0x00088808
		public RipeMD128Digest(RipeMD128Digest t) : base(t)
		{
			this.CopyIn(t);
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x00088828 File Offset: 0x00088828
		private void CopyIn(RipeMD128Digest t)
		{
			base.CopyIn(t);
			this.H0 = t.H0;
			this.H1 = t.H1;
			this.H2 = t.H2;
			this.H3 = t.H3;
			Array.Copy(t.X, 0, this.X, 0, t.X.Length);
			this.xOff = t.xOff;
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06001A1A RID: 6682 RVA: 0x00088898 File Offset: 0x00088898
		public override string AlgorithmName
		{
			get
			{
				return "RIPEMD128";
			}
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x000888A0 File Offset: 0x000888A0
		public override int GetDigestSize()
		{
			return 16;
		}

		// Token: 0x06001A1C RID: 6684 RVA: 0x000888A4 File Offset: 0x000888A4
		internal override void ProcessWord(byte[] input, int inOff)
		{
			this.X[this.xOff++] = ((int)(input[inOff] & byte.MaxValue) | (int)(input[inOff + 1] & byte.MaxValue) << 8 | (int)(input[inOff + 2] & byte.MaxValue) << 16 | (int)(input[inOff + 3] & byte.MaxValue) << 24);
			if (this.xOff == 16)
			{
				this.ProcessBlock();
			}
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x00088918 File Offset: 0x00088918
		internal override void ProcessLength(long bitLength)
		{
			if (this.xOff > 14)
			{
				this.ProcessBlock();
			}
			this.X[14] = (int)(bitLength & (long)((ulong)-1));
			this.X[15] = (int)((ulong)bitLength >> 32);
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x0008894C File Offset: 0x0008894C
		private void UnpackWord(int word, byte[] outBytes, int outOff)
		{
			outBytes[outOff] = (byte)word;
			outBytes[outOff + 1] = (byte)((uint)word >> 8);
			outBytes[outOff + 2] = (byte)((uint)word >> 16);
			outBytes[outOff + 3] = (byte)((uint)word >> 24);
		}

		// Token: 0x06001A1F RID: 6687 RVA: 0x00088970 File Offset: 0x00088970
		public override int DoFinal(byte[] output, int outOff)
		{
			base.Finish();
			this.UnpackWord(this.H0, output, outOff);
			this.UnpackWord(this.H1, output, outOff + 4);
			this.UnpackWord(this.H2, output, outOff + 8);
			this.UnpackWord(this.H3, output, outOff + 12);
			this.Reset();
			return 16;
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x000889D0 File Offset: 0x000889D0
		public override void Reset()
		{
			base.Reset();
			this.H0 = 1732584193;
			this.H1 = -271733879;
			this.H2 = -1732584194;
			this.H3 = 271733878;
			this.xOff = 0;
			for (int num = 0; num != this.X.Length; num++)
			{
				this.X[num] = 0;
			}
		}

		// Token: 0x06001A21 RID: 6689 RVA: 0x00088A3C File Offset: 0x00088A3C
		private int RL(int x, int n)
		{
			return x << n | (int)((uint)x >> 32 - n);
		}

		// Token: 0x06001A22 RID: 6690 RVA: 0x00088A50 File Offset: 0x00088A50
		private int F1(int x, int y, int z)
		{
			return x ^ y ^ z;
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x00088A58 File Offset: 0x00088A58
		private int F2(int x, int y, int z)
		{
			return (x & y) | (~x & z);
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x00088A64 File Offset: 0x00088A64
		private int F3(int x, int y, int z)
		{
			return (x | ~y) ^ z;
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x00088A6C File Offset: 0x00088A6C
		private int F4(int x, int y, int z)
		{
			return (x & z) | (y & ~z);
		}

		// Token: 0x06001A26 RID: 6694 RVA: 0x00088A78 File Offset: 0x00088A78
		private int F1(int a, int b, int c, int d, int x, int s)
		{
			return this.RL(a + this.F1(b, c, d) + x, s);
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x00088A94 File Offset: 0x00088A94
		private int F2(int a, int b, int c, int d, int x, int s)
		{
			return this.RL(a + this.F2(b, c, d) + x + 1518500249, s);
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x00088AB4 File Offset: 0x00088AB4
		private int F3(int a, int b, int c, int d, int x, int s)
		{
			return this.RL(a + this.F3(b, c, d) + x + 1859775393, s);
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x00088AD4 File Offset: 0x00088AD4
		private int F4(int a, int b, int c, int d, int x, int s)
		{
			return this.RL(a + this.F4(b, c, d) + x + -1894007588, s);
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x00088AF4 File Offset: 0x00088AF4
		private int FF1(int a, int b, int c, int d, int x, int s)
		{
			return this.RL(a + this.F1(b, c, d) + x, s);
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x00088B10 File Offset: 0x00088B10
		private int FF2(int a, int b, int c, int d, int x, int s)
		{
			return this.RL(a + this.F2(b, c, d) + x + 1836072691, s);
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x00088B30 File Offset: 0x00088B30
		private int FF3(int a, int b, int c, int d, int x, int s)
		{
			return this.RL(a + this.F3(b, c, d) + x + 1548603684, s);
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x00088B50 File Offset: 0x00088B50
		private int FF4(int a, int b, int c, int d, int x, int s)
		{
			return this.RL(a + this.F4(b, c, d) + x + 1352829926, s);
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x00088B70 File Offset: 0x00088B70
		internal override void ProcessBlock()
		{
			int num2;
			int num = num2 = this.H0;
			int num4;
			int num3 = num4 = this.H1;
			int num6;
			int num5 = num6 = this.H2;
			int num8;
			int num7 = num8 = this.H3;
			num2 = this.F1(num2, num4, num6, num8, this.X[0], 11);
			num8 = this.F1(num8, num2, num4, num6, this.X[1], 14);
			num6 = this.F1(num6, num8, num2, num4, this.X[2], 15);
			num4 = this.F1(num4, num6, num8, num2, this.X[3], 12);
			num2 = this.F1(num2, num4, num6, num8, this.X[4], 5);
			num8 = this.F1(num8, num2, num4, num6, this.X[5], 8);
			num6 = this.F1(num6, num8, num2, num4, this.X[6], 7);
			num4 = this.F1(num4, num6, num8, num2, this.X[7], 9);
			num2 = this.F1(num2, num4, num6, num8, this.X[8], 11);
			num8 = this.F1(num8, num2, num4, num6, this.X[9], 13);
			num6 = this.F1(num6, num8, num2, num4, this.X[10], 14);
			num4 = this.F1(num4, num6, num8, num2, this.X[11], 15);
			num2 = this.F1(num2, num4, num6, num8, this.X[12], 6);
			num8 = this.F1(num8, num2, num4, num6, this.X[13], 7);
			num6 = this.F1(num6, num8, num2, num4, this.X[14], 9);
			num4 = this.F1(num4, num6, num8, num2, this.X[15], 8);
			num2 = this.F2(num2, num4, num6, num8, this.X[7], 7);
			num8 = this.F2(num8, num2, num4, num6, this.X[4], 6);
			num6 = this.F2(num6, num8, num2, num4, this.X[13], 8);
			num4 = this.F2(num4, num6, num8, num2, this.X[1], 13);
			num2 = this.F2(num2, num4, num6, num8, this.X[10], 11);
			num8 = this.F2(num8, num2, num4, num6, this.X[6], 9);
			num6 = this.F2(num6, num8, num2, num4, this.X[15], 7);
			num4 = this.F2(num4, num6, num8, num2, this.X[3], 15);
			num2 = this.F2(num2, num4, num6, num8, this.X[12], 7);
			num8 = this.F2(num8, num2, num4, num6, this.X[0], 12);
			num6 = this.F2(num6, num8, num2, num4, this.X[9], 15);
			num4 = this.F2(num4, num6, num8, num2, this.X[5], 9);
			num2 = this.F2(num2, num4, num6, num8, this.X[2], 11);
			num8 = this.F2(num8, num2, num4, num6, this.X[14], 7);
			num6 = this.F2(num6, num8, num2, num4, this.X[11], 13);
			num4 = this.F2(num4, num6, num8, num2, this.X[8], 12);
			num2 = this.F3(num2, num4, num6, num8, this.X[3], 11);
			num8 = this.F3(num8, num2, num4, num6, this.X[10], 13);
			num6 = this.F3(num6, num8, num2, num4, this.X[14], 6);
			num4 = this.F3(num4, num6, num8, num2, this.X[4], 7);
			num2 = this.F3(num2, num4, num6, num8, this.X[9], 14);
			num8 = this.F3(num8, num2, num4, num6, this.X[15], 9);
			num6 = this.F3(num6, num8, num2, num4, this.X[8], 13);
			num4 = this.F3(num4, num6, num8, num2, this.X[1], 15);
			num2 = this.F3(num2, num4, num6, num8, this.X[2], 14);
			num8 = this.F3(num8, num2, num4, num6, this.X[7], 8);
			num6 = this.F3(num6, num8, num2, num4, this.X[0], 13);
			num4 = this.F3(num4, num6, num8, num2, this.X[6], 6);
			num2 = this.F3(num2, num4, num6, num8, this.X[13], 5);
			num8 = this.F3(num8, num2, num4, num6, this.X[11], 12);
			num6 = this.F3(num6, num8, num2, num4, this.X[5], 7);
			num4 = this.F3(num4, num6, num8, num2, this.X[12], 5);
			num2 = this.F4(num2, num4, num6, num8, this.X[1], 11);
			num8 = this.F4(num8, num2, num4, num6, this.X[9], 12);
			num6 = this.F4(num6, num8, num2, num4, this.X[11], 14);
			num4 = this.F4(num4, num6, num8, num2, this.X[10], 15);
			num2 = this.F4(num2, num4, num6, num8, this.X[0], 14);
			num8 = this.F4(num8, num2, num4, num6, this.X[8], 15);
			num6 = this.F4(num6, num8, num2, num4, this.X[12], 9);
			num4 = this.F4(num4, num6, num8, num2, this.X[4], 8);
			num2 = this.F4(num2, num4, num6, num8, this.X[13], 9);
			num8 = this.F4(num8, num2, num4, num6, this.X[3], 14);
			num6 = this.F4(num6, num8, num2, num4, this.X[7], 5);
			num4 = this.F4(num4, num6, num8, num2, this.X[15], 6);
			num2 = this.F4(num2, num4, num6, num8, this.X[14], 8);
			num8 = this.F4(num8, num2, num4, num6, this.X[5], 6);
			num6 = this.F4(num6, num8, num2, num4, this.X[6], 5);
			num4 = this.F4(num4, num6, num8, num2, this.X[2], 12);
			num = this.FF4(num, num3, num5, num7, this.X[5], 8);
			num7 = this.FF4(num7, num, num3, num5, this.X[14], 9);
			num5 = this.FF4(num5, num7, num, num3, this.X[7], 9);
			num3 = this.FF4(num3, num5, num7, num, this.X[0], 11);
			num = this.FF4(num, num3, num5, num7, this.X[9], 13);
			num7 = this.FF4(num7, num, num3, num5, this.X[2], 15);
			num5 = this.FF4(num5, num7, num, num3, this.X[11], 15);
			num3 = this.FF4(num3, num5, num7, num, this.X[4], 5);
			num = this.FF4(num, num3, num5, num7, this.X[13], 7);
			num7 = this.FF4(num7, num, num3, num5, this.X[6], 7);
			num5 = this.FF4(num5, num7, num, num3, this.X[15], 8);
			num3 = this.FF4(num3, num5, num7, num, this.X[8], 11);
			num = this.FF4(num, num3, num5, num7, this.X[1], 14);
			num7 = this.FF4(num7, num, num3, num5, this.X[10], 14);
			num5 = this.FF4(num5, num7, num, num3, this.X[3], 12);
			num3 = this.FF4(num3, num5, num7, num, this.X[12], 6);
			num = this.FF3(num, num3, num5, num7, this.X[6], 9);
			num7 = this.FF3(num7, num, num3, num5, this.X[11], 13);
			num5 = this.FF3(num5, num7, num, num3, this.X[3], 15);
			num3 = this.FF3(num3, num5, num7, num, this.X[7], 7);
			num = this.FF3(num, num3, num5, num7, this.X[0], 12);
			num7 = this.FF3(num7, num, num3, num5, this.X[13], 8);
			num5 = this.FF3(num5, num7, num, num3, this.X[5], 9);
			num3 = this.FF3(num3, num5, num7, num, this.X[10], 11);
			num = this.FF3(num, num3, num5, num7, this.X[14], 7);
			num7 = this.FF3(num7, num, num3, num5, this.X[15], 7);
			num5 = this.FF3(num5, num7, num, num3, this.X[8], 12);
			num3 = this.FF3(num3, num5, num7, num, this.X[12], 7);
			num = this.FF3(num, num3, num5, num7, this.X[4], 6);
			num7 = this.FF3(num7, num, num3, num5, this.X[9], 15);
			num5 = this.FF3(num5, num7, num, num3, this.X[1], 13);
			num3 = this.FF3(num3, num5, num7, num, this.X[2], 11);
			num = this.FF2(num, num3, num5, num7, this.X[15], 9);
			num7 = this.FF2(num7, num, num3, num5, this.X[5], 7);
			num5 = this.FF2(num5, num7, num, num3, this.X[1], 15);
			num3 = this.FF2(num3, num5, num7, num, this.X[3], 11);
			num = this.FF2(num, num3, num5, num7, this.X[7], 8);
			num7 = this.FF2(num7, num, num3, num5, this.X[14], 6);
			num5 = this.FF2(num5, num7, num, num3, this.X[6], 6);
			num3 = this.FF2(num3, num5, num7, num, this.X[9], 14);
			num = this.FF2(num, num3, num5, num7, this.X[11], 12);
			num7 = this.FF2(num7, num, num3, num5, this.X[8], 13);
			num5 = this.FF2(num5, num7, num, num3, this.X[12], 5);
			num3 = this.FF2(num3, num5, num7, num, this.X[2], 14);
			num = this.FF2(num, num3, num5, num7, this.X[10], 13);
			num7 = this.FF2(num7, num, num3, num5, this.X[0], 13);
			num5 = this.FF2(num5, num7, num, num3, this.X[4], 7);
			num3 = this.FF2(num3, num5, num7, num, this.X[13], 5);
			num = this.FF1(num, num3, num5, num7, this.X[8], 15);
			num7 = this.FF1(num7, num, num3, num5, this.X[6], 5);
			num5 = this.FF1(num5, num7, num, num3, this.X[4], 8);
			num3 = this.FF1(num3, num5, num7, num, this.X[1], 11);
			num = this.FF1(num, num3, num5, num7, this.X[3], 14);
			num7 = this.FF1(num7, num, num3, num5, this.X[11], 14);
			num5 = this.FF1(num5, num7, num, num3, this.X[15], 6);
			num3 = this.FF1(num3, num5, num7, num, this.X[0], 14);
			num = this.FF1(num, num3, num5, num7, this.X[5], 6);
			num7 = this.FF1(num7, num, num3, num5, this.X[12], 9);
			num5 = this.FF1(num5, num7, num, num3, this.X[2], 12);
			num3 = this.FF1(num3, num5, num7, num, this.X[13], 9);
			num = this.FF1(num, num3, num5, num7, this.X[9], 12);
			num7 = this.FF1(num7, num, num3, num5, this.X[7], 5);
			num5 = this.FF1(num5, num7, num, num3, this.X[10], 15);
			num3 = this.FF1(num3, num5, num7, num, this.X[14], 8);
			num7 += num6 + this.H1;
			this.H1 = this.H2 + num8 + num;
			this.H2 = this.H3 + num2 + num3;
			this.H3 = this.H0 + num4 + num5;
			this.H0 = num7;
			this.xOff = 0;
			for (int num9 = 0; num9 != this.X.Length; num9++)
			{
				this.X[num9] = 0;
			}
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x000897E4 File Offset: 0x000897E4
		public override IMemoable Copy()
		{
			return new RipeMD128Digest(this);
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x000897EC File Offset: 0x000897EC
		public override void Reset(IMemoable other)
		{
			RipeMD128Digest t = (RipeMD128Digest)other;
			this.CopyIn(t);
		}

		// Token: 0x0400117D RID: 4477
		private const int DigestLength = 16;

		// Token: 0x0400117E RID: 4478
		private int H0;

		// Token: 0x0400117F RID: 4479
		private int H1;

		// Token: 0x04001180 RID: 4480
		private int H2;

		// Token: 0x04001181 RID: 4481
		private int H3;

		// Token: 0x04001182 RID: 4482
		private int[] X = new int[16];

		// Token: 0x04001183 RID: 4483
		private int xOff;
	}
}
