using System;

namespace Org.BouncyCastle.Crypto.Prng.Drbg
{
	// Token: 0x0200047E RID: 1150
	public interface ISP80090Drbg
	{
		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x0600238D RID: 9101
		int BlockSize { get; }

		// Token: 0x0600238E RID: 9102
		int Generate(byte[] output, byte[] additionalInput, bool predictionResistant);

		// Token: 0x0600238F RID: 9103
		void Reseed(byte[] additionalInput);
	}
}
