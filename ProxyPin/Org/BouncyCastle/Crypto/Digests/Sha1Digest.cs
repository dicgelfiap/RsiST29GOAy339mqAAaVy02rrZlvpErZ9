using System;
using Org.BouncyCastle.Crypto.Utilities;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x02000361 RID: 865
	public class Sha1Digest : GeneralDigest
	{
		// Token: 0x06001A71 RID: 6769 RVA: 0x0008F114 File Offset: 0x0008F114
		public Sha1Digest()
		{
			this.Reset();
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x0008F130 File Offset: 0x0008F130
		public Sha1Digest(Sha1Digest t) : base(t)
		{
			this.CopyIn(t);
		}

		// Token: 0x06001A73 RID: 6771 RVA: 0x0008F150 File Offset: 0x0008F150
		private void CopyIn(Sha1Digest t)
		{
			base.CopyIn(t);
			this.H1 = t.H1;
			this.H2 = t.H2;
			this.H3 = t.H3;
			this.H4 = t.H4;
			this.H5 = t.H5;
			Array.Copy(t.X, 0, this.X, 0, t.X.Length);
			this.xOff = t.xOff;
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06001A74 RID: 6772 RVA: 0x0008F1CC File Offset: 0x0008F1CC
		public override string AlgorithmName
		{
			get
			{
				return "SHA-1";
			}
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x0008F1D4 File Offset: 0x0008F1D4
		public override int GetDigestSize()
		{
			return 20;
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x0008F1D8 File Offset: 0x0008F1D8
		internal override void ProcessWord(byte[] input, int inOff)
		{
			this.X[this.xOff] = Pack.BE_To_UInt32(input, inOff);
			if (++this.xOff == 16)
			{
				this.ProcessBlock();
			}
		}

		// Token: 0x06001A77 RID: 6775 RVA: 0x0008F21C File Offset: 0x0008F21C
		internal override void ProcessLength(long bitLength)
		{
			if (this.xOff > 14)
			{
				this.ProcessBlock();
			}
			this.X[14] = (uint)((ulong)bitLength >> 32);
			this.X[15] = (uint)bitLength;
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x0008F24C File Offset: 0x0008F24C
		public override int DoFinal(byte[] output, int outOff)
		{
			base.Finish();
			Pack.UInt32_To_BE(this.H1, output, outOff);
			Pack.UInt32_To_BE(this.H2, output, outOff + 4);
			Pack.UInt32_To_BE(this.H3, output, outOff + 8);
			Pack.UInt32_To_BE(this.H4, output, outOff + 12);
			Pack.UInt32_To_BE(this.H5, output, outOff + 16);
			this.Reset();
			return 20;
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x0008F2B8 File Offset: 0x0008F2B8
		public override void Reset()
		{
			base.Reset();
			this.H1 = 1732584193U;
			this.H2 = 4023233417U;
			this.H3 = 2562383102U;
			this.H4 = 271733878U;
			this.H5 = 3285377520U;
			this.xOff = 0;
			Array.Clear(this.X, 0, this.X.Length);
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x0008F324 File Offset: 0x0008F324
		private static uint F(uint u, uint v, uint w)
		{
			return (u & v) | (~u & w);
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x0008F330 File Offset: 0x0008F330
		private static uint H(uint u, uint v, uint w)
		{
			return u ^ v ^ w;
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x0008F338 File Offset: 0x0008F338
		private static uint G(uint u, uint v, uint w)
		{
			return (u & v) | (u & w) | (v & w);
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x0008F348 File Offset: 0x0008F348
		internal override void ProcessBlock()
		{
			for (int i = 16; i < 80; i++)
			{
				uint num = this.X[i - 3] ^ this.X[i - 8] ^ this.X[i - 14] ^ this.X[i - 16];
				this.X[i] = (num << 1 | num >> 31);
			}
			uint num2 = this.H1;
			uint num3 = this.H2;
			uint num4 = this.H3;
			uint num5 = this.H4;
			uint num6 = this.H5;
			int num7 = 0;
			for (int j = 0; j < 4; j++)
			{
				num6 += (num2 << 5 | num2 >> 27) + Sha1Digest.F(num3, num4, num5) + this.X[num7++] + 1518500249U;
				num3 = (num3 << 30 | num3 >> 2);
				num5 += (num6 << 5 | num6 >> 27) + Sha1Digest.F(num2, num3, num4) + this.X[num7++] + 1518500249U;
				num2 = (num2 << 30 | num2 >> 2);
				num4 += (num5 << 5 | num5 >> 27) + Sha1Digest.F(num6, num2, num3) + this.X[num7++] + 1518500249U;
				num6 = (num6 << 30 | num6 >> 2);
				num3 += (num4 << 5 | num4 >> 27) + Sha1Digest.F(num5, num6, num2) + this.X[num7++] + 1518500249U;
				num5 = (num5 << 30 | num5 >> 2);
				num2 += (num3 << 5 | num3 >> 27) + Sha1Digest.F(num4, num5, num6) + this.X[num7++] + 1518500249U;
				num4 = (num4 << 30 | num4 >> 2);
			}
			for (int k = 0; k < 4; k++)
			{
				num6 += (num2 << 5 | num2 >> 27) + Sha1Digest.H(num3, num4, num5) + this.X[num7++] + 1859775393U;
				num3 = (num3 << 30 | num3 >> 2);
				num5 += (num6 << 5 | num6 >> 27) + Sha1Digest.H(num2, num3, num4) + this.X[num7++] + 1859775393U;
				num2 = (num2 << 30 | num2 >> 2);
				num4 += (num5 << 5 | num5 >> 27) + Sha1Digest.H(num6, num2, num3) + this.X[num7++] + 1859775393U;
				num6 = (num6 << 30 | num6 >> 2);
				num3 += (num4 << 5 | num4 >> 27) + Sha1Digest.H(num5, num6, num2) + this.X[num7++] + 1859775393U;
				num5 = (num5 << 30 | num5 >> 2);
				num2 += (num3 << 5 | num3 >> 27) + Sha1Digest.H(num4, num5, num6) + this.X[num7++] + 1859775393U;
				num4 = (num4 << 30 | num4 >> 2);
			}
			for (int l = 0; l < 4; l++)
			{
				num6 += (num2 << 5 | num2 >> 27) + Sha1Digest.G(num3, num4, num5) + this.X[num7++] + 2400959708U;
				num3 = (num3 << 30 | num3 >> 2);
				num5 += (num6 << 5 | num6 >> 27) + Sha1Digest.G(num2, num3, num4) + this.X[num7++] + 2400959708U;
				num2 = (num2 << 30 | num2 >> 2);
				num4 += (num5 << 5 | num5 >> 27) + Sha1Digest.G(num6, num2, num3) + this.X[num7++] + 2400959708U;
				num6 = (num6 << 30 | num6 >> 2);
				num3 += (num4 << 5 | num4 >> 27) + Sha1Digest.G(num5, num6, num2) + this.X[num7++] + 2400959708U;
				num5 = (num5 << 30 | num5 >> 2);
				num2 += (num3 << 5 | num3 >> 27) + Sha1Digest.G(num4, num5, num6) + this.X[num7++] + 2400959708U;
				num4 = (num4 << 30 | num4 >> 2);
			}
			for (int m = 0; m < 4; m++)
			{
				num6 += (num2 << 5 | num2 >> 27) + Sha1Digest.H(num3, num4, num5) + this.X[num7++] + 3395469782U;
				num3 = (num3 << 30 | num3 >> 2);
				num5 += (num6 << 5 | num6 >> 27) + Sha1Digest.H(num2, num3, num4) + this.X[num7++] + 3395469782U;
				num2 = (num2 << 30 | num2 >> 2);
				num4 += (num5 << 5 | num5 >> 27) + Sha1Digest.H(num6, num2, num3) + this.X[num7++] + 3395469782U;
				num6 = (num6 << 30 | num6 >> 2);
				num3 += (num4 << 5 | num4 >> 27) + Sha1Digest.H(num5, num6, num2) + this.X[num7++] + 3395469782U;
				num5 = (num5 << 30 | num5 >> 2);
				num2 += (num3 << 5 | num3 >> 27) + Sha1Digest.H(num4, num5, num6) + this.X[num7++] + 3395469782U;
				num4 = (num4 << 30 | num4 >> 2);
			}
			this.H1 += num2;
			this.H2 += num3;
			this.H3 += num4;
			this.H4 += num5;
			this.H5 += num6;
			this.xOff = 0;
			Array.Clear(this.X, 0, 16);
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x0008F8E8 File Offset: 0x0008F8E8
		public override IMemoable Copy()
		{
			return new Sha1Digest(this);
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x0008F8F0 File Offset: 0x0008F8F0
		public override void Reset(IMemoable other)
		{
			Sha1Digest t = (Sha1Digest)other;
			this.CopyIn(t);
		}

		// Token: 0x040011A4 RID: 4516
		private const int DigestLength = 20;

		// Token: 0x040011A5 RID: 4517
		private const uint Y1 = 1518500249U;

		// Token: 0x040011A6 RID: 4518
		private const uint Y2 = 1859775393U;

		// Token: 0x040011A7 RID: 4519
		private const uint Y3 = 2400959708U;

		// Token: 0x040011A8 RID: 4520
		private const uint Y4 = 3395469782U;

		// Token: 0x040011A9 RID: 4521
		private uint H1;

		// Token: 0x040011AA RID: 4522
		private uint H2;

		// Token: 0x040011AB RID: 4523
		private uint H3;

		// Token: 0x040011AC RID: 4524
		private uint H4;

		// Token: 0x040011AD RID: 4525
		private uint H5;

		// Token: 0x040011AE RID: 4526
		private uint[] X = new uint[80];

		// Token: 0x040011AF RID: 4527
		private int xOff;
	}
}
