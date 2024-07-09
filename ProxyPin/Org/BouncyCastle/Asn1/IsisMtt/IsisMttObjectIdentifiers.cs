using System;

namespace Org.BouncyCastle.Asn1.IsisMtt
{
	// Token: 0x0200017F RID: 383
	public abstract class IsisMttObjectIdentifiers
	{
		// Token: 0x040008E4 RID: 2276
		public static readonly DerObjectIdentifier IdIsisMtt = new DerObjectIdentifier("1.3.36.8");

		// Token: 0x040008E5 RID: 2277
		public static readonly DerObjectIdentifier IdIsisMttCP = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMtt + ".1");

		// Token: 0x040008E6 RID: 2278
		public static readonly DerObjectIdentifier IdIsisMttCPAccredited = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttCP + ".1");

		// Token: 0x040008E7 RID: 2279
		public static readonly DerObjectIdentifier IdIsisMttAT = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMtt + ".3");

		// Token: 0x040008E8 RID: 2280
		public static readonly DerObjectIdentifier IdIsisMttATDateOfCertGen = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttAT + ".1");

		// Token: 0x040008E9 RID: 2281
		public static readonly DerObjectIdentifier IdIsisMttATProcuration = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttAT + ".2");

		// Token: 0x040008EA RID: 2282
		public static readonly DerObjectIdentifier IdIsisMttATAdmission = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttAT + ".3");

		// Token: 0x040008EB RID: 2283
		public static readonly DerObjectIdentifier IdIsisMttATMonetaryLimit = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttAT + ".4");

		// Token: 0x040008EC RID: 2284
		public static readonly DerObjectIdentifier IdIsisMttATDeclarationOfMajority = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttAT + ".5");

		// Token: 0x040008ED RID: 2285
		public static readonly DerObjectIdentifier IdIsisMttATIccsn = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttAT + ".6");

		// Token: 0x040008EE RID: 2286
		public static readonly DerObjectIdentifier IdIsisMttATPKReference = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttAT + ".7");

		// Token: 0x040008EF RID: 2287
		public static readonly DerObjectIdentifier IdIsisMttATRestriction = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttAT + ".8");

		// Token: 0x040008F0 RID: 2288
		public static readonly DerObjectIdentifier IdIsisMttATRetrieveIfAllowed = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttAT + ".9");

		// Token: 0x040008F1 RID: 2289
		public static readonly DerObjectIdentifier IdIsisMttATRequestedCertificate = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttAT + ".10");

		// Token: 0x040008F2 RID: 2290
		public static readonly DerObjectIdentifier IdIsisMttATNamingAuthorities = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttAT + ".11");

		// Token: 0x040008F3 RID: 2291
		public static readonly DerObjectIdentifier IdIsisMttATCertInDirSince = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttAT + ".12");

		// Token: 0x040008F4 RID: 2292
		public static readonly DerObjectIdentifier IdIsisMttATCertHash = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttAT + ".13");

		// Token: 0x040008F5 RID: 2293
		public static readonly DerObjectIdentifier IdIsisMttATNameAtBirth = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttAT + ".14");

		// Token: 0x040008F6 RID: 2294
		public static readonly DerObjectIdentifier IdIsisMttATAdditionalInformation = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttAT + ".15");

		// Token: 0x040008F7 RID: 2295
		public static readonly DerObjectIdentifier IdIsisMttATLiabilityLimitationFlag = new DerObjectIdentifier("0.2.262.1.10.12.0");
	}
}
