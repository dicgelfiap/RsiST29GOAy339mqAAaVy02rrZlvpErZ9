using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Security
{
	// Token: 0x020006AB RID: 1707
	public sealed class DotNetUtilities
	{
		// Token: 0x06003BB4 RID: 15284 RVA: 0x0014673C File Offset: 0x0014673C
		private DotNetUtilities()
		{
		}

		// Token: 0x06003BB5 RID: 15285 RVA: 0x00146744 File Offset: 0x00146744
		public static System.Security.Cryptography.X509Certificates.X509Certificate ToX509Certificate(X509CertificateStructure x509Struct)
		{
			return new System.Security.Cryptography.X509Certificates.X509Certificate(x509Struct.GetDerEncoded());
		}

		// Token: 0x06003BB6 RID: 15286 RVA: 0x00146754 File Offset: 0x00146754
		public static System.Security.Cryptography.X509Certificates.X509Certificate ToX509Certificate(Org.BouncyCastle.X509.X509Certificate x509Cert)
		{
			return new System.Security.Cryptography.X509Certificates.X509Certificate(x509Cert.GetEncoded());
		}

		// Token: 0x06003BB7 RID: 15287 RVA: 0x00146764 File Offset: 0x00146764
		public static Org.BouncyCastle.X509.X509Certificate FromX509Certificate(System.Security.Cryptography.X509Certificates.X509Certificate x509Cert)
		{
			return new X509CertificateParser().ReadCertificate(x509Cert.GetRawCertData());
		}

		// Token: 0x06003BB8 RID: 15288 RVA: 0x00146778 File Offset: 0x00146778
		public static AsymmetricCipherKeyPair GetDsaKeyPair(DSA dsa)
		{
			return DotNetUtilities.GetDsaKeyPair(dsa.ExportParameters(true));
		}

		// Token: 0x06003BB9 RID: 15289 RVA: 0x00146788 File Offset: 0x00146788
		public static AsymmetricCipherKeyPair GetDsaKeyPair(DSAParameters dp)
		{
			DsaValidationParameters parameters = (dp.Seed != null) ? new DsaValidationParameters(dp.Seed, dp.Counter) : null;
			DsaParameters parameters2 = new DsaParameters(new BigInteger(1, dp.P), new BigInteger(1, dp.Q), new BigInteger(1, dp.G), parameters);
			DsaPublicKeyParameters publicParameter = new DsaPublicKeyParameters(new BigInteger(1, dp.Y), parameters2);
			DsaPrivateKeyParameters privateParameter = new DsaPrivateKeyParameters(new BigInteger(1, dp.X), parameters2);
			return new AsymmetricCipherKeyPair(publicParameter, privateParameter);
		}

		// Token: 0x06003BBA RID: 15290 RVA: 0x0014681C File Offset: 0x0014681C
		public static DsaPublicKeyParameters GetDsaPublicKey(DSA dsa)
		{
			return DotNetUtilities.GetDsaPublicKey(dsa.ExportParameters(false));
		}

		// Token: 0x06003BBB RID: 15291 RVA: 0x0014682C File Offset: 0x0014682C
		public static DsaPublicKeyParameters GetDsaPublicKey(DSAParameters dp)
		{
			DsaValidationParameters parameters = (dp.Seed != null) ? new DsaValidationParameters(dp.Seed, dp.Counter) : null;
			DsaParameters parameters2 = new DsaParameters(new BigInteger(1, dp.P), new BigInteger(1, dp.Q), new BigInteger(1, dp.G), parameters);
			return new DsaPublicKeyParameters(new BigInteger(1, dp.Y), parameters2);
		}

		// Token: 0x06003BBC RID: 15292 RVA: 0x001468A4 File Offset: 0x001468A4
		public static AsymmetricCipherKeyPair GetRsaKeyPair(RSA rsa)
		{
			return DotNetUtilities.GetRsaKeyPair(rsa.ExportParameters(true));
		}

		// Token: 0x06003BBD RID: 15293 RVA: 0x001468B4 File Offset: 0x001468B4
		public static AsymmetricCipherKeyPair GetRsaKeyPair(RSAParameters rp)
		{
			BigInteger modulus = new BigInteger(1, rp.Modulus);
			BigInteger bigInteger = new BigInteger(1, rp.Exponent);
			RsaKeyParameters publicParameter = new RsaKeyParameters(false, modulus, bigInteger);
			RsaPrivateCrtKeyParameters privateParameter = new RsaPrivateCrtKeyParameters(modulus, bigInteger, new BigInteger(1, rp.D), new BigInteger(1, rp.P), new BigInteger(1, rp.Q), new BigInteger(1, rp.DP), new BigInteger(1, rp.DQ), new BigInteger(1, rp.InverseQ));
			return new AsymmetricCipherKeyPair(publicParameter, privateParameter);
		}

		// Token: 0x06003BBE RID: 15294 RVA: 0x00146948 File Offset: 0x00146948
		public static RsaKeyParameters GetRsaPublicKey(RSA rsa)
		{
			return DotNetUtilities.GetRsaPublicKey(rsa.ExportParameters(false));
		}

		// Token: 0x06003BBF RID: 15295 RVA: 0x00146958 File Offset: 0x00146958
		public static RsaKeyParameters GetRsaPublicKey(RSAParameters rp)
		{
			return new RsaKeyParameters(false, new BigInteger(1, rp.Modulus), new BigInteger(1, rp.Exponent));
		}

		// Token: 0x06003BC0 RID: 15296 RVA: 0x0014697C File Offset: 0x0014697C
		public static AsymmetricCipherKeyPair GetKeyPair(AsymmetricAlgorithm privateKey)
		{
			if (privateKey is DSA)
			{
				return DotNetUtilities.GetDsaKeyPair((DSA)privateKey);
			}
			if (privateKey is RSA)
			{
				return DotNetUtilities.GetRsaKeyPair((RSA)privateKey);
			}
			throw new ArgumentException("Unsupported algorithm specified", "privateKey");
		}

		// Token: 0x06003BC1 RID: 15297 RVA: 0x001469BC File Offset: 0x001469BC
		public static RSA ToRSA(RsaKeyParameters rsaKey)
		{
			return DotNetUtilities.CreateRSAProvider(DotNetUtilities.ToRSAParameters(rsaKey));
		}

		// Token: 0x06003BC2 RID: 15298 RVA: 0x001469CC File Offset: 0x001469CC
		public static RSA ToRSA(RsaKeyParameters rsaKey, CspParameters csp)
		{
			return DotNetUtilities.CreateRSAProvider(DotNetUtilities.ToRSAParameters(rsaKey), csp);
		}

		// Token: 0x06003BC3 RID: 15299 RVA: 0x001469DC File Offset: 0x001469DC
		public static RSA ToRSA(RsaPrivateCrtKeyParameters privKey)
		{
			return DotNetUtilities.CreateRSAProvider(DotNetUtilities.ToRSAParameters(privKey));
		}

		// Token: 0x06003BC4 RID: 15300 RVA: 0x001469EC File Offset: 0x001469EC
		public static RSA ToRSA(RsaPrivateCrtKeyParameters privKey, CspParameters csp)
		{
			return DotNetUtilities.CreateRSAProvider(DotNetUtilities.ToRSAParameters(privKey), csp);
		}

		// Token: 0x06003BC5 RID: 15301 RVA: 0x001469FC File Offset: 0x001469FC
		public static RSA ToRSA(RsaPrivateKeyStructure privKey)
		{
			return DotNetUtilities.CreateRSAProvider(DotNetUtilities.ToRSAParameters(privKey));
		}

		// Token: 0x06003BC6 RID: 15302 RVA: 0x00146A0C File Offset: 0x00146A0C
		public static RSA ToRSA(RsaPrivateKeyStructure privKey, CspParameters csp)
		{
			return DotNetUtilities.CreateRSAProvider(DotNetUtilities.ToRSAParameters(privKey), csp);
		}

		// Token: 0x06003BC7 RID: 15303 RVA: 0x00146A1C File Offset: 0x00146A1C
		public static RSAParameters ToRSAParameters(RsaKeyParameters rsaKey)
		{
			RSAParameters result = default(RSAParameters);
			result.Modulus = rsaKey.Modulus.ToByteArrayUnsigned();
			if (rsaKey.IsPrivate)
			{
				result.D = DotNetUtilities.ConvertRSAParametersField(rsaKey.Exponent, result.Modulus.Length);
			}
			else
			{
				result.Exponent = rsaKey.Exponent.ToByteArrayUnsigned();
			}
			return result;
		}

		// Token: 0x06003BC8 RID: 15304 RVA: 0x00146A88 File Offset: 0x00146A88
		public static RSAParameters ToRSAParameters(RsaPrivateCrtKeyParameters privKey)
		{
			RSAParameters result = default(RSAParameters);
			result.Modulus = privKey.Modulus.ToByteArrayUnsigned();
			result.Exponent = privKey.PublicExponent.ToByteArrayUnsigned();
			result.P = privKey.P.ToByteArrayUnsigned();
			result.Q = privKey.Q.ToByteArrayUnsigned();
			result.D = DotNetUtilities.ConvertRSAParametersField(privKey.Exponent, result.Modulus.Length);
			result.DP = DotNetUtilities.ConvertRSAParametersField(privKey.DP, result.P.Length);
			result.DQ = DotNetUtilities.ConvertRSAParametersField(privKey.DQ, result.Q.Length);
			result.InverseQ = DotNetUtilities.ConvertRSAParametersField(privKey.QInv, result.Q.Length);
			return result;
		}

		// Token: 0x06003BC9 RID: 15305 RVA: 0x00146B58 File Offset: 0x00146B58
		public static RSAParameters ToRSAParameters(RsaPrivateKeyStructure privKey)
		{
			RSAParameters result = default(RSAParameters);
			result.Modulus = privKey.Modulus.ToByteArrayUnsigned();
			result.Exponent = privKey.PublicExponent.ToByteArrayUnsigned();
			result.P = privKey.Prime1.ToByteArrayUnsigned();
			result.Q = privKey.Prime2.ToByteArrayUnsigned();
			result.D = DotNetUtilities.ConvertRSAParametersField(privKey.PrivateExponent, result.Modulus.Length);
			result.DP = DotNetUtilities.ConvertRSAParametersField(privKey.Exponent1, result.P.Length);
			result.DQ = DotNetUtilities.ConvertRSAParametersField(privKey.Exponent2, result.Q.Length);
			result.InverseQ = DotNetUtilities.ConvertRSAParametersField(privKey.Coefficient, result.Q.Length);
			return result;
		}

		// Token: 0x06003BCA RID: 15306 RVA: 0x00146C28 File Offset: 0x00146C28
		private static byte[] ConvertRSAParametersField(BigInteger n, int size)
		{
			byte[] array = n.ToByteArrayUnsigned();
			if (array.Length == size)
			{
				return array;
			}
			if (array.Length > size)
			{
				throw new ArgumentException("Specified size too small", "size");
			}
			byte[] array2 = new byte[size];
			Array.Copy(array, 0, array2, size - array.Length, array.Length);
			return array2;
		}

		// Token: 0x06003BCB RID: 15307 RVA: 0x00146C7C File Offset: 0x00146C7C
		private static RSA CreateRSAProvider(RSAParameters rp)
		{
			RSACryptoServiceProvider rsacryptoServiceProvider = new RSACryptoServiceProvider(new CspParameters
			{
				KeyContainerName = string.Format("BouncyCastle-{0}", Guid.NewGuid())
			});
			rsacryptoServiceProvider.ImportParameters(rp);
			return rsacryptoServiceProvider;
		}

		// Token: 0x06003BCC RID: 15308 RVA: 0x00146CBC File Offset: 0x00146CBC
		private static RSA CreateRSAProvider(RSAParameters rp, CspParameters csp)
		{
			RSACryptoServiceProvider rsacryptoServiceProvider = new RSACryptoServiceProvider(csp);
			rsacryptoServiceProvider.ImportParameters(rp);
			return rsacryptoServiceProvider;
		}
	}
}
