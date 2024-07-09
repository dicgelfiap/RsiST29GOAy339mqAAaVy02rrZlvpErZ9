using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000500 RID: 1280
	public abstract class EncryptionAlgorithm
	{
		// Token: 0x0400195D RID: 6493
		public const int NULL = 0;

		// Token: 0x0400195E RID: 6494
		public const int RC4_40 = 1;

		// Token: 0x0400195F RID: 6495
		public const int RC4_128 = 2;

		// Token: 0x04001960 RID: 6496
		public const int RC2_CBC_40 = 3;

		// Token: 0x04001961 RID: 6497
		public const int IDEA_CBC = 4;

		// Token: 0x04001962 RID: 6498
		public const int DES40_CBC = 5;

		// Token: 0x04001963 RID: 6499
		public const int DES_CBC = 6;

		// Token: 0x04001964 RID: 6500
		public const int cls_3DES_EDE_CBC = 7;

		// Token: 0x04001965 RID: 6501
		public const int AES_128_CBC = 8;

		// Token: 0x04001966 RID: 6502
		public const int AES_256_CBC = 9;

		// Token: 0x04001967 RID: 6503
		public const int AES_128_GCM = 10;

		// Token: 0x04001968 RID: 6504
		public const int AES_256_GCM = 11;

		// Token: 0x04001969 RID: 6505
		public const int CAMELLIA_128_CBC = 12;

		// Token: 0x0400196A RID: 6506
		public const int CAMELLIA_256_CBC = 13;

		// Token: 0x0400196B RID: 6507
		public const int SEED_CBC = 14;

		// Token: 0x0400196C RID: 6508
		public const int AES_128_CCM = 15;

		// Token: 0x0400196D RID: 6509
		public const int AES_128_CCM_8 = 16;

		// Token: 0x0400196E RID: 6510
		public const int AES_256_CCM = 17;

		// Token: 0x0400196F RID: 6511
		public const int AES_256_CCM_8 = 18;

		// Token: 0x04001970 RID: 6512
		public const int CAMELLIA_128_GCM = 19;

		// Token: 0x04001971 RID: 6513
		public const int CAMELLIA_256_GCM = 20;

		// Token: 0x04001972 RID: 6514
		public const int CHACHA20_POLY1305 = 21;

		// Token: 0x04001973 RID: 6515
		public const int AES_128_OCB_TAGLEN96 = 103;

		// Token: 0x04001974 RID: 6516
		public const int AES_256_OCB_TAGLEN96 = 104;
	}
}
