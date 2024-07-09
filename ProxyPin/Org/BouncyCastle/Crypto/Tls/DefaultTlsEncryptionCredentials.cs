using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004EB RID: 1259
	public class DefaultTlsEncryptionCredentials : AbstractTlsEncryptionCredentials
	{
		// Token: 0x060026A9 RID: 9897 RVA: 0x000D15F4 File Offset: 0x000D15F4
		public DefaultTlsEncryptionCredentials(TlsContext context, Certificate certificate, AsymmetricKeyParameter privateKey)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			if (certificate.IsEmpty)
			{
				throw new ArgumentException("cannot be empty", "certificate");
			}
			if (privateKey == null)
			{
				throw new ArgumentNullException("'privateKey' cannot be null");
			}
			if (!privateKey.IsPrivate)
			{
				throw new ArgumentException("must be private", "privateKey");
			}
			if (!(privateKey is RsaKeyParameters))
			{
				throw new ArgumentException("type not supported: " + Platform.GetTypeName(privateKey), "privateKey");
			}
			this.mContext = context;
			this.mCertificate = certificate;
			this.mPrivateKey = privateKey;
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x060026AA RID: 9898 RVA: 0x000D16A0 File Offset: 0x000D16A0
		public override Certificate Certificate
		{
			get
			{
				return this.mCertificate;
			}
		}

		// Token: 0x060026AB RID: 9899 RVA: 0x000D16A8 File Offset: 0x000D16A8
		public override byte[] DecryptPreMasterSecret(byte[] encryptedPreMasterSecret)
		{
			return TlsRsaUtilities.SafeDecryptPreMasterSecret(this.mContext, (RsaKeyParameters)this.mPrivateKey, encryptedPreMasterSecret);
		}

		// Token: 0x04001917 RID: 6423
		protected readonly TlsContext mContext;

		// Token: 0x04001918 RID: 6424
		protected readonly Certificate mCertificate;

		// Token: 0x04001919 RID: 6425
		protected readonly AsymmetricKeyParameter mPrivateKey;
	}
}
