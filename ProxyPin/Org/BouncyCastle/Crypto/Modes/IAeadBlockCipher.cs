using System;

namespace Org.BouncyCastle.Crypto.Modes
{
	// Token: 0x020003FA RID: 1018
	public interface IAeadBlockCipher : IAeadCipher
	{
		// Token: 0x06002051 RID: 8273
		int GetBlockSize();

		// Token: 0x06002052 RID: 8274
		IBlockCipher GetUnderlyingCipher();
	}
}
