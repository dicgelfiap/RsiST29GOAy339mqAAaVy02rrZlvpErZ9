using System;
using Org.BouncyCastle.Asn1.Pkcs;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000102 RID: 258
	public abstract class CmsObjectIdentifiers
	{
		// Token: 0x040006C4 RID: 1732
		public static readonly DerObjectIdentifier Data = PkcsObjectIdentifiers.Data;

		// Token: 0x040006C5 RID: 1733
		public static readonly DerObjectIdentifier SignedData = PkcsObjectIdentifiers.SignedData;

		// Token: 0x040006C6 RID: 1734
		public static readonly DerObjectIdentifier EnvelopedData = PkcsObjectIdentifiers.EnvelopedData;

		// Token: 0x040006C7 RID: 1735
		public static readonly DerObjectIdentifier SignedAndEnvelopedData = PkcsObjectIdentifiers.SignedAndEnvelopedData;

		// Token: 0x040006C8 RID: 1736
		public static readonly DerObjectIdentifier DigestedData = PkcsObjectIdentifiers.DigestedData;

		// Token: 0x040006C9 RID: 1737
		public static readonly DerObjectIdentifier EncryptedData = PkcsObjectIdentifiers.EncryptedData;

		// Token: 0x040006CA RID: 1738
		public static readonly DerObjectIdentifier AuthenticatedData = PkcsObjectIdentifiers.IdCTAuthData;

		// Token: 0x040006CB RID: 1739
		public static readonly DerObjectIdentifier CompressedData = PkcsObjectIdentifiers.IdCTCompressedData;

		// Token: 0x040006CC RID: 1740
		public static readonly DerObjectIdentifier AuthEnvelopedData = PkcsObjectIdentifiers.IdCTAuthEnvelopedData;

		// Token: 0x040006CD RID: 1741
		public static readonly DerObjectIdentifier timestampedData = PkcsObjectIdentifiers.IdCTTimestampedData;

		// Token: 0x040006CE RID: 1742
		public static readonly DerObjectIdentifier id_ri = new DerObjectIdentifier("1.3.6.1.5.5.7.16");

		// Token: 0x040006CF RID: 1743
		public static readonly DerObjectIdentifier id_ri_ocsp_response = CmsObjectIdentifiers.id_ri.Branch("2");

		// Token: 0x040006D0 RID: 1744
		public static readonly DerObjectIdentifier id_ri_scvp = CmsObjectIdentifiers.id_ri.Branch("4");
	}
}
