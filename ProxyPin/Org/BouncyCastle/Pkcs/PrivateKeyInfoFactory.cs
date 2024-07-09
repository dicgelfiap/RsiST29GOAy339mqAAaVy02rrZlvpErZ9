using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.CryptoPro;
using Org.BouncyCastle.Asn1.EdEC;
using Org.BouncyCastle.Asn1.Oiw;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.Rosstandart;
using Org.BouncyCastle.Asn1.Sec;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Pkcs
{
	// Token: 0x02000688 RID: 1672
	public sealed class PrivateKeyInfoFactory
	{
		// Token: 0x06003A53 RID: 14931 RVA: 0x0013A310 File Offset: 0x0013A310
		private PrivateKeyInfoFactory()
		{
		}

		// Token: 0x06003A54 RID: 14932 RVA: 0x0013A318 File Offset: 0x0013A318
		public static PrivateKeyInfo CreatePrivateKeyInfo(AsymmetricKeyParameter privateKey)
		{
			return PrivateKeyInfoFactory.CreatePrivateKeyInfo(privateKey, null);
		}

		// Token: 0x06003A55 RID: 14933 RVA: 0x0013A324 File Offset: 0x0013A324
		public static PrivateKeyInfo CreatePrivateKeyInfo(AsymmetricKeyParameter privateKey, Asn1Set attributes)
		{
			if (privateKey == null)
			{
				throw new ArgumentNullException("privateKey");
			}
			if (!privateKey.IsPrivate)
			{
				throw new ArgumentException("Public key passed - private key expected", "privateKey");
			}
			if (privateKey is ElGamalPrivateKeyParameters)
			{
				ElGamalPrivateKeyParameters elGamalPrivateKeyParameters = (ElGamalPrivateKeyParameters)privateKey;
				ElGamalParameters parameters = elGamalPrivateKeyParameters.Parameters;
				return new PrivateKeyInfo(new AlgorithmIdentifier(OiwObjectIdentifiers.ElGamalAlgorithm, new ElGamalParameter(parameters.P, parameters.G).ToAsn1Object()), new DerInteger(elGamalPrivateKeyParameters.X), attributes);
			}
			if (privateKey is DsaPrivateKeyParameters)
			{
				DsaPrivateKeyParameters dsaPrivateKeyParameters = (DsaPrivateKeyParameters)privateKey;
				DsaParameters parameters2 = dsaPrivateKeyParameters.Parameters;
				return new PrivateKeyInfo(new AlgorithmIdentifier(X9ObjectIdentifiers.IdDsa, new DsaParameter(parameters2.P, parameters2.Q, parameters2.G).ToAsn1Object()), new DerInteger(dsaPrivateKeyParameters.X), attributes);
			}
			if (privateKey is DHPrivateKeyParameters)
			{
				DHPrivateKeyParameters dhprivateKeyParameters = (DHPrivateKeyParameters)privateKey;
				DHParameter dhparameter = new DHParameter(dhprivateKeyParameters.Parameters.P, dhprivateKeyParameters.Parameters.G, dhprivateKeyParameters.Parameters.L);
				return new PrivateKeyInfo(new AlgorithmIdentifier(dhprivateKeyParameters.AlgorithmOid, dhparameter.ToAsn1Object()), new DerInteger(dhprivateKeyParameters.X), attributes);
			}
			if (privateKey is RsaKeyParameters)
			{
				AlgorithmIdentifier privateKeyAlgorithm = new AlgorithmIdentifier(PkcsObjectIdentifiers.RsaEncryption, DerNull.Instance);
				RsaPrivateKeyStructure rsaPrivateKeyStructure;
				if (privateKey is RsaPrivateCrtKeyParameters)
				{
					RsaPrivateCrtKeyParameters rsaPrivateCrtKeyParameters = (RsaPrivateCrtKeyParameters)privateKey;
					rsaPrivateKeyStructure = new RsaPrivateKeyStructure(rsaPrivateCrtKeyParameters.Modulus, rsaPrivateCrtKeyParameters.PublicExponent, rsaPrivateCrtKeyParameters.Exponent, rsaPrivateCrtKeyParameters.P, rsaPrivateCrtKeyParameters.Q, rsaPrivateCrtKeyParameters.DP, rsaPrivateCrtKeyParameters.DQ, rsaPrivateCrtKeyParameters.QInv);
				}
				else
				{
					RsaKeyParameters rsaKeyParameters = (RsaKeyParameters)privateKey;
					rsaPrivateKeyStructure = new RsaPrivateKeyStructure(rsaKeyParameters.Modulus, BigInteger.Zero, rsaKeyParameters.Exponent, BigInteger.Zero, BigInteger.Zero, BigInteger.Zero, BigInteger.Zero, BigInteger.Zero);
				}
				return new PrivateKeyInfo(privateKeyAlgorithm, rsaPrivateKeyStructure.ToAsn1Object(), attributes);
			}
			if (privateKey is ECPrivateKeyParameters)
			{
				ECPrivateKeyParameters ecprivateKeyParameters = (ECPrivateKeyParameters)privateKey;
				DerBitString publicKey = new DerBitString(ECKeyPairGenerator.GetCorrespondingPublicKey(ecprivateKeyParameters).Q.GetEncoded(false));
				ECDomainParameters parameters3 = ecprivateKeyParameters.Parameters;
				if (parameters3 is ECGost3410Parameters)
				{
					ECGost3410Parameters ecgost3410Parameters = (ECGost3410Parameters)parameters3;
					Gost3410PublicKeyAlgParameters parameters4 = new Gost3410PublicKeyAlgParameters(ecgost3410Parameters.PublicKeyParamSet, ecgost3410Parameters.DigestParamSet, ecgost3410Parameters.EncryptionParamSet);
					bool flag = ecprivateKeyParameters.D.BitLength > 256;
					DerObjectIdentifier algorithm = flag ? RosstandartObjectIdentifiers.id_tc26_gost_3410_12_512 : RosstandartObjectIdentifiers.id_tc26_gost_3410_12_256;
					int num = flag ? 64 : 32;
					byte[] array = new byte[num];
					PrivateKeyInfoFactory.ExtractBytes(array, num, 0, ecprivateKeyParameters.D);
					return new PrivateKeyInfo(new AlgorithmIdentifier(algorithm, parameters4), new DerOctetString(array));
				}
				int bitLength = parameters3.N.BitLength;
				AlgorithmIdentifier privateKeyAlgorithm2;
				ECPrivateKeyStructure privateKey2;
				if (ecprivateKeyParameters.AlgorithmName == "ECGOST3410")
				{
					if (ecprivateKeyParameters.PublicKeyParamSet == null)
					{
						throw Platform.CreateNotImplementedException("Not a CryptoPro parameter set");
					}
					Gost3410PublicKeyAlgParameters parameters5 = new Gost3410PublicKeyAlgParameters(ecprivateKeyParameters.PublicKeyParamSet, CryptoProObjectIdentifiers.GostR3411x94CryptoProParamSet);
					privateKeyAlgorithm2 = new AlgorithmIdentifier(CryptoProObjectIdentifiers.GostR3410x2001, parameters5);
					privateKey2 = new ECPrivateKeyStructure(bitLength, ecprivateKeyParameters.D, publicKey, null);
				}
				else
				{
					X962Parameters parameters6;
					if (ecprivateKeyParameters.PublicKeyParamSet == null)
					{
						X9ECParameters ecParameters = new X9ECParameters(parameters3.Curve, parameters3.G, parameters3.N, parameters3.H, parameters3.GetSeed());
						parameters6 = new X962Parameters(ecParameters);
					}
					else
					{
						parameters6 = new X962Parameters(ecprivateKeyParameters.PublicKeyParamSet);
					}
					privateKey2 = new ECPrivateKeyStructure(bitLength, ecprivateKeyParameters.D, publicKey, parameters6);
					privateKeyAlgorithm2 = new AlgorithmIdentifier(X9ObjectIdentifiers.IdECPublicKey, parameters6);
				}
				return new PrivateKeyInfo(privateKeyAlgorithm2, privateKey2, attributes);
			}
			else if (privateKey is Gost3410PrivateKeyParameters)
			{
				Gost3410PrivateKeyParameters gost3410PrivateKeyParameters = (Gost3410PrivateKeyParameters)privateKey;
				if (gost3410PrivateKeyParameters.PublicKeyParamSet == null)
				{
					throw Platform.CreateNotImplementedException("Not a CryptoPro parameter set");
				}
				byte[] array2 = gost3410PrivateKeyParameters.X.ToByteArrayUnsigned();
				byte[] array3 = new byte[array2.Length];
				for (int num2 = 0; num2 != array3.Length; num2++)
				{
					array3[num2] = array2[array2.Length - 1 - num2];
				}
				Gost3410PublicKeyAlgParameters gost3410PublicKeyAlgParameters = new Gost3410PublicKeyAlgParameters(gost3410PrivateKeyParameters.PublicKeyParamSet, CryptoProObjectIdentifiers.GostR3411x94CryptoProParamSet, null);
				AlgorithmIdentifier privateKeyAlgorithm3 = new AlgorithmIdentifier(CryptoProObjectIdentifiers.GostR3410x94, gost3410PublicKeyAlgParameters.ToAsn1Object());
				return new PrivateKeyInfo(privateKeyAlgorithm3, new DerOctetString(array3), attributes);
			}
			else
			{
				if (privateKey is X448PrivateKeyParameters)
				{
					X448PrivateKeyParameters x448PrivateKeyParameters = (X448PrivateKeyParameters)privateKey;
					return new PrivateKeyInfo(new AlgorithmIdentifier(EdECObjectIdentifiers.id_X448), new DerOctetString(x448PrivateKeyParameters.GetEncoded()), attributes, x448PrivateKeyParameters.GeneratePublicKey().GetEncoded());
				}
				if (privateKey is X25519PrivateKeyParameters)
				{
					X25519PrivateKeyParameters x25519PrivateKeyParameters = (X25519PrivateKeyParameters)privateKey;
					return new PrivateKeyInfo(new AlgorithmIdentifier(EdECObjectIdentifiers.id_X25519), new DerOctetString(x25519PrivateKeyParameters.GetEncoded()), attributes, x25519PrivateKeyParameters.GeneratePublicKey().GetEncoded());
				}
				if (privateKey is Ed448PrivateKeyParameters)
				{
					Ed448PrivateKeyParameters ed448PrivateKeyParameters = (Ed448PrivateKeyParameters)privateKey;
					return new PrivateKeyInfo(new AlgorithmIdentifier(EdECObjectIdentifiers.id_Ed448), new DerOctetString(ed448PrivateKeyParameters.GetEncoded()), attributes, ed448PrivateKeyParameters.GeneratePublicKey().GetEncoded());
				}
				if (privateKey is Ed25519PrivateKeyParameters)
				{
					Ed25519PrivateKeyParameters ed25519PrivateKeyParameters = (Ed25519PrivateKeyParameters)privateKey;
					return new PrivateKeyInfo(new AlgorithmIdentifier(EdECObjectIdentifiers.id_Ed25519), new DerOctetString(ed25519PrivateKeyParameters.GetEncoded()), attributes, ed25519PrivateKeyParameters.GeneratePublicKey().GetEncoded());
				}
				throw new ArgumentException("Class provided is not convertible: " + Platform.GetTypeName(privateKey));
			}
		}

		// Token: 0x06003A56 RID: 14934 RVA: 0x0013A890 File Offset: 0x0013A890
		public static PrivateKeyInfo CreatePrivateKeyInfo(char[] passPhrase, EncryptedPrivateKeyInfo encInfo)
		{
			return PrivateKeyInfoFactory.CreatePrivateKeyInfo(passPhrase, false, encInfo);
		}

		// Token: 0x06003A57 RID: 14935 RVA: 0x0013A89C File Offset: 0x0013A89C
		public static PrivateKeyInfo CreatePrivateKeyInfo(char[] passPhrase, bool wrongPkcs12Zero, EncryptedPrivateKeyInfo encInfo)
		{
			AlgorithmIdentifier encryptionAlgorithm = encInfo.EncryptionAlgorithm;
			IBufferedCipher bufferedCipher = PbeUtilities.CreateEngine(encryptionAlgorithm) as IBufferedCipher;
			if (bufferedCipher == null)
			{
				throw new Exception("Unknown encryption algorithm: " + encryptionAlgorithm.Algorithm);
			}
			ICipherParameters parameters = PbeUtilities.GenerateCipherParameters(encryptionAlgorithm, passPhrase, wrongPkcs12Zero);
			bufferedCipher.Init(false, parameters);
			byte[] obj = bufferedCipher.DoFinal(encInfo.GetEncryptedData());
			return PrivateKeyInfo.GetInstance(obj);
		}

		// Token: 0x06003A58 RID: 14936 RVA: 0x0013A900 File Offset: 0x0013A900
		private static void ExtractBytes(byte[] encKey, int size, int offSet, BigInteger bI)
		{
			byte[] array = bI.ToByteArray();
			if (array.Length < size)
			{
				byte[] array2 = new byte[size];
				Array.Copy(array, 0, array2, array2.Length - array.Length, array.Length);
				array = array2;
			}
			for (int num = 0; num != size; num++)
			{
				encKey[offSet + num] = array[array.Length - 1 - num];
			}
		}
	}
}
