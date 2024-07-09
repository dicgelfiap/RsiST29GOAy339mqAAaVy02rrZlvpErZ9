using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x02000661 RID: 1633
	public class PgpSecretKey
	{
		// Token: 0x060038C7 RID: 14535 RVA: 0x001315F4 File Offset: 0x001315F4
		internal PgpSecretKey(SecretKeyPacket secret, PgpPublicKey pub)
		{
			this.secret = secret;
			this.pub = pub;
		}

		// Token: 0x060038C8 RID: 14536 RVA: 0x0013160C File Offset: 0x0013160C
		internal PgpSecretKey(PgpPrivateKey privKey, PgpPublicKey pubKey, SymmetricKeyAlgorithmTag encAlgorithm, byte[] rawPassPhrase, bool clearPassPhrase, bool useSha1, SecureRandom rand, bool isMasterKey)
		{
			this.pub = pubKey;
			PublicKeyAlgorithmTag algorithm = pubKey.Algorithm;
			BcpgObject bcpgObject;
			switch (algorithm)
			{
			case PublicKeyAlgorithmTag.RsaGeneral:
			case PublicKeyAlgorithmTag.RsaEncrypt:
			case PublicKeyAlgorithmTag.RsaSign:
			{
				RsaPrivateCrtKeyParameters rsaPrivateCrtKeyParameters = (RsaPrivateCrtKeyParameters)privKey.Key;
				bcpgObject = new RsaSecretBcpgKey(rsaPrivateCrtKeyParameters.Exponent, rsaPrivateCrtKeyParameters.P, rsaPrivateCrtKeyParameters.Q);
				break;
			}
			default:
				switch (algorithm)
				{
				case PublicKeyAlgorithmTag.ElGamalEncrypt:
				case PublicKeyAlgorithmTag.ElGamalGeneral:
				{
					ElGamalPrivateKeyParameters elGamalPrivateKeyParameters = (ElGamalPrivateKeyParameters)privKey.Key;
					bcpgObject = new ElGamalSecretBcpgKey(elGamalPrivateKeyParameters.X);
					break;
				}
				case PublicKeyAlgorithmTag.Dsa:
				{
					DsaPrivateKeyParameters dsaPrivateKeyParameters = (DsaPrivateKeyParameters)privKey.Key;
					bcpgObject = new DsaSecretBcpgKey(dsaPrivateKeyParameters.X);
					break;
				}
				case PublicKeyAlgorithmTag.EC:
				case PublicKeyAlgorithmTag.ECDsa:
				{
					ECPrivateKeyParameters ecprivateKeyParameters = (ECPrivateKeyParameters)privKey.Key;
					bcpgObject = new ECSecretBcpgKey(ecprivateKeyParameters.D);
					break;
				}
				default:
					throw new PgpException("unknown key class");
				}
				break;
			}
			try
			{
				MemoryStream memoryStream = new MemoryStream();
				BcpgOutputStream bcpgOutputStream = new BcpgOutputStream(memoryStream);
				bcpgOutputStream.WriteObject(bcpgObject);
				byte[] array = memoryStream.ToArray();
				byte[] b = PgpSecretKey.Checksum(useSha1, array, array.Length);
				array = Arrays.Concatenate(array, b);
				if (encAlgorithm == SymmetricKeyAlgorithmTag.Null)
				{
					if (isMasterKey)
					{
						this.secret = new SecretKeyPacket(this.pub.publicPk, encAlgorithm, null, null, array);
					}
					else
					{
						this.secret = new SecretSubkeyPacket(this.pub.publicPk, encAlgorithm, null, null, array);
					}
				}
				else
				{
					S2k s2k;
					byte[] iv;
					byte[] secKeyData;
					if (this.pub.Version >= 4)
					{
						secKeyData = PgpSecretKey.EncryptKeyDataV4(array, encAlgorithm, HashAlgorithmTag.Sha1, rawPassPhrase, clearPassPhrase, rand, out s2k, out iv);
					}
					else
					{
						secKeyData = PgpSecretKey.EncryptKeyDataV3(array, encAlgorithm, rawPassPhrase, clearPassPhrase, rand, out s2k, out iv);
					}
					int s2kUsage = useSha1 ? 254 : 255;
					if (isMasterKey)
					{
						this.secret = new SecretKeyPacket(this.pub.publicPk, encAlgorithm, s2kUsage, s2k, iv, secKeyData);
					}
					else
					{
						this.secret = new SecretSubkeyPacket(this.pub.publicPk, encAlgorithm, s2kUsage, s2k, iv, secKeyData);
					}
				}
			}
			catch (PgpException ex)
			{
				throw ex;
			}
			catch (Exception exception)
			{
				throw new PgpException("Exception encrypting key", exception);
			}
		}

		// Token: 0x060038C9 RID: 14537 RVA: 0x00131868 File Offset: 0x00131868
		[Obsolete("Use the constructor taking an explicit 'useSha1' parameter instead")]
		public PgpSecretKey(int certificationLevel, PgpKeyPair keyPair, string id, SymmetricKeyAlgorithmTag encAlgorithm, char[] passPhrase, PgpSignatureSubpacketVector hashedPackets, PgpSignatureSubpacketVector unhashedPackets, SecureRandom rand) : this(certificationLevel, keyPair, id, encAlgorithm, passPhrase, false, hashedPackets, unhashedPackets, rand)
		{
		}

		// Token: 0x060038CA RID: 14538 RVA: 0x00131890 File Offset: 0x00131890
		public PgpSecretKey(int certificationLevel, PgpKeyPair keyPair, string id, SymmetricKeyAlgorithmTag encAlgorithm, char[] passPhrase, bool useSha1, PgpSignatureSubpacketVector hashedPackets, PgpSignatureSubpacketVector unhashedPackets, SecureRandom rand) : this(certificationLevel, keyPair, id, encAlgorithm, false, passPhrase, useSha1, hashedPackets, unhashedPackets, rand)
		{
		}

		// Token: 0x060038CB RID: 14539 RVA: 0x001318B8 File Offset: 0x001318B8
		public PgpSecretKey(int certificationLevel, PgpKeyPair keyPair, string id, SymmetricKeyAlgorithmTag encAlgorithm, bool utf8PassPhrase, char[] passPhrase, bool useSha1, PgpSignatureSubpacketVector hashedPackets, PgpSignatureSubpacketVector unhashedPackets, SecureRandom rand) : this(certificationLevel, keyPair, id, encAlgorithm, PgpUtilities.EncodePassPhrase(passPhrase, utf8PassPhrase), true, useSha1, hashedPackets, unhashedPackets, rand)
		{
		}

		// Token: 0x060038CC RID: 14540 RVA: 0x001318E8 File Offset: 0x001318E8
		public PgpSecretKey(int certificationLevel, PgpKeyPair keyPair, string id, SymmetricKeyAlgorithmTag encAlgorithm, byte[] rawPassPhrase, bool useSha1, PgpSignatureSubpacketVector hashedPackets, PgpSignatureSubpacketVector unhashedPackets, SecureRandom rand) : this(certificationLevel, keyPair, id, encAlgorithm, rawPassPhrase, false, useSha1, hashedPackets, unhashedPackets, rand)
		{
		}

		// Token: 0x060038CD RID: 14541 RVA: 0x00131910 File Offset: 0x00131910
		internal PgpSecretKey(int certificationLevel, PgpKeyPair keyPair, string id, SymmetricKeyAlgorithmTag encAlgorithm, byte[] rawPassPhrase, bool clearPassPhrase, bool useSha1, PgpSignatureSubpacketVector hashedPackets, PgpSignatureSubpacketVector unhashedPackets, SecureRandom rand) : this(keyPair.PrivateKey, PgpSecretKey.CertifiedPublicKey(certificationLevel, keyPair, id, hashedPackets, unhashedPackets), encAlgorithm, rawPassPhrase, clearPassPhrase, useSha1, rand, true)
		{
		}

		// Token: 0x060038CE RID: 14542 RVA: 0x00131944 File Offset: 0x00131944
		public PgpSecretKey(int certificationLevel, PgpKeyPair keyPair, string id, SymmetricKeyAlgorithmTag encAlgorithm, HashAlgorithmTag hashAlgorithm, char[] passPhrase, bool useSha1, PgpSignatureSubpacketVector hashedPackets, PgpSignatureSubpacketVector unhashedPackets, SecureRandom rand) : this(certificationLevel, keyPair, id, encAlgorithm, hashAlgorithm, false, passPhrase, useSha1, hashedPackets, unhashedPackets, rand)
		{
		}

		// Token: 0x060038CF RID: 14543 RVA: 0x00131970 File Offset: 0x00131970
		public PgpSecretKey(int certificationLevel, PgpKeyPair keyPair, string id, SymmetricKeyAlgorithmTag encAlgorithm, HashAlgorithmTag hashAlgorithm, bool utf8PassPhrase, char[] passPhrase, bool useSha1, PgpSignatureSubpacketVector hashedPackets, PgpSignatureSubpacketVector unhashedPackets, SecureRandom rand) : this(certificationLevel, keyPair, id, encAlgorithm, hashAlgorithm, PgpUtilities.EncodePassPhrase(passPhrase, utf8PassPhrase), true, useSha1, hashedPackets, unhashedPackets, rand)
		{
		}

		// Token: 0x060038D0 RID: 14544 RVA: 0x001319A0 File Offset: 0x001319A0
		public PgpSecretKey(int certificationLevel, PgpKeyPair keyPair, string id, SymmetricKeyAlgorithmTag encAlgorithm, HashAlgorithmTag hashAlgorithm, byte[] rawPassPhrase, bool useSha1, PgpSignatureSubpacketVector hashedPackets, PgpSignatureSubpacketVector unhashedPackets, SecureRandom rand) : this(certificationLevel, keyPair, id, encAlgorithm, hashAlgorithm, rawPassPhrase, false, useSha1, hashedPackets, unhashedPackets, rand)
		{
		}

		// Token: 0x060038D1 RID: 14545 RVA: 0x001319CC File Offset: 0x001319CC
		internal PgpSecretKey(int certificationLevel, PgpKeyPair keyPair, string id, SymmetricKeyAlgorithmTag encAlgorithm, HashAlgorithmTag hashAlgorithm, byte[] rawPassPhrase, bool clearPassPhrase, bool useSha1, PgpSignatureSubpacketVector hashedPackets, PgpSignatureSubpacketVector unhashedPackets, SecureRandom rand) : this(keyPair.PrivateKey, PgpSecretKey.CertifiedPublicKey(certificationLevel, keyPair, id, hashedPackets, unhashedPackets, hashAlgorithm), encAlgorithm, rawPassPhrase, clearPassPhrase, useSha1, rand, true)
		{
		}

		// Token: 0x060038D2 RID: 14546 RVA: 0x00131A04 File Offset: 0x00131A04
		private static PgpPublicKey CertifiedPublicKey(int certificationLevel, PgpKeyPair keyPair, string id, PgpSignatureSubpacketVector hashedPackets, PgpSignatureSubpacketVector unhashedPackets)
		{
			PgpSignatureGenerator pgpSignatureGenerator;
			try
			{
				pgpSignatureGenerator = new PgpSignatureGenerator(keyPair.PublicKey.Algorithm, HashAlgorithmTag.Sha1);
			}
			catch (Exception ex)
			{
				throw new PgpException("Creating signature generator: " + ex.Message, ex);
			}
			pgpSignatureGenerator.InitSign(certificationLevel, keyPair.PrivateKey);
			pgpSignatureGenerator.SetHashedSubpackets(hashedPackets);
			pgpSignatureGenerator.SetUnhashedSubpackets(unhashedPackets);
			PgpPublicKey result;
			try
			{
				PgpSignature certification = pgpSignatureGenerator.GenerateCertification(id, keyPair.PublicKey);
				result = PgpPublicKey.AddCertification(keyPair.PublicKey, id, certification);
			}
			catch (Exception ex2)
			{
				throw new PgpException("Exception doing certification: " + ex2.Message, ex2);
			}
			return result;
		}

		// Token: 0x060038D3 RID: 14547 RVA: 0x00131AB8 File Offset: 0x00131AB8
		private static PgpPublicKey CertifiedPublicKey(int certificationLevel, PgpKeyPair keyPair, string id, PgpSignatureSubpacketVector hashedPackets, PgpSignatureSubpacketVector unhashedPackets, HashAlgorithmTag hashAlgorithm)
		{
			PgpSignatureGenerator pgpSignatureGenerator;
			try
			{
				pgpSignatureGenerator = new PgpSignatureGenerator(keyPair.PublicKey.Algorithm, hashAlgorithm);
			}
			catch (Exception ex)
			{
				throw new PgpException("Creating signature generator: " + ex.Message, ex);
			}
			pgpSignatureGenerator.InitSign(certificationLevel, keyPair.PrivateKey);
			pgpSignatureGenerator.SetHashedSubpackets(hashedPackets);
			pgpSignatureGenerator.SetUnhashedSubpackets(unhashedPackets);
			PgpPublicKey result;
			try
			{
				PgpSignature certification = pgpSignatureGenerator.GenerateCertification(id, keyPair.PublicKey);
				result = PgpPublicKey.AddCertification(keyPair.PublicKey, id, certification);
			}
			catch (Exception ex2)
			{
				throw new PgpException("Exception doing certification: " + ex2.Message, ex2);
			}
			return result;
		}

		// Token: 0x060038D4 RID: 14548 RVA: 0x00131B6C File Offset: 0x00131B6C
		public PgpSecretKey(int certificationLevel, PublicKeyAlgorithmTag algorithm, AsymmetricKeyParameter pubKey, AsymmetricKeyParameter privKey, DateTime time, string id, SymmetricKeyAlgorithmTag encAlgorithm, char[] passPhrase, PgpSignatureSubpacketVector hashedPackets, PgpSignatureSubpacketVector unhashedPackets, SecureRandom rand) : this(certificationLevel, new PgpKeyPair(algorithm, pubKey, privKey, time), id, encAlgorithm, passPhrase, false, hashedPackets, unhashedPackets, rand)
		{
		}

		// Token: 0x060038D5 RID: 14549 RVA: 0x00131B9C File Offset: 0x00131B9C
		public PgpSecretKey(int certificationLevel, PublicKeyAlgorithmTag algorithm, AsymmetricKeyParameter pubKey, AsymmetricKeyParameter privKey, DateTime time, string id, SymmetricKeyAlgorithmTag encAlgorithm, char[] passPhrase, bool useSha1, PgpSignatureSubpacketVector hashedPackets, PgpSignatureSubpacketVector unhashedPackets, SecureRandom rand) : this(certificationLevel, new PgpKeyPair(algorithm, pubKey, privKey, time), id, encAlgorithm, passPhrase, useSha1, hashedPackets, unhashedPackets, rand)
		{
		}

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x060038D6 RID: 14550 RVA: 0x00131BD0 File Offset: 0x00131BD0
		public bool IsSigningKey
		{
			get
			{
				PublicKeyAlgorithmTag algorithm = this.pub.Algorithm;
				switch (algorithm)
				{
				case PublicKeyAlgorithmTag.RsaGeneral:
				case PublicKeyAlgorithmTag.RsaSign:
					break;
				case PublicKeyAlgorithmTag.RsaEncrypt:
					return false;
				default:
					switch (algorithm)
					{
					case PublicKeyAlgorithmTag.Dsa:
					case PublicKeyAlgorithmTag.ECDsa:
					case PublicKeyAlgorithmTag.ElGamalGeneral:
						break;
					case PublicKeyAlgorithmTag.EC:
						return false;
					default:
						return false;
					}
					break;
				}
				return true;
			}
		}

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x060038D7 RID: 14551 RVA: 0x00131C24 File Offset: 0x00131C24
		public bool IsMasterKey
		{
			get
			{
				return this.pub.IsMasterKey;
			}
		}

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x060038D8 RID: 14552 RVA: 0x00131C34 File Offset: 0x00131C34
		public bool IsPrivateKeyEmpty
		{
			get
			{
				byte[] secretKeyData = this.secret.GetSecretKeyData();
				return secretKeyData == null || secretKeyData.Length < 1;
			}
		}

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x060038D9 RID: 14553 RVA: 0x00131C60 File Offset: 0x00131C60
		public SymmetricKeyAlgorithmTag KeyEncryptionAlgorithm
		{
			get
			{
				return this.secret.EncAlgorithm;
			}
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x060038DA RID: 14554 RVA: 0x00131C70 File Offset: 0x00131C70
		public long KeyId
		{
			get
			{
				return this.pub.KeyId;
			}
		}

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x060038DB RID: 14555 RVA: 0x00131C80 File Offset: 0x00131C80
		public int S2kUsage
		{
			get
			{
				return this.secret.S2kUsage;
			}
		}

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x060038DC RID: 14556 RVA: 0x00131C90 File Offset: 0x00131C90
		public S2k S2k
		{
			get
			{
				return this.secret.S2k;
			}
		}

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x060038DD RID: 14557 RVA: 0x00131CA0 File Offset: 0x00131CA0
		public PgpPublicKey PublicKey
		{
			get
			{
				return this.pub;
			}
		}

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x060038DE RID: 14558 RVA: 0x00131CA8 File Offset: 0x00131CA8
		public IEnumerable UserIds
		{
			get
			{
				return this.pub.GetUserIds();
			}
		}

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x060038DF RID: 14559 RVA: 0x00131CB8 File Offset: 0x00131CB8
		public IEnumerable UserAttributes
		{
			get
			{
				return this.pub.GetUserAttributes();
			}
		}

		// Token: 0x060038E0 RID: 14560 RVA: 0x00131CC8 File Offset: 0x00131CC8
		private byte[] ExtractKeyData(byte[] rawPassPhrase, bool clearPassPhrase)
		{
			SymmetricKeyAlgorithmTag encAlgorithm = this.secret.EncAlgorithm;
			byte[] secretKeyData = this.secret.GetSecretKeyData();
			if (encAlgorithm == SymmetricKeyAlgorithmTag.Null)
			{
				return secretKeyData;
			}
			byte[] result;
			try
			{
				KeyParameter key = PgpUtilities.DoMakeKeyFromPassPhrase(this.secret.EncAlgorithm, this.secret.S2k, rawPassPhrase, clearPassPhrase);
				byte[] array = this.secret.GetIV();
				byte[] array2;
				if (this.secret.PublicKeyPacket.Version >= 4)
				{
					array2 = PgpSecretKey.RecoverKeyData(encAlgorithm, "/CFB/NoPadding", key, array, secretKeyData, 0, secretKeyData.Length);
					bool flag = this.secret.S2kUsage == 254;
					byte[] array3 = PgpSecretKey.Checksum(flag, array2, flag ? (array2.Length - 20) : (array2.Length - 2));
					for (int num = 0; num != array3.Length; num++)
					{
						if (array3[num] != array2[array2.Length - array3.Length + num])
						{
							throw new PgpException(string.Concat(new object[]
							{
								"Checksum mismatch at ",
								num,
								" of ",
								array3.Length
							}));
						}
					}
				}
				else
				{
					array2 = new byte[secretKeyData.Length];
					array = Arrays.Clone(array);
					int num2 = 0;
					for (int num3 = 0; num3 != 4; num3++)
					{
						int num4 = (((int)(secretKeyData[num2] & byte.MaxValue) << 8 | (int)(secretKeyData[num2 + 1] & byte.MaxValue)) + 7) / 8;
						array2[num2] = secretKeyData[num2];
						array2[num2 + 1] = secretKeyData[num2 + 1];
						num2 += 2;
						if (num4 > secretKeyData.Length - num2)
						{
							throw new PgpException("out of range encLen found in encData");
						}
						byte[] sourceArray = PgpSecretKey.RecoverKeyData(encAlgorithm, "/CFB/NoPadding", key, array, secretKeyData, num2, num4);
						Array.Copy(sourceArray, 0, array2, num2, num4);
						num2 += num4;
						if (num3 != 3)
						{
							Array.Copy(secretKeyData, num2 - array.Length, array, 0, array.Length);
						}
					}
					array2[num2] = secretKeyData[num2];
					array2[num2 + 1] = secretKeyData[num2 + 1];
					int num5 = ((int)secretKeyData[num2] << 8 & 65280) | (int)(secretKeyData[num2 + 1] & byte.MaxValue);
					int num6 = 0;
					for (int i = 0; i < num2; i++)
					{
						num6 += (int)(array2[i] & byte.MaxValue);
					}
					num6 &= 65535;
					if (num6 != num5)
					{
						throw new PgpException("Checksum mismatch: passphrase wrong, expected " + num5.ToString("X") + " found " + num6.ToString("X"));
					}
				}
				result = array2;
			}
			catch (PgpException ex)
			{
				throw ex;
			}
			catch (Exception exception)
			{
				throw new PgpException("Exception decrypting key", exception);
			}
			return result;
		}

		// Token: 0x060038E1 RID: 14561 RVA: 0x00131FA0 File Offset: 0x00131FA0
		private static byte[] RecoverKeyData(SymmetricKeyAlgorithmTag encAlgorithm, string modeAndPadding, KeyParameter key, byte[] iv, byte[] keyData, int keyOff, int keyLen)
		{
			IBufferedCipher cipher;
			try
			{
				string symmetricCipherName = PgpUtilities.GetSymmetricCipherName(encAlgorithm);
				cipher = CipherUtilities.GetCipher(symmetricCipherName + modeAndPadding);
			}
			catch (Exception exception)
			{
				throw new PgpException("Exception creating cipher", exception);
			}
			cipher.Init(false, new ParametersWithIV(key, iv));
			return cipher.DoFinal(keyData, keyOff, keyLen);
		}

		// Token: 0x060038E2 RID: 14562 RVA: 0x00132000 File Offset: 0x00132000
		public PgpPrivateKey ExtractPrivateKey(char[] passPhrase)
		{
			return this.DoExtractPrivateKey(PgpUtilities.EncodePassPhrase(passPhrase, false), true);
		}

		// Token: 0x060038E3 RID: 14563 RVA: 0x00132010 File Offset: 0x00132010
		public PgpPrivateKey ExtractPrivateKeyUtf8(char[] passPhrase)
		{
			return this.DoExtractPrivateKey(PgpUtilities.EncodePassPhrase(passPhrase, true), true);
		}

		// Token: 0x060038E4 RID: 14564 RVA: 0x00132020 File Offset: 0x00132020
		public PgpPrivateKey ExtractPrivateKeyRaw(byte[] rawPassPhrase)
		{
			return this.DoExtractPrivateKey(rawPassPhrase, false);
		}

		// Token: 0x060038E5 RID: 14565 RVA: 0x0013202C File Offset: 0x0013202C
		internal PgpPrivateKey DoExtractPrivateKey(byte[] rawPassPhrase, bool clearPassPhrase)
		{
			if (this.IsPrivateKeyEmpty)
			{
				return null;
			}
			PublicKeyPacket publicKeyPacket = this.secret.PublicKeyPacket;
			PgpPrivateKey result;
			try
			{
				byte[] buffer = this.ExtractKeyData(rawPassPhrase, clearPassPhrase);
				BcpgInputStream bcpgIn = BcpgInputStream.Wrap(new MemoryStream(buffer, false));
				PublicKeyAlgorithmTag algorithm = publicKeyPacket.Algorithm;
				AsymmetricKeyParameter privateKey;
				switch (algorithm)
				{
				case PublicKeyAlgorithmTag.RsaGeneral:
				case PublicKeyAlgorithmTag.RsaEncrypt:
				case PublicKeyAlgorithmTag.RsaSign:
				{
					RsaPublicBcpgKey rsaPublicBcpgKey = (RsaPublicBcpgKey)publicKeyPacket.Key;
					RsaSecretBcpgKey rsaSecretBcpgKey = new RsaSecretBcpgKey(bcpgIn);
					RsaPrivateCrtKeyParameters rsaPrivateCrtKeyParameters = new RsaPrivateCrtKeyParameters(rsaSecretBcpgKey.Modulus, rsaPublicBcpgKey.PublicExponent, rsaSecretBcpgKey.PrivateExponent, rsaSecretBcpgKey.PrimeP, rsaSecretBcpgKey.PrimeQ, rsaSecretBcpgKey.PrimeExponentP, rsaSecretBcpgKey.PrimeExponentQ, rsaSecretBcpgKey.CrtCoefficient);
					privateKey = rsaPrivateCrtKeyParameters;
					break;
				}
				default:
					switch (algorithm)
					{
					case PublicKeyAlgorithmTag.ElGamalEncrypt:
					case PublicKeyAlgorithmTag.ElGamalGeneral:
					{
						ElGamalPublicBcpgKey elGamalPublicBcpgKey = (ElGamalPublicBcpgKey)publicKeyPacket.Key;
						ElGamalSecretBcpgKey elGamalSecretBcpgKey = new ElGamalSecretBcpgKey(bcpgIn);
						ElGamalParameters parameters = new ElGamalParameters(elGamalPublicBcpgKey.P, elGamalPublicBcpgKey.G);
						privateKey = new ElGamalPrivateKeyParameters(elGamalSecretBcpgKey.X, parameters);
						break;
					}
					case PublicKeyAlgorithmTag.Dsa:
					{
						DsaPublicBcpgKey dsaPublicBcpgKey = (DsaPublicBcpgKey)publicKeyPacket.Key;
						DsaSecretBcpgKey dsaSecretBcpgKey = new DsaSecretBcpgKey(bcpgIn);
						DsaParameters parameters2 = new DsaParameters(dsaPublicBcpgKey.P, dsaPublicBcpgKey.Q, dsaPublicBcpgKey.G);
						privateKey = new DsaPrivateKeyParameters(dsaSecretBcpgKey.X, parameters2);
						break;
					}
					case PublicKeyAlgorithmTag.EC:
						privateKey = this.GetECKey("ECDH", bcpgIn);
						break;
					case PublicKeyAlgorithmTag.ECDsa:
						privateKey = this.GetECKey("ECDSA", bcpgIn);
						break;
					default:
						throw new PgpException("unknown public key algorithm encountered");
					}
					break;
				}
				result = new PgpPrivateKey(this.KeyId, publicKeyPacket, privateKey);
			}
			catch (PgpException ex)
			{
				throw ex;
			}
			catch (Exception exception)
			{
				throw new PgpException("Exception constructing key", exception);
			}
			return result;
		}

		// Token: 0x060038E6 RID: 14566 RVA: 0x00132218 File Offset: 0x00132218
		private ECPrivateKeyParameters GetECKey(string algorithm, BcpgInputStream bcpgIn)
		{
			ECPublicBcpgKey ecpublicBcpgKey = (ECPublicBcpgKey)this.secret.PublicKeyPacket.Key;
			ECSecretBcpgKey ecsecretBcpgKey = new ECSecretBcpgKey(bcpgIn);
			return new ECPrivateKeyParameters(algorithm, ecsecretBcpgKey.X, ecpublicBcpgKey.CurveOid);
		}

		// Token: 0x060038E7 RID: 14567 RVA: 0x00132258 File Offset: 0x00132258
		private static byte[] Checksum(bool useSha1, byte[] bytes, int length)
		{
			if (useSha1)
			{
				try
				{
					IDigest digest = DigestUtilities.GetDigest("SHA1");
					digest.BlockUpdate(bytes, 0, length);
					return DigestUtilities.DoFinal(digest);
				}
				catch (Exception exception)
				{
					throw new PgpException("Can't find SHA-1", exception);
				}
			}
			int num = 0;
			for (int num2 = 0; num2 != length; num2++)
			{
				num += (int)bytes[num2];
			}
			return new byte[]
			{
				(byte)(num >> 8),
				(byte)num
			};
		}

		// Token: 0x060038E8 RID: 14568 RVA: 0x001322E0 File Offset: 0x001322E0
		public byte[] GetEncoded()
		{
			MemoryStream memoryStream = new MemoryStream();
			this.Encode(memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x060038E9 RID: 14569 RVA: 0x00132304 File Offset: 0x00132304
		public void Encode(Stream outStr)
		{
			BcpgOutputStream bcpgOutputStream = BcpgOutputStream.Wrap(outStr);
			bcpgOutputStream.WritePacket(this.secret);
			if (this.pub.trustPk != null)
			{
				bcpgOutputStream.WritePacket(this.pub.trustPk);
			}
			if (this.pub.subSigs == null)
			{
				foreach (object obj in this.pub.keySigs)
				{
					PgpSignature pgpSignature = (PgpSignature)obj;
					pgpSignature.Encode(bcpgOutputStream);
				}
				for (int num = 0; num != this.pub.ids.Count; num++)
				{
					object obj2 = this.pub.ids[num];
					if (obj2 is string)
					{
						string id = (string)obj2;
						bcpgOutputStream.WritePacket(new UserIdPacket(id));
					}
					else
					{
						PgpUserAttributeSubpacketVector pgpUserAttributeSubpacketVector = (PgpUserAttributeSubpacketVector)obj2;
						bcpgOutputStream.WritePacket(new UserAttributePacket(pgpUserAttributeSubpacketVector.ToSubpacketArray()));
					}
					if (this.pub.idTrusts[num] != null)
					{
						bcpgOutputStream.WritePacket((ContainedPacket)this.pub.idTrusts[num]);
					}
					foreach (object obj3 in ((IList)this.pub.idSigs[num]))
					{
						PgpSignature pgpSignature2 = (PgpSignature)obj3;
						pgpSignature2.Encode(bcpgOutputStream);
					}
				}
				return;
			}
			foreach (object obj4 in this.pub.subSigs)
			{
				PgpSignature pgpSignature3 = (PgpSignature)obj4;
				pgpSignature3.Encode(bcpgOutputStream);
			}
		}

		// Token: 0x060038EA RID: 14570 RVA: 0x0013251C File Offset: 0x0013251C
		public static PgpSecretKey CopyWithNewPassword(PgpSecretKey key, char[] oldPassPhrase, char[] newPassPhrase, SymmetricKeyAlgorithmTag newEncAlgorithm, SecureRandom rand)
		{
			return PgpSecretKey.DoCopyWithNewPassword(key, PgpUtilities.EncodePassPhrase(oldPassPhrase, false), PgpUtilities.EncodePassPhrase(newPassPhrase, false), true, newEncAlgorithm, rand);
		}

		// Token: 0x060038EB RID: 14571 RVA: 0x00132538 File Offset: 0x00132538
		public static PgpSecretKey CopyWithNewPasswordUtf8(PgpSecretKey key, char[] oldPassPhrase, char[] newPassPhrase, SymmetricKeyAlgorithmTag newEncAlgorithm, SecureRandom rand)
		{
			return PgpSecretKey.DoCopyWithNewPassword(key, PgpUtilities.EncodePassPhrase(oldPassPhrase, true), PgpUtilities.EncodePassPhrase(newPassPhrase, true), true, newEncAlgorithm, rand);
		}

		// Token: 0x060038EC RID: 14572 RVA: 0x00132554 File Offset: 0x00132554
		public static PgpSecretKey CopyWithNewPasswordRaw(PgpSecretKey key, byte[] rawOldPassPhrase, byte[] rawNewPassPhrase, SymmetricKeyAlgorithmTag newEncAlgorithm, SecureRandom rand)
		{
			return PgpSecretKey.DoCopyWithNewPassword(key, rawOldPassPhrase, rawNewPassPhrase, false, newEncAlgorithm, rand);
		}

		// Token: 0x060038ED RID: 14573 RVA: 0x00132564 File Offset: 0x00132564
		internal static PgpSecretKey DoCopyWithNewPassword(PgpSecretKey key, byte[] rawOldPassPhrase, byte[] rawNewPassPhrase, bool clearPassPhrase, SymmetricKeyAlgorithmTag newEncAlgorithm, SecureRandom rand)
		{
			if (key.IsPrivateKeyEmpty)
			{
				throw new PgpException("no private key in this SecretKey - public key present only.");
			}
			byte[] array = key.ExtractKeyData(rawOldPassPhrase, clearPassPhrase);
			int num = key.secret.S2kUsage;
			byte[] iv = null;
			S2k s2k = null;
			PublicKeyPacket publicKeyPacket = key.secret.PublicKeyPacket;
			byte[] array2;
			if (newEncAlgorithm == SymmetricKeyAlgorithmTag.Null)
			{
				num = 0;
				if (key.secret.S2kUsage == 254)
				{
					array2 = new byte[array.Length - 18];
					Array.Copy(array, 0, array2, 0, array2.Length - 2);
					byte[] array3 = PgpSecretKey.Checksum(false, array2, array2.Length - 2);
					array2[array2.Length - 2] = array3[0];
					array2[array2.Length - 1] = array3[1];
				}
				else
				{
					array2 = array;
				}
			}
			else
			{
				if (num == 0)
				{
					num = 255;
				}
				try
				{
					if (publicKeyPacket.Version >= 4)
					{
						array2 = PgpSecretKey.EncryptKeyDataV4(array, newEncAlgorithm, HashAlgorithmTag.Sha1, rawNewPassPhrase, clearPassPhrase, rand, out s2k, out iv);
					}
					else
					{
						array2 = PgpSecretKey.EncryptKeyDataV3(array, newEncAlgorithm, rawNewPassPhrase, clearPassPhrase, rand, out s2k, out iv);
					}
				}
				catch (PgpException ex)
				{
					throw ex;
				}
				catch (Exception exception)
				{
					throw new PgpException("Exception encrypting key", exception);
				}
			}
			SecretKeyPacket secretKeyPacket;
			if (key.secret is SecretSubkeyPacket)
			{
				secretKeyPacket = new SecretSubkeyPacket(publicKeyPacket, newEncAlgorithm, num, s2k, iv, array2);
			}
			else
			{
				secretKeyPacket = new SecretKeyPacket(publicKeyPacket, newEncAlgorithm, num, s2k, iv, array2);
			}
			return new PgpSecretKey(secretKeyPacket, key.pub);
		}

		// Token: 0x060038EE RID: 14574 RVA: 0x001326DC File Offset: 0x001326DC
		public static PgpSecretKey ReplacePublicKey(PgpSecretKey secretKey, PgpPublicKey publicKey)
		{
			if (publicKey.KeyId != secretKey.KeyId)
			{
				throw new ArgumentException("KeyId's do not match");
			}
			return new PgpSecretKey(secretKey.secret, publicKey);
		}

		// Token: 0x060038EF RID: 14575 RVA: 0x00132708 File Offset: 0x00132708
		private static byte[] EncryptKeyDataV3(byte[] rawKeyData, SymmetricKeyAlgorithmTag encAlgorithm, byte[] rawPassPhrase, bool clearPassPhrase, SecureRandom random, out S2k s2k, out byte[] iv)
		{
			s2k = null;
			iv = null;
			KeyParameter key = PgpUtilities.DoMakeKeyFromPassPhrase(encAlgorithm, s2k, rawPassPhrase, clearPassPhrase);
			byte[] array = new byte[rawKeyData.Length];
			int num = 0;
			for (int num2 = 0; num2 != 4; num2++)
			{
				int num3 = (((int)(rawKeyData[num] & byte.MaxValue) << 8 | (int)(rawKeyData[num + 1] & byte.MaxValue)) + 7) / 8;
				array[num] = rawKeyData[num];
				array[num + 1] = rawKeyData[num + 1];
				if (num3 > rawKeyData.Length - (num + 2))
				{
					throw new PgpException("out of range encLen found in rawKeyData");
				}
				byte[] array2;
				if (num2 == 0)
				{
					array2 = PgpSecretKey.EncryptData(encAlgorithm, key, rawKeyData, num + 2, num3, random, ref iv);
				}
				else
				{
					byte[] array3 = Arrays.CopyOfRange(array, num - iv.Length, num);
					array2 = PgpSecretKey.EncryptData(encAlgorithm, key, rawKeyData, num + 2, num3, random, ref array3);
				}
				Array.Copy(array2, 0, array, num + 2, array2.Length);
				num += 2 + num3;
			}
			array[num] = rawKeyData[num];
			array[num + 1] = rawKeyData[num + 1];
			return array;
		}

		// Token: 0x060038F0 RID: 14576 RVA: 0x001327FC File Offset: 0x001327FC
		private static byte[] EncryptKeyDataV4(byte[] rawKeyData, SymmetricKeyAlgorithmTag encAlgorithm, HashAlgorithmTag hashAlgorithm, byte[] rawPassPhrase, bool clearPassPhrase, SecureRandom random, out S2k s2k, out byte[] iv)
		{
			s2k = PgpUtilities.GenerateS2k(hashAlgorithm, 96, random);
			KeyParameter key = PgpUtilities.DoMakeKeyFromPassPhrase(encAlgorithm, s2k, rawPassPhrase, clearPassPhrase);
			iv = null;
			return PgpSecretKey.EncryptData(encAlgorithm, key, rawKeyData, 0, rawKeyData.Length, random, ref iv);
		}

		// Token: 0x060038F1 RID: 14577 RVA: 0x0013283C File Offset: 0x0013283C
		private static byte[] EncryptData(SymmetricKeyAlgorithmTag encAlgorithm, KeyParameter key, byte[] data, int dataOff, int dataLen, SecureRandom random, ref byte[] iv)
		{
			IBufferedCipher cipher;
			try
			{
				string symmetricCipherName = PgpUtilities.GetSymmetricCipherName(encAlgorithm);
				cipher = CipherUtilities.GetCipher(symmetricCipherName + "/CFB/NoPadding");
			}
			catch (Exception exception)
			{
				throw new PgpException("Exception creating cipher", exception);
			}
			if (iv == null)
			{
				iv = PgpUtilities.GenerateIV(cipher.GetBlockSize(), random);
			}
			cipher.Init(true, new ParametersWithRandom(new ParametersWithIV(key, iv), random));
			return cipher.DoFinal(data, dataOff, dataLen);
		}

		// Token: 0x060038F2 RID: 14578 RVA: 0x001328BC File Offset: 0x001328BC
		public static PgpSecretKey ParseSecretKeyFromSExpr(Stream inputStream, char[] passPhrase, PgpPublicKey pubKey)
		{
			return PgpSecretKey.DoParseSecretKeyFromSExpr(inputStream, PgpUtilities.EncodePassPhrase(passPhrase, false), true, pubKey);
		}

		// Token: 0x060038F3 RID: 14579 RVA: 0x001328D0 File Offset: 0x001328D0
		public static PgpSecretKey ParseSecretKeyFromSExprUtf8(Stream inputStream, char[] passPhrase, PgpPublicKey pubKey)
		{
			return PgpSecretKey.DoParseSecretKeyFromSExpr(inputStream, PgpUtilities.EncodePassPhrase(passPhrase, true), true, pubKey);
		}

		// Token: 0x060038F4 RID: 14580 RVA: 0x001328E4 File Offset: 0x001328E4
		public static PgpSecretKey ParseSecretKeyFromSExprRaw(Stream inputStream, byte[] rawPassPhrase, PgpPublicKey pubKey)
		{
			return PgpSecretKey.DoParseSecretKeyFromSExpr(inputStream, rawPassPhrase, false, pubKey);
		}

		// Token: 0x060038F5 RID: 14581 RVA: 0x001328F0 File Offset: 0x001328F0
		internal static PgpSecretKey DoParseSecretKeyFromSExpr(Stream inputStream, byte[] rawPassPhrase, bool clearPassPhrase, PgpPublicKey pubKey)
		{
			SXprUtilities.SkipOpenParenthesis(inputStream);
			string text = SXprUtilities.ReadString(inputStream, inputStream.ReadByte());
			if (!text.Equals("protected-private-key"))
			{
				throw new PgpException("unknown key type found");
			}
			SXprUtilities.SkipOpenParenthesis(inputStream);
			string text2 = SXprUtilities.ReadString(inputStream, inputStream.ReadByte());
			if (!text2.Equals("ecc"))
			{
				throw new PgpException("no curve details found");
			}
			SXprUtilities.SkipOpenParenthesis(inputStream);
			SXprUtilities.ReadString(inputStream, inputStream.ReadByte());
			string curveName = SXprUtilities.ReadString(inputStream, inputStream.ReadByte());
			SXprUtilities.SkipCloseParenthesis(inputStream);
			SXprUtilities.SkipOpenParenthesis(inputStream);
			text = SXprUtilities.ReadString(inputStream, inputStream.ReadByte());
			if (text.Equals("q"))
			{
				SXprUtilities.ReadBytes(inputStream, inputStream.ReadByte());
				SXprUtilities.SkipCloseParenthesis(inputStream);
				byte[] dvalue = PgpSecretKey.GetDValue(inputStream, rawPassPhrase, clearPassPhrase, curveName);
				return new PgpSecretKey(new SecretKeyPacket(pubKey.PublicKeyPacket, SymmetricKeyAlgorithmTag.Null, null, null, new ECSecretBcpgKey(new BigInteger(1, dvalue)).GetEncoded()), pubKey);
			}
			throw new PgpException("no q value found");
		}

		// Token: 0x060038F6 RID: 14582 RVA: 0x00132A00 File Offset: 0x00132A00
		public static PgpSecretKey ParseSecretKeyFromSExpr(Stream inputStream, char[] passPhrase)
		{
			return PgpSecretKey.DoParseSecretKeyFromSExpr(inputStream, PgpUtilities.EncodePassPhrase(passPhrase, false), true);
		}

		// Token: 0x060038F7 RID: 14583 RVA: 0x00132A10 File Offset: 0x00132A10
		public static PgpSecretKey ParseSecretKeyFromSExprUtf8(Stream inputStream, char[] passPhrase)
		{
			return PgpSecretKey.DoParseSecretKeyFromSExpr(inputStream, PgpUtilities.EncodePassPhrase(passPhrase, true), true);
		}

		// Token: 0x060038F8 RID: 14584 RVA: 0x00132A20 File Offset: 0x00132A20
		public static PgpSecretKey ParseSecretKeyFromSExprRaw(Stream inputStream, byte[] rawPassPhrase)
		{
			return PgpSecretKey.DoParseSecretKeyFromSExpr(inputStream, rawPassPhrase, false);
		}

		// Token: 0x060038F9 RID: 14585 RVA: 0x00132A2C File Offset: 0x00132A2C
		internal static PgpSecretKey DoParseSecretKeyFromSExpr(Stream inputStream, byte[] rawPassPhrase, bool clearPassPhrase)
		{
			SXprUtilities.SkipOpenParenthesis(inputStream);
			string text = SXprUtilities.ReadString(inputStream, inputStream.ReadByte());
			if (!text.Equals("protected-private-key"))
			{
				throw new PgpException("unknown key type found");
			}
			SXprUtilities.SkipOpenParenthesis(inputStream);
			string text2 = SXprUtilities.ReadString(inputStream, inputStream.ReadByte());
			if (!text2.Equals("ecc"))
			{
				throw new PgpException("no curve details found");
			}
			SXprUtilities.SkipOpenParenthesis(inputStream);
			SXprUtilities.ReadString(inputStream, inputStream.ReadByte());
			string text3 = SXprUtilities.ReadString(inputStream, inputStream.ReadByte());
			if (Platform.StartsWith(text3, "NIST "))
			{
				text3 = text3.Substring("NIST ".Length);
			}
			SXprUtilities.SkipCloseParenthesis(inputStream);
			SXprUtilities.SkipOpenParenthesis(inputStream);
			text = SXprUtilities.ReadString(inputStream, inputStream.ReadByte());
			if (text.Equals("q"))
			{
				byte[] bytes = SXprUtilities.ReadBytes(inputStream, inputStream.ReadByte());
				PublicKeyPacket publicKeyPacket = new PublicKeyPacket(PublicKeyAlgorithmTag.ECDsa, DateTime.UtcNow, new ECDsaPublicBcpgKey(ECNamedCurveTable.GetOid(text3), new BigInteger(1, bytes)));
				SXprUtilities.SkipCloseParenthesis(inputStream);
				byte[] dvalue = PgpSecretKey.GetDValue(inputStream, rawPassPhrase, clearPassPhrase, text3);
				return new PgpSecretKey(new SecretKeyPacket(publicKeyPacket, SymmetricKeyAlgorithmTag.Null, null, null, new ECSecretBcpgKey(new BigInteger(1, dvalue)).GetEncoded()), new PgpPublicKey(publicKeyPacket));
			}
			throw new PgpException("no q value found");
		}

		// Token: 0x060038FA RID: 14586 RVA: 0x00132B80 File Offset: 0x00132B80
		private static byte[] GetDValue(Stream inputStream, byte[] rawPassPhrase, bool clearPassPhrase, string curveName)
		{
			SXprUtilities.SkipOpenParenthesis(inputStream);
			string text = SXprUtilities.ReadString(inputStream, inputStream.ReadByte());
			if (text.Equals("protected"))
			{
				SXprUtilities.ReadString(inputStream, inputStream.ReadByte());
				SXprUtilities.SkipOpenParenthesis(inputStream);
				S2k s2k = SXprUtilities.ParseS2k(inputStream);
				byte[] iv = SXprUtilities.ReadBytes(inputStream, inputStream.ReadByte());
				SXprUtilities.SkipCloseParenthesis(inputStream);
				byte[] array = SXprUtilities.ReadBytes(inputStream, inputStream.ReadByte());
				KeyParameter key = PgpUtilities.DoMakeKeyFromPassPhrase(SymmetricKeyAlgorithmTag.Aes128, s2k, rawPassPhrase, clearPassPhrase);
				byte[] buffer = PgpSecretKey.RecoverKeyData(SymmetricKeyAlgorithmTag.Aes128, "/CBC/NoPadding", key, iv, array, 0, array.Length);
				Stream stream = new MemoryStream(buffer, false);
				SXprUtilities.SkipOpenParenthesis(stream);
				SXprUtilities.SkipOpenParenthesis(stream);
				SXprUtilities.SkipOpenParenthesis(stream);
				SXprUtilities.ReadString(stream, stream.ReadByte());
				return SXprUtilities.ReadBytes(stream, stream.ReadByte());
			}
			throw new PgpException("protected block not found");
		}

		// Token: 0x04001DE6 RID: 7654
		private readonly SecretKeyPacket secret;

		// Token: 0x04001DE7 RID: 7655
		private readonly PgpPublicKey pub;
	}
}
