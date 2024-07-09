using System;
using System.IO;
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
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Security
{
	// Token: 0x020006B4 RID: 1716
	public sealed class PrivateKeyFactory
	{
		// Token: 0x06003C18 RID: 15384 RVA: 0x0014A69C File Offset: 0x0014A69C
		private PrivateKeyFactory()
		{
		}

		// Token: 0x06003C19 RID: 15385 RVA: 0x0014A6A4 File Offset: 0x0014A6A4
		public static AsymmetricKeyParameter CreateKey(byte[] privateKeyInfoData)
		{
			return PrivateKeyFactory.CreateKey(PrivateKeyInfo.GetInstance(Asn1Object.FromByteArray(privateKeyInfoData)));
		}

		// Token: 0x06003C1A RID: 15386 RVA: 0x0014A6B8 File Offset: 0x0014A6B8
		public static AsymmetricKeyParameter CreateKey(Stream inStr)
		{
			return PrivateKeyFactory.CreateKey(PrivateKeyInfo.GetInstance(Asn1Object.FromStream(inStr)));
		}

		// Token: 0x06003C1B RID: 15387 RVA: 0x0014A6CC File Offset: 0x0014A6CC
		public static AsymmetricKeyParameter CreateKey(PrivateKeyInfo keyInfo)
		{
			AlgorithmIdentifier privateKeyAlgorithm = keyInfo.PrivateKeyAlgorithm;
			DerObjectIdentifier algorithm = privateKeyAlgorithm.Algorithm;
			if (algorithm.Equals(PkcsObjectIdentifiers.RsaEncryption) || algorithm.Equals(X509ObjectIdentifiers.IdEARsa) || algorithm.Equals(PkcsObjectIdentifiers.IdRsassaPss) || algorithm.Equals(PkcsObjectIdentifiers.IdRsaesOaep))
			{
				RsaPrivateKeyStructure instance = RsaPrivateKeyStructure.GetInstance(keyInfo.ParsePrivateKey());
				return new RsaPrivateCrtKeyParameters(instance.Modulus, instance.PublicExponent, instance.PrivateExponent, instance.Prime1, instance.Prime2, instance.Exponent1, instance.Exponent2, instance.Coefficient);
			}
			if (algorithm.Equals(PkcsObjectIdentifiers.DhKeyAgreement))
			{
				DHParameter dhparameter = new DHParameter(Asn1Sequence.GetInstance(privateKeyAlgorithm.Parameters.ToAsn1Object()));
				DerInteger derInteger = (DerInteger)keyInfo.ParsePrivateKey();
				BigInteger l = dhparameter.L;
				int l2 = (l == null) ? 0 : l.IntValue;
				DHParameters parameters = new DHParameters(dhparameter.P, dhparameter.G, null, l2);
				return new DHPrivateKeyParameters(derInteger.Value, parameters, algorithm);
			}
			if (algorithm.Equals(OiwObjectIdentifiers.ElGamalAlgorithm))
			{
				ElGamalParameter elGamalParameter = new ElGamalParameter(Asn1Sequence.GetInstance(privateKeyAlgorithm.Parameters.ToAsn1Object()));
				DerInteger derInteger2 = (DerInteger)keyInfo.ParsePrivateKey();
				return new ElGamalPrivateKeyParameters(derInteger2.Value, new ElGamalParameters(elGamalParameter.P, elGamalParameter.G));
			}
			if (algorithm.Equals(X9ObjectIdentifiers.IdDsa))
			{
				DerInteger derInteger3 = (DerInteger)keyInfo.ParsePrivateKey();
				Asn1Encodable parameters2 = privateKeyAlgorithm.Parameters;
				DsaParameters parameters3 = null;
				if (parameters2 != null)
				{
					DsaParameter instance2 = DsaParameter.GetInstance(parameters2.ToAsn1Object());
					parameters3 = new DsaParameters(instance2.P, instance2.Q, instance2.G);
				}
				return new DsaPrivateKeyParameters(derInteger3.Value, parameters3);
			}
			if (algorithm.Equals(X9ObjectIdentifiers.IdECPublicKey))
			{
				X962Parameters instance3 = X962Parameters.GetInstance(privateKeyAlgorithm.Parameters.ToAsn1Object());
				X9ECParameters x9ECParameters;
				if (instance3.IsNamedCurve)
				{
					x9ECParameters = ECKeyPairGenerator.FindECCurveByOid((DerObjectIdentifier)instance3.Parameters);
				}
				else
				{
					x9ECParameters = new X9ECParameters((Asn1Sequence)instance3.Parameters);
				}
				ECPrivateKeyStructure instance4 = ECPrivateKeyStructure.GetInstance(keyInfo.ParsePrivateKey());
				BigInteger key = instance4.GetKey();
				if (instance3.IsNamedCurve)
				{
					return new ECPrivateKeyParameters("EC", key, (DerObjectIdentifier)instance3.Parameters);
				}
				ECDomainParameters parameters4 = new ECDomainParameters(x9ECParameters.Curve, x9ECParameters.G, x9ECParameters.N, x9ECParameters.H, x9ECParameters.GetSeed());
				return new ECPrivateKeyParameters(key, parameters4);
			}
			else if (algorithm.Equals(CryptoProObjectIdentifiers.GostR3410x2001))
			{
				Gost3410PublicKeyAlgParameters instance5 = Gost3410PublicKeyAlgParameters.GetInstance(privateKeyAlgorithm.Parameters.ToAsn1Object());
				ECDomainParameters byOid = ECGost3410NamedCurves.GetByOid(instance5.PublicKeyParamSet);
				if (byOid == null)
				{
					throw new ArgumentException("Unrecognized curve OID for GostR3410x2001 private key");
				}
				Asn1Object asn1Object = keyInfo.ParsePrivateKey();
				ECPrivateKeyStructure ecprivateKeyStructure;
				if (asn1Object is DerInteger)
				{
					ecprivateKeyStructure = new ECPrivateKeyStructure(byOid.N.BitLength, ((DerInteger)asn1Object).PositiveValue);
				}
				else
				{
					ecprivateKeyStructure = ECPrivateKeyStructure.GetInstance(asn1Object);
				}
				return new ECPrivateKeyParameters("ECGOST3410", ecprivateKeyStructure.GetKey(), instance5.PublicKeyParamSet);
			}
			else
			{
				if (algorithm.Equals(CryptoProObjectIdentifiers.GostR3410x94))
				{
					Gost3410PublicKeyAlgParameters instance6 = Gost3410PublicKeyAlgParameters.GetInstance(privateKeyAlgorithm.Parameters);
					Asn1Object asn1Object2 = keyInfo.ParsePrivateKey();
					BigInteger x;
					if (asn1Object2 is DerInteger)
					{
						x = DerInteger.GetInstance(asn1Object2).PositiveValue;
					}
					else
					{
						x = new BigInteger(1, Arrays.Reverse(Asn1OctetString.GetInstance(asn1Object2).GetOctets()));
					}
					return new Gost3410PrivateKeyParameters(x, instance6.PublicKeyParamSet);
				}
				if (algorithm.Equals(EdECObjectIdentifiers.id_X25519))
				{
					return new X25519PrivateKeyParameters(PrivateKeyFactory.GetRawKey(keyInfo, X25519PrivateKeyParameters.KeySize), 0);
				}
				if (algorithm.Equals(EdECObjectIdentifiers.id_X448))
				{
					return new X448PrivateKeyParameters(PrivateKeyFactory.GetRawKey(keyInfo, X448PrivateKeyParameters.KeySize), 0);
				}
				if (algorithm.Equals(EdECObjectIdentifiers.id_Ed25519))
				{
					return new Ed25519PrivateKeyParameters(PrivateKeyFactory.GetRawKey(keyInfo, Ed25519PrivateKeyParameters.KeySize), 0);
				}
				if (algorithm.Equals(EdECObjectIdentifiers.id_Ed448))
				{
					return new Ed448PrivateKeyParameters(PrivateKeyFactory.GetRawKey(keyInfo, Ed448PrivateKeyParameters.KeySize), 0);
				}
				if (algorithm.Equals(RosstandartObjectIdentifiers.id_tc26_gost_3410_12_512) || algorithm.Equals(RosstandartObjectIdentifiers.id_tc26_gost_3410_12_256))
				{
					Gost3410PublicKeyAlgParameters instance7 = Gost3410PublicKeyAlgParameters.GetInstance(keyInfo.PrivateKeyAlgorithm.Parameters);
					Asn1Object asn1Object3 = keyInfo.PrivateKeyAlgorithm.Parameters.ToAsn1Object();
					ECGost3410Parameters dp;
					BigInteger d;
					if (asn1Object3 is Asn1Sequence && (Asn1Sequence.GetInstance(asn1Object3).Count == 2 || Asn1Sequence.GetInstance(asn1Object3).Count == 3))
					{
						ECDomainParameters byOid2 = ECGost3410NamedCurves.GetByOid(instance7.PublicKeyParamSet);
						dp = new ECGost3410Parameters(new ECNamedDomainParameters(instance7.PublicKeyParamSet, byOid2), instance7.PublicKeyParamSet, instance7.DigestParamSet, instance7.EncryptionParamSet);
						Asn1Encodable asn1Encodable = keyInfo.ParsePrivateKey();
						if (asn1Encodable is DerInteger)
						{
							d = DerInteger.GetInstance(asn1Encodable).PositiveValue;
						}
						else
						{
							byte[] bytes = Arrays.Reverse(Asn1OctetString.GetInstance(asn1Encodable).GetOctets());
							d = new BigInteger(1, bytes);
						}
					}
					else
					{
						X962Parameters instance8 = X962Parameters.GetInstance(keyInfo.PrivateKeyAlgorithm.Parameters);
						if (instance8.IsNamedCurve)
						{
							DerObjectIdentifier instance9 = DerObjectIdentifier.GetInstance(instance8.Parameters);
							X9ECParameters byOid3 = ECNamedCurveTable.GetByOid(instance9);
							if (byOid3 == null)
							{
								ECDomainParameters byOid4 = ECGost3410NamedCurves.GetByOid(instance9);
								dp = new ECGost3410Parameters(new ECNamedDomainParameters(instance9, byOid4.Curve, byOid4.G, byOid4.N, byOid4.H, byOid4.GetSeed()), instance7.PublicKeyParamSet, instance7.DigestParamSet, instance7.EncryptionParamSet);
							}
							else
							{
								dp = new ECGost3410Parameters(new ECNamedDomainParameters(instance9, byOid3.Curve, byOid3.G, byOid3.N, byOid3.H, byOid3.GetSeed()), instance7.PublicKeyParamSet, instance7.DigestParamSet, instance7.EncryptionParamSet);
							}
						}
						else if (instance8.IsImplicitlyCA)
						{
							dp = null;
						}
						else
						{
							X9ECParameters instance10 = X9ECParameters.GetInstance(instance8.Parameters);
							dp = new ECGost3410Parameters(new ECNamedDomainParameters(algorithm, instance10.Curve, instance10.G, instance10.N, instance10.H, instance10.GetSeed()), instance7.PublicKeyParamSet, instance7.DigestParamSet, instance7.EncryptionParamSet);
						}
						Asn1Encodable asn1Encodable2 = keyInfo.ParsePrivateKey();
						if (asn1Encodable2 is DerInteger)
						{
							DerInteger instance11 = DerInteger.GetInstance(asn1Encodable2);
							d = instance11.Value;
						}
						else
						{
							ECPrivateKeyStructure instance12 = ECPrivateKeyStructure.GetInstance(asn1Encodable2);
							d = instance12.GetKey();
						}
					}
					return new ECPrivateKeyParameters(d, new ECGost3410Parameters(dp, instance7.PublicKeyParamSet, instance7.DigestParamSet, instance7.EncryptionParamSet));
				}
				throw new SecurityUtilityException("algorithm identifier in private key not recognised");
			}
		}

		// Token: 0x06003C1C RID: 15388 RVA: 0x0014ADA0 File Offset: 0x0014ADA0
		private static byte[] GetRawKey(PrivateKeyInfo keyInfo, int expectedSize)
		{
			byte[] octets = Asn1OctetString.GetInstance(keyInfo.ParsePrivateKey()).GetOctets();
			if (expectedSize != octets.Length)
			{
				throw new SecurityUtilityException("private key encoding has incorrect length");
			}
			return octets;
		}

		// Token: 0x06003C1D RID: 15389 RVA: 0x0014ADD8 File Offset: 0x0014ADD8
		public static AsymmetricKeyParameter DecryptKey(char[] passPhrase, EncryptedPrivateKeyInfo encInfo)
		{
			return PrivateKeyFactory.CreateKey(PrivateKeyInfoFactory.CreatePrivateKeyInfo(passPhrase, encInfo));
		}

		// Token: 0x06003C1E RID: 15390 RVA: 0x0014ADE8 File Offset: 0x0014ADE8
		public static AsymmetricKeyParameter DecryptKey(char[] passPhrase, byte[] encryptedPrivateKeyInfoData)
		{
			return PrivateKeyFactory.DecryptKey(passPhrase, Asn1Object.FromByteArray(encryptedPrivateKeyInfoData));
		}

		// Token: 0x06003C1F RID: 15391 RVA: 0x0014ADF8 File Offset: 0x0014ADF8
		public static AsymmetricKeyParameter DecryptKey(char[] passPhrase, Stream encryptedPrivateKeyInfoStream)
		{
			return PrivateKeyFactory.DecryptKey(passPhrase, Asn1Object.FromStream(encryptedPrivateKeyInfoStream));
		}

		// Token: 0x06003C20 RID: 15392 RVA: 0x0014AE08 File Offset: 0x0014AE08
		private static AsymmetricKeyParameter DecryptKey(char[] passPhrase, Asn1Object asn1Object)
		{
			return PrivateKeyFactory.DecryptKey(passPhrase, EncryptedPrivateKeyInfo.GetInstance(asn1Object));
		}

		// Token: 0x06003C21 RID: 15393 RVA: 0x0014AE18 File Offset: 0x0014AE18
		public static byte[] EncryptKey(DerObjectIdentifier algorithm, char[] passPhrase, byte[] salt, int iterationCount, AsymmetricKeyParameter key)
		{
			return EncryptedPrivateKeyInfoFactory.CreateEncryptedPrivateKeyInfo(algorithm, passPhrase, salt, iterationCount, key).GetEncoded();
		}

		// Token: 0x06003C22 RID: 15394 RVA: 0x0014AE2C File Offset: 0x0014AE2C
		public static byte[] EncryptKey(string algorithm, char[] passPhrase, byte[] salt, int iterationCount, AsymmetricKeyParameter key)
		{
			return EncryptedPrivateKeyInfoFactory.CreateEncryptedPrivateKeyInfo(algorithm, passPhrase, salt, iterationCount, key).GetEncoded();
		}
	}
}
