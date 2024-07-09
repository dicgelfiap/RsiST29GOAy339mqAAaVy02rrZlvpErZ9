using System;
using Org.BouncyCastle.Asn1.Crmf;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020000F5 RID: 245
	public class RevDetails : Asn1Encodable
	{
		// Token: 0x060008FB RID: 2299 RVA: 0x00043D20 File Offset: 0x00043D20
		private RevDetails(Asn1Sequence seq)
		{
			this.certDetails = CertTemplate.GetInstance(seq[0]);
			this.crlEntryDetails = ((seq.Count <= 1) ? null : X509Extensions.GetInstance(seq[1]));
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x00043D70 File Offset: 0x00043D70
		public static RevDetails GetInstance(object obj)
		{
			if (obj is RevDetails)
			{
				return (RevDetails)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new RevDetails((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x00043DC4 File Offset: 0x00043DC4
		public RevDetails(CertTemplate certDetails) : this(certDetails, null)
		{
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x00043DD0 File Offset: 0x00043DD0
		public RevDetails(CertTemplate certDetails, X509Extensions crlEntryDetails)
		{
			this.certDetails = certDetails;
			this.crlEntryDetails = crlEntryDetails;
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x00043DE8 File Offset: 0x00043DE8
		public virtual CertTemplate CertDetails
		{
			get
			{
				return this.certDetails;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x00043DF0 File Offset: 0x00043DF0
		public virtual X509Extensions CrlEntryDetails
		{
			get
			{
				return this.crlEntryDetails;
			}
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x00043DF8 File Offset: 0x00043DF8
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.certDetails
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.crlEntryDetails
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000698 RID: 1688
		private readonly CertTemplate certDetails;

		// Token: 0x04000699 RID: 1689
		private readonly X509Extensions crlEntryDetails;
	}
}
