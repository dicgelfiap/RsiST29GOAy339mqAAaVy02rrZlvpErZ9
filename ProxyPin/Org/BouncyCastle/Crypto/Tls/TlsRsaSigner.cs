using System;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Signers;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000546 RID: 1350
	public class TlsRsaSigner : AbstractTlsSigner
	{
		// Token: 0x06002977 RID: 10615 RVA: 0x000DE5B4 File Offset: 0x000DE5B4
		public override byte[] GenerateRawSignature(SignatureAndHashAlgorithm algorithm, AsymmetricKeyParameter privateKey, byte[] hash)
		{
			ISigner signer = this.MakeSigner(algorithm, true, true, new ParametersWithRandom(privateKey, this.mContext.SecureRandom));
			signer.BlockUpdate(hash, 0, hash.Length);
			return signer.GenerateSignature();
		}

		// Token: 0x06002978 RID: 10616 RVA: 0x000DE5F4 File Offset: 0x000DE5F4
		public override bool VerifyRawSignature(SignatureAndHashAlgorithm algorithm, byte[] sigBytes, AsymmetricKeyParameter publicKey, byte[] hash)
		{
			ISigner signer = this.MakeSigner(algorithm, true, false, publicKey);
			signer.BlockUpdate(hash, 0, hash.Length);
			return signer.VerifySignature(sigBytes);
		}

		// Token: 0x06002979 RID: 10617 RVA: 0x000DE624 File Offset: 0x000DE624
		public override ISigner CreateSigner(SignatureAndHashAlgorithm algorithm, AsymmetricKeyParameter privateKey)
		{
			return this.MakeSigner(algorithm, false, true, new ParametersWithRandom(privateKey, this.mContext.SecureRandom));
		}

		// Token: 0x0600297A RID: 10618 RVA: 0x000DE650 File Offset: 0x000DE650
		public override ISigner CreateVerifyer(SignatureAndHashAlgorithm algorithm, AsymmetricKeyParameter publicKey)
		{
			return this.MakeSigner(algorithm, false, false, publicKey);
		}

		// Token: 0x0600297B RID: 10619 RVA: 0x000DE65C File Offset: 0x000DE65C
		public override bool IsValidPublicKey(AsymmetricKeyParameter publicKey)
		{
			return publicKey is RsaKeyParameters && !publicKey.IsPrivate;
		}

		// Token: 0x0600297C RID: 10620 RVA: 0x000DE674 File Offset: 0x000DE674
		protected virtual ISigner MakeSigner(SignatureAndHashAlgorithm algorithm, bool raw, bool forSigning, ICipherParameters cp)
		{
			if (algorithm != null != TlsUtilities.IsTlsV12(this.mContext))
			{
				throw new InvalidOperationException();
			}
			if (algorithm != null && algorithm.Signature != 1)
			{
				throw new InvalidOperationException();
			}
			IDigest digest;
			if (raw)
			{
				digest = new NullDigest();
			}
			else if (algorithm == null)
			{
				digest = new CombinedHash();
			}
			else
			{
				digest = TlsUtilities.CreateHash(algorithm.Hash);
			}
			ISigner signer;
			if (algorithm != null)
			{
				signer = new RsaDigestSigner(digest, TlsUtilities.GetOidForHashAlgorithm(algorithm.Hash));
			}
			else
			{
				signer = new GenericSigner(this.CreateRsaImpl(), digest);
			}
			signer.Init(forSigning, cp);
			return signer;
		}

		// Token: 0x0600297D RID: 10621 RVA: 0x000DE71C File Offset: 0x000DE71C
		protected virtual IAsymmetricBlockCipher CreateRsaImpl()
		{
			return new Pkcs1Encoding(new RsaBlindedEngine());
		}
	}
}
