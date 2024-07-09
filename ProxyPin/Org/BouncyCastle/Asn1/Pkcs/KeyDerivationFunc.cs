using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020001AD RID: 429
	public class KeyDerivationFunc : AlgorithmIdentifier
	{
		// Token: 0x06000E00 RID: 3584 RVA: 0x00055B8C File Offset: 0x00055B8C
		internal KeyDerivationFunc(Asn1Sequence seq) : base(seq)
		{
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x00055B98 File Offset: 0x00055B98
		public KeyDerivationFunc(DerObjectIdentifier id, Asn1Encodable parameters) : base(id, parameters)
		{
		}
	}
}
