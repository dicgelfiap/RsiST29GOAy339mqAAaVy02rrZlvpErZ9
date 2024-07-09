using System;
using System.Collections;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Math.EC.Multiplier;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Asn1.GM
{
	// Token: 0x0200016B RID: 363
	public sealed class GMNamedCurves
	{
		// Token: 0x06000C5A RID: 3162 RVA: 0x0004F844 File Offset: 0x0004F844
		private GMNamedCurves()
		{
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x0004F84C File Offset: 0x0004F84C
		private static X9ECPoint ConfigureBasepoint(ECCurve curve, string encoding)
		{
			X9ECPoint x9ECPoint = new X9ECPoint(curve, Hex.DecodeStrict(encoding));
			WNafUtilities.ConfigureBasepoint(x9ECPoint.Point);
			return x9ECPoint;
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x0004F878 File Offset: 0x0004F878
		private static ECCurve ConfigureCurve(ECCurve curve)
		{
			return curve;
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x0004F87C File Offset: 0x0004F87C
		private static BigInteger FromHex(string hex)
		{
			return new BigInteger(1, Hex.DecodeStrict(hex));
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x0004F88C File Offset: 0x0004F88C
		private static void DefineCurve(string name, DerObjectIdentifier oid, X9ECParametersHolder holder)
		{
			GMNamedCurves.objIds.Add(Platform.ToUpperInvariant(name), oid);
			GMNamedCurves.names.Add(oid, name);
			GMNamedCurves.curves.Add(oid, holder);
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x0004F8B8 File Offset: 0x0004F8B8
		static GMNamedCurves()
		{
			GMNamedCurves.DefineCurve("wapip192v1", GMObjectIdentifiers.wapip192v1, GMNamedCurves.WapiP192V1Holder.Instance);
			GMNamedCurves.DefineCurve("sm2p256v1", GMObjectIdentifiers.sm2p256v1, GMNamedCurves.SM2P256V1Holder.Instance);
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x0004F910 File Offset: 0x0004F910
		public static X9ECParameters GetByName(string name)
		{
			DerObjectIdentifier oid = GMNamedCurves.GetOid(name);
			if (oid != null)
			{
				return GMNamedCurves.GetByOid(oid);
			}
			return null;
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x0004F938 File Offset: 0x0004F938
		public static X9ECParameters GetByOid(DerObjectIdentifier oid)
		{
			X9ECParametersHolder x9ECParametersHolder = (X9ECParametersHolder)GMNamedCurves.curves[oid];
			if (x9ECParametersHolder != null)
			{
				return x9ECParametersHolder.Parameters;
			}
			return null;
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x0004F968 File Offset: 0x0004F968
		public static DerObjectIdentifier GetOid(string name)
		{
			return (DerObjectIdentifier)GMNamedCurves.objIds[Platform.ToUpperInvariant(name)];
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x0004F980 File Offset: 0x0004F980
		public static string GetName(DerObjectIdentifier oid)
		{
			return (string)GMNamedCurves.names[oid];
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000C64 RID: 3172 RVA: 0x0004F994 File Offset: 0x0004F994
		public static IEnumerable Names
		{
			get
			{
				return new EnumerableProxy(GMNamedCurves.names.Values);
			}
		}

		// Token: 0x0400083D RID: 2109
		private static readonly IDictionary objIds = Platform.CreateHashtable();

		// Token: 0x0400083E RID: 2110
		private static readonly IDictionary curves = Platform.CreateHashtable();

		// Token: 0x0400083F RID: 2111
		private static readonly IDictionary names = Platform.CreateHashtable();

		// Token: 0x02000D87 RID: 3463
		internal class SM2P256V1Holder : X9ECParametersHolder
		{
			// Token: 0x06008A7E RID: 35454 RVA: 0x0029ACDC File Offset: 0x0029ACDC
			private SM2P256V1Holder()
			{
			}

			// Token: 0x06008A7F RID: 35455 RVA: 0x0029ACE4 File Offset: 0x0029ACE4
			protected override X9ECParameters CreateParameters()
			{
				BigInteger q = GMNamedCurves.FromHex("FFFFFFFEFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF00000000FFFFFFFFFFFFFFFF");
				BigInteger a = GMNamedCurves.FromHex("FFFFFFFEFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF00000000FFFFFFFFFFFFFFFC");
				BigInteger b = GMNamedCurves.FromHex("28E9FA9E9D9F5E344D5A9E4BCF6509A7F39789F515AB8F92DDBCBD414D940E93");
				byte[] seed = null;
				BigInteger bigInteger = GMNamedCurves.FromHex("FFFFFFFEFFFFFFFFFFFFFFFFFFFFFFFF7203DF6B21C6052B53BBF40939D54123");
				BigInteger one = BigInteger.One;
				ECCurve curve = GMNamedCurves.ConfigureCurve(new FpCurve(q, a, b, bigInteger, one));
				X9ECPoint g = GMNamedCurves.ConfigureBasepoint(curve, "0432C4AE2C1F1981195F9904466A39C9948FE30BBFF2660BE1715A4589334C74C7BC3736A2F4F6779C59BDCEE36B692153D0A9877CC62A474002DF32E52139F0A0");
				return new X9ECParameters(curve, g, bigInteger, one, seed);
			}

			// Token: 0x04003FD2 RID: 16338
			internal static readonly X9ECParametersHolder Instance = new GMNamedCurves.SM2P256V1Holder();
		}

		// Token: 0x02000D88 RID: 3464
		internal class WapiP192V1Holder : X9ECParametersHolder
		{
			// Token: 0x06008A81 RID: 35457 RVA: 0x0029AD68 File Offset: 0x0029AD68
			private WapiP192V1Holder()
			{
			}

			// Token: 0x06008A82 RID: 35458 RVA: 0x0029AD70 File Offset: 0x0029AD70
			protected override X9ECParameters CreateParameters()
			{
				BigInteger q = GMNamedCurves.FromHex("BDB6F4FE3E8B1D9E0DA8C0D46F4C318CEFE4AFE3B6B8551F");
				BigInteger a = GMNamedCurves.FromHex("BB8E5E8FBC115E139FE6A814FE48AAA6F0ADA1AA5DF91985");
				BigInteger b = GMNamedCurves.FromHex("1854BEBDC31B21B7AEFC80AB0ECD10D5B1B3308E6DBF11C1");
				byte[] seed = null;
				BigInteger bigInteger = GMNamedCurves.FromHex("BDB6F4FE3E8B1D9E0DA8C0D40FC962195DFAE76F56564677");
				BigInteger one = BigInteger.One;
				ECCurve curve = GMNamedCurves.ConfigureCurve(new FpCurve(q, a, b, bigInteger, one));
				X9ECPoint g = GMNamedCurves.ConfigureBasepoint(curve, "044AD5F7048DE709AD51236DE65E4D4B482C836DC6E410664002BB3A02D4AAADACAE24817A4CA3A1B014B5270432DB27D2");
				return new X9ECParameters(curve, g, bigInteger, one, seed);
			}

			// Token: 0x04003FD3 RID: 16339
			internal static readonly X9ECParametersHolder Instance = new GMNamedCurves.WapiP192V1Holder();
		}
	}
}
