using System;
using Org.BouncyCastle.Asn1.Crmf;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020000F4 RID: 244
	public class RevAnnContent : Asn1Encodable
	{
		// Token: 0x060008F3 RID: 2291 RVA: 0x00043BB0 File Offset: 0x00043BB0
		private RevAnnContent(Asn1Sequence seq)
		{
			this.status = PkiStatusEncodable.GetInstance(seq[0]);
			this.certId = CertId.GetInstance(seq[1]);
			this.willBeRevokedAt = DerGeneralizedTime.GetInstance(seq[2]);
			this.badSinceDate = DerGeneralizedTime.GetInstance(seq[3]);
			if (seq.Count > 4)
			{
				this.crlDetails = X509Extensions.GetInstance(seq[4]);
			}
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x00043C30 File Offset: 0x00043C30
		public static RevAnnContent GetInstance(object obj)
		{
			if (obj is RevAnnContent)
			{
				return (RevAnnContent)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new RevAnnContent((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060008F5 RID: 2293 RVA: 0x00043C84 File Offset: 0x00043C84
		public virtual PkiStatusEncodable Status
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060008F6 RID: 2294 RVA: 0x00043C8C File Offset: 0x00043C8C
		public virtual CertId CertID
		{
			get
			{
				return this.certId;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x00043C94 File Offset: 0x00043C94
		public virtual DerGeneralizedTime WillBeRevokedAt
		{
			get
			{
				return this.willBeRevokedAt;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060008F8 RID: 2296 RVA: 0x00043C9C File Offset: 0x00043C9C
		public virtual DerGeneralizedTime BadSinceDate
		{
			get
			{
				return this.badSinceDate;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x00043CA4 File Offset: 0x00043CA4
		public virtual X509Extensions CrlDetails
		{
			get
			{
				return this.crlDetails;
			}
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x00043CAC File Offset: 0x00043CAC
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.status,
				this.certId,
				this.willBeRevokedAt,
				this.badSinceDate
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.crlDetails
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000693 RID: 1683
		private readonly PkiStatusEncodable status;

		// Token: 0x04000694 RID: 1684
		private readonly CertId certId;

		// Token: 0x04000695 RID: 1685
		private readonly DerGeneralizedTime willBeRevokedAt;

		// Token: 0x04000696 RID: 1686
		private readonly DerGeneralizedTime badSinceDate;

		// Token: 0x04000697 RID: 1687
		private readonly X509Extensions crlDetails;
	}
}
