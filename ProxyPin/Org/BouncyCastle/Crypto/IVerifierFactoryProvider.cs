using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x0200041C RID: 1052
	public interface IVerifierFactoryProvider
	{
		// Token: 0x0600218A RID: 8586
		IVerifierFactory CreateVerifierFactory(object algorithmDetails);
	}
}
