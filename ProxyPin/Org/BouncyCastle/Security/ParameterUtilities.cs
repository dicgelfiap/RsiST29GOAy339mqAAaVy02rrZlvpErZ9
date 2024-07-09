using System;
using System.Collections;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.CryptoPro;
using Org.BouncyCastle.Asn1.Kisa;
using Org.BouncyCastle.Asn1.Misc;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.Ntt;
using Org.BouncyCastle.Asn1.Oiw;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Security
{
	// Token: 0x020006B2 RID: 1714
	public sealed class ParameterUtilities
	{
		// Token: 0x06003BF0 RID: 15344 RVA: 0x0014856C File Offset: 0x0014856C
		private ParameterUtilities()
		{
		}

		// Token: 0x06003BF1 RID: 15345 RVA: 0x00148574 File Offset: 0x00148574
		static ParameterUtilities()
		{
			ParameterUtilities.AddAlgorithm("AES", new object[]
			{
				"AESWRAP"
			});
			ParameterUtilities.AddAlgorithm("AES128", new object[]
			{
				"2.16.840.1.101.3.4.2",
				NistObjectIdentifiers.IdAes128Cbc,
				NistObjectIdentifiers.IdAes128Cfb,
				NistObjectIdentifiers.IdAes128Ecb,
				NistObjectIdentifiers.IdAes128Ofb,
				NistObjectIdentifiers.IdAes128Wrap
			});
			ParameterUtilities.AddAlgorithm("AES192", new object[]
			{
				"2.16.840.1.101.3.4.22",
				NistObjectIdentifiers.IdAes192Cbc,
				NistObjectIdentifiers.IdAes192Cfb,
				NistObjectIdentifiers.IdAes192Ecb,
				NistObjectIdentifiers.IdAes192Ofb,
				NistObjectIdentifiers.IdAes192Wrap
			});
			ParameterUtilities.AddAlgorithm("AES256", new object[]
			{
				"2.16.840.1.101.3.4.42",
				NistObjectIdentifiers.IdAes256Cbc,
				NistObjectIdentifiers.IdAes256Cfb,
				NistObjectIdentifiers.IdAes256Ecb,
				NistObjectIdentifiers.IdAes256Ofb,
				NistObjectIdentifiers.IdAes256Wrap
			});
			ParameterUtilities.AddAlgorithm("BLOWFISH", new object[]
			{
				"1.3.6.1.4.1.3029.1.2"
			});
			ParameterUtilities.AddAlgorithm("CAMELLIA", new object[]
			{
				"CAMELLIAWRAP"
			});
			ParameterUtilities.AddAlgorithm("CAMELLIA128", new object[]
			{
				NttObjectIdentifiers.IdCamellia128Cbc,
				NttObjectIdentifiers.IdCamellia128Wrap
			});
			ParameterUtilities.AddAlgorithm("CAMELLIA192", new object[]
			{
				NttObjectIdentifiers.IdCamellia192Cbc,
				NttObjectIdentifiers.IdCamellia192Wrap
			});
			ParameterUtilities.AddAlgorithm("CAMELLIA256", new object[]
			{
				NttObjectIdentifiers.IdCamellia256Cbc,
				NttObjectIdentifiers.IdCamellia256Wrap
			});
			ParameterUtilities.AddAlgorithm("CAST5", new object[]
			{
				"1.2.840.113533.7.66.10"
			});
			ParameterUtilities.AddAlgorithm("CAST6", new object[0]);
			ParameterUtilities.AddAlgorithm("CHACHA", new object[0]);
			ParameterUtilities.AddAlgorithm("CHACHA7539", new object[]
			{
				"CHACHA20",
				"CHACHA20-POLY1305",
				PkcsObjectIdentifiers.IdAlgAeadChaCha20Poly1305
			});
			ParameterUtilities.AddAlgorithm("DES", new object[]
			{
				OiwObjectIdentifiers.DesCbc,
				OiwObjectIdentifiers.DesCfb,
				OiwObjectIdentifiers.DesEcb,
				OiwObjectIdentifiers.DesOfb
			});
			ParameterUtilities.AddAlgorithm("DESEDE", new object[]
			{
				"DESEDEWRAP",
				"TDEA",
				OiwObjectIdentifiers.DesEde,
				PkcsObjectIdentifiers.IdAlgCms3DesWrap
			});
			ParameterUtilities.AddAlgorithm("DESEDE3", new object[]
			{
				PkcsObjectIdentifiers.DesEde3Cbc
			});
			ParameterUtilities.AddAlgorithm("GOST28147", new object[]
			{
				"GOST",
				"GOST-28147",
				CryptoProObjectIdentifiers.GostR28147Cbc
			});
			ParameterUtilities.AddAlgorithm("HC128", new object[0]);
			ParameterUtilities.AddAlgorithm("HC256", new object[0]);
			ParameterUtilities.AddAlgorithm("IDEA", new object[]
			{
				"1.3.6.1.4.1.188.7.1.1.2"
			});
			ParameterUtilities.AddAlgorithm("NOEKEON", new object[0]);
			ParameterUtilities.AddAlgorithm("RC2", new object[]
			{
				PkcsObjectIdentifiers.RC2Cbc,
				PkcsObjectIdentifiers.IdAlgCmsRC2Wrap
			});
			ParameterUtilities.AddAlgorithm("RC4", new object[]
			{
				"ARC4",
				"1.2.840.113549.3.4"
			});
			ParameterUtilities.AddAlgorithm("RC5", new object[]
			{
				"RC5-32"
			});
			ParameterUtilities.AddAlgorithm("RC5-64", new object[0]);
			ParameterUtilities.AddAlgorithm("RC6", new object[0]);
			ParameterUtilities.AddAlgorithm("RIJNDAEL", new object[0]);
			ParameterUtilities.AddAlgorithm("SALSA20", new object[0]);
			ParameterUtilities.AddAlgorithm("SEED", new object[]
			{
				KisaObjectIdentifiers.IdNpkiAppCmsSeedWrap,
				KisaObjectIdentifiers.IdSeedCbc
			});
			ParameterUtilities.AddAlgorithm("SERPENT", new object[0]);
			ParameterUtilities.AddAlgorithm("SKIPJACK", new object[0]);
			ParameterUtilities.AddAlgorithm("SM4", new object[0]);
			ParameterUtilities.AddAlgorithm("TEA", new object[0]);
			ParameterUtilities.AddAlgorithm("THREEFISH-256", new object[0]);
			ParameterUtilities.AddAlgorithm("THREEFISH-512", new object[0]);
			ParameterUtilities.AddAlgorithm("THREEFISH-1024", new object[0]);
			ParameterUtilities.AddAlgorithm("TNEPRES", new object[0]);
			ParameterUtilities.AddAlgorithm("TWOFISH", new object[0]);
			ParameterUtilities.AddAlgorithm("VMPC", new object[0]);
			ParameterUtilities.AddAlgorithm("VMPC-KSA3", new object[0]);
			ParameterUtilities.AddAlgorithm("XTEA", new object[0]);
			ParameterUtilities.AddBasicIVSizeEntries(8, new string[]
			{
				"BLOWFISH",
				"CHACHA",
				"DES",
				"DESEDE",
				"DESEDE3",
				"SALSA20"
			});
			ParameterUtilities.AddBasicIVSizeEntries(12, new string[]
			{
				"CHACHA7539"
			});
			ParameterUtilities.AddBasicIVSizeEntries(16, new string[]
			{
				"AES",
				"AES128",
				"AES192",
				"AES256",
				"CAMELLIA",
				"CAMELLIA128",
				"CAMELLIA192",
				"CAMELLIA256",
				"NOEKEON",
				"SEED",
				"SM4"
			});
		}

		// Token: 0x06003BF2 RID: 15346 RVA: 0x00148AF0 File Offset: 0x00148AF0
		private static void AddAlgorithm(string canonicalName, params object[] aliases)
		{
			ParameterUtilities.algorithms[canonicalName] = canonicalName;
			foreach (object obj in aliases)
			{
				ParameterUtilities.algorithms[obj.ToString()] = canonicalName;
			}
		}

		// Token: 0x06003BF3 RID: 15347 RVA: 0x00148B38 File Offset: 0x00148B38
		private static void AddBasicIVSizeEntries(int size, params string[] algorithms)
		{
			foreach (string key in algorithms)
			{
				ParameterUtilities.basicIVSizes.Add(key, size);
			}
		}

		// Token: 0x06003BF4 RID: 15348 RVA: 0x00148B78 File Offset: 0x00148B78
		public static string GetCanonicalAlgorithmName(string algorithm)
		{
			return (string)ParameterUtilities.algorithms[Platform.ToUpperInvariant(algorithm)];
		}

		// Token: 0x06003BF5 RID: 15349 RVA: 0x00148B90 File Offset: 0x00148B90
		public static KeyParameter CreateKeyParameter(DerObjectIdentifier algOid, byte[] keyBytes)
		{
			return ParameterUtilities.CreateKeyParameter(algOid.Id, keyBytes, 0, keyBytes.Length);
		}

		// Token: 0x06003BF6 RID: 15350 RVA: 0x00148BA4 File Offset: 0x00148BA4
		public static KeyParameter CreateKeyParameter(string algorithm, byte[] keyBytes)
		{
			return ParameterUtilities.CreateKeyParameter(algorithm, keyBytes, 0, keyBytes.Length);
		}

		// Token: 0x06003BF7 RID: 15351 RVA: 0x00148BB4 File Offset: 0x00148BB4
		public static KeyParameter CreateKeyParameter(DerObjectIdentifier algOid, byte[] keyBytes, int offset, int length)
		{
			return ParameterUtilities.CreateKeyParameter(algOid.Id, keyBytes, offset, length);
		}

		// Token: 0x06003BF8 RID: 15352 RVA: 0x00148BC4 File Offset: 0x00148BC4
		public static KeyParameter CreateKeyParameter(string algorithm, byte[] keyBytes, int offset, int length)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			string canonicalAlgorithmName = ParameterUtilities.GetCanonicalAlgorithmName(algorithm);
			if (canonicalAlgorithmName == null)
			{
				throw new SecurityUtilityException("Algorithm " + algorithm + " not recognised.");
			}
			if (canonicalAlgorithmName == "DES")
			{
				return new DesParameters(keyBytes, offset, length);
			}
			if (canonicalAlgorithmName == "DESEDE" || canonicalAlgorithmName == "DESEDE3")
			{
				return new DesEdeParameters(keyBytes, offset, length);
			}
			if (canonicalAlgorithmName == "RC2")
			{
				return new RC2Parameters(keyBytes, offset, length);
			}
			return new KeyParameter(keyBytes, offset, length);
		}

		// Token: 0x06003BF9 RID: 15353 RVA: 0x00148C6C File Offset: 0x00148C6C
		public static ICipherParameters GetCipherParameters(DerObjectIdentifier algOid, ICipherParameters key, Asn1Object asn1Params)
		{
			return ParameterUtilities.GetCipherParameters(algOid.Id, key, asn1Params);
		}

		// Token: 0x06003BFA RID: 15354 RVA: 0x00148C7C File Offset: 0x00148C7C
		public static ICipherParameters GetCipherParameters(string algorithm, ICipherParameters key, Asn1Object asn1Params)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			string canonicalAlgorithmName = ParameterUtilities.GetCanonicalAlgorithmName(algorithm);
			if (canonicalAlgorithmName == null)
			{
				throw new SecurityUtilityException("Algorithm " + algorithm + " not recognised.");
			}
			byte[] array = null;
			try
			{
				int num = ParameterUtilities.FindBasicIVSize(canonicalAlgorithmName);
				if (num != -1 || canonicalAlgorithmName == "RIJNDAEL" || canonicalAlgorithmName == "SKIPJACK" || canonicalAlgorithmName == "TWOFISH")
				{
					array = ((Asn1OctetString)asn1Params).GetOctets();
				}
				else if (canonicalAlgorithmName == "CAST5")
				{
					array = Cast5CbcParameters.GetInstance(asn1Params).GetIV();
				}
				else if (canonicalAlgorithmName == "IDEA")
				{
					array = IdeaCbcPar.GetInstance(asn1Params).GetIV();
				}
				else if (canonicalAlgorithmName == "RC2")
				{
					array = RC2CbcParameter.GetInstance(asn1Params).GetIV();
				}
			}
			catch (Exception innerException)
			{
				throw new ArgumentException("Could not process ASN.1 parameters", innerException);
			}
			if (array != null)
			{
				return new ParametersWithIV(key, array);
			}
			throw new SecurityUtilityException("Algorithm " + algorithm + " not recognised.");
		}

		// Token: 0x06003BFB RID: 15355 RVA: 0x00148DB4 File Offset: 0x00148DB4
		public static Asn1Encodable GenerateParameters(DerObjectIdentifier algID, SecureRandom random)
		{
			return ParameterUtilities.GenerateParameters(algID.Id, random);
		}

		// Token: 0x06003BFC RID: 15356 RVA: 0x00148DC4 File Offset: 0x00148DC4
		public static Asn1Encodable GenerateParameters(string algorithm, SecureRandom random)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			string canonicalAlgorithmName = ParameterUtilities.GetCanonicalAlgorithmName(algorithm);
			if (canonicalAlgorithmName == null)
			{
				throw new SecurityUtilityException("Algorithm " + algorithm + " not recognised.");
			}
			int num = ParameterUtilities.FindBasicIVSize(canonicalAlgorithmName);
			if (num != -1)
			{
				return ParameterUtilities.CreateIVOctetString(random, num);
			}
			if (canonicalAlgorithmName == "CAST5")
			{
				return new Cast5CbcParameters(ParameterUtilities.CreateIV(random, 8), 128);
			}
			if (canonicalAlgorithmName == "IDEA")
			{
				return new IdeaCbcPar(ParameterUtilities.CreateIV(random, 8));
			}
			if (canonicalAlgorithmName == "RC2")
			{
				return new RC2CbcParameter(ParameterUtilities.CreateIV(random, 8));
			}
			throw new SecurityUtilityException("Algorithm " + algorithm + " not recognised.");
		}

		// Token: 0x06003BFD RID: 15357 RVA: 0x00148E90 File Offset: 0x00148E90
		public static ICipherParameters WithRandom(ICipherParameters cp, SecureRandom random)
		{
			if (random != null)
			{
				cp = new ParametersWithRandom(cp, random);
			}
			return cp;
		}

		// Token: 0x06003BFE RID: 15358 RVA: 0x00148EA4 File Offset: 0x00148EA4
		private static Asn1OctetString CreateIVOctetString(SecureRandom random, int ivLength)
		{
			return new DerOctetString(ParameterUtilities.CreateIV(random, ivLength));
		}

		// Token: 0x06003BFF RID: 15359 RVA: 0x00148EB4 File Offset: 0x00148EB4
		private static byte[] CreateIV(SecureRandom random, int ivLength)
		{
			return SecureRandom.GetNextBytes(random, ivLength);
		}

		// Token: 0x06003C00 RID: 15360 RVA: 0x00148EC0 File Offset: 0x00148EC0
		private static int FindBasicIVSize(string canonicalName)
		{
			if (!ParameterUtilities.basicIVSizes.Contains(canonicalName))
			{
				return -1;
			}
			return (int)ParameterUtilities.basicIVSizes[canonicalName];
		}

		// Token: 0x04001E9D RID: 7837
		private static readonly IDictionary algorithms = Platform.CreateHashtable();

		// Token: 0x04001E9E RID: 7838
		private static readonly IDictionary basicIVSizes = Platform.CreateHashtable();
	}
}
