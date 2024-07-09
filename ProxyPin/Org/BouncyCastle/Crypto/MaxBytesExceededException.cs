using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x02000566 RID: 1382
	[Serializable]
	public class MaxBytesExceededException : CryptoException
	{
		// Token: 0x06002AF0 RID: 10992 RVA: 0x000E5090 File Offset: 0x000E5090
		public MaxBytesExceededException()
		{
		}

		// Token: 0x06002AF1 RID: 10993 RVA: 0x000E5098 File Offset: 0x000E5098
		public MaxBytesExceededException(string message) : base(message)
		{
		}

		// Token: 0x06002AF2 RID: 10994 RVA: 0x000E50A4 File Offset: 0x000E50A4
		public MaxBytesExceededException(string message, Exception e) : base(message, e)
		{
		}
	}
}
