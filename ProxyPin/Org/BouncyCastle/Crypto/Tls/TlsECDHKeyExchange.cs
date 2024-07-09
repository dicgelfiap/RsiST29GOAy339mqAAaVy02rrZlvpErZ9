using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000537 RID: 1335
	public class TlsECDHKeyExchange : AbstractTlsKeyExchange
	{
		// Token: 0x060028FC RID: 10492 RVA: 0x000DCBC0 File Offset: 0x000DCBC0
		public TlsECDHKeyExchange(int keyExchange, IList supportedSignatureAlgorithms, int[] namedCurves, byte[] clientECPointFormats, byte[] serverECPointFormats) : base(keyExchange, supportedSignatureAlgorithms)
		{
			switch (keyExchange)
			{
			case 16:
			case 18:
			case 20:
				this.mTlsSigner = null;
				break;
			case 17:
				this.mTlsSigner = new TlsECDsaSigner();
				break;
			case 19:
				this.mTlsSigner = new TlsRsaSigner();
				break;
			default:
				throw new InvalidOperationException("unsupported key exchange algorithm");
			}
			this.mNamedCurves = namedCurves;
			this.mClientECPointFormats = clientECPointFormats;
			this.mServerECPointFormats = serverECPointFormats;
		}

		// Token: 0x060028FD RID: 10493 RVA: 0x000DCC4C File Offset: 0x000DCC4C
		public override void Init(TlsContext context)
		{
			base.Init(context);
			if (this.mTlsSigner != null)
			{
				this.mTlsSigner.Init(context);
			}
		}

		// Token: 0x060028FE RID: 10494 RVA: 0x000DCC6C File Offset: 0x000DCC6C
		public override void SkipServerCredentials()
		{
			if (this.mKeyExchange != 20)
			{
				throw new TlsFatalAlert(10);
			}
		}

		// Token: 0x060028FF RID: 10495 RVA: 0x000DCC84 File Offset: 0x000DCC84
		public override void ProcessServerCertificate(Certificate serverCertificate)
		{
			if (this.mKeyExchange == 20)
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
					this.mECAgreePublicKey = TlsEccUtilities.ValidateECPublicKey((ECPublicKeyParameters)this.mServerPublicKey);
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

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06002900 RID: 10496 RVA: 0x000DCD6C File Offset: 0x000DCD6C
		public override bool RequiresServerKeyExchange
		{
			get
			{
				switch (this.mKeyExchange)
				{
				case 17:
				case 19:
				case 20:
					return true;
				}
				return false;
			}
		}

		// Token: 0x06002901 RID: 10497 RVA: 0x000DCDA8 File Offset: 0x000DCDA8
		public override byte[] GenerateServerKeyExchange()
		{
			if (!this.RequiresServerKeyExchange)
			{
				return null;
			}
			MemoryStream memoryStream = new MemoryStream();
			this.mECAgreePrivateKey = TlsEccUtilities.GenerateEphemeralServerKeyExchange(this.mContext.SecureRandom, this.mNamedCurves, this.mClientECPointFormats, memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x06002902 RID: 10498 RVA: 0x000DCDF8 File Offset: 0x000DCDF8
		public override void ProcessServerKeyExchange(Stream input)
		{
			if (!this.RequiresServerKeyExchange)
			{
				throw new TlsFatalAlert(10);
			}
			ECDomainParameters curve_params = TlsEccUtilities.ReadECParameters(this.mNamedCurves, this.mClientECPointFormats, input);
			byte[] encoding = TlsUtilities.ReadOpaque8(input);
			this.mECAgreePublicKey = TlsEccUtilities.ValidateECPublicKey(TlsEccUtilities.DeserializeECPublicKey(this.mClientECPointFormats, curve_params, encoding));
		}

		// Token: 0x06002903 RID: 10499 RVA: 0x000DCE50 File Offset: 0x000DCE50
		public override void ValidateCertificateRequest(CertificateRequest certificateRequest)
		{
			if (this.mKeyExchange == 20)
			{
				throw new TlsFatalAlert(40);
			}
			foreach (byte b in certificateRequest.CertificateTypes)
			{
				switch (b)
				{
				case 1:
				case 2:
					break;
				default:
					switch (b)
					{
					case 64:
					case 65:
					case 66:
						break;
					default:
						throw new TlsFatalAlert(47);
					}
					break;
				}
			}
		}

		// Token: 0x06002904 RID: 10500 RVA: 0x000DCEC0 File Offset: 0x000DCEC0
		public override void ProcessClientCredentials(TlsCredentials clientCredentials)
		{
			if (this.mKeyExchange == 20)
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

		// Token: 0x06002905 RID: 10501 RVA: 0x000DCF14 File Offset: 0x000DCF14
		public override void GenerateClientKeyExchange(Stream output)
		{
			if (this.mAgreementCredentials == null)
			{
				this.mECAgreePrivateKey = TlsEccUtilities.GenerateEphemeralClientKeyExchange(this.mContext.SecureRandom, this.mServerECPointFormats, this.mECAgreePublicKey.Parameters, output);
			}
		}

		// Token: 0x06002906 RID: 10502 RVA: 0x000DCF58 File Offset: 0x000DCF58
		public override void ProcessClientCertificate(Certificate clientCertificate)
		{
			if (this.mKeyExchange == 20)
			{
				throw new TlsFatalAlert(10);
			}
		}

		// Token: 0x06002907 RID: 10503 RVA: 0x000DCF70 File Offset: 0x000DCF70
		public override void ProcessClientKeyExchange(Stream input)
		{
			if (this.mECAgreePublicKey != null)
			{
				return;
			}
			byte[] encoding = TlsUtilities.ReadOpaque8(input);
			ECDomainParameters parameters = this.mECAgreePrivateKey.Parameters;
			this.mECAgreePublicKey = TlsEccUtilities.ValidateECPublicKey(TlsEccUtilities.DeserializeECPublicKey(this.mServerECPointFormats, parameters, encoding));
		}

		// Token: 0x06002908 RID: 10504 RVA: 0x000DCFB8 File Offset: 0x000DCFB8
		public override byte[] GeneratePremasterSecret()
		{
			if (this.mAgreementCredentials != null)
			{
				return this.mAgreementCredentials.GenerateAgreement(this.mECAgreePublicKey);
			}
			if (this.mECAgreePrivateKey != null)
			{
				return TlsEccUtilities.CalculateECDHBasicAgreement(this.mECAgreePublicKey, this.mECAgreePrivateKey);
			}
			throw new TlsFatalAlert(80);
		}

		// Token: 0x04001AD9 RID: 6873
		protected TlsSigner mTlsSigner;

		// Token: 0x04001ADA RID: 6874
		protected int[] mNamedCurves;

		// Token: 0x04001ADB RID: 6875
		protected byte[] mClientECPointFormats;

		// Token: 0x04001ADC RID: 6876
		protected byte[] mServerECPointFormats;

		// Token: 0x04001ADD RID: 6877
		protected AsymmetricKeyParameter mServerPublicKey;

		// Token: 0x04001ADE RID: 6878
		protected TlsAgreementCredentials mAgreementCredentials;

		// Token: 0x04001ADF RID: 6879
		protected ECPrivateKeyParameters mECAgreePrivateKey;

		// Token: 0x04001AE0 RID: 6880
		protected ECPublicKeyParameters mECAgreePublicKey;
	}
}
