using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004C5 RID: 1221
	public interface TlsSigner
	{
		// Token: 0x060025D1 RID: 9681
		void Init(TlsContext context);

		// Token: 0x060025D2 RID: 9682
		byte[] GenerateRawSignature(AsymmetricKeyParameter privateKey, byte[] md5AndSha1);

		// Token: 0x060025D3 RID: 9683
		byte[] GenerateRawSignature(SignatureAndHashAlgorithm algorithm, AsymmetricKeyParameter privateKey, byte[] hash);

		// Token: 0x060025D4 RID: 9684
		bool VerifyRawSignature(byte[] sigBytes, AsymmetricKeyParameter publicKey, byte[] md5AndSha1);

		// Token: 0x060025D5 RID: 9685
		bool VerifyRawSignature(SignatureAndHashAlgorithm algorithm, byte[] sigBytes, AsymmetricKeyParameter publicKey, byte[] hash);

		// Token: 0x060025D6 RID: 9686
		ISigner CreateSigner(AsymmetricKeyParameter privateKey);

		// Token: 0x060025D7 RID: 9687
		ISigner CreateSigner(SignatureAndHashAlgorithm algorithm, AsymmetricKeyParameter privateKey);

		// Token: 0x060025D8 RID: 9688
		ISigner CreateVerifyer(AsymmetricKeyParameter publicKey);

		// Token: 0x060025D9 RID: 9689
		ISigner CreateVerifyer(SignatureAndHashAlgorithm algorithm, AsymmetricKeyParameter publicKey);

		// Token: 0x060025DA RID: 9690
		bool IsValidPublicKey(AsymmetricKeyParameter publicKey);
	}
}
