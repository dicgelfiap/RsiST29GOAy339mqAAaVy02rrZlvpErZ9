using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x02000414 RID: 1044
	public interface IKeyUnwrapper
	{
		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x0600216D RID: 8557
		object AlgorithmDetails { get; }

		// Token: 0x0600216E RID: 8558
		IBlockResult Unwrap(byte[] cipherText, int offset, int length);
	}
}
