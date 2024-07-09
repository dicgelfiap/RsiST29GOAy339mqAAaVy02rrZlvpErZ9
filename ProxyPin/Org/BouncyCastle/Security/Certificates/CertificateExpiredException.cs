using System;

namespace Org.BouncyCastle.Security.Certificates
{
	// Token: 0x020006A4 RID: 1700
	[Serializable]
	public class CertificateExpiredException : CertificateException
	{
		// Token: 0x06003B8A RID: 15242 RVA: 0x0014440C File Offset: 0x0014440C
		public CertificateExpiredException()
		{
		}

		// Token: 0x06003B8B RID: 15243 RVA: 0x00144414 File Offset: 0x00144414
		public CertificateExpiredException(string message) : base(message)
		{
		}

		// Token: 0x06003B8C RID: 15244 RVA: 0x00144420 File Offset: 0x00144420
		public CertificateExpiredException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
