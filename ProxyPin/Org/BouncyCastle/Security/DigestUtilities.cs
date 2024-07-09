using System;
using System.Collections;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.CryptoPro;
using Org.BouncyCastle.Asn1.GM;
using Org.BouncyCastle.Asn1.Misc;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.Oiw;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.Rosstandart;
using Org.BouncyCastle.Asn1.TeleTrust;
using Org.BouncyCastle.Asn1.UA;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Security
{
	// Token: 0x020006AA RID: 1706
	public sealed class DigestUtilities
	{
		// Token: 0x06003BA9 RID: 15273 RVA: 0x00145AFC File Offset: 0x00145AFC
		private DigestUtilities()
		{
		}

		// Token: 0x06003BAA RID: 15274 RVA: 0x00145B04 File Offset: 0x00145B04
		static DigestUtilities()
		{
			((DigestUtilities.DigestAlgorithm)Enums.GetArbitraryValue(typeof(DigestUtilities.DigestAlgorithm))).ToString();
			DigestUtilities.algorithms[PkcsObjectIdentifiers.MD2.Id] = "MD2";
			DigestUtilities.algorithms[PkcsObjectIdentifiers.MD4.Id] = "MD4";
			DigestUtilities.algorithms[PkcsObjectIdentifiers.MD5.Id] = "MD5";
			DigestUtilities.algorithms["SHA1"] = "SHA-1";
			DigestUtilities.algorithms[OiwObjectIdentifiers.IdSha1.Id] = "SHA-1";
			DigestUtilities.algorithms["SHA224"] = "SHA-224";
			DigestUtilities.algorithms[NistObjectIdentifiers.IdSha224.Id] = "SHA-224";
			DigestUtilities.algorithms["SHA256"] = "SHA-256";
			DigestUtilities.algorithms[NistObjectIdentifiers.IdSha256.Id] = "SHA-256";
			DigestUtilities.algorithms["SHA384"] = "SHA-384";
			DigestUtilities.algorithms[NistObjectIdentifiers.IdSha384.Id] = "SHA-384";
			DigestUtilities.algorithms["SHA512"] = "SHA-512";
			DigestUtilities.algorithms[NistObjectIdentifiers.IdSha512.Id] = "SHA-512";
			DigestUtilities.algorithms["SHA512/224"] = "SHA-512/224";
			DigestUtilities.algorithms[NistObjectIdentifiers.IdSha512_224.Id] = "SHA-512/224";
			DigestUtilities.algorithms["SHA512/256"] = "SHA-512/256";
			DigestUtilities.algorithms[NistObjectIdentifiers.IdSha512_256.Id] = "SHA-512/256";
			DigestUtilities.algorithms["RIPEMD-128"] = "RIPEMD128";
			DigestUtilities.algorithms[TeleTrusTObjectIdentifiers.RipeMD128.Id] = "RIPEMD128";
			DigestUtilities.algorithms["RIPEMD-160"] = "RIPEMD160";
			DigestUtilities.algorithms[TeleTrusTObjectIdentifiers.RipeMD160.Id] = "RIPEMD160";
			DigestUtilities.algorithms["RIPEMD-256"] = "RIPEMD256";
			DigestUtilities.algorithms[TeleTrusTObjectIdentifiers.RipeMD256.Id] = "RIPEMD256";
			DigestUtilities.algorithms["RIPEMD-320"] = "RIPEMD320";
			DigestUtilities.algorithms[CryptoProObjectIdentifiers.GostR3411.Id] = "GOST3411";
			DigestUtilities.algorithms["KECCAK224"] = "KECCAK-224";
			DigestUtilities.algorithms["KECCAK256"] = "KECCAK-256";
			DigestUtilities.algorithms["KECCAK288"] = "KECCAK-288";
			DigestUtilities.algorithms["KECCAK384"] = "KECCAK-384";
			DigestUtilities.algorithms["KECCAK512"] = "KECCAK-512";
			DigestUtilities.algorithms[NistObjectIdentifiers.IdSha3_224.Id] = "SHA3-224";
			DigestUtilities.algorithms[NistObjectIdentifiers.IdSha3_256.Id] = "SHA3-256";
			DigestUtilities.algorithms[NistObjectIdentifiers.IdSha3_384.Id] = "SHA3-384";
			DigestUtilities.algorithms[NistObjectIdentifiers.IdSha3_512.Id] = "SHA3-512";
			DigestUtilities.algorithms[NistObjectIdentifiers.IdShake128.Id] = "SHAKE128";
			DigestUtilities.algorithms[NistObjectIdentifiers.IdShake256.Id] = "SHAKE256";
			DigestUtilities.algorithms[GMObjectIdentifiers.sm3.Id] = "SM3";
			DigestUtilities.algorithms[MiscObjectIdentifiers.id_blake2b160.Id] = "BLAKE2B-160";
			DigestUtilities.algorithms[MiscObjectIdentifiers.id_blake2b256.Id] = "BLAKE2B-256";
			DigestUtilities.algorithms[MiscObjectIdentifiers.id_blake2b384.Id] = "BLAKE2B-384";
			DigestUtilities.algorithms[MiscObjectIdentifiers.id_blake2b512.Id] = "BLAKE2B-512";
			DigestUtilities.algorithms[MiscObjectIdentifiers.id_blake2s128.Id] = "BLAKE2S-128";
			DigestUtilities.algorithms[MiscObjectIdentifiers.id_blake2s160.Id] = "BLAKE2S-160";
			DigestUtilities.algorithms[MiscObjectIdentifiers.id_blake2s224.Id] = "BLAKE2S-224";
			DigestUtilities.algorithms[MiscObjectIdentifiers.id_blake2s256.Id] = "BLAKE2S-256";
			DigestUtilities.algorithms[RosstandartObjectIdentifiers.id_tc26_gost_3411_12_256.Id] = "GOST3411-2012-256";
			DigestUtilities.algorithms[RosstandartObjectIdentifiers.id_tc26_gost_3411_12_512.Id] = "GOST3411-2012-512";
			DigestUtilities.algorithms[UAObjectIdentifiers.dstu7564digest_256.Id] = "DSTU7564-256";
			DigestUtilities.algorithms[UAObjectIdentifiers.dstu7564digest_384.Id] = "DSTU7564-384";
			DigestUtilities.algorithms[UAObjectIdentifiers.dstu7564digest_512.Id] = "DSTU7564-512";
			DigestUtilities.oids["MD2"] = PkcsObjectIdentifiers.MD2;
			DigestUtilities.oids["MD4"] = PkcsObjectIdentifiers.MD4;
			DigestUtilities.oids["MD5"] = PkcsObjectIdentifiers.MD5;
			DigestUtilities.oids["SHA-1"] = OiwObjectIdentifiers.IdSha1;
			DigestUtilities.oids["SHA-224"] = NistObjectIdentifiers.IdSha224;
			DigestUtilities.oids["SHA-256"] = NistObjectIdentifiers.IdSha256;
			DigestUtilities.oids["SHA-384"] = NistObjectIdentifiers.IdSha384;
			DigestUtilities.oids["SHA-512"] = NistObjectIdentifiers.IdSha512;
			DigestUtilities.oids["SHA-512/224"] = NistObjectIdentifiers.IdSha512_224;
			DigestUtilities.oids["SHA-512/256"] = NistObjectIdentifiers.IdSha512_256;
			DigestUtilities.oids["SHA3-224"] = NistObjectIdentifiers.IdSha3_224;
			DigestUtilities.oids["SHA3-256"] = NistObjectIdentifiers.IdSha3_256;
			DigestUtilities.oids["SHA3-384"] = NistObjectIdentifiers.IdSha3_384;
			DigestUtilities.oids["SHA3-512"] = NistObjectIdentifiers.IdSha3_512;
			DigestUtilities.oids["SHAKE128"] = NistObjectIdentifiers.IdShake128;
			DigestUtilities.oids["SHAKE256"] = NistObjectIdentifiers.IdShake256;
			DigestUtilities.oids["RIPEMD128"] = TeleTrusTObjectIdentifiers.RipeMD128;
			DigestUtilities.oids["RIPEMD160"] = TeleTrusTObjectIdentifiers.RipeMD160;
			DigestUtilities.oids["RIPEMD256"] = TeleTrusTObjectIdentifiers.RipeMD256;
			DigestUtilities.oids["GOST3411"] = CryptoProObjectIdentifiers.GostR3411;
			DigestUtilities.oids["SM3"] = GMObjectIdentifiers.sm3;
			DigestUtilities.oids["BLAKE2B-160"] = MiscObjectIdentifiers.id_blake2b160;
			DigestUtilities.oids["BLAKE2B-256"] = MiscObjectIdentifiers.id_blake2b256;
			DigestUtilities.oids["BLAKE2B-384"] = MiscObjectIdentifiers.id_blake2b384;
			DigestUtilities.oids["BLAKE2B-512"] = MiscObjectIdentifiers.id_blake2b512;
			DigestUtilities.oids["BLAKE2S-128"] = MiscObjectIdentifiers.id_blake2s128;
			DigestUtilities.oids["BLAKE2S-160"] = MiscObjectIdentifiers.id_blake2s160;
			DigestUtilities.oids["BLAKE2S-224"] = MiscObjectIdentifiers.id_blake2s224;
			DigestUtilities.oids["BLAKE2S-256"] = MiscObjectIdentifiers.id_blake2s256;
			DigestUtilities.oids["GOST3411-2012-256"] = RosstandartObjectIdentifiers.id_tc26_gost_3411_12_256;
			DigestUtilities.oids["GOST3411-2012-512"] = RosstandartObjectIdentifiers.id_tc26_gost_3411_12_512;
			DigestUtilities.oids["DSTU7564-256"] = UAObjectIdentifiers.dstu7564digest_256;
			DigestUtilities.oids["DSTU7564-384"] = UAObjectIdentifiers.dstu7564digest_384;
			DigestUtilities.oids["DSTU7564-512"] = UAObjectIdentifiers.dstu7564digest_512;
		}

		// Token: 0x06003BAB RID: 15275 RVA: 0x00146284 File Offset: 0x00146284
		public static DerObjectIdentifier GetObjectIdentifier(string mechanism)
		{
			if (mechanism == null)
			{
				throw new ArgumentNullException("mechanism");
			}
			mechanism = Platform.ToUpperInvariant(mechanism);
			string text = (string)DigestUtilities.algorithms[mechanism];
			if (text != null)
			{
				mechanism = text;
			}
			return (DerObjectIdentifier)DigestUtilities.oids[mechanism];
		}

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x06003BAC RID: 15276 RVA: 0x001462D8 File Offset: 0x001462D8
		public static ICollection Algorithms
		{
			get
			{
				return DigestUtilities.oids.Keys;
			}
		}

		// Token: 0x06003BAD RID: 15277 RVA: 0x001462E4 File Offset: 0x001462E4
		public static IDigest GetDigest(DerObjectIdentifier id)
		{
			return DigestUtilities.GetDigest(id.Id);
		}

		// Token: 0x06003BAE RID: 15278 RVA: 0x001462F4 File Offset: 0x001462F4
		public static IDigest GetDigest(string algorithm)
		{
			string text = Platform.ToUpperInvariant(algorithm);
			string text2 = (string)DigestUtilities.algorithms[text];
			if (text2 == null)
			{
				text2 = text;
			}
			try
			{
				switch ((DigestUtilities.DigestAlgorithm)Enums.GetEnumValue(typeof(DigestUtilities.DigestAlgorithm), text2))
				{
				case DigestUtilities.DigestAlgorithm.BLAKE2B_160:
					return new Blake2bDigest(160);
				case DigestUtilities.DigestAlgorithm.BLAKE2B_256:
					return new Blake2bDigest(256);
				case DigestUtilities.DigestAlgorithm.BLAKE2B_384:
					return new Blake2bDigest(384);
				case DigestUtilities.DigestAlgorithm.BLAKE2B_512:
					return new Blake2bDigest(512);
				case DigestUtilities.DigestAlgorithm.BLAKE2S_128:
					return new Blake2sDigest(128);
				case DigestUtilities.DigestAlgorithm.BLAKE2S_160:
					return new Blake2sDigest(160);
				case DigestUtilities.DigestAlgorithm.BLAKE2S_224:
					return new Blake2sDigest(224);
				case DigestUtilities.DigestAlgorithm.BLAKE2S_256:
					return new Blake2sDigest(256);
				case DigestUtilities.DigestAlgorithm.DSTU7564_256:
					return new Dstu7564Digest(256);
				case DigestUtilities.DigestAlgorithm.DSTU7564_384:
					return new Dstu7564Digest(384);
				case DigestUtilities.DigestAlgorithm.DSTU7564_512:
					return new Dstu7564Digest(512);
				case DigestUtilities.DigestAlgorithm.GOST3411:
					return new Gost3411Digest();
				case DigestUtilities.DigestAlgorithm.GOST3411_2012_256:
					return new Gost3411_2012_256Digest();
				case DigestUtilities.DigestAlgorithm.GOST3411_2012_512:
					return new Gost3411_2012_512Digest();
				case DigestUtilities.DigestAlgorithm.KECCAK_224:
					return new KeccakDigest(224);
				case DigestUtilities.DigestAlgorithm.KECCAK_256:
					return new KeccakDigest(256);
				case DigestUtilities.DigestAlgorithm.KECCAK_288:
					return new KeccakDigest(288);
				case DigestUtilities.DigestAlgorithm.KECCAK_384:
					return new KeccakDigest(384);
				case DigestUtilities.DigestAlgorithm.KECCAK_512:
					return new KeccakDigest(512);
				case DigestUtilities.DigestAlgorithm.MD2:
					return new MD2Digest();
				case DigestUtilities.DigestAlgorithm.MD4:
					return new MD4Digest();
				case DigestUtilities.DigestAlgorithm.MD5:
					return new MD5Digest();
				case DigestUtilities.DigestAlgorithm.NONE:
					return new NullDigest();
				case DigestUtilities.DigestAlgorithm.RIPEMD128:
					return new RipeMD128Digest();
				case DigestUtilities.DigestAlgorithm.RIPEMD160:
					return new RipeMD160Digest();
				case DigestUtilities.DigestAlgorithm.RIPEMD256:
					return new RipeMD256Digest();
				case DigestUtilities.DigestAlgorithm.RIPEMD320:
					return new RipeMD320Digest();
				case DigestUtilities.DigestAlgorithm.SHA_1:
					return new Sha1Digest();
				case DigestUtilities.DigestAlgorithm.SHA_224:
					return new Sha224Digest();
				case DigestUtilities.DigestAlgorithm.SHA_256:
					return new Sha256Digest();
				case DigestUtilities.DigestAlgorithm.SHA_384:
					return new Sha384Digest();
				case DigestUtilities.DigestAlgorithm.SHA_512:
					return new Sha512Digest();
				case DigestUtilities.DigestAlgorithm.SHA_512_224:
					return new Sha512tDigest(224);
				case DigestUtilities.DigestAlgorithm.SHA_512_256:
					return new Sha512tDigest(256);
				case DigestUtilities.DigestAlgorithm.SHA3_224:
					return new Sha3Digest(224);
				case DigestUtilities.DigestAlgorithm.SHA3_256:
					return new Sha3Digest(256);
				case DigestUtilities.DigestAlgorithm.SHA3_384:
					return new Sha3Digest(384);
				case DigestUtilities.DigestAlgorithm.SHA3_512:
					return new Sha3Digest(512);
				case DigestUtilities.DigestAlgorithm.SHAKE128:
					return new ShakeDigest(128);
				case DigestUtilities.DigestAlgorithm.SHAKE256:
					return new ShakeDigest(256);
				case DigestUtilities.DigestAlgorithm.SM3:
					return new SM3Digest();
				case DigestUtilities.DigestAlgorithm.TIGER:
					return new TigerDigest();
				case DigestUtilities.DigestAlgorithm.WHIRLPOOL:
					return new WhirlpoolDigest();
				}
			}
			catch (ArgumentException)
			{
			}
			throw new SecurityUtilityException("Digest " + text2 + " not recognised.");
		}

		// Token: 0x06003BAF RID: 15279 RVA: 0x001466AC File Offset: 0x001466AC
		public static string GetAlgorithmName(DerObjectIdentifier oid)
		{
			return (string)DigestUtilities.algorithms[oid.Id];
		}

		// Token: 0x06003BB0 RID: 15280 RVA: 0x001466C4 File Offset: 0x001466C4
		public static byte[] CalculateDigest(DerObjectIdentifier id, byte[] input)
		{
			return DigestUtilities.CalculateDigest(id.Id, input);
		}

		// Token: 0x06003BB1 RID: 15281 RVA: 0x001466D4 File Offset: 0x001466D4
		public static byte[] CalculateDigest(string algorithm, byte[] input)
		{
			IDigest digest = DigestUtilities.GetDigest(algorithm);
			digest.BlockUpdate(input, 0, input.Length);
			return DigestUtilities.DoFinal(digest);
		}

		// Token: 0x06003BB2 RID: 15282 RVA: 0x00146700 File Offset: 0x00146700
		public static byte[] DoFinal(IDigest digest)
		{
			byte[] array = new byte[digest.GetDigestSize()];
			digest.DoFinal(array, 0);
			return array;
		}

		// Token: 0x06003BB3 RID: 15283 RVA: 0x00146728 File Offset: 0x00146728
		public static byte[] DoFinal(IDigest digest, byte[] input)
		{
			digest.BlockUpdate(input, 0, input.Length);
			return DigestUtilities.DoFinal(digest);
		}

		// Token: 0x04001E97 RID: 7831
		private static readonly IDictionary algorithms = Platform.CreateHashtable();

		// Token: 0x04001E98 RID: 7832
		private static readonly IDictionary oids = Platform.CreateHashtable();

		// Token: 0x02000E6C RID: 3692
		private enum DigestAlgorithm
		{
			// Token: 0x040042B1 RID: 17073
			BLAKE2B_160,
			// Token: 0x040042B2 RID: 17074
			BLAKE2B_256,
			// Token: 0x040042B3 RID: 17075
			BLAKE2B_384,
			// Token: 0x040042B4 RID: 17076
			BLAKE2B_512,
			// Token: 0x040042B5 RID: 17077
			BLAKE2S_128,
			// Token: 0x040042B6 RID: 17078
			BLAKE2S_160,
			// Token: 0x040042B7 RID: 17079
			BLAKE2S_224,
			// Token: 0x040042B8 RID: 17080
			BLAKE2S_256,
			// Token: 0x040042B9 RID: 17081
			DSTU7564_256,
			// Token: 0x040042BA RID: 17082
			DSTU7564_384,
			// Token: 0x040042BB RID: 17083
			DSTU7564_512,
			// Token: 0x040042BC RID: 17084
			GOST3411,
			// Token: 0x040042BD RID: 17085
			GOST3411_2012_256,
			// Token: 0x040042BE RID: 17086
			GOST3411_2012_512,
			// Token: 0x040042BF RID: 17087
			KECCAK_224,
			// Token: 0x040042C0 RID: 17088
			KECCAK_256,
			// Token: 0x040042C1 RID: 17089
			KECCAK_288,
			// Token: 0x040042C2 RID: 17090
			KECCAK_384,
			// Token: 0x040042C3 RID: 17091
			KECCAK_512,
			// Token: 0x040042C4 RID: 17092
			MD2,
			// Token: 0x040042C5 RID: 17093
			MD4,
			// Token: 0x040042C6 RID: 17094
			MD5,
			// Token: 0x040042C7 RID: 17095
			NONE,
			// Token: 0x040042C8 RID: 17096
			RIPEMD128,
			// Token: 0x040042C9 RID: 17097
			RIPEMD160,
			// Token: 0x040042CA RID: 17098
			RIPEMD256,
			// Token: 0x040042CB RID: 17099
			RIPEMD320,
			// Token: 0x040042CC RID: 17100
			SHA_1,
			// Token: 0x040042CD RID: 17101
			SHA_224,
			// Token: 0x040042CE RID: 17102
			SHA_256,
			// Token: 0x040042CF RID: 17103
			SHA_384,
			// Token: 0x040042D0 RID: 17104
			SHA_512,
			// Token: 0x040042D1 RID: 17105
			SHA_512_224,
			// Token: 0x040042D2 RID: 17106
			SHA_512_256,
			// Token: 0x040042D3 RID: 17107
			SHA3_224,
			// Token: 0x040042D4 RID: 17108
			SHA3_256,
			// Token: 0x040042D5 RID: 17109
			SHA3_384,
			// Token: 0x040042D6 RID: 17110
			SHA3_512,
			// Token: 0x040042D7 RID: 17111
			SHAKE128,
			// Token: 0x040042D8 RID: 17112
			SHAKE256,
			// Token: 0x040042D9 RID: 17113
			SM3,
			// Token: 0x040042DA RID: 17114
			TIGER,
			// Token: 0x040042DB RID: 17115
			WHIRLPOOL
		}
	}
}
