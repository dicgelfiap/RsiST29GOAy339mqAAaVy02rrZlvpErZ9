using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Ocsp
{
	// Token: 0x02000641 RID: 1601
	public class RevokedStatus : CertificateStatus
	{
		// Token: 0x060037B6 RID: 14262 RVA: 0x0012AAEC File Offset: 0x0012AAEC
		public RevokedStatus(RevokedInfo info)
		{
			this.info = info;
		}

		// Token: 0x060037B7 RID: 14263 RVA: 0x0012AAFC File Offset: 0x0012AAFC
		public RevokedStatus(DateTime revocationDate, int reason)
		{
			this.info = new RevokedInfo(new DerGeneralizedTime(revocationDate), new CrlReason(reason));
		}

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x060037B8 RID: 14264 RVA: 0x0012AB1C File Offset: 0x0012AB1C
		public DateTime RevocationTime
		{
			get
			{
				return this.info.RevocationTime.ToDateTime();
			}
		}

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x060037B9 RID: 14265 RVA: 0x0012AB30 File Offset: 0x0012AB30
		public bool HasRevocationReason
		{
			get
			{
				return this.info.RevocationReason != null;
			}
		}

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x060037BA RID: 14266 RVA: 0x0012AB44 File Offset: 0x0012AB44
		public int RevocationReason
		{
			get
			{
				if (this.info.RevocationReason == null)
				{
					throw new InvalidOperationException("attempt to get a reason where none is available");
				}
				return this.info.RevocationReason.IntValueExact;
			}
		}

		// Token: 0x04001D6E RID: 7534
		internal readonly RevokedInfo info;
	}
}
