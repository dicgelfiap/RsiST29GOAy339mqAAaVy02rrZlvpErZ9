using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x0200034C RID: 844
	public interface IDigest
	{
		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06001919 RID: 6425
		string AlgorithmName { get; }

		// Token: 0x0600191A RID: 6426
		int GetDigestSize();

		// Token: 0x0600191B RID: 6427
		int GetByteLength();

		// Token: 0x0600191C RID: 6428
		void Update(byte input);

		// Token: 0x0600191D RID: 6429
		void BlockUpdate(byte[] input, int inOff, int length);

		// Token: 0x0600191E RID: 6430
		int DoFinal(byte[] output, int outOff);

		// Token: 0x0600191F RID: 6431
		void Reset();
	}
}
