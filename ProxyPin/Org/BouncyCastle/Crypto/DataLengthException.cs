using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x02000563 RID: 1379
	[Serializable]
	public class DataLengthException : CryptoException
	{
		// Token: 0x06002AE9 RID: 10985 RVA: 0x000E5050 File Offset: 0x000E5050
		public DataLengthException()
		{
		}

		// Token: 0x06002AEA RID: 10986 RVA: 0x000E5058 File Offset: 0x000E5058
		public DataLengthException(string message) : base(message)
		{
		}

		// Token: 0x06002AEB RID: 10987 RVA: 0x000E5064 File Offset: 0x000E5064
		public DataLengthException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
