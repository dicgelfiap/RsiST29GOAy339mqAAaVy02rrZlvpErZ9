using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x02000359 RID: 857
	public class MD4Digest : GeneralDigest
	{
		// Token: 0x060019E4 RID: 6628 RVA: 0x0008702C File Offset: 0x0008702C
		public MD4Digest()
		{
			this.Reset();
		}

		// Token: 0x060019E5 RID: 6629 RVA: 0x00087048 File Offset: 0x00087048
		public MD4Digest(MD4Digest t) : base(t)
		{
			this.CopyIn(t);
		}

		// Token: 0x060019E6 RID: 6630 RVA: 0x00087068 File Offset: 0x00087068
		private void CopyIn(MD4Digest t)
		{
			base.CopyIn(t);
			this.H1 = t.H1;
			this.H2 = t.H2;
			this.H3 = t.H3;
			this.H4 = t.H4;
			Array.Copy(t.X, 0, this.X, 0, t.X.Length);
			this.xOff = t.xOff;
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x060019E7 RID: 6631 RVA: 0x000870D8 File Offset: 0x000870D8
		public override string AlgorithmName
		{
			get
			{
				return "MD4";
			}
		}

		// Token: 0x060019E8 RID: 6632 RVA: 0x000870E0 File Offset: 0x000870E0
		public override int GetDigestSize()
		{
			return 16;
		}

		// Token: 0x060019E9 RID: 6633 RVA: 0x000870E4 File Offset: 0x000870E4
		internal override void ProcessWord(byte[] input, int inOff)
		{
			this.X[this.xOff++] = ((int)(input[inOff] & byte.MaxValue) | (int)(input[inOff + 1] & byte.MaxValue) << 8 | (int)(input[inOff + 2] & byte.MaxValue) << 16 | (int)(input[inOff + 3] & byte.MaxValue) << 24);
			if (this.xOff == 16)
			{
				this.ProcessBlock();
			}
		}

		// Token: 0x060019EA RID: 6634 RVA: 0x00087158 File Offset: 0x00087158
		internal override void ProcessLength(long bitLength)
		{
			if (this.xOff > 14)
			{
				this.ProcessBlock();
			}
			this.X[14] = (int)(bitLength & (long)((ulong)-1));
			this.X[15] = (int)((ulong)bitLength >> 32);
		}

		// Token: 0x060019EB RID: 6635 RVA: 0x0008718C File Offset: 0x0008718C
		private void UnpackWord(int word, byte[] outBytes, int outOff)
		{
			outBytes[outOff] = (byte)word;
			outBytes[outOff + 1] = (byte)((uint)word >> 8);
			outBytes[outOff + 2] = (byte)((uint)word >> 16);
			outBytes[outOff + 3] = (byte)((uint)word >> 24);
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x000871B0 File Offset: 0x000871B0
		public override int DoFinal(byte[] output, int outOff)
		{
			base.Finish();
			this.UnpackWord(this.H1, output, outOff);
			this.UnpackWord(this.H2, output, outOff + 4);
			this.UnpackWord(this.H3, output, outOff + 8);
			this.UnpackWord(this.H4, output, outOff + 12);
			this.Reset();
			return 16;
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x00087210 File Offset: 0x00087210
		public override void Reset()
		{
			base.Reset();
			this.H1 = 1732584193;
			this.H2 = -271733879;
			this.H3 = -1732584194;
			this.H4 = 271733878;
			this.xOff = 0;
			for (int num = 0; num != this.X.Length; num++)
			{
				this.X[num] = 0;
			}
		}

		// Token: 0x060019EE RID: 6638 RVA: 0x0008727C File Offset: 0x0008727C
		private int RotateLeft(int x, int n)
		{
			return x << n | (int)((uint)x >> 32 - n);
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x00087290 File Offset: 0x00087290
		private int F(int u, int v, int w)
		{
			return (u & v) | (~u & w);
		}

		// Token: 0x060019F0 RID: 6640 RVA: 0x0008729C File Offset: 0x0008729C
		private int G(int u, int v, int w)
		{
			return (u & v) | (u & w) | (v & w);
		}

		// Token: 0x060019F1 RID: 6641 RVA: 0x000872AC File Offset: 0x000872AC
		private int H(int u, int v, int w)
		{
			return u ^ v ^ w;
		}

		// Token: 0x060019F2 RID: 6642 RVA: 0x000872B4 File Offset: 0x000872B4
		internal override void ProcessBlock()
		{
			int num = this.H1;
			int num2 = this.H2;
			int num3 = this.H3;
			int num4 = this.H4;
			num = this.RotateLeft(num + this.F(num2, num3, num4) + this.X[0], 3);
			num4 = this.RotateLeft(num4 + this.F(num, num2, num3) + this.X[1], 7);
			num3 = this.RotateLeft(num3 + this.F(num4, num, num2) + this.X[2], 11);
			num2 = this.RotateLeft(num2 + this.F(num3, num4, num) + this.X[3], 19);
			num = this.RotateLeft(num + this.F(num2, num3, num4) + this.X[4], 3);
			num4 = this.RotateLeft(num4 + this.F(num, num2, num3) + this.X[5], 7);
			num3 = this.RotateLeft(num3 + this.F(num4, num, num2) + this.X[6], 11);
			num2 = this.RotateLeft(num2 + this.F(num3, num4, num) + this.X[7], 19);
			num = this.RotateLeft(num + this.F(num2, num3, num4) + this.X[8], 3);
			num4 = this.RotateLeft(num4 + this.F(num, num2, num3) + this.X[9], 7);
			num3 = this.RotateLeft(num3 + this.F(num4, num, num2) + this.X[10], 11);
			num2 = this.RotateLeft(num2 + this.F(num3, num4, num) + this.X[11], 19);
			num = this.RotateLeft(num + this.F(num2, num3, num4) + this.X[12], 3);
			num4 = this.RotateLeft(num4 + this.F(num, num2, num3) + this.X[13], 7);
			num3 = this.RotateLeft(num3 + this.F(num4, num, num2) + this.X[14], 11);
			num2 = this.RotateLeft(num2 + this.F(num3, num4, num) + this.X[15], 19);
			num = this.RotateLeft(num + this.G(num2, num3, num4) + this.X[0] + 1518500249, 3);
			num4 = this.RotateLeft(num4 + this.G(num, num2, num3) + this.X[4] + 1518500249, 5);
			num3 = this.RotateLeft(num3 + this.G(num4, num, num2) + this.X[8] + 1518500249, 9);
			num2 = this.RotateLeft(num2 + this.G(num3, num4, num) + this.X[12] + 1518500249, 13);
			num = this.RotateLeft(num + this.G(num2, num3, num4) + this.X[1] + 1518500249, 3);
			num4 = this.RotateLeft(num4 + this.G(num, num2, num3) + this.X[5] + 1518500249, 5);
			num3 = this.RotateLeft(num3 + this.G(num4, num, num2) + this.X[9] + 1518500249, 9);
			num2 = this.RotateLeft(num2 + this.G(num3, num4, num) + this.X[13] + 1518500249, 13);
			num = this.RotateLeft(num + this.G(num2, num3, num4) + this.X[2] + 1518500249, 3);
			num4 = this.RotateLeft(num4 + this.G(num, num2, num3) + this.X[6] + 1518500249, 5);
			num3 = this.RotateLeft(num3 + this.G(num4, num, num2) + this.X[10] + 1518500249, 9);
			num2 = this.RotateLeft(num2 + this.G(num3, num4, num) + this.X[14] + 1518500249, 13);
			num = this.RotateLeft(num + this.G(num2, num3, num4) + this.X[3] + 1518500249, 3);
			num4 = this.RotateLeft(num4 + this.G(num, num2, num3) + this.X[7] + 1518500249, 5);
			num3 = this.RotateLeft(num3 + this.G(num4, num, num2) + this.X[11] + 1518500249, 9);
			num2 = this.RotateLeft(num2 + this.G(num3, num4, num) + this.X[15] + 1518500249, 13);
			num = this.RotateLeft(num + this.H(num2, num3, num4) + this.X[0] + 1859775393, 3);
			num4 = this.RotateLeft(num4 + this.H(num, num2, num3) + this.X[8] + 1859775393, 9);
			num3 = this.RotateLeft(num3 + this.H(num4, num, num2) + this.X[4] + 1859775393, 11);
			num2 = this.RotateLeft(num2 + this.H(num3, num4, num) + this.X[12] + 1859775393, 15);
			num = this.RotateLeft(num + this.H(num2, num3, num4) + this.X[2] + 1859775393, 3);
			num4 = this.RotateLeft(num4 + this.H(num, num2, num3) + this.X[10] + 1859775393, 9);
			num3 = this.RotateLeft(num3 + this.H(num4, num, num2) + this.X[6] + 1859775393, 11);
			num2 = this.RotateLeft(num2 + this.H(num3, num4, num) + this.X[14] + 1859775393, 15);
			num = this.RotateLeft(num + this.H(num2, num3, num4) + this.X[1] + 1859775393, 3);
			num4 = this.RotateLeft(num4 + this.H(num, num2, num3) + this.X[9] + 1859775393, 9);
			num3 = this.RotateLeft(num3 + this.H(num4, num, num2) + this.X[5] + 1859775393, 11);
			num2 = this.RotateLeft(num2 + this.H(num3, num4, num) + this.X[13] + 1859775393, 15);
			num = this.RotateLeft(num + this.H(num2, num3, num4) + this.X[3] + 1859775393, 3);
			num4 = this.RotateLeft(num4 + this.H(num, num2, num3) + this.X[11] + 1859775393, 9);
			num3 = this.RotateLeft(num3 + this.H(num4, num, num2) + this.X[7] + 1859775393, 11);
			num2 = this.RotateLeft(num2 + this.H(num3, num4, num) + this.X[15] + 1859775393, 15);
			this.H1 += num;
			this.H2 += num2;
			this.H3 += num3;
			this.H4 += num4;
			this.xOff = 0;
			for (int num5 = 0; num5 != this.X.Length; num5++)
			{
				this.X[num5] = 0;
			}
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x00087978 File Offset: 0x00087978
		public override IMemoable Copy()
		{
			return new MD4Digest(this);
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x00087980 File Offset: 0x00087980
		public override void Reset(IMemoable other)
		{
			MD4Digest t = (MD4Digest)other;
			this.CopyIn(t);
		}

		// Token: 0x04001151 RID: 4433
		private const int DigestLength = 16;

		// Token: 0x04001152 RID: 4434
		private const int S11 = 3;

		// Token: 0x04001153 RID: 4435
		private const int S12 = 7;

		// Token: 0x04001154 RID: 4436
		private const int S13 = 11;

		// Token: 0x04001155 RID: 4437
		private const int S14 = 19;

		// Token: 0x04001156 RID: 4438
		private const int S21 = 3;

		// Token: 0x04001157 RID: 4439
		private const int S22 = 5;

		// Token: 0x04001158 RID: 4440
		private const int S23 = 9;

		// Token: 0x04001159 RID: 4441
		private const int S24 = 13;

		// Token: 0x0400115A RID: 4442
		private const int S31 = 3;

		// Token: 0x0400115B RID: 4443
		private const int S32 = 9;

		// Token: 0x0400115C RID: 4444
		private const int S33 = 11;

		// Token: 0x0400115D RID: 4445
		private const int S34 = 15;

		// Token: 0x0400115E RID: 4446
		private int H1;

		// Token: 0x0400115F RID: 4447
		private int H2;

		// Token: 0x04001160 RID: 4448
		private int H3;

		// Token: 0x04001161 RID: 4449
		private int H4;

		// Token: 0x04001162 RID: 4450
		private int[] X = new int[16];

		// Token: 0x04001163 RID: 4451
		private int xOff;
	}
}
