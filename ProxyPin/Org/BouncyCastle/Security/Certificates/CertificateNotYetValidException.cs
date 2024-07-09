using System;

namespace Org.BouncyCastle.Security.Certificates
{
	// Token: 0x020006A5 RID: 1701
	[Serializable]
	public class CertificateNotYetValidException : CertificateException
	{
		// Token: 0x06003B8D RID: 15245 RVA: 0x0014442C File Offset: 0x0014442C
		public CertificateNotYetValidException()
		{
		}

		// Token: 0x06003B8E RID: 15246 RVA: 0x00144434 File Offset: 0x00144434
		public CertificateNotYetValidException(string message) : base(message)
		{
		}

		// Token: 0x06003B8F RID: 15247 RVA: 0x00144440 File Offset: 0x00144440
		public CertificateNotYetValidException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
