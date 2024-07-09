using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.BC
{
	// Token: 0x020000CB RID: 203
	public class LinkedCertificate : Asn1Encodable
	{
		// Token: 0x060007E4 RID: 2020 RVA: 0x0004070C File Offset: 0x0004070C
		public LinkedCertificate(DigestInfo digest, GeneralName certLocation) : this(digest, certLocation, null, null)
		{
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00040718 File Offset: 0x00040718
		public LinkedCertificate(DigestInfo digest, GeneralName certLocation, X509Name certIssuer, GeneralNames caCerts)
		{
			this.mDigest = digest;
			this.mCertLocation = certLocation;
			this.mCertIssuer = certIssuer;
			this.mCACerts = caCerts;
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00040740 File Offset: 0x00040740
		private LinkedCertificate(Asn1Sequence seq)
		{
			this.mDigest = DigestInfo.GetInstance(seq[0]);
			this.mCertLocation = GeneralName.GetInstance(seq[1]);
			for (int i = 2; i < seq.Count; i++)
			{
				Asn1TaggedObject instance = Asn1TaggedObject.GetInstance(seq[i]);
				switch (instance.TagNo)
				{
				case 0:
					this.mCertIssuer = X509Name.GetInstance(instance, false);
					break;
				case 1:
					this.mCACerts = GeneralNames.GetInstance(instance, false);
					break;
				default:
					throw new ArgumentException("unknown tag in tagged field");
				}
			}
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x000407E8 File Offset: 0x000407E8
		public static LinkedCertificate GetInstance(object obj)
		{
			if (obj is LinkedCertificate)
			{
				return (LinkedCertificate)obj;
			}
			if (obj != null)
			{
				return new LinkedCertificate(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060007E8 RID: 2024 RVA: 0x00040810 File Offset: 0x00040810
		public virtual DigestInfo Digest
		{
			get
			{
				return this.mDigest;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060007E9 RID: 2025 RVA: 0x00040818 File Offset: 0x00040818
		public virtual GeneralName CertLocation
		{
			get
			{
				return this.mCertLocation;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060007EA RID: 2026 RVA: 0x00040820 File Offset: 0x00040820
		public virtual X509Name CertIssuer
		{
			get
			{
				return this.mCertIssuer;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060007EB RID: 2027 RVA: 0x00040828 File Offset: 0x00040828
		public virtual GeneralNames CACerts
		{
			get
			{
				return this.mCACerts;
			}
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x00040830 File Offset: 0x00040830
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.mDigest,
				this.mCertLocation
			});
			asn1EncodableVector.AddOptionalTagged(false, 0, this.mCertIssuer);
			asn1EncodableVector.AddOptionalTagged(false, 1, this.mCACerts);
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040005BD RID: 1469
		private readonly DigestInfo mDigest;

		// Token: 0x040005BE RID: 1470
		private readonly GeneralName mCertLocation;

		// Token: 0x040005BF RID: 1471
		private X509Name mCertIssuer;

		// Token: 0x040005C0 RID: 1472
		private GeneralNames mCACerts;
	}
}
