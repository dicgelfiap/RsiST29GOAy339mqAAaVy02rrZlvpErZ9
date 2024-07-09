using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.IO;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;
using Org.BouncyCastle.Utilities.IO;
using Org.BouncyCastle.X509.Store;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020002F3 RID: 755
	public class CmsSignedDataParser : CmsContentInfoParser
	{
		// Token: 0x060016CB RID: 5835 RVA: 0x00076210 File Offset: 0x00076210
		public CmsSignedDataParser(byte[] sigBlock) : this(new MemoryStream(sigBlock, false))
		{
		}

		// Token: 0x060016CC RID: 5836 RVA: 0x00076220 File Offset: 0x00076220
		public CmsSignedDataParser(CmsTypedStream signedContent, byte[] sigBlock) : this(signedContent, new MemoryStream(sigBlock, false))
		{
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x00076230 File Offset: 0x00076230
		public CmsSignedDataParser(Stream sigData) : this(null, sigData)
		{
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x0007623C File Offset: 0x0007623C
		public CmsSignedDataParser(CmsTypedStream signedContent, Stream sigData) : base(sigData)
		{
			try
			{
				this._signedContent = signedContent;
				this._signedData = SignedDataParser.GetInstance(this.contentInfo.GetContent(16));
				this._digests = Platform.CreateHashtable();
				this._digestOids = new HashSet();
				Asn1SetParser digestAlgorithms = this._signedData.GetDigestAlgorithms();
				IAsn1Convertible asn1Convertible;
				while ((asn1Convertible = digestAlgorithms.ReadObject()) != null)
				{
					AlgorithmIdentifier instance = AlgorithmIdentifier.GetInstance(asn1Convertible.ToAsn1Object());
					try
					{
						string id = instance.Algorithm.Id;
						string digestAlgName = CmsSignedDataParser.Helper.GetDigestAlgName(id);
						if (!this._digests.Contains(digestAlgName))
						{
							this._digests[digestAlgName] = CmsSignedDataParser.Helper.GetDigestInstance(digestAlgName);
							this._digestOids.Add(id);
						}
					}
					catch (SecurityUtilityException)
					{
					}
				}
				ContentInfoParser encapContentInfo = this._signedData.GetEncapContentInfo();
				Asn1OctetStringParser asn1OctetStringParser = (Asn1OctetStringParser)encapContentInfo.GetContent(4);
				if (asn1OctetStringParser != null)
				{
					CmsTypedStream cmsTypedStream = new CmsTypedStream(encapContentInfo.ContentType.Id, asn1OctetStringParser.GetOctetStream());
					if (this._signedContent == null)
					{
						this._signedContent = cmsTypedStream;
					}
					else
					{
						cmsTypedStream.Drain();
					}
				}
				this._signedContentType = ((this._signedContent == null) ? encapContentInfo.ContentType : new DerObjectIdentifier(this._signedContent.ContentType));
			}
			catch (IOException ex)
			{
				throw new CmsException("io exception: " + ex.Message, ex);
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x060016CF RID: 5839 RVA: 0x000763E4 File Offset: 0x000763E4
		public int Version
		{
			get
			{
				return this._signedData.Version.IntValueExact;
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x060016D0 RID: 5840 RVA: 0x000763F8 File Offset: 0x000763F8
		public ISet DigestOids
		{
			get
			{
				return new HashSet(this._digestOids);
			}
		}

		// Token: 0x060016D1 RID: 5841 RVA: 0x00076408 File Offset: 0x00076408
		public SignerInformationStore GetSignerInfos()
		{
			if (this._signerInfoStore == null)
			{
				this.PopulateCertCrlSets();
				IList list = Platform.CreateArrayList();
				IDictionary dictionary = Platform.CreateHashtable();
				foreach (object key in this._digests.Keys)
				{
					dictionary[key] = DigestUtilities.DoFinal((IDigest)this._digests[key]);
				}
				try
				{
					Asn1SetParser signerInfos = this._signedData.GetSignerInfos();
					IAsn1Convertible asn1Convertible;
					while ((asn1Convertible = signerInfos.ReadObject()) != null)
					{
						SignerInfo instance = SignerInfo.GetInstance(asn1Convertible.ToAsn1Object());
						string digestAlgName = CmsSignedDataParser.Helper.GetDigestAlgName(instance.DigestAlgorithm.Algorithm.Id);
						byte[] digest = (byte[])dictionary[digestAlgName];
						list.Add(new SignerInformation(instance, this._signedContentType, null, new BaseDigestCalculator(digest)));
					}
				}
				catch (IOException ex)
				{
					throw new CmsException("io exception: " + ex.Message, ex);
				}
				this._signerInfoStore = new SignerInformationStore(list);
			}
			return this._signerInfoStore;
		}

		// Token: 0x060016D2 RID: 5842 RVA: 0x00076554 File Offset: 0x00076554
		public IX509Store GetAttributeCertificates(string type)
		{
			if (this._attributeStore == null)
			{
				this.PopulateCertCrlSets();
				this._attributeStore = CmsSignedDataParser.Helper.CreateAttributeStore(type, this._certSet);
			}
			return this._attributeStore;
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x00076584 File Offset: 0x00076584
		public IX509Store GetCertificates(string type)
		{
			if (this._certificateStore == null)
			{
				this.PopulateCertCrlSets();
				this._certificateStore = CmsSignedDataParser.Helper.CreateCertificateStore(type, this._certSet);
			}
			return this._certificateStore;
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x000765B4 File Offset: 0x000765B4
		public IX509Store GetCrls(string type)
		{
			if (this._crlStore == null)
			{
				this.PopulateCertCrlSets();
				this._crlStore = CmsSignedDataParser.Helper.CreateCrlStore(type, this._crlSet);
			}
			return this._crlStore;
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x000765E4 File Offset: 0x000765E4
		private void PopulateCertCrlSets()
		{
			if (this._isCertCrlParsed)
			{
				return;
			}
			this._isCertCrlParsed = true;
			try
			{
				this._certSet = CmsSignedDataParser.GetAsn1Set(this._signedData.GetCertificates());
				this._crlSet = CmsSignedDataParser.GetAsn1Set(this._signedData.GetCrls());
			}
			catch (IOException e)
			{
				throw new CmsException("problem parsing cert/crl sets", e);
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x060016D6 RID: 5846 RVA: 0x00076654 File Offset: 0x00076654
		public DerObjectIdentifier SignedContentType
		{
			get
			{
				return this._signedContentType;
			}
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x0007665C File Offset: 0x0007665C
		public CmsTypedStream GetSignedContent()
		{
			if (this._signedContent == null)
			{
				return null;
			}
			Stream stream = this._signedContent.ContentStream;
			foreach (object obj in this._digests.Values)
			{
				IDigest readDigest = (IDigest)obj;
				stream = new DigestStream(stream, readDigest, null);
			}
			return new CmsTypedStream(this._signedContent.ContentType, stream);
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x000766F0 File Offset: 0x000766F0
		public static Stream ReplaceSigners(Stream original, SignerInformationStore signerInformationStore, Stream outStr)
		{
			CmsSignedDataStreamGenerator cmsSignedDataStreamGenerator = new CmsSignedDataStreamGenerator();
			CmsSignedDataParser cmsSignedDataParser = new CmsSignedDataParser(original);
			cmsSignedDataStreamGenerator.AddSigners(signerInformationStore);
			CmsTypedStream signedContent = cmsSignedDataParser.GetSignedContent();
			bool flag = signedContent != null;
			Stream stream = cmsSignedDataStreamGenerator.Open(outStr, cmsSignedDataParser.SignedContentType.Id, flag);
			if (flag)
			{
				Streams.PipeAll(signedContent.ContentStream, stream);
			}
			cmsSignedDataStreamGenerator.AddAttributeCertificates(cmsSignedDataParser.GetAttributeCertificates("Collection"));
			cmsSignedDataStreamGenerator.AddCertificates(cmsSignedDataParser.GetCertificates("Collection"));
			cmsSignedDataStreamGenerator.AddCrls(cmsSignedDataParser.GetCrls("Collection"));
			Platform.Dispose(stream);
			return outStr;
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x00076788 File Offset: 0x00076788
		public static Stream ReplaceCertificatesAndCrls(Stream original, IX509Store x509Certs, IX509Store x509Crls, IX509Store x509AttrCerts, Stream outStr)
		{
			CmsSignedDataStreamGenerator cmsSignedDataStreamGenerator = new CmsSignedDataStreamGenerator();
			CmsSignedDataParser cmsSignedDataParser = new CmsSignedDataParser(original);
			cmsSignedDataStreamGenerator.AddDigests(cmsSignedDataParser.DigestOids);
			CmsTypedStream signedContent = cmsSignedDataParser.GetSignedContent();
			bool flag = signedContent != null;
			Stream stream = cmsSignedDataStreamGenerator.Open(outStr, cmsSignedDataParser.SignedContentType.Id, flag);
			if (flag)
			{
				Streams.PipeAll(signedContent.ContentStream, stream);
			}
			if (x509AttrCerts != null)
			{
				cmsSignedDataStreamGenerator.AddAttributeCertificates(x509AttrCerts);
			}
			if (x509Certs != null)
			{
				cmsSignedDataStreamGenerator.AddCertificates(x509Certs);
			}
			if (x509Crls != null)
			{
				cmsSignedDataStreamGenerator.AddCrls(x509Crls);
			}
			cmsSignedDataStreamGenerator.AddSigners(cmsSignedDataParser.GetSignerInfos());
			Platform.Dispose(stream);
			return outStr;
		}

		// Token: 0x060016DA RID: 5850 RVA: 0x00076828 File Offset: 0x00076828
		private static Asn1Set GetAsn1Set(Asn1SetParser asn1SetParser)
		{
			if (asn1SetParser != null)
			{
				return Asn1Set.GetInstance(asn1SetParser.ToAsn1Object());
			}
			return null;
		}

		// Token: 0x04000F6C RID: 3948
		private static readonly CmsSignedHelper Helper = CmsSignedHelper.Instance;

		// Token: 0x04000F6D RID: 3949
		private SignedDataParser _signedData;

		// Token: 0x04000F6E RID: 3950
		private DerObjectIdentifier _signedContentType;

		// Token: 0x04000F6F RID: 3951
		private CmsTypedStream _signedContent;

		// Token: 0x04000F70 RID: 3952
		private IDictionary _digests;

		// Token: 0x04000F71 RID: 3953
		private ISet _digestOids;

		// Token: 0x04000F72 RID: 3954
		private SignerInformationStore _signerInfoStore;

		// Token: 0x04000F73 RID: 3955
		private Asn1Set _certSet;

		// Token: 0x04000F74 RID: 3956
		private Asn1Set _crlSet;

		// Token: 0x04000F75 RID: 3957
		private bool _isCertCrlParsed;

		// Token: 0x04000F76 RID: 3958
		private IX509Store _attributeStore;

		// Token: 0x04000F77 RID: 3959
		private IX509Store _certificateStore;

		// Token: 0x04000F78 RID: 3960
		private IX509Store _crlStore;
	}
}
