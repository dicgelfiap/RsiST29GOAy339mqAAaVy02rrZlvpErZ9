using System;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x020002B2 RID: 690
	public enum PublicKeyAlgorithmTag
	{
		// Token: 0x04000E61 RID: 3681
		RsaGeneral = 1,
		// Token: 0x04000E62 RID: 3682
		RsaEncrypt,
		// Token: 0x04000E63 RID: 3683
		RsaSign,
		// Token: 0x04000E64 RID: 3684
		ElGamalEncrypt = 16,
		// Token: 0x04000E65 RID: 3685
		Dsa,
		// Token: 0x04000E66 RID: 3686
		[Obsolete("Use 'ECDH' instead")]
		EC,
		// Token: 0x04000E67 RID: 3687
		ECDH = 18,
		// Token: 0x04000E68 RID: 3688
		ECDsa,
		// Token: 0x04000E69 RID: 3689
		ElGamalGeneral,
		// Token: 0x04000E6A RID: 3690
		DiffieHellman,
		// Token: 0x04000E6B RID: 3691
		Experimental_1 = 100,
		// Token: 0x04000E6C RID: 3692
		Experimental_2,
		// Token: 0x04000E6D RID: 3693
		Experimental_3,
		// Token: 0x04000E6E RID: 3694
		Experimental_4,
		// Token: 0x04000E6F RID: 3695
		Experimental_5,
		// Token: 0x04000E70 RID: 3696
		Experimental_6,
		// Token: 0x04000E71 RID: 3697
		Experimental_7,
		// Token: 0x04000E72 RID: 3698
		Experimental_8,
		// Token: 0x04000E73 RID: 3699
		Experimental_9,
		// Token: 0x04000E74 RID: 3700
		Experimental_10,
		// Token: 0x04000E75 RID: 3701
		Experimental_11
	}
}
