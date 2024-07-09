using System;

namespace Org.BouncyCastle.Asn1.Crmf
{
	// Token: 0x02000131 RID: 305
	public abstract class CrmfObjectIdentifiers
	{
		// Token: 0x04000768 RID: 1896
		public static readonly DerObjectIdentifier id_pkix = new DerObjectIdentifier("1.3.6.1.5.5.7");

		// Token: 0x04000769 RID: 1897
		public static readonly DerObjectIdentifier id_pkip = CrmfObjectIdentifiers.id_pkix.Branch("5");

		// Token: 0x0400076A RID: 1898
		public static readonly DerObjectIdentifier id_regCtrl = CrmfObjectIdentifiers.id_pkip.Branch("1");

		// Token: 0x0400076B RID: 1899
		public static readonly DerObjectIdentifier id_regCtrl_regToken = CrmfObjectIdentifiers.id_regCtrl.Branch("1");

		// Token: 0x0400076C RID: 1900
		public static readonly DerObjectIdentifier id_regCtrl_authenticator = CrmfObjectIdentifiers.id_regCtrl.Branch("2");

		// Token: 0x0400076D RID: 1901
		public static readonly DerObjectIdentifier id_regCtrl_pkiPublicationInfo = CrmfObjectIdentifiers.id_regCtrl.Branch("3");

		// Token: 0x0400076E RID: 1902
		public static readonly DerObjectIdentifier id_regCtrl_pkiArchiveOptions = CrmfObjectIdentifiers.id_regCtrl.Branch("4");

		// Token: 0x0400076F RID: 1903
		public static readonly DerObjectIdentifier id_ct_encKeyWithID = new DerObjectIdentifier("1.2.840.113549.1.9.16.1.21");
	}
}
