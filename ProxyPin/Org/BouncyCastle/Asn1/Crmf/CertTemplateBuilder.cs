using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Crmf
{
	// Token: 0x0200012F RID: 303
	public class CertTemplateBuilder
	{
		// Token: 0x06000ABD RID: 2749 RVA: 0x0004959C File Offset: 0x0004959C
		public virtual CertTemplateBuilder SetVersion(int ver)
		{
			this.version = new DerInteger(ver);
			return this;
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x000495AC File Offset: 0x000495AC
		public virtual CertTemplateBuilder SetSerialNumber(DerInteger ser)
		{
			this.serialNumber = ser;
			return this;
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x000495B8 File Offset: 0x000495B8
		public virtual CertTemplateBuilder SetSigningAlg(AlgorithmIdentifier aid)
		{
			this.signingAlg = aid;
			return this;
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x000495C4 File Offset: 0x000495C4
		public virtual CertTemplateBuilder SetIssuer(X509Name name)
		{
			this.issuer = name;
			return this;
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x000495D0 File Offset: 0x000495D0
		public virtual CertTemplateBuilder SetValidity(OptionalValidity v)
		{
			this.validity = v;
			return this;
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x000495DC File Offset: 0x000495DC
		public virtual CertTemplateBuilder SetSubject(X509Name name)
		{
			this.subject = name;
			return this;
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x000495E8 File Offset: 0x000495E8
		public virtual CertTemplateBuilder SetPublicKey(SubjectPublicKeyInfo spki)
		{
			this.publicKey = spki;
			return this;
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x000495F4 File Offset: 0x000495F4
		public virtual CertTemplateBuilder SetIssuerUID(DerBitString uid)
		{
			this.issuerUID = uid;
			return this;
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x00049600 File Offset: 0x00049600
		public virtual CertTemplateBuilder SetSubjectUID(DerBitString uid)
		{
			this.subjectUID = uid;
			return this;
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x0004960C File Offset: 0x0004960C
		public virtual CertTemplateBuilder SetExtensions(X509Extensions extens)
		{
			this.extensions = extens;
			return this;
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x00049618 File Offset: 0x00049618
		public virtual CertTemplate Build()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			this.AddOptional(asn1EncodableVector, 0, false, this.version);
			this.AddOptional(asn1EncodableVector, 1, false, this.serialNumber);
			this.AddOptional(asn1EncodableVector, 2, false, this.signingAlg);
			this.AddOptional(asn1EncodableVector, 3, true, this.issuer);
			this.AddOptional(asn1EncodableVector, 4, false, this.validity);
			this.AddOptional(asn1EncodableVector, 5, true, this.subject);
			this.AddOptional(asn1EncodableVector, 6, false, this.publicKey);
			this.AddOptional(asn1EncodableVector, 7, false, this.issuerUID);
			this.AddOptional(asn1EncodableVector, 8, false, this.subjectUID);
			this.AddOptional(asn1EncodableVector, 9, false, this.extensions);
			return CertTemplate.GetInstance(new DerSequence(asn1EncodableVector));
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x000496D4 File Offset: 0x000496D4
		private void AddOptional(Asn1EncodableVector v, int tagNo, bool isExplicit, Asn1Encodable obj)
		{
			if (obj != null)
			{
				v.Add(new DerTaggedObject(isExplicit, tagNo, obj));
			}
		}

		// Token: 0x0400075D RID: 1885
		private DerInteger version;

		// Token: 0x0400075E RID: 1886
		private DerInteger serialNumber;

		// Token: 0x0400075F RID: 1887
		private AlgorithmIdentifier signingAlg;

		// Token: 0x04000760 RID: 1888
		private X509Name issuer;

		// Token: 0x04000761 RID: 1889
		private OptionalValidity validity;

		// Token: 0x04000762 RID: 1890
		private X509Name subject;

		// Token: 0x04000763 RID: 1891
		private SubjectPublicKeyInfo publicKey;

		// Token: 0x04000764 RID: 1892
		private DerBitString issuerUID;

		// Token: 0x04000765 RID: 1893
		private DerBitString subjectUID;

		// Token: 0x04000766 RID: 1894
		private X509Extensions extensions;
	}
}
