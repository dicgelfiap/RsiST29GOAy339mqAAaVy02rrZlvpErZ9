using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x02000418 RID: 1048
	public interface ISignatureFactory
	{
		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x0600217D RID: 8573
		object AlgorithmDetails { get; }

		// Token: 0x0600217E RID: 8574
		IStreamCalculator CreateCalculator();
	}
}
