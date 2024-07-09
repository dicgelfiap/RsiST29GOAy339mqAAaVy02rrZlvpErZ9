using System;
using System.IO;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200053F RID: 1343
	public class TlsNoCloseNotifyException : EndOfStreamException
	{
		// Token: 0x06002951 RID: 10577 RVA: 0x000DDAB0 File Offset: 0x000DDAB0
		public TlsNoCloseNotifyException() : base("No close_notify alert received before connection closed")
		{
		}
	}
}
