using System;

namespace Org.BouncyCastle.Asn1.Misc
{
	// Token: 0x02000185 RID: 389
	public class NetscapeCertType : DerBitString
	{
		// Token: 0x06000CEF RID: 3311 RVA: 0x00052684 File Offset: 0x00052684
		public NetscapeCertType(int usage) : base(usage)
		{
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x00052690 File Offset: 0x00052690
		public NetscapeCertType(DerBitString usage) : base(usage.GetBytes(), usage.PadBits)
		{
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x000526A4 File Offset: 0x000526A4
		public override string ToString()
		{
			byte[] bytes = this.GetBytes();
			return "NetscapeCertType: 0x" + ((int)(bytes[0] & byte.MaxValue)).ToString("X");
		}

		// Token: 0x0400092A RID: 2346
		public const int SslClient = 128;

		// Token: 0x0400092B RID: 2347
		public const int SslServer = 64;

		// Token: 0x0400092C RID: 2348
		public const int Smime = 32;

		// Token: 0x0400092D RID: 2349
		public const int ObjectSigning = 16;

		// Token: 0x0400092E RID: 2350
		public const int Reserved = 8;

		// Token: 0x0400092F RID: 2351
		public const int SslCA = 4;

		// Token: 0x04000930 RID: 2352
		public const int SmimeCA = 2;

		// Token: 0x04000931 RID: 2353
		public const int ObjectSigningCA = 1;
	}
}
