using System;
using Org.BouncyCastle.Crypto.Utilities;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x02000364 RID: 868
	public class Sha384Digest : LongDigest
	{
		// Token: 0x06001AA5 RID: 6821 RVA: 0x00090660 File Offset: 0x00090660
		public Sha384Digest()
		{
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x00090668 File Offset: 0x00090668
		public Sha384Digest(Sha384Digest t) : base(t)
		{
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06001AA7 RID: 6823 RVA: 0x00090674 File Offset: 0x00090674
		public override string AlgorithmName
		{
			get
			{
				return "SHA-384";
			}
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x0009067C File Offset: 0x0009067C
		public override int GetDigestSize()
		{
			return 48;
		}

		// Token: 0x06001AA9 RID: 6825 RVA: 0x00090680 File Offset: 0x00090680
		public override int DoFinal(byte[] output, int outOff)
		{
			base.Finish();
			Pack.UInt64_To_BE(this.H1, output, outOff);
			Pack.UInt64_To_BE(this.H2, output, outOff + 8);
			Pack.UInt64_To_BE(this.H3, output, outOff + 16);
			Pack.UInt64_To_BE(this.H4, output, outOff + 24);
			Pack.UInt64_To_BE(this.H5, output, outOff + 32);
			Pack.UInt64_To_BE(this.H6, output, outOff + 40);
			this.Reset();
			return 48;
		}

		// Token: 0x06001AAA RID: 6826 RVA: 0x000906FC File Offset: 0x000906FC
		public override void Reset()
		{
			base.Reset();
			this.H1 = 14680500436340154072UL;
			this.H2 = 7105036623409894663UL;
			this.H3 = 10473403895298186519UL;
			this.H4 = 1526699215303891257UL;
			this.H5 = 7436329637833083697UL;
			this.H6 = 10282925794625328401UL;
			this.H7 = 15784041429090275239UL;
			this.H8 = 5167115440072839076UL;
		}

		// Token: 0x06001AAB RID: 6827 RVA: 0x0009078C File Offset: 0x0009078C
		public override IMemoable Copy()
		{
			return new Sha384Digest(this);
		}

		// Token: 0x06001AAC RID: 6828 RVA: 0x00090794 File Offset: 0x00090794
		public override void Reset(IMemoable other)
		{
			Sha384Digest t = (Sha384Digest)other;
			base.CopyIn(t);
		}

		// Token: 0x040011C8 RID: 4552
		private const int DigestLength = 48;
	}
}
