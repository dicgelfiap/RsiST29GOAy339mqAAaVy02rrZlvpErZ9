using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000545 RID: 1349
	public class TlsRsaKeyExchange : AbstractTlsKeyExchange
	{
		// Token: 0x0600296D RID: 10605 RVA: 0x000DE3A4 File Offset: 0x000DE3A4
		public TlsRsaKeyExchange(IList supportedSignatureAlgorithms) : base(1, supportedSignatureAlgorithms)
		{
		}

		// Token: 0x0600296E RID: 10606 RVA: 0x000DE3C4 File Offset: 0x000DE3C4
		public override void SkipServerCredentials()
		{
			throw new TlsFatalAlert(10);
		}

		// Token: 0x0600296F RID: 10607 RVA: 0x000DE3D0 File Offset: 0x000DE3D0
		public override void ProcessServerCredentials(TlsCredentials serverCredentials)
		{
			if (!(serverCredentials is TlsEncryptionCredentials))
			{
				throw new TlsFatalAlert(80);
			}
			this.ProcessServerCertificate(serverCredentials.Certificate);
			this.mServerCredentials = (TlsEncryptionCredentials)serverCredentials;
		}

		// Token: 0x06002970 RID: 10608 RVA: 0x000DE400 File Offset: 0x000DE400
		public override void ProcessServerCertificate(Certificate serverCertificate)
		{
			if (serverCertificate.IsEmpty)
			{
				throw new TlsFatalAlert(42);
			}
			X509CertificateStructure certificateAt = serverCertificate.GetCertificateAt(0);
			SubjectPublicKeyInfo subjectPublicKeyInfo = certificateAt.SubjectPublicKeyInfo;
			try
			{
				this.mServerPublicKey = PublicKeyFactory.CreateKey(subjectPublicKeyInfo);
			}
			catch (Exception alertCause)
			{
				throw new TlsFatalAlert(43, alertCause);
			}
			if (this.mServerPublicKey.IsPrivate)
			{
				throw new TlsFatalAlert(80);
			}
			this.mRsaServerPublicKey = this.ValidateRsaPublicKey((RsaKeyParameters)this.mServerPublicKey);
			TlsUtilities.ValidateKeyUsage(certificateAt, 32);
			base.ProcessServerCertificate(serverCertificate);
		}

		// Token: 0x06002971 RID: 10609 RVA: 0x000DE498 File Offset: 0x000DE498
		public override void ValidateCertificateRequest(CertificateRequest certificateRequest)
		{
			foreach (byte b in certificateRequest.CertificateTypes)
			{
				switch (b)
				{
				case 1:
				case 2:
					break;
				default:
					if (b != 64)
					{
						throw new TlsFatalAlert(47);
					}
					break;
				}
			}
		}

		// Token: 0x06002972 RID: 10610 RVA: 0x000DE4E8 File Offset: 0x000DE4E8
		public override void ProcessClientCredentials(TlsCredentials clientCredentials)
		{
			if (!(clientCredentials is TlsSignerCredentials))
			{
				throw new TlsFatalAlert(80);
			}
		}

		// Token: 0x06002973 RID: 10611 RVA: 0x000DE500 File Offset: 0x000DE500
		public override void GenerateClientKeyExchange(Stream output)
		{
			this.mPremasterSecret = TlsRsaUtilities.GenerateEncryptedPreMasterSecret(this.mContext, this.mRsaServerPublicKey, output);
		}

		// Token: 0x06002974 RID: 10612 RVA: 0x000DE51C File Offset: 0x000DE51C
		public override void ProcessClientKeyExchange(Stream input)
		{
			byte[] encryptedPreMasterSecret;
			if (TlsUtilities.IsSsl(this.mContext))
			{
				encryptedPreMasterSecret = Streams.ReadAll(input);
			}
			else
			{
				encryptedPreMasterSecret = TlsUtilities.ReadOpaque16(input);
			}
			this.mPremasterSecret = this.mServerCredentials.DecryptPreMasterSecret(encryptedPreMasterSecret);
		}

		// Token: 0x06002975 RID: 10613 RVA: 0x000DE564 File Offset: 0x000DE564
		public override byte[] GeneratePremasterSecret()
		{
			if (this.mPremasterSecret == null)
			{
				throw new TlsFatalAlert(80);
			}
			byte[] result = this.mPremasterSecret;
			this.mPremasterSecret = null;
			return result;
		}

		// Token: 0x06002976 RID: 10614 RVA: 0x000DE598 File Offset: 0x000DE598
		protected virtual RsaKeyParameters ValidateRsaPublicKey(RsaKeyParameters key)
		{
			if (!key.Exponent.IsProbablePrime(2))
			{
				throw new TlsFatalAlert(47);
			}
			return key;
		}

		// Token: 0x04001AFE RID: 6910
		protected AsymmetricKeyParameter mServerPublicKey = null;

		// Token: 0x04001AFF RID: 6911
		protected RsaKeyParameters mRsaServerPublicKey = null;

		// Token: 0x04001B00 RID: 6912
		protected TlsEncryptionCredentials mServerCredentials = null;

		// Token: 0x04001B01 RID: 6913
		protected byte[] mPremasterSecret;
	}
}
