using System;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020002E4 RID: 740
	public class CmsEnvelopedData
	{
		// Token: 0x06001646 RID: 5702 RVA: 0x0007437C File Offset: 0x0007437C
		public CmsEnvelopedData(byte[] envelopedData) : this(CmsUtilities.ReadContentInfo(envelopedData))
		{
		}

		// Token: 0x06001647 RID: 5703 RVA: 0x0007438C File Offset: 0x0007438C
		public CmsEnvelopedData(Stream envelopedData) : this(CmsUtilities.ReadContentInfo(envelopedData))
		{
		}

		// Token: 0x06001648 RID: 5704 RVA: 0x0007439C File Offset: 0x0007439C
		public CmsEnvelopedData(ContentInfo contentInfo)
		{
			this.contentInfo = contentInfo;
			EnvelopedData instance = EnvelopedData.GetInstance(contentInfo.Content);
			Asn1Set recipientInfos = instance.RecipientInfos;
			EncryptedContentInfo encryptedContentInfo = instance.EncryptedContentInfo;
			this.encAlg = encryptedContentInfo.ContentEncryptionAlgorithm;
			CmsReadable readable = new CmsProcessableByteArray(encryptedContentInfo.EncryptedContent.GetOctets());
			CmsSecureReadable secureReadable = new CmsEnvelopedHelper.CmsEnvelopedSecureReadable(this.encAlg, readable);
			this.recipientInfoStore = CmsEnvelopedHelper.BuildRecipientInformationStore(recipientInfos, secureReadable);
			this.unprotectedAttributes = instance.UnprotectedAttrs;
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001649 RID: 5705 RVA: 0x0007441C File Offset: 0x0007441C
		public AlgorithmIdentifier EncryptionAlgorithmID
		{
			get
			{
				return this.encAlg;
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x0600164A RID: 5706 RVA: 0x00074424 File Offset: 0x00074424
		public string EncryptionAlgOid
		{
			get
			{
				return this.encAlg.Algorithm.Id;
			}
		}

		// Token: 0x0600164B RID: 5707 RVA: 0x00074438 File Offset: 0x00074438
		public RecipientInformationStore GetRecipientInfos()
		{
			return this.recipientInfoStore;
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x0600164C RID: 5708 RVA: 0x00074440 File Offset: 0x00074440
		public ContentInfo ContentInfo
		{
			get
			{
				return this.contentInfo;
			}
		}

		// Token: 0x0600164D RID: 5709 RVA: 0x00074448 File Offset: 0x00074448
		public Org.BouncyCastle.Asn1.Cms.AttributeTable GetUnprotectedAttributes()
		{
			if (this.unprotectedAttributes == null)
			{
				return null;
			}
			return new Org.BouncyCastle.Asn1.Cms.AttributeTable(this.unprotectedAttributes);
		}

		// Token: 0x0600164E RID: 5710 RVA: 0x00074464 File Offset: 0x00074464
		public byte[] GetEncoded()
		{
			return this.contentInfo.GetEncoded();
		}

		// Token: 0x04000F2F RID: 3887
		internal RecipientInformationStore recipientInfoStore;

		// Token: 0x04000F30 RID: 3888
		internal ContentInfo contentInfo;

		// Token: 0x04000F31 RID: 3889
		private AlgorithmIdentifier encAlg;

		// Token: 0x04000F32 RID: 3890
		private Asn1Set unprotectedAttributes;
	}
}
