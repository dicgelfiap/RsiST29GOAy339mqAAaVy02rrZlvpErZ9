using System;
using Org.BouncyCastle.Crypto.Prng.Drbg;

namespace Org.BouncyCastle.Crypto.Prng
{
	// Token: 0x0200048B RID: 1163
	internal interface IDrbgProvider
	{
		// Token: 0x060023D9 RID: 9177
		ISP80090Drbg Get(IEntropySource entropySource);
	}
}
