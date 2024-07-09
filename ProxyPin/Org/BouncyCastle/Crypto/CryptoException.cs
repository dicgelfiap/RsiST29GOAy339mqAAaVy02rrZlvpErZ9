using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x02000562 RID: 1378
	[Serializable]
	public class CryptoException : Exception
	{
		// Token: 0x06002AE6 RID: 10982 RVA: 0x000E5030 File Offset: 0x000E5030
		public CryptoException()
		{
		}

		// Token: 0x06002AE7 RID: 10983 RVA: 0x000E5038 File Offset: 0x000E5038
		public CryptoException(string message) : base(message)
		{
		}

		// Token: 0x06002AE8 RID: 10984 RVA: 0x000E5044 File Offset: 0x000E5044
		public CryptoException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
