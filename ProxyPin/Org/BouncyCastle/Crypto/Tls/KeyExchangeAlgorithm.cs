using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200050A RID: 1290
	public abstract class KeyExchangeAlgorithm
	{
		// Token: 0x040019BF RID: 6591
		public const int NULL = 0;

		// Token: 0x040019C0 RID: 6592
		public const int RSA = 1;

		// Token: 0x040019C1 RID: 6593
		public const int RSA_EXPORT = 2;

		// Token: 0x040019C2 RID: 6594
		public const int DHE_DSS = 3;

		// Token: 0x040019C3 RID: 6595
		public const int DHE_DSS_EXPORT = 4;

		// Token: 0x040019C4 RID: 6596
		public const int DHE_RSA = 5;

		// Token: 0x040019C5 RID: 6597
		public const int DHE_RSA_EXPORT = 6;

		// Token: 0x040019C6 RID: 6598
		public const int DH_DSS = 7;

		// Token: 0x040019C7 RID: 6599
		public const int DH_DSS_EXPORT = 8;

		// Token: 0x040019C8 RID: 6600
		public const int DH_RSA = 9;

		// Token: 0x040019C9 RID: 6601
		public const int DH_RSA_EXPORT = 10;

		// Token: 0x040019CA RID: 6602
		public const int DH_anon = 11;

		// Token: 0x040019CB RID: 6603
		public const int DH_anon_EXPORT = 12;

		// Token: 0x040019CC RID: 6604
		public const int PSK = 13;

		// Token: 0x040019CD RID: 6605
		public const int DHE_PSK = 14;

		// Token: 0x040019CE RID: 6606
		public const int RSA_PSK = 15;

		// Token: 0x040019CF RID: 6607
		public const int ECDH_ECDSA = 16;

		// Token: 0x040019D0 RID: 6608
		public const int ECDHE_ECDSA = 17;

		// Token: 0x040019D1 RID: 6609
		public const int ECDH_RSA = 18;

		// Token: 0x040019D2 RID: 6610
		public const int ECDHE_RSA = 19;

		// Token: 0x040019D3 RID: 6611
		public const int ECDH_anon = 20;

		// Token: 0x040019D4 RID: 6612
		public const int SRP = 21;

		// Token: 0x040019D5 RID: 6613
		public const int SRP_DSS = 22;

		// Token: 0x040019D6 RID: 6614
		public const int SRP_RSA = 23;

		// Token: 0x040019D7 RID: 6615
		public const int ECDHE_PSK = 24;
	}
}
