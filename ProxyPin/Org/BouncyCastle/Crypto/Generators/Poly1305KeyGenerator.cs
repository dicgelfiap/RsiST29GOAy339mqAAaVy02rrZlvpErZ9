using System;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x020003D4 RID: 980
	public class Poly1305KeyGenerator : CipherKeyGenerator
	{
		// Token: 0x06001EFA RID: 7930 RVA: 0x000B5D40 File Offset: 0x000B5D40
		protected override void engineInit(KeyGenerationParameters param)
		{
			this.random = param.Random;
			this.strength = 32;
		}

		// Token: 0x06001EFB RID: 7931 RVA: 0x000B5D58 File Offset: 0x000B5D58
		protected override byte[] engineGenerateKey()
		{
			byte[] array = base.engineGenerateKey();
			Poly1305KeyGenerator.Clamp(array);
			return array;
		}

		// Token: 0x06001EFC RID: 7932 RVA: 0x000B5D78 File Offset: 0x000B5D78
		public static void Clamp(byte[] key)
		{
			if (key.Length != 32)
			{
				throw new ArgumentException("Poly1305 key must be 256 bits.");
			}
			key[3] = (key[3] & 15);
			key[7] = (key[7] & 15);
			key[11] = (key[11] & 15);
			key[15] = (key[15] & 15);
			key[4] = (key[4] & 252);
			key[8] = (key[8] & 252);
			key[12] = (key[12] & 252);
		}

		// Token: 0x06001EFD RID: 7933 RVA: 0x000B5E04 File Offset: 0x000B5E04
		public static void CheckKey(byte[] key)
		{
			if (key.Length != 32)
			{
				throw new ArgumentException("Poly1305 key must be 256 bits.");
			}
			Poly1305KeyGenerator.CheckMask(key[3], 15);
			Poly1305KeyGenerator.CheckMask(key[7], 15);
			Poly1305KeyGenerator.CheckMask(key[11], 15);
			Poly1305KeyGenerator.CheckMask(key[15], 15);
			Poly1305KeyGenerator.CheckMask(key[4], 252);
			Poly1305KeyGenerator.CheckMask(key[8], 252);
			Poly1305KeyGenerator.CheckMask(key[12], 252);
		}

		// Token: 0x06001EFE RID: 7934 RVA: 0x000B5E7C File Offset: 0x000B5E7C
		private static void CheckMask(byte b, byte mask)
		{
			if ((b & ~(mask != 0)) != 0)
			{
				throw new ArgumentException("Invalid format for r portion of Poly1305 key.");
			}
		}

		// Token: 0x04001474 RID: 5236
		private const byte R_MASK_LOW_2 = 252;

		// Token: 0x04001475 RID: 5237
		private const byte R_MASK_HIGH_4 = 15;
	}
}
