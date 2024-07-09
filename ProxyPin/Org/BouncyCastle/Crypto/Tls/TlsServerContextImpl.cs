using System;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000549 RID: 1353
	internal class TlsServerContextImpl : AbstractTlsContext, TlsServerContext, TlsContext
	{
		// Token: 0x06002982 RID: 10626 RVA: 0x000DE8D8 File Offset: 0x000DE8D8
		internal TlsServerContextImpl(SecureRandom secureRandom, SecurityParameters securityParameters) : base(secureRandom, securityParameters)
		{
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06002983 RID: 10627 RVA: 0x000DE8E4 File Offset: 0x000DE8E4
		public override bool IsServer
		{
			get
			{
				return true;
			}
		}
	}
}
