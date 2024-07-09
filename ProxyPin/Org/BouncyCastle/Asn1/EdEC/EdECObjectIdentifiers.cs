using System;

namespace Org.BouncyCastle.Asn1.EdEC
{
	// Token: 0x02000148 RID: 328
	public abstract class EdECObjectIdentifiers
	{
		// Token: 0x040007E4 RID: 2020
		public static readonly DerObjectIdentifier id_edwards_curve_algs = new DerObjectIdentifier("1.3.101");

		// Token: 0x040007E5 RID: 2021
		public static readonly DerObjectIdentifier id_X25519 = EdECObjectIdentifiers.id_edwards_curve_algs.Branch("110");

		// Token: 0x040007E6 RID: 2022
		public static readonly DerObjectIdentifier id_X448 = EdECObjectIdentifiers.id_edwards_curve_algs.Branch("111");

		// Token: 0x040007E7 RID: 2023
		public static readonly DerObjectIdentifier id_Ed25519 = EdECObjectIdentifiers.id_edwards_curve_algs.Branch("112");

		// Token: 0x040007E8 RID: 2024
		public static readonly DerObjectIdentifier id_Ed448 = EdECObjectIdentifiers.id_edwards_curve_algs.Branch("113");
	}
}
