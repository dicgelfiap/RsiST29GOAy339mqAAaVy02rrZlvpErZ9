using System;
using Org.BouncyCastle.Asn1.Pkcs;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000101 RID: 257
	public abstract class CmsAttributes
	{
		// Token: 0x040006BF RID: 1727
		public static readonly DerObjectIdentifier ContentType = PkcsObjectIdentifiers.Pkcs9AtContentType;

		// Token: 0x040006C0 RID: 1728
		public static readonly DerObjectIdentifier MessageDigest = PkcsObjectIdentifiers.Pkcs9AtMessageDigest;

		// Token: 0x040006C1 RID: 1729
		public static readonly DerObjectIdentifier SigningTime = PkcsObjectIdentifiers.Pkcs9AtSigningTime;

		// Token: 0x040006C2 RID: 1730
		public static readonly DerObjectIdentifier CounterSignature = PkcsObjectIdentifiers.Pkcs9AtCounterSignature;

		// Token: 0x040006C3 RID: 1731
		public static readonly DerObjectIdentifier ContentHint = PkcsObjectIdentifiers.IdAAContentHint;
	}
}
