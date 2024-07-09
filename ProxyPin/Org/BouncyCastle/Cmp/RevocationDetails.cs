using System;
using Org.BouncyCastle.Asn1.Cmp;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Cmp
{
	// Token: 0x020002CE RID: 718
	public class RevocationDetails
	{
		// Token: 0x060015E6 RID: 5606 RVA: 0x00073034 File Offset: 0x00073034
		public RevocationDetails(RevDetails revDetails)
		{
			this.revDetails = revDetails;
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x060015E7 RID: 5607 RVA: 0x00073044 File Offset: 0x00073044
		public X509Name Subject
		{
			get
			{
				return this.revDetails.CertDetails.Subject;
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x060015E8 RID: 5608 RVA: 0x00073058 File Offset: 0x00073058
		public X509Name Issuer
		{
			get
			{
				return this.revDetails.CertDetails.Issuer;
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x060015E9 RID: 5609 RVA: 0x0007306C File Offset: 0x0007306C
		public BigInteger SerialNumber
		{
			get
			{
				return this.revDetails.CertDetails.SerialNumber.Value;
			}
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x00073084 File Offset: 0x00073084
		public RevDetails ToASN1Structure()
		{
			return this.revDetails;
		}

		// Token: 0x04000EEB RID: 3819
		private readonly RevDetails revDetails;
	}
}
