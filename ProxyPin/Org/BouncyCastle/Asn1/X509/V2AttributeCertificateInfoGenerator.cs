using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000219 RID: 537
	public class V2AttributeCertificateInfoGenerator
	{
		// Token: 0x06001144 RID: 4420 RVA: 0x00062988 File Offset: 0x00062988
		public V2AttributeCertificateInfoGenerator()
		{
			this.version = new DerInteger(1);
			this.attributes = new Asn1EncodableVector();
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x000629A8 File Offset: 0x000629A8
		public void SetHolder(Holder holder)
		{
			this.holder = holder;
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x000629B4 File Offset: 0x000629B4
		public void AddAttribute(string oid, Asn1Encodable value)
		{
			this.attributes.Add(new AttributeX509(new DerObjectIdentifier(oid), new DerSet(value)));
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x000629D4 File Offset: 0x000629D4
		public void AddAttribute(AttributeX509 attribute)
		{
			this.attributes.Add(attribute);
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x000629E4 File Offset: 0x000629E4
		public void SetSerialNumber(DerInteger serialNumber)
		{
			this.serialNumber = serialNumber;
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x000629F0 File Offset: 0x000629F0
		public void SetSignature(AlgorithmIdentifier signature)
		{
			this.signature = signature;
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x000629FC File Offset: 0x000629FC
		public void SetIssuer(AttCertIssuer issuer)
		{
			this.issuer = issuer;
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x00062A08 File Offset: 0x00062A08
		public void SetStartDate(DerGeneralizedTime startDate)
		{
			this.startDate = startDate;
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x00062A14 File Offset: 0x00062A14
		public void SetEndDate(DerGeneralizedTime endDate)
		{
			this.endDate = endDate;
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x00062A20 File Offset: 0x00062A20
		public void SetIssuerUniqueID(DerBitString issuerUniqueID)
		{
			this.issuerUniqueID = issuerUniqueID;
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x00062A2C File Offset: 0x00062A2C
		public void SetExtensions(X509Extensions extensions)
		{
			this.extensions = extensions;
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x00062A38 File Offset: 0x00062A38
		public AttributeCertificateInfo GenerateAttributeCertificateInfo()
		{
			if (this.serialNumber == null || this.signature == null || this.issuer == null || this.startDate == null || this.endDate == null || this.holder == null || this.attributes == null)
			{
				throw new InvalidOperationException("not all mandatory fields set in V2 AttributeCertificateInfo generator");
			}
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version,
				this.holder,
				this.issuer,
				this.signature,
				this.serialNumber
			});
			asn1EncodableVector.Add(new AttCertValidityPeriod(this.startDate, this.endDate));
			asn1EncodableVector.Add(new DerSequence(this.attributes));
			if (this.issuerUniqueID != null)
			{
				asn1EncodableVector.Add(this.issuerUniqueID);
			}
			if (this.extensions != null)
			{
				asn1EncodableVector.Add(this.extensions);
			}
			return AttributeCertificateInfo.GetInstance(new DerSequence(asn1EncodableVector));
		}

		// Token: 0x04000C64 RID: 3172
		internal DerInteger version;

		// Token: 0x04000C65 RID: 3173
		internal Holder holder;

		// Token: 0x04000C66 RID: 3174
		internal AttCertIssuer issuer;

		// Token: 0x04000C67 RID: 3175
		internal AlgorithmIdentifier signature;

		// Token: 0x04000C68 RID: 3176
		internal DerInteger serialNumber;

		// Token: 0x04000C69 RID: 3177
		internal Asn1EncodableVector attributes;

		// Token: 0x04000C6A RID: 3178
		internal DerBitString issuerUniqueID;

		// Token: 0x04000C6B RID: 3179
		internal X509Extensions extensions;

		// Token: 0x04000C6C RID: 3180
		internal DerGeneralizedTime startDate;

		// Token: 0x04000C6D RID: 3181
		internal DerGeneralizedTime endDate;
	}
}
