using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x02000485 RID: 1157
	public interface IEntropySource
	{
		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x060023BD RID: 9149
		bool IsPredictionResistant { get; }

		// Token: 0x060023BE RID: 9150
		byte[] GetEntropy();

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x060023BF RID: 9151
		int EntropySize { get; }
	}
}
