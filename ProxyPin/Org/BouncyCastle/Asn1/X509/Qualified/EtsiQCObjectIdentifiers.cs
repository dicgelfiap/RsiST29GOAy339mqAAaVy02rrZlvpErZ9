using System;

namespace Org.BouncyCastle.Asn1.X509.Qualified
{
	// Token: 0x020001D8 RID: 472
	public abstract class EtsiQCObjectIdentifiers
	{
		// Token: 0x04000B72 RID: 2930
		public static readonly DerObjectIdentifier IdEtsiQcs = new DerObjectIdentifier("0.4.0.1862.1");

		// Token: 0x04000B73 RID: 2931
		public static readonly DerObjectIdentifier IdEtsiQcsQcCompliance = new DerObjectIdentifier(EtsiQCObjectIdentifiers.IdEtsiQcs + ".1");

		// Token: 0x04000B74 RID: 2932
		public static readonly DerObjectIdentifier IdEtsiQcsLimitValue = new DerObjectIdentifier(EtsiQCObjectIdentifiers.IdEtsiQcs + ".2");

		// Token: 0x04000B75 RID: 2933
		public static readonly DerObjectIdentifier IdEtsiQcsRetentionPeriod = new DerObjectIdentifier(EtsiQCObjectIdentifiers.IdEtsiQcs + ".3");

		// Token: 0x04000B76 RID: 2934
		public static readonly DerObjectIdentifier IdEtsiQcsQcSscd = new DerObjectIdentifier(EtsiQCObjectIdentifiers.IdEtsiQcs + ".4");
	}
}
