using System;

namespace Org.BouncyCastle.Asn1.X9
{
	// Token: 0x02000235 RID: 565
	public abstract class X9ObjectIdentifiers
	{
		// Token: 0x04000D14 RID: 3348
		internal const string AnsiX962 = "1.2.840.10045";

		// Token: 0x04000D15 RID: 3349
		[Obsolete("Use 'id_ecSigType' instead")]
		public const string IdECSigType = "1.2.840.10045.4";

		// Token: 0x04000D16 RID: 3350
		[Obsolete("Use 'id_publicKeyType' instead")]
		public const string IdPublicKeyType = "1.2.840.10045.2";

		// Token: 0x04000D17 RID: 3351
		public static readonly DerObjectIdentifier ansi_X9_62 = new DerObjectIdentifier("1.2.840.10045");

		// Token: 0x04000D18 RID: 3352
		public static readonly DerObjectIdentifier IdFieldType = X9ObjectIdentifiers.ansi_X9_62.Branch("1");

		// Token: 0x04000D19 RID: 3353
		public static readonly DerObjectIdentifier PrimeField = X9ObjectIdentifiers.IdFieldType.Branch("1");

		// Token: 0x04000D1A RID: 3354
		public static readonly DerObjectIdentifier CharacteristicTwoField = X9ObjectIdentifiers.IdFieldType.Branch("2");

		// Token: 0x04000D1B RID: 3355
		public static readonly DerObjectIdentifier GNBasis = X9ObjectIdentifiers.CharacteristicTwoField.Branch("3.1");

		// Token: 0x04000D1C RID: 3356
		public static readonly DerObjectIdentifier TPBasis = X9ObjectIdentifiers.CharacteristicTwoField.Branch("3.2");

		// Token: 0x04000D1D RID: 3357
		public static readonly DerObjectIdentifier PPBasis = X9ObjectIdentifiers.CharacteristicTwoField.Branch("3.3");

		// Token: 0x04000D1E RID: 3358
		public static readonly DerObjectIdentifier id_ecSigType = X9ObjectIdentifiers.ansi_X9_62.Branch("4");

		// Token: 0x04000D1F RID: 3359
		public static readonly DerObjectIdentifier ECDsaWithSha1 = X9ObjectIdentifiers.id_ecSigType.Branch("1");

		// Token: 0x04000D20 RID: 3360
		public static readonly DerObjectIdentifier id_publicKeyType = X9ObjectIdentifiers.ansi_X9_62.Branch("2");

		// Token: 0x04000D21 RID: 3361
		public static readonly DerObjectIdentifier IdECPublicKey = X9ObjectIdentifiers.id_publicKeyType.Branch("1");

		// Token: 0x04000D22 RID: 3362
		public static readonly DerObjectIdentifier ECDsaWithSha2 = X9ObjectIdentifiers.id_ecSigType.Branch("3");

		// Token: 0x04000D23 RID: 3363
		public static readonly DerObjectIdentifier ECDsaWithSha224 = X9ObjectIdentifiers.ECDsaWithSha2.Branch("1");

		// Token: 0x04000D24 RID: 3364
		public static readonly DerObjectIdentifier ECDsaWithSha256 = X9ObjectIdentifiers.ECDsaWithSha2.Branch("2");

		// Token: 0x04000D25 RID: 3365
		public static readonly DerObjectIdentifier ECDsaWithSha384 = X9ObjectIdentifiers.ECDsaWithSha2.Branch("3");

		// Token: 0x04000D26 RID: 3366
		public static readonly DerObjectIdentifier ECDsaWithSha512 = X9ObjectIdentifiers.ECDsaWithSha2.Branch("4");

		// Token: 0x04000D27 RID: 3367
		public static readonly DerObjectIdentifier EllipticCurve = X9ObjectIdentifiers.ansi_X9_62.Branch("3");

		// Token: 0x04000D28 RID: 3368
		public static readonly DerObjectIdentifier CTwoCurve = X9ObjectIdentifiers.EllipticCurve.Branch("0");

		// Token: 0x04000D29 RID: 3369
		public static readonly DerObjectIdentifier C2Pnb163v1 = X9ObjectIdentifiers.CTwoCurve.Branch("1");

