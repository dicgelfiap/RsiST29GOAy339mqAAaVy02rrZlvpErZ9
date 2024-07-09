using System;
using System.Collections;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.BC;
using Org.BouncyCastle.Asn1.Bsi;
using Org.BouncyCastle.Asn1.CryptoPro;
using Org.BouncyCastle.Asn1.Eac;
using Org.BouncyCastle.Asn1.GM;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.Oiw;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.Rosstandart;
using Org.BouncyCastle.Asn1.TeleTrust;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020002F6 RID: 758
	public class DefaultSignatureAlgorithmIdentifierFinder
	{
		// Token: 0x060016FD RID: 5885 RVA: 0x00077024 File Offset: 0x00077024
		static DefaultSignatureAlgorithmIdentifierFinder()
		{
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["MD2WITHRSAENCRYPTION"] = PkcsObjectIdentifiers.MD2WithRsaEncryption;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["MD2WITHRSA"] = PkcsObjectIdentifiers.MD2WithRsaEncryption;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["MD5WITHRSAENCRYPTION"] = PkcsObjectIdentifiers.MD5WithRsaEncryption;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["MD5WITHRSA"] = PkcsObjectIdentifiers.MD5WithRsaEncryption;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA1WITHRSAENCRYPTION"] = PkcsObjectIdentifiers.Sha1WithRsaEncryption;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA1WITHRSA"] = PkcsObjectIdentifiers.Sha1WithRsaEncryption;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA-1WITHRSA"] = PkcsObjectIdentifiers.Sha1WithRsaEncryption;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA224WITHRSAENCRYPTION"] = PkcsObjectIdentifiers.Sha224WithRsaEncryption;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA224WITHRSA"] = PkcsObjectIdentifiers.Sha224WithRsaEncryption;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA256WITHRSAENCRYPTION"] = PkcsObjectIdentifiers.Sha256WithRsaEncryption;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA256WITHRSA"] = PkcsObjectIdentifiers.Sha256WithRsaEncryption;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA384WITHRSAENCRYPTION"] = PkcsObjectIdentifiers.Sha384WithRsaEncryption;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA384WITHRSA"] = PkcsObjectIdentifiers.Sha384WithRsaEncryption;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA512WITHRSAENCRYPTION"] = PkcsObjectIdentifiers.Sha512WithRsaEncryption;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA512WITHRSA"] = PkcsObjectIdentifiers.Sha512WithRsaEncryption;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA1WITHRSAANDMGF1"] = PkcsObjectIdentifiers.IdRsassaPss;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA224WITHRSAANDMGF1"] = PkcsObjectIdentifiers.IdRsassaPss;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA256WITHRSAANDMGF1"] = PkcsObjectIdentifiers.IdRsassaPss;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA384WITHRSAANDMGF1"] = PkcsObjectIdentifiers.IdRsassaPss;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA512WITHRSAANDMGF1"] = PkcsObjectIdentifiers.IdRsassaPss;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA3-224WITHRSAANDMGF1"] = PkcsObjectIdentifiers.IdRsassaPss;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA3-256WITHRSAANDMGF1"] = PkcsObjectIdentifiers.IdRsassaPss;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA3-384WITHRSAANDMGF1"] = PkcsObjectIdentifiers.IdRsassaPss;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA3-512WITHRSAANDMGF1"] = PkcsObjectIdentifiers.IdRsassaPss;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["RIPEMD160WITHRSAENCRYPTION"] = TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD160;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["RIPEMD160WITHRSA"] = TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD160;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["RIPEMD128WITHRSAENCRYPTION"] = TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD128;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["RIPEMD128WITHRSA"] = TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD128;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["RIPEMD256WITHRSAENCRYPTION"] = TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD256;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["RIPEMD256WITHRSA"] = TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD256;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA1WITHDSA"] = X9ObjectIdentifiers.IdDsaWithSha1;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA-1WITHDSA"] = X9ObjectIdentifiers.IdDsaWithSha1;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["DSAWITHSHA1"] = X9ObjectIdentifiers.IdDsaWithSha1;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA224WITHDSA"] = NistObjectIdentifiers.DsaWithSha224;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA256WITHDSA"] = NistObjectIdentifiers.DsaWithSha256;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA384WITHDSA"] = NistObjectIdentifiers.DsaWithSha384;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA512WITHDSA"] = NistObjectIdentifiers.DsaWithSha512;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA3-224WITHDSA"] = NistObjectIdentifiers.IdDsaWithSha3_224;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA3-256WITHDSA"] = NistObjectIdentifiers.IdDsaWithSha3_256;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA3-384WITHDSA"] = NistObjectIdentifiers.IdDsaWithSha3_384;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA3-512WITHDSA"] = NistObjectIdentifiers.IdDsaWithSha3_512;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA3-224WITHECDSA"] = NistObjectIdentifiers.IdEcdsaWithSha3_224;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA3-256WITHECDSA"] = NistObjectIdentifiers.IdEcdsaWithSha3_256;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA3-384WITHECDSA"] = NistObjectIdentifiers.IdEcdsaWithSha3_384;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA3-512WITHECDSA"] = NistObjectIdentifiers.IdEcdsaWithSha3_512;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA3-224WITHRSA"] = NistObjectIdentifiers.IdRsassaPkcs1V15WithSha3_224;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA3-256WITHRSA"] = NistObjectIdentifiers.IdRsassaPkcs1V15WithSha3_256;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA3-384WITHRSA"] = NistObjectIdentifiers.IdRsassaPkcs1V15WithSha3_384;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA3-512WITHRSA"] = NistObjectIdentifiers.IdRsassaPkcs1V15WithSha3_512;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA3-224WITHRSAENCRYPTION"] = NistObjectIdentifiers.IdRsassaPkcs1V15WithSha3_224;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA3-256WITHRSAENCRYPTION"] = NistObjectIdentifiers.IdRsassaPkcs1V15WithSha3_256;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA3-384WITHRSAENCRYPTION"] = NistObjectIdentifiers.IdRsassaPkcs1V15WithSha3_384;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA3-512WITHRSAENCRYPTION"] = NistObjectIdentifiers.IdRsassaPkcs1V15WithSha3_512;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA1WITHECDSA"] = X9ObjectIdentifiers.ECDsaWithSha1;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["ECDSAWITHSHA1"] = X9ObjectIdentifiers.ECDsaWithSha1;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA224WITHECDSA"] = X9ObjectIdentifiers.ECDsaWithSha224;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA256WITHECDSA"] = X9ObjectIdentifiers.ECDsaWithSha224;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA384WITHECDSA"] = X9ObjectIdentifiers.ECDsaWithSha384;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA512WITHECDSA"] = X9ObjectIdentifiers.ECDsaWithSha256;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["GOST3411WITHGOST3410"] = CryptoProObjectIdentifiers.GostR3411x94WithGostR3410x94;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["GOST3411WITHGOST3410-94"] = CryptoProObjectIdentifiers.GostR3411x94WithGostR3410x94;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["GOST3411WITHECGOST3410"] = CryptoProObjectIdentifiers.GostR3411x94WithGostR3410x2001;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["GOST3411WITHECGOST3410-2001"] = CryptoProObjectIdentifiers.GostR3411x94WithGostR3410x2001;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["GOST3411WITHGOST3410-2001"] = CryptoProObjectIdentifiers.GostR3411x94WithGostR3410x2001;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["GOST3411WITHECGOST3410-2012-256"] = RosstandartObjectIdentifiers.id_tc26_signwithdigest_gost_3410_12_256;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["GOST3411WITHECGOST3410-2012-512"] = RosstandartObjectIdentifiers.id_tc26_signwithdigest_gost_3410_12_512;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["GOST3411WITHGOST3410-2012-256"] = RosstandartObjectIdentifiers.id_tc26_signwithdigest_gost_3410_12_256;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["GOST3411WITHGOST3410-2012-512"] = RosstandartObjectIdentifiers.id_tc26_signwithdigest_gost_3410_12_512;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["GOST3411-2012-256WITHECGOST3410-2012-256"] = RosstandartObjectIdentifiers.id_tc26_signwithdigest_gost_3410_12_256;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["GOST3411-2012-512WITHECGOST3410-2012-512"] = RosstandartObjectIdentifiers.id_tc26_signwithdigest_gost_3410_12_512;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["GOST3411-2012-256WITHGOST3410-2012-256"] = RosstandartObjectIdentifiers.id_tc26_signwithdigest_gost_3410_12_256;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["GOST3411-2012-512WITHGOST3410-2012-512"] = RosstandartObjectIdentifiers.id_tc26_signwithdigest_gost_3410_12_512;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA1WITHPLAIN-ECDSA"] = BsiObjectIdentifiers.ecdsa_plain_SHA1;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA224WITHPLAIN-ECDSA"] = BsiObjectIdentifiers.ecdsa_plain_SHA224;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA256WITHPLAIN-ECDSA"] = BsiObjectIdentifiers.ecdsa_plain_SHA256;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA384WITHPLAIN-ECDSA"] = BsiObjectIdentifiers.ecdsa_plain_SHA384;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA512WITHPLAIN-ECDSA"] = BsiObjectIdentifiers.ecdsa_plain_SHA512;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["RIPEMD160WITHPLAIN-ECDSA"] = BsiObjectIdentifiers.ecdsa_plain_RIPEMD160;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA1WITHCVC-ECDSA"] = EacObjectIdentifiers.id_TA_ECDSA_SHA_1;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA224WITHCVC-ECDSA"] = EacObjectIdentifiers.id_TA_ECDSA_SHA_224;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA256WITHCVC-ECDSA"] = EacObjectIdentifiers.id_TA_ECDSA_SHA_256;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA384WITHCVC-ECDSA"] = EacObjectIdentifiers.id_TA_ECDSA_SHA_384;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA512WITHCVC-ECDSA"] = EacObjectIdentifiers.id_TA_ECDSA_SHA_512;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA3-512WITHSPHINCS256"] = BCObjectIdentifiers.sphincs256_with_SHA3_512;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA512WITHSPHINCS256"] = BCObjectIdentifiers.sphincs256_with_SHA512;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA256WITHSM2"] = GMObjectIdentifiers.sm2sign_with_sha256;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SM3WITHSM2"] = GMObjectIdentifiers.sm2sign_with_sm3;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA256WITHXMSS"] = BCObjectIdentifiers.xmss_with_SHA256;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA512WITHXMSS"] = BCObjectIdentifiers.xmss_with_SHA512;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHAKE128WITHXMSS"] = BCObjectIdentifiers.xmss_with_SHAKE128;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHAKE256WITHXMSS"] = BCObjectIdentifiers.xmss_with_SHAKE256;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA256WITHXMSSMT"] = BCObjectIdentifiers.xmss_mt_with_SHA256;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHA512WITHXMSSMT"] = BCObjectIdentifiers.xmss_mt_with_SHA512;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHAKE128WITHXMSSMT"] = BCObjectIdentifiers.xmss_mt_with_SHAKE128;
			DefaultSignatureAlgorithmIdentifierFinder.algorithms["SHAKE256WITHXMSSMT"] = BCObjectIdentifiers.xmss_mt_with_SHAKE256;
			DefaultSignatureAlgorithmIdentifierFinder.noParams.Add(X9ObjectIdentifiers.ECDsaWithSha1);
			DefaultSignatureAlgorithmIdentifierFinder.noParams.Add(X9ObjectIdentifiers.ECDsaWithSha224);
			DefaultSignatureAlgorithmIdentifierFinder.noParams.Add(X9ObjectIdentifiers.ECDsaWithSha256);
			DefaultSignatureAlgorithmIdentifierFinder.noParams.Add(X9ObjectIdentifiers.ECDsaWithSha384);
			DefaultSignatureAlgorithmIdentifierFinder.noParams.Add(X9ObjectIdentifiers.ECDsaWithSha512);
			DefaultSignatureAlgorithmIdentifierFinder.noParams.Add(X9ObjectIdentifiers.IdDsaWithSha1);
			DefaultSignatureAlgorithmIdentifierFinder.noParams.Add(NistObjectIdentifiers.DsaWithSha224);
			DefaultSignatureAlgorithmIdentifierFinder.noParams.Add(NistObjectIdentifiers.DsaWithSha256);
			DefaultSignatureAlgorithmIdentifierFinder.noParams.Add(NistObjectIdentifiers.DsaWithSha384);
			DefaultSignatureAlgorithmIdentifierFinder.noParams.Add(NistObjectIdentifiers.DsaWithSha512);
			DefaultSignatureAlgorithmIdentifierFinder.noParams.Add(NistObjectIdentifiers.IdDsaWithSha3_224);
			DefaultSignatureAlgorithmIdentifierFinder.noParams.Add(NistObjectIdentifiers.IdDsaWithSha3_256);
			DefaultSignatureAlgorithmIdentifierFinder.noParams.Add(NistObjectIdentifiers.IdDsaWithSha3_384);
			DefaultSignatureAlgorithmIdentifierFinder.noParams.Add(NistObjectIdentifiers.IdDsaWithSha3_512);
			DefaultSignatureAlgorithmIdentifierFinder.noParams.Add(NistObjectIdentifiers.IdEcdsaWithSha3_224);
			DefaultSignatureAlgorithmIdentifierFinder.noParams.Add(NistObjectIdentifiers.IdEcdsaWithSha3_256);
			DefaultSignatureAlgorithmIdentifierFinder.noParams.Add(NistObjectIdentifiers.IdEcdsaWithSha3_384);
			DefaultSignatureAlgorithmIdentifierFinder.noParams.Add(NistObjectIdentifiers.IdEcdsaWithSha3_512);
			DefaultSignatureAlgorithmIdentifierFinder.noParams.Add(CryptoProObjectIdentifiers.GostR3411x94WithGostR3410x94);
			DefaultSignatureAlgorithmIdentifierFinder.noParams.Add(CryptoProObjectIdentifiers.GostR3411x94WithGostR3410x2001);
			DefaultSignatureAlgorithmIdentifierFinder.noParams.Add(RosstandartObjectIdentifiers.id_tc26_signwithdigest_gost_3410_12_256);
			DefaultSignatureAlgorithmIdentifierFinder.noParams.Add(RosstandartObjectIdentifiers.id_tc26_signwithdigest_gost_3410_12_512);
			DefaultSignatureAlgorithmIdentifierFinder.noParams.Add(BCObjectIdentifiers.sphincs256_with_SHA512);
			DefaultSignatureAlgorithmIdentifierFinder.noParams.Add(BCObjectIdentifiers.sphincs256_with_SHA3_512);
			DefaultSignatureAlgorithmIdentifierFinder.noParams.Add(BCObjectIdentifiers.xmss_with_SHA256);
			DefaultSignatureAlgorithmIdentifierFinder.noParams.Add(BCObjectIdentifiers.xmss_with_SHA512);
			DefaultSignatureAlgorithmIdentifierFinder.noParams.Add(BCObjectIdentifiers.xmss_with_SHAKE128);
			DefaultSignatureAlgorithmIdentifierFinder.noParams.Add(BCObjectIdentifiers.xmss_with_SHAKE256);
			DefaultSignatureAlgorithmIdentifierFinder.noParams.Add(BCObjectIdentifiers.xmss_mt_with_SHA256);
			DefaultSignatureAlgorithmIdentifierFinder.noParams.Add(BCObjectIdentifiers.xmss_mt_with_SHA512);
			DefaultSignatureAlgorithmIdentifierFinder.noParams.Add(BCObjectIdentifiers.xmss_mt_with_SHAKE128);
			DefaultSignatureAlgorithmIdentifierFinder.noParams.Add(BCObjectIdentifiers.xmss_mt_with_SHAKE256);
			DefaultSignatureAlgorithmIdentifierFinder.noParams.Add(GMObjectIdentifiers.sm2sign_with_sha256);
			DefaultSignatureAlgorithmIdentifierFinder.noParams.Add(GMObjectIdentifiers.sm2sign_with_sm3);
			DefaultSignatureAlgorithmIdentifierFinder.pkcs15RsaEncryption.Add(PkcsObjectIdentifiers.Sha1WithRsaEncryption);
			DefaultSignatureAlgorithmIdentifierFinder.pkcs15RsaEncryption.Add(PkcsObjectIdentifiers.Sha224WithRsaEncryption);
			DefaultSignatureAlgorithmIdentifierFinder.pkcs15RsaEncryption.Add(PkcsObjectIdentifiers.Sha256WithRsaEncryption);
			DefaultSignatureAlgorithmIdentifierFinder.pkcs15RsaEncryption.Add(PkcsObjectIdentifiers.Sha384WithRsaEncryption);
			DefaultSignatureAlgorithmIdentifierFinder.pkcs15RsaEncryption.Add(PkcsObjectIdentifiers.Sha512WithRsaEncryption);
			DefaultSignatureAlgorithmIdentifierFinder.pkcs15RsaEncryption.Add(TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD128);
			DefaultSignatureAlgorithmIdentifierFinder.pkcs15RsaEncryption.Add(TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD160);
			DefaultSignatureAlgorithmIdentifierFinder.pkcs15RsaEncryption.Add(TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD256);
			DefaultSignatureAlgorithmIdentifierFinder.pkcs15RsaEncryption.Add(NistObjectIdentifiers.IdRsassaPkcs1V15WithSha3_224);
			DefaultSignatureAlgorithmIdentifierFinder.pkcs15RsaEncryption.Add(NistObjectIdentifiers.IdRsassaPkcs1V15WithSha3_256);
			DefaultSignatureAlgorithmIdentifierFinder.pkcs15RsaEncryption.Add(NistObjectIdentifiers.IdRsassaPkcs1V15WithSha3_384);
			DefaultSignatureAlgorithmIdentifierFinder.pkcs15RsaEncryption.Add(NistObjectIdentifiers.IdRsassaPkcs1V15WithSha3_512);
			AlgorithmIdentifier hashAlgId = new AlgorithmIdentifier(OiwObjectIdentifiers.IdSha1, DerNull.Instance);
			DefaultSignatureAlgorithmIdentifierFinder._params["SHA1WITHRSAANDMGF1"] = DefaultSignatureAlgorithmIdentifierFinder.CreatePssParams(hashAlgId, 20);
			AlgorithmIdentifier hashAlgId2 = new AlgorithmIdentifier(NistObjectIdentifiers.IdSha224, DerNull.Instance);
			DefaultSignatureAlgorithmIdentifierFinder._params["SHA224WITHRSAANDMGF1"] = DefaultSignatureAlgorithmIdentifierFinder.CreatePssParams(hashAlgId2, 28);
			AlgorithmIdentifier hashAlgId3 = new AlgorithmIdentifier(NistObjectIdentifiers.IdSha256, DerNull.Instance);
			DefaultSignatureAlgorithmIdentifierFinder._params["SHA256WITHRSAANDMGF1"] = DefaultSignatureAlgorithmIdentifierFinder.CreatePssParams(hashAlgId3, 32);
			AlgorithmIdentifier hashAlgId4 = new AlgorithmIdentifier(NistObjectIdentifiers.IdSha384, DerNull.Instance);
			DefaultSignatureAlgorithmIdentifierFinder._params["SHA384WITHRSAANDMGF1"] = DefaultSignatureAlgorithmIdentifierFinder.CreatePssParams(hashAlgId4, 48);
			AlgorithmIdentifier hashAlgId5 = new AlgorithmIdentifier(NistObjectIdentifiers.IdSha512, DerNull.Instance);
			DefaultSignatureAlgorithmIdentifierFinder._params["SHA512WITHRSAANDMGF1"] = DefaultSignatureAlgorithmIdentifierFinder.CreatePssParams(hashAlgId5, 64);
			AlgorithmIdentifier hashAlgId6 = new AlgorithmIdentifier(NistObjectIdentifiers.IdSha3_224, DerNull.Instance);
			DefaultSignatureAlgorithmIdentifierFinder._params["SHA3-224WITHRSAANDMGF1"] = DefaultSignatureAlgorithmIdentifierFinder.CreatePssParams(hashAlgId6, 28);
			AlgorithmIdentifier hashAlgId7 = new AlgorithmIdentifier(NistObjectIdentifiers.IdSha3_256, DerNull.Instance);
			DefaultSignatureAlgorithmIdentifierFinder._params["SHA3-256WITHRSAANDMGF1"] = DefaultSignatureAlgorithmIdentifierFinder.CreatePssParams(hashAlgId7, 32);
			AlgorithmIdentifier hashAlgId8 = new AlgorithmIdentifier(NistObjectIdentifiers.IdSha3_384, DerNull.Instance);
			DefaultSignatureAlgorithmIdentifierFinder._params["SHA3-384WITHRSAANDMGF1"] = DefaultSignatureAlgorithmIdentifierFinder.CreatePssParams(hashAlgId8, 48);
			AlgorithmIdentifier hashAlgId9 = new AlgorithmIdentifier(NistObjectIdentifiers.IdSha3_512, DerNull.Instance);
			DefaultSignatureAlgorithmIdentifierFinder._params["SHA3-512WITHRSAANDMGF1"] = DefaultSignatureAlgorithmIdentifierFinder.CreatePssParams(hashAlgId9, 64);
			DefaultSignatureAlgorithmIdentifierFinder.digestOids[PkcsObjectIdentifiers.Sha224WithRsaEncryption] = NistObjectIdentifiers.IdSha224;
			DefaultSignatureAlgorithmIdentifierFinder.digestOids[PkcsObjectIdentifiers.Sha256WithRsaEncryption] = NistObjectIdentifiers.IdSha256;
			DefaultSignatureAlgorithmIdentifierFinder.digestOids[PkcsObjectIdentifiers.Sha384WithRsaEncryption] = NistObjectIdentifiers.IdSha384;
			DefaultSignatureAlgorithmIdentifierFinder.digestOids[PkcsObjectIdentifiers.Sha512WithRsaEncryption] = NistObjectIdentifiers.IdSha512;
			DefaultSignatureAlgorithmIdentifierFinder.digestOids[NistObjectIdentifiers.DsaWithSha224] = NistObjectIdentifiers.IdSha224;
			DefaultSignatureAlgorithmIdentifierFinder.digestOids[NistObjectIdentifiers.DsaWithSha224] = NistObjectIdentifiers.IdSha256;
			DefaultSignatureAlgorithmIdentifierFinder.digestOids[NistObjectIdentifiers.DsaWithSha224] = NistObjectIdentifiers.IdSha384;
			DefaultSignatureAlgorithmIdentifierFinder.digestOids[NistObjectIdentifiers.DsaWithSha224] = NistObjectIdentifiers.IdSha512;
			DefaultSignatureAlgorithmIdentifierFinder.digestOids[NistObjectIdentifiers.IdDsaWithSha3_224] = NistObjectIdentifiers.IdSha3_224;
			DefaultSignatureAlgorithmIdentifierFinder.digestOids[NistObjectIdentifiers.IdDsaWithSha3_256] = NistObjectIdentifiers.IdSha3_256;
			DefaultSignatureAlgorithmIdentifierFinder.digestOids[NistObjectIdentifiers.IdDsaWithSha3_384] = NistObjectIdentifiers.IdSha3_384;
			DefaultSignatureAlgorithmIdentifierFinder.digestOids[NistObjectIdentifiers.IdDsaWithSha3_512] = NistObjectIdentifiers.IdSha3_512;
			DefaultSignatureAlgorithmIdentifierFinder.digestOids[NistObjectIdentifiers.IdEcdsaWithSha3_224] = NistObjectIdentifiers.IdSha3_224;
			DefaultSignatureAlgorithmIdentifierFinder.digestOids[NistObjectIdentifiers.IdEcdsaWithSha3_256] = NistObjectIdentifiers.IdSha3_256;
			DefaultSignatureAlgorithmIdentifierFinder.digestOids[NistObjectIdentifiers.IdEcdsaWithSha3_384] = NistObjectIdentifiers.IdSha3_384;
			DefaultSignatureAlgorithmIdentifierFinder.digestOids[NistObjectIdentifiers.IdEcdsaWithSha3_512] = NistObjectIdentifiers.IdSha3_512;
			DefaultSignatureAlgorithmIdentifierFinder.digestOids[NistObjectIdentifiers.IdRsassaPkcs1V15WithSha3_224] = NistObjectIdentifiers.IdSha3_224;
			DefaultSignatureAlgorithmIdentifierFinder.digestOids[NistObjectIdentifiers.IdRsassaPkcs1V15WithSha3_256] = NistObjectIdentifiers.IdSha3_256;
			DefaultSignatureAlgorithmIdentifierFinder.digestOids[NistObjectIdentifiers.IdRsassaPkcs1V15WithSha3_384] = NistObjectIdentifiers.IdSha3_384;
			DefaultSignatureAlgorithmIdentifierFinder.digestOids[NistObjectIdentifiers.IdRsassaPkcs1V15WithSha3_512] = NistObjectIdentifiers.IdSha3_512;
			DefaultSignatureAlgorithmIdentifierFinder.digestOids[PkcsObjectIdentifiers.MD2WithRsaEncryption] = PkcsObjectIdentifiers.MD2;
			DefaultSignatureAlgorithmIdentifierFinder.digestOids[PkcsObjectIdentifiers.MD4WithRsaEncryption] = PkcsObjectIdentifiers.MD4;
			DefaultSignatureAlgorithmIdentifierFinder.digestOids[PkcsObjectIdentifiers.MD5WithRsaEncryption] = PkcsObjectIdentifiers.MD5;
			DefaultSignatureAlgorithmIdentifierFinder.digestOids[PkcsObjectIdentifiers.Sha1WithRsaEncryption] = OiwObjectIdentifiers.IdSha1;
			DefaultSignatureAlgorithmIdentifierFinder.digestOids[TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD128] = TeleTrusTObjectIdentifiers.RipeMD128;
			DefaultSignatureAlgorithmIdentifierFinder.digestOids[TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD160] = TeleTrusTObjectIdentifiers.RipeMD160;
			DefaultSignatureAlgorithmIdentifierFinder.digestOids[TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD256] = TeleTrusTObjectIdentifiers.RipeMD256;
			DefaultSignatureAlgorithmIdentifierFinder.digestOids[CryptoProObjectIdentifiers.GostR3411x94WithGostR3410x94] = CryptoProObjectIdentifiers.GostR3411;
			DefaultSignatureAlgorithmIdentifierFinder.digestOids[CryptoProObjectIdentifiers.GostR3411x94WithGostR3410x2001] = CryptoProObjectIdentifiers.GostR3411;
			DefaultSignatureAlgorithmIdentifierFinder.digestOids[RosstandartObjectIdentifiers.id_tc26_signwithdigest_gost_3410_12_256] = RosstandartObjectIdentifiers.id_tc26_gost_3411_12_256;
			DefaultSignatureAlgorithmIdentifierFinder.digestOids[RosstandartObjectIdentifiers.id_tc26_signwithdigest_gost_3410_12_512] = RosstandartObjectIdentifiers.id_tc26_gost_3411_12_512;
			DefaultSignatureAlgorithmIdentifierFinder.digestOids[GMObjectIdentifiers.sm2sign_with_sha256] = NistObjectIdentifiers.IdSha256;
			DefaultSignatureAlgorithmIdentifierFinder.digestOids[GMObjectIdentifiers.sm2sign_with_sm3] = GMObjectIdentifiers.sm3;
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x00077EDC File Offset: 0x00077EDC
		private static AlgorithmIdentifier Generate(string signatureAlgorithm)
		{
			string text = Strings.ToUpperCase(signatureAlgorithm);
			DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)DefaultSignatureAlgorithmIdentifierFinder.algorithms[text];
			if (derObjectIdentifier == null)
			{
				throw new ArgumentException("Unknown signature type requested: " + text);
			}
			AlgorithmIdentifier algorithmIdentifier;
			if (DefaultSignatureAlgorithmIdentifierFinder.noParams.Contains(derObjectIdentifier))
			{
				algorithmIdentifier = new AlgorithmIdentifier(derObjectIdentifier);
			}
			else if (DefaultSignatureAlgorithmIdentifierFinder._params.Contains(text))
			{
				algorithmIdentifier = new AlgorithmIdentifier(derObjectIdentifier, (Asn1Encodable)DefaultSignatureAlgorithmIdentifierFinder._params[text]);
			}
			else
			{
				algorithmIdentifier = new AlgorithmIdentifier(derObjectIdentifier, DerNull.Instance);
			}
			if (DefaultSignatureAlgorithmIdentifierFinder.pkcs15RsaEncryption.Contains(derObjectIdentifier))
			{
				new AlgorithmIdentifier(PkcsObjectIdentifiers.RsaEncryption, DerNull.Instance);
			}
			if (algorithmIdentifier.Algorithm.Equals(PkcsObjectIdentifiers.IdRsassaPss))
			{
				AlgorithmIdentifier hashAlgorithm = ((RsassaPssParameters)algorithmIdentifier.Parameters).HashAlgorithm;
			}
			else
			{
				new AlgorithmIdentifier((DerObjectIdentifier)DefaultSignatureAlgorithmIdentifierFinder.digestOids[derObjectIdentifier], DerNull.Instance);
			}
			return algorithmIdentifier;
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x00077FD8 File Offset: 0x00077FD8
		private static RsassaPssParameters CreatePssParams(AlgorithmIdentifier hashAlgId, int saltSize)
		{
			return new RsassaPssParameters(hashAlgId, new AlgorithmIdentifier(PkcsObjectIdentifiers.IdMgf1, hashAlgId), new DerInteger(saltSize), new DerInteger(1));
		}

		// Token: 0x06001700 RID: 5888 RVA: 0x00077FF8 File Offset: 0x00077FF8
		public AlgorithmIdentifier Find(string sigAlgName)
		{
			return DefaultSignatureAlgorithmIdentifierFinder.Generate(sigAlgName);
		}

		// Token: 0x04000F80 RID: 3968
		private static readonly IDictionary algorithms = Platform.CreateHashtable();

		// Token: 0x04000F81 RID: 3969
		private static readonly ISet noParams = new HashSet();

		// Token: 0x04000F82 RID: 3970
		private static readonly IDictionary _params = Platform.CreateHashtable();

		// Token: 0x04000F83 RID: 3971
		private static readonly ISet pkcs15RsaEncryption = new HashSet();

		// Token: 0x04000F84 RID: 3972
		private static readonly IDictionary digestOids = Platform.CreateHashtable();

		// Token: 0x04000F85 RID: 3973
		private static readonly IDictionary digestBuilders = Platform.CreateHashtable();

		// Token: 0x04000F86 RID: 3974
		private static readonly DerObjectIdentifier ENCRYPTION_RSA = PkcsObjectIdentifiers.RsaEncryption;

		// Token: 0x04000F87 RID: 3975
		private static readonly DerObjectIdentifier ENCRYPTION_DSA = X9ObjectIdentifiers.IdDsaWithSha1;

		// Token: 0x04000F88 RID: 3976
		private static readonly DerObjectIdentifier ENCRYPTION_ECDSA = X9ObjectIdentifiers.ECDsaWithSha1;

		// Token: 0x04000F89 RID: 3977
		private static readonly DerObjectIdentifier ENCRYPTION_RSA_PSS = PkcsObjectIdentifiers.IdRsassaPss;

		// Token: 0x04000F8A RID: 3978
		private static readonly DerObjectIdentifier ENCRYPTION_GOST3410 = CryptoProObjectIdentifiers.GostR3410x94;

		// Token: 0x04000F8B RID: 3979
		private static readonly DerObjectIdentifier ENCRYPTION_ECGOST3410 = CryptoProObjectIdentifiers.GostR3410x2001;

		// Token: 0x04000F8C RID: 3980
		private static readonly DerObjectIdentifier ENCRYPTION_ECGOST3410_2012_256 = RosstandartObjectIdentifiers.id_tc26_gost_3410_12_256;

		// Token: 0x04000F8D RID: 3981
		private static readonly DerObjectIdentifier ENCRYPTION_ECGOST3410_2012_512 = RosstandartObjectIdentifiers.id_tc26_gost_3410_12_512;
	}
}
