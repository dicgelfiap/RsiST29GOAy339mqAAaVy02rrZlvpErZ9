using System;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020002E6 RID: 742
	public class CmsEnvelopedDataParser : CmsContentInfoParser
	{
		// Token: 0x06001655 RID: 5717 RVA: 0x000748CC File Offset: 0x000748CC
		public CmsEnvelopedDataParser(byte[] envelopedData) : this(new MemoryStream(envelopedData, false))
		{
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x000748DC File Offset: 0x000748DC
		public CmsEnvelopedDataParser(Stream envelopedData) : base(envelopedData)
		{
			this._attrNotRead = true;
			this.envelopedData = new EnvelopedDataParser((Asn1SequenceParser)this.contentInfo.GetContent(16));
			Asn1Set instance = Asn1Set.GetInstance(this.envelopedData.GetRecipientInfos().ToAsn1Object());
			EncryptedContentInfoParser encryptedContentInfo = this.envelopedData.GetEncryptedContentInfo();
			this._encAlg = encryptedContentInfo.ContentEncryptionAlgorithm;
			CmsReadable readable = new CmsProcessableInputStream(((Asn1OctetStringParser)encryptedContentInfo.GetEncryptedContent(4)).GetOctetStream());
			CmsSecureReadable secureReadable = new CmsEnvelopedHelper.CmsEnvelopedSecureReadable(this._encAlg, readable);
			this.recipientInfoStore = CmsEnvelopedHelper.BuildRecipientInformationStore(instance, secureReadable);
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001657 RID: 5719 RVA: 0x00074978 File Offset: 0x00074978
		public AlgorithmIdentifier EncryptionAlgorithmID
		{
			get
			{
				return this._encAlg;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001658 RID: 5720 RVA: 0x00074980 File Offset: 0x00074980
		public string EncryptionAlgOid
		{
			get
			{
				return this._encAlg.Algorithm.Id;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06001659 RID: 5721 RVA: 0x00074994 File Offset: 0x00074994
		public Asn1Object EncryptionAlgParams
		{
			get
			{
				Asn1Encodable parameters = this._encAlg.Parameters;
				if (parameters != null)
				{
					return parameters.ToAsn1Object();
				}
				return null;
			}
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x000749C0 File Offset: 0x000749C0
		public RecipientInformationStore GetRecipientInfos()
		{
			return this.recipientInfoStore;
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x000749C8 File Offset: 0x000749C8
		public Org.BouncyCastle.Asn1.Cms.AttributeTable GetUnprotectedAttributes()
		{
			if (this._unprotectedAttributes == null && this._attrNotRead)
			{
				Asn1SetParser unprotectedAttrs = this.envelopedData.GetUnprotectedAttrs();
				this._attrNotRead = false;
				if (unprotectedAttrs != null)
				{
					Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
					IAsn1Convertible asn1Convertible;
					while ((asn1Convertible = unprotectedAttrs.ReadObject()) != null)
					{
						Asn1SequenceParser asn1SequenceParser = (Asn1SequenceParser)asn1Convertible;
						asn1EncodableVector.Add(asn1SequenceParser.ToAsn1Object());
					}
					this._unprotectedAttributes = new Org.BouncyCastle.Asn1.Cms.AttributeTable(new DerSet(asn1EncodableVector));
				}
			}
			return this._unprotectedAttributes;
		}

		// Token: 0x04000F33 RID: 3891
		internal RecipientInformationStore recipientInfoStore;

		// Token: 0x04000F34 RID: 3892
		internal EnvelopedDataParser envelopedData;

		// Token: 0x04000F35 RID: 3893
		private AlgorithmIdentifier _encAlg;

		// Token: 0x04000F36 RID: 3894
		private Org.BouncyCastle.Asn1.Cms.AttributeTable _unprotectedAttributes;

		// Token: 0x04000F37 RID: 3895
		private bool _attrNotRead;
	}
}
