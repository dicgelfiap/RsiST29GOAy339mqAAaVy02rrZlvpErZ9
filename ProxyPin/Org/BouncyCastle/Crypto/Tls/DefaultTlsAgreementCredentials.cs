using System;
using Org.BouncyCastle.Crypto.Agreement;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004E6 RID: 1254
	public class DefaultTlsAgreementCredentials : AbstractTlsAgreementCredentials
	{
		// Token: 0x06002677 RID: 9847 RVA: 0x000D0D44 File Offset: 0x000D0D44
		public DefaultTlsAgreementCredentials(Certificate certificate, AsymmetricKeyParameter privateKey)
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
				throw new ArgumentNullException("privateKey");
			}
			if (!privateKey.IsPrivate)
			{
				throw new ArgumentException("must be private", "privateKey");
			}
			if (privateKey is DHPrivateKeyParameters)
			{
				this.mBasicAgreement = new DHBasicAgreement();
				this.mTruncateAgreement = true;
			}
			else
			{
				if (!(privateKey is ECPrivateKeyParameters))
				{
					throw new ArgumentException("type not supported: " + Platform.GetTypeName(privateKey), "privateKey");
				}
				this.mBasicAgreement = new ECDHBasicAgreement();
				this.mTruncateAgreement = false;
			}
			this.mCertificate = certificate;
			this.mPrivateKey = privateKey;
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06002678 RID: 9848 RVA: 0x000D0E20 File Offset: 0x000D0E20
		public override Certificate Certificate
		{
			get
			{
				return this.mCertificate;
			}
		}

		// Token: 0x06002679 RID: 9849 RVA: 0x000D0E28 File Offset: 0x000D0E28
		public override byte[] GenerateAgreement(AsymmetricKeyParameter peerPublicKey)
		{
			this.mBasicAgreement.Init(this.mPrivateKey);
			BigInteger n = this.mBasicAgreement.CalculateAgreement(peerPublicKey);
			if (this.mTruncateAgreement)
			{
				return BigIntegers.AsUnsignedByteArray(n);
			}
			return BigIntegers.AsUnsignedByteArray(this.mBasicAgreement.GetFieldSize(), n);
		}

		// Token: 0x0400190E RID: 6414
		protected readonly Certificate mCertificate;

		// Token: 0x0400190F RID: 6415
		protected readonly AsymmetricKeyParameter mPrivateKey;

		// Token: 0x04001910 RID: 6416
		protected readonly IBasicAgreement mBasicAgreement;

		// Token: 0x04001911 RID: 6417
		protected readonly bool mTruncateAgreement;
	}
}
