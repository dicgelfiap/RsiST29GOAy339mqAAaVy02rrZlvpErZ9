using System;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x020002B1 RID: 689
	public enum PacketTag
	{
		// Token: 0x04000E4A RID: 3658
		Reserved,
		// Token: 0x04000E4B RID: 3659
		PublicKeyEncryptedSession,
		// Token: 0x04000E4C RID: 3660
		Signature,
		// Token: 0x04000E4D RID: 3661
		SymmetricKeyEncryptedSessionKey,
		// Token: 0x04000E4E RID: 3662
		OnePassSignature,
		// Token: 0x04000E4F RID: 3663
		SecretKey,
		// Token: 0x04000E50 RID: 3664
		PublicKey,
		// Token: 0x04000E51 RID: 3665
		SecretSubkey,
		// Token: 0x04000E52 RID: 3666
		CompressedData,
		// Token: 0x04000E53 RID: 3667
		SymmetricKeyEncrypted,
		// Token: 0x04000E54 RID: 3668
		Marker,
		// Token: 0x04000E55 RID: 3669
		LiteralData,
		// Token: 0x04000E56 RID: 3670
		Trust,
		// Token: 0x04000E57 RID: 3671
		UserId,
		// Token: 0x04000E58 RID: 3672
		PublicSubkey,
		// Token: 0x04000E59 RID: 3673
		UserAttribute = 17,
		// Token: 0x04000E5A RID: 3674
		SymmetricEncryptedIntegrityProtected,
		// Token: 0x04000E5B RID: 3675
		ModificationDetectionCode,
		// Token: 0x04000E5C RID: 3676
		Experimental1 = 60,
		// Token: 0x04000E5D RID: 3677
		Experimental2,
		// Token: 0x04000E5E RID: 3678
		Experimental3,
		// Token: 0x04000E5F RID: 3679
		Experimental4
	}
}
