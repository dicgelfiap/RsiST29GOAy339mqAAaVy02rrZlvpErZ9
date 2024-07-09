using System;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x020002BD RID: 701
	public enum SignatureSubpacketTag
	{
		// Token: 0x04000EAA RID: 3754
		CreationTime = 2,
		// Token: 0x04000EAB RID: 3755
		ExpireTime,
		// Token: 0x04000EAC RID: 3756
		Exportable,
		// Token: 0x04000EAD RID: 3757
		TrustSig,
		// Token: 0x04000EAE RID: 3758
		RegExp,
		// Token: 0x04000EAF RID: 3759
		Revocable,
		// Token: 0x04000EB0 RID: 3760
		KeyExpireTime = 9,
		// Token: 0x04000EB1 RID: 3761
		Placeholder,
		// Token: 0x04000EB2 RID: 3762
		PreferredSymmetricAlgorithms,
		// Token: 0x04000EB3 RID: 3763
		RevocationKey,
		// Token: 0x04000EB4 RID: 3764
		IssuerKeyId = 16,
		// Token: 0x04000EB5 RID: 3765
		NotationData = 20,
		// Token: 0x04000EB6 RID: 3766
		PreferredHashAlgorithms,
		// Token: 0x04000EB7 RID: 3767
		PreferredCompressionAlgorithms,
		// Token: 0x04000EB8 RID: 3768
		KeyServerPreferences,
		// Token: 0x04000EB9 RID: 3769
		PreferredKeyServer,
		// Token: 0x04000EBA RID: 3770
		PrimaryUserId,
		// Token: 0x04000EBB RID: 3771
		PolicyUrl,
		// Token: 0x04000EBC RID: 3772
		KeyFlags,
		// Token: 0x04000EBD RID: 3773
		SignerUserId,
		// Token: 0x04000EBE RID: 3774
		RevocationReason,
		// Token: 0x04000EBF RID: 3775
		Features,
		// Token: 0x04000EC0 RID: 3776
		SignatureTarget,
		// Token: 0x04000EC1 RID: 3777
		EmbeddedSignature
	}
}
