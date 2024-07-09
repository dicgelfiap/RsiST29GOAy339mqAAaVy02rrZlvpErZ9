using System;

namespace Org.BouncyCastle.Security.Certificates
{
	// Token: 0x020006A3 RID: 1699
	[Serializable]
	public class CertificateEncodingException : CertificateException
	{
		// Token: 0x06003B87 RID: 15239 RVA: 0x001443EC File Offset: 0x001443EC
		public CertificateEncodingException()
		{
		}

		// Token: 0x06003B88 RID: 15240 RVA: 0x001443F4 File Offset: 0x001443F4
		public CertificateEncodingException(string msg) : base(msg)
		{
		}

		// Token: 0x06003B89 RID: 15241 RVA: 0x00144400 File Offset: 0x00144400
		public CertificateEncodingException(string msg, Exception e) : base(msg, e)
		{
		}
	}
}
