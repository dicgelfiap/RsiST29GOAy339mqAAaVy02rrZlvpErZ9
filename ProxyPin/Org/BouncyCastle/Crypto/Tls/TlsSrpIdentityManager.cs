using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000520 RID: 1312
	public interface TlsSrpIdentityManager
	{
		// Token: 0x060027F0 RID: 10224
		TlsSrpLoginParameters GetLoginParameters(byte[] identity);
	}
}
