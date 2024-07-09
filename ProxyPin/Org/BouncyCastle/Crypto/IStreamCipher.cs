using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x02000382 RID: 898
	public interface IStreamCipher
	{
		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06001C19 RID: 7193
		string AlgorithmName { get; }

		// Token: 0x06001C1A RID: 7194
		void Init(bool forEncryption, ICipherParameters parameters);

		// Token: 0x06001C1B RID: 7195
		byte ReturnByte(byte input);

		// Token: 0x06001C1C RID: 7196
		void ProcessBytes(byte[] input, int inOff, int length, byte[] output, int outOff);

		// Token: 0x06001C1D RID: 7197
		void Reset();
	}
}
