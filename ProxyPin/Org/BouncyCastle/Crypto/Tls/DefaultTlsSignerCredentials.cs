using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004ED RID: 1261
	public class DefaultTlsSignerCredentials : AbstractTlsSignerCredentials
	{
		// Token: 0x060026BB RID: 9915 RVA: 0x000D18D8 File Offset: 0x000D18D8
		public DefaultTlsSignerCredentials(TlsContext context, Certificate certificate, AsymmetricKeyParameter privateKey) : this(context, certificate, privateKey, null)
		{
		}

		// Token: 0x060026BC RID: 9916 RVA: 0x000D18E4 File Offset: 0x000D18E4
		public DefaultTlsSignerCredentials(TlsContext context, Certificate certificate, AsymmetricKeyParameter privateKey, SignatureAndHashAlgorithm signatureAndHashAlgorithm)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			if (certificate.IsEmpty)
			{
				throw new ArgumentException("cannot be empty", "clientCertificate");
			}
			if (privateKey == null)
			{
				throw new ArgumentNullException("privateKey");
			}
			if (!privateKey.IsPrivate)
			{
				throw new ArgumentException("must be private", "privateKey");
			}
			if (TlsUtilities.IsTlsV12(context) && signatureAndHashAlgorithm == null)
			{
				throw new ArgumentException("cannot be null for (D)TLS 1.2+", "signatureAndHashAlgorithm");
			}
			if (privateKey is RsaKeyParameters)
			{
				this.mSigner = new TlsRsaSigner();
			}
			else if (privateKey is DsaPrivateKeyParameters)
			{
				this.mSigner = new TlsDssSigner();
			}
			else
			{
				if (!(privateKey is ECPrivateKeyParameters))
				{
					throw new ArgumentException("type not supported: " + Platform.GetTypeName(privateKey), "privateKey");
				}
				this.mSigner = new TlsECDsaSigner();
			}
			this.mSigner.Init(context);
			this.mContext = context;
			this.mCertificate = certificate;
			this.mPrivateKey = privateKey;
			this.mSignatureAndHashAlgorithm = signatureAndHashAlgorithm;
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x060026BD RID: 9917 RVA: 0x000D1A0C File Offset: 0x000D1A0C
		public override Certificate Certificate
		{
			get
			{
				return this.mCertificate;
			}
		}

		// Token: 0x060026BE RID: 9918 RVA: 0x000D1A14 File Offset: 0x000D1A14
		public override byte[] GenerateCertificateSignature(byte[] hash)
		{
			byte[] result;
			try
			{
				if (TlsUtilities.IsTlsV12(this.mContext))
				{
					result = this.mSigner.GenerateRawSignature(this.mSignatureAndHashAlgorithm, this.mPrivateKey, hash);
				}
				else
				{
					result = this.mSigner.GenerateRawSignature(this.mPrivateKey, hash);
				}
			}
			catch (CryptoException alertCause)
			{
				throw new TlsFatalAlert(80, alertCause);
			}
			return result;
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x060026BF RID: 9919 RVA: 0x000D1A84 File Offset: 0x000D1A84
		public override SignatureAndHashAlgorithm SignatureAndHashAlgorithm
		{
			get
			{
				return this.mSignatureAndHashAlgorithm;
			}
		}

		// Token: 0x0400191A RID: 6426
		protected readonly TlsContext mContext;

		// Token: 0x0400191B RID: 6427
		protected readonly Certificate mCertificate;

		// Token: 0x0400191C RID: 6428
		protected readonly AsymmetricKeyParameter mPrivateKey;

		// Token: 0x0400191D RID: 6429
		protected readonly SignatureAndHashAlgorithm mSignatureAndHashAlgorithm;

		// Token: 0x0400191E RID: 6430
		protected readonly TlsSigner mSigner;
	}
}
