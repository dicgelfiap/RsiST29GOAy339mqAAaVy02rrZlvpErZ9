using System;

namespace Org.BouncyCastle.Security.Certificates
{
	// Token: 0x020006A6 RID: 1702
	[Serializable]
	public class CertificateParsingException : CertificateException
	{
		// Token: 0x06003B90 RID: 15248 RVA: 0x0014444C File Offset: 0x0014444C
		public CertificateParsingException()
		{
		}

		// Token: 0x06003B91 RID: 15249 RVA: 0x00144454 File Offset: 0x00144454
		public CertificateParsingException(string message) : base(message)
		{
		}

		// Token: 0x06003B92 RID: 15250 RVA: 0x00144460 File Offset: 0x00144460
		public CertificateParsingException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
