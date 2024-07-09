using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x02000410 RID: 1040
	public interface IKeyWrapper
	{
		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06002163 RID: 8547
		object AlgorithmDetails { get; }

		// Token: 0x06002164 RID: 8548
		IBlockResult Wrap(byte[] keyData);
	}
}
