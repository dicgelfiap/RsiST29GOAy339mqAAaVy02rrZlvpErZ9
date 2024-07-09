using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000531 RID: 1329
	public class TlsDHKeyExchange : AbstractTlsKeyExchange
	{
		// Token: 0x06002899 RID: 10393 RVA: 0x000DB038 File Offset: 0x000DB038
		[Obsolete("Use constructor that takes a TlsDHVerifier")]
		public TlsDHKeyExchange(int keyExchange, IList supportedSignatureAlgorithms, DHParameters dhParameters) : this(keyExchange, supportedSignatureAlgorithms, new DefaultTlsDHVerifier(), dhParameters)
		{
		}

		// Token: 0x0600289A RID: 10394 RVA: 0x000DB048 File Offset: 0x000DB048
		public TlsDHKeyExchange(int keyExchange, IList supportedSignatureAlgorithms, TlsDHVerifier dhVerifier, DHParameters dhParameters) : base(keyExchange, supportedSignatureAlgorithms)
		{
			switch (keyExchange)
			{
			case 3:
				this.mTlsSigner = new TlsDssSigner();
				goto IL_72;
			case 5:
				this.mTlsSigner = new TlsRsaSigner();
				goto IL_72;
			case 7:
			case 9:
			case 11:
				this.mTlsSigner = null;
				goto IL_72;
			}
			throw new InvalidOperationException("unsupported key exchange algorithm");
			IL_72:
			this.mDHVerifier = dhVerifier;
			this.mDHParameters = dhParameters;
		}

		// Token: 0x0600289B RID: 10395 RVA: 0x000DB0DC File Offset: 0x000DB0DC
		public override void Init(TlsContext context)
		{
			base.Init(context);
			if (this.mTlsSigner != null)
			{
				this.mTlsSigner.Init(context);
			}
		}

		// Token: 0x0600289C RID: 10396 RVA: 0x000DB0FC File Offset: 0x000DB0FC
		public override void SkipServerCredentials()
		{
			if (this.mKeyExchange != 11)
			{
				throw new TlsFatalAlert(10);
			}
		}

		// Token: 0x0600289D RID: 10397 RVA: 0x000DB114 File Offset: 0x000DB114
		public override void ProcessServerCertificate(Certificate serverCertificate)
		{
			if (this.mKeyExchange == 11)
			{
				throw new TlsFatalAlert(10);
			}
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
			if (this.mTlsSigner == null)
			{
				try
				{
					this.mDHAgreePublicKey = (DHPublicKeyParameters)this.mServerPublicKey;
					this.mDHParameters = this.mDHAgreePublicKey.Parameters;
				}
				catch (InvalidCastException alertCause2)
				{
					throw new TlsFatalAlert(46, alertCause2);
				}
				TlsUtilities.ValidateKeyUsage(certificateAt, 8);
			}
			else
			{
				if (!this.mTlsSigner.IsValidPublicKey(this.mServerPublicKey))
				{
					throw new TlsFatalAlert(46);
				}
				TlsUtilities.ValidateKeyUsage(certificateAt, 128);
			}
			base.ProcessServerCertificate(serverCertificate);
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x0600289E RID: 10398 RVA: 0x000DB208 File Offset: 0x000DB208
		public override bool RequiresServerKeyExchange
		{
			get
			{
				int mKeyExchange = this.mKeyExchange;
				switch (mKeyExchange)
				{
				case 3:
				case 5:
					break;
				case 4:
					return false;
				default:
					if (mKeyExchange != 11)
					{
						return false;
					}
					break;
				}
				return true;
			}
		}

		// Token: 0x0600289F RID: 10399 RVA: 0x000DB240 File Offset: 0x000DB240
		public override byte[] GenerateServerKeyExchange()
		{
			if (!this.RequiresServerKeyExchange)
			{
				return null;
			}
			MemoryStream memoryStream = new MemoryStream();
			this.mDHAgreePrivateKey = TlsDHUtilities.GenerateEphemeralServerKeyExchange(this.mContext.SecureRandom, this.mDHParameters, memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x060028A0 RID: 10400 RVA: 0x000DB288 File Offset: 0x000DB288
		public override void ProcessServerKeyExchange(Stream input)
		{
			if (!this.RequiresServerKeyExchange)
			{
				throw new TlsFatalAlert(10);
			}
			this.mDHParameters = TlsDHUtilities.ReceiveDHParameters(this.mDHVerifier, input);
			this.mDHAgreePublicKey = new DHPublicKeyParameters(TlsDHUtilities.ReadDHParameter(input), this.mDHParameters);
		}

		// Token: 0x060028A1 RID: 10401 RVA: 0x000DB2C8 File Offset: 0x000DB2C8
		public override void ValidateCertificateRequest(CertificateRequest certificateRequest)
		{
			if (this.mKeyExchange == 11)
			{
				throw new TlsFatalAlert(40);
			}
			foreach (byte b in certificateRequest.CertificateTypes)
			{
				switch (b)
				{
				case 1:
				case 2:
				case 3:
				case 4:
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

		// Token: 0x060028A2 RID: 10402 RVA: 0x000DB334 File Offset: 0x000DB334
		public override void ProcessClientCredentials(TlsCredentials clientCredentials)
		{
			if (this.mKeyExchange == 11)
			{
				throw new TlsFatalAlert(80);
			}
			if (clientCredentials is TlsAgreementCredentials)
			{
				this.mAgreementCredentials = (TlsAgreementCredentials)clientCredentials;
				return;
			}
			if (clientCredentials is TlsSignerCredentials)
			{
				return;
			}
			throw new TlsFatalAlert(80);
		}

		// Token: 0x060028A3 RID: 10403 RVA: 0x000DB388 File Offset: 0x000DB388
		public override void GenerateClientKeyExchange(Stream output)
		{
			if (this.mAgreementCredentials == null)
			{
				this.mDHAgreePrivateKey = TlsDHUtilities.GenerateEphemeralClientKeyExchange(this.mContext.SecureRandom, this.mDHParameters, output);
			}
		}

		// Token: 0x060028A4 RID: 10404 RVA: 0x000DB3B4 File Offset: 0x000DB3B4
		public override void ProcessClientCertificate(Certificate clientCertificate)
		{
			if (this.mKeyExchange == 11)
			{
				throw new TlsFatalAlert(10);
			}
		}

		// Token: 0x060028A5 RID: 10405 RVA: 0x000DB3CC File Offset: 0x000DB3CC
		public override void ProcessClientKeyExchange(Stream input)
		{
			if (this.mDHAgreePublicKey != null)
			{
				return;
			}
			this.mDHAgreePublicKey = new DHPublicKeyParameters(TlsDHUtilities.ReadDHParameter(input), this.mDHParameters);
		}

		// Token: 0x060028A6 RID: 10406 RVA: 0x000DB3F4 File Offset: 0x000DB3F4
		public override byte[] GeneratePremasterSecret()
		{
			if (this.mAgreementCredentials != null)
			{
				return this.mAgreementCredentials.GenerateAgreement(this.mDHAgreePublicKey);
			}
			if (this.mDHAgreePrivateKey != null)
			{
				return TlsDHUtilities.CalculateDHBasicAgreement(this.mDHAgreePublicKey, this.mDHAgreePrivateKey);
			}
			throw new TlsFatalAlert(80);
		}

		// Token: 0x04001AC5 RID: 6853
		protected TlsSigner mTlsSigner;

		// Token: 0x04001AC6 RID: 6854
		protected TlsDHVerifier mDHVerifier;

		// Token: 0x04001AC7 RID: 6855
		protected DHParameters mDHParameters;

		// Token: 0x04001AC8 RID: 6856
		protected AsymmetricKeyParameter mServerPublicKey;

		// Token: 0x04001AC9 RID: 6857
		protected TlsAgreementCredentials mAgreementCredentials;

		// Token: 0x04001ACA RID: 6858
		protected DHPrivateKeyParameters mDHAgreePrivateKey;

		// Token: 0x04001ACB RID: 6859
		protected DHPublicKeyParameters mDHAgreePublicKey;
	}
}
