using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x02000315 RID: 789
	public class SignerInfoGeneratorBuilder
	{
		// Token: 0x060017CB RID: 6091 RVA: 0x0007BA00 File Offset: 0x0007BA00
		public SignerInfoGeneratorBuilder SetDirectSignature(bool hasNoSignedAttributes)
		{
			this.directSignature = hasNoSignedAttributes;
			return this;
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x0007BA0C File Offset: 0x0007BA0C
		public SignerInfoGeneratorBuilder WithSignedAttributeGenerator(CmsAttributeTableGenerator signedGen)
		{
			this.signedGen = signedGen;
			return this;
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x0007BA18 File Offset: 0x0007BA18
		public SignerInfoGeneratorBuilder WithUnsignedAttributeGenerator(CmsAttributeTableGenerator unsignedGen)
		{
			this.unsignedGen = unsignedGen;
			return this;
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x0007BA24 File Offset: 0x0007BA24
		public SignerInfoGenerator Build(ISignatureFactory contentSigner, X509Certificate certificate)
		{
			SignerIdentifier sigId = new SignerIdentifier(new IssuerAndSerialNumber(certificate.IssuerDN, new DerInteger(certificate.SerialNumber)));
			SignerInfoGenerator signerInfoGenerator = this.CreateGenerator(contentSigner, sigId);
			signerInfoGenerator.setAssociatedCertificate(certificate);
			return signerInfoGenerator;
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x0007BA64 File Offset: 0x0007BA64
		public SignerInfoGenerator Build(ISignatureFactory signerFactory, byte[] subjectKeyIdentifier)
		{
			SignerIdentifier sigId = new SignerIdentifier(new DerOctetString(subjectKeyIdentifier));
			return this.CreateGenerator(signerFactory, sigId);
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x0007BA8C File Offset: 0x0007BA8C
		private SignerInfoGenerator CreateGenerator(ISignatureFactory contentSigner, SignerIdentifier sigId)
		{
			if (this.directSignature)
			{
				return new SignerInfoGenerator(sigId, contentSigner, true);
			}
			if (this.signedGen != null || this.unsignedGen != null)
			{
				if (this.signedGen == null)
				{
					this.signedGen = new DefaultSignedAttributeTableGenerator();
				}
				return new SignerInfoGenerator(sigId, contentSigner, this.signedGen, this.unsignedGen);
			}
			return new SignerInfoGenerator(sigId, contentSigner);
		}

		// Token: 0x04000FDD RID: 4061
		private bool directSignature;

		// Token: 0x04000FDE RID: 4062
		private CmsAttributeTableGenerator signedGen;

		// Token: 0x04000FDF RID: 4063
		private CmsAttributeTableGenerator unsignedGen;
	}
}
