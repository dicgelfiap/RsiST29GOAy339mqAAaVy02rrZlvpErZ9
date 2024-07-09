using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000538 RID: 1336
	public class TlsECDheKeyExchange : TlsECDHKeyExchange
	{
		// Token: 0x06002909 RID: 10505 RVA: 0x000DD00C File Offset: 0x000DD00C
		public TlsECDheKeyExchange(int keyExchange, IList supportedSignatureAlgorithms, int[] namedCurves, byte[] clientECPointFormats, byte[] serverECPointFormats) : base(keyExchange, supportedSignatureAlgorithms, namedCurves, clientECPointFormats, serverECPointFormats)
		{
		}

		// Token: 0x0600290A RID: 10506 RVA: 0x000DD024 File Offset: 0x000DD024
		public override void ProcessServerCredentials(TlsCredentials serverCredentials)
		{
			if (!(serverCredentials is TlsSignerCredentials))
			{
				throw new TlsFatalAlert(80);
			}
			this.ProcessServerCertificate(serverCredentials.Certificate);
			this.mServerCredentials = (TlsSignerCredentials)serverCredentials;
		}

		// Token: 0x0600290B RID: 10507 RVA: 0x000DD054 File Offset: 0x000DD054
		public override byte[] GenerateServerKeyExchange()
		{
			DigestInputBuffer digestInputBuffer = new DigestInputBuffer();
			this.mECAgreePrivateKey = TlsEccUtilities.GenerateEphemeralServerKeyExchange(this.mContext.SecureRandom, this.mNamedCurves, this.mClientECPointFormats, digestInputBuffer);
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

		// Token: 0x0600290C RID: 10508 RVA: 0x000DD114 File Offset: 0x000DD114
		public override void ProcessServerKeyExchange(Stream input)
		{
			SecurityParameters securityParameters = this.mContext.SecurityParameters;
			SignerInputBuffer signerInputBuffer = new SignerInputBuffer();
			Stream input2 = new TeeInputStream(input, signerInputBuffer);
			ECDomainParameters curve_params = TlsEccUtilities.ReadECParameters(this.mNamedCurves, this.mClientECPointFormats, input2);
			byte[] encoding = TlsUtilities.ReadOpaque8(input2);
			DigitallySigned digitallySigned = this.ParseSignature(input);
			ISigner signer = this.InitVerifyer(this.mTlsSigner, digitallySigned.Algorithm, securityParameters);
			signerInputBuffer.UpdateSigner(signer);
			if (!signer.VerifySignature(digitallySigned.Signature))
			{
				throw new TlsFatalAlert(51);
			}
			this.mECAgreePublicKey = TlsEccUtilities.ValidateECPublicKey(TlsEccUtilities.DeserializeECPublicKey(this.mClientECPointFormats, curve_params, encoding));
		}

		// Token: 0x0600290D RID: 10509 RVA: 0x000DD1B8 File Offset: 0x000DD1B8
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

		// Token: 0x0600290E RID: 10510 RVA: 0x000DD208 File Offset: 0x000DD208
		public override void ProcessClientCredentials(TlsCredentials clientCredentials)
		{
			if (clientCredentials is TlsSignerCredentials)
			{
				return;
			}
			throw new TlsFatalAlert(80);
		}

		// Token: 0x0600290F RID: 10511 RVA: 0x000DD220 File Offset: 0x000DD220
		protected virtual ISigner InitVerifyer(TlsSigner tlsSigner, SignatureAndHashAlgorithm algorithm, SecurityParameters securityParameters)
		{
			ISigner signer = tlsSigner.CreateVerifyer(algorithm, this.mServerPublicKey);
			signer.BlockUpdate(securityParameters.clientRandom, 0, securityParameters.clientRandom.Length);
			signer.BlockUpdate(securityParameters.serverRandom, 0, securityParameters.serverRandom.Length);
			return signer;
		}

		// Token: 0x04001AE1 RID: 6881
		protected TlsSignerCredentials mServerCredentials = null;
	}
}
