using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020001A5 RID: 421
	public class CertificationRequestInfo : Asn1Encodable
	{
		// Token: 0x06000DC7 RID: 3527 RVA: 0x000550E4 File Offset: 0x000550E4
		public static CertificationRequestInfo GetInstance(object obj)
		{
			if (obj is CertificationRequestInfo)
			{
				return (CertificationRequestInfo)obj;
			}
			if (obj != null)
			{
				return new CertificationRequestInfo(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x0005510C File Offset: 0x0005510C
		public CertificationRequestInfo(X509Name subject, SubjectPublicKeyInfo pkInfo, Asn1Set attributes)
		{
			this.subject = subject;
			this.subjectPKInfo = pkInfo;
			this.attributes = attributes;
			CertificationRequestInfo.ValidateAttributes(attributes);
			if (subject == null || this.version == null || this.subjectPKInfo == null)
			{
				throw new ArgumentException("Not all mandatory fields set in CertificationRequestInfo generator.");
			}
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x00055174 File Offset: 0x00055174
		private CertificationRequestInfo(Asn1Sequence seq)
		{
			this.version = (DerInteger)seq[0];
			this.subject = X509Name.GetInstance(seq[1]);
			this.subjectPKInfo = SubjectPublicKeyInfo.GetInstance(seq[2]);
			if (seq.Count > 3)
			{
				DerTaggedObject obj = (DerTaggedObject)seq[3];
				this.attributes = Asn1Set.GetInstance(obj, false);
			}
			CertificationRequestInfo.ValidateAttributes(this.attributes);
			if (this.subject == null || this.version == null || this.subjectPKInfo == null)
			{
				throw new ArgumentException("Not all mandatory fields set in CertificationRequestInfo generator.");
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000DCA RID: 3530 RVA: 0x0005522C File Offset: 0x0005522C
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000DCB RID: 3531 RVA: 0x00055234 File Offset: 0x00055234
		public X509Name Subject
		{
			get
			{
				return this.subject;
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000DCC RID: 3532 RVA: 0x0005523C File Offset: 0x0005523C
		public SubjectPublicKeyInfo SubjectPublicKeyInfo
		{
			get
			{
				return this.subjectPKInfo;
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000DCD RID: 3533 RVA: 0x00055244 File Offset: 0x00055244
		public Asn1Set Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x0005524C File Offset: 0x0005524C
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version,
				this.subject,
				this.subjectPKInfo
			});
			asn1EncodableVector.AddOptionalTagged(false, 0, this.attributes);
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x000552A8 File Offset: 0x000552A8
		private static void ValidateAttributes(Asn1Set attributes)
		{
			if (attributes == null)
			{
				return;
			}
			foreach (object obj in attributes)
			{
				Asn1Encodable asn1Encodable = (Asn1Encodable)obj;
				Asn1Object obj2 = asn1Encodable.ToAsn1Object();
				AttributePkcs instance = AttributePkcs.GetInstance(obj2);
				if (instance.AttrType.Equals(PkcsObjectIdentifiers.Pkcs9AtChallengePassword) && instance.AttrValues.Count != 1)
				{
					throw new ArgumentException("challengePassword attribute must have one value");
				}
			}
		}

		// Token: 0x040009D3 RID: 2515
		internal DerInteger version = new DerInteger(0);

		// Token: 0x040009D4 RID: 2516
		internal X509Name subject;

		// Token: 0x040009D5 RID: 2517
		internal SubjectPublicKeyInfo subjectPKInfo;

		// Token: 0x040009D6 RID: 2518
		internal Asn1Set attributes;
	}
}
