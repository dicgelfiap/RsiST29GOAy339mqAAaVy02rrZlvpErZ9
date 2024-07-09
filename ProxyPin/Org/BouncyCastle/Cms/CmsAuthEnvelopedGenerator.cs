using System;
using Org.BouncyCastle.Asn1.Nist;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020002DF RID: 735
	internal class CmsAuthEnvelopedGenerator
	{
		// Token: 0x04000F25 RID: 3877
		public static readonly string Aes128Ccm = NistObjectIdentifiers.IdAes128Ccm.Id;

		// Token: 0x04000F26 RID: 3878
		public static readonly string Aes192Ccm = NistObjectIdentifiers.IdAes192Ccm.Id;

		// Token: 0x04000F27 RID: 3879
		public static readonly string Aes256Ccm = NistObjectIdentifiers.IdAes256Ccm.Id;

		// Token: 0x04000F28 RID: 3880
		public static readonly string Aes128Gcm = NistObjectIdentifiers.IdAes128Gcm.Id;

		// Token: 0x04000F29 RID: 3881
		public static readonly string Aes192Gcm = NistObjectIdentifiers.IdAes192Gcm.Id;

		// Token: 0x04000F2A RID: 3882
		public static readonly string Aes256Gcm = NistObjectIdentifiers.IdAes256Gcm.Id;
	}
}
