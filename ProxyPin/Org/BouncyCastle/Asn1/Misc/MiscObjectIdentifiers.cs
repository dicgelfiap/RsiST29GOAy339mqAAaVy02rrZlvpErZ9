using System;

namespace Org.BouncyCastle.Asn1.Misc
{
	// Token: 0x02000184 RID: 388
	public abstract class MiscObjectIdentifiers
	{
		// Token: 0x04000904 RID: 2308
		public static readonly DerObjectIdentifier Netscape = new DerObjectIdentifier("2.16.840.1.113730.1");

		// Token: 0x04000905 RID: 2309
		public static readonly DerObjectIdentifier NetscapeCertType = MiscObjectIdentifiers.Netscape.Branch("1");

		// Token: 0x04000906 RID: 2310
		public static readonly DerObjectIdentifier NetscapeBaseUrl = MiscObjectIdentifiers.Netscape.Branch("2");

		// Token: 0x04000907 RID: 2311
		public static readonly DerObjectIdentifier NetscapeRevocationUrl = MiscObjectIdentifiers.Netscape.Branch("3");

		// Token: 0x04000908 RID: 2312
		public static readonly DerObjectIdentifier NetscapeCARevocationUrl = MiscObjectIdentifiers.Netscape.Branch("4");

		// Token: 0x04000909 RID: 2313
		public static readonly DerObjectIdentifier NetscapeRenewalUrl = MiscObjectIdentifiers.Netscape.Branch("7");

		// Token: 0x0400090A RID: 2314
		public static readonly DerObjectIdentifier NetscapeCAPolicyUrl = MiscObjectIdentifiers.Netscape.Branch("8");

		// Token: 0x0400090B RID: 2315
		public static readonly DerObjectIdentifier NetscapeSslServerName = MiscObjectIdentifiers.Netscape.Branch("12");

		// Token: 0x0400090C RID: 2316
		public static readonly DerObjectIdentifier NetscapeCertComment = MiscObjectIdentifiers.Netscape.Branch("13");

		// Token: 0x0400090D RID: 2317
		public static readonly DerObjectIdentifier Verisign = new DerObjectIdentifier("2.16.840.1.113733.1");

		// Token: 0x0400090E RID: 2318
		public static readonly DerObjectIdentifier VerisignCzagExtension = MiscObjectIdentifiers.Verisign.Branch("6.3");

		// Token: 0x0400090F RID: 2319
		public static readonly DerObjectIdentifier VerisignPrivate_6_9 = MiscObjectIdentifiers.Verisign.Branch("6.9");

		// Token: 0x04000910 RID: 2320
		public static readonly DerObjectIdentifier VerisignOnSiteJurisdictionHash = MiscObjectIdentifiers.Verisign.Branch("6.11");

		// Token: 0x04000911 RID: 2321
		public static readonly DerObjectIdentifier VerisignBitString_6_13 = MiscObjectIdentifiers.Verisign.Branch("6.13");

		// Token: 0x04000912 RID: 2322
		public static readonly DerObjectIdentifier VerisignDnbDunsNumber = MiscObjectIdentifiers.Verisign.Branch("6.15");

		// Token: 0x04000913 RID: 2323
		public static readonly DerObjectIdentifier VerisignIssStrongCrypto = MiscObjectIdentifiers.Verisign.Branch("8.1");

		// Token: 0x04000914 RID: 2324
		public static readonly string Novell = "2.16.840.1.113719";

		// Token: 0x04000915 RID: 2325
		public static readonly DerObjectIdentifier NovellSecurityAttribs = new DerObjectIdentifier(MiscObjectIdentifiers.Novell + ".1.9.4.1");

		// Token: 0x04000916 RID: 2326
		public static readonly string Entrust = "1.2.840.113533.7";

		// Token: 0x04000917 RID: 2327
		public static readonly DerObjectIdentifier EntrustVersionExtension = new DerObjectIdentifier(MiscObjectIdentifiers.Entrust + ".65.0");

		// Token: 0x04000918 RID: 2328
		public static readonly DerObjectIdentifier cast5CBC = new DerObjectIdentifier(MiscObjectIdentifiers.Entrust + ".66.10");

		// Token: 0x04000919 RID: 2329
		public static readonly DerObjectIdentifier as_sys_sec_alg_ideaCBC = new DerObjectIdentifier("1.3.6.1.4.1.188.7.1.1.2");

		// Token: 0x0400091A RID: 2330
		public static readonly DerObjectIdentifier cryptlib = new DerObjectIdentifier("1.3.6.1.4.1.3029");

		// Token: 0x0400091B RID: 2331
		public static readonly DerObjectIdentifier cryptlib_algorithm = MiscObjectIdentifiers.cryptlib.Branch("1");

		// Token: 0x0400091C RID: 2332
		public static readonly DerObjectIdentifier cryptlib_algorithm_blowfish_ECB = MiscObjectIdentifiers.cryptlib_algorithm.Branch("1.1");

		// Token: 0x0400091D RID: 2333
		public static readonly DerObjectIdentifier cryptlib_algorithm_blowfish_CBC = MiscObjectIdentifiers.cryptlib_algorithm.Branch("1.2");

		// Token: 0x0400091E RID: 2334
		public static readonly DerObjectIdentifier cryptlib_algorithm_blowfish_CFB = MiscObjectIdentifiers.cryptlib_algorithm.Branch("1.3");

		// Token: 0x0400091F RID: 2335
		public static readonly DerObjectIdentifier cryptlib_algorithm_blowfish_OFB = MiscObjectIdentifiers.cryptlib_algorithm.Branch("1.4");

		// Token: 0x04000920 RID: 2336
		public static readonly DerObjectIdentifier blake2 = new DerObjectIdentifier("1.3.6.1.4.1.1722.12.2");

		// Token: 0x04000921 RID: 2337
		public static readonly DerObjectIdentifier id_blake2b160 = MiscObjectIdentifiers.blake2.Branch("1.5");

		// Token: 0x04000922 RID: 2338
		public static readonly DerObjectIdentifier id_blake2b256 = MiscObjectIdentifiers.blake2.Branch("1.8");

		// Token: 0x04000923 RID: 2339
		public static readonly DerObjectIdentifier id_blake2b384 = MiscObjectIdentifiers.blake2.Branch("1.12");

		// Token: 0x04000924 RID: 2340
		public static readonly DerObjectIdentifier id_blake2b512 = MiscObjectIdentifiers.blake2.Branch("1.16");

		// Token: 0x04000925 RID: 2341
		public static readonly DerObjectIdentifier id_blake2s128 = MiscObjectIdentifiers.blake2.Branch("2.4");

		// Token: 0x04000926 RID: 2342
		public static readonly DerObjectIdentifier id_blake2s160 = MiscObjectIdentifiers.blake2.Branch("2.5");

		// Token: 0x04000927 RID: 2343
		public static readonly DerObjectIdentifier id_blake2s224 = MiscObjectIdentifiers.blake2.Branch("2.7");

		// Token: 0x04000928 RID: 2344
		public static readonly DerObjectIdentifier id_blake2s256 = MiscObjectIdentifiers.blake2.Branch("2.8");

		// Token: 0x04000929 RID: 2345
		public static readonly DerObjectIdentifier id_scrypt = new DerObjectIdentifier("1.3.6.1.4.1.11591.4.11");
	}
}
