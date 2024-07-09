using System;
using System.Collections;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.CryptoPro;
using Org.BouncyCastle.Asn1.Kisa;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.Ntt;
using Org.BouncyCastle.Asn1.Oiw;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Agreement;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Security
{
	// Token: 0x020006A9 RID: 1705
	public sealed class CipherUtilities
	{
		// Token: 0x06003BA0 RID: 15264 RVA: 0x0014472C File Offset: 0x0014472C
		static CipherUtilities()
		{
			((CipherUtilities.CipherAlgorithm)Enums.GetArbitraryValue(typeof(CipherUtilities.CipherAlgorithm))).ToString();
			((CipherUtilities.CipherMode)Enums.GetArbitraryValue(typeof(CipherUtilities.CipherMode))).ToString();
			((CipherUtilities.CipherPadding)Enums.GetArbitraryValue(typeof(CipherUtilities.CipherPadding))).ToString();
			CipherUtilities.algorithms[NistObjectIdentifiers.IdAes128Ecb.Id] = "AES/ECB/PKCS7PADDING";
			CipherUtilities.algorithms[NistObjectIdentifiers.IdAes192Ecb.Id] = "AES/ECB/PKCS7PADDING";
			CipherUtilities.algorithms[NistObjectIdentifiers.IdAes256Ecb.Id] = "AES/ECB/PKCS7PADDING";
			CipherUtilities.algorithms["AES//PKCS7"] = "AES/ECB/PKCS7PADDING";
			CipherUtilities.algorithms["AES//PKCS7PADDING"] = "AES/ECB/PKCS7PADDING";
			CipherUtilities.algorithms["AES//PKCS5"] = "AES/ECB/PKCS7PADDING";
			CipherUtilities.algorithms["AES//PKCS5PADDING"] = "AES/ECB/PKCS7PADDING";
			CipherUtilities.algorithms[NistObjectIdentifiers.IdAes128Cbc.Id] = "AES/CBC/PKCS7PADDING";
			CipherUtilities.algorithms[NistObjectIdentifiers.IdAes192Cbc.Id] = "AES/CBC/PKCS7PADDING";
			CipherUtilities.algorithms[NistObjectIdentifiers.IdAes256Cbc.Id] = "AES/CBC/PKCS7PADDING";
			CipherUtilities.algorithms[NistObjectIdentifiers.IdAes128Ofb.Id] = "AES/OFB/NOPADDING";
			CipherUtilities.algorithms[NistObjectIdentifiers.IdAes192Ofb.Id] = "AES/OFB/NOPADDING";
			CipherUtilities.algorithms[NistObjectIdentifiers.IdAes256Ofb.Id] = "AES/OFB/NOPADDING";
			CipherUtilities.algorithms[NistObjectIdentifiers.IdAes128Cfb.Id] = "AES/CFB/NOPADDING";
			CipherUtilities.algorithms[NistObjectIdentifiers.IdAes192Cfb.Id] = "AES/CFB/NOPADDING";
			CipherUtilities.algorithms[NistObjectIdentifiers.IdAes256Cfb.Id] = "AES/CFB/NOPADDING";
			CipherUtilities.algorithms["RSA/ECB/PKCS1"] = "RSA//PKCS1PADDING";
			CipherUtilities.algorithms["RSA/ECB/PKCS1PADDING"] = "RSA//PKCS1PADDING";
			CipherUtilities.algorithms[PkcsObjectIdentifiers.RsaEncryption.Id] = "RSA//PKCS1PADDING";
			CipherUtilities.algorithms[PkcsObjectIdentifiers.IdRsaesOaep.Id] = "RSA//OAEPPADDING";
			CipherUtilities.algorithms[OiwObjectIdentifiers.DesCbc.Id] = "DES/CBC";
			CipherUtilities.algorithms[OiwObjectIdentifiers.DesCfb.Id] = "DES/CFB";
			CipherUtilities.algorithms[OiwObjectIdentifiers.DesEcb.Id] = "DES/ECB";
			CipherUtilities.algorithms[OiwObjectIdentifiers.DesOfb.Id] = "DES/OFB";
			CipherUtilities.algorithms[OiwObjectIdentifiers.DesEde.Id] = "DESEDE";
			CipherUtilities.algorithms["TDEA"] = "DESEDE";
			CipherUtilities.algorithms[PkcsObjectIdentifiers.DesEde3Cbc.Id] = "DESEDE/CBC";
			CipherUtilities.algorithms[PkcsObjectIdentifiers.RC2Cbc.Id] = "RC2/CBC";
			CipherUtilities.algorithms["1.3.6.1.4.1.188.7.1.1.2"] = "IDEA/CBC";
			CipherUtilities.algorithms["1.2.840.113533.7.66.10"] = "CAST5/CBC";
			CipherUtilities.algorithms["RC4"] = "ARC4";
			CipherUtilities.algorithms["ARCFOUR"] = "ARC4";
			CipherUtilities.algorithms["1.2.840.113549.3.4"] = "ARC4";
			CipherUtilities.algorithms["PBEWITHSHA1AND128BITRC4"] = "PBEWITHSHAAND128BITRC4";
			CipherUtilities.algorithms[PkcsObjectIdentifiers.PbeWithShaAnd128BitRC4.Id] = "PBEWITHSHAAND128BITRC4";
			CipherUtilities.algorithms["PBEWITHSHA1AND40BITRC4"] = "PBEWITHSHAAND40BITRC4";
			CipherUtilities.algorithms[PkcsObjectIdentifiers.PbeWithShaAnd40BitRC4.Id] = "PBEWITHSHAAND40BITRC4";
			CipherUtilities.algorithms["PBEWITHSHA1ANDDES"] = "PBEWITHSHA1ANDDES-CBC";
			CipherUtilities.algorithms[PkcsObjectIdentifiers.PbeWithSha1AndDesCbc.Id] = "PBEWITHSHA1ANDDES-CBC";
			CipherUtilities.algorithms["PBEWITHSHA1ANDRC2"] = "PBEWITHSHA1ANDRC2-CBC";
			CipherUtilities.algorithms[PkcsObjectIdentifiers.PbeWithSha1AndRC2Cbc.Id] = "PBEWITHSHA1ANDRC2-CBC";
			CipherUtilities.algorithms["PBEWITHSHA1AND3-KEYTRIPLEDES-CBC"] = "PBEWITHSHAAND3-KEYTRIPLEDES-CBC";
			CipherUtilities.algorithms["PBEWITHSHAAND3KEYTRIPLEDES"] = "PBEWITHSHAAND3-KEYTRIPLEDES-CBC";
			CipherUtilities.algorithms[PkcsObjectIdentifiers.PbeWithShaAnd3KeyTripleDesCbc.Id] = "PBEWITHSHAAND3-KEYTRIPLEDES-CBC";
			CipherUtilities.algorithms["PBEWITHSHA1ANDDESEDE"] = "PBEWITHSHAAND3-KEYTRIPLEDES-CBC";
			CipherUtilities.algorithms["PBEWITHSHA1AND2-KEYTRIPLEDES-CBC"] = "PBEWITHSHAAND2-KEYTRIPLEDES-CBC";
			CipherUtilities.algorithms[PkcsObjectIdentifiers.PbeWithShaAnd2KeyTripleDesCbc.Id] = "PBEWITHSHAAND2-KEYTRIPLEDES-CBC";
			CipherUtilities.algorithms["PBEWITHSHA1AND128BITRC2-CBC"] = "PBEWITHSHAAND128BITRC2-CBC";
			CipherUtilities.algorithms[PkcsObjectIdentifiers.PbeWithShaAnd128BitRC2Cbc.Id] = "PBEWITHSHAAND128BITRC2-CBC";
			CipherUtilities.algorithms["PBEWITHSHA1AND40BITRC2-CBC"] = "PBEWITHSHAAND40BITRC2-CBC";
			CipherUtilities.algorithms[PkcsObjectIdentifiers.PbewithShaAnd40BitRC2Cbc.Id] = "PBEWITHSHAAND40BITRC2-CBC";
			CipherUtilities.algorithms["PBEWITHSHA1AND128BITAES-CBC-BC"] = "PBEWITHSHAAND128BITAES-CBC-BC";
			CipherUtilities.algorithms["PBEWITHSHA-1AND128BITAES-CBC-BC"] = "PBEWITHSHAAND128BITAES-CBC-BC";
			CipherUtilities.algorithms["PBEWITHSHA1AND192BITAES-CBC-BC"] = "PBEWITHSHAAND192BITAES-CBC-BC";
			CipherUtilities.algorithms["PBEWITHSHA-1AND192BITAES-CBC-BC"] = "PBEWITHSHAAND192BITAES-CBC-BC";
			CipherUtilities.algorithms["PBEWITHSHA1AND256BITAES-CBC-BC"] = "PBEWITHSHAAND256BITAES-CBC-BC";
			CipherUtilities.algorithms["PBEWITHSHA-1AND256BITAES-CBC-BC"] = "PBEWITHSHAAND256BITAES-CBC-BC";
			CipherUtilities.algorithms["PBEWITHSHA-256AND128BITAES-CBC-BC"] = "PBEWITHSHA256AND128BITAES-CBC-BC";
			CipherUtilities.algorithms["PBEWITHSHA-256AND192BITAES-CBC-BC"] = "PBEWITHSHA256AND192BITAES-CBC-BC";
			CipherUtilities.algorithms["PBEWITHSHA-256AND256BITAES-CBC-BC"] = "PBEWITHSHA256AND256BITAES-CBC-BC";
			CipherUtilities.algorithms["GOST"] = "GOST28147";
			CipherUtilities.algorithms["GOST-28147"] = "GOST28147";
			CipherUtilities.algorithms[CryptoProObjectIdentifiers.GostR28147Cbc.Id] = "GOST28147/CBC/PKCS7PADDING";
			CipherUtilities.algorithms["RC5-32"] = "RC5";
			CipherUtilities.algorithms[NttObjectIdentifiers.IdCamellia128Cbc.Id] = "CAMELLIA/CBC/PKCS7PADDING";
			CipherUtilities.algorithms[NttObjectIdentifiers.IdCamellia192Cbc.Id] = "CAMELLIA/CBC/PKCS7PADDING";
			CipherUtilities.algorithms[NttObjectIdentifiers.IdCamellia256Cbc.Id] = "CAMELLIA/CBC/PKCS7PADDING";
			CipherUtilities.algorithms[KisaObjectIdentifiers.IdSeedCbc.Id] = "SEED/CBC/PKCS7PADDING";
			CipherUtilities.algorithms["1.3.6.1.4.1.3029.1.2"] = "BLOWFISH/CBC";
			CipherUtilities.algorithms["CHACHA20"] = "CHACHA7539";
			CipherUtilities.algorithms[PkcsObjectIdentifiers.IdAlgAeadChaCha20Poly1305.Id] = "CHACHA20-POLY1305";
		}

		// Token: 0x06003BA1 RID: 15265 RVA: 0x00144DEC File Offset: 0x00144DEC
		private CipherUtilities()
		{
		}

		// Token: 0x06003BA2 RID: 15266 RVA: 0x00144DF4 File Offset: 0x00144DF4
		public static DerObjectIdentifier GetObjectIdentifier(string mechanism)
		{
			if (mechanism == null)
			{
				throw new ArgumentNullException("mechanism");
			}
			mechanism = Platform.ToUpperInvariant(mechanism);
			string text = (string)CipherUtilities.algorithms[mechanism];
			if (text != null)
			{
				mechanism = text;
			}
			return (DerObjectIdentifier)CipherUtilities.oids[mechanism];
		}

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x06003BA3 RID: 15267 RVA: 0x00144E48 File Offset: 0x00144E48
		public static ICollection Algorithms
		{
			get
			{
				return CipherUtilities.oids.Keys;
			}
		}

		// Token: 0x06003BA4 RID: 15268 RVA: 0x00144E54 File Offset: 0x00144E54
		public static IBufferedCipher GetCipher(DerObjectIdentifier oid)
		{
			return CipherUtilities.GetCipher(oid.Id);
		}

		// Token: 0x06003BA5 RID: 15269 RVA: 0x00144E64 File Offset: 0x00144E64
		public static IBufferedCipher GetCipher(string algorithm)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			algorithm = Platform.ToUpperInvariant(algorithm);
			string text = (string)CipherUtilities.algorithms[algorithm];
			if (text != null)
			{
				algorithm = text;
			}
			IBasicAgreement basicAgreement = null;
			if (algorithm == "IES")
			{
				basicAgreement = new DHBasicAgreement();
			}
			else if (algorithm == "ECIES")
			{
				basicAgreement = new ECDHBasicAgreement();
			}
			if (basicAgreement != null)
			{
				return new BufferedIesCipher(new IesEngine(basicAgreement, new Kdf2BytesGenerator(new Sha1Digest()), new HMac(new Sha1Digest())));
			}
			if (Platform.StartsWith(algorithm, "PBE"))
			{
				if (Platform.EndsWith(algorithm, "-CBC"))
				{
					if (algorithm == "PBEWITHSHA1ANDDES-CBC")
					{
						return new PaddedBufferedBlockCipher(new CbcBlockCipher(new DesEngine()));
					}
					if (algorithm == "PBEWITHSHA1ANDRC2-CBC")
					{
						return new PaddedBufferedBlockCipher(new CbcBlockCipher(new RC2Engine()));
					}
					if (Strings.IsOneOf(algorithm, new string[]
					{
						"PBEWITHSHAAND2-KEYTRIPLEDES-CBC",
						"PBEWITHSHAAND3-KEYTRIPLEDES-CBC"
					}))
					{
						return new PaddedBufferedBlockCipher(new CbcBlockCipher(new DesEdeEngine()));
					}
					if (Strings.IsOneOf(algorithm, new string[]
					{
						"PBEWITHSHAAND128BITRC2-CBC",
						"PBEWITHSHAAND40BITRC2-CBC"
					}))
					{
						return new PaddedBufferedBlockCipher(new CbcBlockCipher(new RC2Engine()));
					}
				}
				else if ((Platform.EndsWith(algorithm, "-BC") || Platform.EndsWith(algorithm, "-OPENSSL")) && Strings.IsOneOf(algorithm, new string[]
				{
					"PBEWITHSHAAND128BITAES-CBC-BC",
					"PBEWITHSHAAND192BITAES-CBC-BC",
					"PBEWITHSHAAND256BITAES-CBC-BC",
					"PBEWITHSHA256AND128BITAES-CBC-BC",
					"PBEWITHSHA256AND192BITAES-CBC-BC",
					"PBEWITHSHA256AND256BITAES-CBC-BC",
					"PBEWITHMD5AND128BITAES-CBC-OPENSSL",
					"PBEWITHMD5AND192BITAES-CBC-OPENSSL",
					"PBEWITHMD5AND256BITAES-CBC-OPENSSL"
				}))
				{
					return new PaddedBufferedBlockCipher(new CbcBlockCipher(new AesEngine()));
				}
			}
			string[] array = algorithm.Split(new char[]
			{
				'/'
			});
			IAeadCipher aeadCipher = null;
			IBlockCipher blockCipher = null;
			IAsymmetricBlockCipher asymmetricBlockCipher = null;
			IStreamCipher streamCipher = null;
			string text2 = array[0];
			string text3 = (string)CipherUtilities.algorithms[text2];
			if (text3 != null)
			{
				text2 = text3;
			}
			CipherUtilities.CipherAlgorithm cipherAlgorithm;
			try
			{
				cipherAlgorithm = (CipherUtilities.CipherAlgorithm)Enums.GetEnumValue(typeof(CipherUtilities.CipherAlgorithm), text2);
			}
			catch (ArgumentException)
			{
				throw new SecurityUtilityException("Cipher " + algorithm + " not recognised.");
			}
			switch (cipherAlgorithm)
			{
			case CipherUtilities.CipherAlgorithm.AES:
				blockCipher = new AesEngine();
				break;
			case CipherUtilities.CipherAlgorithm.ARC4:
				streamCipher = new RC4Engine();
				break;
			case CipherUtilities.CipherAlgorithm.BLOWFISH:
				blockCipher = new BlowfishEngine();
				break;
			case CipherUtilities.CipherAlgorithm.CAMELLIA:
				blockCipher = new CamelliaEngine();
				break;
			case CipherUtilities.CipherAlgorithm.CAST5:
				blockCipher = new Cast5Engine();
				break;
			case CipherUtilities.CipherAlgorithm.CAST6:
				blockCipher = new Cast6Engine();
				break;
			case CipherUtilities.CipherAlgorithm.CHACHA:
				streamCipher = new ChaChaEngine();
				break;
			case CipherUtilities.CipherAlgorithm.CHACHA20_POLY1305:
				aeadCipher = new ChaCha20Poly1305();
				break;
			case CipherUtilities.CipherAlgorithm.CHACHA7539:
				streamCipher = new ChaCha7539Engine();
				break;
			case CipherUtilities.CipherAlgorithm.DES:
				blockCipher = new DesEngine();
				break;
			case CipherUtilities.CipherAlgorithm.DESEDE:
				blockCipher = new DesEdeEngine();
				break;
			case CipherUtilities.CipherAlgorithm.ELGAMAL:
				asymmetricBlockCipher = new ElGamalEngine();
				break;
			case CipherUtilities.CipherAlgorithm.GOST28147:
				blockCipher = new Gost28147Engine();
				break;
			case CipherUtilities.CipherAlgorithm.HC128:
				streamCipher = new HC128Engine();
				break;
			case CipherUtilities.CipherAlgorithm.HC256:
				streamCipher = new HC256Engine();
				break;
			case CipherUtilities.CipherAlgorithm.IDEA:
				blockCipher = new IdeaEngine();
				break;
			case CipherUtilities.CipherAlgorithm.NOEKEON:
				blockCipher = new NoekeonEngine();
				break;
			case CipherUtilities.CipherAlgorithm.PBEWITHSHAAND128BITRC4:
			case CipherUtilities.CipherAlgorithm.PBEWITHSHAAND40BITRC4:
				streamCipher = new RC4Engine();
				break;
			case CipherUtilities.CipherAlgorithm.RC2:
				blockCipher = new RC2Engine();
				break;
			case CipherUtilities.CipherAlgorithm.RC5:
				blockCipher = new RC532Engine();
				break;
			case CipherUtilities.CipherAlgorithm.RC5_64:
				blockCipher = new RC564Engine();
				break;
			case CipherUtilities.CipherAlgorithm.RC6:
				blockCipher = new RC6Engine();
				break;
			case CipherUtilities.CipherAlgorithm.RIJNDAEL:
				blockCipher = new RijndaelEngine();
				break;
			case CipherUtilities.CipherAlgorithm.RSA:
				asymmetricBlockCipher = new RsaBlindedEngine();
				break;
			case CipherUtilities.CipherAlgorithm.SALSA20:
				streamCipher = new Salsa20Engine();
				break;
			case CipherUtilities.CipherAlgorithm.SEED:
				blockCipher = new SeedEngine();
				break;
			case CipherUtilities.CipherAlgorithm.SERPENT:
				blockCipher = new SerpentEngine();
				break;
			case CipherUtilities.CipherAlgorithm.SKIPJACK:
				blockCipher = new SkipjackEngine();
				break;
			case CipherUtilities.CipherAlgorithm.SM4:
				blockCipher = new SM4Engine();
				break;
			case CipherUtilities.CipherAlgorithm.TEA:
				blockCipher = new TeaEngine();
				break;
			case CipherUtilities.CipherAlgorithm.THREEFISH_256:
				blockCipher = new ThreefishEngine(256);
				break;
			case CipherUtilities.CipherAlgorithm.THREEFISH_512:
				blockCipher = new ThreefishEngine(512);
				break;
			case CipherUtilities.CipherAlgorithm.THREEFISH_1024:
				blockCipher = new ThreefishEngine(1024);
				break;
			case CipherUtilities.CipherAlgorithm.TNEPRES:
				blockCipher = new TnepresEngine();
				break;
			case CipherUtilities.CipherAlgorithm.TWOFISH:
				blockCipher = new TwofishEngine();
				break;
			case CipherUtilities.CipherAlgorithm.VMPC:
				streamCipher = new VmpcEngine();
				break;
			case CipherUtilities.CipherAlgorithm.VMPC_KSA3:
				streamCipher = new VmpcKsa3Engine();
				break;
			case CipherUtilities.CipherAlgorithm.XTEA:
				blockCipher = new XteaEngine();
				break;
			default:
				throw new SecurityUtilityException("Cipher " + algorithm + " not recognised.");
			}
			if (aeadCipher != null)
			{
				if (array.Length > 1)
				{
					throw new ArgumentException("Modes and paddings cannot be applied to AEAD ciphers");
				}
				return new BufferedAeadCipher(aeadCipher);
			}
			else if (streamCipher != null)
			{
				if (array.Length > 1)
				{
					throw new ArgumentException("Modes and paddings not used for stream ciphers");
				}
				return new BufferedStreamCipher(streamCipher);
			}
			else
			{
				bool flag = false;
				bool flag2 = true;
				IBlockCipherPadding blockCipherPadding = null;
				IAeadBlockCipher aeadBlockCipher = null;
				if (array.Length > 2)
				{
					if (streamCipher != null)
					{
						throw new ArgumentException("Paddings not used for stream ciphers");
					}
					string text4 = array[2];
					CipherUtilities.CipherPadding cipherPadding;
					if (text4 == "")
					{
						cipherPadding = CipherUtilities.CipherPadding.RAW;
					}
					else if (text4 == "X9.23PADDING")
					{
						cipherPadding = CipherUtilities.CipherPadding.X923PADDING;
					}
					else
					{
						try
						{
							cipherPadding = (CipherUtilities.CipherPadding)Enums.GetEnumValue(typeof(CipherUtilities.CipherPadding), text4);
						}
						catch (ArgumentException)
						{
							throw new SecurityUtilityException("Cipher " + algorithm + " not recognised.");
						}
					}
					switch (cipherPadding)
					{
					case CipherUtilities.CipherPadding.NOPADDING:
						flag2 = false;
						break;
					case CipherUtilities.CipherPadding.RAW:
						break;
					case CipherUtilities.CipherPadding.ISO10126PADDING:
					case CipherUtilities.CipherPadding.ISO10126D2PADDING:
					case CipherUtilities.CipherPadding.ISO10126_2PADDING:
						blockCipherPadding = new ISO10126d2Padding();
						break;
					case CipherUtilities.CipherPadding.ISO7816_4PADDING:
					case CipherUtilities.CipherPadding.ISO9797_1PADDING:
						blockCipherPadding = new ISO7816d4Padding();
						break;
					case CipherUtilities.CipherPadding.ISO9796_1:
					case CipherUtilities.CipherPadding.ISO9796_1PADDING:
						asymmetricBlockCipher = new ISO9796d1Encoding(asymmetricBlockCipher);
						break;
					case CipherUtilities.CipherPadding.OAEP:
					case CipherUtilities.CipherPadding.OAEPPADDING:
						asymmetricBlockCipher = new OaepEncoding(asymmetricBlockCipher);
						break;
					case CipherUtilities.CipherPadding.OAEPWITHMD5ANDMGF1PADDING:
						asymmetricBlockCipher = new OaepEncoding(asymmetricBlockCipher, new MD5Digest());
						break;
					case CipherUtilities.CipherPadding.OAEPWITHSHA1ANDMGF1PADDING:
					case CipherUtilities.CipherPadding.OAEPWITHSHA_1ANDMGF1PADDING:
						asymmetricBlockCipher = new OaepEncoding(asymmetricBlockCipher, new Sha1Digest());
						break;
					case CipherUtilities.CipherPadding.OAEPWITHSHA224ANDMGF1PADDING:
					case CipherUtilities.CipherPadding.OAEPWITHSHA_224ANDMGF1PADDING:
						asymmetricBlockCipher = new OaepEncoding(asymmetricBlockCipher, new Sha224Digest());
						break;
					case CipherUtilities.CipherPadding.OAEPWITHSHA256ANDMGF1PADDING:
					case CipherUtilities.CipherPadding.OAEPWITHSHA_256ANDMGF1PADDING:
						asymmetricBlockCipher = new OaepEncoding(asymmetricBlockCipher, new Sha256Digest());
						break;
					case CipherUtilities.CipherPadding.OAEPWITHSHA384ANDMGF1PADDING:
					case CipherUtilities.CipherPadding.OAEPWITHSHA_384ANDMGF1PADDING:
						asymmetricBlockCipher = new OaepEncoding(asymmetricBlockCipher, new Sha384Digest());
						break;
					case CipherUtilities.CipherPadding.OAEPWITHSHA512ANDMGF1PADDING:
					case CipherUtilities.CipherPadding.OAEPWITHSHA_512ANDMGF1PADDING:
						asymmetricBlockCipher = new OaepEncoding(asymmetricBlockCipher, new Sha512Digest());
						break;
					case CipherUtilities.CipherPadding.PKCS1:
					case CipherUtilities.CipherPadding.PKCS1PADDING:
						asymmetricBlockCipher = new Pkcs1Encoding(asymmetricBlockCipher);
						break;
					case CipherUtilities.CipherPadding.PKCS5:
					case CipherUtilities.CipherPadding.PKCS5PADDING:
					case CipherUtilities.CipherPadding.PKCS7:
					case CipherUtilities.CipherPadding.PKCS7PADDING:
						blockCipherPadding = new Pkcs7Padding();
						break;
					case CipherUtilities.CipherPadding.TBCPADDING:
						blockCipherPadding = new TbcPadding();
						break;
					case CipherUtilities.CipherPadding.WITHCTS:
						flag = true;
						break;
					case CipherUtilities.CipherPadding.X923PADDING:
						blockCipherPadding = new X923Padding();
						break;
					case CipherUtilities.CipherPadding.ZEROBYTEPADDING:
						blockCipherPadding = new ZeroBytePadding();
						break;
					default:
						throw new SecurityUtilityException("Cipher " + algorithm + " not recognised.");
					}
				}
				if (array.Length > 1)
				{
					string text5 = array[1];
					int digitIndex = CipherUtilities.GetDigitIndex(text5);
					string text6 = (digitIndex >= 0) ? text5.Substring(0, digitIndex) : text5;
					try
					{
						switch ((text6 == "") ? CipherUtilities.CipherMode.NONE : ((CipherUtilities.CipherMode)Enums.GetEnumValue(typeof(CipherUtilities.CipherMode), text6)))
						{
						case CipherUtilities.CipherMode.ECB:
						case CipherUtilities.CipherMode.NONE:
							break;
						case CipherUtilities.CipherMode.CBC:
							blockCipher = new CbcBlockCipher(blockCipher);
							break;
						case CipherUtilities.CipherMode.CCM:
							aeadBlockCipher = new CcmBlockCipher(blockCipher);
							break;
						case CipherUtilities.CipherMode.CFB:
						{
							int bitBlockSize = (digitIndex < 0) ? (8 * blockCipher.GetBlockSize()) : int.Parse(text5.Substring(digitIndex));
							blockCipher = new CfbBlockCipher(blockCipher, bitBlockSize);
							break;
						}
						case CipherUtilities.CipherMode.CTR:
							blockCipher = new SicBlockCipher(blockCipher);
							break;
						case CipherUtilities.CipherMode.CTS:
							flag = true;
							blockCipher = new CbcBlockCipher(blockCipher);
							break;
						case CipherUtilities.CipherMode.EAX:
							aeadBlockCipher = new EaxBlockCipher(blockCipher);
							break;
						case CipherUtilities.CipherMode.GCM:
							aeadBlockCipher = new GcmBlockCipher(blockCipher);
							break;
						case CipherUtilities.CipherMode.GOFB:
							blockCipher = new GOfbBlockCipher(blockCipher);
							break;
						case CipherUtilities.CipherMode.OCB:
							aeadBlockCipher = new OcbBlockCipher(blockCipher, CipherUtilities.CreateBlockCipher(cipherAlgorithm));
							break;
						case CipherUtilities.CipherMode.OFB:
						{
							int blockSize = (digitIndex < 0) ? (8 * blockCipher.GetBlockSize()) : int.Parse(text5.Substring(digitIndex));
							blockCipher = new OfbBlockCipher(blockCipher, blockSize);
							break;
						}
						case CipherUtilities.CipherMode.OPENPGPCFB:
							blockCipher = new OpenPgpCfbBlockCipher(blockCipher);
							break;
						case CipherUtilities.CipherMode.SIC:
							if (blockCipher.GetBlockSize() < 16)
							{
								throw new ArgumentException("Warning: SIC-Mode can become a twotime-pad if the blocksize of the cipher is too small. Use a cipher with a block size of at least 128 bits (e.g. AES)");
							}
							blockCipher = new SicBlockCipher(blockCipher);
							break;
						default:
							throw new SecurityUtilityException("Cipher " + algorithm + " not recognised.");
						}
					}
					catch (ArgumentException)
					{
						throw new SecurityUtilityException("Cipher " + algorithm + " not recognised.");
					}
				}
				if (aeadBlockCipher != null)
				{
					if (flag)
					{
						throw new SecurityUtilityException("CTS mode not valid for AEAD ciphers.");
					}
					if (flag2 && array.Length > 2 && array[2] != "")
					{
						throw new SecurityUtilityException("Bad padding specified for AEAD cipher.");
					}
					return new BufferedAeadBlockCipher(aeadBlockCipher);
				}
				else if (blockCipher != null)
				{
					if (flag)
					{
						return new CtsBlockCipher(blockCipher);
					}
					if (blockCipherPadding != null)
					{
						return new PaddedBufferedBlockCipher(blockCipher, blockCipherPadding);
					}
					if (!flag2 || blockCipher.IsPartialBlockOkay)
					{
						return new BufferedBlockCipher(blockCipher);
					}
					return new PaddedBufferedBlockCipher(blockCipher);
				}
				else
				{
					if (asymmetricBlockCipher != null)
					{
						return new BufferedAsymmetricBlockCipher(asymmetricBlockCipher);
					}
					throw new SecurityUtilityException("Cipher " + algorithm + " not recognised.");
				}
			}
		}

		// Token: 0x06003BA6 RID: 15270 RVA: 0x00145928 File Offset: 0x00145928
		public static string GetAlgorithmName(DerObjectIdentifier oid)
		{
			return (string)CipherUtilities.algorithms[oid.Id];
		}

		// Token: 0x06003BA7 RID: 15271 RVA: 0x00145940 File Offset: 0x00145940
		private static int GetDigitIndex(string s)
		{
			for (int i = 0; i < s.Length; i++)
			{
				if (char.IsDigit(s[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06003BA8 RID: 15272 RVA: 0x0014597C File Offset: 0x0014597C
		private static IBlockCipher CreateBlockCipher(CipherUtilities.CipherAlgorithm cipherAlgorithm)
		{
			switch (cipherAlgorithm)
			{
			case CipherUtilities.CipherAlgorithm.AES:
				return new AesEngine();
			case CipherUtilities.CipherAlgorithm.BLOWFISH:
				return new BlowfishEngine();
			case CipherUtilities.CipherAlgorithm.CAMELLIA:
				return new CamelliaEngine();
			case CipherUtilities.CipherAlgorithm.CAST5:
				return new Cast5Engine();
			case CipherUtilities.CipherAlgorithm.CAST6:
				return new Cast6Engine();
			case CipherUtilities.CipherAlgorithm.DES:
				return new DesEngine();
			case CipherUtilities.CipherAlgorithm.DESEDE:
				return new DesEdeEngine();
			case CipherUtilities.CipherAlgorithm.GOST28147:
				return new Gost28147Engine();
			case CipherUtilities.CipherAlgorithm.IDEA:
				return new IdeaEngine();
			case CipherUtilities.CipherAlgorithm.NOEKEON:
				return new NoekeonEngine();
			case CipherUtilities.CipherAlgorithm.RC2:
				return new RC2Engine();
			case CipherUtilities.CipherAlgorithm.RC5:
				return new RC532Engine();
			case CipherUtilities.CipherAlgorithm.RC5_64:
				return new RC564Engine();
			case CipherUtilities.CipherAlgorithm.RC6:
				return new RC6Engine();
			case CipherUtilities.CipherAlgorithm.RIJNDAEL:
				return new RijndaelEngine();
			case CipherUtilities.CipherAlgorithm.SEED:
				return new SeedEngine();
			case CipherUtilities.CipherAlgorithm.SERPENT:
				return new SerpentEngine();
			case CipherUtilities.CipherAlgorithm.SKIPJACK:
				return new SkipjackEngine();
			case CipherUtilities.CipherAlgorithm.SM4:
				return new SM4Engine();
			case CipherUtilities.CipherAlgorithm.TEA:
				return new TeaEngine();
			case CipherUtilities.CipherAlgorithm.THREEFISH_256:
				return new ThreefishEngine(256);
			case CipherUtilities.CipherAlgorithm.THREEFISH_512:
				return new ThreefishEngine(512);
			case CipherUtilities.CipherAlgorithm.THREEFISH_1024:
				return new ThreefishEngine(1024);
			case CipherUtilities.CipherAlgorithm.TNEPRES:
				return new TnepresEngine();
			case CipherUtilities.CipherAlgorithm.TWOFISH:
				return new TwofishEngine();
			case CipherUtilities.CipherAlgorithm.XTEA:
				return new XteaEngine();
			}
			throw new SecurityUtilityException("Cipher " + cipherAlgorithm + " not recognised or not a block cipher");
		}

		// Token: 0x04001E95 RID: 7829
		private static readonly IDictionary algorithms = Platform.CreateHashtable();

		// Token: 0x04001E96 RID: 7830
		private static readonly IDictionary oids = Platform.CreateHashtable();

		// Token: 0x02000E69 RID: 3689
		private enum CipherAlgorithm
		{
			// Token: 0x04004259 RID: 16985
			AES,
			// Token: 0x0400425A RID: 16986
			ARC4,
			// Token: 0x0400425B RID: 16987
			BLOWFISH,
			// Token: 0x0400425C RID: 16988
			CAMELLIA,
			// Token: 0x0400425D RID: 16989
			CAST5,
			// Token: 0x0400425E RID: 16990
			CAST6,
			// Token: 0x0400425F RID: 16991
			CHACHA,
			// Token: 0x04004260 RID: 16992
			CHACHA20_POLY1305,
			// Token: 0x04004261 RID: 16993
			CHACHA7539,
			// Token: 0x04004262 RID: 16994
			DES,
			// Token: 0x04004263 RID: 16995
			DESEDE,
			// Token: 0x04004264 RID: 16996
			ELGAMAL,
			// Token: 0x04004265 RID: 16997
			GOST28147,
			// Token: 0x04004266 RID: 16998
			HC128,
			// Token: 0x04004267 RID: 16999
			HC256,
			// Token: 0x04004268 RID: 17000
			IDEA,
			// Token: 0x04004269 RID: 17001
			NOEKEON,
			// Token: 0x0400426A RID: 17002
			PBEWITHSHAAND128BITRC4,
			// Token: 0x0400426B RID: 17003
			PBEWITHSHAAND40BITRC4,
			// Token: 0x0400426C RID: 17004
			RC2,
			// Token: 0x0400426D RID: 17005
			RC5,
			// Token: 0x0400426E RID: 17006
			RC5_64,
			// Token: 0x0400426F RID: 17007
			RC6,
			// Token: 0x04004270 RID: 17008
			RIJNDAEL,
			// Token: 0x04004271 RID: 17009
			RSA,
			// Token: 0x04004272 RID: 17010
			SALSA20,
			// Token: 0x04004273 RID: 17011
			SEED,
			// Token: 0x04004274 RID: 17012
			SERPENT,
			// Token: 0x04004275 RID: 17013
			SKIPJACK,
			// Token: 0x04004276 RID: 17014
			SM4,
			// Token: 0x04004277 RID: 17015
			TEA,
			// Token: 0x04004278 RID: 17016
			THREEFISH_256,
			// Token: 0x04004279 RID: 17017
			THREEFISH_512,
			// Token: 0x0400427A RID: 17018
			THREEFISH_1024,
			// Token: 0x0400427B RID: 17019
			TNEPRES,
			// Token: 0x0400427C RID: 17020
			TWOFISH,
			// Token: 0x0400427D RID: 17021
			VMPC,
			// Token: 0x0400427E RID: 17022
			VMPC_KSA3,
			// Token: 0x0400427F RID: 17023
			XTEA
		}

		// Token: 0x02000E6A RID: 3690
		private enum CipherMode
		{
			// Token: 0x04004281 RID: 17025
			ECB,
			// Token: 0x04004282 RID: 17026
			NONE,
			// Token: 0x04004283 RID: 17027
			CBC,
			// Token: 0x04004284 RID: 17028
			CCM,
			// Token: 0x04004285 RID: 17029
			CFB,
			// Token: 0x04004286 RID: 17030
			CTR,
			// Token: 0x04004287 RID: 17031
			CTS,
			// Token: 0x04004288 RID: 17032
			EAX,
			// Token: 0x04004289 RID: 17033
			GCM,
			// Token: 0x0400428A RID: 17034
			GOFB,
			// Token: 0x0400428B RID: 17035
			OCB,
			// Token: 0x0400428C RID: 17036
			OFB,
			// Token: 0x0400428D RID: 17037
			OPENPGPCFB,
			// Token: 0x0400428E RID: 17038
			SIC
		}

		// Token: 0x02000E6B RID: 3691
		private enum CipherPadding
		{
			// Token: 0x04004290 RID: 17040
			NOPADDING,
			// Token: 0x04004291 RID: 17041
			RAW,
			// Token: 0x04004292 RID: 17042
			ISO10126PADDING,
			// Token: 0x04004293 RID: 17043
			ISO10126D2PADDING,
			// Token: 0x04004294 RID: 17044
			ISO10126_2PADDING,
			// Token: 0x04004295 RID: 17045
			ISO7816_4PADDING,
			// Token: 0x04004296 RID: 17046
			ISO9797_1PADDING,
			// Token: 0x04004297 RID: 17047
			ISO9796_1,
			// Token: 0x04004298 RID: 17048
			ISO9796_1PADDING,
			// Token: 0x04004299 RID: 17049
			OAEP,
			// Token: 0x0400429A RID: 17050
			OAEPPADDING,
			// Token: 0x0400429B RID: 17051
			OAEPWITHMD5ANDMGF1PADDING,
			// Token: 0x0400429C RID: 17052
			OAEPWITHSHA1ANDMGF1PADDING,
			// Token: 0x0400429D RID: 17053
			OAEPWITHSHA_1ANDMGF1PADDING,
			// Token: 0x0400429E RID: 17054
			OAEPWITHSHA224ANDMGF1PADDING,
			// Token: 0x0400429F RID: 17055
			OAEPWITHSHA_224ANDMGF1PADDING,
			// Token: 0x040042A0 RID: 17056
			OAEPWITHSHA256ANDMGF1PADDING,
			// Token: 0x040042A1 RID: 17057
			OAEPWITHSHA_256ANDMGF1PADDING,
			// Token: 0x040042A2 RID: 17058
			OAEPWITHSHA384ANDMGF1PADDING,
			// Token: 0x040042A3 RID: 17059
			OAEPWITHSHA_384ANDMGF1PADDING,
			// Token: 0x040042A4 RID: 17060
			OAEPWITHSHA512ANDMGF1PADDING,
			// Token: 0x040042A5 RID: 17061
			OAEPWITHSHA_512ANDMGF1PADDING,
			// Token: 0x040042A6 RID: 17062
			PKCS1,
			// Token: 0x040042A7 RID: 17063
			PKCS1PADDING,
			// Token: 0x040042A8 RID: 17064
			PKCS5,
			// Token: 0x040042A9 RID: 17065
			PKCS5PADDING,
			// Token: 0x040042AA RID: 17066
			PKCS7,
			// Token: 0x040042AB RID: 17067
			PKCS7PADDING,
			// Token: 0x040042AC RID: 17068
			TBCPADDING,
			// Token: 0x040042AD RID: 17069
			WITHCTS,
			// Token: 0x040042AE RID: 17070
			X923PADDING,
			// Token: 0x040042AF RID: 17071
			ZEROBYTEPADDING
		}
	}
}
