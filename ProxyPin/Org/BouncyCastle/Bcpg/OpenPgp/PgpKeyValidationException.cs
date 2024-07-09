using System;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x02000653 RID: 1619
	[Serializable]
	public class PgpKeyValidationException : PgpException
	{
		// Token: 0x06003836 RID: 14390 RVA: 0x0012E50C File Offset: 0x0012E50C
		public PgpKeyValidationException()
		{
		}

		// Token: 0x06003837 RID: 14391 RVA: 0x0012E514 File Offset: 0x0012E514
		public PgpKeyValidationException(string message) : base(message)
		{
		}

		// Token: 0x06003838 RID: 14392 RVA: 0x0012E520 File Offset: 0x0012E520
		public PgpKeyValidationException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
