using System;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Ocsp
{
	// Token: 0x0200063E RID: 1598
	public class Req : X509ExtensionBase
	{
		// Token: 0x060037A5 RID: 14245 RVA: 0x0012A914 File Offset: 0x0012A914
		public Req(Request req)
		{
			this.req = req;
		}

		// Token: 0x060037A6 RID: 14246 RVA: 0x0012A924 File Offset: 0x0012A924
		public CertificateID GetCertID()
		{
			return new CertificateID(this.req.ReqCert);
		}

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x060037A7 RID: 14247 RVA: 0x0012A938 File Offset: 0x0012A938
		public X509Extensions SingleRequestExtensions
		{
			get
			{
				return this.req.SingleRequestExtensions;
			}
		}

		// Token: 0x060037A8 RID: 14248 RVA: 0x0012A948 File Offset: 0x0012A948
		protected override X509Extensions GetX509Extensions()
		{
			return this.SingleRequestExtensions;
		}

		// Token: 0x04001D6B RID: 7531
		private Request req;
	}
}
