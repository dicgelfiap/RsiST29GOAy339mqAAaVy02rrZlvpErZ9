using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020001E5 RID: 485
	public class AttributeCertificate : Asn1Encodable
	{
		// Token: 0x06000F8D RID: 3981 RVA: 0x0005CBAC File Offset: 0x0005CBAC
		public static AttributeCertificate GetInstance(object obj)
		{
			if (obj is AttributeCertificate)
			{
				return (AttributeCertificate)obj;
			}
			if (obj != null)
			{
				return new AttributeCertificate(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x0005CBD4 File Offset: 0x0005CBD4
		public AttributeCertificate(AttributeCertificateInfo acinfo, AlgorithmIdentifier signatureAlgorithm, DerBitString signatureValue)
		{
			this.acinfo = acinfo;
			this.signatureAlgorithm = signatureAlgorithm;
			this.signatureValue = signatureValue;
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x0005CBF4 File Offset: 0x0005CBF4
		private AttributeCertificate(Asn1Sequence seq)
		{
			if (seq.Count != 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			this.acinfo = AttributeCertificateInfo.GetInstance(seq[0]);
			this.signatureAlgorithm = AlgorithmIdentifier.GetInstance(seq[1]);
			this.signatureValue = DerBitString.GetInstance(seq[2]);
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000F90 RID: 3984 RVA: 0x0005CC68 File Offset: 0x0005CC68
		public AttributeCertificateInfo ACInfo
		{
			get
			{
				return this.acinfo;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000F91 RID: 3985 RVA: 0x0005CC70 File Offset: 0x0005CC70
		public AlgorithmIdentifier SignatureAlgorithm
		{
			get
			{
				return this.signatureAlgorithm;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000F92 RID: 3986 RVA: 0x0005CC78 File Offset: 0x0005CC78
		public DerBitString SignatureValue
		{
			get
			{
				return this.signatureValue;
			}
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x0005CC80 File Offset: 0x0005CC80
		public byte[] GetSignatureOctets()
		{
			return this.signatureValue.GetOctets();
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x0005CC90 File Offset: 0x0005CC90
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.acinfo,
				this.signatureAlgorithm,
				this.signatureValue
			});
		}

		// Token: 0x04000BA0 RID: 2976
		private readonly AttributeCertificateInfo acinfo;

		// Token: 0x04000BA1 RID: 2977
		private readonly AlgorithmIdentifier signatureAlgorithm;

		// Token: 0x04000BA2 RID: 2978
		private readonly DerBitString signatureValue;
	}
}
