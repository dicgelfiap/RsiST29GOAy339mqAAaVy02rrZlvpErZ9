using System;
using System.IO;

namespace Org.BouncyCastle.Pkcs
{
	// Token: 0x02000687 RID: 1671
	public class PkcsIOException : IOException
	{
		// Token: 0x06003A51 RID: 14929 RVA: 0x0013A2F8 File Offset: 0x0013A2F8
		public PkcsIOException(string message) : base(message)
		{
		}

		// Token: 0x06003A52 RID: 14930 RVA: 0x0013A304 File Offset: 0x0013A304
		public PkcsIOException(string message, Exception underlying) : base(message, underlying)
		{
		}
	}
}
