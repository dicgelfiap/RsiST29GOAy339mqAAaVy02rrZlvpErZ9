using System;

namespace Org.BouncyCastle.Pkcs
{
	// Token: 0x02000686 RID: 1670
	public class PkcsException : Exception
	{
		// Token: 0x06003A4F RID: 14927 RVA: 0x0013A2E0 File Offset: 0x0013A2E0
		public PkcsException(string message) : base(message)
		{
		}

		// Token: 0x06003A50 RID: 14928 RVA: 0x0013A2EC File Offset: 0x0013A2EC
		public PkcsException(string message, Exception underlying) : base(message, underlying)
		{
		}
	}
}
