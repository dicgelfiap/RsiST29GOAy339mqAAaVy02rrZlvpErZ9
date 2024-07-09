using System;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020002DB RID: 731
	public class CmsAuthenticatedDataParser : CmsContentInfoParser
	{
		// Token: 0x0600161D RID: 5661 RVA: 0x000739CC File Offset: 0x000739CC
		public CmsAuthenticatedDataParser(byte[] envelopedData) : this(new MemoryStream(envelopedData, false))
		{
		}

		// Token: 0x0600161E RID: 5662 RVA: 0x000739DC File Offset: 0x000739DC
		public CmsAuthenticatedDataParser(Stream envelopedData) : base(envelopedData)
		{
			this.authAttrNotRead = true;
			this.authData = new AuthenticatedDataParser((Asn1SequenceParser)this.contentInfo.GetContent(16));
			Asn1Set instance = Asn1Set.GetInstance(this.authData.GetRecipientInfos().ToAsn1Object());
			this.macAlg = this.authData.GetMacAlgorithm();
			ContentInfoParser enapsulatedContentInfo = this.authData.GetEnapsulatedContentInfo();
			CmsReadable readable = new CmsProcessableInputStream(((Asn1OctetStringParser)enapsulatedContentInfo.GetContent(4)).GetOctetStream());
			CmsSecureReadable secureReadable = new CmsEnvelopedHelper.CmsAuthenticatedSecureReadable(this.macAlg, readable);
			this._recipientInfoStore = CmsEnvelopedHelper.BuildRecipientInformationStore(instance, secureReadable);
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x0600161F RID: 5663 RVA: 0x00073A7C File Offset: 0x00073A7C
		public AlgorithmIdentifier MacAlgorithmID
		{
			get
			{
				return this.macAlg;
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06001620 RID: 5664 RVA: 0x00073A84 File Offset: 0x00073A84
		public string MacAlgOid
		{
			get
			{
				return this.macAlg.Algorithm.Id;
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06001621 RID: 5665 RVA: 0x00073A98 File Offset: 0x00073A98
		public Asn1Object MacAlgParams
		{
			get
			{
				Asn1Encodable parameters = this.macAlg.Parameters;
				if (parameters != null)
				{
					return parameters.ToAsn1Object();
				}
				return null;
			}
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x00073AC4 File Offset: 0x00073AC4
		public RecipientInformationStore GetRecipientInfos()
		{
			return this._recipientInfoStore;
		}

		// Token: 0x06001623 RID: 5667 RVA: 0x00073ACC File Offset: 0x00073ACC
		public byte[] GetMac()
		{
			if (this.mac == null)
			{
				this.GetAuthAttrs();
				this.mac = this.authData.GetMac().GetOctets();
			}
			return Arrays.Clone(this.mac);
		}

		// Token: 0x06001624 RID: 5668 RVA: 0x00073B04 File Offset: 0x00073B04
		public Org.BouncyCastle.Asn1.Cms.AttributeTable GetAuthAttrs()
		{
			if (this.authAttrs == null && this.authAttrNotRead)
			{
				Asn1SetParser asn1SetParser = this.authData.GetAuthAttrs();
				this.authAttrNotRead = false;
				if (asn1SetParser != null)
				{
					Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
					IAsn1Convertible asn1Convertible;
					while ((asn1Convertible = asn1SetParser.ReadObject()) != null)
					{
						Asn1SequenceParser asn1SequenceParser = (Asn1SequenceParser)asn1Convertible;
						asn1EncodableVector.Add(asn1SequenceParser.ToAsn1Object());
					}
					this.authAttrs = new Org.BouncyCastle.Asn1.Cms.AttributeTable(new DerSet(asn1EncodableVector));
				}
			}
			return this.authAttrs;
		}

		// Token: 0x06001625 RID: 5669 RVA: 0x00073B84 File Offset: 0x00073B84
		public Org.BouncyCastle.Asn1.Cms.AttributeTable GetUnauthAttrs()
		{
			if (this.unauthAttrs == null && this.unauthAttrNotRead)
			{
				Asn1SetParser asn1SetParser = this.authData.GetUnauthAttrs();
				this.unauthAttrNotRead = false;
				if (asn1SetParser != null)
				{
					Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
					IAsn1Convertible asn1Convertible;
					while ((asn1Convertible = asn1SetParser.ReadObject()) != null)
					{
						Asn1SequenceParser asn1SequenceParser = (Asn1SequenceParser)asn1Convertible;
						asn1EncodableVector.Add(asn1SequenceParser.ToAsn1Object());
					}
					this.unauthAttrs = new Org.BouncyCastle.Asn1.Cms.AttributeTable(new DerSet(asn1EncodableVector));
				}
			}
			return this.unauthAttrs;
		}

		// Token: 0x04000F14 RID: 3860
		internal RecipientInformationStore _recipientInfoStore;

		// Token: 0x04000F15 RID: 3861
		internal AuthenticatedDataParser authData;

		// Token: 0x04000F16 RID: 3862
		private AlgorithmIdentifier macAlg;

		// Token: 0x04000F17 RID: 3863
		private byte[] mac;

		// Token: 0x04000F18 RID: 3864
		private Org.BouncyCastle.Asn1.Cms.AttributeTable authAttrs;

		// Token: 0x04000F19 RID: 3865
		private Org.BouncyCastle.Asn1.Cms.AttributeTable unauthAttrs;

		// Token: 0x04000F1A RID: 3866
		private bool authAttrNotRead;

		// Token: 0x04000F1B RID: 3867
		private bool unauthAttrNotRead;
	}
}
