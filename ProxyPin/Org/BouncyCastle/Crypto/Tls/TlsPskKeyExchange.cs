using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000544 RID: 1348
	public class TlsPskKeyExchange : AbstractTlsKeyExchange
	{
		// Token: 0x0600295E RID: 10590 RVA: 0x000DDCF4 File Offset: 0x000DDCF4
		[Obsolete("Use constructor that takes a TlsDHVerifier")]
		public TlsPskKeyExchange(int keyExchange, IList supportedSignatureAlgorithms, TlsPskIdentity pskIdentity, TlsPskIdentityManager pskIdentityManager, DHParameters dhParameters, int[] namedCurves, byte[] clientECPointFormats, byte[] serverECPointFormats) : this(keyExchange, supportedSignatureAlgorithms, pskIdentity, pskIdentityManager, new DefaultTlsDHVerifier(), dhParameters, namedCurves, clientECPointFormats, serverECPointFormats)
		{
		}

		// Token: 0x0600295F RID: 10591 RVA: 0x000DDD20 File Offset: 0x000DDD20
		public TlsPskKeyExchange(int keyExchange, IList supportedSignatureAlgorithms, TlsPskIdentity pskIdentity, TlsPskIdentityManager pskIdentityManager, TlsDHVerifier dhVerifier, DHParameters dhParameters, int[] namedCurves, byte[] clientECPointFormats, byte[] serverECPointFormats) : base(keyExchange, supportedSignatureAlgorithms)
		{
			switch (keyExchange)
			{
			case 13:
			case 14:
			case 15:
				break;
			default:
				if (keyExchange != 24)
				{
					throw new InvalidOperationException("unsupported key exchange algorithm");
				}
				break;
			}
			this.mPskIdentity = pskIdentity;
			this.mPskIdentityManager = pskIdentityManager;
			this.mDHVerifier = dhVerifier;
			this.mDHParameters = dhParameters;
			this.mNamedCurves = namedCurves;
			this.mClientECPointFormats = clientECPointFormats;
			this.mServerECPointFormats = serverECPointFormats;
		}

		// Token: 0x06002960 RID: 10592 RVA: 0x000DDDDC File Offset: 0x000DDDDC
		public override void SkipServerCredentials()
		{
			if (this.mKeyExchange == 15)
			{
				throw new TlsFatalAlert(10);
			}
		}

		// Token: 0x06002961 RID: 10593 RVA: 0x000DDDF4 File Offset: 0x000DDDF4
		public override void ProcessServerCredentials(TlsCredentials serverCredentials)
		{
			if (!(serverCredentials is TlsEncryptionCredentials))
			{
				throw new TlsFatalAlert(80);
			}
			this.ProcessServerCertificate(serverCredentials.Certificate);
			this.mServerCredentials = (TlsEncryptionCredentials)serverCredentials;
		}

		// Token: 0x06002962 RID: 10594 RVA: 0x000DDE24 File Offset: 0x000DDE24
		public override byte[] GenerateServerKeyExchange()
		{
			this.mPskIdentityHint = this.mPskIdentityManager.GetHint();
			if (this.mPskIdentityHint == null && !this.RequiresServerKeyExchange)
			{
				return null;
			}
			MemoryStream memoryStream = new MemoryStream();
			if (this.mPskIdentityHint == null)
			{
				TlsUtilities.WriteOpaque16(TlsUtilities.EmptyBytes, memoryStream);
			}
			else
			{
				TlsUtilities.WriteOpaque16(this.mPskIdentityHint, memoryStream);
			}
			if (this.mKeyExchange == 14)
			{
				if (this.mDHParameters == null)
				{
					throw new TlsFatalAlert(80);
				}
				this.mDHAgreePrivateKey = TlsDHUtilities.GenerateEphemeralServerKeyExchange(this.mContext.SecureRandom, this.mDHParameters, memoryStream);
			}
			else if (this.mKeyExchange == 24)
			{
				this.mECAgreePrivateKey = TlsEccUtilities.GenerateEphemeralServerKeyExchange(this.mContext.SecureRandom, this.mNamedCurves, this.mClientECPointFormats, memoryStream);
			}
			return memoryStream.ToArray();
		}

		// Token: 0x06002963 RID: 10595 RVA: 0x000DDF04 File Offset: 0x000DDF04
		public override void ProcessServerCertificate(Certificate serverCertificate)
		{
			if (this.mKeyExchange != 15)
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
			if (this.mServerPublicKey.IsPrivate)
			{
				throw new TlsFatalAlert(80);
			}
			this.mRsaServerPublicKey = this.ValidateRsaPublicKey((RsaKeyParameters)this.mServerPublicKey);
			TlsUtilities.ValidateKeyUsage(certificateAt, 32);
			base.ProcessServerCertificate(serverCertificate);
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06002964 RID: 10596 RVA: 0x000DDFB4 File Offset: 0x000DDFB4
		public override bool RequiresServerKeyExchange
		{
			get
			{
				int mKeyExchange = this.mKeyExchange;
				return mKeyExchange == 14 || mKeyExchange == 24;
			}
		}

		// Token: 0x06002965 RID: 10597 RVA: 0x000DDFE0 File Offset: 0x000DDFE0
		public override void ProcessServerKeyExchange(Stream input)
		{
			this.mPskIdentityHint = TlsUtilities.ReadOpaque16(input);
			if (this.mKeyExchange == 14)
			{
				this.mDHParameters = TlsDHUtilities.ReceiveDHParameters(this.mDHVerifier, input);
				this.mDHAgreePublicKey = new DHPublicKeyParameters(TlsDHUtilities.ReadDHParameter(input), this.mDHParameters);
				return;
			}
			if (this.mKeyExchange == 24)
			{
				ECDomainParameters curve_params = TlsEccUtilities.ReadECParameters(this.mNamedCurves, this.mClientECPointFormats, input);
				byte[] encoding = TlsUtilities.ReadOpaque8(input);
				this.mECAgreePublicKey = TlsEccUtilities.ValidateECPublicKey(TlsEccUtilities.DeserializeECPublicKey(this.mClientECPointFormats, curve_params, encoding));
			}
		}

		// Token: 0x06002966 RID: 10598 RVA: 0x000DE074 File Offset: 0x000DE074
		public override void ValidateCertificateRequest(CertificateRequest certificateRequest)
		{
			throw new TlsFatalAlert(10);
		}

		// Token: 0x06002967 RID: 10599 RVA: 0x000DE080 File Offset: 0x000DE080
		public override void ProcessClientCredentials(TlsCredentials clientCredentials)
		{
			throw new TlsFatalAlert(80);
		}

		// Token: 0x06002968 RID: 10600 RVA: 0x000DE08C File Offset: 0x000DE08C
		public override void GenerateClientKeyExchange(Stream output)
		{
			if (this.mPskIdentityHint == null)
			{
				this.mPskIdentity.SkipIdentityHint();
			}
			else
			{
				this.mPskIdentity.NotifyIdentityHint(this.mPskIdentityHint);
			}
			byte[] pskIdentity = this.mPskIdentity.GetPskIdentity();
			if (pskIdentity == null)
			{
				throw new TlsFatalAlert(80);
			}
			this.mPsk = this.mPskIdentity.GetPsk();
			if (this.mPsk == null)
			{
				throw new TlsFatalAlert(80);
			}
			TlsUtilities.WriteOpaque16(pskIdentity, output);
			this.mContext.SecurityParameters.pskIdentity = pskIdentity;
			if (this.mKeyExchange == 14)
			{
				this.mDHAgreePrivateKey = TlsDHUtilities.GenerateEphemeralClientKeyExchange(this.mContext.SecureRandom, this.mDHParameters, output);
				return;
			}
			if (this.mKeyExchange == 24)
			{
				this.mECAgreePrivateKey = TlsEccUtilities.GenerateEphemeralClientKeyExchange(this.mContext.SecureRandom, this.mServerECPointFormats, this.mECAgreePublicKey.Parameters, output);
				return;
			}
			if (this.mKeyExchange == 15)
			{
				this.mPremasterSecret = TlsRsaUtilities.GenerateEncryptedPreMasterSecret(this.mContext, this.mRsaServerPublicKey, output);
			}
		}

		// Token: 0x06002969 RID: 10601 RVA: 0x000DE1A8 File Offset: 0x000DE1A8
		public override void ProcessClientKeyExchange(Stream input)
		{
			byte[] array = TlsUtilities.ReadOpaque16(input);
			this.mPsk = this.mPskIdentityManager.GetPsk(array);
			if (this.mPsk == null)
			{
				throw new TlsFatalAlert(115);
			}
			this.mContext.SecurityParameters.pskIdentity = array;
			if (this.mKeyExchange == 14)
			{
				this.mDHAgreePublicKey = new DHPublicKeyParameters(TlsDHUtilities.ReadDHParameter(input), this.mDHParameters);
				return;
			}
			if (this.mKeyExchange == 24)
			{
				byte[] encoding = TlsUtilities.ReadOpaque8(input);
				ECDomainParameters parameters = this.mECAgreePrivateKey.Parameters;
				this.mECAgreePublicKey = TlsEccUtilities.ValidateECPublicKey(TlsEccUtilities.DeserializeECPublicKey(this.mServerECPointFormats, parameters, encoding));
				return;
			}
			if (this.mKeyExchange == 15)
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
		}

		// Token: 0x0600296A RID: 10602 RVA: 0x000DE298 File Offset: 0x000DE298
		public override byte[] GeneratePremasterSecret()
		{
			byte[] array = this.GenerateOtherSecret(this.mPsk.Length);
			MemoryStream memoryStream = new MemoryStream(4 + array.Length + this.mPsk.Length);
			TlsUtilities.WriteOpaque16(array, memoryStream);
			TlsUtilities.WriteOpaque16(this.mPsk, memoryStream);
			Arrays.Fill(this.mPsk, 0);
			this.mPsk = null;
			return memoryStream.ToArray();
		}

		// Token: 0x0600296B RID: 10603 RVA: 0x000DE2F8 File Offset: 0x000DE2F8
		protected virtual byte[] GenerateOtherSecret(int pskLength)
		{
			if (this.mKeyExchange == 14)
			{
				if (this.mDHAgreePrivateKey != null)
				{
					return TlsDHUtilities.CalculateDHBasicAgreement(this.mDHAgreePublicKey, this.mDHAgreePrivateKey);
				}
				throw new TlsFatalAlert(80);
			}
			else if (this.mKeyExchange == 24)
			{
				if (this.mECAgreePrivateKey != null)
				{
					return TlsEccUtilities.CalculateECDHBasicAgreement(this.mECAgreePublicKey, this.mECAgreePrivateKey);
				}
				throw new TlsFatalAlert(80);
			}
			else
			{
				if (this.mKeyExchange == 15)
				{
					return this.mPremasterSecret;
				}
				return new byte[pskLength];
			}
		}

		// Token: 0x0600296C RID: 10604 RVA: 0x000DE388 File Offset: 0x000DE388
		protected virtual RsaKeyParameters ValidateRsaPublicKey(RsaKeyParameters key)
		{
			if (!key.Exponent.IsProbablePrime(2))
			{
				throw new TlsFatalAlert(47);
			}
			return key;
		}

		// Token: 0x04001AED RID: 6893
		protected TlsPskIdentity mPskIdentity;

		// Token: 0x04001AEE RID: 6894
		protected TlsPskIdentityManager mPskIdentityManager;

		// Token: 0x04001AEF RID: 6895
		protected TlsDHVerifier mDHVerifier;

		// Token: 0x04001AF0 RID: 6896
		protected DHParameters mDHParameters;

		// Token: 0x04001AF1 RID: 6897
		protected int[] mNamedCurves;

		// Token: 0x04001AF2 RID: 6898
		protected byte[] mClientECPointFormats;

		// Token: 0x04001AF3 RID: 6899
		protected byte[] mServerECPointFormats;

		// Token: 0x04001AF4 RID: 6900
		protected byte[] mPskIdentityHint = null;

		// Token: 0x04001AF5 RID: 6901
		protected byte[] mPsk = null;

		// Token: 0x04001AF6 RID: 6902
		protected DHPrivateKeyParameters mDHAgreePrivateKey = null;

		// Token: 0x04001AF7 RID: 6903
		protected DHPublicKeyParameters mDHAgreePublicKey = null;

		// Token: 0x04001AF8 RID: 6904
		protected ECPrivateKeyParameters mECAgreePrivateKey = null;

		// Token: 0x04001AF9 RID: 6905
		protected ECPublicKeyParameters mECAgreePublicKey = null;

		// Token: 0x04001AFA RID: 6906
		protected AsymmetricKeyParameter mServerPublicKey = null;

		// Token: 0x04001AFB RID: 6907
		protected RsaKeyParameters mRsaServerPublicKey = null;

		// Token: 0x04001AFC RID: 6908
		protected TlsEncryptionCredentials mServerCredentials = null;

		// Token: 0x04001AFD RID: 6909
		protected byte[] mPremasterSecret;
	}
}
