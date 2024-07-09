using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto.Agreement.Srp;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200054D RID: 1357
	public class TlsSrpKeyExchange : AbstractTlsKeyExchange
	{
		// Token: 0x060029A3 RID: 10659 RVA: 0x000DF954 File Offset: 0x000DF954
		protected static TlsSigner CreateSigner(int keyExchange)
		{
			switch (keyExchange)
			{
			case 21:
				return null;
			case 22:
				return new TlsDssSigner();
			case 23:
				return new TlsRsaSigner();
			default:
				throw new ArgumentException("unsupported key exchange algorithm");
			}
		}

		// Token: 0x060029A4 RID: 10660 RVA: 0x000DF99C File Offset: 0x000DF99C
		[Obsolete("Use constructor taking an explicit 'groupVerifier' argument")]
		public TlsSrpKeyExchange(int keyExchange, IList supportedSignatureAlgorithms, byte[] identity, byte[] password) : this(keyExchange, supportedSignatureAlgorithms, new DefaultTlsSrpGroupVerifier(), identity, password)
		{
		}

		// Token: 0x060029A5 RID: 10661 RVA: 0x000DF9B0 File Offset: 0x000DF9B0
		public TlsSrpKeyExchange(int keyExchange, IList supportedSignatureAlgorithms, TlsSrpGroupVerifier groupVerifier, byte[] identity, byte[] password)
		{
			this.mServerPublicKey = null;
			this.mSrpGroup = null;
			this.mSrpClient = null;
			this.mSrpServer = null;
			this.mSrpPeerCredentials = null;
			this.mSrpVerifier = null;
			this.mSrpSalt = null;
			this.mServerCredentials = null;
			base..ctor(keyExchange, supportedSignatureAlgorithms);
			this.mTlsSigner = TlsSrpKeyExchange.CreateSigner(keyExchange);
			this.mGroupVerifier = groupVerifier;
			this.mIdentity = identity;
			this.mPassword = password;
			this.mSrpClient = new Srp6Client();
		}

		// Token: 0x060029A6 RID: 10662 RVA: 0x000DFA30 File Offset: 0x000DFA30
		public TlsSrpKeyExchange(int keyExchange, IList supportedSignatureAlgorithms, byte[] identity, TlsSrpLoginParameters loginParameters)
		{
			this.mServerPublicKey = null;
			this.mSrpGroup = null;
			this.mSrpClient = null;
			this.mSrpServer = null;
			this.mSrpPeerCredentials = null;
			this.mSrpVerifier = null;
			this.mSrpSalt = null;
			this.mServerCredentials = null;
			base..ctor(keyExchange, supportedSignatureAlgorithms);
			this.mTlsSigner = TlsSrpKeyExchange.CreateSigner(keyExchange);
			this.mIdentity = identity;
			this.mSrpServer = new Srp6Server();
			this.mSrpGroup = loginParameters.Group;
			this.mSrpVerifier = loginParameters.Verifier;
			this.mSrpSalt = loginParameters.Salt;
		}

		// Token: 0x060029A7 RID: 10663 RVA: 0x000DFAC8 File Offset: 0x000DFAC8
		public override void Init(TlsContext context)
		{
			base.Init(context);
			if (this.mTlsSigner != null)
			{
				this.mTlsSigner.Init(context);
			}
		}

		// Token: 0x060029A8 RID: 10664 RVA: 0x000DFAE8 File Offset: 0x000DFAE8
		public override void SkipServerCredentials()
		{
			if (this.mTlsSigner != null)
			{
				throw new TlsFatalAlert(10);
			}
		}

		// Token: 0x060029A9 RID: 10665 RVA: 0x000DFB00 File Offset: 0x000DFB00
		public override void ProcessServerCertificate(Certificate serverCertificate)
		{
			if (this.mTlsSigner == null)
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
			if (!this.mTlsSigner.IsValidPublicKey(this.mServerPublicKey))
			{
				throw new TlsFatalAlert(46);
			}
			TlsUtilities.ValidateKeyUsage(certificateAt, 128);
			base.ProcessServerCertificate(serverCertificate);
		}

		// Token: 0x060029AA RID: 10666 RVA: 0x000DFBA0 File Offset: 0x000DFBA0
		public override void ProcessServerCredentials(TlsCredentials serverCredentials)
		{
			if (this.mKeyExchange == 21 || !(serverCredentials is TlsSignerCredentials))
			{
				throw new TlsFatalAlert(80);
			}
			this.ProcessServerCertificate(serverCredentials.Certificate);
			this.mServerCredentials = (TlsSignerCredentials)serverCredentials;
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x060029AB RID: 10667 RVA: 0x000DFBDC File Offset: 0x000DFBDC
		public override bool RequiresServerKeyExchange
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060029AC RID: 10668 RVA: 0x000DFBE0 File Offset: 0x000DFBE0
		public override byte[] GenerateServerKeyExchange()
		{
			this.mSrpServer.Init(this.mSrpGroup, this.mSrpVerifier, TlsUtilities.CreateHash(2), this.mContext.SecureRandom);
			BigInteger b = this.mSrpServer.GenerateServerCredentials();
			ServerSrpParams serverSrpParams = new ServerSrpParams(this.mSrpGroup.N, this.mSrpGroup.G, this.mSrpSalt, b);
			DigestInputBuffer digestInputBuffer = new DigestInputBuffer();
			serverSrpParams.Encode(digestInputBuffer);
			if (this.mServerCredentials != null)
			{
				SignatureAndHashAlgorithm signatureAndHashAlgorithm = TlsUtilities.GetSignatureAndHashAlgorithm(this.mContext, this.mServerCredentials);
				IDigest digest = TlsUtilities.CreateHash(signatureAndHashAlgorithm);
				SecurityParameters securityParameters = this.mContext.SecurityParameters;
				digest.BlockUpdate(securityParameters.clientRandom, 0, securityParameters.clientRandom.Length);
				digest.BlockUpdate(securityParameters.serverRandom, 0, securityParameters.serverRandom.Length);
				digestInputBuffer.UpdateDigest(digest);
				byte[] array = new byte[digest.GetDigestSize()];
				digest.DoFinal(array, 0);
				byte[] signature = this.mServerCredentials.GenerateCertificateSignature(array);
				DigitallySigned digitallySigned = new DigitallySigned(signatureAndHashAlgorithm, signature);
				digitallySigned.Encode(digestInputBuffer);
			}
			return digestInputBuffer.ToArray();
		}

		// Token: 0x060029AD RID: 10669 RVA: 0x000DFD00 File Offset: 0x000DFD00
		public override void ProcessServerKeyExchange(Stream input)
		{
			SecurityParameters securityParameters = this.mContext.SecurityParameters;
			SignerInputBuffer signerInputBuffer = null;
			Stream input2 = input;
			if (this.mTlsSigner != null)
			{
				signerInputBuffer = new SignerInputBuffer();
				input2 = new TeeInputStream(input, signerInputBuffer);
			}
			ServerSrpParams serverSrpParams = ServerSrpParams.Parse(input2);
			if (signerInputBuffer != null)
			{
				DigitallySigned digitallySigned = this.ParseSignature(input);
				ISigner signer = this.InitVerifyer(this.mTlsSigner, digitallySigned.Algorithm, securityParameters);
				signerInputBuffer.UpdateSigner(signer);
				if (!signer.VerifySignature(digitallySigned.Signature))
				{
					throw new TlsFatalAlert(51);
				}
			}
			this.mSrpGroup = new Srp6GroupParameters(serverSrpParams.N, serverSrpParams.G);
			if (!this.mGroupVerifier.Accept(this.mSrpGroup))
			{
				throw new TlsFatalAlert(71);
			}
			this.mSrpSalt = serverSrpParams.S;
			try
			{
				this.mSrpPeerCredentials = Srp6Utilities.ValidatePublicValue(this.mSrpGroup.N, serverSrpParams.B);
			}
			catch (CryptoException alertCause)
			{
				throw new TlsFatalAlert(47, alertCause);
			}
			this.mSrpClient.Init(this.mSrpGroup, TlsUtilities.CreateHash(2), this.mContext.SecureRandom);
		}

		// Token: 0x060029AE RID: 10670 RVA: 0x000DFE28 File Offset: 0x000DFE28
		public override void ValidateCertificateRequest(CertificateRequest certificateRequest)
		{
			throw new TlsFatalAlert(10);
		}

		// Token: 0x060029AF RID: 10671 RVA: 0x000DFE34 File Offset: 0x000DFE34
		public override void ProcessClientCredentials(TlsCredentials clientCredentials)
		{
			throw new TlsFatalAlert(80);
		}

		// Token: 0x060029B0 RID: 10672 RVA: 0x000DFE40 File Offset: 0x000DFE40
		public override void GenerateClientKeyExchange(Stream output)
		{
			BigInteger x = this.mSrpClient.GenerateClientCredentials(this.mSrpSalt, this.mIdentity, this.mPassword);
			TlsSrpUtilities.WriteSrpParameter(x, output);
			this.mContext.SecurityParameters.srpIdentity = Arrays.Clone(this.mIdentity);
		}

		// Token: 0x060029B1 RID: 10673 RVA: 0x000DFE94 File Offset: 0x000DFE94
		public override void ProcessClientKeyExchange(Stream input)
		{
			try
			{
				this.mSrpPeerCredentials = Srp6Utilities.ValidatePublicValue(this.mSrpGroup.N, TlsSrpUtilities.ReadSrpParameter(input));
			}
			catch (CryptoException alertCause)
			{
				throw new TlsFatalAlert(47, alertCause);
			}
			this.mContext.SecurityParameters.srpIdentity = Arrays.Clone(this.mIdentity);
		}

		// Token: 0x060029B2 RID: 10674 RVA: 0x000DFEF8 File Offset: 0x000DFEF8
		public override byte[] GeneratePremasterSecret()
		{
			byte[] result;
			try
			{
				BigInteger n = (this.mSrpServer != null) ? this.mSrpServer.CalculateSecret(this.mSrpPeerCredentials) : this.mSrpClient.CalculateSecret(this.mSrpPeerCredentials);
				result = BigIntegers.AsUnsignedByteArray(n);
			}
			catch (CryptoException alertCause)
			{
				throw new TlsFatalAlert(47, alertCause);
			}
			return result;
		}

		// Token: 0x060029B3 RID: 10675 RVA: 0x000DFF60 File Offset: 0x000DFF60
		protected virtual ISigner InitVerifyer(TlsSigner tlsSigner, SignatureAndHashAlgorithm algorithm, SecurityParameters securityParameters)
		{
			ISigner signer = tlsSigner.CreateVerifyer(algorithm, this.mServerPublicKey);
			signer.BlockUpdate(securityParameters.clientRandom, 0, securityParameters.clientRandom.Length);
			signer.BlockUpdate(securityParameters.serverRandom, 0, securityParameters.serverRandom.Length);
			return signer;
		}

		// Token: 0x04001B0C RID: 6924
		protected TlsSigner mTlsSigner;

		// Token: 0x04001B0D RID: 6925
		protected TlsSrpGroupVerifier mGroupVerifier;

		// Token: 0x04001B0E RID: 6926
		protected byte[] mIdentity;

		// Token: 0x04001B0F RID: 6927
		protected byte[] mPassword;

		// Token: 0x04001B10 RID: 6928
		protected AsymmetricKeyParameter mServerPublicKey;

		// Token: 0x04001B11 RID: 6929
		protected Srp6GroupParameters mSrpGroup;

		// Token: 0x04001B12 RID: 6930
		protected Srp6Client mSrpClient;

		// Token: 0x04001B13 RID: 6931
		protected Srp6Server mSrpServer;

		// Token: 0x04001B14 RID: 6932
		protected BigInteger mSrpPeerCredentials;

		// Token: 0x04001B15 RID: 6933
		protected BigInteger mSrpVerifier;

		// Token: 0x04001B16 RID: 6934
		protected byte[] mSrpSalt;

		// Token: 0x04001B17 RID: 6935
		protected TlsSignerCredentials mServerCredentials;
	}
}
