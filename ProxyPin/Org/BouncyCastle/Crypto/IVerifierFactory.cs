using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x0200041A RID: 1050
	public interface IVerifierFactory
	{
		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06002184 RID: 8580
		object AlgorithmDetails { get; }

		// Token: 0x06002185 RID: 8581
		IStreamCalculator CreateCalculator();
	}
}
