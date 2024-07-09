using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000532 RID: 1330
	public class TlsDheKeyExchange : TlsDHKeyExchange
	{
		// Token: 0x060028A7 RID: 10407 RVA: 0x000DB448 File Offset: 0x000DB448
		[Obsolete("Use constructor that takes a TlsDHVerifier")]
		public TlsDheKeyExchange(int keyExchange, IList supportedSignatureAlgorithms, DHParameters dhParameters) : this(keyExchange, supportedSignatureAlgorithms, new DefaultTlsDHVerifier(), dhParameters)
		{
		}

		// Token: 0x060028A8 RID: 10408 RVA: 0x000DB458 File Offset: 0x000DB458
		public TlsDheKeyExchange(int keyExchange, IList supportedSignatureAlgorithms, TlsDHVerifier dhVerifier, DHParameters dhParameters) : base(keyExchange, supportedSignatureAlgorithms, dhVerifier, dhParameters)
		{
		}

		// Token: 0x060028A9 RID: 10409 RVA: 0x000DB46C File Offset: 0x000DB46C
		public override void ProcessServerCredentials(TlsCredentials serverCredentials)
		{
			if (!(serverCredentials is TlsSignerCredentials))
			{
				throw new TlsFatalAlert(80);
			}
			this.ProcessServerCertificate(serverCredentials.Certificate);
			this.mServerCredentials = (TlsSignerCredentials)serverCredentials;
		}

		// Token: 0x060028AA RID: 10410 RVA: 0x000DB49C File Offset: 0x000DB49C
		public override byte[] GenerateServerKeyExchange()
		{
			if (this.mDHParameters == null)
			{
				throw new TlsFatalAlert(80);
			}
			DigestInputBuffer digestInputBuffer = new DigestInputBuffer();
			this.mDHAgreePrivateKey = TlsDHUtilities.GenerateEphemeralServerKeyExchange(this.mContext.SecureRandom, this.mDHParameters, digestInputBuffer);
			SignatureAndHashAlgorithm signatureAndHashAlgorithm = TlsUtilities.GetSignatureAndHashAlgorithm(this.mContext, this.mServerCredentials);
			IDigest digest = TlsUtilities.CreateHash(signatureAndHashAlgorithm);
			SecurityParameters securityParameters = this.mContext.SecurityParameters;
			digest.BlockUpdate(securityParameters.clientRandom, 0, securityParameters.clientRandom.Length);
			digest.BlockUpdate(securityParameters.serverRandom, 0, securityParameters.serverRandom.Length);
			digestInputBuffer.UpdateDigest(digest);
			byte[] hash = DigestUtilities.DoFinal(digest);
			byte[] signature = this.mServerCredentials.GenerateCertificateSignature(hash);
			DigitallySigned digitallySigned = new DigitallySigned(signatureAndHashAlgorithm, signature);
			digitallySigned.Encode(digestInputBuffer);
			return digestInputBuffer.ToArray();
		}

		// Token: 0x060028AB RID: 10411 RVA: 0x000DB568 File Offset: 0x000DB568
		public override void ProcessServerKeyExchange(Stream input)
		{
			SecurityParameters securityParameters = this.mContext.SecurityParameters;
			SignerInputBuffer signerInputBuffer = new SignerInputBuffer();
			Stream input2 = new TeeInputStream(input, signerInputBuffer);
			this.mDHParameters = TlsDHUtilities.ReceiveDHParameters(this.mDHVerifier, input2);
			this.mDHAgreePublicKey = new DHPublicKeyParameters(TlsDHUtilities.ReadDHParameter(input2), this.mDHParameters);
			DigitallySigned digitallySigned = this.ParseSignature(input);
			ISigner signer = this.InitVerifyer(this.mTlsSigner, digitallySigned.Algorithm, securityParameters);
			signerInputBuffer.UpdateSigner(signer);
			if (!signer.VerifySignature(digitallySigned.Signature))
			{
				throw new TlsFatalAlert(51);
			}
		}

		// Token: 0x060028AC RID: 10412 RVA: 0x000DB5FC File Offset: 0x000DB5FC
		protected virtual ISigner InitVerifyer(TlsSigner tlsSigner, SignatureAndHashAlgorithm algorithm, SecurityParameters securityParameters)
		{
			ISigner signer = tlsSigner.CreateVerifyer(algorithm, this.mServerPublicKey);
			signer.BlockUpdate(securityParameters.clientRandom, 0, securityParameters.clientRandom.Length);
			signer.BlockUpdate(securityParameters.serverRandom, 0, securityParameters.serverRandom.Length);
			return signer;
		}

		// Token: 0x04001ACC RID: 6860
		protected TlsSignerCredentials mServerCredentials = null;
	}
}
