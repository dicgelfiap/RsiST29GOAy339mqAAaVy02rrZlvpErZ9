using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004C6 RID: 1222
	public abstract class AbstractTlsSigner : TlsSigner
	{
		// Token: 0x060025DB RID: 9691 RVA: 0x000CF568 File Offset: 0x000CF568
		public virtual void Init(TlsContext context)
		{
			this.mContext = context;
		}

		// Token: 0x060025DC RID: 9692 RVA: 0x000CF574 File Offset: 0x000CF574
		public virtual byte[] GenerateRawSignature(AsymmetricKeyParameter privateKey, byte[] md5AndSha1)
		{
			return this.GenerateRawSignature(null, privateKey, md5AndSha1);
		}

		// Token: 0x060025DD RID: 9693
		public abstract byte[] GenerateRawSignature(SignatureAndHashAlgorithm algorithm, AsymmetricKeyParameter privateKey, byte[] hash);

		// Token: 0x060025DE RID: 9694 RVA: 0x000CF580 File Offset: 0x000CF580
		public virtual bool VerifyRawSignature(byte[] sigBytes, AsymmetricKeyParameter publicKey, byte[] md5AndSha1)
		{
			return this.VerifyRawSignature(null, sigBytes, publicKey, md5AndSha1);
		}

		// Token: 0x060025DF RID: 9695
		public abstract bool VerifyRawSignature(SignatureAndHashAlgorithm algorithm, byte[] sigBytes, AsymmetricKeyParameter publicKey, byte[] hash);

		// Token: 0x060025E0 RID: 9696 RVA: 0x000CF58C File Offset: 0x000CF58C
		public virtual ISigner CreateSigner(AsymmetricKeyParameter privateKey)
		{
			return this.CreateSigner(null, privateKey);
		}

		// Token: 0x060025E1 RID: 9697
		public abstract ISigner CreateSigner(SignatureAndHashAlgorithm algorithm, AsymmetricKeyParameter privateKey);

		// Token: 0x060025E2 RID: 9698 RVA: 0x000CF598 File Offset: 0x000CF598
		public virtual ISigner CreateVerifyer(AsymmetricKeyParameter publicKey)
		{
			return this.CreateVerifyer(null, publicKey);
		}

		// Token: 0x060025E3 RID: 9699
		public abstract ISigner CreateVerifyer(SignatureAndHashAlgorithm algorithm, AsymmetricKeyParameter publicKey);

		// Token: 0x060025E4 RID: 9700
		public abstract bool IsValidPublicKey(AsymmetricKeyParameter publicKey);

		// Token: 0x040017A6 RID: 6054
		protected TlsContext mContext;
	}
}
