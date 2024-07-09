using System;

namespace Org.BouncyCastle.Crypto.Modes
{
	// Token: 0x020003F9 RID: 1017
	public interface IAeadCipher
	{
		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06002046 RID: 8262
		string AlgorithmName { get; }

		// Token: 0x06002047 RID: 8263
		void Init(bool forEncryption, ICipherParameters parameters);

		// Token: 0x06002048 RID: 8264
		void ProcessAadByte(byte input);

		// Token: 0x06002049 RID: 8265
		void ProcessAadBytes(byte[] inBytes, int inOff, int len);

		// Token: 0x0600204A RID: 8266
		int ProcessByte(byte input, byte[] outBytes, int outOff);

		// Token: 0x0600204B RID: 8267
		int ProcessBytes(byte[] inBytes, int inOff, int len, byte[] outBytes, int outOff);

		// Token: 0x0600204C RID: 8268
		int DoFinal(byte[] outBytes, int outOff);

		// Token: 0x0600204D RID: 8269
		byte[] GetMac();

		// Token: 0x0600204E RID: 8270
		int GetUpdateOutputSize(int len);

		// Token: 0x0600204F RID: 8271
		int GetOutputSize(int len);

		// Token: 0x06002050 RID: 8272
		void Reset();
	}
}
