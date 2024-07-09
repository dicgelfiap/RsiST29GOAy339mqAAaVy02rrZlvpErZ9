using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200050D RID: 1293
	public abstract class NamedCurve
	{
		// Token: 0x0600276C RID: 10092 RVA: 0x000D5A44 File Offset: 0x000D5A44
		public static bool IsValid(int namedCurve)
		{
			return (namedCurve >= 1 && namedCurve <= 28) || (namedCurve >= 65281 && namedCurve <= 65282);
		}

		// Token: 0x0600276D RID: 10093 RVA: 0x000D5A70 File Offset: 0x000D5A70
		public static bool RefersToASpecificNamedCurve(int namedCurve)
		{
			switch (namedCurve)
			{
			case 65281:
			case 65282:
				return false;
			default:
				return true;
			}
		}

		// Token: 0x040019E4 RID: 6628
		public const int sect163k1 = 1;

		// Token: 0x040019E5 RID: 6629
		public const int sect163r1 = 2;

		// Token: 0x040019E6 RID: 6630
		public const int sect163r2 = 3;

		// Token: 0x040019E7 RID: 6631
		public const int sect193r1 = 4;

		// Token: 0x040019E8 RID: 6632
		public const int sect193r2 = 5;

		// Token: 0x040019E9 RID: 6633
		public const int sect233k1 = 6;

		// Token: 0x040019EA RID: 6634
		public const int sect233r1 = 7;

		// Token: 0x040019EB RID: 6635
		public const int sect239k1 = 8;

		// Token: 0x040019EC RID: 6636
		public const int sect283k1 = 9;

		// Token: 0x040019ED RID: 6637
		public const int sect283r1 = 10;

		// Token: 0x040019EE RID: 6638
		public const int sect409k1 = 11;

		// Token: 0x040019EF RID: 6639
		public const int sect409r1 = 12;

		// Token: 0x040019F0 RID: 6640
		public const int sect571k1 = 13;

		// Token: 0x040019F1 RID: 6641
		public const int sect571r1 = 14;

		// Token: 0x040019F2 RID: 6642
		public const int secp160k1 = 15;

		// Token: 0x040019F3 RID: 6643
		public const int secp160r1 = 16;

		// Token: 0x040019F4 RID: 6644
		public const int secp160r2 = 17;

		// Token: 0x040019F5 RID: 6645
		public const int secp192k1 = 18;

		// Token: 0x040019F6 RID: 6646
		public const int secp192r1 = 19;

		// Token: 0x040019F7 RID: 6647
		public const int secp224k1 = 20;

		// Token: 0x040019F8 RID: 6648
		public const int secp224r1 = 21;

		// Token: 0x040019F9 RID: 6649
		public const int secp256k1 = 22;

		// Token: 0x040019FA RID: 6650
		public const int secp256r1 = 23;

		// Token: 0x040019FB RID: 6651
		public const int secp384r1 = 24;

		// Token: 0x040019FC RID: 6652
		public const int secp521r1 = 25;

		// Token: 0x040019FD RID: 6653
		public const int brainpoolP256r1 = 26;

		// Token: 0x040019FE RID: 6654
		public const int brainpoolP384r1 = 27;

		// Token: 0x040019FF RID: 6655
		public const int brainpoolP512r1 = 28;

		// Token: 0x04001A00 RID: 6656
		public const int arbitrary_explicit_prime_curves = 65281;

		// Token: 0x04001A01 RID: 6657
		public const int arbitrary_explicit_char2_curves = 65282;
	}
}
