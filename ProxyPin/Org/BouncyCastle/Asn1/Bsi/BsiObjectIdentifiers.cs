using System;

namespace Org.BouncyCastle.Asn1.Bsi
{
	// Token: 0x020000CC RID: 204
	public abstract class BsiObjectIdentifiers
	{
		// Token: 0x040005C1 RID: 1473
		public static readonly DerObjectIdentifier bsi_de = new DerObjectIdentifier("0.4.0.127.0.7");

		// Token: 0x040005C2 RID: 1474
		public static readonly DerObjectIdentifier id_ecc = BsiObjectIdentifiers.bsi_de.Branch("1.1");

		// Token: 0x040005C3 RID: 1475
		public static readonly DerObjectIdentifier ecdsa_plain_signatures = BsiObjectIdentifiers.id_ecc.Branch("4.1");

		// Token: 0x040005C4 RID: 1476
		public static readonly DerObjectIdentifier ecdsa_plain_SHA1 = BsiObjectIdentifiers.ecdsa_plain_signatures.Branch("1");

		// Token: 0x040005C5 RID: 1477
		public static readonly DerObjectIdentifier ecdsa_plain_SHA224 = BsiObjectIdentifiers.ecdsa_plain_signatures.Branch("2");

		// Token: 0x040005C6 RID: 1478
		public static readonly DerObjectIdentifier ecdsa_plain_SHA256 = BsiObjectIdentifiers.ecdsa_plain_signatures.Branch("3");

		// Token: 0x040005C7 RID: 1479
		public static readonly DerObjectIdentifier ecdsa_plain_SHA384 = BsiObjectIdentifiers.ecdsa_plain_signatures.Branch("4");

		// Token: 0x040005C8 RID: 1480
		public static readonly DerObjectIdentifier ecdsa_plain_SHA512 = BsiObjectIdentifiers.ecdsa_plain_signatures.Branch("5");

		// Token: 0x040005C9 RID: 1481
		public static readonly DerObjectIdentifier ecdsa_plain_RIPEMD160 = BsiObjectIdentifiers.ecdsa_plain_signatures.Branch("6");

		// Token: 0x040005CA RID: 1482
		public static readonly DerObjectIdentifier algorithm = BsiObjectIdentifiers.bsi_de.Branch("1");

		// Token: 0x040005CB RID: 1483
		public static readonly DerObjectIdentifier ecka_eg = BsiObjectIdentifiers.id_ecc.Branch("5.1");

		// Token: 0x040005CC RID: 1484
		public static readonly DerObjectIdentifier ecka_eg_X963kdf = BsiObjectIdentifiers.ecka_eg.Branch("1");

		// Token: 0x040005CD RID: 1485
		public static readonly DerObjectIdentifier ecka_eg_X963kdf_SHA1 = BsiObjectIdentifiers.ecka_eg_X963kdf.Branch("1");

		// Token: 0x040005CE RID: 1486
		public static readonly DerObjectIdentifier ecka_eg_X963kdf_SHA224 = BsiObjectIdentifiers.ecka_eg_X963kdf.Branch("2");

		// Token: 0x040005CF RID: 1487
		public static readonly DerObjectIdentifier ecka_eg_X963kdf_SHA256 = BsiObjectIdentifiers.ecka_eg_X963kdf.Branch("3");

		// Token: 0x040005D0 RID: 1488
		public static readonly DerObjectIdentifier ecka_eg_X963kdf_SHA384 = BsiObjectIdentifiers.ecka_eg_X963kdf.Branch("4");

		// Token: 0x040005D1 RID: 1489
		public static readonly DerObjectIdentifier ecka_eg_X963kdf_SHA512 = BsiObjectIdentifiers.ecka_eg_X963kdf.Branch("5");

		// Token: 0x040005D2 RID: 1490
		public static readonly DerObjectIdentifier ecka_eg_X963kdf_RIPEMD160 = BsiObjectIdentifiers.ecka_eg_X963kdf.Branch("6");

		// Token: 0x040005D3 RID: 1491
		public static readonly DerObjectIdentifier ecka_eg_SessionKDF = BsiObjectIdentifiers.ecka_eg.Branch("2");

		// Token: 0x040005D4 RID: 1492
		public static readonly DerObjectIdentifier ecka_eg_SessionKDF_3DES = BsiObjectIdentifiers.ecka_eg_SessionKDF.Branch("1");

		// Token: 0x040005D5 RID: 1493
		public static readonly DerObjectIdentifier ecka_eg_SessionKDF_AES128 = BsiObjectIdentifiers.ecka_eg_SessionKDF.Branch("2");

		// Token: 0x040005D6 RID: 1494
		public static readonly DerObjectIdentifier ecka_eg_SessionKDF_AES192 = BsiObjectIdentifiers.ecka_eg_SessionKDF.Branch("3");

		// Token: 0x040005D7 RID: 1495
		public static readonly DerObjectIdentifier ecka_eg_SessionKDF_AES256 = BsiObjectIdentifiers.ecka_eg_SessionKDF.Branch("4");
	}
}
