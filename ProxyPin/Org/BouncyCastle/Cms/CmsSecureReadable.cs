using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020002DE RID: 734
	internal interface CmsSecureReadable
	{
		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06001631 RID: 5681
		AlgorithmIdentifier Algorithm { get; }

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06001632 RID: 5682
		object CryptoObject { get; }

		// Token: 0x06001633 RID: 5683
		CmsReadable GetReadable(KeyParameter key);
	}
}
