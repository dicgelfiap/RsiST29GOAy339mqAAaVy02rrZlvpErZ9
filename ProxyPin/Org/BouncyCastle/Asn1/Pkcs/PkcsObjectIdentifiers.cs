using System;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020001B4 RID: 436
	public abstract class PkcsObjectIdentifiers
	{
		// Token: 0x040009F3 RID: 2547
		public const string Pkcs1 = "1.2.840.113549.1.1";

		// Token: 0x040009F4 RID: 2548
		public const string Pkcs3 = "1.2.840.113549.1.3";

		// Token: 0x040009F5 RID: 2549
		public const string Pkcs5 = "1.2.840.113549.1.5";

		// Token: 0x040009F6 RID: 2550
		public const string EncryptionAlgorithm = "1.2.840.113549.3";

		// Token: 0x040009F7 RID: 2551
		public const string DigestAlgorithm = "1.2.840.113549.2";

		// Token: 0x040009F8 RID: 2552
		public const string Pkcs7 = "1.2.840.113549.1.7";

		// Token: 0x040009F9 RID: 2553
		public const string Pkcs9 = "1.2.840.113549.1.9";

		// Token: 0x040009FA RID: 2554
		public const string CertTypes = "1.2.840.113549.1.9.22";

		// Token: 0x040009FB RID: 2555
		public const string CrlTypes = "1.2.840.113549.1.9.23";

		// Token: 0x040009FC RID: 2556
		public const string IdCT = "1.2.840.113549.1.9.16.1";

		// Token: 0x040009FD RID: 2557
		public const string IdCti = "1.2.840.113549.1.9.16.6";

		// Token: 0x040009FE RID: 2558
		public const string IdAA = "1.2.840.113549.1.9.16.2";

		// Token: 0x040009FF RID: 2559
		public const string IdSpq = "1.2.840.113549.1.9.16.5";

		// Token: 0x04000A00 RID: 2560
		public const string Pkcs12 = "1.2.840.113549.1.12";

		// Token: 0x04000A01 RID: 2561
		public const string BagTypes = "1.2.840.113549.1.12.10.1";

		// Token: 0x04000A02 RID: 2562
		public const string Pkcs12PbeIds = "1.2.840.113549.1.12.1";

		// Token: 0x04000A03 RID: 2563
		internal static readonly DerObjectIdentifier Pkcs1Oid = new DerObjectIdentifier("1.2.840.113549.1.1");

		// Token: 0x04000A04 RID: 2564
		public static readonly DerObjectIdentifier RsaEncryption = PkcsObjectIdentifiers.Pkcs1Oid.Branch("1");

		// Token: 0x04000A05 RID: 2565
		public static readonly DerObjectIdentifier MD2WithRsaEncryption = PkcsObjectIdentifiers.Pkcs1Oid.Branch("2");

		// Token: 0x04000A06 RID: 2566
		public static readonly DerObjectIdentifier MD4WithRsaEncryption = PkcsObjectIdentifiers.Pkcs1Oid.Branch("3");

		// Token: 0x04000A07 RID: 2567
		public static readonly DerObjectIdentifier MD5WithRsaEncryption = PkcsObjectIdentifiers.Pkcs1Oid.Branch("4");

		// Token: 0x04000A08 RID: 2568
		public static readonly DerObjectIdentifier Sha1WithRsaEncryption = PkcsObjectIdentifiers.Pkcs1Oid.Branch("5");

		// Token: 0x04000A09 RID: 2569
		public static readonly DerObjectIdentifier SrsaOaepEncryptionSet = PkcsObjectIdentifiers.Pkcs1Oid.Branch("6");

		// Token: 0x04000A0A RID: 2570
		public static readonly DerObjectIdentifier IdRsaesOaep = PkcsObjectIdentifiers.Pkcs1Oid.Branch("7");

		// Token: 0x04000A0B RID: 2571
		public static readonly DerObjectIdentifier IdMgf1 = PkcsObjectIdentifiers.Pkcs1Oid.Branch("8");

		// Token: 0x04000A0C RID: 2572
		public static readonly DerObjectIdentifier IdPSpecified = PkcsObjectIdentifiers.Pkcs1Oid.Branch("9");

		// Token: 0x04000A0D RID: 2573
		public static readonly DerObjectIdentifier IdRsassaPss = PkcsObjectIdentifiers.Pkcs1Oid.Branch("10");

		// Token: 0x04000A0E RID: 2574
		public static readonly DerObjectIdentifier Sha256WithRsaEncryption = PkcsObjectIdentifiers.Pkcs1Oid.Branch("11");

		// Token: 0x04000A0F RID: 2575
		public static readonly DerObjectIdentifier Sha384WithRsaEncryption = PkcsObjectIdentifiers.Pkcs1Oid.Branch("12");

		// Token: 0x04000A10 RID: 2576
		public static readonly DerObjectIdentifier Sha512WithRsaEncryption = PkcsObjectIdentifiers.Pkcs1Oid.Branch("13");

		// Token: 0x04000A11 RID: 2577
		public static readonly DerObjectIdentifier Sha224WithRsaEncryption = PkcsObjectIdentifiers.Pkcs1Oid.Branch("14");

		// Token: 0x04000A12 RID: 2578
		public static readonly DerObjectIdentifier Sha512_224WithRSAEncryption = PkcsObjectIdentifiers.Pkcs1Oid.Branch("15");

		// Token: 0x04000A13 RID: 2579
		public static readonly DerObjectIdentifier Sha512_256WithRSAEncryption = PkcsObjectIdentifiers.Pkcs1Oid.Branch("16");

		// Token: 0x04000A14 RID: 2580
		public static readonly DerObjectIdentifier DhKeyAgreement = new DerObjectIdentifier("1.2.840.113549.1.3.1");

		// Token: 0x04000A15 RID: 2581
		public static readonly DerObjectIdentifier PbeWithMD2AndDesCbc = new DerObjectIdentifier("1.2.840.113549.1.5.1");

		// Token: 0x04000A16 RID: 2582
		public static readonly DerObjectIdentifier PbeWithMD2AndRC2Cbc = new DerObjectIdentifier("1.2.840.113549.1.5.4");

		// Token: 0x04000A17 RID: 2583
		public static readonly DerObjectIdentifier PbeWithMD5AndDesCbc = new DerObjectIdentifier("1.2.840.113549.1.5.3");

		// Token: 0x04000A18 RID: 2584
		public static readonly DerObjectIdentifier PbeWithMD5AndRC2Cbc = new DerObjectIdentifier("1.2.840.113549.1.5.6");

		// Token: 0x04000A19 RID: 2585
		public static readonly DerObjectIdentifier PbeWithSha1AndDesCbc = new DerObjectIdentifier("1.2.840.113549.1.5.10");

		// Token: 0x04000A1A RID: 2586
		public static readonly DerObjectIdentifier PbeWithSha1AndRC2Cbc = new DerObjectIdentifier("1.2.840.113549.1.5.11");

		// Token: 0x04000A1B RID: 2587
		public static readonly DerObjectIdentifier IdPbeS2 = new DerObjectIdentifier("1.2.840.113549.1.5.13");

		// Token: 0x04000A1C RID: 2588
		public static readonly DerObjectIdentifier IdPbkdf2 = new DerObjectIdentifier("1.2.840.113549.1.5.12");

		// Token: 0x04000A1D RID: 2589
		public static readonly DerObjectIdentifier DesEde3Cbc = new DerObjectIdentifier("1.2.840.113549.3.7");

		// Token: 0x04000A1E RID: 2590
		public static readonly DerObjectIdentifier RC2Cbc = new DerObjectIdentifier("1.2.840.113549.3.2");

		// Token: 0x04000A1F RID: 2591
		public static readonly DerObjectIdentifier rc4 = new DerObjectIdentifier("1.2.840.113549.3.4");

		// Token: 0x04000A20 RID: 2592
		public static readonly DerObjectIdentifier MD2 = new DerObjectIdentifier("1.2.840.113549.2.2");

		// Token: 0x04000A21 RID: 2593
		public static readonly DerObjectIdentifier MD4 = new DerObjectIdentifier("1.2.840.113549.2.4");

		// Token: 0x04000A22 RID: 2594
		public static readonly DerObjectIdentifier MD5 = new DerObjectIdentifier("1.2.840.113549.2.5");

		// Token: 0x04000A23 RID: 2595
		public static readonly DerObjectIdentifier IdHmacWithSha1 = new DerObjectIdentifier("1.2.840.113549.2.7");

		// Token: 0x04000A24 RID: 2596
		public static readonly DerObjectIdentifier IdHmacWithSha224 = new DerObjectIdentifier("1.2.840.113549.2.8");

		// Token: 0x04000A25 RID: 2597
		public static readonly DerObjectIdentifier IdHmacWithSha256 = new DerObjectIdentifier("1.2.840.113549.2.9");

		// Token: 0x04000A26 RID: 2598
		public static readonly DerObjectIdentifier IdHmacWithSha384 = new DerObjectIdentifier("1.2.840.113549.2.10");

		// Token: 0x04000A27 RID: 2599
		public static readonly DerObjectIdentifier IdHmacWithSha512 = new DerObjectIdentifier("1.2.840.113549.2.11");

		// Token: 0x04000A28 RID: 2600
		public static readonly DerObjectIdentifier Data = new DerObjectIdentifier("1.2.840.113549.1.7.1");

		// Token: 0x04000A29 RID: 2601
		public static readonly DerObjectIdentifier SignedData = new DerObjectIdentifier("1.2.840.113549.1.7.2");

		// Token: 0x04000A2A RID: 2602
		public static readonly DerObjectIdentifier EnvelopedData = new DerObjectIdentifier("1.2.840.113549.1.7.3");

		// Token: 0x04000A2B RID: 2603
		public static readonly DerObjectIdentifier SignedAndEnvelopedData = new DerObjectIdentifier("1.2.840.113549.1.7.4");

		// Token: 0x04000A2C RID: 2604
		public static readonly DerObjectIdentifier DigestedData = new DerObjectIdentifier("1.2.840.113549.1.7.5");

		// Token: 0x04000A2D RID: 2605
		public static readonly DerObjectIdentifier EncryptedData = new DerObjectIdentifier("1.2.840.113549.1.7.6");

		// Token: 0x04000A2E RID: 2606
		public static readonly DerObjectIdentifier Pkcs9AtEmailAddress = new DerObjectIdentifier("1.2.840.113549.1.9.1");

		// Token: 0x04000A2F RID: 2607
		public static readonly DerObjectIdentifier Pkcs9AtUnstructuredName = new DerObjectIdentifier("1.2.840.113549.1.9.2");

		// Token: 0x04000A30 RID: 2608
		public static readonly DerObjectIdentifier Pkcs9AtContentType = new DerObjectIdentifier("1.2.840.113549.1.9.3");

		// Token: 0x04000A31 RID: 2609
		public static readonly DerObjectIdentifier Pkcs9AtMessageDigest = new DerObjectIdentifier("1.2.840.113549.1.9.4");

		// Token: 0x04000A32 RID: 2610
		public static readonly DerObjectIdentifier Pkcs9AtSigningTime = new DerObjectIdentifier("1.2.840.113549.1.9.5");

		// Token: 0x04000A33 RID: 2611
		public static readonly DerObjectIdentifier Pkcs9AtCounterSignature = new DerObjectIdentifier("1.2.840.113549.1.9.6");

		// Token: 0x04000A34 RID: 2612
		public static readonly DerObjectIdentifier Pkcs9AtChallengePassword = new DerObjectIdentifier("1.2.840.113549.1.9.7");

		// Token: 0x04000A35 RID: 2613
		public static readonly DerObjectIdentifier Pkcs9AtUnstructuredAddress = new DerObjectIdentifier("1.2.840.113549.1.9.8");

		// Token: 0x04000A36 RID: 2614
		public static readonly DerObjectIdentifier Pkcs9AtExtendedCertificateAttributes = new DerObjectIdentifier("1.2.840.113549.1.9.9");

		// Token: 0x04000A37 RID: 2615
		public static readonly DerObjectIdentifier Pkcs9AtSigningDescription = new DerObjectIdentifier("1.2.840.113549.1.9.13");

		// Token: 0x04000A38 RID: 2616
		public static readonly DerObjectIdentifier Pkcs9AtExtensionRequest = new DerObjectIdentifier("1.2.840.113549.1.9.14");

		// Token: 0x04000A39 RID: 2617
		public static readonly DerObjectIdentifier Pkcs9AtSmimeCapabilities = new DerObjectIdentifier("1.2.840.113549.1.9.15");

		// Token: 0x04000A3A RID: 2618
		public static readonly DerObjectIdentifier IdSmime = new DerObjectIdentifier("1.2.840.113549.1.9.16");

		// Token: 0x04000A3B RID: 2619
		public static readonly DerObjectIdentifier Pkcs9AtFriendlyName = new DerObjectIdentifier("1.2.840.113549.1.9.20");

		// Token: 0x04000A3C RID: 2620
		public static readonly DerObjectIdentifier Pkcs9AtLocalKeyID = new DerObjectIdentifier("1.2.840.113549.1.9.21");

		// Token: 0x04000A3D RID: 2621
		[Obsolete("Use X509Certificate instead")]
		public static readonly DerObjectIdentifier X509CertType = new DerObjectIdentifier("1.2.840.113549.1.9.22.1");

		// Token: 0x04000A3E RID: 2622
		public static readonly DerObjectIdentifier X509Certificate = new DerObjectIdentifier("1.2.840.113549.1.9.22.1");

		// Token: 0x04000A3F RID: 2623
		public static readonly DerObjectIdentifier SdsiCertificate = new DerObjectIdentifier("1.2.840.113549.1.9.22.2");

		// Token: 0x04000A40 RID: 2624
		public static readonly DerObjectIdentifier X509Crl = new DerObjectIdentifier("1.2.840.113549.1.9.23.1");

		// Token: 0x04000A41 RID: 2625
		public static readonly DerObjectIdentifier IdAlg = PkcsObjectIdentifiers.IdSmime.Branch("3");

		// Token: 0x04000A42 RID: 2626
		public static readonly DerObjectIdentifier IdAlgEsdh = PkcsObjectIdentifiers.IdAlg.Branch("5");

		// Token: 0x04000A43 RID: 2627
		public static readonly DerObjectIdentifier IdAlgCms3DesWrap = PkcsObjectIdentifiers.IdAlg.Branch("6");

		// Token: 0x04000A44 RID: 2628
		public static readonly DerObjectIdentifier IdAlgCmsRC2Wrap = PkcsObjectIdentifiers.IdAlg.Branch("7");

		// Token: 0x04000A45 RID: 2629
		public static readonly DerObjectIdentifier IdAlgPwriKek = PkcsObjectIdentifiers.IdAlg.Branch("9");

		// Token: 0x04000A46 RID: 2630
		public static readonly DerObjectIdentifier IdAlgSsdh = PkcsObjectIdentifiers.IdAlg.Branch("10");

		// Token: 0x04000A47 RID: 2631
		public static readonly DerObjectIdentifier IdRsaKem = PkcsObjectIdentifiers.IdAlg.Branch("14");

		// Token: 0x04000A48 RID: 2632
		public static readonly DerObjectIdentifier IdAlgAeadChaCha20Poly1305 = PkcsObjectIdentifiers.IdAlg.Branch("18");

		// Token: 0x04000A49 RID: 2633
		public static readonly DerObjectIdentifier PreferSignedData = PkcsObjectIdentifiers.Pkcs9AtSmimeCapabilities.Branch("1");

		// Token: 0x04000A4A RID: 2634
		public static readonly DerObjectIdentifier CannotDecryptAny = PkcsObjectIdentifiers.Pkcs9AtSmimeCapabilities.Branch("2");

		// Token: 0x04000A4B RID: 2635
		public static readonly DerObjectIdentifier SmimeCapabilitiesVersions = PkcsObjectIdentifiers.Pkcs9AtSmimeCapabilities.Branch("3");

		// Token: 0x04000A4C RID: 2636
		public static readonly DerObjectIdentifier IdAAReceiptRequest = PkcsObjectIdentifiers.IdSmime.Branch("2.1");

		// Token: 0x04000A4D RID: 2637
		public static readonly DerObjectIdentifier IdCTAuthData = new DerObjectIdentifier("1.2.840.113549.1.9.16.1.2");

		// Token: 0x04000A4E RID: 2638
		public static readonly DerObjectIdentifier IdCTTstInfo = new DerObjectIdentifier("1.2.840.113549.1.9.16.1.4");

		// Token: 0x04000A4F RID: 2639
		public static readonly DerObjectIdentifier IdCTCompressedData = new DerObjectIdentifier("1.2.840.113549.1.9.16.1.9");

		// Token: 0x04000A50 RID: 2640
		public static readonly DerObjectIdentifier IdCTAuthEnvelopedData = new DerObjectIdentifier("1.2.840.113549.1.9.16.1.23");

		// Token: 0x04000A51 RID: 2641
		public static readonly DerObjectIdentifier IdCTTimestampedData = new DerObjectIdentifier("1.2.840.113549.1.9.16.1.31");

		// Token: 0x04000A52 RID: 2642
		public static readonly DerObjectIdentifier IdCtiEtsProofOfOrigin = new DerObjectIdentifier("1.2.840.113549.1.9.16.6.1");

		// Token: 0x04000A53 RID: 2643
		public static readonly DerObjectIdentifier IdCtiEtsProofOfReceipt = new DerObjectIdentifier("1.2.840.113549.1.9.16.6.2");

		// Token: 0x04000A54 RID: 2644
		public static readonly DerObjectIdentifier IdCtiEtsProofOfDelivery = new DerObjectIdentifier("1.2.840.113549.1.9.16.6.3");

		// Token: 0x04000A55 RID: 2645
		public static readonly DerObjectIdentifier IdCtiEtsProofOfSender = new DerObjectIdentifier("1.2.840.113549.1.9.16.6.4");

		// Token: 0x04000A56 RID: 2646
		public static readonly DerObjectIdentifier IdCtiEtsProofOfApproval = new DerObjectIdentifier("1.2.840.113549.1.9.16.6.5");

		// Token: 0x04000A57 RID: 2647
		public static readonly DerObjectIdentifier IdCtiEtsProofOfCreation = new DerObjectIdentifier("1.2.840.113549.1.9.16.6.6");

		// Token: 0x04000A58 RID: 2648
		public static readonly DerObjectIdentifier IdAAOid = new DerObjectIdentifier("1.2.840.113549.1.9.16.2");

		// Token: 0x04000A59 RID: 2649
		public static readonly DerObjectIdentifier IdAAContentHint = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.4");

		// Token: 0x04000A5A RID: 2650
		public static readonly DerObjectIdentifier IdAAMsgSigDigest = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.5");

		// Token: 0x04000A5B RID: 2651
		public static readonly DerObjectIdentifier IdAAContentReference = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.10");

		// Token: 0x04000A5C RID: 2652
		public static readonly DerObjectIdentifier IdAAEncrypKeyPref = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.11");

		// Token: 0x04000A5D RID: 2653
		public static readonly DerObjectIdentifier IdAASigningCertificate = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.12");

		// Token: 0x04000A5E RID: 2654
		public static readonly DerObjectIdentifier IdAASigningCertificateV2 = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.47");

		// Token: 0x04000A5F RID: 2655
		public static readonly DerObjectIdentifier IdAAContentIdentifier = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.7");

		// Token: 0x04000A60 RID: 2656
		public static readonly DerObjectIdentifier IdAASignatureTimeStampToken = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.14");

		// Token: 0x04000A61 RID: 2657
		public static readonly DerObjectIdentifier IdAAEtsSigPolicyID = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.15");

		// Token: 0x04000A62 RID: 2658
		public static readonly DerObjectIdentifier IdAAEtsCommitmentType = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.16");

		// Token: 0x04000A63 RID: 2659
		public static readonly DerObjectIdentifier IdAAEtsSignerLocation = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.17");

		// Token: 0x04000A64 RID: 2660
		public static readonly DerObjectIdentifier IdAAEtsSignerAttr = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.18");

		// Token: 0x04000A65 RID: 2661
		public static readonly DerObjectIdentifier IdAAEtsOtherSigCert = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.19");

		// Token: 0x04000A66 RID: 2662
		public static readonly DerObjectIdentifier IdAAEtsContentTimestamp = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.20");

		// Token: 0x04000A67 RID: 2663
		public static readonly DerObjectIdentifier IdAAEtsCertificateRefs = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.21");

		// Token: 0x04000A68 RID: 2664
		public static readonly DerObjectIdentifier IdAAEtsRevocationRefs = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.22");

		// Token: 0x04000A69 RID: 2665
		public static readonly DerObjectIdentifier IdAAEtsCertValues = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.23");

		// Token: 0x04000A6A RID: 2666
		public static readonly DerObjectIdentifier IdAAEtsRevocationValues = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.24");

		// Token: 0x04000A6B RID: 2667
		public static readonly DerObjectIdentifier IdAAEtsEscTimeStamp = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.25");

		// Token: 0x04000A6C RID: 2668
		public static readonly DerObjectIdentifier IdAAEtsCertCrlTimestamp = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.26");

		// Token: 0x04000A6D RID: 2669
		public static readonly DerObjectIdentifier IdAAEtsArchiveTimestamp = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.27");

		// Token: 0x04000A6E RID: 2670
		public static readonly DerObjectIdentifier IdAADecryptKeyID = PkcsObjectIdentifiers.IdAAOid.Branch("37");

		// Token: 0x04000A6F RID: 2671
		public static readonly DerObjectIdentifier IdAAImplCryptoAlgs = PkcsObjectIdentifiers.IdAAOid.Branch("38");

		// Token: 0x04000A70 RID: 2672
		public static readonly DerObjectIdentifier IdAAAsymmDecryptKeyID = PkcsObjectIdentifiers.IdAAOid.Branch("54");

		// Token: 0x04000A71 RID: 2673
		public static readonly DerObjectIdentifier IdAAImplCompressAlgs = PkcsObjectIdentifiers.IdAAOid.Branch("43");

		// Token: 0x04000A72 RID: 2674
		public static readonly DerObjectIdentifier IdAACommunityIdentifiers = PkcsObjectIdentifiers.IdAAOid.Branch("40");

		// Token: 0x04000A73 RID: 2675
		[Obsolete("Use 'IdAAEtsSigPolicyID' instead")]
		public static readonly DerObjectIdentifier IdAASigPolicyID = PkcsObjectIdentifiers.IdAAEtsSigPolicyID;

		// Token: 0x04000A74 RID: 2676
		[Obsolete("Use 'IdAAEtsCommitmentType' instead")]
		public static readonly DerObjectIdentifier IdAACommitmentType = PkcsObjectIdentifiers.IdAAEtsCommitmentType;

		// Token: 0x04000A75 RID: 2677
		[Obsolete("Use 'IdAAEtsSignerLocation' instead")]
		public static readonly DerObjectIdentifier IdAASignerLocation = PkcsObjectIdentifiers.IdAAEtsSignerLocation;

		// Token: 0x04000A76 RID: 2678
		[Obsolete("Use 'IdAAEtsOtherSigCert' instead")]
		public static readonly DerObjectIdentifier IdAAOtherSigCert = PkcsObjectIdentifiers.IdAAEtsOtherSigCert;

		// Token: 0x04000A77 RID: 2679
		public static readonly DerObjectIdentifier IdSpqEtsUri = new DerObjectIdentifier("1.2.840.113549.1.9.16.5.1");

		// Token: 0x04000A78 RID: 2680
		public static readonly DerObjectIdentifier IdSpqEtsUNotice = new DerObjectIdentifier("1.2.840.113549.1.9.16.5.2");

		// Token: 0x04000A79 RID: 2681
		public static readonly DerObjectIdentifier KeyBag = new DerObjectIdentifier("1.2.840.113549.1.12.10.1.1");

		// Token: 0x04000A7A RID: 2682
		public static readonly DerObjectIdentifier Pkcs8ShroudedKeyBag = new DerObjectIdentifier("1.2.840.113549.1.12.10.1.2");

		// Token: 0x04000A7B RID: 2683
		public static readonly DerObjectIdentifier CertBag = new DerObjectIdentifier("1.2.840.113549.1.12.10.1.3");

		// Token: 0x04000A7C RID: 2684
		public static readonly DerObjectIdentifier CrlBag = new DerObjectIdentifier("1.2.840.113549.1.12.10.1.4");

		// Token: 0x04000A7D RID: 2685
		public static readonly DerObjectIdentifier SecretBag = new DerObjectIdentifier("1.2.840.113549.1.12.10.1.5");

		// Token: 0x04000A7E RID: 2686
		public static readonly DerObjectIdentifier SafeContentsBag = new DerObjectIdentifier("1.2.840.113549.1.12.10.1.6");

		// Token: 0x04000A7F RID: 2687
		public static readonly DerObjectIdentifier PbeWithShaAnd128BitRC4 = new DerObjectIdentifier("1.2.840.113549.1.12.1.1");

		// Token: 0x04000A80 RID: 2688
		public static readonly DerObjectIdentifier PbeWithShaAnd40BitRC4 = new DerObjectIdentifier("1.2.840.113549.1.12.1.2");

		// Token: 0x04000A81 RID: 2689
		public static readonly DerObjectIdentifier PbeWithShaAnd3KeyTripleDesCbc = new DerObjectIdentifier("1.2.840.113549.1.12.1.3");

		// Token: 0x04000A82 RID: 2690
		public static readonly DerObjectIdentifier PbeWithShaAnd2KeyTripleDesCbc = new DerObjectIdentifier("1.2.840.113549.1.12.1.4");

		// Token: 0x04000A83 RID: 2691
		public static readonly DerObjectIdentifier PbeWithShaAnd128BitRC2Cbc = new DerObjectIdentifier("1.2.840.113549.1.12.1.5");

		// Token: 0x04000A84 RID: 2692
		public static readonly DerObjectIdentifier PbewithShaAnd40BitRC2Cbc = new DerObjectIdentifier("1.2.840.113549.1.12.1.6");
	}
}
