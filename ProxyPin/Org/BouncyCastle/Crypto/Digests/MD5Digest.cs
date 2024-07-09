using System;
using Org.BouncyCastle.Crypto.Utilities;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x0200035A RID: 858
	public class MD5Digest : GeneralDigest
	{
		// Token: 0x060019F5 RID: 6645 RVA: 0x000879A0 File Offset: 0x000879A0
		public MD5Digest()
		{
			this.Reset();
		}

		// Token: 0x060019F6 RID: 6646 RVA: 0x000879BC File Offset: 0x000879BC
		public MD5Digest(MD5Digest t) : base(t)
		{
			this.CopyIn(t);
		}

		// Token: 0x060019F7 RID: 6647 RVA: 0x000879DC File Offset: 0x000879DC
		private void CopyIn(MD5Digest t)
		{
			base.CopyIn(t);
			this.H1 = t.H1;
			this.H2 = t.H2;
			this.H3 = t.H3;
			this.H4 = t.H4;
			Array.Copy(t.X, 0, this.X, 0, t.X.Length);
			this.xOff = t.xOff;
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x060019F8 RID: 6648 RVA: 0x00087A4C File Offset: 0x00087A4C
		public override string AlgorithmName
		{
			get
			{
				return "MD5";
			}
		}

		// Token: 0x060019F9 RID: 6649 RVA: 0x00087A54 File Offset: 0x00087A54
		public override int GetDigestSize()
		{
			return 16;
		}

		// Token: 0x060019FA RID: 6650 RVA: 0x00087A58 File Offset: 0x00087A58
		internal override void ProcessWord(byte[] input, int inOff)
		{
			this.X[this.xOff] = Pack.LE_To_UInt32(input, inOff);
			if (++this.xOff == 16)
			{
				this.ProcessBlock();
			}
		}

		// Token: 0x060019FB RID: 6651 RVA: 0x00087A9C File Offset: 0x00087A9C
		internal override void ProcessLength(long bitLength)
		{
			if (this.xOff > 14)
			{
				if (this.xOff == 15)
				{
					this.X[15] = 0U;
				}
				this.ProcessBlock();
			}
			for (int i = this.xOff; i < 14; i++)
			{
				this.X[i] = 0U;
			}
			this.X[14] = (uint)bitLength;
			this.X[15] = (uint)((ulong)bitLength >> 32);
		}

		// Token: 0x060019FC RID: 6652 RVA: 0x00087B10 File Offset: 0x00087B10
		public override int DoFinal(byte[] output, int outOff)
		{
			base.Finish();
			Pack.UInt32_To_LE(this.H1, output, outOff);
			Pack.UInt32_To_LE(this.H2, output, outOff + 4);
			Pack.UInt32_To_LE(this.H3, output, outOff + 8);
			Pack.UInt32_To_LE(this.H4, output, outOff + 12);
			this.Reset();
			return 16;
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x00087B6C File Offset: 0x00087B6C
		public override void Reset()
		{
			base.Reset();
			this.H1 = 1732584193U;
			this.H2 = 4023233417U;
			this.H3 = 2562383102U;
			this.H4 = 271733878U;
			this.xOff = 0;
			for (int num = 0; num != this.X.Length; num++)
			{
				this.X[num] = 0U;
			}
		}

		// Token: 0x060019FE RID: 6654 RVA: 0x00087BD8 File Offset: 0x00087BD8
		private static uint RotateLeft(uint x, int n)
		{
			return x << n | x >> 32 - n;
		}

		// Token: 0x060019FF RID: 6655 RVA: 0x00087BEC File Offset: 0x00087BEC
		private static uint F(uint u, uint v, uint w)
		{
			return (u & v) | (~u & w);
		}

		// Token: 0x06001A00 RID: 6656 RVA: 0x00087BF8 File Offset: 0x00087BF8
		private static uint G(uint u, uint v, uint w)
		{
			return (u & w) | (v & ~w);
		}

		// Token: 0x06001A01 RID: 6657 RVA: 0x00087C04 File Offset: 0x00087C04
		private static uint H(uint u, uint v, uint w)
		{
			return u ^ v ^ w;
		}

		// Token: 0x06001A02 RID: 6658 RVA: 0x00087C0C File Offset: 0x00087C0C
		private static uint K(uint u, uint v, uint w)
		{
			return v ^ (u | ~w);
		}

		// Token: 0x06001A03 RID: 6659 RVA: 0x00087C14 File Offset: 0x00087C14
		internal override void ProcessBlock()
		{
			uint num = this.H1;
			uint num2 = this.H2;
			uint num3 = this.H3;
			uint num4 = this.H4;
			num = MD5Digest.RotateLeft(num + MD5Digest.F(num2, num3, num4) + this.X[0] + 3614090360U, MD5Digest.S11) + num2;
			num4 = MD5Digest.RotateLeft(num4 + MD5Digest.F(num, num2, num3) + this.X[1] + 3905402710U, MD5Digest.S12) + num;
			num3 = MD5Digest.RotateLeft(num3 + MD5Digest.F(num4, num, num2) + this.X[2] + 606105819U, MD5Digest.S13) + num4;
			num2 = MD5Digest.RotateLeft(num2 + MD5Digest.F(num3, num4, num) + this.X[3] + 3250441966U, MD5Digest.S14) + num3;
			num = MD5Digest.RotateLeft(num + MD5Digest.F(num2, num3, num4) + this.X[4] + 4118548399U, MD5Digest.S11) + num2;
			num4 = MD5Digest.RotateLeft(num4 + MD5Digest.F(num, num2, num3) + this.X[5] + 1200080426U, MD5Digest.S12) + num;
			num3 = MD5Digest.RotateLeft(num3 + MD5Digest.F(num4, num, num2) + this.X[6] + 2821735955U, MD5Digest.S13) + num4;
			num2 = MD5Digest.RotateLeft(num2 + MD5Digest.F(num3, num4, num) + this.X[7] + 4249261313U, MD5Digest.S14) + num3;
			num = MD5Digest.RotateLeft(num + MD5Digest.F(num2, num3, num4) + this.X[8] + 1770035416U, MD5Digest.S11) + num2;
			num4 = MD5Digest.RotateLeft(num4 + MD5Digest.F(num, num2, num3) + this.X[9] + 2336552879U, MD5Digest.S12) + num;
			num3 = MD5Digest.RotateLeft(num3 + MD5Digest.F(num4, num, num2) + this.X[10] + 4294925233U, MD5Digest.S13) + num4;
			num2 = MD5Digest.RotateLeft(num2 + MD5Digest.F(num3, num4, num) + this.X[11] + 2304563134U, MD5Digest.S14) + num3;
			num = MD5Digest.RotateLeft(num + MD5Digest.F(num2, num3, num4) + this.X[12] + 1804603682U, MD5Digest.S11) + num2;
			num4 = MD5Digest.RotateLeft(num4 + MD5Digest.F(num, num2, num3) + this.X[13] + 4254626195U, MD5Digest.S12) + num;
			num3 = MD5Digest.RotateLeft(num3 + MD5Digest.F(num4, num, num2) + this.X[14] + 2792965006U, MD5Digest.S13) + num4;
			num2 = MD5Digest.RotateLeft(num2 + MD5Digest.F(num3, num4, num) + this.X[15] + 1236535329U, MD5Digest.S14) + num3;
			num = MD5Digest.RotateLeft(num + MD5Digest.G(num2, num3, num4) + this.X[1] + 4129170786U, MD5Digest.S21) + num2;
			num4 = MD5Digest.RotateLeft(num4 + MD5Digest.G(num, num2, num3) + this.X[6] + 3225465664U, MD5Digest.S22) + num;
			num3 = MD5Digest.RotateLeft(num3 + MD5Digest.G(num4, num, num2) + this.X[11] + 643717713U, MD5Digest.S23) + num4;
			num2 = MD5Digest.RotateLeft(num2 + MD5Digest.G(num3, num4, num) + this.X[0] + 3921069994U, MD5Digest.S24) + num3;
			num = MD5Digest.RotateLeft(num + MD5Digest.G(num2, num3, num4) + this.X[5] + 3593408605U, MD5Digest.S21) + num2;
			num4 = MD5Digest.RotateLeft(num4 + MD5Digest.G(num, num2, num3) + this.X[10] + 38016083U, MD5Digest.S22) + num;
			num3 = MD5Digest.RotateLeft(num3 + MD5Digest.G(num4, num, num2) + this.X[15] + 3634488961U, MD5Digest.S23) + num4;
			num2 = MD5Digest.RotateLeft(num2 + MD5Digest.G(num3, num4, num) + this.X[4] + 3889429448U, MD5Digest.S24) + num3;
			num = MD5Digest.RotateLeft(num + MD5Digest.G(num2, num3, num4) + this.X[9] + 568446438U, MD5Digest.S21) + num2;
			num4 = MD5Digest.RotateLeft(num4 + MD5Digest.G(num, num2, num3) + this.X[14] + 3275163606U, MD5Digest.S22) + num;
			num3 = MD5Digest.RotateLeft(num3 + MD5Digest.G(num4, num, num2) + this.X[3] + 4107603335U, MD5Digest.S23) + num4;
			num2 = MD5Digest.RotateLeft(num2 + MD5Digest.G(num3, num4, num) + this.X[8] + 1163531501U, MD5Digest.S24) + num3;
			num = MD5Digest.RotateLeft(num + MD5Digest.G(num2, num3, num4) + this.X[13] + 2850285829U, MD5Digest.S21) + num2;
			num4 = MD5Digest.RotateLeft(num4 + MD5Digest.G(num, num2, num3) + this.X[2] + 4243563512U, MD5Digest.S22) + num;
			num3 = MD5Digest.RotateLeft(num3 + MD5Digest.G(num4, num, num2) + this.X[7] + 1735328473U, MD5Digest.S23) + num4;
			num2 = MD5Digest.RotateLeft(num2 + MD5Digest.G(num3, num4, num) + this.X[12] + 2368359562U, MD5Digest.S24) + num3;
			num = MD5Digest.RotateLeft(num + MD5Digest.H(num2, num3, num4) + this.X[5] + 4294588738U, MD5Digest.S31) + num2;
			num4 = MD5Digest.RotateLeft(num4 + MD5Digest.H(num, num2, num3) + this.X[8] + 2272392833U, MD5Digest.S32) + num;
			num3 = MD5Digest.RotateLeft(num3 + MD5Digest.H(num4, num, num2) + this.X[11] + 1839030562U, MD5Digest.S33) + num4;
			num2 = MD5Digest.RotateLeft(num2 + MD5Digest.H(num3, num4, num) + this.X[14] + 4259657740U, MD5Digest.S34) + num3;
			num = MD5Digest.RotateLeft(num + MD5Digest.H(num2, num3, num4) + this.X[1] + 2763975236U, MD5Digest.S31) + num2;
			num4 = MD5Digest.RotateLeft(num4 + MD5Digest.H(num, num2, num3) + this.X[4] + 1272893353U, MD5Digest.S32) + num;
			num3 = MD5Digest.RotateLeft(num3 + MD5Digest.H(num4, num, num2) + this.X[7] + 4139469664U, MD5Digest.S33) + num4;
			num2 = MD5Digest.RotateLeft(num2 + MD5Digest.H(num3, num4, num) + this.X[10] + 3200236656U, MD5Digest.S34) + num3;
			num = MD5Digest.RotateLeft(num + MD5Digest.H(num2, num3, num4) + this.X[13] + 681279174U, MD5Digest.S31) + num2;
			num4 = MD5Digest.RotateLeft(num4 + MD5Digest.H(num, num2, num3) + this.X[0] + 3936430074U, MD5Digest.S32) + num;
			num3 = MD5Digest.RotateLeft(num3 + MD5Digest.H(num4, num, num2) + this.X[3] + 3572445317U, MD5Digest.S33) + num4;
			num2 = MD5Digest.RotateLeft(num2 + MD5Digest.H(num3, num4, num) + this.X[6] + 76029189U, MD5Digest.S34) + num3;
			num = MD5Digest.RotateLeft(num + MD5Digest.H(num2, num3, num4) + this.X[9] + 3654602809U, MD5Digest.S31) + num2;
			num4 = MD5Digest.RotateLeft(num4 + MD5Digest.H(num, num2, num3) + this.X[12] + 3873151461U, MD5Digest.S32) + num;
			num3 = MD5Digest.RotateLeft(num3 + MD5Digest.H(num4, num, num2) + this.X[15] + 530742520U, MD5Digest.S33) + num4;
			num2 = MD5Digest.RotateLeft(num2 + MD5Digest.H(num3, num4, num) + this.X[2] + 3299628645U, MD5Digest.S34) + num3;
			num = MD5Digest.RotateLeft(num + MD5Digest.K(num2, num3, num4) + this.X[0] + 4096336452U, MD5Digest.S41) + num2;
			num4 = MD5Digest.RotateLeft(num4 + MD5Digest.K(num, num2, num3) + this.X[7] + 1126891415U, MD5Digest.S42) + num;
			num3 = MD5Digest.RotateLeft(num3 + MD5Digest.K(num4, num, num2) + this.X[14] + 2878612391U, MD5Digest.S43) + num4;
			num2 = MD5Digest.RotateLeft(num2 + MD5Digest.K(num3, num4, num) + this.X[5] + 4237533241U, MD5Digest.S44) + num3;
			num = MD5Digest.RotateLeft(num + MD5Digest.K(num2, num3, num4) + this.X[12] + 1700485571U, MD5Digest.S41) + num2;
			num4 = MD5Digest.RotateLeft(num4 + MD5Digest.K(num, num2, num3) + this.X[3] + 2399980690U, MD5Digest.S42) + num;
			num3 = MD5Digest.RotateLeft(num3 + MD5Digest.K(num4, num, num2) + this.X[10] + 4293915773U, MD5Digest.S43) + num4;
			num2 = MD5Digest.RotateLeft(num2 + MD5Digest.K(num3, num4, num) + this.X[1] + 2240044497U, MD5Digest.S44) + num3;
			num = MD5Digest.RotateLeft(num + MD5Digest.K(num2, num3, num4) + this.X[8] + 1873313359U, MD5Digest.S41) + num2;
			num4 = MD5Digest.RotateLeft(num4 + MD5Digest.K(num, num2, num3) + this.X[15] + 4264355552U, MD5Digest.S42) + num;
			num3 = MD5Digest.RotateLeft(num3 + MD5Digest.K(num4, num, num2) + this.X[6] + 2734768916U, MD5Digest.S43) + num4;
			num2 = MD5Digest.RotateLeft(num2 + MD5Digest.K(num3, num4, num) + this.X[13] + 1309151649U, MD5Digest.S44) + num3;
			num = MD5Digest.RotateLeft(num + MD5Digest.K(num2, num3, num4) + this.X[4] + 4149444226U, MD5Digest.S41) + num2;
			num4 = MD5Digest.RotateLeft(num4 + MD5Digest.K(num, num2, num3) + this.X[11] + 3174756917U, MD5Digest.S42) + num;
			num3 = MD5Digest.RotateLeft(num3 + MD5Digest.K(num4, num, num2) + this.X[2] + 718787259U, MD5Digest.S43) + num4;
			num2 = MD5Digest.RotateLeft(num2 + MD5Digest.K(num3, num4, num) + this.X[9] + 3951481745U, MD5Digest.S44) + num3;
			this.H1 += num;
			this.H2 += num2;
			this.H3 += num3;
			this.H4 += num4;
			this.xOff = 0;
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x0008861C File Offset: 0x0008861C
		public override IMemoable Copy()
		{
			return new MD5Digest(this);
		}

		// Token: 0x06001A05 RID: 6661 RVA: 0x00088624 File Offset: 0x00088624
		public override void Reset(IMemoable other)
		{
			MD5Digest t = (MD5Digest)other;
			this.CopyIn(t);
		}

		// Token: 0x04001164 RID: 4452
		private const int DigestLength = 16;

		// Token: 0x04001165 RID: 4453
		private uint H1;

		// Token: 0x04001166 RID: 4454
		private uint H2;

		// Token: 0x04001167 RID: 4455
		private uint H3;

		// Token: 0x04001168 RID: 4456
		private uint H4;

		// Token: 0x04001169 RID: 4457
		private uint[] X = new uint[16];

		// Token: 0x0400116A RID: 4458
		private int xOff;

		// Token: 0x0400116B RID: 4459
		private static readonly int S11 = 7;

		// Token: 0x0400116C RID: 4460
		private static readonly int S12 = 12;

		// Token: 0x0400116D RID: 4461
		private static readonly int S13 = 17;

		// Token: 0x0400116E RID: 4462
		private static readonly int S14 = 22;

		// Token: 0x0400116F RID: 4463
		private static readonly int S21 = 5;

		// Token: 0x04001170 RID: 4464
		private static readonly int S22 = 9;

		// Token: 0x04001171 RID: 4465
		private static readonly int S23 = 14;

		// Token: 0x04001172 RID: 4466
		private static readonly int S24 = 20;

		// Token: 0x04001173 RID: 4467
		private static readonly int S31 = 4;

		// Token: 0x04001174 RID: 4468
		private static readonly int S32 = 11;

		// Token: 0x04001175 RID: 4469
		private static readonly int S33 = 16;

		// Token: 0x04001176 RID: 4470
		private static readonly int S34 = 23;

		// Token: 0x04001177 RID: 4471
		private static readonly int S41 = 6;

		// Token: 0x04001178 RID: 4472
		private static readonly int S42 = 10;

		// Token: 0x04001179 RID: 4473
		private static readonly int S43 = 15;

		// Token: 0x0400117A RID: 4474
		private static readonly int S44 = 21;
	}
}
