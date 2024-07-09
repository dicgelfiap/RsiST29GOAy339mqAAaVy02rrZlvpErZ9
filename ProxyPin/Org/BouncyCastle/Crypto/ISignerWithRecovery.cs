using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x020004A8 RID: 1192
	public interface ISignerWithRecovery : ISigner
	{
		// Token: 0x060024A1 RID: 9377
		bool HasFullMessage();

		// Token: 0x060024A2 RID: 9378
		byte[] GetRecoveredMessage();

		// Token: 0x060024A3 RID: 9379
		void UpdateWithRecoveredMessage(byte[] signature);
	}
}
