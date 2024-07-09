using System;

namespace Org.BouncyCastle.Security.Certificates
{
	// Token: 0x020006A2 RID: 1698
	[Serializable]
	public class CertificateException : GeneralSecurityException
	{
		// Token: 0x06003B84 RID: 15236 RVA: 0x001443CC File Offset: 0x001443CC
		public CertificateException()
		{
		}

		// Token: 0x06003B85 RID: 15237 RVA: 0x001443D4 File Offset: 0x001443D4
		public CertificateException(string message) : base(message)
		{
		}

		// Token: 0x06003B86 RID: 15238 RVA: 0x001443E0 File Offset: 0x001443E0
		public CertificateException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
