using System;

namespace Org.BouncyCastle.Asn1.Nist
{
	// Token: 0x0200018B RID: 395
	public sealed class NistObjectIdentifiers
	{
		// Token: 0x06000D0E RID: 3342 RVA: 0x00052AF0 File Offset: 0x00052AF0
		private NistObjectIdentifiers()
		{
		}

		// Token: 0x04000938 RID: 2360
		public static readonly DerObjectIdentifier NistAlgorithm = new DerObjectIdentifier("2.16.840.1.101.3.4");

		// Token: 0x04000939 RID: 2361
		public static readonly DerObjectIdentifier HashAlgs = NistObjectIdentifiers.NistAlgorithm.Branch("2");

		// Token: 0x0400093A RID: 2362
		public static readonly DerObjectIdentifier IdSha256 = NistObjectIdentifiers.HashAlgs.Branch("1");

		// Token: 0x0400093B RID: 2363
		public static readonly DerObjectIdentifier IdSha384 = NistObjectIdentifiers.HashAlgs.Branch("2");

		// Token: 0x0400093C RID: 2364
		public static readonly DerObjectIdentifier IdSha512 = NistObjectIdentifiers.HashAlgs.Branch("3");

		// Token: 0x0400093D RID: 2365
		public static readonly DerObjectIdentifier IdSha224 = NistObjectIdentifiers.HashAlgs.Branch("4");

		// Token: 0x0400093E RID: 2366
		public static readonly DerObjectIdentifier IdSha512_224 = NistObjectIdentifiers.HashAlgs.Branch("5");

		// Token: 0x0400093F RID: 2367
		public static readonly DerObjectIdentifier IdSha512_256 = NistObjectIdentifiers.HashAlgs.Branch("6");

		// Token: 0x04000940 RID: 2368
		public static readonly DerObjectIdentifier IdSha3_224 = NistObjectIdentifiers.HashAlgs.Branch("7");

		// Token: 0x04000941 RID: 2369
		public static readonly DerObjectIdentifier IdSha3_256 = NistObjectIdentifiers.HashAlgs.Branch("8");

		// Token: 0x04000942 RID: 2370
		public static readonly DerObjectIdentifier IdSha3_384 = NistObjectIdentifiers.HashAlgs.Branch("9");

		// Token: 0x04000943 RID: 2371
		public static readonly DerObjectIdentifier IdSha3_512 = NistObjectIdentifiers.HashAlgs.Branch("10");

		// Token: 0x04000944 RID: 2372
		public static readonly DerObjectIdentifier IdShake128 = NistObjectIdentifiers.HashAlgs.Branch("11");

		// Token: 0x04000945 RID: 2373
		public static readonly DerObjectIdentifier IdShake256 = NistObjectIdentifiers.HashAlgs.Branch("12");

		// Token: 0x04000946 RID: 2374
		public static readonly DerObjectIdentifier IdHMacWithSha3_224 = NistObjectIdentifiers.HashAlgs.Branch("13");

		// Token: 0x04000947 RID: 2375
		public static readonly DerObjectIdentifier IdHMacWithSha3_256 = NistObjectIdentifiers.HashAlgs.Branch("14");

		// Token: 0x04000948 RID: 2376
		public static readonly DerObjectIdentifier IdHMacWithSha3_384 = NistObjectIdentifiers.HashAlgs.Branch("15");

		// Token: 0x04000949 RID: 2377
		public static readonly DerObjectIdentifier IdHMacWithSha3_512 = NistObjectIdentifiers.HashAlgs.Branch("16");

		// Token: 0x0400094A RID: 2378
		public static readonly DerObjectIdentifier IdShake128Len = NistObjectIdentifiers.HashAlgs.Branch("17");

		// Token: 0x0400094B RID: 2379
		public static readonly DerObjectIdentifier IdShake256Len = NistObjectIdentifiers.HashAlgs.Branch("18");

		// Token: 0x0400094C RID: 2380
		public static readonly DerObjectIdentifier IdKmacWithShake128 = NistObjectIdentifiers.HashAlgs.Branch("19");

		// Token: 0x0400094D RID: 2381
		public static readonly DerObjectIdentifier IdKmacWithShake256 = NistObjectIdentifiers.HashAlgs.Branch("20");

		// Token: 0x0400094E RID: 2382
		public static readonly DerObjectIdentifier Aes = new DerObjectIdentifier(NistObjectIdentifiers.NistAlgorithm + ".1");

		// Token: 0x0400094F RID: 2383
		public static readonly DerObjectIdentifier IdAes128Ecb = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".1");

