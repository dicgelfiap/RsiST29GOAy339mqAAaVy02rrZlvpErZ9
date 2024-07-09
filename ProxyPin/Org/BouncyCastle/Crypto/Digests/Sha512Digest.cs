using System;
using Org.BouncyCastle.Crypto.Utilities;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x02000366 RID: 870
	public class Sha512Digest : LongDigest
	{
		// Token: 0x06001AB5 RID: 6837 RVA: 0x000908FC File Offset: 0x000908FC
		public Sha512Digest()
		{
		}

		// Token: 0x06001AB6 RID: 6838 RVA: 0x00090904 File Offset: 0x00090904
		public Sha512Digest(Sha512Digest t) : base(t)
		{
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06001AB7 RID: 6839 RVA: 0x00090910 File Offset: 0x00090910
		public override string AlgorithmName
		{
			get
			{
				return "SHA-512";
			}
		}

		// Token: 0x06001AB8 RID: 6840 RVA: 0x00090918 File Offset: 0x00090918
		public override int GetDigestSize()
		{
			return 64;
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x0009091C File Offset: 0x0009091C
		public override int DoFinal(byte[] output, int outOff)
		{
			base.Finish();
			Pack.UInt64_To_BE(this.H1, output, outOff);
			Pack.UInt64_To_BE(this.H2, output, outOff + 8);
			Pack.UInt64_To_BE(this.H3, output, outOff + 16);
			Pack.UInt64_To_BE(this.H4, output, outOff + 24);
			Pack.UInt64_To_BE(this.H5, output, outOff + 32);
			Pack.UInt64_To_BE(this.H6, output, outOff + 40);
			Pack.UInt64_To_BE(this.H7, output, outOff + 48);
			Pack.UInt64_To_BE(this.H8, output, outOff + 56);
			this.Reset();
			return 64;
		}

		// Token: 0x06001ABA RID: 6842 RVA: 0x000909B8 File Offset: 0x000909B8
		public override void Reset()
		{
			base.Reset();
			this.H1 = 7640891576956012808UL;
			this.H2 = 13503953896175478587UL;
			this.H3 = 4354685564936845355UL;
			this.H4 = 11912009170470909681UL;
			this.H5 = 5840696475078001361UL;
			this.H6 = 11170449401992604703UL;
			this.H7 = 2270897969802886507UL;
			this.H8 = 6620516959819538809UL;
		}

		// Token: 0x06001ABB RID: 6843 RVA: 0x00090A48 File Offset: 0x00090A48
		public override IMemoable Copy()
		{
			return new Sha512Digest(this);
		}

		// Token: 0x06001ABC RID: 6844 RVA: 0x00090A50 File Offset: 0x00090A50
		public override void Reset(IMemoable other)
		{
			Sha512Digest t = (Sha512Digest)other;
			base.CopyIn(t);
		}

		// Token: 0x040011C9 RID: 4553
		private const int DigestLength = 64;
	}
}
