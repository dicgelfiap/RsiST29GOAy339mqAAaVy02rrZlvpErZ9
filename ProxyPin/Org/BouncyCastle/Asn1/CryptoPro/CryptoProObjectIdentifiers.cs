using System;

namespace Org.BouncyCastle.Asn1.CryptoPro
{
	// Token: 0x02000140 RID: 320
	public abstract class CryptoProObjectIdentifiers
	{
		// Token: 0x040007A0 RID: 1952
		public const string GostID = "1.2.643.2.2";

		// Token: 0x040007A1 RID: 1953
		public static readonly DerObjectIdentifier GostR3411 = new DerObjectIdentifier("1.2.643.2.2.9");

		// Token: 0x040007A2 RID: 1954
		public static readonly DerObjectIdentifier GostR3411Hmac = new DerObjectIdentifier("1.2.643.2.2.10");

		// Token: 0x040007A3 RID: 1955
		public static readonly DerObjectIdentifier GostR28147Cbc = new DerObjectIdentifier("1.2.643.2.2.21");

		// Token: 0x040007A4 RID: 1956
		public static readonly DerObjectIdentifier ID_Gost28147_89_CryptoPro_A_ParamSet = new DerObjectIdentifier("1.2.643.2.2.31.1");

		// Token: 0x040007A5 RID: 1957
		public static readonly DerObjectIdentifier GostR3410x94 = new DerObjectIdentifier("1.2.643.2.2.20");

		// Token: 0x040007A6 RID: 1958
		public static readonly DerObjectIdentifier GostR3410x2001 = new DerObjectIdentifier("1.2.643.2.2.19");

		// Token: 0x040007A7 RID: 1959
		public static readonly DerObjectIdentifier GostR3411x94WithGostR3410x94 = new DerObjectIdentifier("1.2.643.2.2.4");

		// Token: 0x040007A8 RID: 1960
		public static readonly DerObjectIdentifier GostR3411x94WithGostR3410x2001 = new DerObjectIdentifier("1.2.643.2.2.3");

		// Token: 0x040007A9 RID: 1961
		public static readonly DerObjectIdentifier GostR3411x94CryptoProParamSet = new DerObjectIdentifier("1.2.643.2.2.30.1");

		// Token: 0x040007AA RID: 1962
		public static readonly DerObjectIdentifier GostR3410x94CryptoProA = new DerObjectIdentifier("1.2.643.2.2.32.2");

		// Token: 0x040007AB RID: 1963
		public static readonly DerObjectIdentifier GostR3410x94CryptoProB = new DerObjectIdentifier("1.2.643.2.2.32.3");

		// Token: 0x040007AC RID: 1964
		public static readonly DerObjectIdentifier GostR3410x94CryptoProC = new DerObjectIdentifier("1.2.643.2.2.32.4");

		// Token: 0x040007AD RID: 1965
		public static readonly DerObjectIdentifier GostR3410x94CryptoProD = new DerObjectIdentifier("1.2.643.2.2.32.5");

		// Token: 0x040007AE RID: 1966
		public static readonly DerObjectIdentifier GostR3410x94CryptoProXchA = new DerObjectIdentifier("1.2.643.2.2.33.1");

		// Token: 0x040007AF RID: 1967
		public static readonly DerObjectIdentifier GostR3410x94CryptoProXchB = new DerObjectIdentifier("1.2.643.2.2.33.2");

		// Token: 0x040007B0 RID: 1968
		public static readonly DerObjectIdentifier GostR3410x94CryptoProXchC = new DerObjectIdentifier("1.2.643.2.2.33.3");

		// Token: 0x040007B1 RID: 1969
		public static readonly DerObjectIdentifier GostR3410x2001CryptoProA = new DerObjectIdentifier("1.2.643.2.2.35.1");

		// Token: 0x040007B2 RID: 1970
		public static readonly DerObjectIdentifier GostR3410x2001CryptoProB = new DerObjectIdentifier("1.2.643.2.2.35.2");

		// Token: 0x040007B3 RID: 1971
		public static readonly DerObjectIdentifier GostR3410x2001CryptoProC = new DerObjectIdentifier("1.2.643.2.2.35.3");

		// Token: 0x040007B4 RID: 1972
		public static readonly DerObjectIdentifier GostR3410x2001CryptoProXchA = new DerObjectIdentifier("1.2.643.2.2.36.0");

		// Token: 0x040007B5 RID: 1973
		public static readonly DerObjectIdentifier GostR3410x2001CryptoProXchB = new DerObjectIdentifier("1.2.643.2.2.36.1");

		// Token: 0x040007B6 RID: 1974
		public static readonly DerObjectIdentifier GostElSgDH3410Default = new DerObjectIdentifier("1.2.643.2.2.36.0");

		// Token: 0x040007B7 RID: 1975
		public static readonly DerObjectIdentifier GostElSgDH3410x1 = new DerObjectIdentifier("1.2.643.2.2.36.1");
	}
}
