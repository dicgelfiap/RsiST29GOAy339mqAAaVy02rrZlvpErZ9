using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x020003E1 RID: 993
	public interface IMac
	{
		// Token: 0x06001F6A RID: 8042
		void Init(ICipherParameters parameters);

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001F6B RID: 8043
		string AlgorithmName { get; }

		// Token: 0x06001F6C RID: 8044
		int GetMacSize();

		// Token: 0x06001F6D RID: 8045
		void Update(byte input);

		// Token: 0x06001F6E RID: 8046
		void BlockUpdate(byte[] input, int inOff, int len);

		// Token: 0x06001F6F RID: 8047
		int DoFinal(byte[] output, int outOff);

		// Token: 0x06001F70 RID: 8048
		void Reset();
	}
}
