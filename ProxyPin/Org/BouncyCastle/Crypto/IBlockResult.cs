using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x02000328 RID: 808
	public interface IBlockResult
	{
		// Token: 0x06001845 RID: 6213
		byte[] Collect();

		// Token: 0x06001846 RID: 6214
		int Collect(byte[] destination, int offset);
	}
}
