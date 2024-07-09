using System;
using System.Collections;
using Org.BouncyCastle.Asn1.Rosstandart;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Math.EC.Multiplier;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Asn1.CryptoPro
{
	// Token: 0x02000141 RID: 321
	public sealed class ECGost3410NamedCurves
	{
		// Token: 0x06000B43 RID: 2883 RVA: 0x0004ADA8 File Offset: 0x0004ADA8
		private static ECPoint ConfigureBasepoint(ECCurve curve, BigInteger x, BigInteger y)
		{
			ECPoint ecpoint = curve.CreatePoint(x, y);
			WNafUtilities.ConfigureBasepoint(ecpoint);
			return ecpoint;
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x0004ADCC File Offset: 0x0004ADCC
		private static ECCurve ConfigureCurve(ECCurve curve)
		{
			return curve;
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x0004ADD0 File Offset: 0x0004ADD0
		private ECGost3410NamedCurves()
		{
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x0004ADD8 File Offset: 0x0004ADD8
		static ECGost3410NamedCurves()
		{
			BigInteger q = new BigInteger("115792089237316195423570985008687907853269984665640564039457584007913129639319");
			BigInteger bigInteger = new BigInteger("115792089237316195423570985008687907853073762908499243225378155805079068850323");
			ECCurve curve = ECGost3410NamedCurves.ConfigureCurve(new FpCurve(q, new BigInteger("115792089237316195423570985008687907853269984665640564039457584007913129639316"), new BigInteger("166"), bigInteger, BigInteger.One));
			ECDomainParameters value = new ECDomainParameters(curve, ECGost3410NamedCurves.ConfigureBasepoint(curve, new BigInteger("1"), new BigInteger("64033881142927202683649881450433473985931760268884941288852745803908878638612")), bigInteger, BigInteger.One);
			ECGost3410NamedCurves.parameters[CryptoProObjectIdentifiers.GostR3410x2001CryptoProA] = value;
			q = new BigInteger("115792089237316195423570985008687907853269984665640564039457584007913129639319");
			bigInteger = new BigInteger("115792089237316195423570985008687907853073762908499243225378155805079068850323");
			curve = ECGost3410NamedCurves.ConfigureCurve(new FpCurve(q, new BigInteger("115792089237316195423570985008687907853269984665640564039457584007913129639316"), new BigInteger("166"), bigInteger, BigInteger.One));
			value = new ECDomainParameters(curve, ECGost3410NamedCurves.ConfigureBasepoint(curve, new BigInteger("1"), new BigInteger("64033881142927202683649881450433473985931760268884941288852745803908878638612")), bigInteger, BigInteger.One);
			ECGost3410NamedCurves.parameters[CryptoProObjectIdentifiers.GostR3410x2001CryptoProXchA] = value;
			q = new BigInteger("57896044618658097711785492504343953926634992332820282019728792003956564823193");
			bigInteger = new BigInteger("57896044618658097711785492504343953927102133160255826820068844496087732066703");
			curve = ECGost3410NamedCurves.ConfigureCurve(new FpCurve(q, new BigInteger("57896044618658097711785492504343953926634992332820282019728792003956564823190"), new BigInteger("28091019353058090096996979000309560759124368558014865957655842872397301267595"), bigInteger, BigInteger.One));
			value = new ECDomainParameters(curve, ECGost3410NamedCurves.ConfigureBasepoint(curve, new BigInteger("1"), new BigInteger("28792665814854611296992347458380284135028636778229113005756334730996303888124")), bigInteger, BigInteger.One);
			ECGost3410NamedCurves.parameters[CryptoProObjectIdentifiers.GostR3410x2001CryptoProB] = value;
			q = new BigInteger("70390085352083305199547718019018437841079516630045180471284346843705633502619");
			bigInteger = new BigInteger("70390085352083305199547718019018437840920882647164081035322601458352298396601");
			curve = ECGost3410NamedCurves.ConfigureCurve(new FpCurve(q, new BigInteger("70390085352083305199547718019018437841079516630045180471284346843705633502616"), new BigInteger("32858"), bigInteger, BigInteger.One));
			value = new ECDomainParameters(curve, ECGost3410NamedCurves.ConfigureBasepoint(curve, new BigInteger("0"), new BigInteger("29818893917731240733471273240314769927240550812383695689146495261604565990247")), bigInteger, BigInteger.One);
			ECGost3410NamedCurves.parameters[CryptoProObjectIdentifiers.GostR3410x2001CryptoProXchB] = value;
			q = new BigInteger("70390085352083305199547718019018437841079516630045180471284346843705633502619");
			bigInteger = new BigInteger("70390085352083305199547718019018437840920882647164081035322601458352298396601");
			curve = ECGost3410NamedCurves.ConfigureCurve(new FpCurve(q, new BigInteger("70390085352083305199547718019018437841079516630045180471284346843705633502616"), new BigInteger("32858"), bigInteger, BigInteger.One));
			value = new ECDomainParameters(curve, ECGost3410NamedCurves.ConfigureBasepoint(curve, new BigInteger("0"), new BigInteger("29818893917731240733471273240314769927240550812383695689146495261604565990247")), bigInteger, BigInteger.One);
			ECGost3410NamedCurves.parameters[CryptoProObjectIdentifiers.GostR3410x2001CryptoProC] = value;
			q = new BigInteger("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFD97", 16);
			bigInteger = new BigInteger("400000000000000000000000000000000FD8CDDFC87B6635C115AF556C360C67", 16);
			curve = ECGost3410NamedCurves.ConfigureCurve(new FpCurve(q, new BigInteger("C2173F1513981673AF4892C23035A27CE25E2013BF95AA33B22C656F277E7335", 16), new BigInteger("295F9BAE7428ED9CCC20E7C359A9D41A22FCCD9108E17BF7BA9337A6F8AE9513", 16), bigInteger, BigInteger.Four));
			value = new ECDomainParameters(curve, ECGost3410NamedCurves.ConfigureBasepoint(curve, new BigInteger("91E38443A5E82C0D880923425712B2BB658B9196932E02C78B2582FE742DAA28", 16), new BigInteger("32879423AB1A0375895786C4BB46E9565FDE0B5344766740AF268ADB32322E5C", 16)), bigInteger, BigInteger.Four);
			ECGost3410NamedCurves.parameters[RosstandartObjectIdentifiers.id_tc26_gost_3410_12_256_paramSetA] = value;
			q = new BigInteger("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFDC7", 16);
			bigInteger = new BigInteger("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF27E69532F48D89116FF22B8D4E0560609B4B38ABFAD2B85DCACDB1411F10B275", 16);
			curve = ECGost3410NamedCurves.ConfigureCurve(new FpCurve(q, new BigInteger("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFDC4", 16), new BigInteger("E8C2505DEDFC86DDC1BD0B2B6667F1DA34B82574761CB0E879BD081CFD0B6265EE3CB090F30D27614CB4574010DA90DD862EF9D4EBEE4761503190785A71C760", 16), bigInteger, BigInteger.One));
			value = new ECDomainParameters(curve, ECGost3410NamedCurves.ConfigureBasepoint(curve, new BigInteger("00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000003"), new BigInteger("7503CFE87A836AE3A61B8816E25450E6CE5E1C93ACF1ABC1778064FDCBEFA921DF1626BE4FD036E93D75E6A50E3A41E98028FE5FC235F5B889A589CB5215F2A4", 16)), bigInteger, BigInteger.One);
			ECGost3410NamedCurves.parameters[RosstandartObjectIdentifiers.id_tc26_gost_3410_12_512_paramSetA] = value;
			q = new BigInteger("8000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000006F", 16);
			bigInteger = new BigInteger("800000000000000000000000000000000000000000000000000000000000000149A1EC142565A545ACFDB77BD9D40CFA8B996712101BEA0EC6346C54374F25BD", 16);
			curve = ECGost3410NamedCurves.ConfigureCurve(new FpCurve(q, new BigInteger("8000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000006C", 16), new BigInteger("687D1B459DC841457E3E06CF6F5E2517B97C7D614AF138BCBF85DC806C4B289F3E965D2DB1416D217F8B276FAD1AB69C50F78BEE1FA3106EFB8CCBC7C5140116", 16), bigInteger, BigInteger.One));
			value = new ECDomainParameters(curve, ECGost3410NamedCurves.ConfigureBasepoint(curve, new BigInteger("00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000002"), new BigInteger("1A8F7EDA389B094C2C071E3647A8940F3C123B697578C213BE6DD9E6C8EC7335DCB228FD1EDF4A39152CBCAAF8C0398828041055F94CEEEC7E21340780FE41BD", 16)), bigInteger, BigInteger.One);
			ECGost3410NamedCurves.parameters[RosstandartObjectIdentifiers.id_tc26_gost_3410_12_512_paramSetB] = value;
			q = new BigInteger("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFDC7", 16);
			bigInteger = new BigInteger("3FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFC98CDBA46506AB004C33A9FF5147502CC8EDA9E7A769A12694623CEF47F023ED", 16);
			curve = ECGost3410NamedCurves.ConfigureCurve(new FpCurve(q, new BigInteger("DC9203E514A721875485A529D2C722FB187BC8980EB866644DE41C68E143064546E861C0E2C9EDD92ADE71F46FCF50FF2AD97F951FDA9F2A2EB6546F39689BD3", 16), new BigInteger("B4C4EE28CEBC6C2C8AC12952CF37F16AC7EFB6A9F69F4B57FFDA2E4F0DE5ADE038CBC2FFF719D2C18DE0284B8BFEF3B52B8CC7A5F5BF0A3C8D2319A5312557E1", 16), bigInteger, BigInteger.Four));
			value = new ECDomainParameters(curve, ECGost3410NamedCurves.ConfigureBasepoint(curve, new BigInteger("E2E31EDFC23DE7BDEBE241CE593EF5DE2295B7A9CBAEF021D385F7074CEA043AA27272A7AE602BF2A7B9033DB9ED3610C6FB85487EAE97AAC5BC7928C1950148", 16), new BigInteger("F5CE40D95B5EB899ABBCCFF5911CB8577939804D6527378B8C108C3D2090FF9BE18E2D33E3021ED2EF32D85822423B6304F726AA854BAE07D0396E9A9ADDC40F", 16)), bigInteger, BigInteger.Four);
			ECGost3410NamedCurves.parameters[RosstandartObjectIdentifiers.id_tc26_gost_3410_12_512_paramSetC] = value;
			ECGost3410NamedCurves.objIds["GostR3410-2001-CryptoPro-A"] = CryptoProObjectIdentifiers.GostR3410x2001CryptoProA;
			ECGost3410NamedCurves.objIds["GostR3410-2001-CryptoPro-B"] = CryptoProObjectIdentifiers.GostR3410x2001CryptoProB;
			ECGost3410NamedCurves.objIds["GostR3410-2001-CryptoPro-C"] = CryptoProObjectIdentifiers.GostR3410x2001CryptoProC;
			ECGost3410NamedCurves.objIds["GostR3410-2001-CryptoPro-XchA"] = CryptoProObjectIdentifiers.GostR3410x2001CryptoProXchA;
			ECGost3410NamedCurves.objIds["GostR3410-2001-CryptoPro-XchB"] = CryptoProObjectIdentifiers.GostR3410x2001CryptoProXchB;
			ECGost3410NamedCurves.objIds["Tc26-Gost-3410-12-256-paramSetA"] = RosstandartObjectIdentifiers.id_tc26_gost_3410_12_256_paramSetA;
			ECGost3410NamedCurves.objIds["Tc26-Gost-3410-12-512-paramSetA"] = RosstandartObjectIdentifiers.id_tc26_gost_3410_12_512_paramSetA;
			ECGost3410NamedCurves.objIds["Tc26-Gost-3410-12-512-paramSetB"] = RosstandartObjectIdentifiers.id_tc26_gost_3410_12_512_paramSetB;
			ECGost3410NamedCurves.objIds["Tc26-Gost-3410-12-512-paramSetC"] = RosstandartObjectIdentifiers.id_tc26_gost_3410_12_512_paramSetC;
			ECGost3410NamedCurves.names[CryptoProObjectIdentifiers.GostR3410x2001CryptoProA] = "GostR3410-2001-CryptoPro-A";
			ECGost3410NamedCurves.names[CryptoProObjectIdentifiers.GostR3410x2001CryptoProB] = "GostR3410-2001-CryptoPro-B";
			ECGost3410NamedCurves.names[CryptoProObjectIdentifiers.GostR3410x2001CryptoProC] = "GostR3410-2001-CryptoPro-C";
			ECGost3410NamedCurves.names[CryptoProObjectIdentifiers.GostR3410x2001CryptoProXchA] = "GostR3410-2001-CryptoPro-XchA";
			ECGost3410NamedCurves.names[CryptoProObjectIdentifiers.GostR3410x2001CryptoProXchB] = "GostR3410-2001-CryptoPro-XchB";
			ECGost3410NamedCurves.names[RosstandartObjectIdentifiers.id_tc26_gost_3410_12_256_paramSetA] = "Tc26-Gost-3410-12-256-paramSetA";
			ECGost3410NamedCurves.names[RosstandartObjectIdentifiers.id_tc26_gost_3410_12_512_paramSetA] = "Tc26-Gost-3410-12-512-paramSetA";
			ECGost3410NamedCurves.names[RosstandartObjectIdentifiers.id_tc26_gost_3410_12_512_paramSetB] = "Tc26-Gost-3410-12-512-paramSetB";
			ECGost3410NamedCurves.names[RosstandartObjectIdentifiers.id_tc26_gost_3410_12_512_paramSetC] = "Tc26-Gost-3410-12-512-paramSetC";
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x0004B3A8 File Offset: 0x0004B3A8
		public static ECDomainParameters GetByOid(DerObjectIdentifier oid)
		{
			return (ECDomainParameters)ECGost3410NamedCurves.parameters[oid];
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000B48 RID: 2888 RVA: 0x0004B3BC File Offset: 0x0004B3BC
		public static IEnumerable Names
		{
			get
			{
				return new EnumerableProxy(ECGost3410NamedCurves.names.Values);
			}
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x0004B3D0 File Offset: 0x0004B3D0
		public static ECDomainParameters GetByName(string name)
		{
			DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)ECGost3410NamedCurves.objIds[name];
			if (derObjectIdentifier != null)
			{
				return (ECDomainParameters)ECGost3410NamedCurves.parameters[derObjectIdentifier];
			}
			return null;
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x0004B40C File Offset: 0x0004B40C
		public static string GetName(DerObjectIdentifier oid)
		{
			return (string)ECGost3410NamedCurves.names[oid];
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x0004B420 File Offset: 0x0004B420
		public static DerObjectIdentifier GetOid(string name)
		{
			return (DerObjectIdentifier)ECGost3410NamedCurves.objIds[name];
		}

		// Token: 0x040007B8 RID: 1976
		internal static readonly IDictionary objIds = Platform.CreateHashtable();

		// Token: 0x040007B9 RID: 1977
		internal static readonly IDictionary parameters = Platform.CreateHashtable();

		// Token: 0x040007BA RID: 1978
		internal static readonly IDictionary names = Platform.CreateHashtable();
	}
}
