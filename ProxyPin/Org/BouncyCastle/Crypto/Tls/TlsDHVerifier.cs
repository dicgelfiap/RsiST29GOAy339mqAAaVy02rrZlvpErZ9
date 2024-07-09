using System;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004E9 RID: 1257
	public interface TlsDHVerifier
	{
		// Token: 0x0600269D RID: 9885
		bool Accept(DHParameters dhParameters);
	}
}