		// Token: 0x04000D2A RID: 3370
		public static readonly DerObjectIdentifier C2Pnb163v2 = X9ObjectIdentifiers.CTwoCurve.Branch("2");

		// Token: 0x04000D2B RID: 3371
		public static readonly DerObjectIdentifier C2Pnb163v3 = X9ObjectIdentifiers.CTwoCurve.Branch("3");

		// Token: 0x04000D2C RID: 3372
		public static readonly DerObjectIdentifier C2Pnb176w1 = X9ObjectIdentifiers.CTwoCurve.Branch("4");

		// Token: 0x04000D2D RID: 3373
		public static readonly DerObjectIdentifier C2Tnb191v1 = X9ObjectIdentifiers.CTwoCurve.Branch("5");

		// Token: 0x04000D2E RID: 3374
		public static readonly DerObjectIdentifier C2Tnb191v2 = X9ObjectIdentifiers.CTwoCurve.Branch("6");

		// Token: 0x04000D2F RID: 3375
		public static readonly DerObjectIdentifier C2Tnb191v3 = X9ObjectIdentifiers.CTwoCurve.Branch("7");

		// Token: 0x04000D30 RID: 3376
		public static readonly DerObjectIdentifier C2Onb191v4 = X9ObjectIdentifiers.CTwoCurve.Branch("8");

		// Token: 0x04000D31 RID: 3377
		public static readonly DerObjectIdentifier C2Onb191v5 = X9ObjectIdentifiers.CTwoCurve.Branch("9");

		// Token: 0x04000D32 RID: 3378
		public static readonly DerObjectIdentifier C2Pnb208w1 = X9ObjectIdentifiers.CTwoCurve.Branch("10");

		// Token: 0x04000D33 RID: 3379
		public static readonly DerObjectIdentifier C2Tnb239v1 = X9ObjectIdentifiers.CTwoCurve.Branch("11");

		// Token: 0x04000D34 RID: 3380
		public static readonly DerObjectIdentifier C2Tnb239v2 = X9ObjectIdentifiers.CTwoCurve.Branch("12");

		// Token: 0x04000D35 RID: 3381
		public static readonly DerObjectIdentifier C2Tnb239v3 = X9ObjectIdentifiers.CTwoCurve.Branch("13");

		// Token: 0x04000D36 RID: 3382
		public static readonly DerObjectIdentifier C2Onb239v4 = X9ObjectIdentifiers.CTwoCurve.Branch("14");

		// Token: 0x04000D37 RID: 3383
		public static readonly DerObjectIdentifier C2Onb239v5 = X9ObjectIdentifiers.CTwoCurve.Branch("15");

		// Token: 0x04000D38 RID: 3384
		public static readonly DerObjectIdentifier C2Pnb272w1 = X9ObjectIdentifiers.CTwoCurve.Branch("16");

		// Token: 0x04000D39 RID: 3385
		public static readonly DerObjectIdentifier C2Pnb304w1 = X9ObjectIdentifiers.CTwoCurve.Branch("17");

		// Token: 0x04000D3A RID: 3386
		public static readonly DerObjectIdentifier C2Tnb359v1 = X9ObjectIdentifiers.CTwoCurve.Branch("18");

		// Token: 0x04000D3B RID: 3387
		public static readonly DerObjectIdentifier C2Pnb368w1 = X9ObjectIdentifiers.CTwoCurve.Branch("19");

		// Token: 0x04000D3C RID: 3388
		public static readonly DerObjectIdentifier C2Tnb431r1 = X9ObjectIdentifiers.CTwoCurve.Branch("20");

		// Token: 0x04000D3D RID: 3389
		public static readonly DerObjectIdentifier PrimeCurve = X9ObjectIdentifiers.EllipticCurve.Branch("1");

		// Token: 0x04000D3E RID: 3390
		public static readonly DerObjectIdentifier Prime192v1 = X9ObjectIdentifiers.PrimeCurve.Branch("1");

		// Token: 0x04000D3F RID: 3391
		public static readonly DerObjectIdentifier Prime192v2 = X9ObjectIdentifiers.PrimeCurve.Branch("2");

