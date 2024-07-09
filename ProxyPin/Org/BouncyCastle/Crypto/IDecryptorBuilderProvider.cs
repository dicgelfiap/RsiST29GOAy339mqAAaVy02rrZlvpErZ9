using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x02000564 RID: 1380
	public interface IDecryptorBuilderProvider
	{
		// Token: 0x06002AEC RID: 10988
		ICipherBuilder CreateDecryptorBuilder(object algorithmDetails);
	}
}
