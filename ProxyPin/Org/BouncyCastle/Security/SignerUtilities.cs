﻿using System;
using System.Collections;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Bsi;
using Org.BouncyCastle.Asn1.CryptoPro;
using Org.BouncyCastle.Asn1.Eac;
using Org.BouncyCastle.Asn1.EdEC;
using Org.BouncyCastle.Asn1.GM;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.Oiw;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.TeleTrust;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Signers;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Security
{
	// Token: 0x020006B8 RID: 1720
	public sealed class SignerUtilities
	{
		// Token: 0x06003C30 RID: 15408 RVA: 0x0014B614 File Offset: 0x0014B614
		private SignerUtilities()
		{
		}

		// Token: 0x06003C31 RID: 15409 RVA: 0x0014B61C File Offset: 0x0014B61C
		static SignerUtilities()
		{
			SignerUtilities.algorithms["MD2WITHRSA"] = "MD2withRSA";
			SignerUtilities.algorithms["MD2WITHRSAENCRYPTION"] = "MD2withRSA";
			SignerUtilities.algorithms[PkcsObjectIdentifiers.MD2WithRsaEncryption.Id] = "MD2withRSA";
			SignerUtilities.algorithms["MD4WITHRSA"] = "MD4withRSA";
			SignerUtilities.algorithms["MD4WITHRSAENCRYPTION"] = "MD4withRSA";
			SignerUtilities.algorithms[PkcsObjectIdentifiers.MD4WithRsaEncryption.Id] = "MD4withRSA";
			SignerUtilities.algorithms[OiwObjectIdentifiers.MD4WithRsa.Id] = "MD4withRSA";
			SignerUtilities.algorithms[OiwObjectIdentifiers.MD4WithRsaEncryption.Id] = "MD4withRSA";
			SignerUtilities.algorithms["MD5WITHRSA"] = "MD5withRSA";
			SignerUtilities.algorithms["MD5WITHRSAENCRYPTION"] = "MD5withRSA";
			SignerUtilities.algorithms[PkcsObjectIdentifiers.MD5WithRsaEncryption.Id] = "MD5withRSA";
			SignerUtilities.algorithms[OiwObjectIdentifiers.MD5WithRsa.Id] = "MD5withRSA";
			SignerUtilities.algorithms["SHA1WITHRSA"] = "SHA-1withRSA";
			SignerUtilities.algorithms["SHA1WITHRSAENCRYPTION"] = "SHA-1withRSA";
			SignerUtilities.algorithms["SHA-1WITHRSA"] = "SHA-1withRSA";
			SignerUtilities.algorithms[PkcsObjectIdentifiers.Sha1WithRsaEncryption.Id] = "SHA-1withRSA";
			SignerUtilities.algorithms[OiwObjectIdentifiers.Sha1WithRsa.Id] = "SHA-1withRSA";
			SignerUtilities.algorithms["SHA224WITHRSA"] = "SHA-224withRSA";
			SignerUtilities.algorithms["SHA224WITHRSAENCRYPTION"] = "SHA-224withRSA";
			SignerUtilities.algorithms[PkcsObjectIdentifiers.Sha224WithRsaEncryption.Id] = "SHA-224withRSA";
			SignerUtilities.algorithms["SHA-224WITHRSA"] = "SHA-224withRSA";
			SignerUtilities.algorithms["SHA256WITHRSA"] = "SHA-256withRSA";
			SignerUtilities.algorithms["SHA256WITHRSAENCRYPTION"] = "SHA-256withRSA";
			SignerUtilities.algorithms[PkcsObjectIdentifiers.Sha256WithRsaEncryption.Id] = "SHA-256withRSA";
			SignerUtilities.algorithms["SHA-256WITHRSA"] = "SHA-256withRSA";
			SignerUtilities.algorithms["SHA384WITHRSA"] = "SHA-384withRSA";
			SignerUtilities.algorithms["SHA384WITHRSAENCRYPTION"] = "SHA-384withRSA";
			SignerUtilities.algorithms[PkcsObjectIdentifiers.Sha384WithRsaEncryption.Id] = "SHA-384withRSA";
			SignerUtilities.algorithms["SHA-384WITHRSA"] = "SHA-384withRSA";
			SignerUtilities.algorithms["SHA512WITHRSA"] = "SHA-512withRSA";
			SignerUtilities.algorithms["SHA512WITHRSAENCRYPTION"] = "SHA-512withRSA";
			SignerUtilities.algorithms[PkcsObjectIdentifiers.Sha512WithRsaEncryption.Id] = "SHA-512withRSA";
			SignerUtilities.algorithms["SHA-512WITHRSA"] = "SHA-512withRSA";
			SignerUtilities.algorithms["PSSWITHRSA"] = "PSSwithRSA";
			SignerUtilities.algorithms["RSASSA-PSS"] = "PSSwithRSA";
			SignerUtilities.algorithms[PkcsObjectIdentifiers.IdRsassaPss.Id] = "PSSwithRSA";
			SignerUtilities.algorithms["RSAPSS"] = "PSSwithRSA";
			SignerUtilities.algorithms["SHA1WITHRSAANDMGF1"] = "SHA-1withRSAandMGF1";
			SignerUtilities.algorithms["SHA-1WITHRSAANDMGF1"] = "SHA-1withRSAandMGF1";
			SignerUtilities.algorithms["SHA1WITHRSA/PSS"] = "SHA-1withRSAandMGF1";
			SignerUtilities.algorithms["SHA-1WITHRSA/PSS"] = "SHA-1withRSAandMGF1";
			SignerUtilities.algorithms["SHA224WITHRSAANDMGF1"] = "SHA-224withRSAandMGF1";
			SignerUtilities.algorithms["SHA-224WITHRSAANDMGF1"] = "SHA-224withRSAandMGF1";
			SignerUtilities.algorithms["SHA224WITHRSA/PSS"] = "SHA-224withRSAandMGF1";
			SignerUtilities.algorithms["SHA-224WITHRSA/PSS"] = "SHA-224withRSAandMGF1";
			SignerUtilities.algorithms["SHA256WITHRSAANDMGF1"] = "SHA-256withRSAandMGF1";
			SignerUtilities.algorithms["SHA-256WITHRSAANDMGF1"] = "SHA-256withRSAandMGF1";
			SignerUtilities.algorithms["SHA256WITHRSA/PSS"] = "SHA-256withRSAandMGF1";
			SignerUtilities.algorithms["SHA-256WITHRSA/PSS"] = "SHA-256withRSAandMGF1";
			SignerUtilities.algorithms["SHA384WITHRSAANDMGF1"] = "SHA-384withRSAandMGF1";
			SignerUtilities.algorithms["SHA-384WITHRSAANDMGF1"] = "SHA-384withRSAandMGF1";
			SignerUtilities.algorithms["SHA384WITHRSA/PSS"] = "SHA-384withRSAandMGF1";
			SignerUtilities.algorithms["SHA-384WITHRSA/PSS"] = "SHA-384withRSAandMGF1";
			SignerUtilities.algorithms["SHA512WITHRSAANDMGF1"] = "SHA-512withRSAandMGF1";
			SignerUtilities.algorithms["SHA-512WITHRSAANDMGF1"] = "SHA-512withRSAandMGF1";
			SignerUtilities.algorithms["SHA512WITHRSA/PSS"] = "SHA-512withRSAandMGF1";
			SignerUtilities.algorithms["SHA-512WITHRSA/PSS"] = "SHA-512withRSAandMGF1";
			SignerUtilities.algorithms["RIPEMD128WITHRSA"] = "RIPEMD128withRSA";
			SignerUtilities.algorithms["RIPEMD128WITHRSAENCRYPTION"] = "RIPEMD128withRSA";
			SignerUtilities.algorithms[TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD128.Id] = "RIPEMD128withRSA";
			SignerUtilities.algorithms["RIPEMD160WITHRSA"] = "RIPEMD160withRSA";
			SignerUtilities.algorithms["RIPEMD160WITHRSAENCRYPTION"] = "RIPEMD160withRSA";
			SignerUtilities.algorithms[TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD160.Id] = "RIPEMD160withRSA";
			SignerUtilities.algorithms["RIPEMD256WITHRSA"] = "RIPEMD256withRSA";
			SignerUtilities.algorithms["RIPEMD256WITHRSAENCRYPTION"] = "RIPEMD256withRSA";
			SignerUtilities.algorithms[TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD256.Id] = "RIPEMD256withRSA";
			SignerUtilities.algorithms["NONEWITHRSA"] = "RSA";
			SignerUtilities.algorithms["RSAWITHNONE"] = "RSA";
			SignerUtilities.algorithms["RAWRSA"] = "RSA";
			SignerUtilities.algorithms["RAWRSAPSS"] = "RAWRSASSA-PSS";
			SignerUtilities.algorithms["NONEWITHRSAPSS"] = "RAWRSASSA-PSS";
			SignerUtilities.algorithms["NONEWITHRSASSA-PSS"] = "RAWRSASSA-PSS";
			SignerUtilities.algorithms["NONEWITHDSA"] = "NONEwithDSA";
			SignerUtilities.algorithms["DSAWITHNONE"] = "NONEwithDSA";
			SignerUtilities.algorithms["RAWDSA"] = "NONEwithDSA";
			SignerUtilities.algorithms["DSA"] = "SHA-1withDSA";
			SignerUtilities.algorithms["DSAWITHSHA1"] = "SHA-1withDSA";
			SignerUtilities.algorithms["DSAWITHSHA-1"] = "SHA-1withDSA";
			SignerUtilities.algorithms["SHA/DSA"] = "SHA-1withDSA";
			SignerUtilities.algorithms["SHA1/DSA"] = "SHA-1withDSA";
			SignerUtilities.algorithms["SHA-1/DSA"] = "SHA-1withDSA";
			SignerUtilities.algorithms["SHA1WITHDSA"] = "SHA-1withDSA";
			SignerUtilities.algorithms["SHA-1WITHDSA"] = "SHA-1withDSA";
			SignerUtilities.algorithms[X9ObjectIdentifiers.IdDsaWithSha1.Id] = "SHA-1withDSA";
			SignerUtilities.algorithms[OiwObjectIdentifiers.DsaWithSha1.Id] = "SHA-1withDSA";
			SignerUtilities.algorithms["DSAWITHSHA224"] = "SHA-224withDSA";
			SignerUtilities.algorithms["DSAWITHSHA-224"] = "SHA-224withDSA";
			SignerUtilities.algorithms["SHA224/DSA"] = "SHA-224withDSA";
			SignerUtilities.algorithms["SHA-224/DSA"] = "SHA-224withDSA";
			SignerUtilities.algorithms["SHA224WITHDSA"] = "SHA-224withDSA";
			SignerUtilities.algorithms["SHA-224WITHDSA"] = "SHA-224withDSA";
			SignerUtilities.algorithms[NistObjectIdentifiers.DsaWithSha224.Id] = "SHA-224withDSA";
			SignerUtilities.algorithms["DSAWITHSHA256"] = "SHA-256withDSA";
			SignerUtilities.algorithms["DSAWITHSHA-256"] = "SHA-256withDSA";
			SignerUtilities.algorithms["SHA256/DSA"] = "SHA-256withDSA";
			SignerUtilities.algorithms["SHA-256/DSA"] = "SHA-256withDSA";
			SignerUtilities.algorithms["SHA256WITHDSA"] = "SHA-256withDSA";
			SignerUtilities.algorithms["SHA-256WITHDSA"] = "SHA-256withDSA";
			SignerUtilities.algorithms[NistObjectIdentifiers.DsaWithSha256.Id] = "SHA-256withDSA";
			SignerUtilities.algorithms["DSAWITHSHA384"] = "SHA-384withDSA";
			SignerUtilities.algorithms["DSAWITHSHA-384"] = "SHA-384withDSA";
			SignerUtilities.algorithms["SHA384/DSA"] = "SHA-384withDSA";
			SignerUtilities.algorithms["SHA-384/DSA"] = "SHA-384withDSA";
			SignerUtilities.algorithms["SHA384WITHDSA"] = "SHA-384withDSA";
			SignerUtilities.algorithms["SHA-384WITHDSA"] = "SHA-384withDSA";
			SignerUtilities.algorithms[NistObjectIdentifiers.DsaWithSha384.Id] = "SHA-384withDSA";
			SignerUtilities.algorithms["DSAWITHSHA512"] = "SHA-512withDSA";
			SignerUtilities.algorithms["DSAWITHSHA-512"] = "SHA-512withDSA";
			SignerUtilities.algorithms["SHA512/DSA"] = "SHA-512withDSA";
			SignerUtilities.algorithms["SHA-512/DSA"] = "SHA-512withDSA";
			SignerUtilities.algorithms["SHA512WITHDSA"] = "SHA-512withDSA";
			SignerUtilities.algorithms["SHA-512WITHDSA"] = "SHA-512withDSA";
			SignerUtilities.algorithms[NistObjectIdentifiers.DsaWithSha512.Id] = "SHA-512withDSA";
			SignerUtilities.algorithms["NONEWITHECDSA"] = "NONEwithECDSA";
			SignerUtilities.algorithms["ECDSAWITHNONE"] = "NONEwithECDSA";
			SignerUtilities.algorithms["ECDSA"] = "SHA-1withECDSA";
			SignerUtilities.algorithms["SHA1/ECDSA"] = "SHA-1withECDSA";
			SignerUtilities.algorithms["SHA-1/ECDSA"] = "SHA-1withECDSA";
			SignerUtilities.algorithms["ECDSAWITHSHA1"] = "SHA-1withECDSA";
			SignerUtilities.algorithms["ECDSAWITHSHA-1"] = "SHA-1withECDSA";
			SignerUtilities.algorithms["SHA1WITHECDSA"] = "SHA-1withECDSA";
			SignerUtilities.algorithms["SHA-1WITHECDSA"] = "SHA-1withECDSA";
			SignerUtilities.algorithms[X9ObjectIdentifiers.ECDsaWithSha1.Id] = "SHA-1withECDSA";
			SignerUtilities.algorithms[TeleTrusTObjectIdentifiers.ECSignWithSha1.Id] = "SHA-1withECDSA";
			SignerUtilities.algorithms["SHA224/ECDSA"] = "SHA-224withECDSA";
			SignerUtilities.algorithms["SHA-224/ECDSA"] = "SHA-224withECDSA";
			SignerUtilities.algorithms["ECDSAWITHSHA224"] = "SHA-224withECDSA";
			SignerUtilities.algorithms["ECDSAWITHSHA-224"] = "SHA-224withECDSA";
			SignerUtilities.algorithms["SHA224WITHECDSA"] = "SHA-224withECDSA";
			SignerUtilities.algorithms["SHA-224WITHECDSA"] = "SHA-224withECDSA";
			SignerUtilities.algorithms[X9ObjectIdentifiers.ECDsaWithSha224.Id] = "SHA-224withECDSA";
			SignerUtilities.algorithms["SHA256/ECDSA"] = "SHA-256withECDSA";
			SignerUtilities.algorithms["SHA-256/ECDSA"] = "SHA-256withECDSA";
			SignerUtilities.algorithms["ECDSAWITHSHA256"] = "SHA-256withECDSA";
			SignerUtilities.algorithms["ECDSAWITHSHA-256"] = "SHA-256withECDSA";
			SignerUtilities.algorithms["SHA256WITHECDSA"] = "SHA-256withECDSA";
			SignerUtilities.algorithms["SHA-256WITHECDSA"] = "SHA-256withECDSA";
			SignerUtilities.algorithms[X9ObjectIdentifiers.ECDsaWithSha256.Id] = "SHA-256withECDSA";
			SignerUtilities.algorithms["SHA384/ECDSA"] = "SHA-384withECDSA";
			SignerUtilities.algorithms["SHA-384/ECDSA"] = "SHA-384withECDSA";
			SignerUtilities.algorithms["ECDSAWITHSHA384"] = "SHA-384withECDSA";
			SignerUtilities.algorithms["ECDSAWITHSHA-384"] = "SHA-384withECDSA";
			SignerUtilities.algorithms["SHA384WITHECDSA"] = "SHA-384withECDSA";
			SignerUtilities.algorithms["SHA-384WITHECDSA"] = "SHA-384withECDSA";
			SignerUtilities.algorithms[X9ObjectIdentifiers.ECDsaWithSha384.Id] = "SHA-384withECDSA";
			SignerUtilities.algorithms["SHA512/ECDSA"] = "SHA-512withECDSA";
			SignerUtilities.algorithms["SHA-512/ECDSA"] = "SHA-512withECDSA";
			SignerUtilities.algorithms["ECDSAWITHSHA512"] = "SHA-512withECDSA";
			SignerUtilities.algorithms["ECDSAWITHSHA-512"] = "SHA-512withECDSA";
			SignerUtilities.algorithms["SHA512WITHECDSA"] = "SHA-512withECDSA";
			SignerUtilities.algorithms["SHA-512WITHECDSA"] = "SHA-512withECDSA";
			SignerUtilities.algorithms[X9ObjectIdentifiers.ECDsaWithSha512.Id] = "SHA-512withECDSA";
			SignerUtilities.algorithms["RIPEMD160/ECDSA"] = "RIPEMD160withECDSA";
			SignerUtilities.algorithms["ECDSAWITHRIPEMD160"] = "RIPEMD160withECDSA";
			SignerUtilities.algorithms["RIPEMD160WITHECDSA"] = "RIPEMD160withECDSA";
			SignerUtilities.algorithms[TeleTrusTObjectIdentifiers.ECSignWithRipeMD160.Id] = "RIPEMD160withECDSA";
			SignerUtilities.algorithms["NONEWITHCVC-ECDSA"] = "NONEwithCVC-ECDSA";
			SignerUtilities.algorithms["CVC-ECDSAWITHNONE"] = "NONEwithCVC-ECDSA";
			SignerUtilities.algorithms["SHA1/CVC-ECDSA"] = "SHA-1withCVC-ECDSA";
			SignerUtilities.algorithms["SHA-1/CVC-ECDSA"] = "SHA-1withCVC-ECDSA";
			SignerUtilities.algorithms["CVC-ECDSAWITHSHA1"] = "SHA-1withCVC-ECDSA";
			SignerUtilities.algorithms["CVC-ECDSAWITHSHA-1"] = "SHA-1withCVC-ECDSA";
			SignerUtilities.algorithms["SHA1WITHCVC-ECDSA"] = "SHA-1withCVC-ECDSA";
			SignerUtilities.algorithms["SHA-1WITHCVC-ECDSA"] = "SHA-1withCVC-ECDSA";
			SignerUtilities.algorithms[EacObjectIdentifiers.id_TA_ECDSA_SHA_1.Id] = "SHA-1withCVC-ECDSA";
			SignerUtilities.algorithms["SHA224/CVC-ECDSA"] = "SHA-224withCVC-ECDSA";
			SignerUtilities.algorithms["SHA-224/CVC-ECDSA"] = "SHA-224withCVC-ECDSA";
			SignerUtilities.algorithms["CVC-ECDSAWITHSHA224"] = "SHA-224withCVC-ECDSA";
			SignerUtilities.algorithms["CVC-ECDSAWITHSHA-224"] = "SHA-224withCVC-ECDSA";
			SignerUtilities.algorithms["SHA224WITHCVC-ECDSA"] = "SHA-224withCVC-ECDSA";
			SignerUtilities.algorithms["SHA-224WITHCVC-ECDSA"] = "SHA-224withCVC-ECDSA";
			SignerUtilities.algorithms[EacObjectIdentifiers.id_TA_ECDSA_SHA_224.Id] = "SHA-224withCVC-ECDSA";
			SignerUtilities.algorithms["SHA256/CVC-ECDSA"] = "SHA-256withCVC-ECDSA";
			SignerUtilities.algorithms["SHA-256/CVC-ECDSA"] = "SHA-256withCVC-ECDSA";
			SignerUtilities.algorithms["CVC-ECDSAWITHSHA256"] = "SHA-256withCVC-ECDSA";
			SignerUtilities.algorithms["CVC-ECDSAWITHSHA-256"] = "SHA-256withCVC-ECDSA";
			SignerUtilities.algorithms["SHA256WITHCVC-ECDSA"] = "SHA-256withCVC-ECDSA";
			SignerUtilities.algorithms["SHA-256WITHCVC-ECDSA"] = "SHA-256withCVC-ECDSA";
			SignerUtilities.algorithms[EacObjectIdentifiers.id_TA_ECDSA_SHA_256.Id] = "SHA-256withCVC-ECDSA";
			SignerUtilities.algorithms["SHA384/CVC-ECDSA"] = "SHA-384withCVC-ECDSA";
			SignerUtilities.algorithms["SHA-384/CVC-ECDSA"] = "SHA-384withCVC-ECDSA";
			SignerUtilities.algorithms["CVC-ECDSAWITHSHA384"] = "SHA-384withCVC-ECDSA";
			SignerUtilities.algorithms["CVC-ECDSAWITHSHA-384"] = "SHA-384withCVC-ECDSA";
			SignerUtilities.algorithms["SHA384WITHCVC-ECDSA"] = "SHA-384withCVC-ECDSA";
			SignerUtilities.algorithms["SHA-384WITHCVC-ECDSA"] = "SHA-384withCVC-ECDSA";
			SignerUtilities.algorithms[EacObjectIdentifiers.id_TA_ECDSA_SHA_384.Id] = "SHA-384withCVC-ECDSA";
			SignerUtilities.algorithms["SHA512/CVC-ECDSA"] = "SHA-512withCVC-ECDSA";
			SignerUtilities.algorithms["SHA-512/CVC-ECDSA"] = "SHA-512withCVC-ECDSA";
			SignerUtilities.algorithms["CVC-ECDSAWITHSHA512"] = "SHA-512withCVC-ECDSA";
			SignerUtilities.algorithms["CVC-ECDSAWITHSHA-512"] = "SHA-512withCVC-ECDSA";
			SignerUtilities.algorithms["SHA512WITHCVC-ECDSA"] = "SHA-512withCVC-ECDSA";
			SignerUtilities.algorithms["SHA-512WITHCVC-ECDSA"] = "SHA-512withCVC-ECDSA";
			SignerUtilities.algorithms[EacObjectIdentifiers.id_TA_ECDSA_SHA_512.Id] = "SHA-512withCVC-ECDSA";
			SignerUtilities.algorithms["NONEWITHPLAIN-ECDSA"] = "NONEwithPLAIN-ECDSA";
			SignerUtilities.algorithms["PLAIN-ECDSAWITHNONE"] = "NONEwithPLAIN-ECDSA";
			SignerUtilities.algorithms["SHA1/PLAIN-ECDSA"] = "SHA-1withPLAIN-ECDSA";
			SignerUtilities.algorithms["SHA-1/PLAIN-ECDSA"] = "SHA-1withPLAIN-ECDSA";
			SignerUtilities.algorithms["PLAIN-ECDSAWITHSHA1"] = "SHA-1withPLAIN-ECDSA";
			SignerUtilities.algorithms["PLAIN-ECDSAWITHSHA-1"] = "SHA-1withPLAIN-ECDSA";
			SignerUtilities.algorithms["SHA1WITHPLAIN-ECDSA"] = "SHA-1withPLAIN-ECDSA";
			SignerUtilities.algorithms["SHA-1WITHPLAIN-ECDSA"] = "SHA-1withPLAIN-ECDSA";
			SignerUtilities.algorithms[BsiObjectIdentifiers.ecdsa_plain_SHA1.Id] = "SHA-1withPLAIN-ECDSA";
			SignerUtilities.algorithms["SHA224/PLAIN-ECDSA"] = "SHA-224withPLAIN-ECDSA";
			SignerUtilities.algorithms["SHA-224/PLAIN-ECDSA"] = "SHA-224withPLAIN-ECDSA";
			SignerUtilities.algorithms["PLAIN-ECDSAWITHSHA224"] = "SHA-224withPLAIN-ECDSA";
			SignerUtilities.algorithms["PLAIN-ECDSAWITHSHA-224"] = "SHA-224withPLAIN-ECDSA";
			SignerUtilities.algorithms["SHA224WITHPLAIN-ECDSA"] = "SHA-224withPLAIN-ECDSA";
			SignerUtilities.algorithms["SHA-224WITHPLAIN-ECDSA"] = "SHA-224withPLAIN-ECDSA";
			SignerUtilities.algorithms[BsiObjectIdentifiers.ecdsa_plain_SHA224.Id] = "SHA-224withPLAIN-ECDSA";
			SignerUtilities.algorithms["SHA256/PLAIN-ECDSA"] = "SHA-256withPLAIN-ECDSA";
			SignerUtilities.algorithms["SHA-256/PLAIN-ECDSA"] = "SHA-256withPLAIN-ECDSA";
			SignerUtilities.algorithms["PLAIN-ECDSAWITHSHA256"] = "SHA-256withPLAIN-ECDSA";
			SignerUtilities.algorithms["PLAIN-ECDSAWITHSHA-256"] = "SHA-256withPLAIN-ECDSA";
			SignerUtilities.algorithms["SHA256WITHPLAIN-ECDSA"] = "SHA-256withPLAIN-ECDSA";
			SignerUtilities.algorithms["SHA-256WITHPLAIN-ECDSA"] = "SHA-256withPLAIN-ECDSA";
			SignerUtilities.algorithms[BsiObjectIdentifiers.ecdsa_plain_SHA256.Id] = "SHA-256withPLAIN-ECDSA";
			SignerUtilities.algorithms["SHA384/PLAIN-ECDSA"] = "SHA-384withPLAIN-ECDSA";
			SignerUtilities.algorithms["SHA-384/PLAIN-ECDSA"] = "SHA-384withPLAIN-ECDSA";
			SignerUtilities.algorithms["PLAIN-ECDSAWITHSHA384"] = "SHA-384withPLAIN-ECDSA";
			SignerUtilities.algorithms["PLAIN-ECDSAWITHSHA-384"] = "SHA-384withPLAIN-ECDSA";
			SignerUtilities.algorithms["SHA384WITHPLAIN-ECDSA"] = "SHA-384withPLAIN-ECDSA";
			SignerUtilities.algorithms["SHA-384WITHPLAIN-ECDSA"] = "SHA-384withPLAIN-ECDSA";
			SignerUtilities.algorithms[BsiObjectIdentifiers.ecdsa_plain_SHA384.Id] = "SHA-384withPLAIN-ECDSA";
			SignerUtilities.algorithms["SHA512/PLAIN-ECDSA"] = "SHA-512withPLAIN-ECDSA";
			SignerUtilities.algorithms["SHA-512/PLAIN-ECDSA"] = "SHA-512withPLAIN-ECDSA";
			SignerUtilities.algorithms["PLAIN-ECDSAWITHSHA512"] = "SHA-512withPLAIN-ECDSA";
			SignerUtilities.algorithms["PLAIN-ECDSAWITHSHA-512"] = "SHA-512withPLAIN-ECDSA";
			SignerUtilities.algorithms["SHA512WITHPLAIN-ECDSA"] = "SHA-512withPLAIN-ECDSA";
			SignerUtilities.algorithms["SHA-512WITHPLAIN-ECDSA"] = "SHA-512withPLAIN-ECDSA";
			SignerUtilities.algorithms[BsiObjectIdentifiers.ecdsa_plain_SHA512.Id] = "SHA-512withPLAIN-ECDSA";
			SignerUtilities.algorithms["RIPEMD160/PLAIN-ECDSA"] = "RIPEMD160withPLAIN-ECDSA";
			SignerUtilities.algorithms["PLAIN-ECDSAWITHRIPEMD160"] = "RIPEMD160withPLAIN-ECDSA";
			SignerUtilities.algorithms["RIPEMD160WITHPLAIN-ECDSA"] = "RIPEMD160withPLAIN-ECDSA";
			SignerUtilities.algorithms[BsiObjectIdentifiers.ecdsa_plain_RIPEMD160.Id] = "RIPEMD160withPLAIN-ECDSA";
			SignerUtilities.algorithms["SHA1WITHECNR"] = "SHA-1withECNR";
			SignerUtilities.algorithms["SHA-1WITHECNR"] = "SHA-1withECNR";
			SignerUtilities.algorithms["SHA224WITHECNR"] = "SHA-224withECNR";
			SignerUtilities.algorithms["SHA-224WITHECNR"] = "SHA-224withECNR";
			SignerUtilities.algorithms["SHA256WITHECNR"] = "SHA-256withECNR";
			SignerUtilities.algorithms["SHA-256WITHECNR"] = "SHA-256withECNR";
			SignerUtilities.algorithms["SHA384WITHECNR"] = "SHA-384withECNR";
			SignerUtilities.algorithms["SHA-384WITHECNR"] = "SHA-384withECNR";
			SignerUtilities.algorithms["SHA512WITHECNR"] = "SHA-512withECNR";
			SignerUtilities.algorithms["SHA-512WITHECNR"] = "SHA-512withECNR";
			SignerUtilities.algorithms["GOST-3410"] = "GOST3410";
			SignerUtilities.algorithms["GOST-3410-94"] = "GOST3410";
			SignerUtilities.algorithms["GOST3411WITHGOST3410"] = "GOST3410";
			SignerUtilities.algorithms[CryptoProObjectIdentifiers.GostR3411x94WithGostR3410x94.Id] = "GOST3410";
			SignerUtilities.algorithms["ECGOST-3410"] = "ECGOST3410";
			SignerUtilities.algorithms["ECGOST-3410-2001"] = "ECGOST3410";
			SignerUtilities.algorithms["GOST3411WITHECGOST3410"] = "ECGOST3410";
			SignerUtilities.algorithms[CryptoProObjectIdentifiers.GostR3411x94WithGostR3410x2001.Id] = "ECGOST3410";
			SignerUtilities.algorithms["ED25519"] = "Ed25519";
			SignerUtilities.algorithms[EdECObjectIdentifiers.id_Ed25519.Id] = "Ed25519";
			SignerUtilities.algorithms["ED25519CTX"] = "Ed25519ctx";
			SignerUtilities.algorithms["ED25519PH"] = "Ed25519ph";
			SignerUtilities.algorithms["ED448"] = "Ed448";
			SignerUtilities.algorithms[EdECObjectIdentifiers.id_Ed448.Id] = "Ed448";
			SignerUtilities.algorithms["ED448PH"] = "Ed448ph";
			SignerUtilities.algorithms["SHA256WITHSM2"] = "SHA256withSM2";
			SignerUtilities.algorithms[GMObjectIdentifiers.sm2sign_with_sha256.Id] = "SHA256withSM2";
			SignerUtilities.algorithms["SM3WITHSM2"] = "SM3withSM2";
			SignerUtilities.algorithms[GMObjectIdentifiers.sm2sign_with_sm3.Id] = "SM3withSM2";
			SignerUtilities.oids["MD2withRSA"] = PkcsObjectIdentifiers.MD2WithRsaEncryption;
			SignerUtilities.oids["MD4withRSA"] = PkcsObjectIdentifiers.MD4WithRsaEncryption;
			SignerUtilities.oids["MD5withRSA"] = PkcsObjectIdentifiers.MD5WithRsaEncryption;
			SignerUtilities.oids["SHA-1withRSA"] = PkcsObjectIdentifiers.Sha1WithRsaEncryption;
			SignerUtilities.oids["SHA-224withRSA"] = PkcsObjectIdentifiers.Sha224WithRsaEncryption;
			SignerUtilities.oids["SHA-256withRSA"] = PkcsObjectIdentifiers.Sha256WithRsaEncryption;
			SignerUtilities.oids["SHA-384withRSA"] = PkcsObjectIdentifiers.Sha384WithRsaEncryption;
			SignerUtilities.oids["SHA-512withRSA"] = PkcsObjectIdentifiers.Sha512WithRsaEncryption;
			SignerUtilities.oids["PSSwithRSA"] = PkcsObjectIdentifiers.IdRsassaPss;
			SignerUtilities.oids["SHA-1withRSAandMGF1"] = PkcsObjectIdentifiers.IdRsassaPss;
			SignerUtilities.oids["SHA-224withRSAandMGF1"] = PkcsObjectIdentifiers.IdRsassaPss;
			SignerUtilities.oids["SHA-256withRSAandMGF1"] = PkcsObjectIdentifiers.IdRsassaPss;
			SignerUtilities.oids["SHA-384withRSAandMGF1"] = PkcsObjectIdentifiers.IdRsassaPss;
			SignerUtilities.oids["SHA-512withRSAandMGF1"] = PkcsObjectIdentifiers.IdRsassaPss;
			SignerUtilities.oids["RIPEMD128withRSA"] = TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD128;
			SignerUtilities.oids["RIPEMD160withRSA"] = TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD160;
			SignerUtilities.oids["RIPEMD256withRSA"] = TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD256;
			SignerUtilities.oids["SHA-1withDSA"] = X9ObjectIdentifiers.IdDsaWithSha1;
			SignerUtilities.oids["SHA-1withECDSA"] = X9ObjectIdentifiers.ECDsaWithSha1;
			SignerUtilities.oids["SHA-224withECDSA"] = X9ObjectIdentifiers.ECDsaWithSha224;
			SignerUtilities.oids["SHA-256withECDSA"] = X9ObjectIdentifiers.ECDsaWithSha256;
			SignerUtilities.oids["SHA-384withECDSA"] = X9ObjectIdentifiers.ECDsaWithSha384;
			SignerUtilities.oids["SHA-512withECDSA"] = X9ObjectIdentifiers.ECDsaWithSha512;
			SignerUtilities.oids["RIPEMD160withECDSA"] = TeleTrusTObjectIdentifiers.ECSignWithRipeMD160;
			SignerUtilities.oids["SHA-1withCVC-ECDSA"] = EacObjectIdentifiers.id_TA_ECDSA_SHA_1;
			SignerUtilities.oids["SHA-224withCVC-ECDSA"] = EacObjectIdentifiers.id_TA_ECDSA_SHA_224;
			SignerUtilities.oids["SHA-256withCVC-ECDSA"] = EacObjectIdentifiers.id_TA_ECDSA_SHA_256;
			SignerUtilities.oids["SHA-384withCVC-ECDSA"] = EacObjectIdentifiers.id_TA_ECDSA_SHA_384;
			SignerUtilities.oids["SHA-512withCVC-ECDSA"] = EacObjectIdentifiers.id_TA_ECDSA_SHA_512;
			SignerUtilities.oids["SHA-1withPLAIN-ECDSA"] = BsiObjectIdentifiers.ecdsa_plain_SHA1;
			SignerUtilities.oids["SHA-224withPLAIN-ECDSA"] = BsiObjectIdentifiers.ecdsa_plain_SHA224;
			SignerUtilities.oids["SHA-256withPLAIN-ECDSA"] = BsiObjectIdentifiers.ecdsa_plain_SHA256;
			SignerUtilities.oids["SHA-384withPLAIN-ECDSA"] = BsiObjectIdentifiers.ecdsa_plain_SHA384;
			SignerUtilities.oids["SHA-512withPLAIN-ECDSA"] = BsiObjectIdentifiers.ecdsa_plain_SHA512;
			SignerUtilities.oids["RIPEMD160withPLAIN-ECDSA"] = BsiObjectIdentifiers.ecdsa_plain_RIPEMD160;
			SignerUtilities.oids["GOST3410"] = CryptoProObjectIdentifiers.GostR3411x94WithGostR3410x94;
			SignerUtilities.oids["ECGOST3410"] = CryptoProObjectIdentifiers.GostR3411x94WithGostR3410x2001;
			SignerUtilities.oids["Ed25519"] = EdECObjectIdentifiers.id_Ed25519;
			SignerUtilities.oids["Ed448"] = EdECObjectIdentifiers.id_Ed448;
			SignerUtilities.oids["SHA256withSM2"] = GMObjectIdentifiers.sm2sign_with_sha256;
			SignerUtilities.oids["SM3withSM2"] = GMObjectIdentifiers.sm2sign_with_sm3;
		}

		// Token: 0x06003C32 RID: 15410 RVA: 0x0014CEE8 File Offset: 0x0014CEE8
		public static DerObjectIdentifier GetObjectIdentifier(string mechanism)
		{
			if (mechanism == null)
			{
				throw new ArgumentNullException("mechanism");
			}
			mechanism = Platform.ToUpperInvariant(mechanism);
			string text = (string)SignerUtilities.algorithms[mechanism];
			if (text != null)
			{
				mechanism = text;
			}
			return (DerObjectIdentifier)SignerUtilities.oids[mechanism];
		}

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x06003C33 RID: 15411 RVA: 0x0014CF3C File Offset: 0x0014CF3C
		public static ICollection Algorithms
		{
			get
			{
				return SignerUtilities.oids.Keys;
			}
		}

		// Token: 0x06003C34 RID: 15412 RVA: 0x0014CF48 File Offset: 0x0014CF48
		public static Asn1Encodable GetDefaultX509Parameters(DerObjectIdentifier id)
		{
			return SignerUtilities.GetDefaultX509Parameters(id.Id);
		}

		// Token: 0x06003C35 RID: 15413 RVA: 0x0014CF58 File Offset: 0x0014CF58
		public static Asn1Encodable GetDefaultX509Parameters(string algorithm)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			algorithm = Platform.ToUpperInvariant(algorithm);
			string text = (string)SignerUtilities.algorithms[algorithm];
			if (text == null)
			{
				text = algorithm;
			}
			if (text == "PSSwithRSA")
			{
				return SignerUtilities.GetPssX509Parameters("SHA-1");
			}
			if (Platform.EndsWith(text, "withRSAandMGF1"))
			{
				string digestName = text.Substring(0, text.Length - "withRSAandMGF1".Length);
				return SignerUtilities.GetPssX509Parameters(digestName);
			}
			return DerNull.Instance;
		}

		// Token: 0x06003C36 RID: 15414 RVA: 0x0014CFEC File Offset: 0x0014CFEC
		private static Asn1Encodable GetPssX509Parameters(string digestName)
		{
			AlgorithmIdentifier algorithmIdentifier = new AlgorithmIdentifier(DigestUtilities.GetObjectIdentifier(digestName), DerNull.Instance);
			AlgorithmIdentifier maskGenAlgorithm = new AlgorithmIdentifier(PkcsObjectIdentifiers.IdMgf1, algorithmIdentifier);
			int digestSize = DigestUtilities.GetDigest(digestName).GetDigestSize();
			return new RsassaPssParameters(algorithmIdentifier, maskGenAlgorithm, new DerInteger(digestSize), new DerInteger(1));
		}

		// Token: 0x06003C37 RID: 15415 RVA: 0x0014D03C File Offset: 0x0014D03C
		public static ISigner GetSigner(DerObjectIdentifier id)
		{
			return SignerUtilities.GetSigner(id.Id);
		}

		// Token: 0x06003C38 RID: 15416 RVA: 0x0014D04C File Offset: 0x0014D04C
		public static ISigner GetSigner(string algorithm)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			algorithm = Platform.ToUpperInvariant(algorithm);
			string text = (string)SignerUtilities.algorithms[algorithm];
			if (text == null)
			{
				text = algorithm;
			}
			if (Platform.StartsWith(text, "Ed"))
			{
				if (text.Equals("Ed25519"))
				{
					return new Ed25519Signer();
				}
				if (text.Equals("Ed25519ctx"))
				{
					return new Ed25519ctxSigner(Arrays.EmptyBytes);
				}
				if (text.Equals("Ed25519ph"))
				{
					return new Ed25519phSigner(Arrays.EmptyBytes);
				}
				if (text.Equals("Ed448"))
				{
					return new Ed448Signer(Arrays.EmptyBytes);
				}
				if (text.Equals("Ed448ph"))
				{
					return new Ed448phSigner(Arrays.EmptyBytes);
				}
			}
			if (text.Equals("RSA"))
			{
				return new RsaDigestSigner(new NullDigest(), null);
			}
			if (text.Equals("RAWRSASSA-PSS"))
			{
				return PssSigner.CreateRawSigner(new RsaBlindedEngine(), new Sha1Digest());
			}
			if (text.Equals("PSSwithRSA"))
			{
				return new PssSigner(new RsaBlindedEngine(), new Sha1Digest());
			}
			if (Platform.EndsWith(text, "withRSA"))
			{
				string algorithm2 = text.Substring(0, text.LastIndexOf("with"));
				IDigest digest = DigestUtilities.GetDigest(algorithm2);
				return new RsaDigestSigner(digest);
			}
			if (Platform.EndsWith(text, "withRSAandMGF1"))
			{
				string algorithm3 = text.Substring(0, text.LastIndexOf("with"));
				IDigest digest2 = DigestUtilities.GetDigest(algorithm3);
				return new PssSigner(new RsaBlindedEngine(), digest2);
			}
			if (Platform.EndsWith(text, "withDSA"))
			{
				string algorithm4 = text.Substring(0, text.LastIndexOf("with"));
				IDigest digest3 = DigestUtilities.GetDigest(algorithm4);
				return new DsaDigestSigner(new DsaSigner(), digest3);
			}
			if (Platform.EndsWith(text, "withECDSA"))
			{
				string algorithm5 = text.Substring(0, text.LastIndexOf("with"));
				IDigest digest4 = DigestUtilities.GetDigest(algorithm5);
				return new DsaDigestSigner(new ECDsaSigner(), digest4);
			}
			if (Platform.EndsWith(text, "withCVC-ECDSA") || Platform.EndsWith(text, "withPLAIN-ECDSA"))
			{
				string algorithm6 = text.Substring(0, text.LastIndexOf("with"));
				IDigest digest5 = DigestUtilities.GetDigest(algorithm6);
				return new DsaDigestSigner(new ECDsaSigner(), digest5, PlainDsaEncoding.Instance);
			}
			if (Platform.EndsWith(text, "withECNR"))
			{
				string algorithm7 = text.Substring(0, text.LastIndexOf("with"));
				IDigest digest6 = DigestUtilities.GetDigest(algorithm7);
				return new DsaDigestSigner(new ECNRSigner(), digest6);
			}
			if (Platform.EndsWith(text, "withSM2"))
			{
				string algorithm8 = text.Substring(0, text.LastIndexOf("with"));
				IDigest digest7 = DigestUtilities.GetDigest(algorithm8);
				return new SM2Signer(digest7);
			}
			if (text.Equals("GOST3410"))
			{
				return new Gost3410DigestSigner(new Gost3410Signer(), new Gost3411Digest());
			}
			if (text.Equals("ECGOST3410"))
			{
				return new Gost3410DigestSigner(new ECGost3410Signer(), new Gost3411Digest());
			}
			if (text.Equals("SHA1WITHRSA/ISO9796-2"))
			{
				return new Iso9796d2Signer(new RsaBlindedEngine(), new Sha1Digest(), true);
			}
			if (text.Equals("MD5WITHRSA/ISO9796-2"))
			{
				return new Iso9796d2Signer(new RsaBlindedEngine(), new MD5Digest(), true);
			}
			if (text.Equals("RIPEMD160WITHRSA/ISO9796-2"))
			{
				return new Iso9796d2Signer(new RsaBlindedEngine(), new RipeMD160Digest(), true);
			}
			if (Platform.EndsWith(text, "/X9.31"))
			{
				string text2 = text.Substring(0, text.Length - "/X9.31".Length);
				int num = Platform.IndexOf(text2, "WITH");
				if (num > 0)
				{
					int num2 = num + "WITH".Length;
					string algorithm9 = text2.Substring(0, num);
					IDigest digest8 = DigestUtilities.GetDigest(algorithm9);
					string text3 = text2.Substring(num2, text2.Length - num2);
					if (text3.Equals("RSA"))
					{
						IAsymmetricBlockCipher cipher = new RsaBlindedEngine();
						return new X931Signer(cipher, digest8);
					}
				}
			}
			throw new SecurityUtilityException("Signer " + algorithm + " not recognised.");
		}

		// Token: 0x06003C39 RID: 15417 RVA: 0x0014D46C File Offset: 0x0014D46C
		public static string GetEncodingName(DerObjectIdentifier oid)
		{
			return (string)SignerUtilities.algorithms[oid.Id];
		}

		// Token: 0x06003C3A RID: 15418 RVA: 0x0014D484 File Offset: 0x0014D484
		public static ISigner InitSigner(DerObjectIdentifier algorithmOid, bool forSigning, AsymmetricKeyParameter privateKey, SecureRandom random)
		{
			return SignerUtilities.InitSigner(algorithmOid.Id, forSigning, privateKey, random);
		}

		// Token: 0x06003C3B RID: 15419 RVA: 0x0014D494 File Offset: 0x0014D494
		public static ISigner InitSigner(string algorithm, bool forSigning, AsymmetricKeyParameter privateKey, SecureRandom random)
		{
			ISigner signer = SignerUtilities.GetSigner(algorithm);
			signer.Init(forSigning, ParameterUtilities.WithRandom(privateKey, random));
			return signer;
		}

		// Token: 0x04001EA6 RID: 7846
		internal static readonly IDictionary algorithms = Platform.CreateHashtable();

		// Token: 0x04001EA7 RID: 7847
		internal static readonly IDictionary oids = Platform.CreateHashtable();
	}
}