		// Token: 0x04000950 RID: 2384
		public static readonly DerObjectIdentifier IdAes128Cbc = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".2");

		// Token: 0x04000951 RID: 2385
		public static readonly DerObjectIdentifier IdAes128Ofb = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".3");

		// Token: 0x04000952 RID: 2386
		public static readonly DerObjectIdentifier IdAes128Cfb = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".4");

		// Token: 0x04000953 RID: 2387
		public static readonly DerObjectIdentifier IdAes128Wrap = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".5");

		// Token: 0x04000954 RID: 2388
		public static readonly DerObjectIdentifier IdAes128Gcm = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".6");

		// Token: 0x04000955 RID: 2389
		public static readonly DerObjectIdentifier IdAes128Ccm = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".7");

		// Token: 0x04000956 RID: 2390
		public static readonly DerObjectIdentifier IdAes192Ecb = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".21");

		// Token: 0x04000957 RID: 2391
		public static readonly DerObjectIdentifier IdAes192Cbc = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".22");

		// Token: 0x04000958 RID: 2392
		public static readonly DerObjectIdentifier IdAes192Ofb = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".23");

		// Token: 0x04000959 RID: 2393
		public static readonly DerObjectIdentifier IdAes192Cfb = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".24");

		// Token: 0x0400095A RID: 2394
		public static readonly DerObjectIdentifier IdAes192Wrap = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".25");

		// Token: 0x0400095B RID: 2395
		public static readonly DerObjectIdentifier IdAes192Gcm = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".26");

		// Token: 0x0400095C RID: 2396
		public static readonly DerObjectIdentifier IdAes192Ccm = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".27");

		// Token: 0x0400095D RID: 2397
		public static readonly DerObjectIdentifier IdAes256Ecb = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".41");

		// Token: 0x0400095E RID: 2398
		public static readonly DerObjectIdentifier IdAes256Cbc = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".42");

		// Token: 0x0400095F RID: 2399
		public static readonly DerObjectIdentifier IdAes256Ofb = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".43");

		// Token: 0x04000960 RID: 2400
		public static readonly DerObjectIdentifier IdAes256Cfb = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".44");

		// Token: 0x04000961 RID: 2401
		public static readonly DerObjectIdentifier IdAes256Wrap = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".45");

		// Token: 0x04000962 RID: 2402
		public static readonly DerObjectIdentifier IdAes256Gcm = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".46");

		// Token: 0x04000963 RID: 2403
		public static readonly DerObjectIdentifier IdAes256Ccm = new DerObjectIdentifier(NistObjectIdentifiers.Aes + ".47");

		// Token: 0x04000964 RID: 2404
		public static readonly DerObjectIdentifier IdDsaWithSha2 = new DerObjectIdentifier(NistObjectIdentifiers.NistAlgorithm + ".3");

		// Token: 0x04000965 RID: 2405
		public static readonly DerObjectIdentifier DsaWithSha224 = new DerObjectIdentifier(NistObjectIdentifiers.IdDsaWithSha2 + ".1");

		// Token: 0x04000966 RID: 2406
		public static readonly DerObjectIdentifier DsaWithSha256 = new DerObjectIdentifier(NistObjectIdentifiers.IdDsaWithSha2 + ".2");

		// Token: 0x04000967 RID: 2407
		public static readonly DerObjectIdentifier DsaWithSha384 = new DerObjectIdentifier(NistObjectIdentifiers.IdDsaWithSha2 + ".3");

		// Token: 0x04000968 RID: 2408
		public static readonly DerObjectIdentifier DsaWithSha512 = new DerObjectIdentifier(NistObjectIdentifiers.IdDsaWithSha2 + ".4");

		// Token: 0x04000969 RID: 2409
		public static readonly DerObjectIdentifier IdDsaWithSha3_224 = new DerObjectIdentifier(NistObjectIdentifiers.IdDsaWithSha2 + ".5");

		// Token: 0x0400096A RID: 2410
		public static readonly DerObjectIdentifier IdDsaWithSha3_256 = new DerObjectIdentifier(NistObjectIdentifiers.IdDsaWithSha2 + ".6");

		// Token: 0x0400096B RID: 2411
		public static readonly DerObjectIdentifier IdDsaWithSha3_384 = new DerObjectIdentifier(NistObjectIdentifiers.IdDsaWithSha2 + ".7");

		// Token: 0x0400096C RID: 2412
		public static readonly DerObjectIdentifier IdDsaWithSha3_512 = new DerObjectIdentifier(NistObjectIdentifiers.IdDsaWithSha2 + ".8");

		// Token: 0x0400096D RID: 2413
		public static readonly DerObjectIdentifier IdEcdsaWithSha3_224 = new DerObjectIdentifier(NistObjectIdentifiers.IdDsaWithSha2 + ".9");

		// Token: 0x0400096E RID: 2414
		public static readonly DerObjectIdentifier IdEcdsaWithSha3_256 = new DerObjectIdentifier(NistObjectIdentifiers.IdDsaWithSha2 + ".10");

		// Token: 0x0400096F RID: 2415
		public static readonly DerObjectIdentifier IdEcdsaWithSha3_384 = new DerObjectIdentifier(NistObjectIdentifiers.IdDsaWithSha2 + ".11");

		// Token: 0x04000970 RID: 2416
		public static readonly DerObjectIdentifier IdEcdsaWithSha3_512 = new DerObjectIdentifier(NistObjectIdentifiers.IdDsaWithSha2 + ".12");

		// Token: 0x04000971 RID: 2417
		public static readonly DerObjectIdentifier IdRsassaPkcs1V15WithSha3_224 = new DerObjectIdentifier(NistObjectIdentifiers.IdDsaWithSha2 + ".13");

		// Token: 0x04000972 RID: 2418
		public static readonly DerObjectIdentifier IdRsassaPkcs1V15WithSha3_256 = new DerObjectIdentifier(NistObjectIdentifiers.IdDsaWithSha2 + ".14");

		// Token: 0x04000973 RID: 2419
		public static readonly DerObjectIdentifier IdRsassaPkcs1V15WithSha3_384 = new DerObjectIdentifier(NistObjectIdentifiers.IdDsaWithSha2 + ".15");

		// Token: 0x04000974 RID: 2420
		public static readonly DerObjectIdentifier IdRsassaPkcs1V15WithSha3_512 = new DerObjectIdentifier(NistObjectIdentifiers.IdDsaWithSha2 + ".16");
	}
}
