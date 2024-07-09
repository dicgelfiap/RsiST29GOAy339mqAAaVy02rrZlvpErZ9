using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000218 RID: 536
	public class V1TbsCertificateGenerator
	{
		// Token: 0x0600113A RID: 4410 RVA: 0x0006282C File Offset: 0x0006282C
		public void SetSerialNumber(DerInteger serialNumber)
		{
			this.serialNumber = serialNumber;
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x00062838 File Offset: 0x00062838
		public void SetSignature(AlgorithmIdentifier signature)
		{
			this.signature = signature;
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x00062844 File Offset: 0x00062844
		public void SetIssuer(X509Name issuer)
		{
			this.issuer = issuer;
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x00062850 File Offset: 0x00062850
		public void SetStartDate(Time startDate)
		{
			this.startDate = startDate;
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x0006285C File Offset: 0x0006285C
		public void SetStartDate(DerUtcTime startDate)
		{
			this.startDate = new Time(startDate);
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x0006286C File Offset: 0x0006286C
		public void SetEndDate(Time endDate)
		{
			this.endDate = endDate;
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x00062878 File Offset: 0x00062878
		public void SetEndDate(DerUtcTime endDate)
		{
			this.endDate = new Time(endDate);
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x00062888 File Offset: 0x00062888
		public void SetSubject(X509Name subject)
		{
			this.subject = subject;
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x00062894 File Offset: 0x00062894
		public void SetSubjectPublicKeyInfo(SubjectPublicKeyInfo pubKeyInfo)
		{
			this.subjectPublicKeyInfo = pubKeyInfo;
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x000628A0 File Offset: 0x000628A0
		public TbsCertificateStructure GenerateTbsCertificate()
		{
			if (this.serialNumber == null || this.signature == null || this.issuer == null || this.startDate == null || this.endDate == null || this.subject == null || this.subjectPublicKeyInfo == null)
			{
				throw new InvalidOperationException("not all mandatory fields set in V1 TBScertificate generator");
			}
			return new TbsCertificateStructure(new DerSequence(new Asn1Encodable[]
			{
				this.serialNumber,
				this.signature,
				this.issuer,
				new DerSequence(new Asn1Encodable[]
				{
					this.startDate,
					this.endDate
				}),
				this.subject,
				this.subjectPublicKeyInfo
			}));
		}

		// Token: 0x04000C5C RID: 3164
		internal DerTaggedObject version = new DerTaggedObject(0, new DerInteger(0));

		// Token: 0x04000C5D RID: 3165
		internal DerInteger serialNumber;

		// Token: 0x04000C5E RID: 3166
		internal AlgorithmIdentifier signature;

		// Token: 0x04000C5F RID: 3167
		internal X509Name issuer;

		// Token: 0x04000C60 RID: 3168
		internal Time startDate;

		// Token: 0x04000C61 RID: 3169
		internal Time endDate;

		// Token: 0x04000C62 RID: 3170
		internal X509Name subject;

		// Token: 0x04000C63 RID: 3171
		internal SubjectPublicKeyInfo subjectPublicKeyInfo;
	}
}
