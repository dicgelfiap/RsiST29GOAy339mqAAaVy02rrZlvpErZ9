using System;
using System.IO;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.IO;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x0200065E RID: 1630
	public class PgpPublicKeyEncryptedData : PgpEncryptedData
	{
		// Token: 0x060038A2 RID: 14498 RVA: 0x001307A4 File Offset: 0x001307A4
		internal PgpPublicKeyEncryptedData(PublicKeyEncSessionPacket keyData, InputStreamPacket encData) : base(encData)
		{
			this.keyData = keyData;
		}

		// Token: 0x060038A3 RID: 14499 RVA: 0x001307B4 File Offset: 0x001307B4
		private static IBufferedCipher GetKeyCipher(PublicKeyAlgorithmTag algorithm)
		{
			IBufferedCipher cipher;
			try
			{
				switch (algorithm)
				{
				case PublicKeyAlgorithmTag.RsaGeneral:
				case PublicKeyAlgorithmTag.RsaEncrypt:
					cipher = CipherUtilities.GetCipher("RSA//PKCS1Padding");
					break;
				default:
					if (algorithm != PublicKeyAlgorithmTag.ElGamalEncrypt && algorithm != PublicKeyAlgorithmTag.ElGamalGeneral)
					{
						throw new PgpException("unknown asymmetric algorithm: " + algorithm);
					}
					cipher = CipherUtilities.GetCipher("ElGamal/ECB/PKCS1Padding");
					break;
				}
			}
			catch (PgpException ex)
			{
				throw ex;
			}
			catch (Exception exception)
			{
				throw new PgpException("Exception creating cipher", exception);
			}
			return cipher;
		}

		// Token: 0x060038A4 RID: 14500 RVA: 0x0013084C File Offset: 0x0013084C
		private bool ConfirmCheckSum(byte[] sessionInfo)
		{
			int num = 0;
			for (int num2 = 1; num2 != sessionInfo.Length - 2; num2++)
			{
				num += (int)(sessionInfo[num2] & byte.MaxValue);
			}
			return sessionInfo[sessionInfo.Length - 2] == (byte)(num >> 8) && sessionInfo[sessionInfo.Length - 1] == (byte)num;
		}

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x060038A5 RID: 14501 RVA: 0x0013089C File Offset: 0x0013089C
		public long KeyId
		{
			get
			{
				return this.keyData.KeyId;
			}
		}

		// Token: 0x060038A6 RID: 14502 RVA: 0x001308AC File Offset: 0x001308AC
		public SymmetricKeyAlgorithmTag GetSymmetricAlgorithm(PgpPrivateKey privKey)
		{
			byte[] array = this.RecoverSessionData(privKey);
			return (SymmetricKeyAlgorithmTag)array[0];
		}

		// Token: 0x060038A7 RID: 14503 RVA: 0x001308C8 File Offset: 0x001308C8
		public Stream GetDataStream(PgpPrivateKey privKey)
		{
			byte[] array = this.RecoverSessionData(privKey);
			if (!this.ConfirmCheckSum(array))
			{
				throw new PgpKeyValidationException("key checksum failed");
			}
			SymmetricKeyAlgorithmTag symmetricKeyAlgorithmTag = (SymmetricKeyAlgorithmTag)array[0];
			if (symmetricKeyAlgorithmTag == SymmetricKeyAlgorithmTag.Null)
			{
				return this.encData.GetInputStream();
			}
			string symmetricCipherName = PgpUtilities.GetSymmetricCipherName(symmetricKeyAlgorithmTag);
			string text = symmetricCipherName;
			IBufferedCipher cipher;
			try
			{
				if (this.encData is SymmetricEncIntegrityPacket)
				{
					text += "/CFB/NoPadding";
				}
				else
				{
					text += "/OpenPGPCFB/NoPadding";
				}
				cipher = CipherUtilities.GetCipher(text);
			}
			catch (PgpException ex)
			{
				throw ex;
			}
			catch (Exception exception)
			{
				throw new PgpException("exception creating cipher", exception);
			}
			Stream encStream;
			try
			{
				KeyParameter parameters = ParameterUtilities.CreateKeyParameter(symmetricCipherName, array, 1, array.Length - 3);
				byte[] array2 = new byte[cipher.GetBlockSize()];
				cipher.Init(false, new ParametersWithIV(parameters, array2));
				this.encStream = BcpgInputStream.Wrap(new CipherStream(this.encData.GetInputStream(), cipher, null));
				if (this.encData is SymmetricEncIntegrityPacket)
				{
					this.truncStream = new PgpEncryptedData.TruncatedStream(this.encStream);
					string digestName = PgpUtilities.GetDigestName(HashAlgorithmTag.Sha1);
					IDigest digest = DigestUtilities.GetDigest(digestName);
					this.encStream = new DigestStream(this.truncStream, digest, null);
				}
				if (Streams.ReadFully(this.encStream, array2, 0, array2.Length) < array2.Length)
				{
					throw new EndOfStreamException("unexpected end of stream.");
				}
				int num = this.encStream.ReadByte();
				int num2 = this.encStream.ReadByte();
				if (num < 0 || num2 < 0)
				{
					throw new EndOfStreamException("unexpected end of stream.");
				}
				encStream = this.encStream;
			}
			catch (PgpException ex2)
			{
				throw ex2;
			}
			catch (Exception exception2)
			{
				throw new PgpException("Exception starting decryption", exception2);
			}
			return encStream;
		}

		// Token: 0x060038A8 RID: 14504 RVA: 0x00130AA8 File Offset: 0x00130AA8
		private byte[] RecoverSessionData(PgpPrivateKey privKey)
		{
			byte[][] encSessionKey = this.keyData.GetEncSessionKey();
			if (this.keyData.Algorithm != PublicKeyAlgorithmTag.EC)
			{
				IBufferedCipher keyCipher = PgpPublicKeyEncryptedData.GetKeyCipher(this.keyData.Algorithm);
				try
				{
					keyCipher.Init(false, privKey.Key);
				}
				catch (InvalidKeyException exception)
				{
					throw new PgpException("error setting asymmetric cipher", exception);
				}
				if (this.keyData.Algorithm == PublicKeyAlgorithmTag.RsaEncrypt || this.keyData.Algorithm == PublicKeyAlgorithmTag.RsaGeneral)
				{
					byte[] array = encSessionKey[0];
					keyCipher.ProcessBytes(array, 2, array.Length - 2);
				}
				else
				{
					ElGamalPrivateKeyParameters elGamalPrivateKeyParameters = (ElGamalPrivateKeyParameters)privKey.Key;
					int size = (elGamalPrivateKeyParameters.Parameters.P.BitLength + 7) / 8;
					PgpPublicKeyEncryptedData.ProcessEncodedMpi(keyCipher, size, encSessionKey[0]);
					PgpPublicKeyEncryptedData.ProcessEncodedMpi(keyCipher, size, encSessionKey[1]);
				}
				byte[] result;
				try
				{
					result = keyCipher.DoFinal();
				}
				catch (Exception exception2)
				{
					throw new PgpException("exception decrypting secret key", exception2);
				}
				return result;
			}
			ECDHPublicBcpgKey ecdhpublicBcpgKey = (ECDHPublicBcpgKey)privKey.PublicKeyPacket.Key;
			X9ECParameters x9ECParameters = ECKeyPairGenerator.FindECCurveByOid(ecdhpublicBcpgKey.CurveOid);
			byte[] array2 = encSessionKey[0];
			int num = (((int)(array2[0] & byte.MaxValue) << 8) + (int)(array2[1] & byte.MaxValue) + 7) / 8;
			if (2 + num + 1 > array2.Length)
			{
				throw new PgpException("encoded length out of range");
			}
			byte[] array3 = new byte[num];
			Array.Copy(array2, 2, array3, 0, num);
			int num2 = (int)array2[num + 2];
			if (2 + num + 1 + num2 > array2.Length)
			{
				throw new PgpException("encoded length out of range");
			}
			byte[] array4 = new byte[num2];
			Array.Copy(array2, 2 + num + 1, array4, 0, array4.Length);
			ECPoint ecpoint = x9ECParameters.Curve.DecodePoint(array3);
			ECPrivateKeyParameters ecprivateKeyParameters = (ECPrivateKeyParameters)privKey.Key;
			ECPoint s = ecpoint.Multiply(ecprivateKeyParameters.D).Normalize();
			KeyParameter parameters = new KeyParameter(Rfc6637Utilities.CreateKey(privKey.PublicKeyPacket, s));
			IWrapper wrapper = PgpUtilities.CreateWrapper(ecdhpublicBcpgKey.SymmetricKeyAlgorithm);
			wrapper.Init(false, parameters);
			return PgpPad.UnpadSessionData(wrapper.Unwrap(array4, 0, array4.Length));
		}

		// Token: 0x060038A9 RID: 14505 RVA: 0x00130CE8 File Offset: 0x00130CE8
		private static void ProcessEncodedMpi(IBufferedCipher cipher, int size, byte[] mpiEnc)
		{
			if (mpiEnc.Length - 2 > size)
			{
				cipher.ProcessBytes(mpiEnc, 3, mpiEnc.Length - 3);
				return;
			}
			byte[] array = new byte[size];
			Array.Copy(mpiEnc, 2, array, array.Length - (mpiEnc.Length - 2), mpiEnc.Length - 2);
			cipher.ProcessBytes(array, 0, array.Length);
		}

		// Token: 0x04001DE2 RID: 7650
		private PublicKeyEncSessionPacket keyData;
	}
}
