using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x02000483 RID: 1155
	public interface IEntropySourceProvider
	{
		// Token: 0x060023BA RID: 9146
		IEntropySource Get(int bitsRequired);
	}
}
