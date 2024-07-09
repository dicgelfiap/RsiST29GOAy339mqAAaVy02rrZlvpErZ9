using System;
using System.Collections;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.CryptoPro;
using Org.BouncyCastle.Asn1.EdEC;
using Org.BouncyCastle.Asn1.Iana;
using Org.BouncyCastle.Asn1.Kisa;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.Ntt;
using Org.BouncyCastle.Asn1.Oiw;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.Rosstandart;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Security
{
	// Token: 0x020006AC RID: 1708
	public sealed class GeneratorUtilities
	{
		// Token: 0x06003BCD RID: 15309 RVA: 0x00146CDC File Offset: 0x00146CDC
		private GeneratorUtilities()
		{
		}

		// Token: 0x06003BCE RID: 15310 RVA: 0x00146CE4 File Offset: 0x00146CE4
		static GeneratorUtilities()
		{
			GeneratorUtilities.AddKgAlgorithm("AES", new object[]
			{
				"AESWRAP"
			});
			GeneratorUtilities.AddKgAlgorithm("AES128", new object[]
			{
				"2.16.840.1.101.3.4.2",
				NistObjectIdentifiers.IdAes128Cbc,
				NistObjectIdentifiers.IdAes128Cfb,
				NistObjectIdentifiers.IdAes128Ecb,
				NistObjectIdentifiers.IdAes128Ofb,
				NistObjectIdentifiers.IdAes128Wrap
			});
			GeneratorUtilities.AddKgAlgorithm("AES192", new object[]
			{
				"2.16.840.1.101.3.4.22",
				NistObjectIdentifiers.IdAes192Cbc,
				NistObjectIdentifiers.IdAes192Cfb,
				NistObjectIdentifiers.IdAes192Ecb,
				NistObjectIdentifiers.IdAes192Ofb,
				NistObjectIdentifiers.IdAes192Wrap
			});
			GeneratorUtilities.AddKgAlgorithm("AES256", new object[]
			{
				"2.16.840.1.101.3.4.42",
				NistObjectIdentifiers.IdAes256Cbc,
				NistObjectIdentifiers.IdAes256Cfb,
				NistObjectIdentifiers.IdAes256Ecb,
				NistObjectIdentifiers.IdAes256Ofb,
				NistObjectIdentifiers.IdAes256Wrap
			});
			GeneratorUtilities.AddKgAlgorithm("BLOWFISH", new object[]
			{
				"1.3.6.1.4.1.3029.1.2"
			});
			GeneratorUtilities.AddKgAlgorithm("CAMELLIA", new object[]
			{
				"CAMELLIAWRAP"
			});
			GeneratorUtilities.AddKgAlgorithm("CAMELLIA128", new object[]
			{
				NttObjectIdentifiers.IdCamellia128Cbc,
				NttObjectIdentifiers.IdCamellia128Wrap
			});
			GeneratorUtilities.AddKgAlgorithm("CAMELLIA192", new object[]
			{
				NttObjectIdentifiers.IdCamellia192Cbc,
				NttObjectIdentifiers.IdCamellia192Wrap
			});
			GeneratorUtilities.AddKgAlgorithm("CAMELLIA256", new object[]
			{
				NttObjectIdentifiers.IdCamellia256Cbc,
				NttObjectIdentifiers.IdCamellia256Wrap
			});
			GeneratorUtilities.AddKgAlgorithm("CAST5", new object[]
			{
				"1.2.840.113533.7.66.10"
			});
			GeneratorUtilities.AddKgAlgorithm("CAST6", new object[0]);
			GeneratorUtilities.AddKgAlgorithm("CHACHA", new object[0]);
			GeneratorUtilities.AddKgAlgorithm("CHACHA7539", new object[]
			{
				"CHACHA20",
				"CHACHA20-POLY1305",
				PkcsObjectIdentifiers.IdAlgAeadChaCha20Poly1305
			});
			GeneratorUtilities.AddKgAlgorithm("DES", new object[]
			{
				OiwObjectIdentifiers.DesCbc,
				OiwObjectIdentifiers.DesCfb,
				OiwObjectIdentifiers.DesEcb,
				OiwObjectIdentifiers.DesOfb
			});
			GeneratorUtilities.AddKgAlgorithm("DESEDE", new object[]
			{
				"DESEDEWRAP",
				"TDEA",
				OiwObjectIdentifiers.DesEde
			});
			GeneratorUtilities.AddKgAlgorithm("DESEDE3", new object[]
			{
				PkcsObjectIdentifiers.DesEde3Cbc,
				PkcsObjectIdentifiers.IdAlgCms3DesWrap
			});
			GeneratorUtilities.AddKgAlgorithm("GOST28147", new object[]
			{
				"GOST",
				"GOST-28147",
				CryptoProObjectIdentifiers.GostR28147Cbc
			});
			GeneratorUtilities.AddKgAlgorithm("HC128", new object[0]);
			GeneratorUtilities.AddKgAlgorithm("HC256", new object[0]);
			GeneratorUtilities.AddKgAlgorithm("IDEA", new object[]
			{
				"1.3.6.1.4.1.188.7.1.1.2"
			});
			GeneratorUtilities.AddKgAlgorithm("NOEKEON", new object[0]);
			GeneratorUtilities.AddKgAlgorithm("RC2", new object[]
			{
				PkcsObjectIdentifiers.RC2Cbc,
				PkcsObjectIdentifiers.IdAlgCmsRC2Wrap
			});
			GeneratorUtilities.AddKgAlgorithm("RC4", new object[]
			{
				"ARC4",
				"1.2.840.113549.3.4"
			});
			GeneratorUtilities.AddKgAlgorithm("RC5", new object[]
			{
				"RC5-32"
			});
			GeneratorUtilities.AddKgAlgorithm("RC5-64", new object[0]);
			GeneratorUtilities.AddKgAlgorithm("RC6", new object[0]);
			GeneratorUtilities.AddKgAlgorithm("RIJNDAEL", new object[0]);
			GeneratorUtilities.AddKgAlgorithm("SALSA20", new object[0]);
			GeneratorUtilities.AddKgAlgorithm("SEED", new object[]
			{
				KisaObjectIdentifiers.IdNpkiAppCmsSeedWrap,
				KisaObjectIdentifiers.IdSeedCbc
			});
			GeneratorUtilities.AddKgAlgorithm("SERPENT", new object[0]);
			GeneratorUtilities.AddKgAlgorithm("SKIPJACK", new object[0]);
			GeneratorUtilities.AddKgAlgorithm("SM4", new object[0]);
			GeneratorUtilities.AddKgAlgorithm("TEA", new object[0]);
			GeneratorUtilities.AddKgAlgorithm("THREEFISH-256", new object[0]);
			GeneratorUtilities.AddKgAlgorithm("THREEFISH-512", new object[0]);
			GeneratorUtilities.AddKgAlgorithm("THREEFISH-1024", new object[0]);
			GeneratorUtilities.AddKgAlgorithm("TNEPRES", new object[0]);
			GeneratorUtilities.AddKgAlgorithm("TWOFISH", new object[0]);
			GeneratorUtilities.AddKgAlgorithm("VMPC", new object[0]);
			GeneratorUtilities.AddKgAlgorithm("VMPC-KSA3", new object[0]);
			GeneratorUtilities.AddKgAlgorithm("XTEA", new object[0]);
			GeneratorUtilities.AddHMacKeyGenerator("MD2", new object[0]);
			GeneratorUtilities.AddHMacKeyGenerator("MD4", new object[0]);
			GeneratorUtilities.AddHMacKeyGenerator("MD5", new object[]
			{
				IanaObjectIdentifiers.HmacMD5
			});
			GeneratorUtilities.AddHMacKeyGenerator("SHA1", new object[]
			{
				PkcsObjectIdentifiers.IdHmacWithSha1,
				IanaObjectIdentifiers.HmacSha1
			});
			GeneratorUtilities.AddHMacKeyGenerator("SHA224", new object[]
			{
				PkcsObjectIdentifiers.IdHmacWithSha224
			});
			GeneratorUtilities.AddHMacKeyGenerator("SHA256", new object[]
			{
				PkcsObjectIdentifiers.IdHmacWithSha256
			});
			GeneratorUtilities.AddHMacKeyGenerator("SHA384", new object[]
			{
				PkcsObjectIdentifiers.IdHmacWithSha384
			});
			GeneratorUtilities.AddHMacKeyGenerator("SHA512", new object[]
			{
				PkcsObjectIdentifiers.IdHmacWithSha512
			});
			GeneratorUtilities.AddHMacKeyGenerator("SHA512/224", new object[0]);
			GeneratorUtilities.AddHMacKeyGenerator("SHA512/256", new object[0]);
			GeneratorUtilities.AddHMacKeyGenerator("KECCAK224", new object[0]);
			GeneratorUtilities.AddHMacKeyGenerator("KECCAK256", new object[0]);
			GeneratorUtilities.AddHMacKeyGenerator("KECCAK288", new object[0]);
			GeneratorUtilities.AddHMacKeyGenerator("KECCAK384", new object[0]);
			GeneratorUtilities.AddHMacKeyGenerator("KECCAK512", new object[0]);
			GeneratorUtilities.AddHMacKeyGenerator("SHA3-224", new object[]
			{
				NistObjectIdentifiers.IdHMacWithSha3_224
			});
			GeneratorUtilities.AddHMacKeyGenerator("SHA3-256", new object[]
			{
				NistObjectIdentifiers.IdHMacWithSha3_256
			});
			GeneratorUtilities.AddHMacKeyGenerator("SHA3-384", new object[]
			{
				NistObjectIdentifiers.IdHMacWithSha3_384
			});
			GeneratorUtilities.AddHMacKeyGenerator("SHA3-512", new object[]
			{
				NistObjectIdentifiers.IdHMacWithSha3_512
			});
			GeneratorUtilities.AddHMacKeyGenerator("RIPEMD128", new object[0]);
			GeneratorUtilities.AddHMacKeyGenerator("RIPEMD160", new object[]
			{
				IanaObjectIdentifiers.HmacRipeMD160
			});
			GeneratorUtilities.AddHMacKeyGenerator("TIGER", new object[]
			{
				IanaObjectIdentifiers.HmacTiger
			});
			GeneratorUtilities.AddHMacKeyGenerator("GOST3411-2012-256", new object[]
			{
				RosstandartObjectIdentifiers.id_tc26_hmac_gost_3411_12_256
			});
			GeneratorUtilities.AddHMacKeyGenerator("GOST3411-2012-512", new object[]
			{
				RosstandartObjectIdentifiers.id_tc26_hmac_gost_3411_12_512
			});
			GeneratorUtilities.AddKpgAlgorithm("DH", new object[]
			{
				"DIFFIEHELLMAN"
			});
			GeneratorUtilities.AddKpgAlgorithm("DSA", new object[0]);
			GeneratorUtilities.AddKpgAlgorithm("EC", new object[]
			{
				X9ObjectIdentifiers.DHSinglePassStdDHSha1KdfScheme
			});
			GeneratorUtilities.AddKpgAlgorithm("ECDH", new object[]
			{
				"ECIES"
			});
			GeneratorUtilities.AddKpgAlgorithm("ECDHC", new object[0]);
			GeneratorUtilities.AddKpgAlgorithm("ECMQV", new object[]
			{
				X9ObjectIdentifiers.MqvSinglePassSha1KdfScheme
			});
			GeneratorUtilities.AddKpgAlgorithm("ECDSA", new object[0]);
			GeneratorUtilities.AddKpgAlgorithm("ECGOST3410", new object[]
			{
				"ECGOST-3410",
				"GOST-3410-2001"
			});
			GeneratorUtilities.AddKpgAlgorithm("Ed25519", new object[]
			{
				"Ed25519ctx",
				"Ed25519ph",
				EdECObjectIdentifiers.id_Ed25519
			});
			GeneratorUtilities.AddKpgAlgorithm("Ed448", new object[]
			{
				"Ed448ph",
				EdECObjectIdentifiers.id_Ed448
			});
			GeneratorUtilities.AddKpgAlgorithm("ELGAMAL", new object[0]);
			GeneratorUtilities.AddKpgAlgorithm("GOST3410", new object[]
			{
				"GOST-3410",
				"GOST-3410-94"
			});
			GeneratorUtilities.AddKpgAlgorithm("RSA", new object[]
			{
				"1.2.840.113549.1.1.1"
			});
			GeneratorUtilities.AddKpgAlgorithm("X25519", new object[]
			{
				EdECObjectIdentifiers.id_X25519
			});
			GeneratorUtilities.AddKpgAlgorithm("X448", new object[]
			{
				EdECObjectIdentifiers.id_X448
			});
			GeneratorUtilities.AddDefaultKeySizeEntries(64, new string[]
			{
				"DES"
			});
			GeneratorUtilities.AddDefaultKeySizeEntries(80, new string[]
			{
				"SKIPJACK"
			});
			GeneratorUtilities.AddDefaultKeySizeEntries(128, new string[]
			{
				"AES128",
				"BLOWFISH",
				"CAMELLIA128",
				"CAST5",
				"CHACHA",
				"DESEDE",
				"HC128",
				"HMACMD2",
				"HMACMD4",
				"HMACMD5",
				"HMACRIPEMD128",
				"IDEA",
				"NOEKEON",
				"RC2",
				"RC4",
				"RC5",
				"SALSA20",
				"SEED",
				"SM4",
				"TEA",
				"XTEA",
				"VMPC",
				"VMPC-KSA3"
			});
			GeneratorUtilities.AddDefaultKeySizeEntries(160, new string[]
			{
				"HMACRIPEMD160",
				"HMACSHA1"
			});
			GeneratorUtilities.AddDefaultKeySizeEntries(192, new string[]
			{
				"AES",
				"AES192",
				"CAMELLIA192",
				"DESEDE3",
				"HMACTIGER",
				"RIJNDAEL",
				"SERPENT",
				"TNEPRES"
			});
			GeneratorUtilities.AddDefaultKeySizeEntries(224, new string[]
			{
				"HMACSHA3-224",
				"HMACKECCAK224",
				"HMACSHA224",
				"HMACSHA512/224"
			});
			GeneratorUtilities.AddDefaultKeySizeEntries(256, new string[]
			{
				"AES256",
				"CAMELLIA",
				"CAMELLIA256",
				"CAST6",
				"CHACHA7539",
				"GOST28147",
				"HC256",
				"HMACGOST3411-2012-256",
				"HMACSHA3-256",
				"HMACKECCAK256",
				"HMACSHA256",
				"HMACSHA512/256",
				"RC5-64",
				"RC6",
				"THREEFISH-256",
				"TWOFISH"
			});
			GeneratorUtilities.AddDefaultKeySizeEntries(288, new string[]
			{
				"HMACKECCAK288"
			});
			GeneratorUtilities.AddDefaultKeySizeEntries(384, new string[]
			{
				"HMACSHA3-384",
				"HMACKECCAK384",
				"HMACSHA384"
			});
			GeneratorUtilities.AddDefaultKeySizeEntries(512, new string[]
			{
				"HMACGOST3411-2012-512",
				"HMACSHA3-512",
				"HMACKECCAK512",
				"HMACSHA512",
				"THREEFISH-512"
			});
			GeneratorUtilities.AddDefaultKeySizeEntries(1024, new string[]
			{
				"THREEFISH-1024"
			});
		}

		// Token: 0x06003BCF RID: 15311 RVA: 0x001478E0 File Offset: 0x001478E0
		private static void AddDefaultKeySizeEntries(int size, params string[] algorithms)
		{
			foreach (string key in algorithms)
			{
				GeneratorUtilities.defaultKeySizes.Add(key, size);
			}
		}

		// Token: 0x06003BD0 RID: 15312 RVA: 0x00147920 File Offset: 0x00147920
		private static void AddKgAlgorithm(string canonicalName, params object[] aliases)
		{
			GeneratorUtilities.kgAlgorithms[Platform.ToUpperInvariant(canonicalName)] = canonicalName;
			foreach (object obj in aliases)
			{
				GeneratorUtilities.kgAlgorithms[Platform.ToUpperInvariant(obj.ToString())] = canonicalName;
			}
		}

		// Token: 0x06003BD1 RID: 15313 RVA: 0x00147970 File Offset: 0x00147970
		private static void AddKpgAlgorithm(string canonicalName, params object[] aliases)
		{
			GeneratorUtilities.kpgAlgorithms[Platform.ToUpperInvariant(canonicalName)] = canonicalName;
			foreach (object obj in aliases)
			{
				GeneratorUtilities.kpgAlgorithms[Platform.ToUpperInvariant(obj.ToString())] = canonicalName;
			}
		}

		// Token: 0x06003BD2 RID: 15314 RVA: 0x001479C0 File Offset: 0x001479C0
		private static void AddHMacKeyGenerator(string algorithm, params object[] aliases)
		{
			string text = "HMAC" + algorithm;
			GeneratorUtilities.kgAlgorithms[text] = text;
			GeneratorUtilities.kgAlgorithms["HMAC-" + algorithm] = text;
			GeneratorUtilities.kgAlgorithms["HMAC/" + algorithm] = text;
			foreach (object obj in aliases)
			{
				GeneratorUtilities.kgAlgorithms[Platform.ToUpperInvariant(obj.ToString())] = text;
			}
		}

		// Token: 0x06003BD3 RID: 15315 RVA: 0x00147A44 File Offset: 0x00147A44
		internal static string GetCanonicalKeyGeneratorAlgorithm(string algorithm)
		{
			return (string)GeneratorUtilities.kgAlgorithms[Platform.ToUpperInvariant(algorithm)];
		}

		// Token: 0x06003BD4 RID: 15316 RVA: 0x00147A5C File Offset: 0x00147A5C
		internal static string GetCanonicalKeyPairGeneratorAlgorithm(string algorithm)
		{
			return (string)GeneratorUtilities.kpgAlgorithms[Platform.ToUpperInvariant(algorithm)];
		}

		// Token: 0x06003BD5 RID: 15317 RVA: 0x00147A74 File Offset: 0x00147A74
		public static CipherKeyGenerator GetKeyGenerator(DerObjectIdentifier oid)
		{
			return GeneratorUtilities.GetKeyGenerator(oid.Id);
		}

		// Token: 0x06003BD6 RID: 15318 RVA: 0x00147A84 File Offset: 0x00147A84
		public static CipherKeyGenerator GetKeyGenerator(string algorithm)
		{
			string canonicalKeyGeneratorAlgorithm = GeneratorUtilities.GetCanonicalKeyGeneratorAlgorithm(algorithm);
			if (canonicalKeyGeneratorAlgorithm == null)
			{
				throw new SecurityUtilityException("KeyGenerator " + algorithm + " not recognised.");
			}
			int num = GeneratorUtilities.FindDefaultKeySize(canonicalKeyGeneratorAlgorithm);
			if (num == -1)
			{
				throw new SecurityUtilityException(string.Concat(new string[]
				{
					"KeyGenerator ",
					algorithm,
					" (",
					canonicalKeyGeneratorAlgorithm,
					") not supported."
				}));
			}
			if (canonicalKeyGeneratorAlgorithm == "DES")
			{
				return new DesKeyGenerator(num);
			}
			if (canonicalKeyGeneratorAlgorithm == "DESEDE" || canonicalKeyGeneratorAlgorithm == "DESEDE3")
			{
				return new DesEdeKeyGenerator(num);
			}
			return new CipherKeyGenerator(num);
		}

		// Token: 0x06003BD7 RID: 15319 RVA: 0x00147B54 File Offset: 0x00147B54
		public static IAsymmetricCipherKeyPairGenerator GetKeyPairGenerator(DerObjectIdentifier oid)
		{
			return GeneratorUtilities.GetKeyPairGenerator(oid.Id);
		}

		// Token: 0x06003BD8 RID: 15320 RVA: 0x00147B64 File Offset: 0x00147B64
		public static IAsymmetricCipherKeyPairGenerator GetKeyPairGenerator(string algorithm)
		{
			string canonicalKeyPairGeneratorAlgorithm = GeneratorUtilities.GetCanonicalKeyPairGeneratorAlgorithm(algorithm);
			if (canonicalKeyPairGeneratorAlgorithm == null)
			{
				throw new SecurityUtilityException("KeyPairGenerator " + algorithm + " not recognised.");
			}
			if (canonicalKeyPairGeneratorAlgorithm == "DH")
			{
				return new DHKeyPairGenerator();
			}
			if (canonicalKeyPairGeneratorAlgorithm == "DSA")
			{
				return new DsaKeyPairGenerator();
			}
			if (Platform.StartsWith(canonicalKeyPairGeneratorAlgorithm, "EC"))
			{
				return new ECKeyPairGenerator(canonicalKeyPairGeneratorAlgorithm);
			}
			if (canonicalKeyPairGeneratorAlgorithm == "Ed25519")
			{
				return new Ed25519KeyPairGenerator();
			}
			if (canonicalKeyPairGeneratorAlgorithm == "Ed448")
			{
				return new Ed448KeyPairGenerator();
			}
			if (canonicalKeyPairGeneratorAlgorithm == "ELGAMAL")
			{
				return new ElGamalKeyPairGenerator();
			}
			if (canonicalKeyPairGeneratorAlgorithm == "GOST3410")
			{
				return new Gost3410KeyPairGenerator();
			}
			if (canonicalKeyPairGeneratorAlgorithm == "RSA")
			{
				return new RsaKeyPairGenerator();
			}
			if (canonicalKeyPairGeneratorAlgorithm == "X25519")
			{
				return new X25519KeyPairGenerator();
			}
			if (canonicalKeyPairGeneratorAlgorithm == "X448")
			{
				return new X448KeyPairGenerator();
			}
			throw new SecurityUtilityException(string.Concat(new string[]
			{
				"KeyPairGenerator ",
				algorithm,
				" (",
				canonicalKeyPairGeneratorAlgorithm,
				") not supported."
			}));
		}

		// Token: 0x06003BD9 RID: 15321 RVA: 0x00147CBC File Offset: 0x00147CBC
		internal static int GetDefaultKeySize(DerObjectIdentifier oid)
		{
			return GeneratorUtilities.GetDefaultKeySize(oid.Id);
		}

		// Token: 0x06003BDA RID: 15322 RVA: 0x00147CCC File Offset: 0x00147CCC
		internal static int GetDefaultKeySize(string algorithm)
		{
			string canonicalKeyGeneratorAlgorithm = GeneratorUtilities.GetCanonicalKeyGeneratorAlgorithm(algorithm);
			if (canonicalKeyGeneratorAlgorithm == null)
			{
				throw new SecurityUtilityException("KeyGenerator " + algorithm + " not recognised.");
			}
			int num = GeneratorUtilities.FindDefaultKeySize(canonicalKeyGeneratorAlgorithm);
			if (num == -1)
			{
				throw new SecurityUtilityException(string.Concat(new string[]
				{
					"KeyGenerator ",
					algorithm,
					" (",
					canonicalKeyGeneratorAlgorithm,
					") not supported."
				}));
			}
			return num;
		}

		// Token: 0x06003BDB RID: 15323 RVA: 0x00147D58 File Offset: 0x00147D58
		private static int FindDefaultKeySize(string canonicalName)
		{
			if (!GeneratorUtilities.defaultKeySizes.Contains(canonicalName))
			{
				return -1;
			}
			return (int)GeneratorUtilities.defaultKeySizes[canonicalName];
		}

		// Token: 0x04001E99 RID: 7833
		private static readonly IDictionary kgAlgorithms = Platform.CreateHashtable();

		// Token: 0x04001E9A RID: 7834
		private static readonly IDictionary kpgAlgorithms = Platform.CreateHashtable();

		// Token: 0x04001E9B RID: 7835
		private static readonly IDictionary defaultKeySizes = Platform.CreateHashtable();
	}
}
