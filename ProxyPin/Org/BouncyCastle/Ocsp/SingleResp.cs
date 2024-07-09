using System;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities.Date;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Ocsp
{
	// Token: 0x02000642 RID: 1602
	public class SingleResp : X509ExtensionBase
	{
		// Token: 0x060037BB RID: 14267 RVA: 0x0012AB74 File Offset: 0x0012AB74
		public SingleResp(SingleResponse resp)
		{
			this.resp = resp;
		}

		// Token: 0x060037BC RID: 14268 RVA: 0x0012AB84 File Offset: 0x0012AB84
		public CertificateID GetCertID()
		{
			return new CertificateID(this.resp.CertId);
		}

		// Token: 0x060037BD RID: 14269 RVA: 0x0012AB98 File Offset: 0x0012AB98
		public object GetCertStatus()
		{
			CertStatus certStatus = this.resp.CertStatus;
			if (certStatus.TagNo == 0)
			{
				return null;
			}
			if (certStatus.TagNo == 1)
			{
				return new RevokedStatus(RevokedInfo.GetInstance(certStatus.Status));
			}
			return new UnknownStatus();
		}

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x060037BE RID: 14270 RVA: 0x0012ABE4 File Offset: 0x0012ABE4
		public DateTime ThisUpdate
		{
			get
			{
				return this.resp.ThisUpdate.ToDateTime();
			}
		}

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x060037BF RID: 14271 RVA: 0x0012ABF8 File Offset: 0x0012ABF8
		public DateTimeObject NextUpdate
		{
			get
			{
				if (this.resp.NextUpdate != null)
				{
					return new DateTimeObject(this.resp.NextUpdate.ToDateTime());
				}
				return null;
			}
		}

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x060037C0 RID: 14272 RVA: 0x0012AC24 File Offset: 0x0012AC24
		public X509Extensions SingleExtensions
		{
			get
			{
				return this.resp.SingleExtensions;
			}
		}

		// Token: 0x060037C1 RID: 14273 RVA: 0x0012AC34 File Offset: 0x0012AC34
		protected override X509Extensions GetX509Extensions()
		{
			return this.SingleExtensions;
		}

		// Token: 0x04001D6F RID: 7535
		internal readonly SingleResponse resp;
	}
}
