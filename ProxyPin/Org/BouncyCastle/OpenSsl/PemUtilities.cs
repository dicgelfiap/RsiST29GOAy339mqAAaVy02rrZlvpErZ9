using System;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.OpenSsl
{
	// Token: 0x02000678 RID: 1656
	internal sealed class PemUtilities
	{
		// Token: 0x060039E2 RID: 14818 RVA: 0x00136DF4 File Offset: 0x00136DF4
		static PemUtilities()
		{
			((PemUtilities.PemBaseAlg)Enums.GetArbitraryValue(typeof(PemUtilities.PemBaseAlg))).ToString();
			((PemUtilities.PemMode)Enums.GetArbitraryValue(typeof(PemUtilities.PemMode))).ToString();
		}

		// Token: 0x060039E3 RID: 14819 RVA: 0x00136E48 File Offset: 0x00136E48
		private static void ParseDekAlgName(string dekAlgName, out PemUtilities.PemBaseAlg baseAlg, out PemUtilities.PemMode mode)
		{
			try
			{
				mode = PemUtilities.PemMode.ECB;
				if (dekAlgName == "DES-EDE" || dekAlgName == "DES-EDE3")
				{
					baseAlg = (PemUtilities.PemBaseAlg)Enums.GetEnumValue(typeof(PemUtilities.PemBaseAlg), dekAlgName);
					return;
				}
				int num = dekAlgName.LastIndexOf('-');
				if (num >= 0)
				{
					baseAlg = (PemUtilities.PemBaseAlg)Enums.GetEnumValue(typeof(PemUtilities.PemBaseAlg), dekAlgName.Substring(0, num));
					mode = (PemUtilities.PemMode)Enums.GetEnumValue(typeof(PemUtilities.PemMode), dekAlgName.Substring(num + 1));
					return;
				}
			}
			catch (ArgumentException)
			{
			}
			throw new EncryptionException("Unknown DEK algorithm: " + dekAlgName);
		}

		// Token: 0x060039E4 RID: 14820 RVA: 0x00136F18 File Offset: 0x00136F18
		internal static byte[] Crypt(bool encrypt, byte[] bytes, char[] password, string dekAlgName, byte[] iv)
		{
			PemUtilities.PemBaseAlg baseAlg;
			PemUtilities.PemMode pemMode;
			PemUtilities.ParseDekAlgName(dekAlgName, out baseAlg, out pemMode);
			string text;
			switch (pemMode)
			{
			case PemUtilities.PemMode.CBC:
			case PemUtilities.PemMode.ECB:
				text = "PKCS5Padding";
				break;
			case PemUtilities.PemMode.CFB:
			case PemUtilities.PemMode.OFB:
				text = "NoPadding";
				break;
			default:
				throw new EncryptionException("Unknown DEK algorithm: " + dekAlgName);
			}
			byte[] array = iv;
			string text2;
			switch (baseAlg)
			{
			case PemUtilities.PemBaseAlg.AES_128:
			case PemUtilities.PemBaseAlg.AES_192:
			case PemUtilities.PemBaseAlg.AES_256:
				text2 = "AES";
				if (array.Length > 8)
				{
					array = new byte[8];
					Array.Copy(iv, 0, array, 0, array.Length);
				}
				break;
			case PemUtilities.PemBaseAlg.BF:
				text2 = "BLOWFISH";
				break;
			case PemUtilities.PemBaseAlg.DES:
				text2 = "DES";
				break;
			case PemUtilities.PemBaseAlg.DES_EDE:
			case PemUtilities.PemBaseAlg.DES_EDE3:
				text2 = "DESede";
				break;
			case PemUtilities.PemBaseAlg.RC2:
			case PemUtilities.PemBaseAlg.RC2_40:
			case PemUtilities.PemBaseAlg.RC2_64:
				text2 = "RC2";
				break;
			default:
				throw new EncryptionException("Unknown DEK algorithm: " + dekAlgName);
			}
			string algorithm = string.Concat(new object[]
			{
				text2,
				"/",
				pemMode,
				"/",
				text
			});
			IBufferedCipher cipher = CipherUtilities.GetCipher(algorithm);
			ICipherParameters parameters = PemUtilities.GetCipherParameters(password, baseAlg, array);
			if (pemMode != PemUtilities.PemMode.ECB)
			{
				parameters = new ParametersWithIV(parameters, iv);
			}
			cipher.Init(encrypt, parameters);
			return cipher.DoFinal(bytes);
		}

		// Token: 0x060039E5 RID: 14821 RVA: 0x00137090 File Offset: 0x00137090
		private static ICipherParameters GetCipherParameters(char[] password, PemUtilities.PemBaseAlg baseAlg, byte[] salt)
		{
			int keySize;
			string algorithm;
			switch (baseAlg)
			{
			case PemUtilities.PemBaseAlg.AES_128:
				keySize = 128;
				algorithm = "AES128";
				break;
			case PemUtilities.PemBaseAlg.AES_192:
				keySize = 192;
				algorithm = "AES192";
				break;
			case PemUtilities.PemBaseAlg.AES_256:
				keySize = 256;
				algorithm = "AES256";
				break;
			case PemUtilities.PemBaseAlg.BF:
				keySize = 128;
				algorithm = "BLOWFISH";
				break;
			case PemUtilities.PemBaseAlg.DES:
				keySize = 64;
				algorithm = "DES";
				break;
			case PemUtilities.PemBaseAlg.DES_EDE:
				keySize = 128;
				algorithm = "DESEDE";
				break;
			case PemUtilities.PemBaseAlg.DES_EDE3:
				keySize = 192;
				algorithm = "DESEDE3";
				break;
			case PemUtilities.PemBaseAlg.RC2:
				keySize = 128;
				algorithm = "RC2";
				break;
			case PemUtilities.PemBaseAlg.RC2_40:
				keySize = 40;
				algorithm = "RC2";
				break;
			case PemUtilities.PemBaseAlg.RC2_64:
				keySize = 64;
				algorithm = "RC2";
				break;
			default:
				return null;
			}
			OpenSslPbeParametersGenerator openSslPbeParametersGenerator = new OpenSslPbeParametersGenerator();
			openSslPbeParametersGenerator.Init(PbeParametersGenerator.Pkcs5PasswordToBytes(password), salt);
			return openSslPbeParametersGenerator.GenerateDerivedParameters(algorithm, keySize);
		}

		// Token: 0x02000E65 RID: 3685
		private enum PemBaseAlg
		{
			// Token: 0x04004246 RID: 16966
			AES_128,
			// Token: 0x04004247 RID: 16967
			AES_192,
			// Token: 0x04004248 RID: 16968
			AES_256,
			// Token: 0x04004249 RID: 16969
			BF,
			// Token: 0x0400424A RID: 16970
			DES,
			// Token: 0x0400424B RID: 16971
			DES_EDE,
			// Token: 0x0400424C RID: 16972
			DES_EDE3,
			// Token: 0x0400424D RID: 16973
			RC2,
			// Token: 0x0400424E RID: 16974
			RC2_40,
			// Token: 0x0400424F RID: 16975
			RC2_64
		}

		// Token: 0x02000E66 RID: 3686
		private enum PemMode
		{
			// Token: 0x04004251 RID: 16977
			CBC,
			// Token: 0x04004252 RID: 16978
			CFB,
			// Token: 0x04004253 RID: 16979
			ECB,
			// Token: 0x04004254 RID: 16980
			OFB
		}
	}
}
