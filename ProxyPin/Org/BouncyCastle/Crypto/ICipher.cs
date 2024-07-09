using System;
using System.IO;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x0200040E RID: 1038
	public interface ICipher
	{
		// Token: 0x0600215C RID: 8540
		int GetMaxOutputSize(int inputLen);

		// Token: 0x0600215D RID: 8541
		int GetUpdateOutputSize(int inputLen);

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x0600215E RID: 8542
		Stream Stream { get; }
	}
}
