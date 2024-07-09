using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000214 RID: 532
	public class CrlEntry : Asn1Encodable
	{
		// Token: 0x06001118 RID: 4376 RVA: 0x0006210C File Offset: 0x0006210C
		public CrlEntry(Asn1Sequence seq)
		{
			if (seq.Count < 2 || seq.Count > 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			this.seq = seq;
			this.userCertificate = DerInteger.GetInstance(seq[0]);
			this.revocationDate = Time.GetInstance(seq[1]);
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06001119 RID: 4377 RVA: 0x00062184 File Offset: 0x00062184
		public DerInteger UserCertificate
		{
			get
			{
				return this.userCertificate;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x0600111A RID: 4378 RVA: 0x0006218C File Offset: 0x0006218C
		public Time RevocationDate
		{
			get
			{
				return this.revocationDate;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x0600111B RID: 4379 RVA: 0x00062194 File Offset: 0x00062194
		public X509Extensions Extensions
		{
			get
			{
				if (this.crlEntryExtensions == null && this.seq.Count == 3)
				{
					this.crlEntryExtensions = X509Extensions.GetInstance(this.seq[2]);
				}
				return this.crlEntryExtensions;
			}
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x000621D0 File Offset: 0x000621D0
		public override Asn1Object ToAsn1Object()
		{
			return this.seq;
		}

		// Token: 0x04000C4D RID: 3149
		internal Asn1Sequence seq;

		// Token: 0x04000C4E RID: 3150
		internal DerInteger userCertificate;

		// Token: 0x04000C4F RID: 3151
		internal Time revocationDate;

		// Token: 0x04000C50 RID: 3152
		internal X509Extensions crlEntryExtensions;
	}
}
