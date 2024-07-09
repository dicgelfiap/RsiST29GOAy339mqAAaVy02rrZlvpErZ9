using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.X509
{
	// Token: 0x02000630 RID: 1584
	public interface IX509Extension
	{
		// Token: 0x0600373A RID: 14138
		ISet GetCriticalExtensionOids();

		// Token: 0x0600373B RID: 14139
		ISet GetNonCriticalExtensionOids();

		// Token: 0x0600373C RID: 14140
		[Obsolete("Use version taking a DerObjectIdentifier instead")]
		Asn1OctetString GetExtensionValue(string oid);

		// Token: 0x0600373D RID: 14141
		Asn1OctetString GetExtensionValue(DerObjectIdentifier oid);
	}
}
