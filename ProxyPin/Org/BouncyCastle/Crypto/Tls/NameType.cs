using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200050E RID: 1294
	public abstract class NameType
	{
		// Token: 0x0600276F RID: 10095 RVA: 0x000D5AA8 File Offset: 0x000D5AA8
		public static bool IsValid(byte nameType)
		{
			return nameType == 0;
		}

		// Token: 0x04001A02 RID: 6658
		public const byte host_name = 0;
	}
}
