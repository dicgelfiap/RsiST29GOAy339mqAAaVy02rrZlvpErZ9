using System;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200052B RID: 1323
	internal class TlsClientContextImpl : AbstractTlsContext, TlsClientContext, TlsContext
	{
		// Token: 0x06002827 RID: 10279 RVA: 0x000D8254 File Offset: 0x000D8254
		internal TlsClientContextImpl(SecureRandom secureRandom, SecurityParameters securityParameters) : base(secureRandom, securityParameters)
		{
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06002828 RID: 10280 RVA: 0x000D8260 File Offset: 0x000D8260
		public override bool IsServer
		{
			get
			{
				return false;
			}
		}
	}
}
