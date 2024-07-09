using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.X509
{
	// Token: 0x0200071F RID: 1823
	public class X509KeyUsage : Asn1Encodable
	{
		// Token: 0x06003FEA RID: 16362 RVA: 0x0015E578 File Offset: 0x0015E578
		public X509KeyUsage(int usage)
		{
			this.usage = usage;
		}

		// Token: 0x06003FEB RID: 16363 RVA: 0x0015E588 File Offset: 0x0015E588
		public override Asn1Object ToAsn1Object()
		{
			return new KeyUsage(this.usage);
		}

		// Token: 0x040020BF RID: 8383
		public const int DigitalSignature = 128;

		// Token: 0x040020C0 RID: 8384
		public const int NonRepudiation = 64;

		// Token: 0x040020C1 RID: 8385
		public const int KeyEncipherment = 32;

		// Token: 0x040020C2 RID: 8386
		public const int DataEncipherment = 16;

		// Token: 0x040020C3 RID: 8387
		public const int KeyAgreement = 8;

		// Token: 0x040020C4 RID: 8388
		public const int KeyCertSign = 4;

		// Token: 0x040020C5 RID: 8389
		public const int CrlSign = 2;

		// Token: 0x040020C6 RID: 8390
		public const int EncipherOnly = 1;

		// Token: 0x040020C7 RID: 8391
		public const int DecipherOnly = 32768;

		// Token: 0x040020C8 RID: 8392
		private readonly int usage;
	}
}
