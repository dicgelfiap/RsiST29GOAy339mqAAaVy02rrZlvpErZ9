using System;
using System.IO;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x0200040B RID: 1035
	public interface ICipherBuilder
	{
		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06002153 RID: 8531
		object AlgorithmDetails { get; }

		// Token: 0x06002154 RID: 8532
		int GetMaxOutputSize(int inputLen);

		// Token: 0x06002155 RID: 8533
		ICipher BuildCipher(Stream stream);
	}
}
