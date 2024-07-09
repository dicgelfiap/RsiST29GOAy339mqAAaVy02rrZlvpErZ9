using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crmf
{
	// Token: 0x0200031F RID: 799
	public class DefaultPKMacPrimitivesProvider : IPKMacPrimitivesProvider
	{
		// Token: 0x06001825 RID: 6181 RVA: 0x0007D2FC File Offset: 0x0007D2FC
		public IDigest CreateDigest(AlgorithmIdentifier digestAlg)
		{
			return DigestUtilities.GetDigest(digestAlg.Algorithm);
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x0007D30C File Offset: 0x0007D30C
		public IMac CreateMac(AlgorithmIdentifier macAlg)
		{
			return MacUtilities.GetMac(macAlg.Algorithm);
		}
	}
}
