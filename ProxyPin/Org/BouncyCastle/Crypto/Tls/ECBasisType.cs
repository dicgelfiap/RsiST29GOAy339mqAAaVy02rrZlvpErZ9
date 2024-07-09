using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004FD RID: 1277
	public abstract class ECBasisType
	{
		// Token: 0x0600274C RID: 10060 RVA: 0x000D5674 File Offset: 0x000D5674
		public static bool IsValid(byte ecBasisType)
		{
			return ecBasisType >= 1 && ecBasisType <= 2;
		}

		// Token: 0x04001955 RID: 6485
		public const byte ec_basis_trinomial = 1;

		// Token: 0x04001956 RID: 6486
		public const byte ec_basis_pentanomial = 2;
	}
}
