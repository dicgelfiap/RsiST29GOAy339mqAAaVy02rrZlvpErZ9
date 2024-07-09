using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x02000334 RID: 820
	public interface IDerivationFunction
	{
		// Token: 0x0600189F RID: 6303
		void Init(IDerivationParameters parameters);

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x060018A0 RID: 6304
		IDigest Digest { get; }

		// Token: 0x060018A1 RID: 6305
		int GenerateBytes(byte[] output, int outOff, int length);
	}
}
