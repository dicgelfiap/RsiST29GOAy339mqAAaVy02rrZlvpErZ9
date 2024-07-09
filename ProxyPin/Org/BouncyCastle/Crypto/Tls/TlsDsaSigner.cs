using System;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Signers;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000534 RID: 1332
	public abstract class TlsDsaSigner : AbstractTlsSigner
	{
		// Token: 0x060028C6 RID: 10438 RVA: 0x000DBD24 File Offset: 0x000DBD24
		public override byte[] GenerateRawSignature(SignatureAndHashAlgorithm algorithm, AsymmetricKeyParameter privateKey, byte[] hash)
		{
			ISigner signer = this.MakeSigner(algorithm, true, true, new ParametersWithRandom(privateKey, this.mContext.SecureRandom));
			if (algorithm == null)
			{
				signer.BlockUpdate(hash, 16, 20);
			}
			else
			{
				signer.BlockUpdate(hash, 0, hash.Length);
			}
			return signer.GenerateSignature();
		}

		// Token: 0x060028C7 RID: 10439 RVA: 0x000DBD78 File Offset: 0x000DBD78
		public override bool VerifyRawSignature(SignatureAndHashAlgorithm algorithm, byte[] sigBytes, AsymmetricKeyParameter publicKey, byte[] hash)
		{
			ISigner signer = this.MakeSigner(algorithm, true, false, publicKey);
			if (algorithm == null)
			{
				signer.BlockUpdate(hash, 16, 20);
			}
			else
			{
				signer.BlockUpdate(hash, 0, hash.Length);
			}
			return signer.VerifySignature(sigBytes);
		}

		// Token: 0x060028C8 RID: 10440 RVA: 0x000DBDC0 File Offset: 0x000DBDC0
		public override ISigner CreateSigner(SignatureAndHashAlgorithm algorithm, AsymmetricKeyParameter privateKey)
		{
			return this.MakeSigner(algorithm, false, true, privateKey);
		}

		// Token: 0x060028C9 RID: 10441 RVA: 0x000DBDCC File Offset: 0x000DBDCC
		public override ISigner CreateVerifyer(SignatureAndHashAlgorithm algorithm, AsymmetricKeyParameter publicKey)
		{
			return this.MakeSigner(algorithm, false, false, publicKey);
		}

		// Token: 0x060028CA RID: 10442 RVA: 0x000DBDD8 File Offset: 0x000DBDD8
		protected virtual ICipherParameters MakeInitParameters(bool forSigning, ICipherParameters cp)
		{
			return cp;
		}

		// Token: 0x060028CB RID: 10443 RVA: 0x000DBDDC File Offset: 0x000DBDDC
		protected virtual ISigner MakeSigner(SignatureAndHashAlgorithm algorithm, bool raw, bool forSigning, ICipherParameters cp)
		{
			if (algorithm != null != TlsUtilities.IsTlsV12(this.mContext))
			{
				throw new InvalidOperationException();
			}
			if (algorithm != null && algorithm.Signature != this.SignatureAlgorithm)
			{
				throw new InvalidOperationException();
			}
			byte hashAlgorithm = (algorithm == null) ? 2 : algorithm.Hash;
			IDigest digest = raw ? new NullDigest() : TlsUtilities.CreateHash(hashAlgorithm);
			ISigner signer = new DsaDigestSigner(this.CreateDsaImpl(hashAlgorithm), digest);
			signer.Init(forSigning, this.MakeInitParameters(forSigning, cp));
			return signer;
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x060028CC RID: 10444
		protected abstract byte SignatureAlgorithm { get; }

		// Token: 0x060028CD RID: 10445
		protected abstract IDsa CreateDsaImpl(byte hashAlgorithm);
	}
}
