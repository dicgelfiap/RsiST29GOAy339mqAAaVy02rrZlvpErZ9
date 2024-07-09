using System;
using Org.BouncyCastle.Asn1.Crmf;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020000F7 RID: 247
	public class RevRepContentBuilder
	{
		// Token: 0x06000908 RID: 2312 RVA: 0x00044064 File Offset: 0x00044064
		public virtual RevRepContentBuilder Add(PkiStatusInfo status)
		{
			this.status.Add(status);
			return this;
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x00044074 File Offset: 0x00044074
		public virtual RevRepContentBuilder Add(PkiStatusInfo status, CertId certId)
		{
			if (this.status.Count != this.revCerts.Count)
			{
				throw new InvalidOperationException("status and revCerts sequence must be in common order");
			}
			this.status.Add(status);
			this.revCerts.Add(certId);
			return this;
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x000440C4 File Offset: 0x000440C4
		public virtual RevRepContentBuilder AddCrl(CertificateList crl)
		{
			this.crls.Add(crl);
			return this;
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x000440D4 File Offset: 0x000440D4
		public virtual RevRepContent Build()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			asn1EncodableVector.Add(new DerSequence(this.status));
			if (this.revCerts.Count != 0)
			{
				asn1EncodableVector.Add(new DerTaggedObject(true, 0, new DerSequence(this.revCerts)));
			}
			if (this.crls.Count != 0)
			{
				asn1EncodableVector.Add(new DerTaggedObject(true, 1, new DerSequence(this.crls)));
			}
			return RevRepContent.GetInstance(new DerSequence(asn1EncodableVector));
		}

		// Token: 0x0400069D RID: 1693
		private readonly Asn1EncodableVector status = new Asn1EncodableVector();

		// Token: 0x0400069E RID: 1694
		private readonly Asn1EncodableVector revCerts = new Asn1EncodableVector();

		// Token: 0x0400069F RID: 1695
		private readonly Asn1EncodableVector crls = new Asn1EncodableVector();
	}
}
