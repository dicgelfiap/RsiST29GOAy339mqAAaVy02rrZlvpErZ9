using System;

namespace Org.BouncyCastle.Asn1.BC
{
	// Token: 0x020000C8 RID: 200
	public abstract class BCObjectIdentifiers
	{
		// Token: 0x04000597 RID: 1431
		public static readonly DerObjectIdentifier bc = new DerObjectIdentifier("1.3.6.1.4.1.22554");

		// Token: 0x04000598 RID: 1432
		public static readonly DerObjectIdentifier bc_pbe = BCObjectIdentifiers.bc.Branch("1");

		// Token: 0x04000599 RID: 1433
		public static readonly DerObjectIdentifier bc_pbe_sha1 = BCObjectIdentifiers.bc_pbe.Branch("1");

		// Token: 0x0400059A RID: 1434
		public static readonly DerObjectIdentifier bc_pbe_sha256 = BCObjectIdentifiers.bc_pbe.Branch("2.1");

		// Token: 0x0400059B RID: 1435
		public static readonly DerObjectIdentifier bc_pbe_sha384 = BCObjectIdentifiers.bc_pbe.Branch("2.2");

		// Token: 0x0400059C RID: 1436
		public static readonly DerObjectIdentifier bc_pbe_sha512 = BCObjectIdentifiers.bc_pbe.Branch("2.3");

		// Token: 0x0400059D RID: 1437
		public static readonly DerObjectIdentifier bc_pbe_sha224 = BCObjectIdentifiers.bc_pbe.Branch("2.4");

		// Token: 0x0400059E RID: 1438
		public static readonly DerObjectIdentifier bc_pbe_sha1_pkcs5 = BCObjectIdentifiers.bc_pbe_sha1.Branch("1");

		// Token: 0x0400059F RID: 1439
		public static readonly DerObjectIdentifier bc_pbe_sha1_pkcs12 = BCObjectIdentifiers.bc_pbe_sha1.Branch("2");

		// Token: 0x040005A0 RID: 1440
		public static readonly DerObjectIdentifier bc_pbe_sha256_pkcs5 = BCObjectIdentifiers.bc_pbe_sha256.Branch("1");

		// Token: 0x040005A1 RID: 1441
		public static readonly DerObjectIdentifier bc_pbe_sha256_pkcs12 = BCObjectIdentifiers.bc_pbe_sha256.Branch("2");

		// Token: 0x040005A2 RID: 1442
		public static readonly DerObjectIdentifier bc_pbe_sha1_pkcs12_aes128_cbc = BCObjectIdentifiers.bc_pbe_sha1_pkcs12.Branch("1.2");

		// Token: 0x040005A3 RID: 1443
		public static readonly DerObjectIdentifier bc_pbe_sha1_pkcs12_aes192_cbc = BCObjectIdentifiers.bc_pbe_sha1_pkcs12.Branch("1.22");

		// Token: 0x040005A4 RID: 1444
		public static readonly DerObjectIdentifier bc_pbe_sha1_pkcs12_aes256_cbc = BCObjectIdentifiers.bc_pbe_sha1_pkcs12.Branch("1.42");

		// Token: 0x040005A5 RID: 1445
		public static readonly DerObjectIdentifier bc_pbe_sha256_pkcs12_aes128_cbc = BCObjectIdentifiers.bc_pbe_sha256_pkcs12.Branch("1.2");

		// Token: 0x040005A6 RID: 1446
		public static readonly DerObjectIdentifier bc_pbe_sha256_pkcs12_aes192_cbc = BCObjectIdentifiers.bc_pbe_sha256_pkcs12.Branch("1.22");

		// Token: 0x040005A7 RID: 1447
		public static readonly DerObjectIdentifier bc_pbe_sha256_pkcs12_aes256_cbc = BCObjectIdentifiers.bc_pbe_sha256_pkcs12.Branch("1.42");

		// Token: 0x040005A8 RID: 1448
		public static readonly DerObjectIdentifier bc_sig = BCObjectIdentifiers.bc.Branch("2");

		// Token: 0x040005A9 RID: 1449
		public static readonly DerObjectIdentifier sphincs256 = BCObjectIdentifiers.bc_sig.Branch("1");

		// Token: 0x040005AA RID: 1450
		public static readonly DerObjectIdentifier sphincs256_with_BLAKE512 = BCObjectIdentifiers.sphincs256.Branch("1");

		// Token: 0x040005AB RID: 1451
		public static readonly DerObjectIdentifier sphincs256_with_SHA512 = BCObjectIdentifiers.sphincs256.Branch("2");

		// Token: 0x040005AC RID: 1452
		public static readonly DerObjectIdentifier sphincs256_with_SHA3_512 = BCObjectIdentifiers.sphincs256.Branch("3");

		// Token: 0x040005AD RID: 1453
		public static readonly DerObjectIdentifier xmss = BCObjectIdentifiers.bc_sig.Branch("2");

		// Token: 0x040005AE RID: 1454
		public static readonly DerObjectIdentifier xmss_with_SHA256 = BCObjectIdentifiers.xmss.Branch("1");

		// Token: 0x040005AF RID: 1455
		public static readonly DerObjectIdentifier xmss_with_SHA512 = BCObjectIdentifiers.xmss.Branch("2");

		// Token: 0x040005B0 RID: 1456
		public static readonly DerObjectIdentifier xmss_with_SHAKE128 = BCObjectIdentifiers.xmss.Branch("3");

		// Token: 0x040005B1 RID: 1457
		public static readonly DerObjectIdentifier xmss_with_SHAKE256 = BCObjectIdentifiers.xmss.Branch("4");

		// Token: 0x040005B2 RID: 1458
		public static readonly DerObjectIdentifier xmss_mt = BCObjectIdentifiers.bc_sig.Branch("3");

		// Token: 0x040005B3 RID: 1459
		public static readonly DerObjectIdentifier xmss_mt_with_SHA256 = BCObjectIdentifiers.xmss_mt.Branch("1");

		// Token: 0x040005B4 RID: 1460
		public static readonly DerObjectIdentifier xmss_mt_with_SHA512 = BCObjectIdentifiers.xmss_mt.Branch("2");

		// Token: 0x040005B5 RID: 1461
		public static readonly DerObjectIdentifier xmss_mt_with_SHAKE128 = BCObjectIdentifiers.xmss_mt.Branch("3");

		// Token: 0x040005B6 RID: 1462
		public static readonly DerObjectIdentifier xmss_mt_with_SHAKE256 = BCObjectIdentifiers.xmss_mt.Branch("4");

		// Token: 0x040005B7 RID: 1463
		public static readonly DerObjectIdentifier bc_exch = BCObjectIdentifiers.bc.Branch("3");

		// Token: 0x040005B8 RID: 1464
		public static readonly DerObjectIdentifier newHope = BCObjectIdentifiers.bc_exch.Branch("1");

		// Token: 0x040005B9 RID: 1465
		public static readonly DerObjectIdentifier bc_ext = BCObjectIdentifiers.bc.Branch("4");

		// Token: 0x040005BA RID: 1466
		public static readonly DerObjectIdentifier linkedCertificate = BCObjectIdentifiers.bc_ext.Branch("1");
	}
}
