using System;

namespace Org.BouncyCastle.Asn1.X509.Qualified
{
	// Token: 0x020001DC RID: 476
	public sealed class Rfc3739QCObjectIdentifiers
	{
		// Token: 0x06000F52 RID: 3922 RVA: 0x0005BFB8 File Offset: 0x0005BFB8
		private Rfc3739QCObjectIdentifiers()
		{
		}

		// Token: 0x04000B80 RID: 2944
		public static readonly DerObjectIdentifier IdQcs = new DerObjectIdentifier("1.3.6.1.5.5.7.11");

		// Token: 0x04000B81 RID: 2945
		public static readonly DerObjectIdentifier IdQcsPkixQCSyntaxV1 = new DerObjectIdentifier(Rfc3739QCObjectIdentifiers.IdQcs + ".1");

		// Token: 0x04000B82 RID: 2946
		public static readonly DerObjectIdentifier IdQcsPkixQCSyntaxV2 = new DerObjectIdentifier(Rfc3739QCObjectIdentifiers.IdQcs + ".2");
	}
}
