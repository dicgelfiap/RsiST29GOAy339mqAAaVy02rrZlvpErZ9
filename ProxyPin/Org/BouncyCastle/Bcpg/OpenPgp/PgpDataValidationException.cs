using System;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x0200064A RID: 1610
	[Serializable]
	public class PgpDataValidationException : PgpException
	{
		// Token: 0x060037FE RID: 14334 RVA: 0x0012D5A0 File Offset: 0x0012D5A0
		public PgpDataValidationException()
		{
		}

		// Token: 0x060037FF RID: 14335 RVA: 0x0012D5A8 File Offset: 0x0012D5A8
		public PgpDataValidationException(string message) : base(message)
		{
		}

		// Token: 0x06003800 RID: 14336 RVA: 0x0012D5B4 File Offset: 0x0012D5B4
		public PgpDataValidationException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
