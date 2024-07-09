using System;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020002D6 RID: 726
	public class CmsAuthenticatedData
	{
		// Token: 0x060015FB RID: 5627 RVA: 0x0007317C File Offset: 0x0007317C
		public CmsAuthenticatedData(byte[] authData) : this(CmsUtilities.ReadContentInfo(authData))
		{
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x0007318C File Offset: 0x0007318C
		public CmsAuthenticatedData(Stream authData) : this(CmsUtilities.ReadContentInfo(authData))
		{
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x0007319C File Offset: 0x0007319C
		public CmsAuthenticatedData(ContentInfo contentInfo)
		{
			this.contentInfo = contentInfo;
			AuthenticatedData instance = AuthenticatedData.GetInstance(contentInfo.Content);
			Asn1Set recipientInfos = instance.RecipientInfos;
			this.macAlg = instance.MacAlgorithm;
			ContentInfo encapsulatedContentInfo = instance.EncapsulatedContentInfo;
			CmsReadable readable = new CmsProcessableByteArray(Asn1OctetString.GetInstance(encapsulatedContentInfo.Content).GetOctets());
			CmsSecureReadable secureReadable = new CmsEnvelopedHelper.CmsAuthenticatedSecureReadable(this.macAlg, readable);
			this.recipientInfoStore = CmsEnvelopedHelper.BuildRecipientInformationStore(recipientInfos, secureReadable);
			this.authAttrs = instance.AuthAttrs;
			this.mac = instance.Mac.GetOctets();
			this.unauthAttrs = instance.UnauthAttrs;
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x0007323C File Offset: 0x0007323C
		public byte[] GetMac()
		{
			return Arrays.Clone(this.mac);
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x060015FF RID: 5631 RVA: 0x0007324C File Offset: 0x0007324C
		public AlgorithmIdentifier MacAlgorithmID
		{
			get
			{
				return this.macAlg;
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06001600 RID: 5632 RVA: 0x00073254 File Offset: 0x00073254
		public string MacAlgOid
		{
			get
			{
				return this.macAlg.Algorithm.Id;
			}
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x00073268 File Offset: 0x00073268
		public RecipientInformationStore GetRecipientInfos()
		{
			return this.recipientInfoStore;
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06001602 RID: 5634 RVA: 0x00073270 File Offset: 0x00073270
		public ContentInfo ContentInfo
		{
			get
			{
				return this.contentInfo;
			}
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x00073278 File Offset: 0x00073278
		public Org.BouncyCastle.Asn1.Cms.AttributeTable GetAuthAttrs()
		{
			if (this.authAttrs == null)
			{
				return null;
			}
			return new Org.BouncyCastle.Asn1.Cms.AttributeTable(this.authAttrs);
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x00073294 File Offset: 0x00073294
		public Org.BouncyCastle.Asn1.Cms.AttributeTable GetUnauthAttrs()
		{
			if (this.unauthAttrs == null)
			{
				return null;
			}
			return new Org.BouncyCastle.Asn1.Cms.AttributeTable(this.unauthAttrs);
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x000732B0 File Offset: 0x000732B0
		public byte[] GetEncoded()
		{
			return this.contentInfo.GetEncoded();
		}

		// Token: 0x04000EF3 RID: 3827
		internal RecipientInformationStore recipientInfoStore;

		// Token: 0x04000EF4 RID: 3828
		internal ContentInfo contentInfo;

		// Token: 0x04000EF5 RID: 3829
		private AlgorithmIdentifier macAlg;

		// Token: 0x04000EF6 RID: 3830
		private Asn1Set authAttrs;

		// Token: 0x04000EF7 RID: 3831
		private Asn1Set unauthAttrs;

		// Token: 0x04000EF8 RID: 3832
		private byte[] mac;
	}
}
