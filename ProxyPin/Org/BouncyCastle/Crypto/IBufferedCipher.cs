using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x020003FE RID: 1022
	public interface IBufferedCipher
	{
		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06002089 RID: 8329
		string AlgorithmName { get; }

		// Token: 0x0600208A RID: 8330
		void Init(bool forEncryption, ICipherParameters parameters);

		// Token: 0x0600208B RID: 8331
		int GetBlockSize();

		// Token: 0x0600208C RID: 8332
		int GetOutputSize(int inputLen);

		// Token: 0x0600208D RID: 8333
		int GetUpdateOutputSize(int inputLen);

		// Token: 0x0600208E RID: 8334
		byte[] ProcessByte(byte input);

		// Token: 0x0600208F RID: 8335
		int ProcessByte(byte input, byte[] output, int outOff);

		// Token: 0x06002090 RID: 8336
		byte[] ProcessBytes(byte[] input);

		// Token: 0x06002091 RID: 8337
		byte[] ProcessBytes(byte[] input, int inOff, int length);

		// Token: 0x06002092 RID: 8338
		int ProcessBytes(byte[] input, byte[] output, int outOff);

		// Token: 0x06002093 RID: 8339
		int ProcessBytes(byte[] input, int inOff, int length, byte[] output, int outOff);

		// Token: 0x06002094 RID: 8340
		byte[] DoFinal();

		// Token: 0x06002095 RID: 8341
		byte[] DoFinal(byte[] input);

		// Token: 0x06002096 RID: 8342
		byte[] DoFinal(byte[] input, int inOff, int length);

		// Token: 0x06002097 RID: 8343
		int DoFinal(byte[] output, int outOff);

		// Token: 0x06002098 RID: 8344
		int DoFinal(byte[] input, byte[] output, int outOff);

		// Token: 0x06002099 RID: 8345
		int DoFinal(byte[] input, int inOff, int length, byte[] output, int outOff);

		// Token: 0x0600209A RID: 8346
		void Reset();
	}
}
