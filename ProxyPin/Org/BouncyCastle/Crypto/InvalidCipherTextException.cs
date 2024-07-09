using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x02000565 RID: 1381
	[Serializable]
	public class InvalidCipherTextException : CryptoException
	{
		// Token: 0x06002AED RID: 10989 RVA: 0x000E5070 File Offset: 0x000E5070
		public InvalidCipherTextException()
		{
		}

		// Token: 0x06002AEE RID: 10990 RVA: 0x000E5078 File Offset: 0x000E5078
		public InvalidCipherTextException(string message) : base(message)
		{
		}

		// Token: 0x06002AEF RID: 10991 RVA: 0x000E5084 File Offset: 0x000E5084
		public InvalidCipherTextException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
