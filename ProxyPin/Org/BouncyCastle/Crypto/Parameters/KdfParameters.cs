using System;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000461 RID: 1121
	public class KdfParameters : IDerivationParameters
	{
		// Token: 0x06002309 RID: 8969 RVA: 0x000C60A0 File Offset: 0x000C60A0
		public KdfParameters(byte[] shared, byte[] iv)
		{
			this.shared = shared;
			this.iv = iv;
		}

		// Token: 0x0600230A RID: 8970 RVA: 0x000C60B8 File Offset: 0x000C60B8
		public byte[] GetSharedSecret()
		{
			return this.shared;
		}

		// Token: 0x0600230B RID: 8971 RVA: 0x000C60C0 File Offset: 0x000C60C0
		public byte[] GetIV()
		{
			return this.iv;
		}

		// Token: 0x0400164B RID: 5707
		private byte[] iv;

		// Token: 0x0400164C RID: 5708
		private byte[] shared;
	}
}
