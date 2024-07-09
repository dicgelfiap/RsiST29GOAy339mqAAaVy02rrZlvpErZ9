using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Crmf
{
	// Token: 0x0200012E RID: 302
	public class CertTemplate : Asn1Encodable
	{
		// Token: 0x06000AB0 RID: 2736 RVA: 0x0004938C File Offset: 0x0004938C
		private CertTemplate(Asn1Sequence seq)
		{
			this.seq = seq;
			foreach (object obj in seq)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)obj;
				switch (asn1TaggedObject.TagNo)
				{
				case 0:
					this.version = DerInteger.GetInstance(asn1TaggedObject, false);
					break;
				case 1:
					this.serialNumber = DerInteger.GetInstance(asn1TaggedObject, false);
					break;
				case 2:
					this.signingAlg = AlgorithmIdentifier.GetInstance(asn1TaggedObject, false);
					break;
				case 3:
					this.issuer = X509Name.GetInstance(asn1TaggedObject, true);
					break;
				case 4:
					this.validity = OptionalValidity.GetInstance(Asn1Sequence.GetInstance(asn1TaggedObject, false));
					break;
				case 5:
					this.subject = X509Name.GetInstance(asn1TaggedObject, true);
					break;
				case 6:
					this.publicKey = SubjectPublicKeyInfo.GetInstance(asn1TaggedObject, false);
					break;
				case 7:
					this.issuerUID = DerBitString.GetInstance(asn1TaggedObject, false);
					break;
				case 8:
					this.subjectUID = DerBitString.GetInstance(asn1TaggedObject, false);
					break;
				case 9:
					this.extensions = X509Extensions.GetInstance(asn1TaggedObject, false);
					break;
				default:
					throw new ArgumentException("unknown tag: " + asn1TaggedObject.TagNo, "seq");
				}
			}
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x00049514 File Offset: 0x00049514
		public static CertTemplate GetInstance(object obj)
		{
			if (obj is CertTemplate)
			{
				return (CertTemplate)obj;
			}
			if (obj != null)
			{
				return new CertTemplate(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000AB2 RID: 2738 RVA: 0x0004953C File Offset: 0x0004953C
		public virtual int Version
		{
			get
			{
				return this.version.IntValueExact;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x0004954C File Offset: 0x0004954C
		public virtual DerInteger SerialNumber
		{
			get
			{
				return this.serialNumber;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000AB4 RID: 2740 RVA: 0x00049554 File Offset: 0x00049554
		public virtual AlgorithmIdentifier SigningAlg
		{
			get
			{
				return this.signingAlg;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000AB5 RID: 2741 RVA: 0x0004955C File Offset: 0x0004955C
		public virtual X509Name Issuer
		{
			get
			{
				return this.issuer;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000AB6 RID: 2742 RVA: 0x00049564 File Offset: 0x00049564
		public virtual OptionalValidity Validity
		{
			get
			{
				return this.validity;
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000AB7 RID: 2743 RVA: 0x0004956C File Offset: 0x0004956C
		public virtual X509Name Subject
		{
			get
			{
				return this.subject;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x00049574 File Offset: 0x00049574
		public virtual SubjectPublicKeyInfo PublicKey
		{
			get
			{
				return this.publicKey;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000AB9 RID: 2745 RVA: 0x0004957C File Offset: 0x0004957C
		public virtual DerBitString IssuerUID
		{
			get
			{
				return this.issuerUID;
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000ABA RID: 2746 RVA: 0x00049584 File Offset: 0x00049584
		public virtual DerBitString SubjectUID
		{
			get
			{
				return this.subjectUID;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x0004958C File Offset: 0x0004958C
		public virtual X509Extensions Extensions
		{
			get
			{
				return this.extensions;
			}
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x00049594 File Offset: 0x00049594
		public override Asn1Object ToAsn1Object()
		{
			return this.seq;
		}

		// Token: 0x04000752 RID: 1874
		private readonly Asn1Sequence seq;

		// Token: 0x04000753 RID: 1875
		private readonly DerInteger version;

		// Token: 0x04000754 RID: 1876
		private readonly DerInteger serialNumber;

		// Token: 0x04000755 RID: 1877
		private readonly AlgorithmIdentifier signingAlg;

		// Token: 0x04000756 RID: 1878
		private readonly X509Name issuer;

		// Token: 0x04000757 RID: 1879
		private readonly OptionalValidity validity;

		// Token: 0x04000758 RID: 1880
		private readonly X509Name subject;

		// Token: 0x04000759 RID: 1881
		private readonly SubjectPublicKeyInfo publicKey;

		// Token: 0x0400075A RID: 1882
		private readonly DerBitString issuerUID;

		// Token: 0x0400075B RID: 1883
		private readonly DerBitString subjectUID;

		// Token: 0x0400075C RID: 1884
		private readonly X509Extensions extensions;
	}
}
