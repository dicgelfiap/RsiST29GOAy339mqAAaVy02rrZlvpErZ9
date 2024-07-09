using System;

namespace Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020000D7 RID: 215
	public abstract class CmpObjectIdentifiers
	{
		// Token: 0x040005EF RID: 1519
		public static readonly DerObjectIdentifier passwordBasedMac = new DerObjectIdentifier("1.2.840.113533.7.66.13");

		// Token: 0x040005F0 RID: 1520
		public static readonly DerObjectIdentifier dhBasedMac = new DerObjectIdentifier("1.2.840.113533.7.66.30");

		// Token: 0x040005F1 RID: 1521
		public static readonly DerObjectIdentifier it_caProtEncCert = new DerObjectIdentifier("1.3.6.1.5.5.7.4.1");

		// Token: 0x040005F2 RID: 1522
		public static readonly DerObjectIdentifier it_signKeyPairTypes = new DerObjectIdentifier("1.3.6.1.5.5.7.4.2");

		// Token: 0x040005F3 RID: 1523
		public static readonly DerObjectIdentifier it_encKeyPairTypes = new DerObjectIdentifier("1.3.6.1.5.5.7.4.3");

		// Token: 0x040005F4 RID: 1524
		public static readonly DerObjectIdentifier it_preferredSymAlg = new DerObjectIdentifier("1.3.6.1.5.5.7.4.4");

		// Token: 0x040005F5 RID: 1525
		public static readonly DerObjectIdentifier it_caKeyUpdateInfo = new DerObjectIdentifier("1.3.6.1.5.5.7.4.5");

		// Token: 0x040005F6 RID: 1526
		public static readonly DerObjectIdentifier it_currentCRL = new DerObjectIdentifier("1.3.6.1.5.5.7.4.6");

		// Token: 0x040005F7 RID: 1527
		public static readonly DerObjectIdentifier it_unsupportedOIDs = new DerObjectIdentifier("1.3.6.1.5.5.7.4.7");

		// Token: 0x040005F8 RID: 1528
		public static readonly DerObjectIdentifier it_keyPairParamReq = new DerObjectIdentifier("1.3.6.1.5.5.7.4.10");

		// Token: 0x040005F9 RID: 1529
		public static readonly DerObjectIdentifier it_keyPairParamRep = new DerObjectIdentifier("1.3.6.1.5.5.7.4.11");

		// Token: 0x040005FA RID: 1530
		public static readonly DerObjectIdentifier it_revPassphrase = new DerObjectIdentifier("1.3.6.1.5.5.7.4.12");

		// Token: 0x040005FB RID: 1531
		public static readonly DerObjectIdentifier it_implicitConfirm = new DerObjectIdentifier("1.3.6.1.5.5.7.4.13");

		// Token: 0x040005FC RID: 1532
		public static readonly DerObjectIdentifier it_confirmWaitTime = new DerObjectIdentifier("1.3.6.1.5.5.7.4.14");

		// Token: 0x040005FD RID: 1533
		public static readonly DerObjectIdentifier it_origPKIMessage = new DerObjectIdentifier("1.3.6.1.5.5.7.4.15");

		// Token: 0x040005FE RID: 1534
		public static readonly DerObjectIdentifier it_suppLangTags = new DerObjectIdentifier("1.3.6.1.5.5.7.4.16");

		// Token: 0x040005FF RID: 1535
		public static readonly DerObjectIdentifier regCtrl_regToken = new DerObjectIdentifier("1.3.6.1.5.5.7.5.1.1");

		// Token: 0x04000600 RID: 1536
		public static readonly DerObjectIdentifier regCtrl_authenticator = new DerObjectIdentifier("1.3.6.1.5.5.7.5.1.2");

		// Token: 0x04000601 RID: 1537
		public static readonly DerObjectIdentifier regCtrl_pkiPublicationInfo = new DerObjectIdentifier("1.3.6.1.5.5.7.5.1.3");

		// Token: 0x04000602 RID: 1538
		public static readonly DerObjectIdentifier regCtrl_pkiArchiveOptions = new DerObjectIdentifier("1.3.6.1.5.5.7.5.1.4");

		// Token: 0x04000603 RID: 1539
		public static readonly DerObjectIdentifier regCtrl_oldCertID = new DerObjectIdentifier("1.3.6.1.5.5.7.5.1.5");

		// Token: 0x04000604 RID: 1540
		public static readonly DerObjectIdentifier regCtrl_protocolEncrKey = new DerObjectIdentifier("1.3.6.1.5.5.7.5.1.6");

		// Token: 0x04000605 RID: 1541
		public static readonly DerObjectIdentifier regCtrl_altCertTemplate = new DerObjectIdentifier("1.3.6.1.5.5.7.5.1.7");

		// Token: 0x04000606 RID: 1542
		public static readonly DerObjectIdentifier regInfo_utf8Pairs = new DerObjectIdentifier("1.3.6.1.5.5.7.5.2.1");

		// Token: 0x04000607 RID: 1543
		public static readonly DerObjectIdentifier regInfo_certReq = new DerObjectIdentifier("1.3.6.1.5.5.7.5.2.2");

		// Token: 0x04000608 RID: 1544
		public static readonly DerObjectIdentifier ct_encKeyWithID = new DerObjectIdentifier("1.2.840.113549.1.9.16.1.21");
	}
}
