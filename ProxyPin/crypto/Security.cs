using System;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Encoders;

namespace crypto
{
	// Token: 0x02000568 RID: 1384
	public class Security
	{
		// Token: 0x06002AF6 RID: 10998 RVA: 0x000E50D0 File Offset: 0x000E50D0
		public static string ComputeHash(string text, string salt)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(text);
			Sha512Digest sha512Digest = new Sha512Digest();
			Pkcs5S2ParametersGenerator pkcs5S2ParametersGenerator = new Pkcs5S2ParametersGenerator(sha512Digest);
			pkcs5S2ParametersGenerator.Init(bytes, Base64.Decode(salt), 2048);
			return Base64.ToBase64String(((KeyParameter)pkcs5S2ParametersGenerator.GenerateDerivedParameters(sha512Digest.GetDigestSize() * 8)).GetKey());
		}

		// Token: 0x06002AF7 RID: 10999 RVA: 0x000E512C File Offset: 0x000E512C
		public static string Decrypt(string cipherText, string key, string iv)
		{
			IBufferedCipher bufferedCipher = Security.CreateCipher(false, key, iv);
			byte[] array = bufferedCipher.DoFinal(Base64.Decode(cipherText));
			return Encoding.UTF8.GetString(array, 0, array.Length);
		}

		// Token: 0x06002AF8 RID: 11000 RVA: 0x000E5164 File Offset: 0x000E5164
		public static string Encrypt(string plainText, string key, string iv)
		{
			IBufferedCipher bufferedCipher = Security.CreateCipher(true, key, iv);
			return Base64.ToBase64String(bufferedCipher.DoFinal(Encoding.UTF8.GetBytes(plainText)));
		}

		// Token: 0x06002AF9 RID: 11001 RVA: 0x000E5194 File Offset: 0x000E5194
		public static string GenerateText(int size)
		{
			byte[] array = new byte[size];
			SecureRandom instance = SecureRandom.GetInstance("SHA256PRNG", true);
			instance.NextBytes(array);
			return Base64.ToBase64String(array);
		}

		// Token: 0x06002AFA RID: 11002 RVA: 0x000E51C8 File Offset: 0x000E51C8
		private static IBufferedCipher CreateCipher(bool isEncryption, string key, string iv)
		{
			IBufferedCipher bufferedCipher = new PaddedBufferedBlockCipher(new CbcBlockCipher(new RijndaelEngine()), new ISO10126d2Padding());
			KeyParameter keyParameter = new KeyParameter(Base64.Decode(key));
			ICipherParameters parameters = (iv == null || iv.Length < 1) ? keyParameter : new ParametersWithIV(keyParameter, Base64.Decode(iv));
			bufferedCipher.Init(isEncryption, parameters);
			return bufferedCipher;
		}
	}
}
