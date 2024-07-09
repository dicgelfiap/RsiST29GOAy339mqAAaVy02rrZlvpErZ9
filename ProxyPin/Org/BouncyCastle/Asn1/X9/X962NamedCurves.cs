using System;
using System.Collections;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Math.EC.Multiplier;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Asn1.X9
{
	// Token: 0x0200022D RID: 557
	public sealed class X962NamedCurves
	{
		// Token: 0x06001202 RID: 4610 RVA: 0x00066310 File Offset: 0x00066310
		private X962NamedCurves()
		{
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x00066318 File Offset: 0x00066318
		private static X9ECPoint ConfigureBasepoint(ECCurve curve, string encoding)
		{
			X9ECPoint x9ECPoint = new X9ECPoint(curve, Hex.DecodeStrict(encoding));
			WNafUtilities.ConfigureBasepoint(x9ECPoint.Point);
			return x9ECPoint;
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x00066344 File Offset: 0x00066344
		private static ECCurve ConfigureCurve(ECCurve curve)
		{
			return curve;
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x00066348 File Offset: 0x00066348
		private static BigInteger FromHex(string hex)
		{
			return new BigInteger(1, Hex.DecodeStrict(hex));
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x00066358 File Offset: 0x00066358
		private static void DefineCurve(string name, DerObjectIdentifier oid, X9ECParametersHolder holder)
		{
			X962NamedCurves.objIds.Add(Platform.ToUpperInvariant(name), oid);
			X962NamedCurves.names.Add(oid, name);
			X962NamedCurves.curves.Add(oid, holder);
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x00066384 File Offset: 0x00066384
		static X962NamedCurves()
		{
			X962NamedCurves.DefineCurve("prime192v1", X9ObjectIdentifiers.Prime192v1, X962NamedCurves.Prime192v1Holder.Instance);
			X962NamedCurves.DefineCurve("prime192v2", X9ObjectIdentifiers.Prime192v2, X962NamedCurves.Prime192v2Holder.Instance);
			X962NamedCurves.DefineCurve("prime192v3", X9ObjectIdentifiers.Prime192v3, X962NamedCurves.Prime192v3Holder.Instance);
			X962NamedCurves.DefineCurve("prime239v1", X9ObjectIdentifiers.Prime239v1, X962NamedCurves.Prime239v1Holder.Instance);
			X962NamedCurves.DefineCurve("prime239v2", X9ObjectIdentifiers.Prime239v2, X962NamedCurves.Prime239v2Holder.Instance);
			X962NamedCurves.DefineCurve("prime239v3", X9ObjectIdentifiers.Prime239v3, X962NamedCurves.Prime239v3Holder.Instance);
			X962NamedCurves.DefineCurve("prime256v1", X9ObjectIdentifiers.Prime256v1, X962NamedCurves.Prime256v1Holder.Instance);
			X962NamedCurves.DefineCurve("c2pnb163v1", X9ObjectIdentifiers.C2Pnb163v1, X962NamedCurves.C2pnb163v1Holder.Instance);
			X962NamedCurves.DefineCurve("c2pnb163v2", X9ObjectIdentifiers.C2Pnb163v2, X962NamedCurves.C2pnb163v2Holder.Instance);
			X962NamedCurves.DefineCurve("c2pnb163v3", X9ObjectIdentifiers.C2Pnb163v3, X962NamedCurves.C2pnb163v3Holder.Instance);
			X962NamedCurves.DefineCurve("c2pnb176w1", X9ObjectIdentifiers.C2Pnb176w1, X962NamedCurves.C2pnb176w1Holder.Instance);
			X962NamedCurves.DefineCurve("c2tnb191v1", X9ObjectIdentifiers.C2Tnb191v1, X962NamedCurves.C2tnb191v1Holder.Instance);
			X962NamedCurves.DefineCurve("c2tnb191v2", X9ObjectIdentifiers.C2Tnb191v2, X962NamedCurves.C2tnb191v2Holder.Instance);
			X962NamedCurves.DefineCurve("c2tnb191v3", X9ObjectIdentifiers.C2Tnb191v3, X962NamedCurves.C2tnb191v3Holder.Instance);
			X962NamedCurves.DefineCurve("c2pnb208w1", X9ObjectIdentifiers.C2Pnb208w1, X962NamedCurves.C2pnb208w1Holder.Instance);
			X962NamedCurves.DefineCurve("c2tnb239v1", X9ObjectIdentifiers.C2Tnb239v1, X962NamedCurves.C2tnb239v1Holder.Instance);
			X962NamedCurves.DefineCurve("c2tnb239v2", X9ObjectIdentifiers.C2Tnb239v2, X962NamedCurves.C2tnb239v2Holder.Instance);
			X962NamedCurves.DefineCurve("c2tnb239v3", X9ObjectIdentifiers.C2Tnb239v3, X962NamedCurves.C2tnb239v3Holder.Instance);
			X962NamedCurves.DefineCurve("c2pnb272w1", X9ObjectIdentifiers.C2Pnb272w1, X962NamedCurves.C2pnb272w1Holder.Instance);
			X962NamedCurves.DefineCurve("c2pnb304w1", X9ObjectIdentifiers.C2Pnb304w1, X962NamedCurves.C2pnb304w1Holder.Instance);
			X962NamedCurves.DefineCurve("c2tnb359v1", X9ObjectIdentifiers.C2Tnb359v1, X962NamedCurves.C2tnb359v1Holder.Instance);
			X962NamedCurves.DefineCurve("c2pnb368w1", X9ObjectIdentifiers.C2Pnb368w1, X962NamedCurves.C2pnb368w1Holder.Instance);
			X962NamedCurves.DefineCurve("c2tnb431r1", X9ObjectIdentifiers.C2Tnb431r1, X962NamedCurves.C2tnb431r1Holder.Instance);
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x00066580 File Offset: 0x00066580
		public static X9ECParameters GetByName(string name)
		{
			DerObjectIdentifier oid = X962NamedCurves.GetOid(name);
			if (oid != null)
			{
				return X962NamedCurves.GetByOid(oid);
			}
			return null;
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x000665A8 File Offset: 0x000665A8
		public static X9ECParameters GetByOid(DerObjectIdentifier oid)
		{
			X9ECParametersHolder x9ECParametersHolder = (X9ECParametersHolder)X962NamedCurves.curves[oid];
			if (x9ECParametersHolder != null)
			{
				return x9ECParametersHolder.Parameters;
			}
			return null;
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x000665D8 File Offset: 0x000665D8
		public static DerObjectIdentifier GetOid(string name)
		{
			return (DerObjectIdentifier)X962NamedCurves.objIds[Platform.ToUpperInvariant(name)];
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x000665F0 File Offset: 0x000665F0
		public static string GetName(DerObjectIdentifier oid)
		{
			return (string)X962NamedCurves.names[oid];
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x0600120C RID: 4620 RVA: 0x00066604 File Offset: 0x00066604
		public static IEnumerable Names
		{
			get
			{
				return new EnumerableProxy(X962NamedCurves.names.Values);
			}
		}

		// Token: 0x04000D01 RID: 3329
		private static readonly IDictionary objIds = Platform.CreateHashtable();

		// Token: 0x04000D02 RID: 3330
		private static readonly IDictionary curves = Platform.CreateHashtable();

		// Token: 0x04000D03 RID: 3331
		private static readonly IDictionary names = Platform.CreateHashtable();

		// Token: 0x02000DBC RID: 3516
		internal class Prime192v1Holder : X9ECParametersHolder
		{
			// Token: 0x06008B13 RID: 35603 RVA: 0x0029C8EC File Offset: 0x0029C8EC
			private Prime192v1Holder()
			{
			}

			// Token: 0x06008B14 RID: 35604 RVA: 0x0029C8F4 File Offset: 0x0029C8F4
			protected override X9ECParameters CreateParameters()
			{
				BigInteger bigInteger = X962NamedCurves.FromHex("ffffffffffffffffffffffff99def836146bc9b1b4d22831");
				BigInteger one = BigInteger.One;
				ECCurve curve = X962NamedCurves.ConfigureCurve(new FpCurve(X962NamedCurves.FromHex("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFFFFFFFFFFFF"), X962NamedCurves.FromHex("fffffffffffffffffffffffffffffffefffffffffffffffc"), X962NamedCurves.FromHex("64210519e59c80e70fa7e9ab72243049feb8deecc146b9b1"), bigInteger, one));
				X9ECPoint g = X962NamedCurves.ConfigureBasepoint(curve, "03188da80eb03090f67cbf20eb43a18800f4ff0afd82ff1012");
				return new X9ECParameters(curve, g, bigInteger, one, Hex.DecodeStrict("3045AE6FC8422f64ED579528D38120EAE12196D5"));
			}

			// Token: 0x04004045 RID: 16453
			internal static readonly X9ECParametersHolder Instance = new X962NamedCurves.Prime192v1Holder();
		}

		// Token: 0x02000DBD RID: 3517
		internal class Prime192v2Holder : X9ECParametersHolder
		{
			// Token: 0x06008B16 RID: 35606 RVA: 0x0029C96C File Offset: 0x0029C96C
			private Prime192v2Holder()
			{
			}

			// Token: 0x06008B17 RID: 35607 RVA: 0x0029C974 File Offset: 0x0029C974
			protected override X9ECParameters CreateParameters()
			{
				BigInteger bigInteger = X962NamedCurves.FromHex("fffffffffffffffffffffffe5fb1a724dc80418648d8dd31");
				BigInteger one = BigInteger.One;
				ECCurve curve = X962NamedCurves.ConfigureCurve(new FpCurve(X962NamedCurves.FromHex("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFFFFFFFFFFFF"), X962NamedCurves.FromHex("fffffffffffffffffffffffffffffffefffffffffffffffc"), X962NamedCurves.FromHex("cc22d6dfb95c6b25e49c0d6364a4e5980c393aa21668d953"), bigInteger, one));
				X9ECPoint g = X962NamedCurves.ConfigureBasepoint(curve, "03eea2bae7e1497842f2de7769cfe9c989c072ad696f48034a");
				return new X9ECParameters(curve, g, bigInteger, one, Hex.DecodeStrict("31a92ee2029fd10d901b113e990710f0d21ac6b6"));
			}

			// Token: 0x04004046 RID: 16454
			internal static readonly X9ECParametersHolder Instance = new X962NamedCurves.Prime192v2Holder();
		}

		// Token: 0x02000DBE RID: 3518
		internal class Prime192v3Holder : X9ECParametersHolder
		{
			// Token: 0x06008B19 RID: 35609 RVA: 0x0029C9EC File Offset: 0x0029C9EC
			private Prime192v3Holder()
			{
			}

			// Token: 0x06008B1A RID: 35610 RVA: 0x0029C9F4 File Offset: 0x0029C9F4
			protected override X9ECParameters CreateParameters()
			{
				BigInteger bigInteger = X962NamedCurves.FromHex("ffffffffffffffffffffffff7a62d031c83f4294f640ec13");
				BigInteger one = BigInteger.One;
				ECCurve curve = X962NamedCurves.ConfigureCurve(new FpCurve(X962NamedCurves.FromHex("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFFFFFFFFFFFF"), X962NamedCurves.FromHex("fffffffffffffffffffffffffffffffefffffffffffffffc"), X962NamedCurves.FromHex("22123dc2395a05caa7423daeccc94760a7d462256bd56916"), bigInteger, one));
				X9ECPoint g = X962NamedCurves.ConfigureBasepoint(curve, "027d29778100c65a1da1783716588dce2b8b4aee8e228f1896");
				return new X9ECParameters(curve, g, bigInteger, one, Hex.DecodeStrict("c469684435deb378c4b65ca9591e2a5763059a2e"));
			}

			// Token: 0x04004047 RID: 16455
			internal static readonly X9ECParametersHolder Instance = new X962NamedCurves.Prime192v3Holder();
		}

		// Token: 0x02000DBF RID: 3519
		internal class Prime239v1Holder : X9ECParametersHolder
		{
			// Token: 0x06008B1C RID: 35612 RVA: 0x0029CA6C File Offset: 0x0029CA6C
			private Prime239v1Holder()
			{
			}

			// Token: 0x06008B1D RID: 35613 RVA: 0x0029CA74 File Offset: 0x0029CA74
			protected override X9ECParameters CreateParameters()
			{
				BigInteger bigInteger = X962NamedCurves.FromHex("7fffffffffffffffffffffff7fffff9e5e9a9f5d9071fbd1522688909d0b");
				BigInteger one = BigInteger.One;
				ECCurve curve = X962NamedCurves.ConfigureCurve(new FpCurve(new BigInteger("883423532389192164791648750360308885314476597252960362792450860609699839"), X962NamedCurves.FromHex("7fffffffffffffffffffffff7fffffffffff8000000000007ffffffffffc"), X962NamedCurves.FromHex("6b016c3bdcf18941d0d654921475ca71a9db2fb27d1d37796185c2942c0a"), bigInteger, one));
				X9ECPoint g = X962NamedCurves.ConfigureBasepoint(curve, "020ffa963cdca8816ccc33b8642bedf905c3d358573d3f27fbbd3b3cb9aaaf");
				return new X9ECParameters(curve, g, bigInteger, one, Hex.DecodeStrict("e43bb460f0b80cc0c0b075798e948060f8321b7d"));
			}

			// Token: 0x04004048 RID: 16456
			internal static readonly X9ECParametersHolder Instance = new X962NamedCurves.Prime239v1Holder();
		}

		// Token: 0x02000DC0 RID: 3520
		internal class Prime239v2Holder : X9ECParametersHolder
		{
			// Token: 0x06008B1F RID: 35615 RVA: 0x0029CAEC File Offset: 0x0029CAEC
			private Prime239v2Holder()
			{
			}

			// Token: 0x06008B20 RID: 35616 RVA: 0x0029CAF4 File Offset: 0x0029CAF4
			protected override X9ECParameters CreateParameters()
			{
				BigInteger bigInteger = X962NamedCurves.FromHex("7fffffffffffffffffffffff800000cfa7e8594377d414c03821bc582063");
				BigInteger one = BigInteger.One;
				ECCurve curve = X962NamedCurves.ConfigureCurve(new FpCurve(new BigInteger("883423532389192164791648750360308885314476597252960362792450860609699839"), X962NamedCurves.FromHex("7fffffffffffffffffffffff7fffffffffff8000000000007ffffffffffc"), X962NamedCurves.FromHex("617fab6832576cbbfed50d99f0249c3fee58b94ba0038c7ae84c8c832f2c"), bigInteger, one));
				X9ECPoint g = X962NamedCurves.ConfigureBasepoint(curve, "0238af09d98727705120c921bb5e9e26296a3cdcf2f35757a0eafd87b830e7");
				return new X9ECParameters(curve, g, bigInteger, one, Hex.DecodeStrict("e8b4011604095303ca3b8099982be09fcb9ae616"));
			}

			// Token: 0x04004049 RID: 16457
			internal static readonly X9ECParametersHolder Instance = new X962NamedCurves.Prime239v2Holder();
		}

		// Token: 0x02000DC1 RID: 3521
		internal class Prime239v3Holder : X9ECParametersHolder
		{
			// Token: 0x06008B22 RID: 35618 RVA: 0x0029CB6C File Offset: 0x0029CB6C
			private Prime239v3Holder()
			{
			}

			// Token: 0x06008B23 RID: 35619 RVA: 0x0029CB74 File Offset: 0x0029CB74
			protected override X9ECParameters CreateParameters()
			{
				BigInteger bigInteger = X962NamedCurves.FromHex("7fffffffffffffffffffffff7fffff975deb41b3a6057c3c432146526551");
				BigInteger one = BigInteger.One;
				ECCurve curve = X962NamedCurves.ConfigureCurve(new FpCurve(new BigInteger("883423532389192164791648750360308885314476597252960362792450860609699839"), X962NamedCurves.FromHex("7fffffffffffffffffffffff7fffffffffff8000000000007ffffffffffc"), X962NamedCurves.FromHex("255705fa2a306654b1f4cb03d6a750a30c250102d4988717d9ba15ab6d3e"), bigInteger, one));
				X9ECPoint g = X962NamedCurves.ConfigureBasepoint(curve, "036768ae8e18bb92cfcf005c949aa2c6d94853d0e660bbf854b1c9505fe95a");
				return new X9ECParameters(curve, g, bigInteger, one, Hex.DecodeStrict("7d7374168ffe3471b60a857686a19475d3bfa2ff"));
			}

			// Token: 0x0400404A RID: 16458
			internal static readonly X9ECParametersHolder Instance = new X962NamedCurves.Prime239v3Holder();
		}

		// Token: 0x02000DC2 RID: 3522
		internal class Prime256v1Holder : X9ECParametersHolder
		{
			// Token: 0x06008B25 RID: 35621 RVA: 0x0029CBEC File Offset: 0x0029CBEC
			private Prime256v1Holder()
			{
			}

			// Token: 0x06008B26 RID: 35622 RVA: 0x0029CBF4 File Offset: 0x0029CBF4
			protected override X9ECParameters CreateParameters()
			{
				BigInteger bigInteger = X962NamedCurves.FromHex("ffffffff00000000ffffffffffffffffbce6faada7179e84f3b9cac2fc632551");
				BigInteger one = BigInteger.One;
				ECCurve curve = X962NamedCurves.ConfigureCurve(new FpCurve(new BigInteger("115792089210356248762697446949407573530086143415290314195533631308867097853951"), X962NamedCurves.FromHex("ffffffff00000001000000000000000000000000fffffffffffffffffffffffc"), X962NamedCurves.FromHex("5ac635d8aa3a93e7b3ebbd55769886bc651d06b0cc53b0f63bce3c3e27d2604b"), bigInteger, one));
				X9ECPoint g = X962NamedCurves.ConfigureBasepoint(curve, "036b17d1f2e12c4247f8bce6e563a440f277037d812deb33a0f4a13945d898c296");
				return new X9ECParameters(curve, g, bigInteger, one, Hex.DecodeStrict("c49d360886e704936a6678e1139d26b7819f7e90"));
			}

			// Token: 0x0400404B RID: 16459
			internal static readonly X9ECParametersHolder Instance = new X962NamedCurves.Prime256v1Holder();
		}

		// Token: 0x02000DC3 RID: 3523
		internal class C2pnb163v1Holder : X9ECParametersHolder
		{
			// Token: 0x06008B28 RID: 35624 RVA: 0x0029CC6C File Offset: 0x0029CC6C
			private C2pnb163v1Holder()
			{
			}

			// Token: 0x06008B29 RID: 35625 RVA: 0x0029CC74 File Offset: 0x0029CC74
			protected override X9ECParameters CreateParameters()
			{
				BigInteger bigInteger = X962NamedCurves.FromHex("0400000000000000000001E60FC8821CC74DAEAFC1");
				BigInteger two = BigInteger.Two;
				ECCurve curve = X962NamedCurves.ConfigureCurve(new F2mCurve(163, 1, 2, 8, X962NamedCurves.FromHex("072546B5435234A422E0789675F432C89435DE5242"), X962NamedCurves.FromHex("00C9517D06D5240D3CFF38C74B20B6CD4D6F9DD4D9"), bigInteger, two));
				X9ECPoint g = X962NamedCurves.ConfigureBasepoint(curve, "0307AF69989546103D79329FCC3D74880F33BBE803CB");
				return new X9ECParameters(curve, g, bigInteger, two, Hex.DecodeStrict("D2C0FB15760860DEF1EEF4D696E6768756151754"));
			}

			// Token: 0x0400404C RID: 16460
			internal static readonly X9ECParametersHolder Instance = new X962NamedCurves.C2pnb163v1Holder();
		}

		// Token: 0x02000DC4 RID: 3524
		internal class C2pnb163v2Holder : X9ECParametersHolder
		{
			// Token: 0x06008B2B RID: 35627 RVA: 0x0029CCEC File Offset: 0x0029CCEC
			private C2pnb163v2Holder()
			{
			}

			// Token: 0x06008B2C RID: 35628 RVA: 0x0029CCF4 File Offset: 0x0029CCF4
			protected override X9ECParameters CreateParameters()
			{
				BigInteger bigInteger = X962NamedCurves.FromHex("03FFFFFFFFFFFFFFFFFFFDF64DE1151ADBB78F10A7");
				BigInteger two = BigInteger.Two;
				ECCurve curve = X962NamedCurves.ConfigureCurve(new F2mCurve(163, 1, 2, 8, X962NamedCurves.FromHex("0108B39E77C4B108BED981ED0E890E117C511CF072"), X962NamedCurves.FromHex("0667ACEB38AF4E488C407433FFAE4F1C811638DF20"), bigInteger, two));
				X9ECPoint g = X962NamedCurves.ConfigureBasepoint(curve, "030024266E4EB5106D0A964D92C4860E2671DB9B6CC5");
				return new X9ECParameters(curve, g, bigInteger, two);
			}

			// Token: 0x0400404D RID: 16461
			internal static readonly X9ECParametersHolder Instance = new X962NamedCurves.C2pnb163v2Holder();
		}

		// Token: 0x02000DC5 RID: 3525
		internal class C2pnb163v3Holder : X9ECParametersHolder
		{
			// Token: 0x06008B2E RID: 35630 RVA: 0x0029CD60 File Offset: 0x0029CD60
			private C2pnb163v3Holder()
			{
			}

			// Token: 0x06008B2F RID: 35631 RVA: 0x0029CD68 File Offset: 0x0029CD68
			protected override X9ECParameters CreateParameters()
			{
				BigInteger bigInteger = X962NamedCurves.FromHex("03FFFFFFFFFFFFFFFFFFFE1AEE140F110AFF961309");
				BigInteger two = BigInteger.Two;
				ECCurve curve = X962NamedCurves.ConfigureCurve(new F2mCurve(163, 1, 2, 8, X962NamedCurves.FromHex("07A526C63D3E25A256A007699F5447E32AE456B50E"), X962NamedCurves.FromHex("03F7061798EB99E238FD6F1BF95B48FEEB4854252B"), bigInteger, two));
				X9ECPoint g = X962NamedCurves.ConfigureBasepoint(curve, "0202F9F87B7C574D0BDECF8A22E6524775F98CDEBDCB");
				return new X9ECParameters(curve, g, bigInteger, two);
			}

			// Token: 0x0400404E RID: 16462
			internal static readonly X9ECParametersHolder Instance = new X962NamedCurves.C2pnb163v3Holder();
		}

		// Token: 0x02000DC6 RID: 3526
		internal class C2pnb176w1Holder : X9ECParametersHolder
		{
			// Token: 0x06008B31 RID: 35633 RVA: 0x0029CDD4 File Offset: 0x0029CDD4
			private C2pnb176w1Holder()
			{
			}

			// Token: 0x06008B32 RID: 35634 RVA: 0x0029CDDC File Offset: 0x0029CDDC
			protected override X9ECParameters CreateParameters()
			{
				BigInteger bigInteger = X962NamedCurves.FromHex("010092537397ECA4F6145799D62B0A19CE06FE26AD");
				BigInteger bigInteger2 = BigInteger.ValueOf(65390L);
				ECCurve curve = X962NamedCurves.ConfigureCurve(new F2mCurve(176, 1, 2, 43, X962NamedCurves.FromHex("E4E6DB2995065C407D9D39B8D0967B96704BA8E9C90B"), X962NamedCurves.FromHex("5DDA470ABE6414DE8EC133AE28E9BBD7FCEC0AE0FFF2"), bigInteger, bigInteger2));
				X9ECPoint g = X962NamedCurves.ConfigureBasepoint(curve, "038D16C2866798B600F9F08BB4A8E860F3298CE04A5798");
				return new X9ECParameters(curve, g, bigInteger, bigInteger2);
			}

			// Token: 0x0400404F RID: 16463
			internal static readonly X9ECParametersHolder Instance = new X962NamedCurves.C2pnb176w1Holder();
		}

		// Token: 0x02000DC7 RID: 3527
		internal class C2tnb191v1Holder : X9ECParametersHolder
		{
			// Token: 0x06008B34 RID: 35636 RVA: 0x0029CE50 File Offset: 0x0029CE50
			private C2tnb191v1Holder()
			{
			}

			// Token: 0x06008B35 RID: 35637 RVA: 0x0029CE58 File Offset: 0x0029CE58
			protected override X9ECParameters CreateParameters()
			{
				BigInteger bigInteger = X962NamedCurves.FromHex("40000000000000000000000004A20E90C39067C893BBB9A5");
				BigInteger two = BigInteger.Two;
				ECCurve curve = X962NamedCurves.ConfigureCurve(new F2mCurve(191, 9, X962NamedCurves.FromHex("2866537B676752636A68F56554E12640276B649EF7526267"), X962NamedCurves.FromHex("2E45EF571F00786F67B0081B9495A3D95462F5DE0AA185EC"), bigInteger, two));
				X9ECPoint g = X962NamedCurves.ConfigureBasepoint(curve, "0236B3DAF8A23206F9C4F299D7B21A9C369137F2C84AE1AA0D");
				return new X9ECParameters(curve, g, bigInteger, two, Hex.DecodeStrict("4E13CA542744D696E67687561517552F279A8C84"));
			}

			// Token: 0x04004050 RID: 16464
			internal static readonly X9ECParametersHolder Instance = new X962NamedCurves.C2tnb191v1Holder();
		}

		// Token: 0x02000DC8 RID: 3528
		internal class C2tnb191v2Holder : X9ECParametersHolder
		{
			// Token: 0x06008B37 RID: 35639 RVA: 0x0029CED0 File Offset: 0x0029CED0
			private C2tnb191v2Holder()
			{
			}

			// Token: 0x06008B38 RID: 35640 RVA: 0x0029CED8 File Offset: 0x0029CED8
			protected override X9ECParameters CreateParameters()
			{
				BigInteger bigInteger = X962NamedCurves.FromHex("20000000000000000000000050508CB89F652824E06B8173");
				BigInteger bigInteger2 = BigInteger.ValueOf(4L);
				ECCurve curve = X962NamedCurves.ConfigureCurve(new F2mCurve(191, 9, X962NamedCurves.FromHex("401028774D7777C7B7666D1366EA432071274F89FF01E718"), X962NamedCurves.FromHex("0620048D28BCBD03B6249C99182B7C8CD19700C362C46A01"), bigInteger, bigInteger2));
				X9ECPoint g = X962NamedCurves.ConfigureBasepoint(curve, "023809B2B7CC1B28CC5A87926AAD83FD28789E81E2C9E3BF10");
				return new X9ECParameters(curve, g, bigInteger, bigInteger2);
			}

			// Token: 0x04004051 RID: 16465
			internal static readonly X9ECParametersHolder Instance = new X962NamedCurves.C2tnb191v2Holder();
		}

		// Token: 0x02000DC9 RID: 3529
		internal class C2tnb191v3Holder : X9ECParametersHolder
		{
			// Token: 0x06008B3A RID: 35642 RVA: 0x0029CF48 File Offset: 0x0029CF48
			private C2tnb191v3Holder()
			{
			}

			// Token: 0x06008B3B RID: 35643 RVA: 0x0029CF50 File Offset: 0x0029CF50
			protected override X9ECParameters CreateParameters()
			{
				BigInteger bigInteger = X962NamedCurves.FromHex("155555555555555555555555610C0B196812BFB6288A3EA3");
				BigInteger bigInteger2 = BigInteger.ValueOf(6L);
				ECCurve curve = X962NamedCurves.ConfigureCurve(new F2mCurve(191, 9, X962NamedCurves.FromHex("6C01074756099122221056911C77D77E77A777E7E7E77FCB"), X962NamedCurves.FromHex("71FE1AF926CF847989EFEF8DB459F66394D90F32AD3F15E8"), bigInteger, bigInteger2));
				X9ECPoint g = X962NamedCurves.ConfigureBasepoint(curve, "03375D4CE24FDE434489DE8746E71786015009E66E38A926DD");
				return new X9ECParameters(curve, g, bigInteger, bigInteger2);
			}

			// Token: 0x04004052 RID: 16466
			internal static readonly X9ECParametersHolder Instance = new X962NamedCurves.C2tnb191v3Holder();
		}

		// Token: 0x02000DCA RID: 3530
		internal class C2pnb208w1Holder : X9ECParametersHolder
		{
			// Token: 0x06008B3D RID: 35645 RVA: 0x0029CFC0 File Offset: 0x0029CFC0
			private C2pnb208w1Holder()
			{
			}

			// Token: 0x06008B3E RID: 35646 RVA: 0x0029CFC8 File Offset: 0x0029CFC8
			protected override X9ECParameters CreateParameters()
			{
				BigInteger bigInteger = X962NamedCurves.FromHex("0101BAF95C9723C57B6C21DA2EFF2D5ED588BDD5717E212F9D");
				BigInteger bigInteger2 = BigInteger.ValueOf(65096L);
				ECCurve curve = X962NamedCurves.ConfigureCurve(new F2mCurve(208, 1, 2, 83, BigInteger.Zero, X962NamedCurves.FromHex("C8619ED45A62E6212E1160349E2BFA844439FAFC2A3FD1638F9E"), bigInteger, bigInteger2));
				X9ECPoint g = X962NamedCurves.ConfigureBasepoint(curve, "0289FDFBE4ABE193DF9559ECF07AC0CE78554E2784EB8C1ED1A57A");
				return new X9ECParameters(curve, g, bigInteger, bigInteger2);
			}

			// Token: 0x04004053 RID: 16467
			internal static readonly X9ECParametersHolder Instance = new X962NamedCurves.C2pnb208w1Holder();
		}

		// Token: 0x02000DCB RID: 3531
		internal class C2tnb239v1Holder : X9ECParametersHolder
		{
			// Token: 0x06008B40 RID: 35648 RVA: 0x0029D038 File Offset: 0x0029D038
			private C2tnb239v1Holder()
			{
			}

			// Token: 0x06008B41 RID: 35649 RVA: 0x0029D040 File Offset: 0x0029D040
			protected override X9ECParameters CreateParameters()
			{
				BigInteger bigInteger = X962NamedCurves.FromHex("2000000000000000000000000000000F4D42FFE1492A4993F1CAD666E447");
				BigInteger bigInteger2 = BigInteger.ValueOf(4L);
				ECCurve curve = X962NamedCurves.ConfigureCurve(new F2mCurve(239, 36, X962NamedCurves.FromHex("32010857077C5431123A46B808906756F543423E8D27877578125778AC76"), X962NamedCurves.FromHex("790408F2EEDAF392B012EDEFB3392F30F4327C0CA3F31FC383C422AA8C16"), bigInteger, bigInteger2));
				X9ECPoint g = X962NamedCurves.ConfigureBasepoint(curve, "0257927098FA932E7C0A96D3FD5B706EF7E5F5C156E16B7E7C86038552E91D");
				return new X9ECParameters(curve, g, bigInteger, bigInteger2);
			}

			// Token: 0x04004054 RID: 16468
			internal static readonly X9ECParametersHolder Instance = new X962NamedCurves.C2tnb239v1Holder();
		}

		// Token: 0x02000DCC RID: 3532
		internal class C2tnb239v2Holder : X9ECParametersHolder
		{
			// Token: 0x06008B43 RID: 35651 RVA: 0x0029D0B0 File Offset: 0x0029D0B0
			private C2tnb239v2Holder()
			{
			}

			// Token: 0x06008B44 RID: 35652 RVA: 0x0029D0B8 File Offset: 0x0029D0B8
			protected override X9ECParameters CreateParameters()
			{
				BigInteger bigInteger = X962NamedCurves.FromHex("1555555555555555555555555555553C6F2885259C31E3FCDF154624522D");
				BigInteger bigInteger2 = BigInteger.ValueOf(6L);
				ECCurve curve = X962NamedCurves.ConfigureCurve(new F2mCurve(239, 36, X962NamedCurves.FromHex("4230017757A767FAE42398569B746325D45313AF0766266479B75654E65F"), X962NamedCurves.FromHex("5037EA654196CFF0CD82B2C14A2FCF2E3FF8775285B545722F03EACDB74B"), bigInteger, bigInteger2));
				X9ECPoint g = X962NamedCurves.ConfigureBasepoint(curve, "0228F9D04E900069C8DC47A08534FE76D2B900B7D7EF31F5709F200C4CA205");
				return new X9ECParameters(curve, g, bigInteger, bigInteger2);
			}

			// Token: 0x04004055 RID: 16469
			internal static readonly X9ECParametersHolder Instance = new X962NamedCurves.C2tnb239v2Holder();
		}

		// Token: 0x02000DCD RID: 3533
		internal class C2tnb239v3Holder : X9ECParametersHolder
		{
			// Token: 0x06008B46 RID: 35654 RVA: 0x0029D128 File Offset: 0x0029D128
			private C2tnb239v3Holder()
			{
			}

			// Token: 0x06008B47 RID: 35655 RVA: 0x0029D130 File Offset: 0x0029D130
			protected override X9ECParameters CreateParameters()
			{
				BigInteger bigInteger = X962NamedCurves.FromHex("0CCCCCCCCCCCCCCCCCCCCCCCCCCCCCAC4912D2D9DF903EF9888B8A0E4CFF");
				BigInteger bigInteger2 = BigInteger.ValueOf(10L);
				ECCurve curve = X962NamedCurves.ConfigureCurve(new F2mCurve(239, 36, X962NamedCurves.FromHex("01238774666A67766D6676F778E676B66999176666E687666D8766C66A9F"), X962NamedCurves.FromHex("6A941977BA9F6A435199ACFC51067ED587F519C5ECB541B8E44111DE1D40"), bigInteger, bigInteger2));
				X9ECPoint g = X962NamedCurves.ConfigureBasepoint(curve, "0370F6E9D04D289C4E89913CE3530BFDE903977D42B146D539BF1BDE4E9C92");
				return new X9ECParameters(curve, g, bigInteger, bigInteger2);
			}

			// Token: 0x04004056 RID: 16470
			internal static readonly X9ECParametersHolder Instance = new X962NamedCurves.C2tnb239v3Holder();
		}

		// Token: 0x02000DCE RID: 3534
		internal class C2pnb272w1Holder : X9ECParametersHolder
		{
			// Token: 0x06008B49 RID: 35657 RVA: 0x0029D1A0 File Offset: 0x0029D1A0
			private C2pnb272w1Holder()
			{
			}

			// Token: 0x06008B4A RID: 35658 RVA: 0x0029D1A8 File Offset: 0x0029D1A8
			protected override X9ECParameters CreateParameters()
			{
				BigInteger bigInteger = X962NamedCurves.FromHex("0100FAF51354E0E39E4892DF6E319C72C8161603FA45AA7B998A167B8F1E629521");
				BigInteger bigInteger2 = BigInteger.ValueOf(65286L);
				ECCurve curve = X962NamedCurves.ConfigureCurve(new F2mCurve(272, 1, 3, 56, X962NamedCurves.FromHex("91A091F03B5FBA4AB2CCF49C4EDD220FB028712D42BE752B2C40094DBACDB586FB20"), X962NamedCurves.FromHex("7167EFC92BB2E3CE7C8AAAFF34E12A9C557003D7C73A6FAF003F99F6CC8482E540F7"), bigInteger, bigInteger2));
				X9ECPoint g = X962NamedCurves.ConfigureBasepoint(curve, "026108BABB2CEEBCF787058A056CBE0CFE622D7723A289E08A07AE13EF0D10D171DD8D");
				return new X9ECParameters(curve, g, bigInteger, bigInteger2);
			}

			// Token: 0x04004057 RID: 16471
			internal static readonly X9ECParametersHolder Instance = new X962NamedCurves.C2pnb272w1Holder();
		}

		// Token: 0x02000DCF RID: 3535
		internal class C2pnb304w1Holder : X9ECParametersHolder
		{
			// Token: 0x06008B4C RID: 35660 RVA: 0x0029D21C File Offset: 0x0029D21C
			private C2pnb304w1Holder()
			{
			}

			// Token: 0x06008B4D RID: 35661 RVA: 0x0029D224 File Offset: 0x0029D224
			protected override X9ECParameters CreateParameters()
			{
				BigInteger bigInteger = X962NamedCurves.FromHex("0101D556572AABAC800101D556572AABAC8001022D5C91DD173F8FB561DA6899164443051D");
				BigInteger bigInteger2 = BigInteger.ValueOf(65070L);
				ECCurve curve = X962NamedCurves.ConfigureCurve(new F2mCurve(304, 1, 2, 11, X962NamedCurves.FromHex("FD0D693149A118F651E6DCE6802085377E5F882D1B510B44160074C1288078365A0396C8E681"), X962NamedCurves.FromHex("BDDB97E555A50A908E43B01C798EA5DAA6788F1EA2794EFCF57166B8C14039601E55827340BE"), bigInteger, bigInteger2));
				X9ECPoint g = X962NamedCurves.ConfigureBasepoint(curve, "02197B07845E9BE2D96ADB0F5F3C7F2CFFBD7A3EB8B6FEC35C7FD67F26DDF6285A644F740A2614");
				return new X9ECParameters(curve, g, bigInteger, bigInteger2);
			}

			// Token: 0x04004058 RID: 16472
			internal static readonly X9ECParametersHolder Instance = new X962NamedCurves.C2pnb304w1Holder();
		}

		// Token: 0x02000DD0 RID: 3536
		internal class C2tnb359v1Holder : X9ECParametersHolder
		{
			// Token: 0x06008B4F RID: 35663 RVA: 0x0029D298 File Offset: 0x0029D298
			private C2tnb359v1Holder()
			{
			}

			// Token: 0x06008B50 RID: 35664 RVA: 0x0029D2A0 File Offset: 0x0029D2A0
			protected override X9ECParameters CreateParameters()
			{
				BigInteger bigInteger = X962NamedCurves.FromHex("01AF286BCA1AF286BCA1AF286BCA1AF286BCA1AF286BC9FB8F6B85C556892C20A7EB964FE7719E74F490758D3B");
				BigInteger bigInteger2 = BigInteger.ValueOf(76L);
				ECCurve curve = X962NamedCurves.ConfigureCurve(new F2mCurve(359, 68, X962NamedCurves.FromHex("5667676A654B20754F356EA92017D946567C46675556F19556A04616B567D223A5E05656FB549016A96656A557"), X962NamedCurves.FromHex("2472E2D0197C49363F1FE7F5B6DB075D52B6947D135D8CA445805D39BC345626089687742B6329E70680231988"), bigInteger, bigInteger2));
				X9ECPoint g = X962NamedCurves.ConfigureBasepoint(curve, "033C258EF3047767E7EDE0F1FDAA79DAEE3841366A132E163ACED4ED2401DF9C6BDCDE98E8E707C07A2239B1B097");
				return new X9ECParameters(curve, g, bigInteger, bigInteger2);
			}

			// Token: 0x04004059 RID: 16473
			internal static readonly X9ECParametersHolder Instance = new X962NamedCurves.C2tnb359v1Holder();
		}

		// Token: 0x02000DD1 RID: 3537
		internal class C2pnb368w1Holder : X9ECParametersHolder
		{
			// Token: 0x06008B52 RID: 35666 RVA: 0x0029D310 File Offset: 0x0029D310
			private C2pnb368w1Holder()
			{
			}

			// Token: 0x06008B53 RID: 35667 RVA: 0x0029D318 File Offset: 0x0029D318
			protected override X9ECParameters CreateParameters()
			{
				BigInteger bigInteger = X962NamedCurves.FromHex("010090512DA9AF72B08349D98A5DD4C7B0532ECA51CE03E2D10F3B7AC579BD87E909AE40A6F131E9CFCE5BD967");
				BigInteger bigInteger2 = BigInteger.ValueOf(65392L);
				ECCurve curve = X962NamedCurves.ConfigureCurve(new F2mCurve(368, 1, 2, 85, X962NamedCurves.FromHex("E0D2EE25095206F5E2A4F9ED229F1F256E79A0E2B455970D8D0D865BD94778C576D62F0AB7519CCD2A1A906AE30D"), X962NamedCurves.FromHex("FC1217D4320A90452C760A58EDCD30C8DD069B3C34453837A34ED50CB54917E1C2112D84D164F444F8F74786046A"), bigInteger, bigInteger2));
				X9ECPoint g = X962NamedCurves.ConfigureBasepoint(curve, "021085E2755381DCCCE3C1557AFA10C2F0C0C2825646C5B34A394CBCFA8BC16B22E7E789E927BE216F02E1FB136A5F");
				return new X9ECParameters(curve, g, bigInteger, bigInteger2);
			}

			// Token: 0x0400405A RID: 16474
			internal static readonly X9ECParametersHolder Instance = new X962NamedCurves.C2pnb368w1Holder();
		}

		// Token: 0x02000DD2 RID: 3538
		internal class C2tnb431r1Holder : X9ECParametersHolder
		{
			// Token: 0x06008B55 RID: 35669 RVA: 0x0029D38C File Offset: 0x0029D38C
			private C2tnb431r1Holder()
			{
			}

			// Token: 0x06008B56 RID: 35670 RVA: 0x0029D394 File Offset: 0x0029D394
			protected override X9ECParameters CreateParameters()
			{
				BigInteger bigInteger = X962NamedCurves.FromHex("0340340340340340340340340340340340340340340340340340340323C313FAB50589703B5EC68D3587FEC60D161CC149C1AD4A91");
				BigInteger bigInteger2 = BigInteger.ValueOf(10080L);
				ECCurve curve = X962NamedCurves.ConfigureCurve(new F2mCurve(431, 120, X962NamedCurves.FromHex("1A827EF00DD6FC0E234CAF046C6A5D8A85395B236CC4AD2CF32A0CADBDC9DDF620B0EB9906D0957F6C6FEACD615468DF104DE296CD8F"), X962NamedCurves.FromHex("10D9B4A3D9047D8B154359ABFB1B7F5485B04CEB868237DDC9DEDA982A679A5A919B626D4E50A8DD731B107A9962381FB5D807BF2618"), bigInteger, bigInteger2));
				X9ECPoint g = X962NamedCurves.ConfigureBasepoint(curve, "02120FC05D3C67A99DE161D2F4092622FECA701BE4F50F4758714E8A87BBF2A658EF8C21E7C5EFE965361F6C2999C0C247B0DBD70CE6B7");
				return new X9ECParameters(curve, g, bigInteger, bigInteger2);
			}

			// Token: 0x0400405B RID: 16475
			internal static readonly X9ECParametersHolder Instance = new X962NamedCurves.C2tnb431r1Holder();
		}
	}
}
