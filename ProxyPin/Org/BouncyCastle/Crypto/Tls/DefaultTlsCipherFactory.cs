using System;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004E7 RID: 1255
	public class DefaultTlsCipherFactory : AbstractTlsCipherFactory
	{
		// Token: 0x0600267A RID: 9850 RVA: 0x000D0E7C File Offset: 0x000D0E7C
		public override TlsCipher CreateCipher(TlsContext context, int encryptionAlgorithm, int macAlgorithm)
		{
			switch (encryptionAlgorithm)
			{
			case 0:
				return this.CreateNullCipher(context, macAlgorithm);
			case 1:
			case 3:
			case 4:
			case 5:
			case 6:
				break;
			case 2:
				return this.CreateRC4Cipher(context, 16, macAlgorithm);
			case 7:
				return this.CreateDesEdeCipher(context, macAlgorithm);
			case 8:
				return this.CreateAESCipher(context, 16, macAlgorithm);
			case 9:
				return this.CreateAESCipher(context, 32, macAlgorithm);
			case 10:
				return this.CreateCipher_Aes_Gcm(context, 16, 16);
			case 11:
				return this.CreateCipher_Aes_Gcm(context, 32, 16);
			case 12:
				return this.CreateCamelliaCipher(context, 16, macAlgorithm);
			case 13:
				return this.CreateCamelliaCipher(context, 32, macAlgorithm);
			case 14:
				return this.CreateSeedCipher(context, macAlgorithm);
			case 15:
				return this.CreateCipher_Aes_Ccm(context, 16, 16);
			case 16:
				return this.CreateCipher_Aes_Ccm(context, 16, 8);
			case 17:
				return this.CreateCipher_Aes_Ccm(context, 32, 16);
			case 18:
				return this.CreateCipher_Aes_Ccm(context, 32, 8);
			case 19:
				return this.CreateCipher_Camellia_Gcm(context, 16, 16);
			case 20:
				return this.CreateCipher_Camellia_Gcm(context, 32, 16);
			case 21:
				return this.CreateChaCha20Poly1305(context);
			default:
				switch (encryptionAlgorithm)
				{
				case 103:
					return this.CreateCipher_Aes_Ocb(context, 16, 12);
				case 104:
					return this.CreateCipher_Aes_Ocb(context, 32, 12);
				}
				break;
			}
			throw new TlsFatalAlert(80);
		}

		// Token: 0x0600267B RID: 9851 RVA: 0x000D0FDC File Offset: 0x000D0FDC
		protected virtual TlsBlockCipher CreateAESCipher(TlsContext context, int cipherKeySize, int macAlgorithm)
		{
			return new TlsBlockCipher(context, this.CreateAesBlockCipher(), this.CreateAesBlockCipher(), this.CreateHMacDigest(macAlgorithm), this.CreateHMacDigest(macAlgorithm), cipherKeySize);
		}

		// Token: 0x0600267C RID: 9852 RVA: 0x000D1010 File Offset: 0x000D1010
		protected virtual TlsBlockCipher CreateCamelliaCipher(TlsContext context, int cipherKeySize, int macAlgorithm)
		{
			return new TlsBlockCipher(context, this.CreateCamelliaBlockCipher(), this.CreateCamelliaBlockCipher(), this.CreateHMacDigest(macAlgorithm), this.CreateHMacDigest(macAlgorithm), cipherKeySize);
		}

		// Token: 0x0600267D RID: 9853 RVA: 0x000D1044 File Offset: 0x000D1044
		protected virtual TlsCipher CreateChaCha20Poly1305(TlsContext context)
		{
			return new Chacha20Poly1305(context);
		}

		// Token: 0x0600267E RID: 9854 RVA: 0x000D104C File Offset: 0x000D104C
		protected virtual TlsAeadCipher CreateCipher_Aes_Ccm(TlsContext context, int cipherKeySize, int macSize)
		{
			return new TlsAeadCipher(context, this.CreateAeadBlockCipher_Aes_Ccm(), this.CreateAeadBlockCipher_Aes_Ccm(), cipherKeySize, macSize);
		}

		// Token: 0x0600267F RID: 9855 RVA: 0x000D1074 File Offset: 0x000D1074
		protected virtual TlsAeadCipher CreateCipher_Aes_Gcm(TlsContext context, int cipherKeySize, int macSize)
		{
			return new TlsAeadCipher(context, this.CreateAeadBlockCipher_Aes_Gcm(), this.CreateAeadBlockCipher_Aes_Gcm(), cipherKeySize, macSize);
		}

		// Token: 0x06002680 RID: 9856 RVA: 0x000D109C File Offset: 0x000D109C
		protected virtual TlsAeadCipher CreateCipher_Aes_Ocb(TlsContext context, int cipherKeySize, int macSize)
		{
			return new TlsAeadCipher(context, this.CreateAeadBlockCipher_Aes_Ocb(), this.CreateAeadBlockCipher_Aes_Ocb(), cipherKeySize, macSize, 2);
		}

		// Token: 0x06002681 RID: 9857 RVA: 0x000D10C4 File Offset: 0x000D10C4
		protected virtual TlsAeadCipher CreateCipher_Camellia_Gcm(TlsContext context, int cipherKeySize, int macSize)
		{
			return new TlsAeadCipher(context, this.CreateAeadBlockCipher_Camellia_Gcm(), this.CreateAeadBlockCipher_Camellia_Gcm(), cipherKeySize, macSize);
		}

		// Token: 0x06002682 RID: 9858 RVA: 0x000D10EC File Offset: 0x000D10EC
		protected virtual TlsBlockCipher CreateDesEdeCipher(TlsContext context, int macAlgorithm)
		{
			return new TlsBlockCipher(context, this.CreateDesEdeBlockCipher(), this.CreateDesEdeBlockCipher(), this.CreateHMacDigest(macAlgorithm), this.CreateHMacDigest(macAlgorithm), 24);
		}

		// Token: 0x06002683 RID: 9859 RVA: 0x000D1120 File Offset: 0x000D1120
		protected virtual TlsNullCipher CreateNullCipher(TlsContext context, int macAlgorithm)
		{
			return new TlsNullCipher(context, this.CreateHMacDigest(macAlgorithm), this.CreateHMacDigest(macAlgorithm));
		}

		// Token: 0x06002684 RID: 9860 RVA: 0x000D1138 File Offset: 0x000D1138
		protected virtual TlsStreamCipher CreateRC4Cipher(TlsContext context, int cipherKeySize, int macAlgorithm)
		{
			return new TlsStreamCipher(context, this.CreateRC4StreamCipher(), this.CreateRC4StreamCipher(), this.CreateHMacDigest(macAlgorithm), this.CreateHMacDigest(macAlgorithm), cipherKeySize, false);
		}

		// Token: 0x06002685 RID: 9861 RVA: 0x000D116C File Offset: 0x000D116C
		protected virtual TlsBlockCipher CreateSeedCipher(TlsContext context, int macAlgorithm)
		{
			return new TlsBlockCipher(context, this.CreateSeedBlockCipher(), this.CreateSeedBlockCipher(), this.CreateHMacDigest(macAlgorithm), this.CreateHMacDigest(macAlgorithm), 16);
		}

		// Token: 0x06002686 RID: 9862 RVA: 0x000D11A0 File Offset: 0x000D11A0
		protected virtual IBlockCipher CreateAesEngine()
		{
			return new AesEngine();
		}

		// Token: 0x06002687 RID: 9863 RVA: 0x000D11A8 File Offset: 0x000D11A8
		protected virtual IBlockCipher CreateCamelliaEngine()
		{
			return new CamelliaEngine();
		}

		// Token: 0x06002688 RID: 9864 RVA: 0x000D11B0 File Offset: 0x000D11B0
		protected virtual IBlockCipher CreateAesBlockCipher()
		{
			return new CbcBlockCipher(this.CreateAesEngine());
		}

		// Token: 0x06002689 RID: 9865 RVA: 0x000D11C0 File Offset: 0x000D11C0
		protected virtual IAeadBlockCipher CreateAeadBlockCipher_Aes_Ccm()
		{
			return new CcmBlockCipher(this.CreateAesEngine());
		}

		// Token: 0x0600268A RID: 9866 RVA: 0x000D11D0 File Offset: 0x000D11D0
		protected virtual IAeadBlockCipher CreateAeadBlockCipher_Aes_Gcm()
		{
			return new GcmBlockCipher(this.CreateAesEngine());
		}

		// Token: 0x0600268B RID: 9867 RVA: 0x000D11E0 File Offset: 0x000D11E0
		protected virtual IAeadBlockCipher CreateAeadBlockCipher_Aes_Ocb()
		{
			return new OcbBlockCipher(this.CreateAesEngine(), this.CreateAesEngine());
		}

		// Token: 0x0600268C RID: 9868 RVA: 0x000D11F4 File Offset: 0x000D11F4
		protected virtual IAeadBlockCipher CreateAeadBlockCipher_Camellia_Gcm()
		{
			return new GcmBlockCipher(this.CreateCamelliaEngine());
		}

		// Token: 0x0600268D RID: 9869 RVA: 0x000D1204 File Offset: 0x000D1204
		protected virtual IBlockCipher CreateCamelliaBlockCipher()
		{
			return new CbcBlockCipher(this.CreateCamelliaEngine());
		}

		// Token: 0x0600268E RID: 9870 RVA: 0x000D1214 File Offset: 0x000D1214
		protected virtual IBlockCipher CreateDesEdeBlockCipher()
		{
			return new CbcBlockCipher(new DesEdeEngine());
		}

		// Token: 0x0600268F RID: 9871 RVA: 0x000D1220 File Offset: 0x000D1220
		protected virtual IStreamCipher CreateRC4StreamCipher()
		{
			return new RC4Engine();
		}

		// Token: 0x06002690 RID: 9872 RVA: 0x000D1228 File Offset: 0x000D1228
		protected virtual IBlockCipher CreateSeedBlockCipher()
		{
			return new CbcBlockCipher(new SeedEngine());
		}

		// Token: 0x06002691 RID: 9873 RVA: 0x000D1234 File Offset: 0x000D1234
		protected virtual IDigest CreateHMacDigest(int macAlgorithm)
		{
			switch (macAlgorithm)
			{
			case 0:
				return null;
			case 1:
				return TlsUtilities.CreateHash(1);
			case 2:
				return TlsUtilities.CreateHash(2);
			case 3:
				return TlsUtilities.CreateHash(4);
			case 4:
				return TlsUtilities.CreateHash(5);
			case 5:
				return TlsUtilities.CreateHash(6);
			default:
				throw new TlsFatalAlert(80);
			}
		}
	}
}
