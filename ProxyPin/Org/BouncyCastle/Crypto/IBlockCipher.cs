using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x02000375 RID: 885
	public interface IBlockCipher
	{
		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06001B7B RID: 7035
		string AlgorithmName { get; }

		// Token: 0x06001B7C RID: 7036
		void Init(bool forEncryption, ICipherParameters parameters);

		// Token: 0x06001B7D RID: 7037
		int GetBlockSize();

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06001B7E RID: 7038
		bool IsPartialBlockOkay { get; }

		// Token: 0x06001B7F RID: 7039
		int ProcessBlock(byte[] inBuf, int inOff, byte[] outBuf, int outOff);

		// Token: 0x06001B80 RID: 7040
		void Reset();
	}
}
