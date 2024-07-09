using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.IO;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x0200064C RID: 1612
	public class PgpEncryptedDataGenerator : IStreamGenerator
	{
		// Token: 0x06003805 RID: 14341 RVA: 0x0012D674 File Offset: 0x0012D674
		public PgpEncryptedDataGenerator(SymmetricKeyAlgorithmTag encAlgorithm)
		{
			this.defAlgorithm = encAlgorithm;
			this.rand = new SecureRandom();
		}

		// Token: 0x06003806 RID: 14342 RVA: 0x0012D69C File Offset: 0x0012D69C
		public PgpEncryptedDataGenerator(SymmetricKeyAlgorithmTag encAlgorithm, bool withIntegrityPacket)
		{
			this.defAlgorithm = encAlgorithm;
			this.withIntegrityPacket = withIntegrityPacket;
			this.rand = new SecureRandom();
		}

		// Token: 0x06003807 RID: 14343 RVA: 0x0012D6C8 File Offset: 0x0012D6C8
		public PgpEncryptedDataGenerator(SymmetricKeyAlgorithmTag encAlgorithm, SecureRandom rand)
		{
			this.defAlgorithm = encAlgorithm;
			this.rand = rand;
		}

		// Token: 0x06003808 RID: 14344 RVA: 0x0012D6EC File Offset: 0x0012D6EC
		public PgpEncryptedDataGenerator(SymmetricKeyAlgorithmTag encAlgorithm, bool withIntegrityPacket, SecureRandom rand)
		{
			this.defAlgorithm = encAlgorithm;
			this.rand = rand;
			this.withIntegrityPacket = withIntegrityPacket;
		}

		// Token: 0x06003809 RID: 14345 RVA: 0x0012D714 File Offset: 0x0012D714
		public PgpEncryptedDataGenerator(SymmetricKeyAlgorithmTag encAlgorithm, SecureRandom rand, bool oldFormat)
		{
			this.defAlgorithm = encAlgorithm;
			this.rand = rand;
			this.oldFormat = oldFormat;
		}

		// Token: 0x0600380A RID: 14346 RVA: 0x0012D73C File Offset: 0x0012D73C
		[Obsolete("Use version that takes an explicit s2kDigest parameter")]
		public void AddMethod(char[] passPhrase)
		{
			this.AddMethod(passPhrase, HashAlgorithmTag.Sha1);
		}

		// Token: 0x0600380B RID: 14347 RVA: 0x0012D748 File Offset: 0x0012D748
		public void AddMethod(char[] passPhrase, HashAlgorithmTag s2kDigest)
		{
			this.DoAddMethod(PgpUtilities.EncodePassPhrase(passPhrase, false), true, s2kDigest);
		}

		// Token: 0x0600380C RID: 14348 RVA: 0x0012D75C File Offset: 0x0012D75C
		public void AddMethodUtf8(char[] passPhrase, HashAlgorithmTag s2kDigest)
		{
			this.DoAddMethod(PgpUtilities.EncodePassPhrase(passPhrase, true), true, s2kDigest);
		}

		// Token: 0x0600380D RID: 14349 RVA: 0x0012D770 File Offset: 0x0012D770
		public void AddMethodRaw(byte[] rawPassPhrase, HashAlgorithmTag s2kDigest)
		{
			this.DoAddMethod(rawPassPhrase, false, s2kDigest);
		}

		// Token: 0x0600380E RID: 14350 RVA: 0x0012D77C File Offset: 0x0012D77C
		internal void DoAddMethod(byte[] rawPassPhrase, bool clearPassPhrase, HashAlgorithmTag s2kDigest)
		{
			S2k s2k = PgpUtilities.GenerateS2k(s2kDigest, 96, this.rand);
			this.methods.Add(new PgpEncryptedDataGenerator.PbeMethod(this.defAlgorithm, s2k, PgpUtilities.DoMakeKeyFromPassPhrase(this.defAlgorithm, s2k, rawPassPhrase, clearPassPhrase)));
		}

		// Token: 0x0600380F RID: 14351 RVA: 0x0012D7C4 File Offset: 0x0012D7C4
		public void AddMethod(PgpPublicKey key)
		{
			if (!key.IsEncryptionKey)
			{
				throw new ArgumentException("passed in key not an encryption key!");
			}
			this.methods.Add(new PgpEncryptedDataGenerator.PubMethod(key));
		}

		// Token: 0x06003810 RID: 14352 RVA: 0x0012D7F0 File Offset: 0x0012D7F0
		private void AddCheckSum(byte[] sessionInfo)
		{
			int num = 0;
			for (int i = 1; i < sessionInfo.Length - 2; i++)
			{
				num += (int)sessionInfo[i];
			}
			sessionInfo[sessionInfo.Length - 2] = (byte)(num >> 8);
			sessionInfo[sessionInfo.Length - 1] = (byte)num;
		}

		// Token: 0x06003811 RID: 14353 RVA: 0x0012D830 File Offset: 0x0012D830
		private byte[] CreateSessionInfo(SymmetricKeyAlgorithmTag algorithm, KeyParameter key)
		{
			byte[] key2 = key.GetKey();
			byte[] array = new byte[key2.Length + 3];
			array[0] = (byte)algorithm;
			key2.CopyTo(array, 1);
			this.AddCheckSum(array);
			return array;
		}

		// Token: 0x06003812 RID: 14354 RVA: 0x0012D868 File Offset: 0x0012D868
		private Stream Open(Stream outStr, long length, byte[] buffer)
		{
			if (this.cOut != null)
			{
				throw new InvalidOperationException("generator already in open state");
			}
			if (this.methods.Count == 0)
			{
				throw new InvalidOperationException("No encryption methods specified");
			}
			if (outStr == null)
			{
				throw new ArgumentNullException("outStr");
			}
			this.pOut = new BcpgOutputStream(outStr);
			KeyParameter keyParameter;
			if (this.methods.Count == 1)
			{
				if (this.methods[0] is PgpEncryptedDataGenerator.PbeMethod)
				{
					PgpEncryptedDataGenerator.PbeMethod pbeMethod = (PgpEncryptedDataGenerator.PbeMethod)this.methods[0];
					keyParameter = pbeMethod.GetKey();
				}
				else
				{
					keyParameter = PgpUtilities.MakeRandomKey(this.defAlgorithm, this.rand);
					byte[] si = this.CreateSessionInfo(this.defAlgorithm, keyParameter);
					PgpEncryptedDataGenerator.PubMethod pubMethod = (PgpEncryptedDataGenerator.PubMethod)this.methods[0];
					try
					{
						pubMethod.AddSessionInfo(si, this.rand);
					}
					catch (Exception exception)
					{
						throw new PgpException("exception encrypting session key", exception);
					}
				}
				this.pOut.WritePacket((ContainedPacket)this.methods[0]);
			}
			else
			{
				keyParameter = PgpUtilities.MakeRandomKey(this.defAlgorithm, this.rand);
				byte[] si2 = this.CreateSessionInfo(this.defAlgorithm, keyParameter);
				for (int num = 0; num != this.methods.Count; num++)
				{
					PgpEncryptedDataGenerator.EncMethod encMethod = (PgpEncryptedDataGenerator.EncMethod)this.methods[num];
					try
					{
						encMethod.AddSessionInfo(si2, this.rand);
					}
					catch (Exception exception2)
					{
						throw new PgpException("exception encrypting session key", exception2);
					}
					this.pOut.WritePacket(encMethod);
				}
			}
			string text = PgpUtilities.GetSymmetricCipherName(this.defAlgorithm);
			if (text == null)
			{
				throw new PgpException("null cipher specified");
			}
			Stream result;
			try
			{
				if (this.withIntegrityPacket)
				{
					text += "/CFB/NoPadding";
				}
				else
				{
					text += "/OpenPGPCFB/NoPadding";
				}
				this.c = CipherUtilities.GetCipher(text);
				byte[] iv = new byte[this.c.GetBlockSize()];
				this.c.Init(true, new ParametersWithRandom(new ParametersWithIV(keyParameter, iv), this.rand));
				if (buffer == null)
				{
					if (this.withIntegrityPacket)
					{
						this.pOut = new BcpgOutputStream(outStr, PacketTag.SymmetricEncryptedIntegrityProtected, length + (long)this.c.GetBlockSize() + 2L + 1L + 22L);
						this.pOut.WriteByte(1);
					}
					else
					{
						this.pOut = new BcpgOutputStream(outStr, PacketTag.SymmetricKeyEncrypted, length + (long)this.c.GetBlockSize() + 2L, this.oldFormat);
					}
				}
				else if (this.withIntegrityPacket)
				{
					this.pOut = new BcpgOutputStream(outStr, PacketTag.SymmetricEncryptedIntegrityProtected, buffer);
					this.pOut.WriteByte(1);
				}
				else
				{
					this.pOut = new BcpgOutputStream(outStr, PacketTag.SymmetricKeyEncrypted, buffer);
				}
				int blockSize = this.c.GetBlockSize();
				byte[] array = new byte[blockSize + 2];
				this.rand.NextBytes(array, 0, blockSize);
				Array.Copy(array, array.Length - 4, array, array.Length - 2, 2);
				Stream stream = this.cOut = new CipherStream(this.pOut, null, this.c);
				if (this.withIntegrityPacket)
				{
					string digestName = PgpUtilities.GetDigestName(HashAlgorithmTag.Sha1);
					IDigest digest = DigestUtilities.GetDigest(digestName);
					stream = (this.digestOut = new DigestStream(stream, null, digest));
				}
				stream.Write(array, 0, array.Length);
				result = new WrappedGeneratorStream(this, stream);
			}
			catch (Exception exception3)
			{
				throw new PgpException("Exception creating cipher", exception3);
			}
			return result;
		}

		// Token: 0x06003813 RID: 14355 RVA: 0x0012DC40 File Offset: 0x0012DC40
		public Stream Open(Stream outStr, long length)
		{
			return this.Open(outStr, length, null);
		}

		// Token: 0x06003814 RID: 14356 RVA: 0x0012DC4C File Offset: 0x0012DC4C
		public Stream Open(Stream outStr, byte[] buffer)
		{
			return this.Open(outStr, 0L, buffer);
		}

		// Token: 0x06003815 RID: 14357 RVA: 0x0012DC58 File Offset: 0x0012DC58
		public void Close()
		{
			if (this.cOut != null)
			{
				if (this.digestOut != null)
				{
					BcpgOutputStream bcpgOutputStream = new BcpgOutputStream(this.digestOut, PacketTag.ModificationDetectionCode, 20L);
					bcpgOutputStream.Flush();
					this.digestOut.Flush();
					byte[] array = DigestUtilities.DoFinal(this.digestOut.WriteDigest());
					this.cOut.Write(array, 0, array.Length);
				}
				this.cOut.Flush();
				try
				{
					this.pOut.Write(this.c.DoFinal());
					this.pOut.Finish();
				}
				catch (Exception ex)
				{
					throw new IOException(ex.Message, ex);
				}
				this.cOut = null;
				this.pOut = null;
			}
		}

		// Token: 0x04001DA2 RID: 7586
		private BcpgOutputStream pOut;

		// Token: 0x04001DA3 RID: 7587
		private CipherStream cOut;

		// Token: 0x04001DA4 RID: 7588
		private IBufferedCipher c;

		// Token: 0x04001DA5 RID: 7589
		private bool withIntegrityPacket;

		// Token: 0x04001DA6 RID: 7590
		private bool oldFormat;

		// Token: 0x04001DA7 RID: 7591
		private DigestStream digestOut;

		// Token: 0x04001DA8 RID: 7592
		private readonly IList methods = Platform.CreateArrayList();

		// Token: 0x04001DA9 RID: 7593
		private readonly SymmetricKeyAlgorithmTag defAlgorithm;

		// Token: 0x04001DAA RID: 7594
		private readonly SecureRandom rand;

		// Token: 0x02000E61 RID: 3681
		private abstract class EncMethod : ContainedPacket
		{
			// Token: 0x06008D52 RID: 36178
			public abstract void AddSessionInfo(byte[] si, SecureRandom random);

			// Token: 0x0400423E RID: 16958
			protected byte[] sessionInfo;

			// Token: 0x0400423F RID: 16959
			protected SymmetricKeyAlgorithmTag encAlgorithm;

			// Token: 0x04004240 RID: 16960
			protected KeyParameter key;
		}

		// Token: 0x02000E62 RID: 3682
		private class PbeMethod : PgpEncryptedDataGenerator.EncMethod
		{
			// Token: 0x06008D54 RID: 36180 RVA: 0x002A62A0 File Offset: 0x002A62A0
			internal PbeMethod(SymmetricKeyAlgorithmTag encAlgorithm, S2k s2k, KeyParameter key)
			{
				this.encAlgorithm = encAlgorithm;
				this.s2k = s2k;
				this.key = key;
			}

			// Token: 0x06008D55 RID: 36181 RVA: 0x002A62C0 File Offset: 0x002A62C0
			public KeyParameter GetKey()
			{
				return this.key;
			}

			// Token: 0x06008D56 RID: 36182 RVA: 0x002A62C8 File Offset: 0x002A62C8
			public override void AddSessionInfo(byte[] si, SecureRandom random)
			{
				string symmetricCipherName = PgpUtilities.GetSymmetricCipherName(this.encAlgorithm);
				IBufferedCipher cipher = CipherUtilities.GetCipher(symmetricCipherName + "/CFB/NoPadding");
				byte[] iv = new byte[cipher.GetBlockSize()];
				cipher.Init(true, new ParametersWithRandom(new ParametersWithIV(this.key, iv), random));
				this.sessionInfo = cipher.DoFinal(si, 0, si.Length - 2);
			}

			// Token: 0x06008D57 RID: 36183 RVA: 0x002A6330 File Offset: 0x002A6330
			public override void Encode(BcpgOutputStream pOut)
			{
				SymmetricKeyEncSessionPacket p = new SymmetricKeyEncSessionPacket(this.encAlgorithm, this.s2k, this.sessionInfo);
				pOut.WritePacket(p);
			}

			// Token: 0x04004241 RID: 16961
			private S2k s2k;
		}

		// Token: 0x02000E63 RID: 3683
		private class PubMethod : PgpEncryptedDataGenerator.EncMethod
		{
			// Token: 0x06008D58 RID: 36184 RVA: 0x002A6360 File Offset: 0x002A6360
			internal PubMethod(PgpPublicKey pubKey)
			{
				this.pubKey = pubKey;
			}

			// Token: 0x06008D59 RID: 36185 RVA: 0x002A6370 File Offset: 0x002A6370
			public override void AddSessionInfo(byte[] sessionInfo, SecureRandom random)
			{
				byte[] encryptedSessionInfo = this.EncryptSessionInfo(sessionInfo, random);
				this.data = this.ProcessSessionInfo(encryptedSessionInfo);
			}

			// Token: 0x06008D5A RID: 36186 RVA: 0x002A6398 File Offset: 0x002A6398
			private byte[] EncryptSessionInfo(byte[] sessionInfo, SecureRandom random)
			{
				if (this.pubKey.Algorithm != PublicKeyAlgorithmTag.EC)
				{
					PublicKeyAlgorithmTag algorithm = this.pubKey.Algorithm;
					IBufferedCipher cipher;
					switch (algorithm)
					{
					case PublicKeyAlgorithmTag.RsaGeneral:
					case PublicKeyAlgorithmTag.RsaEncrypt:
						cipher = CipherUtilities.GetCipher("RSA//PKCS1Padding");
						break;
					default:
						switch (algorithm)
						{
						case PublicKeyAlgorithmTag.ElGamalEncrypt:
						case PublicKeyAlgorithmTag.ElGamalGeneral:
							cipher = CipherUtilities.GetCipher("ElGamal/ECB/PKCS1Padding");
							goto IL_A6;
						case PublicKeyAlgorithmTag.Dsa:
							throw new PgpException("Can't use DSA for encryption.");
						case PublicKeyAlgorithmTag.ECDsa:
							throw new PgpException("Can't use ECDSA for encryption.");
						}
						throw new PgpException("unknown asymmetric algorithm: " + this.pubKey.Algorithm);
					}
					IL_A6:
					AsymmetricKeyParameter key = this.pubKey.GetKey();
					cipher.Init(true, new ParametersWithRandom(key, random));
					return cipher.DoFinal(sessionInfo);
				}
				ECDHPublicBcpgKey ecdhpublicBcpgKey = (ECDHPublicBcpgKey)this.pubKey.PublicKeyPacket.Key;
				IAsymmetricCipherKeyPairGenerator keyPairGenerator = GeneratorUtilities.GetKeyPairGenerator("ECDH");
				keyPairGenerator.Init(new ECKeyGenerationParameters(ecdhpublicBcpgKey.CurveOid, random));
				AsymmetricCipherKeyPair asymmetricCipherKeyPair = keyPairGenerator.GenerateKeyPair();
				ECPrivateKeyParameters ecprivateKeyParameters = (ECPrivateKeyParameters)asymmetricCipherKeyPair.Private;
				ECPublicKeyParameters ecpublicKeyParameters = (ECPublicKeyParameters)asymmetricCipherKeyPair.Public;
				ECPublicKeyParameters ecpublicKeyParameters2 = (ECPublicKeyParameters)this.pubKey.GetKey();
				ECPoint s = ecpublicKeyParameters2.Q.Multiply(ecprivateKeyParameters.D).Normalize();
				KeyParameter parameters = new KeyParameter(Rfc6637Utilities.CreateKey(this.pubKey.PublicKeyPacket, s));
				IWrapper wrapper = PgpUtilities.CreateWrapper(ecdhpublicBcpgKey.SymmetricKeyAlgorithm);
				wrapper.Init(true, new ParametersWithRandom(parameters, random));
				byte[] array = PgpPad.PadSessionData(sessionInfo);
				byte[] array2 = wrapper.Wrap(array, 0, array.Length);
				byte[] encoded = new MPInteger(new BigInteger(1, ecpublicKeyParameters.Q.GetEncoded(false))).GetEncoded();
				byte[] array3 = new byte[encoded.Length + 1 + array2.Length];
				Array.Copy(encoded, 0, array3, 0, encoded.Length);
				array3[encoded.Length] = (byte)array2.Length;
				Array.Copy(array2, 0, array3, encoded.Length + 1, array2.Length);
				return array3;
			}

			// Token: 0x06008D5B RID: 36187 RVA: 0x002A65A8 File Offset: 0x002A65A8
			private byte[][] ProcessSessionInfo(byte[] encryptedSessionInfo)
			{
				PublicKeyAlgorithmTag algorithm = this.pubKey.Algorithm;
				byte[][] result;
				switch (algorithm)
				{
				case PublicKeyAlgorithmTag.RsaGeneral:
				case PublicKeyAlgorithmTag.RsaEncrypt:
					result = new byte[][]
					{
						this.ConvertToEncodedMpi(encryptedSessionInfo)
					};
					break;
				default:
					switch (algorithm)
					{
					case PublicKeyAlgorithmTag.ElGamalEncrypt:
					case PublicKeyAlgorithmTag.ElGamalGeneral:
					{
						int num = encryptedSessionInfo.Length / 2;
						byte[] array = new byte[num];
						byte[] array2 = new byte[num];
						Array.Copy(encryptedSessionInfo, 0, array, 0, num);
						Array.Copy(encryptedSessionInfo, num, array2, 0, num);
						return new byte[][]
						{
							this.ConvertToEncodedMpi(array),
							this.ConvertToEncodedMpi(array2)
						};
					}
					case PublicKeyAlgorithmTag.EC:
						return new byte[][]
						{
							encryptedSessionInfo
						};
					}
					throw new PgpException("unknown asymmetric algorithm: " + this.pubKey.Algorithm);
				}
				return result;
			}

			// Token: 0x06008D5C RID: 36188 RVA: 0x002A66A4 File Offset: 0x002A66A4
			private byte[] ConvertToEncodedMpi(byte[] encryptedSessionInfo)
			{
				byte[] encoded;
				try
				{
					encoded = new MPInteger(new BigInteger(1, encryptedSessionInfo)).GetEncoded();
				}
				catch (IOException ex)
				{
					throw new PgpException("Invalid MPI encoding: " + ex.Message, ex);
				}
				return encoded;
			}

			// Token: 0x06008D5D RID: 36189 RVA: 0x002A66F4 File Offset: 0x002A66F4
			public override void Encode(BcpgOutputStream pOut)
			{
				PublicKeyEncSessionPacket p = new PublicKeyEncSessionPacket(this.pubKey.KeyId, this.pubKey.Algorithm, this.data);
				pOut.WritePacket(p);
			}

			// Token: 0x04004242 RID: 16962
			internal PgpPublicKey pubKey;

			// Token: 0x04004243 RID: 16963
			internal byte[][] data;
		}
	}
}
