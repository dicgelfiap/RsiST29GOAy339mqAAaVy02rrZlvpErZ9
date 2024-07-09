using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;

namespace Org.BouncyCastle.Crmf
{
	// Token: 0x0200031E RID: 798
	public interface IPKMacPrimitivesProvider
	{
		// Token: 0x06001823 RID: 6179
		IDigest CreateDigest(AlgorithmIdentifier digestAlg);

		// Token: 0x06001824 RID: 6180
		IMac CreateMac(AlgorithmIdentifier macAlg);
	}
}
