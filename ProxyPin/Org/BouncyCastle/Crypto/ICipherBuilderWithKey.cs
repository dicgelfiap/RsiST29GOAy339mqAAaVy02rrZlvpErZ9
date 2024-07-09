using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x0200040C RID: 1036
	public interface ICipherBuilderWithKey : ICipherBuilder
	{
		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06002156 RID: 8534
		ICipherParameters Key { get; }
	}
}
