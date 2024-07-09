using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x0200021C RID: 540
	public class V3TbsCertificateGenerator
	{
		// Token: 0x0600116A RID: 4458 RVA: 0x00063040 File Offset: 0x00063040
		public void SetSerialNumber(DerInteger serialNumber)
		{
			this.serialNumber = serialNumber;
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x0006304C File Offset: 0x0006304C
		public void SetSignature(AlgorithmIdentifier signature)
		{
			this.signature = signature;
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x00063058 File Offset: 0x00063058
		public void SetIssuer(X509Name issuer)
		{
			this.issuer = issuer;
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x00063064 File Offset: 0x00063064
		public void SetStartDate(DerUtcTime startDate)
		{
			this.startDate = new Time(startDate);
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x00063074 File Offset: 0x00063074
		public void SetStartDate(Time startDate)
		{
			this.startDate = startDate;
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x00063080 File Offset: 0x00063080
		public void SetEndDate(DerUtcTime endDate)
		{
			this.endDate = new Time(endDate);
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x00063090 File Offset: 0x00063090
		public void SetEndDate(Time endDate)
		{
			this.endDate = endDate;
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x0006309C File Offset: 0x0006309C
		public void SetSubject(X509Name subject)
		{
			this.subject = subject;
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x000630A8 File Offset: 0x000630A8
		public void SetIssuerUniqueID(DerBitString uniqueID)
		{
			this.issuerUniqueID = uniqueID;
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x000630B4 File Offset: 0x000630B4
		public void SetSubjectUniqueID(DerBitString uniqueID)
		{
			this.subjectUniqueID = uniqueID;
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x000630C0 File Offset: 0x000630C0
		public void SetSubjectPublicKeyInfo(SubjectPublicKeyInfo pubKeyInfo)
		{
			this.subjectPublicKeyInfo = pubKeyInfo;
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x000630CC File Offset: 0x000630CC
		public void SetExtensions(X509Extensions extensions)
		{
			this.extensions = extensions;
			if (extensions != null)
			{
				X509Extension extension = extensions.GetExtension(X509Extensions.SubjectAlternativeName);
				if (extension != null && extension.IsCritical)
				{
					this.altNamePresentAndCritical = true;
				}
			}
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x00063110 File Offset: 0x00063110
		public TbsCertificateStructure GenerateTbsCertificate()
		{
			if (this.serialNumber == null || this.signature == null || this.issuer == null || this.startDate == null || this.endDate == null || (this.subject == null && !this.altNamePresentAndCritical) || this.subjectPublicKeyInfo == null)
			{
				throw new InvalidOperationException("not all mandatory fields set in V3 TBScertificate generator");
			}
			DerSequence derSequence = new DerSequence(new Asn1Encodable[]
			{
				this.startDate,
				this.endDate
			});
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version,
				this.serialNumber,
				this.signature,
				this.issuer,
				derSequence
			});
			if (this.subject != null)
			{
				asn1EncodableVector.Add(this.subject);
			}
			else
			{
				asn1EncodableVector.Add(DerSequence.Empty);
			}
			asn1EncodableVector.Add(this.subjectPublicKeyInfo);
			if (this.issuerUniqueID != null)
			{
				asn1EncodableVector.Add(new DerTaggedObject(false, 1, this.issuerUniqueID));
			}
			if (this.subjectUniqueID != null)
			{
				asn1EncodableVector.Add(new DerTaggedObject(false, 2, this.subjectUniqueID));
			}
			if (this.extensions != null)
			{
				asn1EncodableVector.Add(new DerTaggedObject(3, this.extensions));
			}
			return new TbsCertificateStructure(new DerSequence(asn1EncodableVector));
		}

		// Token: 0x04000C78 RID: 3192
		internal DerTaggedObject version = new DerTaggedObject(0, new DerInteger(2));

		// Token: 0x04000C79 RID: 3193
		internal DerInteger serialNumber;

		// Token: 0x04000C7A RID: 3194
		internal AlgorithmIdentifier signature;

		// Token: 0x04000C7B RID: 3195
		internal X509Name issuer;

		// Token: 0x04000C7C RID: 3196
		internal Time startDate;

		// Token: 0x04000C7D RID: 3197
		internal Time endDate;

		// Token: 0x04000C7E RID: 3198
		internal X509Name subject;

		// Token: 0x04000C7F RID: 3199
		internal SubjectPublicKeyInfo subjectPublicKeyInfo;

		// Token: 0x04000C80 RID: 3200
		internal X509Extensions extensions;

		// Token: 0x04000C81 RID: 3201
		private bool altNamePresentAndCritical;

		// Token: 0x04000C82 RID: 3202
		private DerBitString issuerUniqueID;

		// Token: 0x04000C83 RID: 3203
		private DerBitString subjectUniqueID;
	}
}
