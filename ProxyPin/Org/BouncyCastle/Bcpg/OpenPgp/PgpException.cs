using System;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x02000649 RID: 1609
	[Serializable]
	public class PgpException : Exception
	{
		// Token: 0x060037FA RID: 14330 RVA: 0x0012D578 File Offset: 0x0012D578
		public PgpException()
		{
		}

		// Token: 0x060037FB RID: 14331 RVA: 0x0012D580 File Offset: 0x0012D580
		public PgpException(string message) : base(message)
		{
		}

		// Token: 0x060037FC RID: 14332 RVA: 0x0012D58C File Offset: 0x0012D58C
		public PgpException(string message, Exception exception) : base(message, exception)
		{
		}

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x060037FD RID: 14333 RVA: 0x0012D598 File Offset: 0x0012D598
		[Obsolete("Use InnerException property")]
		public Exception UnderlyingException
		{
			get
			{
				return base.InnerException;
			}
		}
	}
}
