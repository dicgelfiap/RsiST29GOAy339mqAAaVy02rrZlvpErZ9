using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x02000371 RID: 881
	public interface IAsymmetricBlockCipher
	{
		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06001B4B RID: 6987
		string AlgorithmName { get; }

		// Token: 0x06001B4C RID: 6988
		void Init(bool forEncryption, ICipherParameters parameters);

		// Token: 0x06001B4D RID: 6989
		int GetInputBlockSize();

		// Token: 0x06001B4E RID: 6990
		int GetOutputBlockSize();

		// Token: 0x06001B4F RID: 6991
		byte[] ProcessBlock(byte[] inBuf, int inOff, int inLen);
	}
}
