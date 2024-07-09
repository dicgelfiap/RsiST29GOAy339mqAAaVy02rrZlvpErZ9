using System;
using System.IO;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200053A RID: 1338
	public class TlsException : IOException
	{
		// Token: 0x06002914 RID: 10516 RVA: 0x000DD298 File Offset: 0x000DD298
		public TlsException(string message, Exception cause) : base(message, cause)
		{
		}
	}
}
