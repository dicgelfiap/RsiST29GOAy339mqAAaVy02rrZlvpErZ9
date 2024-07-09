using System;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x0200045D RID: 1117
	public class Iso18033KdfParameters : IDerivationParameters
	{
		// Token: 0x060022EF RID: 8943 RVA: 0x000C5D88 File Offset: 0x000C5D88
		public Iso18033KdfParameters(byte[] seed)
		{
			this.seed = seed;
		}

		// Token: 0x060022F0 RID: 8944 RVA: 0x000C5D98 File Offset: 0x000C5D98
		public byte[] GetSeed()
		{
			return this.seed;
		}

		// Token: 0x0400163B RID: 5691
		private byte[] seed;
	}
}
