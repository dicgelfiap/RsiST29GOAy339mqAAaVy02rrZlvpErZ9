using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x02000326 RID: 806
	public interface IMacFactory
	{
		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06001840 RID: 6208
		object AlgorithmDetails { get; }

		// Token: 0x06001841 RID: 6209
		IStreamCalculator CreateCalculator();
	}
}
