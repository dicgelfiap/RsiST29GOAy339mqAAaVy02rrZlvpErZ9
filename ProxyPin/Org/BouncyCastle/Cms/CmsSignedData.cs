using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509.Store;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020002F0 RID: 752
	public class CmsSignedData
	{
		// Token: 0x0600168E RID: 5774 RVA: 0x000753B8 File Offset: 0x000753B8
		private CmsSignedData(CmsSignedData c)
		{
			this.signedData = c.signedData;
			this.contentInfo = c.contentInfo;
			this.signedContent = c.signedContent;
			this.signerInfoStore = c.signerInfoStore;
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x000753F0 File Offset: 0x000753F0
		public CmsSignedData(byte[] sigBlock) : this(CmsUtilities.ReadContentInfo(new MemoryStream(sigBlock, false)))
		{
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x00075404 File Offset: 0x00075404
		public CmsSignedData(CmsProcessable signedContent, byte[] sigBlock) : this(signedContent, CmsUtilities.ReadContentInfo(new MemoryStream(sigBlock, false)))
		{
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x0007541C File Offset: 0x0007541C
		public CmsSignedData(IDictionary hashes, byte[] sigBlock) : this(hashes, CmsUtilities.ReadContentInfo(sigBlock))
		{
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x0007542C File Offset: 0x0007542C
		public CmsSignedData(CmsProcessable signedContent, Stream sigData) : this(signedContent, CmsUtilities.ReadContentInfo(sigData))
		{
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x0007543C File Offset: 0x0007543C
		public CmsSignedData(Stream sigData) : this(CmsUtilities.ReadContentInfo(sigData))
		{
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x0007544C File Offset: 0x0007544C
		public CmsSignedData(CmsProcessable signedContent, ContentInfo sigData)
		{
			this.signedContent = signedContent;
			this.contentInfo = sigData;
			this.signedData = SignedData.GetInstance(this.contentInfo.Content);
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x00075478 File Offset: 0x00075478
		public CmsSignedData(IDictionary hashes, ContentInfo sigData)
		{
			this.hashes = hashes;
			this.contentInfo = sigData;
			this.signedData = SignedData.GetInstance(this.contentInfo.Content);
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x000754A4 File Offset: 0x000754A4
		public CmsSignedData(ContentInfo sigData)
		{
			this.contentInfo = sigData;
			this.signedData = SignedData.GetInstance(this.contentInfo.Content);
			if (this.signedData.EncapContentInfo.Content != null)
			{
				this.signedContent = new CmsProcessableByteArray(((Asn1OctetString)this.signedData.EncapContentInfo.Content).GetOctets());
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06001697 RID: 5783 RVA: 0x00075514 File Offset: 0x00075514
		public int Version
		{
			get
			{
				return this.signedData.Version.IntValueExact;
			}
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x00075528 File Offset: 0x00075528
		public SignerInformationStore GetSignerInfos()
		{
			if (this.signerInfoStore == null)
			{
				IList list = Platform.CreateArrayList();
				Asn1Set signerInfos = this.signedData.SignerInfos;
				foreach (object obj in signerInfos)
				{
					SignerInfo instance = SignerInfo.GetInstance(obj);
					DerObjectIdentifier contentType = this.signedData.EncapContentInfo.ContentType;
					if (this.hashes == null)
					{
						list.Add(new SignerInformation(instance, contentType, this.signedContent, null));
					}
					else
					{
						byte[] digest = (byte[])this.hashes[instance.DigestAlgorithm.Algorithm.Id];
						list.Add(new SignerInformation(instance, contentType, null, new BaseDigestCalculator(digest)));
					}
				}
				this.signerInfoStore = new SignerInformationStore(list);
			}
			return this.signerInfoStore;
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x0007562C File Offset: 0x0007562C
		public IX509Store GetAttributeCertificates(string type)
		{
			if (this.attrCertStore == null)
			{
				this.attrCertStore = CmsSignedData.Helper.CreateAttributeStore(type, this.signedData.Certificates);
			}
			return this.attrCertStore;
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x0007565C File Offset: 0x0007565C
		public IX509Store GetCertificates(string type)
		{
			if (this.certificateStore == null)
			{
				this.certificateStore = CmsSignedData.Helper.CreateCertificateStore(type, this.signedData.Certificates);
			}
			return this.certificateStore;
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x0007568C File Offset: 0x0007568C
		public IX509Store GetCrls(string type)
		{
			if (this.crlStore == null)
			{
				this.crlStore = CmsSignedData.Helper.CreateCrlStore(type, this.signedData.CRLs);
			}
			return this.crlStore;
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x0600169C RID: 5788 RVA: 0x000756BC File Offset: 0x000756BC
		[Obsolete("Use 'SignedContentType' property instead.")]
		public string SignedContentTypeOid
		{
			get
			{
				return this.signedData.EncapContentInfo.ContentType.Id;
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x0600169D RID: 5789 RVA: 0x000756D4 File Offset: 0x000756D4
		public DerObjectIdentifier SignedContentType
		{
			get
			{
				return this.signedData.EncapContentInfo.ContentType;
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x0600169E RID: 5790 RVA: 0x000756E8 File Offset: 0x000756E8
		public CmsProcessable SignedContent
		{
			get
			{
				return this.signedContent;
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x0600169F RID: 5791 RVA: 0x000756F0 File Offset: 0x000756F0
		public ContentInfo ContentInfo
		{
			get
			{
				return this.contentInfo;
			}
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x000756F8 File Offset: 0x000756F8
		public byte[] GetEncoded()
		{
			return this.contentInfo.GetEncoded();
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x00075708 File Offset: 0x00075708
		public byte[] GetEncoded(string encoding)
		{
			return this.contentInfo.GetEncoded(encoding);
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x00075718 File Offset: 0x00075718
		public static CmsSignedData ReplaceSigners(CmsSignedData signedData, SignerInformationStore signerInformationStore)
		{
			CmsSignedData cmsSignedData = new CmsSignedData(signedData);
			cmsSignedData.signerInfoStore = signerInformationStore;
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			Asn1EncodableVector asn1EncodableVector2 = new Asn1EncodableVector();
			foreach (object obj in signerInformationStore.GetSigners())
			{
				SignerInformation signerInformation = (SignerInformation)obj;
				asn1EncodableVector.Add(CmsSignedData.Helper.FixAlgID(signerInformation.DigestAlgorithmID));
				asn1EncodableVector2.Add(signerInformation.ToSignerInfo());
			}
			Asn1Set asn1Set = new DerSet(asn1EncodableVector);
			Asn1Set element = new DerSet(asn1EncodableVector2);
			Asn1Sequence asn1Sequence = (Asn1Sequence)signedData.signedData.ToAsn1Object();
			asn1EncodableVector2 = new Asn1EncodableVector(new Asn1Encodable[]
			{
				asn1Sequence[0],
				asn1Set
			});
			for (int num = 2; num != asn1Sequence.Count - 1; num++)
			{
				asn1EncodableVector2.Add(asn1Sequence[num]);
			}
			asn1EncodableVector2.Add(element);
			cmsSignedData.signedData = SignedData.GetInstance(new BerSequence(asn1EncodableVector2));
			cmsSignedData.contentInfo = new ContentInfo(cmsSignedData.contentInfo.ContentType, cmsSignedData.signedData);
			return cmsSignedData;
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x00075864 File Offset: 0x00075864
		public static CmsSignedData ReplaceCertificatesAndCrls(CmsSignedData signedData, IX509Store x509Certs, IX509Store x509Crls, IX509Store x509AttrCerts)
		{
			if (x509AttrCerts != null)
			{
				throw Platform.CreateNotImplementedException("Currently can't replace attribute certificates");
			}
			CmsSignedData cmsSignedData = new CmsSignedData(signedData);
			Asn1Set certificates = null;
			try
			{
				Asn1Set asn1Set = CmsUtilities.CreateBerSetFromList(CmsUtilities.GetCertificatesFromStore(x509Certs));
				if (asn1Set.Count != 0)
				{
					certificates = asn1Set;
				}
			}
			catch (X509StoreException e)
			{
				throw new CmsException("error getting certificates from store", e);
			}
			Asn1Set crls = null;
			try
			{
				Asn1Set asn1Set2 = CmsUtilities.CreateBerSetFromList(CmsUtilities.GetCrlsFromStore(x509Crls));
				if (asn1Set2.Count != 0)
				{
					crls = asn1Set2;
				}
			}
			catch (X509StoreException e2)
			{
				throw new CmsException("error getting CRLs from store", e2);
			}
			SignedData signedData2 = signedData.signedData;
			cmsSignedData.signedData = new SignedData(signedData2.DigestAlgorithms, signedData2.EncapContentInfo, certificates, crls, signedData2.SignerInfos);
			cmsSignedData.contentInfo = new ContentInfo(cmsSignedData.contentInfo.ContentType, cmsSignedData.signedData);
			return cmsSignedData;
		}

		// Token: 0x04000F49 RID: 3913
		private static readonly CmsSignedHelper Helper = CmsSignedHelper.Instance;

		// Token: 0x04000F4A RID: 3914
		private readonly CmsProcessable signedContent;

		// Token: 0x04000F4B RID: 3915
		private SignedData signedData;

		// Token: 0x04000F4C RID: 3916
		private ContentInfo contentInfo;

		// Token: 0x04000F4D RID: 3917
		private SignerInformationStore signerInfoStore;

		// Token: 0x04000F4E RID: 3918
		private IX509Store attrCertStore;

		// Token: 0x04000F4F RID: 3919
		private IX509Store certificateStore;

		// Token: 0x04000F50 RID: 3920
		private IX509Store crlStore;

		// Token: 0x04000F51 RID: 3921
		private IDictionary hashes;
	}
}