		// Token: 0x04000D40 RID: 3392
		public static readonly DerObjectIdentifier Prime192v3 = X9ObjectIdentifiers.PrimeCurve.Branch("3");

		// Token: 0x04000D41 RID: 3393
		public static readonly DerObjectIdentifier Prime239v1 = X9ObjectIdentifiers.PrimeCurve.Branch("4");

		// Token: 0x04000D42 RID: 3394
		public static readonly DerObjectIdentifier Prime239v2 = X9ObjectIdentifiers.PrimeCurve.Branch("5");

		// Token: 0x04000D43 RID: 3395
		public static readonly DerObjectIdentifier Prime239v3 = X9ObjectIdentifiers.PrimeCurve.Branch("6");

		// Token: 0x04000D44 RID: 3396
		public static readonly DerObjectIdentifier Prime256v1 = X9ObjectIdentifiers.PrimeCurve.Branch("7");

		// Token: 0x04000D45 RID: 3397
		public static readonly DerObjectIdentifier IdDsa = new DerObjectIdentifier("1.2.840.10040.4.1");

		// Token: 0x04000D46 RID: 3398
		public static readonly DerObjectIdentifier IdDsaWithSha1 = new DerObjectIdentifier("1.2.840.10040.4.3");

		// Token: 0x04000D47 RID: 3399
		public static readonly DerObjectIdentifier X9x63Scheme = new DerObjectIdentifier("1.3.133.16.840.63.0");

		// Token: 0x04000D48 RID: 3400
		public static readonly DerObjectIdentifier DHSinglePassStdDHSha1KdfScheme = X9ObjectIdentifiers.X9x63Scheme.Branch("2");

		// Token: 0x04000D49 RID: 3401
		public static readonly DerObjectIdentifier DHSinglePassCofactorDHSha1KdfScheme = X9ObjectIdentifiers.X9x63Scheme.Branch("3");

		// Token: 0x04000D4A RID: 3402
		public static readonly DerObjectIdentifier MqvSinglePassSha1KdfScheme = X9ObjectIdentifiers.X9x63Scheme.Branch("16");

		// Token: 0x04000D4B RID: 3403
		public static readonly DerObjectIdentifier ansi_x9_42 = new DerObjectIdentifier("1.2.840.10046");

		// Token: 0x04000D4C RID: 3404
		public static readonly DerObjectIdentifier DHPublicNumber = X9ObjectIdentifiers.ansi_x9_42.Branch("2.1");

		// Token: 0x04000D4D RID: 3405
		public static readonly DerObjectIdentifier X9x42Schemes = X9ObjectIdentifiers.ansi_x9_42.Branch("2.3");

		// Token: 0x04000D4E RID: 3406
		public static readonly DerObjectIdentifier DHStatic = X9ObjectIdentifiers.X9x42Schemes.Branch("1");

		// Token: 0x04000D4F RID: 3407
		public static readonly DerObjectIdentifier DHEphem = X9ObjectIdentifiers.X9x42Schemes.Branch("2");

		// Token: 0x04000D50 RID: 3408
		public static readonly DerObjectIdentifier DHOneFlow = X9ObjectIdentifiers.X9x42Schemes.Branch("3");

		// Token: 0x04000D51 RID: 3409
		public static readonly DerObjectIdentifier DHHybrid1 = X9ObjectIdentifiers.X9x42Schemes.Branch("4");

		// Token: 0x04000D52 RID: 3410
		public static readonly DerObjectIdentifier DHHybrid2 = X9ObjectIdentifiers.X9x42Schemes.Branch("5");

		// Token: 0x04000D53 RID: 3411
		public static readonly DerObjectIdentifier DHHybridOneFlow = X9ObjectIdentifiers.X9x42Schemes.Branch("6");

		// Token: 0x04000D54 RID: 3412
		public static readonly DerObjectIdentifier Mqv2 = X9ObjectIdentifiers.X9x42Schemes.Branch("7");

		// Token: 0x04000D55 RID: 3413
		public static readonly DerObjectIdentifier Mqv1 = X9ObjectIdentifiers.X9x42Schemes.Branch("8");
	}
}
