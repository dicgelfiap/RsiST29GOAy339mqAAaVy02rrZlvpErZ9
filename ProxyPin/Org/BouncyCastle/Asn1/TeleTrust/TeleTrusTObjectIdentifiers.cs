using System;

namespace Org.BouncyCastle.Asn1.TeleTrust
{
	// Token: 0x020001C9 RID: 457
	public sealed class TeleTrusTObjectIdentifiers
	{
		// Token: 0x06000ECB RID: 3787 RVA: 0x00059290 File Offset: 0x00059290
		private TeleTrusTObjectIdentifiers()
		{
		}

		// Token: 0x04000B08 RID: 2824
		public static readonly DerObjectIdentifier TeleTrusTAlgorithm = new DerObjectIdentifier("1.3.36.3");

		// Token: 0x04000B09 RID: 2825
		public static readonly DerObjectIdentifier RipeMD160 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.TeleTrusTAlgorithm + ".2.1");

		// Token: 0x04000B0A RID: 2826
		public static readonly DerObjectIdentifier RipeMD128 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.TeleTrusTAlgorithm + ".2.2");

		// Token: 0x04000B0B RID: 2827
		public static readonly DerObjectIdentifier RipeMD256 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.TeleTrusTAlgorithm + ".2.3");

		// Token: 0x04000B0C RID: 2828
		public static readonly DerObjectIdentifier TeleTrusTRsaSignatureAlgorithm = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.TeleTrusTAlgorithm + ".3.1");

		// Token: 0x04000B0D RID: 2829
		public static readonly DerObjectIdentifier RsaSignatureWithRipeMD160 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.TeleTrusTRsaSignatureAlgorithm + ".2");

		// Token: 0x04000B0E RID: 2830
		public static readonly DerObjectIdentifier RsaSignatureWithRipeMD128 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.TeleTrusTRsaSignatureAlgorithm + ".3");

		// Token: 0x04000B0F RID: 2831
		public static readonly DerObjectIdentifier RsaSignatureWithRipeMD256 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.TeleTrusTRsaSignatureAlgorithm + ".4");

		// Token: 0x04000B10 RID: 2832
		public static readonly DerObjectIdentifier ECSign = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.TeleTrusTAlgorithm + ".3.2");

		// Token: 0x04000B11 RID: 2833
		public static readonly DerObjectIdentifier ECSignWithSha1 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.ECSign + ".1");

		// Token: 0x04000B12 RID: 2834
		public static readonly DerObjectIdentifier ECSignWithRipeMD160 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.ECSign + ".2");

		// Token: 0x04000B13 RID: 2835
		public static readonly DerObjectIdentifier EccBrainpool = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.TeleTrusTAlgorithm + ".3.2.8");

		// Token: 0x04000B14 RID: 2836
		public static readonly DerObjectIdentifier EllipticCurve = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.EccBrainpool + ".1");

		// Token: 0x04000B15 RID: 2837
		public static readonly DerObjectIdentifier VersionOne = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.EllipticCurve + ".1");

		// Token: 0x04000B16 RID: 2838
		public static readonly DerObjectIdentifier BrainpoolP160R1 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.VersionOne + ".1");

		// Token: 0x04000B17 RID: 2839
		public static readonly DerObjectIdentifier BrainpoolP160T1 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.VersionOne + ".2");

		// Token: 0x04000B18 RID: 2840
		public static readonly DerObjectIdentifier BrainpoolP192R1 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.VersionOne + ".3");

		// Token: 0x04000B19 RID: 2841
		public static readonly DerObjectIdentifier BrainpoolP192T1 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.VersionOne + ".4");

		// Token: 0x04000B1A RID: 2842
		public static readonly DerObjectIdentifier BrainpoolP224R1 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.VersionOne + ".5");

		// Token: 0x04000B1B RID: 2843
		public static readonly DerObjectIdentifier BrainpoolP224T1 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.VersionOne + ".6");

		// Token: 0x04000B1C RID: 2844
		public static readonly DerObjectIdentifier BrainpoolP256R1 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.VersionOne + ".7");

		// Token: 0x04000B1D RID: 2845
		public static readonly DerObjectIdentifier BrainpoolP256T1 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.VersionOne + ".8");

		// Token: 0x04000B1E RID: 2846
		public static readonly DerObjectIdentifier BrainpoolP320R1 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.VersionOne + ".9");

		// Token: 0x04000B1F RID: 2847
		public static readonly DerObjectIdentifier BrainpoolP320T1 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.VersionOne + ".10");

		// Token: 0x04000B20 RID: 2848
		public static readonly DerObjectIdentifier BrainpoolP384R1 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.VersionOne + ".11");

		// Token: 0x04000B21 RID: 2849
		public static readonly DerObjectIdentifier BrainpoolP384T1 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.VersionOne + ".12");

		// Token: 0x04000B22 RID: 2850
		public static readonly DerObjectIdentifier BrainpoolP512R1 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.VersionOne + ".13");

		// Token: 0x04000B23 RID: 2851
		public static readonly DerObjectIdentifier BrainpoolP512T1 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.VersionOne + ".14");
	}
}
