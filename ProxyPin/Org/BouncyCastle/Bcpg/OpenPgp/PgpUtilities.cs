using System;
using System.IO;
using System.Text;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x0200066B RID: 1643
	public sealed class PgpUtilities
	{
		// Token: 0x06003991 RID: 14737 RVA: 0x00134E38 File Offset: 0x00134E38
		private PgpUtilities()
		{
		}

		// Token: 0x06003992 RID: 14738 RVA: 0x00134E40 File Offset: 0x00134E40
		public static MPInteger[] DsaSigToMpi(byte[] encoding)
		{
			DerInteger instance2;
			DerInteger instance3;
			try
			{
				Asn1Sequence instance = Asn1Sequence.GetInstance(encoding);
				instance2 = DerInteger.GetInstance(instance[0]);
				instance3 = DerInteger.GetInstance(instance[1]);
			}
			catch (Exception exception)
			{
				throw new PgpException("exception encoding signature", exception);
			}
			return new MPInteger[]
			{
				new MPInteger(instance2.Value),
				new MPInteger(instance3.Value)
			};
		}

		// Token: 0x06003993 RID: 14739 RVA: 0x00134EC0 File Offset: 0x00134EC0
		public static MPInteger[] RsaSigToMpi(byte[] encoding)
		{
			return new MPInteger[]
			{
				new MPInteger(new BigInteger(1, encoding))
			};
		}

		// Token: 0x06003994 RID: 14740 RVA: 0x00134EEC File Offset: 0x00134EEC
		public static string GetDigestName(HashAlgorithmTag hashAlgorithm)
		{
			switch (hashAlgorithm)
			{
			case HashAlgorithmTag.MD5:
				return "MD5";
			case HashAlgorithmTag.Sha1:
				return "SHA1";
			case HashAlgorithmTag.RipeMD160:
				return "RIPEMD160";
			case HashAlgorithmTag.MD2:
				return "MD2";
			case HashAlgorithmTag.Sha256:
				return "SHA256";
			case HashAlgorithmTag.Sha384:
				return "SHA384";
			case HashAlgorithmTag.Sha512:
				return "SHA512";
			case HashAlgorithmTag.Sha224:
				return "SHA224";
			}
			throw new PgpException("unknown hash algorithm tag in GetDigestName: " + hashAlgorithm);
		}

		// Token: 0x06003995 RID: 14741 RVA: 0x00134F80 File Offset: 0x00134F80
		public static string GetSignatureName(PublicKeyAlgorithmTag keyAlgorithm, HashAlgorithmTag hashAlgorithm)
		{
			string str;
			switch (keyAlgorithm)
			{
			case PublicKeyAlgorithmTag.RsaGeneral:
			case PublicKeyAlgorithmTag.RsaSign:
				str = "RSA";
				goto IL_85;
			case PublicKeyAlgorithmTag.RsaEncrypt:
				break;
			default:
				switch (keyAlgorithm)
				{
				case PublicKeyAlgorithmTag.ElGamalEncrypt:
				case PublicKeyAlgorithmTag.ElGamalGeneral:
					str = "ElGamal";
					goto IL_85;
				case PublicKeyAlgorithmTag.Dsa:
					str = "DSA";
					goto IL_85;
				case PublicKeyAlgorithmTag.EC:
					str = "ECDH";
					goto IL_85;
				case PublicKeyAlgorithmTag.ECDsa:
					str = "ECDSA";
					goto IL_85;
				}
				break;
			}
			throw new PgpException("unknown algorithm tag in signature:" + keyAlgorithm);
			IL_85:
			return PgpUtilities.GetDigestName(hashAlgorithm) + "with" + str;
		}

		// Token: 0x06003996 RID: 14742 RVA: 0x00135028 File Offset: 0x00135028
		public static string GetSymmetricCipherName(SymmetricKeyAlgorithmTag algorithm)
		{
			switch (algorithm)
			{
			case SymmetricKeyAlgorithmTag.Null:
				return null;
			case SymmetricKeyAlgorithmTag.Idea:
				return "IDEA";
			case SymmetricKeyAlgorithmTag.TripleDes:
				return "DESEDE";
			case SymmetricKeyAlgorithmTag.Cast5:
				return "CAST5";
			case SymmetricKeyAlgorithmTag.Blowfish:
				return "Blowfish";
			case SymmetricKeyAlgorithmTag.Safer:
				return "SAFER";
			case SymmetricKeyAlgorithmTag.Des:
				return "DES";
			case SymmetricKeyAlgorithmTag.Aes128:
				return "AES";
			case SymmetricKeyAlgorithmTag.Aes192:
				return "AES";
			case SymmetricKeyAlgorithmTag.Aes256:
				return "AES";
			case SymmetricKeyAlgorithmTag.Twofish:
				return "Twofish";
			case SymmetricKeyAlgorithmTag.Camellia128:
				return "Camellia";
			case SymmetricKeyAlgorithmTag.Camellia192:
				return "Camellia";
			case SymmetricKeyAlgorithmTag.Camellia256:
				return "Camellia";
			default:
				throw new PgpException("unknown symmetric algorithm: " + algorithm);
			}
		}

		// Token: 0x06003997 RID: 14743 RVA: 0x001350E4 File Offset: 0x001350E4
		public static int GetKeySize(SymmetricKeyAlgorithmTag algorithm)
		{
			int result;
			switch (algorithm)
			{
			case SymmetricKeyAlgorithmTag.Idea:
			case SymmetricKeyAlgorithmTag.Cast5:
			case SymmetricKeyAlgorithmTag.Blowfish:
			case SymmetricKeyAlgorithmTag.Safer:
			case SymmetricKeyAlgorithmTag.Aes128:
			case SymmetricKeyAlgorithmTag.Camellia128:
				result = 128;
				break;
			case SymmetricKeyAlgorithmTag.TripleDes:
			case SymmetricKeyAlgorithmTag.Aes192:
			case SymmetricKeyAlgorithmTag.Camellia192:
				result = 192;
				break;
			case SymmetricKeyAlgorithmTag.Des:
				result = 64;
				break;
			case SymmetricKeyAlgorithmTag.Aes256:
			case SymmetricKeyAlgorithmTag.Twofish:
			case SymmetricKeyAlgorithmTag.Camellia256:
				result = 256;
				break;
			default:
				throw new PgpException("unknown symmetric algorithm: " + algorithm);
			}
			return result;
		}

		// Token: 0x06003998 RID: 14744 RVA: 0x00135178 File Offset: 0x00135178
		public static KeyParameter MakeKey(SymmetricKeyAlgorithmTag algorithm, byte[] keyBytes)
		{
			string symmetricCipherName = PgpUtilities.GetSymmetricCipherName(algorithm);
			return ParameterUtilities.CreateKeyParameter(symmetricCipherName, keyBytes);
		}

		// Token: 0x06003999 RID: 14745 RVA: 0x00135198 File Offset: 0x00135198
		public static KeyParameter MakeRandomKey(SymmetricKeyAlgorithmTag algorithm, SecureRandom random)
		{
			int keySize = PgpUtilities.GetKeySize(algorithm);
			byte[] array = new byte[(keySize + 7) / 8];
			random.NextBytes(array);
			return PgpUtilities.MakeKey(algorithm, array);
		}

		// Token: 0x0600399A RID: 14746 RVA: 0x001351CC File Offset: 0x001351CC
		internal static byte[] EncodePassPhrase(char[] passPhrase, bool utf8)
		{
			if (passPhrase == null)
			{
				return null;
			}
			if (!utf8)
			{
				return Strings.ToByteArray(passPhrase);
			}
			return Encoding.UTF8.GetBytes(passPhrase);
		}

		// Token: 0x0600399B RID: 14747 RVA: 0x001351F0 File Offset: 0x001351F0
		public static KeyParameter MakeKeyFromPassPhrase(SymmetricKeyAlgorithmTag algorithm, S2k s2k, char[] passPhrase)
		{
			return PgpUtilities.DoMakeKeyFromPassPhrase(algorithm, s2k, PgpUtilities.EncodePassPhrase(passPhrase, false), true);
		}

		// Token: 0x0600399C RID: 14748 RVA: 0x00135204 File Offset: 0x00135204
		public static KeyParameter MakeKeyFromPassPhraseUtf8(SymmetricKeyAlgorithmTag algorithm, S2k s2k, char[] passPhrase)
		{
			return PgpUtilities.DoMakeKeyFromPassPhrase(algorithm, s2k, PgpUtilities.EncodePassPhrase(passPhrase, true), true);
		}

		// Token: 0x0600399D RID: 14749 RVA: 0x00135218 File Offset: 0x00135218
		public static KeyParameter MakeKeyFromPassPhraseRaw(SymmetricKeyAlgorithmTag algorithm, S2k s2k, byte[] rawPassPhrase)
		{
			return PgpUtilities.DoMakeKeyFromPassPhrase(algorithm, s2k, rawPassPhrase, false);
		}

		// Token: 0x0600399E RID: 14750 RVA: 0x00135224 File Offset: 0x00135224
		internal static KeyParameter DoMakeKeyFromPassPhrase(SymmetricKeyAlgorithmTag algorithm, S2k s2k, byte[] rawPassPhrase, bool clearPassPhrase)
		{
			int keySize = PgpUtilities.GetKeySize(algorithm);
			byte[] array = new byte[(keySize + 7) / 8];
			int i = 0;
			int num = 0;
			while (i < array.Length)
			{
				IDigest digest;
				if (s2k != null)
				{
					string digestName = PgpUtilities.GetDigestName(s2k.HashAlgorithm);
					try
					{
						digest = DigestUtilities.GetDigest(digestName);
					}
					catch (Exception exception)
					{
						throw new PgpException("can't find S2k digest", exception);
					}
					for (int num2 = 0; num2 != num; num2++)
					{
						digest.Update(0);
					}
					byte[] iv = s2k.GetIV();
					switch (s2k.Type)
					{
					case 0:
						digest.BlockUpdate(rawPassPhrase, 0, rawPassPhrase.Length);
						goto IL_1D4;
					case 1:
						digest.BlockUpdate(iv, 0, iv.Length);
						digest.BlockUpdate(rawPassPhrase, 0, rawPassPhrase.Length);
						goto IL_1D4;
					case 3:
					{
						long num3 = s2k.IterationCount;
						digest.BlockUpdate(iv, 0, iv.Length);
						digest.BlockUpdate(rawPassPhrase, 0, rawPassPhrase.Length);
						num3 -= (long)(iv.Length + rawPassPhrase.Length);
						while (num3 > 0L)
						{
							if (num3 < (long)iv.Length)
							{
								digest.BlockUpdate(iv, 0, (int)num3);
								break;
							}
							digest.BlockUpdate(iv, 0, iv.Length);
							num3 -= (long)iv.Length;
							if (num3 < (long)rawPassPhrase.Length)
							{
								digest.BlockUpdate(rawPassPhrase, 0, (int)num3);
								num3 = 0L;
							}
							else
							{
								digest.BlockUpdate(rawPassPhrase, 0, rawPassPhrase.Length);
								num3 -= (long)rawPassPhrase.Length;
							}
						}
						goto IL_1D4;
					}
					}
					throw new PgpException("unknown S2k type: " + s2k.Type);
				}
				try
				{
					digest = DigestUtilities.GetDigest("MD5");
					for (int num4 = 0; num4 != num; num4++)
					{
						digest.Update(0);
					}
					digest.BlockUpdate(rawPassPhrase, 0, rawPassPhrase.Length);
				}
				catch (Exception exception2)
				{
					throw new PgpException("can't find MD5 digest", exception2);
				}
				IL_1D4:
				byte[] array2 = DigestUtilities.DoFinal(digest);
				if (array2.Length > array.Length - i)
				{
					Array.Copy(array2, 0, array, i, array.Length - i);
				}
				else
				{
					Array.Copy(array2, 0, array, i, array2.Length);
				}
				i += array2.Length;
				num++;
			}
			if (clearPassPhrase && rawPassPhrase != null)
			{
				Array.Clear(rawPassPhrase, 0, rawPassPhrase.Length);
			}
			return PgpUtilities.MakeKey(algorithm, array);
		}

		// Token: 0x0600399F RID: 14751 RVA: 0x00135490 File Offset: 0x00135490
		public static void WriteFileToLiteralData(Stream output, char fileType, FileInfo file)
		{
			PgpLiteralDataGenerator pgpLiteralDataGenerator = new PgpLiteralDataGenerator();
			Stream pOut = pgpLiteralDataGenerator.Open(output, fileType, file.Name, file.Length, file.LastWriteTime);
			PgpUtilities.PipeFileContents(file, pOut, 32768);
		}

		// Token: 0x060039A0 RID: 14752 RVA: 0x001354D0 File Offset: 0x001354D0
		public static void WriteFileToLiteralData(Stream output, char fileType, FileInfo file, byte[] buffer)
		{
			PgpLiteralDataGenerator pgpLiteralDataGenerator = new PgpLiteralDataGenerator();
			Stream pOut = pgpLiteralDataGenerator.Open(output, fileType, file.Name, file.LastWriteTime, buffer);
			PgpUtilities.PipeFileContents(file, pOut, buffer.Length);
		}

		// Token: 0x060039A1 RID: 14753 RVA: 0x00135508 File Offset: 0x00135508
		private static void PipeFileContents(FileInfo file, Stream pOut, int bufSize)
		{
			FileStream fileStream = file.OpenRead();
			byte[] array = new byte[bufSize];
			try
			{
				int count;
				while ((count = fileStream.Read(array, 0, array.Length)) > 0)
				{
					pOut.Write(array, 0, count);
				}
			}
			finally
			{
				Array.Clear(array, 0, array.Length);
				Platform.Dispose(pOut);
				Platform.Dispose(fileStream);
			}
		}

		// Token: 0x060039A2 RID: 14754 RVA: 0x00135570 File Offset: 0x00135570
		private static bool IsPossiblyBase64(int ch)
		{
			return (ch >= 65 && ch <= 90) || (ch >= 97 && ch <= 122) || (ch >= 48 && ch <= 57) || ch == 43 || ch == 47 || ch == 13 || ch == 10;
		}

		// Token: 0x060039A3 RID: 14755 RVA: 0x001355D0 File Offset: 0x001355D0
		public static Stream GetDecoderStream(Stream inputStream)
		{
			if (!inputStream.CanSeek)
			{
				throw new ArgumentException("inputStream must be seek-able", "inputStream");
			}
			long position = inputStream.Position;
			int num = inputStream.ReadByte();
			if ((num & 128) != 0)
			{
				inputStream.Position = position;
				return inputStream;
			}
			if (!PgpUtilities.IsPossiblyBase64(num))
			{
				inputStream.Position = position;
				return new ArmoredInputStream(inputStream);
			}
			byte[] array = new byte[60];
			int num2 = 1;
			int num3 = 1;
			array[0] = (byte)num;
			while (num2 != 60 && (num = inputStream.ReadByte()) >= 0)
			{
				if (!PgpUtilities.IsPossiblyBase64(num))
				{
					inputStream.Position = position;
					return new ArmoredInputStream(inputStream);
				}
				if (num != 10 && num != 13)
				{
					array[num3++] = (byte)num;
				}
				num2++;
			}
			inputStream.Position = position;
			if (num2 < 4)
			{
				return new ArmoredInputStream(inputStream);
			}
			byte[] array2 = new byte[8];
			Array.Copy(array, 0, array2, 0, array2.Length);
			Stream result;
			try
			{
				byte[] array3 = Base64.Decode(array2);
				bool hasHeaders = (array3[0] & 128) == 0;
				result = new ArmoredInputStream(inputStream, hasHeaders);
			}
			catch (IOException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw new IOException(ex2.Message);
			}
			return result;
		}

		// Token: 0x060039A4 RID: 14756 RVA: 0x0013571C File Offset: 0x0013571C
		internal static IWrapper CreateWrapper(SymmetricKeyAlgorithmTag encAlgorithm)
		{
			switch (encAlgorithm)
			{
			case SymmetricKeyAlgorithmTag.Aes128:
			case SymmetricKeyAlgorithmTag.Aes192:
			case SymmetricKeyAlgorithmTag.Aes256:
				return WrapperUtilities.GetWrapper("AESWRAP");
			case SymmetricKeyAlgorithmTag.Camellia128:
			case SymmetricKeyAlgorithmTag.Camellia192:
			case SymmetricKeyAlgorithmTag.Camellia256:
				return WrapperUtilities.GetWrapper("CAMELLIAWRAP");
			}
			throw new PgpException("unknown wrap algorithm: " + encAlgorithm);
		}

		// Token: 0x060039A5 RID: 14757 RVA: 0x00135784 File Offset: 0x00135784
		internal static byte[] GenerateIV(int length, SecureRandom random)
		{
			byte[] array = new byte[length];
			random.NextBytes(array);
			return array;
		}

		// Token: 0x060039A6 RID: 14758 RVA: 0x001357A4 File Offset: 0x001357A4
		internal static S2k GenerateS2k(HashAlgorithmTag hashAlgorithm, int s2kCount, SecureRandom random)
		{
			byte[] iv = PgpUtilities.GenerateIV(8, random);
			return new S2k(hashAlgorithm, iv, s2kCount);
		}

		// Token: 0x04001E0E RID: 7694
		private const int ReadAhead = 60;
	}
}
