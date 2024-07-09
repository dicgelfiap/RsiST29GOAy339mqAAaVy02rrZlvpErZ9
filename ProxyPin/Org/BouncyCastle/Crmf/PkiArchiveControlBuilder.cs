using System;
using System.IO;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.Crmf;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Cms;
using Org.BouncyCastle.Crypto;

namespace Org.BouncyCastle.Crmf
{
	// Token: 0x02000323 RID: 803
	public class PkiArchiveControlBuilder
	{
		// Token: 0x06001838 RID: 6200 RVA: 0x0007D6A8 File Offset: 0x0007D6A8
		public PkiArchiveControlBuilder(PrivateKeyInfo privateKeyInfo, GeneralName generalName)
		{
			EncKeyWithID encKeyWithID = new EncKeyWithID(privateKeyInfo, generalName);
			try
			{
				this.keyContent = new CmsProcessableByteArray(CrmfObjectIdentifiers.id_ct_encKeyWithID, encKeyWithID.GetEncoded());
			}
			catch (IOException innerException)
			{
				throw new InvalidOperationException("unable to encode key and general name info", innerException);
			}
			this.envGen = new CmsEnvelopedDataGenerator();
		}

		// Token: 0x06001839 RID: 6201 RVA: 0x0007D708 File Offset: 0x0007D708
		public PkiArchiveControlBuilder AddRecipientGenerator(RecipientInfoGenerator recipientGen)
		{
			this.envGen.AddRecipientInfoGenerator(recipientGen);
			return this;
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x0007D718 File Offset: 0x0007D718
		public PkiArchiveControl Build(ICipherBuilderWithKey contentEncryptor)
		{
			CmsEnvelopedData cmsEnvelopedData = this.envGen.Generate(this.keyContent, contentEncryptor);
			EnvelopedData instance = EnvelopedData.GetInstance(cmsEnvelopedData.ContentInfo.Content);
			return new PkiArchiveControl(new PkiArchiveOptions(new EncryptedKey(instance)));
		}

		// Token: 0x0400100E RID: 4110
		private CmsEnvelopedDataGenerator envGen;

		// Token: 0x0400100F RID: 4111
		private CmsProcessableByteArray keyContent;
	}
}
