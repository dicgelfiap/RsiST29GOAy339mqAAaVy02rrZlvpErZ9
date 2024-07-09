using System;
using Org.BouncyCastle.Asn1.Pkcs;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x02000153 RID: 339
	public abstract class EsfAttributes
	{
		// Token: 0x040007FF RID: 2047
		public static readonly DerObjectIdentifier SigPolicyId = PkcsObjectIdentifiers.IdAAEtsSigPolicyID;

		// Token: 0x04000800 RID: 2048
		public static readonly DerObjectIdentifier CommitmentType = PkcsObjectIdentifiers.IdAAEtsCommitmentType;

		// Token: 0x04000801 RID: 2049
		public static readonly DerObjectIdentifier SignerLocation = PkcsObjectIdentifiers.IdAAEtsSignerLocation;

		// Token: 0x04000802 RID: 2050
		public static readonly DerObjectIdentifier SignerAttr = PkcsObjectIdentifiers.IdAAEtsSignerAttr;

		// Token: 0x04000803 RID: 2051
		public static readonly DerObjectIdentifier OtherSigCert = PkcsObjectIdentifiers.IdAAEtsOtherSigCert;

		// Token: 0x04000804 RID: 2052
		public static readonly DerObjectIdentifier ContentTimestamp = PkcsObjectIdentifiers.IdAAEtsContentTimestamp;

		// Token: 0x04000805 RID: 2053
		public static readonly DerObjectIdentifier CertificateRefs = PkcsObjectIdentifiers.IdAAEtsCertificateRefs;

		// Token: 0x04000806 RID: 2054
		public static readonly DerObjectIdentifier RevocationRefs = PkcsObjectIdentifiers.IdAAEtsRevocationRefs;

		// Token: 0x04000807 RID: 2055
		public static readonly DerObjectIdentifier CertValues = PkcsObjectIdentifiers.IdAAEtsCertValues;

		// Token: 0x04000808 RID: 2056
		public static readonly DerObjectIdentifier RevocationValues = PkcsObjectIdentifiers.IdAAEtsRevocationValues;

		// Token: 0x04000809 RID: 2057
		public static readonly DerObjectIdentifier EscTimeStamp = PkcsObjectIdentifiers.IdAAEtsEscTimeStamp;

		// Token: 0x0400080A RID: 2058
		public static readonly DerObjectIdentifier CertCrlTimestamp = PkcsObjectIdentifiers.IdAAEtsCertCrlTimestamp;

		// Token: 0x0400080B RID: 2059
		public static readonly DerObjectIdentifier ArchiveTimestamp = PkcsObjectIdentifiers.IdAAEtsArchiveTimestamp;

		// Token: 0x0400080C RID: 2060
		public static readonly DerObjectIdentifier ArchiveTimestampV2 = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.48");
	}
}
