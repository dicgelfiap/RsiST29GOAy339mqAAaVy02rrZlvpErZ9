using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x0200039F RID: 927
	public interface IRsa
	{
		// Token: 0x06001D6E RID: 7534
		void Init(bool forEncryption, ICipherParameters parameters);

		// Token: 0x06001D6F RID: 7535
		int GetInputBlockSize();

		// Token: 0x06001D70 RID: 7536
		int GetOutputBlockSize();

		// Token: 0x06001D71 RID: 7537
		BigInteger ConvertInput(byte[] buf, int off, int len);

		// Token: 0x06001D72 RID: 7538
		BigInteger ProcessBlock(BigInteger input);

		// Token: 0x06001D73 RID: 7539
		byte[] ConvertOutput(BigInteger result);
	}
}
