using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Cms;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Operators
{
	// Token: 0x0200041F RID: 1055
	public class CmsKeyTransRecipientInfoGenerator : KeyTransRecipientInfoGenerator
	{
		// Token: 0x06002193 RID: 8595 RVA: 0x000C2C50 File Offset: 0x000C2C50
		public CmsKeyTransRecipientInfoGenerator(X509Certificate recipCert, IKeyWrapper keyWrapper) : base(new IssuerAndSerialNumber(recipCert.IssuerDN, new DerInteger(recipCert.SerialNumber)))
		{
			this.keyWrapper = keyWrapper;
			base.RecipientCert = recipCert;
			base.RecipientPublicKey = recipCert.GetPublicKey();
		}

		// Token: 0x06002194 RID: 8596 RVA: 0x000C2C98 File Offset: 0x000C2C98
		public CmsKeyTransRecipientInfoGenerator(byte[] subjectKeyID, IKeyWrapper keyWrapper) : base(subjectKeyID)
		{
			this.keyWrapper = keyWrapper;
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06002195 RID: 8597 RVA: 0x000C2CA8 File Offset: 0x000C2CA8
		protected override AlgorithmIdentifier AlgorithmDetails
		{
			get
			{
				return (AlgorithmIdentifier)this.keyWrapper.AlgorithmDetails;
			}
		}

		// Token: 0x06002196 RID: 8598 RVA: 0x000C2CBC File Offset: 0x000C2CBC
		protected override byte[] GenerateWrappedKey(KeyParameter contentKey)
		{
			return this.keyWrapper.Wrap(contentKey.GetKey()).Collect();
		}

		// Token: 0x040015C9 RID: 5577
		private readonly IKeyWrapper keyWrapper;
	}
}
