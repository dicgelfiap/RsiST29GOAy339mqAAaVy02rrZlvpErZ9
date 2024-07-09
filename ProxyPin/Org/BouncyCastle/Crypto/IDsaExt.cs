using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x02000498 RID: 1176
	public interface IDsaExt : IDsa
	{
		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06002434 RID: 9268
		BigInteger Order { get; }
	}
}
