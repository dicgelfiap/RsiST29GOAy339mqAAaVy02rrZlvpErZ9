using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000207 RID: 519
	public sealed class PolicyQualifierID : DerObjectIdentifier
	{
		// Token: 0x060010BA RID: 4282 RVA: 0x00060FD4 File Offset: 0x00060FD4
		private PolicyQualifierID(string id) : base(id)
		{
		}

		// Token: 0x04000C25 RID: 3109
		private const string IdQt = "1.3.6.1.5.5.7.2";

		// Token: 0x04000C26 RID: 3110
		public static readonly PolicyQualifierID IdQtCps = new PolicyQualifierID("1.3.6.1.5.5.7.2.1");

		// Token: 0x04000C27 RID: 3111
		public static readonly PolicyQualifierID IdQtUnotice = new PolicyQualifierID("1.3.6.1.5.5.7.2.2");
	}
}
