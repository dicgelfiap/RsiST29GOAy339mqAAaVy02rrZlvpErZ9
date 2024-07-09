using System;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020002DD RID: 733
	internal class CmsAuthEnvelopedData
	{
		// Token: 0x0600162E RID: 5678 RVA: 0x00073F38 File Offset: 0x00073F38
		public CmsAuthEnvelopedData(byte[] authEnvData) : this(CmsUtilities.ReadContentInfo(authEnvData))
		{
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x00073F48 File Offset: 0x00073F48
		public CmsAuthEnvelopedData(Stream authEnvData) : this(CmsUtilities.ReadContentInfo(authEnvData))
		{
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x00073F58 File Offset: 0x00073F58
		public CmsAuthEnvelopedData(ContentInfo contentInfo)
		{
			this.contentInfo = contentInfo;
			AuthEnvelopedData instance = AuthEnvelopedData.GetInstance(contentInfo.Content);
			this.originator = instance.OriginatorInfo;
			Asn1Set recipientInfos = instance.RecipientInfos;
			EncryptedContentInfo authEncryptedContentInfo = instance.AuthEncryptedContentInfo;
			this.authEncAlg = authEncryptedContentInfo.ContentEncryptionAlgorithm;
			CmsSecureReadable secureReadable = new CmsAuthEnvelopedData.AuthEnvelopedSecureReadable(this);
			this.recipientInfoStore = CmsEnvelopedHelper.BuildRecipientInformationStore(recipientInfos, secureReadable);
			this.authAttrs = instance.AuthAttrs;
			this.mac = instance.Mac.GetOctets();
			this.unauthAttrs = instance.UnauthAttrs;
		}

		// Token: 0x04000F1E RID: 3870
		internal RecipientInformationStore recipientInfoStore;

		// Token: 0x04000F1F RID: 3871
		internal ContentInfo contentInfo;

		// Token: 0x04000F20 RID: 3872
		private OriginatorInfo originator;

		// Token: 0x04000F21 RID: 3873
		private AlgorithmIdentifier authEncAlg;

		// Token: 0x04000F22 RID: 3874
		private Asn1Set authAttrs;

		// Token: 0x04000F23 RID: 3875
		private byte[] mac;

		// Token: 0x04000F24 RID: 3876
		private Asn1Set unauthAttrs;

		// Token: 0x02000DDA RID: 3546
		private class AuthEnvelopedSecureReadable : CmsSecureReadable
		{
			// Token: 0x06008B6C RID: 35692 RVA: 0x0029D9CC File Offset: 0x0029D9CC
			internal AuthEnvelopedSecureReadable(CmsAuthEnvelopedData parent)
			{
				this.parent = parent;
			}

			// Token: 0x17001D67 RID: 7527
			// (get) Token: 0x06008B6D RID: 35693 RVA: 0x0029D9DC File Offset: 0x0029D9DC
			public AlgorithmIdentifier Algorithm
			{
				get
				{
					return this.parent.authEncAlg;
				}
			}

			// Token: 0x17001D68 RID: 7528
			// (get) Token: 0x06008B6E RID: 35694 RVA: 0x0029D9EC File Offset: 0x0029D9EC
			public object CryptoObject
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06008B6F RID: 35695 RVA: 0x0029D9F0 File Offset: 0x0029D9F0
			public CmsReadable GetReadable(KeyParameter key)
			{
				throw new CmsException("AuthEnveloped data decryption not yet implemented");
			}

			// Token: 0x04004070 RID: 16496
			private readonly CmsAuthEnvelopedData parent;
		}
	}
}
