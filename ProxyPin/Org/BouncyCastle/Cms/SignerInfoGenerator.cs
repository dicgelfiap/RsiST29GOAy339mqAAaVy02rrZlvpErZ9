using System;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x02000314 RID: 788
	public class SignerInfoGenerator
	{
		// Token: 0x060017C6 RID: 6086 RVA: 0x0007B95C File Offset: 0x0007B95C
		internal SignerInfoGenerator(SignerIdentifier sigId, ISignatureFactory signerFactory) : this(sigId, signerFactory, false)
		{
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x0007B968 File Offset: 0x0007B968
		internal SignerInfoGenerator(SignerIdentifier sigId, ISignatureFactory signerFactory, bool isDirectSignature)
		{
			this.sigId = sigId;
			this.contentSigner = signerFactory;
			this.isDirectSignature = isDirectSignature;
			if (this.isDirectSignature)
			{
				this.signedGen = null;
				this.unsignedGen = null;
				return;
			}
			this.signedGen = new DefaultSignedAttributeTableGenerator();
			this.unsignedGen = null;
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x0007B9C0 File Offset: 0x0007B9C0
		internal SignerInfoGenerator(SignerIdentifier sigId, ISignatureFactory contentSigner, CmsAttributeTableGenerator signedGen, CmsAttributeTableGenerator unsignedGen)
		{
			this.sigId = sigId;
			this.contentSigner = contentSigner;
			this.signedGen = signedGen;
			this.unsignedGen = unsignedGen;
			this.isDirectSignature = false;
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x0007B9EC File Offset: 0x0007B9EC
		internal void setAssociatedCertificate(X509Certificate certificate)
		{
			this.certificate = certificate;
		}

		// Token: 0x04000FD7 RID: 4055
		internal X509Certificate certificate;

		// Token: 0x04000FD8 RID: 4056
		internal ISignatureFactory contentSigner;

		// Token: 0x04000FD9 RID: 4057
		internal SignerIdentifier sigId;

		// Token: 0x04000FDA RID: 4058
		internal CmsAttributeTableGenerator signedGen;

		// Token: 0x04000FDB RID: 4059
		internal CmsAttributeTableGenerator unsignedGen;

		// Token: 0x04000FDC RID: 4060
		private bool isDirectSignature;
	}
}
