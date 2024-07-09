using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004D0 RID: 1232
	public abstract class CertChainType
	{
		// Token: 0x0600261C RID: 9756 RVA: 0x000CFE98 File Offset: 0x000CFE98
		public static bool IsValid(byte certChainType)
		{
			return certChainType >= 0 && certChainType <= 1;
		}

		// Token: 0x040017D8 RID: 6104
		public const byte individual_certs = 0;

		// Token: 0x040017D9 RID: 6105
		public const byte pkipath = 1;
	}
}
