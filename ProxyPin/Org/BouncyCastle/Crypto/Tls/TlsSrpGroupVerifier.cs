using System;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004EE RID: 1262
	public interface TlsSrpGroupVerifier
	{
		// Token: 0x060026C0 RID: 9920
		bool Accept(Srp6GroupParameters group);
	}
}
