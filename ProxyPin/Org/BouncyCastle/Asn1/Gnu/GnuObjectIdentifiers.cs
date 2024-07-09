using System;

namespace Org.BouncyCastle.Asn1.Gnu
{
	// Token: 0x0200016D RID: 365
	public abstract class GnuObjectIdentifiers
	{
		// Token: 0x04000881 RID: 2177
		public static readonly DerObjectIdentifier Gnu = new DerObjectIdentifier("1.3.6.1.4.1.11591.1");

		// Token: 0x04000882 RID: 2178
		public static readonly DerObjectIdentifier GnuPG = new DerObjectIdentifier("1.3.6.1.4.1.11591.2");

		// Token: 0x04000883 RID: 2179
		public static readonly DerObjectIdentifier Notation = new DerObjectIdentifier("1.3.6.1.4.1.11591.2.1");

		// Token: 0x04000884 RID: 2180
		public static readonly DerObjectIdentifier PkaAddress = new DerObjectIdentifier("1.3.6.1.4.1.11591.2.1.1");

		// Token: 0x04000885 RID: 2181
		public static readonly DerObjectIdentifier GnuRadar = new DerObjectIdentifier("1.3.6.1.4.1.11591.3");

		// Token: 0x04000886 RID: 2182
		public static readonly DerObjectIdentifier DigestAlgorithm = new DerObjectIdentifier("1.3.6.1.4.1.11591.12");

		// Token: 0x04000887 RID: 2183
		public static readonly DerObjectIdentifier Tiger192 = new DerObjectIdentifier("1.3.6.1.4.1.11591.12.2");

		// Token: 0x04000888 RID: 2184
		public static readonly DerObjectIdentifier EncryptionAlgorithm = new DerObjectIdentifier("1.3.6.1.4.1.11591.13");

		// Token: 0x04000889 RID: 2185
		public static readonly DerObjectIdentifier Serpent = new DerObjectIdentifier("1.3.6.1.4.1.11591.13.2");

		// Token: 0x0400088A RID: 2186
		public static readonly DerObjectIdentifier Serpent128Ecb = new DerObjectIdentifier("1.3.6.1.4.1.11591.13.2.1");

		// Token: 0x0400088B RID: 2187
		public static readonly DerObjectIdentifier Serpent128Cbc = new DerObjectIdentifier("1.3.6.1.4.1.11591.13.2.2");

		// Token: 0x0400088C RID: 2188
		public static readonly DerObjectIdentifier Serpent128Ofb = new DerObjectIdentifier("1.3.6.1.4.1.11591.13.2.3");

		// Token: 0x0400088D RID: 2189
		public static readonly DerObjectIdentifier Serpent128Cfb = new DerObjectIdentifier("1.3.6.1.4.1.11591.13.2.4");

		// Token: 0x0400088E RID: 2190
		public static readonly DerObjectIdentifier Serpent192Ecb = new DerObjectIdentifier("1.3.6.1.4.1.11591.13.2.21");

		// Token: 0x0400088F RID: 2191
		public static readonly DerObjectIdentifier Serpent192Cbc = new DerObjectIdentifier("1.3.6.1.4.1.11591.13.2.22");

		// Token: 0x04000890 RID: 2192
		public static readonly DerObjectIdentifier Serpent192Ofb = new DerObjectIdentifier("1.3.6.1.4.1.11591.13.2.23");

		// Token: 0x04000891 RID: 2193
		public static readonly DerObjectIdentifier Serpent192Cfb = new DerObjectIdentifier("1.3.6.1.4.1.11591.13.2.24");

		// Token: 0x04000892 RID: 2194
		public static readonly DerObjectIdentifier Serpent256Ecb = new DerObjectIdentifier("1.3.6.1.4.1.11591.13.2.41");

		// Token: 0x04000893 RID: 2195
		public static readonly DerObjectIdentifier Serpent256Cbc = new DerObjectIdentifier("1.3.6.1.4.1.11591.13.2.42");

		// Token: 0x04000894 RID: 2196
		public static readonly DerObjectIdentifier Serpent256Ofb = new DerObjectIdentifier("1.3.6.1.4.1.11591.13.2.43");

		// Token: 0x04000895 RID: 2197
		public static readonly DerObjectIdentifier Serpent256Cfb = new DerObjectIdentifier("1.3.6.1.4.1.11591.13.2.44");

		// Token: 0x04000896 RID: 2198
		public static readonly DerObjectIdentifier Crc = new DerObjectIdentifier("1.3.6.1.4.1.11591.14");

		// Token: 0x04000897 RID: 2199
		public static readonly DerObjectIdentifier Crc32 = new DerObjectIdentifier("1.3.6.1.4.1.11591.14.1");

		// Token: 0x04000898 RID: 2200
		public static readonly DerObjectIdentifier EllipticCurve = new DerObjectIdentifier("1.3.6.1.4.1.11591.15");

		// Token: 0x04000899 RID: 2201
		public static readonly DerObjectIdentifier Ed25519 = GnuObjectIdentifiers.EllipticCurve.Branch("1");
	}
}
