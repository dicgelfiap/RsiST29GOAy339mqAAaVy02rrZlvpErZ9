using System;
using System.Collections;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.GM;
using Org.BouncyCastle.Asn1.Sec;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Math.EC.Custom.Djb;
using Org.BouncyCastle.Math.EC.Custom.GM;
using Org.BouncyCastle.Math.EC.Custom.Sec;
using Org.BouncyCastle.Math.EC.Endo;
using Org.BouncyCastle.Math.EC.Multiplier;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Crypto.EC
{
	// Token: 0x02000370 RID: 880
	public sealed class CustomNamedCurves
	{
		// Token: 0x06001B3D RID: 6973 RVA: 0x000939D8 File Offset: 0x000939D8
		private CustomNamedCurves()
		{
		}

		// Token: 0x06001B3E RID: 6974 RVA: 0x000939E0 File Offset: 0x000939E0
		private static X9ECPoint ConfigureBasepoint(ECCurve curve, string encoding)
		{
			X9ECPoint x9ECPoint = new X9ECPoint(curve, Hex.DecodeStrict(encoding));
			WNafUtilities.ConfigureBasepoint(x9ECPoint.Point);
			return x9ECPoint;
		}

		// Token: 0x06001B3F RID: 6975 RVA: 0x00093A0C File Offset: 0x00093A0C
		private static ECCurve ConfigureCurve(ECCurve curve)
		{
			return curve;
		}

		// Token: 0x06001B40 RID: 6976 RVA: 0x00093A10 File Offset: 0x00093A10
		private static ECCurve ConfigureCurveGlv(ECCurve c, GlvTypeBParameters p)
		{
			return c.Configure().SetEndomorphism(new GlvTypeBEndomorphism(c, p)).Create();
		}

		// Token: 0x06001B41 RID: 6977 RVA: 0x00093A2C File Offset: 0x00093A2C
		private static BigInteger FromHex(string hex)
		{
			return new BigInteger(1, Hex.DecodeStrict(hex));
		}

		// Token: 0x06001B42 RID: 6978 RVA: 0x00093A3C File Offset: 0x00093A3C
		private static void DefineCurve(string name, X9ECParametersHolder holder)
		{
			CustomNamedCurves.names.Add(name);
			name = Platform.ToUpperInvariant(name);
			CustomNamedCurves.nameToCurve.Add(name, holder);
		}

		// Token: 0x06001B43 RID: 6979 RVA: 0x00093A60 File Offset: 0x00093A60
		private static void DefineCurveWithOid(string name, DerObjectIdentifier oid, X9ECParametersHolder holder)
		{
			CustomNamedCurves.names.Add(name);
			CustomNamedCurves.oidToName.Add(oid, name);
			CustomNamedCurves.oidToCurve.Add(oid, holder);
			name = Platform.ToUpperInvariant(name);
			CustomNamedCurves.nameToOid.Add(name, oid);
			CustomNamedCurves.nameToCurve.Add(name, holder);
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x00093AB8 File Offset: 0x00093AB8
		private static void DefineCurveAlias(string name, DerObjectIdentifier oid)
		{
			object obj = CustomNamedCurves.oidToCurve[oid];
			if (obj == null)
			{
				throw new InvalidOperationException();
			}
			name = Platform.ToUpperInvariant(name);
			CustomNamedCurves.nameToOid.Add(name, oid);
			CustomNamedCurves.nameToCurve.Add(name, obj);
		}

		// Token: 0x06001B45 RID: 6981 RVA: 0x00093B04 File Offset: 0x00093B04
		static CustomNamedCurves()
		{
			CustomNamedCurves.DefineCurve("curve25519", CustomNamedCurves.Curve25519Holder.Instance);
			CustomNamedCurves.DefineCurveWithOid("secp128r1", SecObjectIdentifiers.SecP128r1, CustomNamedCurves.SecP128R1Holder.Instance);
			CustomNamedCurves.DefineCurveWithOid("secp160k1", SecObjectIdentifiers.SecP160k1, CustomNamedCurves.SecP160K1Holder.Instance);
			CustomNamedCurves.DefineCurveWithOid("secp160r1", SecObjectIdentifiers.SecP160r1, CustomNamedCurves.SecP160R1Holder.Instance);
			CustomNamedCurves.DefineCurveWithOid("secp160r2", SecObjectIdentifiers.SecP160r2, CustomNamedCurves.SecP160R2Holder.Instance);
			CustomNamedCurves.DefineCurveWithOid("secp192k1", SecObjectIdentifiers.SecP192k1, CustomNamedCurves.SecP192K1Holder.Instance);
			CustomNamedCurves.DefineCurveWithOid("secp192r1", SecObjectIdentifiers.SecP192r1, CustomNamedCurves.SecP192R1Holder.Instance);
			CustomNamedCurves.DefineCurveWithOid("secp224k1", SecObjectIdentifiers.SecP224k1, CustomNamedCurves.SecP224K1Holder.Instance);
			CustomNamedCurves.DefineCurveWithOid("secp224r1", SecObjectIdentifiers.SecP224r1, CustomNamedCurves.SecP224R1Holder.Instance);
			CustomNamedCurves.DefineCurveWithOid("secp256k1", SecObjectIdentifiers.SecP256k1, CustomNamedCurves.SecP256K1Holder.Instance);
			CustomNamedCurves.DefineCurveWithOid("secp256r1", SecObjectIdentifiers.SecP256r1, CustomNamedCurves.SecP256R1Holder.Instance);
			CustomNamedCurves.DefineCurveWithOid("secp384r1", SecObjectIdentifiers.SecP384r1, CustomNamedCurves.SecP384R1Holder.Instance);
			CustomNamedCurves.DefineCurveWithOid("secp521r1", SecObjectIdentifiers.SecP521r1, CustomNamedCurves.SecP521R1Holder.Instance);
			CustomNamedCurves.DefineCurveWithOid("sect113r1", SecObjectIdentifiers.SecT113r1, CustomNamedCurves.SecT113R1Holder.Instance);
			CustomNamedCurves.DefineCurveWithOid("sect113r2", SecObjectIdentifiers.SecT113r2, CustomNamedCurves.SecT113R2Holder.Instance);
			CustomNamedCurves.DefineCurveWithOid("sect131r1", SecObjectIdentifiers.SecT131r1, CustomNamedCurves.SecT131R1Holder.Instance);
			CustomNamedCurves.DefineCurveWithOid("sect131r2", SecObjectIdentifiers.SecT131r2, CustomNamedCurves.SecT131R2Holder.Instance);
			CustomNamedCurves.DefineCurveWithOid("sect163k1", SecObjectIdentifiers.SecT163k1, CustomNamedCurves.SecT163K1Holder.Instance);
			CustomNamedCurves.DefineCurveWithOid("sect163r1", SecObjectIdentifiers.SecT163r1, CustomNamedCurves.SecT163R1Holder.Instance);
			CustomNamedCurves.DefineCurveWithOid("sect163r2", SecObjectIdentifiers.SecT163r2, CustomNamedCurves.SecT163R2Holder.Instance);
			CustomNamedCurves.DefineCurveWithOid("sect193r1", SecObjectIdentifiers.SecT193r1, CustomNamedCurves.SecT193R1Holder.Instance);
			CustomNamedCurves.DefineCurveWithOid("sect193r2", SecObjectIdentifiers.SecT193r2, CustomNamedCurves.SecT193R2Holder.Instance);
			CustomNamedCurves.DefineCurveWithOid("sect233k1", SecObjectIdentifiers.SecT233k1, CustomNamedCurves.SecT233K1Holder.Instance);
			CustomNamedCurves.DefineCurveWithOid("sect233r1", SecObjectIdentifiers.SecT233r1, CustomNamedCurves.SecT233R1Holder.Instance);
			CustomNamedCurves.DefineCurveWithOid("sect239k1", SecObjectIdentifiers.SecT239k1, CustomNamedCurves.SecT239K1Holder.Instance);
			CustomNamedCurves.DefineCurveWithOid("sect283k1", SecObjectIdentifiers.SecT283k1, CustomNamedCurves.SecT283K1Holder.Instance);
			CustomNamedCurves.DefineCurveWithOid("sect283r1", SecObjectIdentifiers.SecT283r1, CustomNamedCurves.SecT283R1Holder.Instance);
			CustomNamedCurves.DefineCurveWithOid("sect409k1", SecObjectIdentifiers.SecT409k1, CustomNamedCurves.SecT409K1Holder.Instance);
			CustomNamedCurves.DefineCurveWithOid("sect409r1", SecObjectIdentifiers.SecT409r1, CustomNamedCurves.SecT409R1Holder.Instance);
			CustomNamedCurves.DefineCurveWithOid("sect571k1", SecObjectIdentifiers.SecT571k1, CustomNamedCurves.SecT571K1Holder.Instance);
			CustomNamedCurves.DefineCurveWithOid("sect571r1", SecObjectIdentifiers.SecT571r1, CustomNamedCurves.SecT571R1Holder.Instance);
			CustomNamedCurves.DefineCurveWithOid("sm2p256v1", GMObjectIdentifiers.sm2p256v1, CustomNamedCurves.SM2P256V1Holder.Instance);
			CustomNamedCurves.DefineCurveAlias("B-163", SecObjectIdentifiers.SecT163r2);
			CustomNamedCurves.DefineCurveAlias("B-233", SecObjectIdentifiers.SecT233r1);
			CustomNamedCurves.DefineCurveAlias("B-283", SecObjectIdentifiers.SecT283r1);
			CustomNamedCurves.DefineCurveAlias("B-409", SecObjectIdentifiers.SecT409r1);
			CustomNamedCurves.DefineCurveAlias("B-571", SecObjectIdentifiers.SecT571r1);
			CustomNamedCurves.DefineCurveAlias("K-163", SecObjectIdentifiers.SecT163k1);
			CustomNamedCurves.DefineCurveAlias("K-233", SecObjectIdentifiers.SecT233k1);
			CustomNamedCurves.DefineCurveAlias("K-283", SecObjectIdentifiers.SecT283k1);
			CustomNamedCurves.DefineCurveAlias("K-409", SecObjectIdentifiers.SecT409k1);
			CustomNamedCurves.DefineCurveAlias("K-571", SecObjectIdentifiers.SecT571k1);
			CustomNamedCurves.DefineCurveAlias("P-192", SecObjectIdentifiers.SecP192r1);
			CustomNamedCurves.DefineCurveAlias("P-224", SecObjectIdentifiers.SecP224r1);
			CustomNamedCurves.DefineCurveAlias("P-256", SecObjectIdentifiers.SecP256r1);
			CustomNamedCurves.DefineCurveAlias("P-384", SecObjectIdentifiers.SecP384r1);
			CustomNamedCurves.DefineCurveAlias("P-521", SecObjectIdentifiers.SecP521r1);
		}

		// Token: 0x06001B46 RID: 6982 RVA: 0x00093EA4 File Offset: 0x00093EA4
		public static X9ECParameters GetByName(string name)
		{
			X9ECParametersHolder x9ECParametersHolder = (X9ECParametersHolder)CustomNamedCurves.nameToCurve[Platform.ToUpperInvariant(name)];
			if (x9ECParametersHolder != null)
			{
				return x9ECParametersHolder.Parameters;
			}
			return null;
		}

		// Token: 0x06001B47 RID: 6983 RVA: 0x00093EDC File Offset: 0x00093EDC
		public static X9ECParameters GetByOid(DerObjectIdentifier oid)
		{
			X9ECParametersHolder x9ECParametersHolder = (X9ECParametersHolder)CustomNamedCurves.oidToCurve[oid];
			if (x9ECParametersHolder != null)
			{
				return x9ECParametersHolder.Parameters;
			}
			return null;
		}

		// Token: 0x06001B48 RID: 6984 RVA: 0x00093F0C File Offset: 0x00093F0C
		public static DerObjectIdentifier GetOid(string name)
		{
			return (DerObjectIdentifier)CustomNamedCurves.nameToOid[Platform.ToUpperInvariant(name)];
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x00093F24 File Offset: 0x00093F24
		public static string GetName(DerObjectIdentifier oid)
		{
			return (string)CustomNamedCurves.oidToName[oid];
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06001B4A RID: 6986 RVA: 0x00093F38 File Offset: 0x00093F38
		public static IEnumerable Names
		{
			get
			{
				return new EnumerableProxy(CustomNamedCurves.names);
			}
		}

		// Token: 0x04001218 RID: 4632
		private static readonly IDictionary nameToCurve = Platform.CreateHashtable();

		// Token: 0x04001219 RID: 4633
		private static readonly IDictionary nameToOid = Platform.CreateHashtable();

		// Token: 0x0400121A RID: 4634
		private static readonly IDictionary oidToCurve = Platform.CreateHashtable();

		// Token: 0x0400121B RID: 4635
		private static readonly IDictionary oidToName = Platform.CreateHashtable();

		// Token: 0x0400121C RID: 4636
		private static readonly IList names = Platform.CreateArrayList();

		// Token: 0x02000DE8 RID: 3560
		internal class Curve25519Holder : X9ECParametersHolder
		{
			// Token: 0x06008BA9 RID: 35753 RVA: 0x0029ED64 File Offset: 0x0029ED64
			private Curve25519Holder()
			{
			}

			// Token: 0x06008BAA RID: 35754 RVA: 0x0029ED6C File Offset: 0x0029ED6C
			protected override X9ECParameters CreateParameters()
			{
				byte[] seed = null;
				ECCurve eccurve = CustomNamedCurves.ConfigureCurve(new Curve25519());
				X9ECPoint g = CustomNamedCurves.ConfigureBasepoint(eccurve, "042AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAD245A20AE19A1B8A086B4E01EDD2C7748D14C923D4D7E6D7C61B229E9C5A27ECED3D9");
				return new X9ECParameters(eccurve, g, eccurve.Order, eccurve.Cofactor, seed);
			}

			// Token: 0x040040A5 RID: 16549
			internal static readonly X9ECParametersHolder Instance = new CustomNamedCurves.Curve25519Holder();
		}

		// Token: 0x02000DE9 RID: 3561
		internal class SecP128R1Holder : X9ECParametersHolder
		{
			// Token: 0x06008BAC RID: 35756 RVA: 0x0029EDB8 File Offset: 0x0029EDB8
			private SecP128R1Holder()
			{
			}

			// Token: 0x06008BAD RID: 35757 RVA: 0x0029EDC0 File Offset: 0x0029EDC0
			protected override X9ECParameters CreateParameters()
			{
				byte[] seed = Hex.DecodeStrict("000E0D4D696E6768756151750CC03A4473D03679");
				ECCurve eccurve = CustomNamedCurves.ConfigureCurve(new SecP128R1Curve());
				X9ECPoint g = CustomNamedCurves.ConfigureBasepoint(eccurve, "04161FF7528B899B2D0C28607CA52C5B86CF5AC8395BAFEB13C02DA292DDED7A83");
				return new X9ECParameters(eccurve, g, eccurve.Order, eccurve.Cofactor, seed);
			}

			// Token: 0x040040A6 RID: 16550
			internal static readonly X9ECParametersHolder Instance = new CustomNamedCurves.SecP128R1Holder();
		}

		// Token: 0x02000DEA RID: 3562
		internal class SecP160K1Holder : X9ECParametersHolder
		{
			// Token: 0x06008BAF RID: 35759 RVA: 0x0029EE14 File Offset: 0x0029EE14
			private SecP160K1Holder()
			{
			}

			// Token: 0x06008BB0 RID: 35760 RVA: 0x0029EE1C File Offset: 0x0029EE1C
			protected override X9ECParameters CreateParameters()
			{
				byte[] seed = null;
				GlvTypeBParameters p = new GlvTypeBParameters(new BigInteger("9ba48cba5ebcb9b6bd33b92830b2a2e0e192f10a", 16), new BigInteger("c39c6c3b3a36d7701b9c71a1f5804ae5d0003f4", 16), new ScalarSplitParameters(new BigInteger[]
				{
					new BigInteger("9162fbe73984472a0a9e", 16),
					new BigInteger("-96341f1138933bc2f505", 16)
				}, new BigInteger[]
				{
					new BigInteger("127971af8721782ecffa3", 16),
					new BigInteger("9162fbe73984472a0a9e", 16)
				}, new BigInteger("9162fbe73984472a0a9d0590", 16), new BigInteger("96341f1138933bc2f503fd44", 16), 176));
				ECCurve eccurve = CustomNamedCurves.ConfigureCurveGlv(new SecP160K1Curve(), p);
				X9ECPoint g = CustomNamedCurves.ConfigureBasepoint(eccurve, "043B4C382CE37AA192A4019E763036F4F5DD4D7EBB938CF935318FDCED6BC28286531733C3F03C4FEE");
				return new X9ECParameters(eccurve, g, eccurve.Order, eccurve.Cofactor, seed);
			}

			// Token: 0x040040A7 RID: 16551
			internal static readonly X9ECParametersHolder Instance = new CustomNamedCurves.SecP160K1Holder();
		}

		// Token: 0x02000DEB RID: 3563
		internal class SecP160R1Holder : X9ECParametersHolder
		{
			// Token: 0x06008BB2 RID: 35762 RVA: 0x0029EF08 File Offset: 0x0029EF08
			private SecP160R1Holder()
			{
			}

			// Token: 0x06008BB3 RID: 35763 RVA: 0x0029EF10 File Offset: 0x0029EF10
			protected override X9ECParameters CreateParameters()
			{
				byte[] seed = Hex.DecodeStrict("1053CDE42C14D696E67687561517533BF3F83345");
				ECCurve eccurve = CustomNamedCurves.ConfigureCurve(new SecP160R1Curve());
				X9ECPoint g = CustomNamedCurves.ConfigureBasepoint(eccurve, "044A96B5688EF573284664698968C38BB913CBFC8223A628553168947D59DCC912042351377AC5FB32");
				return new X9ECParameters(eccurve, g, eccurve.Order, eccurve.Cofactor, seed);
			}

			// Token: 0x040040A8 RID: 16552
			internal static readonly X9ECParametersHolder Instance = new CustomNamedCurves.SecP160R1Holder();
		}

		// Token: 0x02000DEC RID: 3564
		internal class SecP160R2Holder : X9ECParametersHolder
		{
			// Token: 0x06008BB5 RID: 35765 RVA: 0x0029EF64 File Offset: 0x0029EF64
			private SecP160R2Holder()
			{
			}

			// Token: 0x06008BB6 RID: 35766 RVA: 0x0029EF6C File Offset: 0x0029EF6C
			protected override X9ECParameters CreateParameters()
			{
				byte[] seed = Hex.DecodeStrict("B99B99B099B323E02709A4D696E6768756151751");
				ECCurve eccurve = CustomNamedCurves.ConfigureCurve(new SecP160R2Curve());
				X9ECPoint g = CustomNamedCurves.ConfigureBasepoint(eccurve, "0452DCB034293A117E1F4FF11B30F7199D3144CE6DFEAFFEF2E331F296E071FA0DF9982CFEA7D43F2E");
				return new X9ECParameters(eccurve, g, eccurve.Order, eccurve.Cofactor, seed);
			}

			// Token: 0x040040A9 RID: 16553
			internal static readonly X9ECParametersHolder Instance = new CustomNamedCurves.SecP160R2Holder();
		}

		// Token: 0x02000DED RID: 3565
		internal class SecP192K1Holder : X9ECParametersHolder
		{
			// Token: 0x06008BB8 RID: 35768 RVA: 0x0029EFC0 File Offset: 0x0029EFC0
			private SecP192K1Holder()
			{
			}

			// Token: 0x06008BB9 RID: 35769 RVA: 0x0029EFC8 File Offset: 0x0029EFC8
			protected override X9ECParameters CreateParameters()
			{
				byte[] seed = null;
				GlvTypeBParameters p = new GlvTypeBParameters(new BigInteger("bb85691939b869c1d087f601554b96b80cb4f55b35f433c2", 16), new BigInteger("3d84f26c12238d7b4f3d516613c1759033b1a5800175d0b1", 16), new ScalarSplitParameters(new BigInteger[]
				{
					new BigInteger("71169be7330b3038edb025f1", 16),
					new BigInteger("-b3fb3400dec5c4adceb8655c", 16)
				}, new BigInteger[]
				{
					new BigInteger("12511cfe811d0f4e6bc688b4d", 16),
					new BigInteger("71169be7330b3038edb025f1", 16)
				}, new BigInteger("71169be7330b3038edb025f1d0f9", 16), new BigInteger("b3fb3400dec5c4adceb8655d4c94", 16), 208));
				ECCurve eccurve = CustomNamedCurves.ConfigureCurveGlv(new SecP192K1Curve(), p);
				X9ECPoint g = CustomNamedCurves.ConfigureBasepoint(eccurve, "04DB4FF10EC057E9AE26B07D0280B7F4341DA5D1B1EAE06C7D9B2F2F6D9C5628A7844163D015BE86344082AA88D95E2F9D");
				return new X9ECParameters(eccurve, g, eccurve.Order, eccurve.Cofactor, seed);
			}

			// Token: 0x040040AA RID: 16554
			internal static readonly X9ECParametersHolder Instance = new CustomNamedCurves.SecP192K1Holder();
		}

		// Token: 0x02000DEE RID: 3566
		internal class SecP192R1Holder : X9ECParametersHolder
		{
			// Token: 0x06008BBB RID: 35771 RVA: 0x0029F0B4 File Offset: 0x0029F0B4
			private SecP192R1Holder()
			{
			}

			// Token: 0x06008BBC RID: 35772 RVA: 0x0029F0BC File Offset: 0x0029F0BC
			protected override X9ECParameters CreateParameters()
			{
				byte[] seed = Hex.DecodeStrict("3045AE6FC8422F64ED579528D38120EAE12196D5");
				ECCurve eccurve = CustomNamedCurves.ConfigureCurve(new SecP192R1Curve());
				X9ECPoint g = CustomNamedCurves.ConfigureBasepoint(eccurve, "04188DA80EB03090F67CBF20EB43A18800F4FF0AFD82FF101207192B95FFC8DA78631011ED6B24CDD573F977A11E794811");
				return new X9ECParameters(eccurve, g, eccurve.Order, eccurve.Cofactor, seed);
			}

			// Token: 0x040040AB RID: 16555
			internal static readonly X9ECParametersHolder Instance = new CustomNamedCurves.SecP192R1Holder();
		}

		// Token: 0x02000DEF RID: 3567
		internal class SecP224K1Holder : X9ECParametersHolder
		{
			// Token: 0x06008BBE RID: 35774 RVA: 0x0029F110 File Offset: 0x0029F110
			private SecP224K1Holder()
			{
			}

			// Token: 0x06008BBF RID: 35775 RVA: 0x0029F118 File Offset: 0x0029F118
			protected override X9ECParameters CreateParameters()
			{
				byte[] seed = null;
				GlvTypeBParameters p = new GlvTypeBParameters(new BigInteger("fe0e87005b4e83761908c5131d552a850b3f58b749c37cf5b84d6768", 16), new BigInteger("60dcd2104c4cbc0be6eeefc2bdd610739ec34e317f9b33046c9e4788", 16), new ScalarSplitParameters(new BigInteger[]
				{
					new BigInteger("6b8cf07d4ca75c88957d9d670591", 16),
					new BigInteger("-b8adf1378a6eb73409fa6c9c637d", 16)
				}, new BigInteger[]
				{
					new BigInteger("1243ae1b4d71613bc9f780a03690e", 16),
					new BigInteger("6b8cf07d4ca75c88957d9d670591", 16)
				}, new BigInteger("6b8cf07d4ca75c88957d9d67059037a4", 16), new BigInteger("b8adf1378a6eb73409fa6c9c637ba7f5", 16), 240));
				ECCurve eccurve = CustomNamedCurves.ConfigureCurveGlv(new SecP224K1Curve(), p);
				X9ECPoint g = CustomNamedCurves.ConfigureBasepoint(eccurve, "04A1455B334DF099DF30FC28A169A467E9E47075A90F7E650EB6B7A45C7E089FED7FBA344282CAFBD6F7E319F7C0B0BD59E2CA4BDB556D61A5");
				return new X9ECParameters(eccurve, g, eccurve.Order, eccurve.Cofactor, seed);
			}

			// Token: 0x040040AC RID: 16556
			internal static readonly X9ECParametersHolder Instance = new CustomNamedCurves.SecP224K1Holder();
		}

		// Token: 0x02000DF0 RID: 3568
		internal class SecP224R1Holder : X9ECParametersHolder
		{
			// Token: 0x06008BC1 RID: 35777 RVA: 0x0029F204 File Offset: 0x0029F204
			private SecP224R1Holder()
			{
			}

			// Token: 0x06008BC2 RID: 35778 RVA: 0x0029F20C File Offset: 0x0029F20C
			protected override X9ECParameters CreateParameters()
			{
				byte[] seed = Hex.DecodeStrict("BD71344799D5C7FCDC45B59FA3B9AB8F6A948BC5");
				ECCurve eccurve = CustomNamedCurves.ConfigureCurve(new SecP224R1Curve());
				X9ECPoint g = CustomNamedCurves.ConfigureBasepoint(eccurve, "04B70E0CBD6BB4BF7F321390B94A03C1D356C21122343280D6115C1D21BD376388B5F723FB4C22DFE6CD4375A05A07476444D5819985007E34");
				return new X9ECParameters(eccurve, g, eccurve.Order, eccurve.Cofactor, seed);
			}

			// Token: 0x040040AD RID: 16557
			internal static readonly X9ECParametersHolder Instance = new CustomNamedCurves.SecP224R1Holder();
		}

		// Token: 0x02000DF1 RID: 3569
		internal class SecP256K1Holder : X9ECParametersHolder
		{
			// Token: 0x06008BC4 RID: 35780 RVA: 0x0029F260 File Offset: 0x0029F260
			private SecP256K1Holder()
			{
			}

			// Token: 0x06008BC5 RID: 35781 RVA: 0x0029F268 File Offset: 0x0029F268
			protected override X9ECParameters CreateParameters()
			{
				byte[] seed = null;
				GlvTypeBParameters p = new GlvTypeBParameters(new BigInteger("7ae96a2b657c07106e64479eac3434e99cf0497512f58995c1396c28719501ee", 16), new BigInteger("5363ad4cc05c30e0a5261c028812645a122e22ea20816678df02967c1b23bd72", 16), new ScalarSplitParameters(new BigInteger[]
				{
					new BigInteger("3086d221a7d46bcde86c90e49284eb15", 16),
					new BigInteger("-e4437ed6010e88286f547fa90abfe4c3", 16)
				}, new BigInteger[]
				{
					new BigInteger("114ca50f7a8e2f3f657c1108d9d44cfd8", 16),
					new BigInteger("3086d221a7d46bcde86c90e49284eb15", 16)
				}, new BigInteger("3086d221a7d46bcde86c90e49284eb153dab", 16), new BigInteger("e4437ed6010e88286f547fa90abfe4c42212", 16), 272));
				ECCurve eccurve = CustomNamedCurves.ConfigureCurveGlv(new SecP256K1Curve(), p);
				X9ECPoint g = CustomNamedCurves.ConfigureBasepoint(eccurve, "0479BE667EF9DCBBAC55A06295CE870B07029BFCDB2DCE28D959F2815B16F81798483ADA7726A3C4655DA4FBFC0E1108A8FD17B448A68554199C47D08FFB10D4B8");
				return new X9ECParameters(eccurve, g, eccurve.Order, eccurve.Cofactor, seed);
			}

			// Token: 0x040040AE RID: 16558
			internal static readonly X9ECParametersHolder Instance = new CustomNamedCurves.SecP256K1Holder();
		}

		// Token: 0x02000DF2 RID: 3570
		internal class SecP256R1Holder : X9ECParametersHolder
		{
			// Token: 0x06008BC7 RID: 35783 RVA: 0x0029F354 File Offset: 0x0029F354
			private SecP256R1Holder()
			{
			}

			// Token: 0x06008BC8 RID: 35784 RVA: 0x0029F35C File Offset: 0x0029F35C
			protected override X9ECParameters CreateParameters()
			{
				byte[] seed = Hex.DecodeStrict("C49D360886E704936A6678E1139D26B7819F7E90");
				ECCurve eccurve = CustomNamedCurves.ConfigureCurve(new SecP256R1Curve());
				X9ECPoint g = CustomNamedCurves.ConfigureBasepoint(eccurve, "046B17D1F2E12C4247F8BCE6E563A440F277037D812DEB33A0F4A13945D898C2964FE342E2FE1A7F9B8EE7EB4A7C0F9E162BCE33576B315ECECBB6406837BF51F5");
				return new X9ECParameters(eccurve, g, eccurve.Order, eccurve.Cofactor, seed);
			}

			// Token: 0x040040AF RID: 16559
			internal static readonly X9ECParametersHolder Instance = new CustomNamedCurves.SecP256R1Holder();
		}

		// Token: 0x02000DF3 RID: 3571
		internal class SecP384R1Holder : X9ECParametersHolder
		{
			// Token: 0x06008BCA RID: 35786 RVA: 0x0029F3B0 File Offset: 0x0029F3B0
			private SecP384R1Holder()
			{
			}

			// Token: 0x06008BCB RID: 35787 RVA: 0x0029F3B8 File Offset: 0x0029F3B8
			protected override X9ECParameters CreateParameters()
			{
				byte[] seed = Hex.DecodeStrict("A335926AA319A27A1D00896A6773A4827ACDAC73");
				ECCurve eccurve = CustomNamedCurves.ConfigureCurve(new SecP384R1Curve());
				X9ECPoint g = CustomNamedCurves.ConfigureBasepoint(eccurve, "04AA87CA22BE8B05378EB1C71EF320AD746E1D3B628BA79B9859F741E082542A385502F25DBF55296C3A545E3872760AB73617DE4A96262C6F5D9E98BF9292DC29F8F41DBD289A147CE9DA3113B5F0B8C00A60B1CE1D7E819D7A431D7C90EA0E5F");
				return new X9ECParameters(eccurve, g, eccurve.Order, eccurve.Cofactor, seed);
			}

			// Token: 0x040040B0 RID: 16560
			internal static readonly X9ECParametersHolder Instance = new CustomNamedCurves.SecP384R1Holder();
		}

		// Token: 0x02000DF4 RID: 3572
		internal class SecP521R1Holder : X9ECParametersHolder
		{
			// Token: 0x06008BCD RID: 35789 RVA: 0x0029F40C File Offset: 0x0029F40C
			private SecP521R1Holder()
			{
			}

			// Token: 0x06008BCE RID: 35790 RVA: 0x0029F414 File Offset: 0x0029F414
			protected override X9ECParameters CreateParameters()
			{
				byte[] seed = Hex.DecodeStrict("D09E8800291CB85396CC6717393284AAA0DA64BA");
				ECCurve eccurve = CustomNamedCurves.ConfigureCurve(new SecP521R1Curve());
				X9ECPoint g = CustomNamedCurves.ConfigureBasepoint(eccurve, "0400C6858E06B70404E9CD9E3ECB662395B4429C648139053FB521F828AF606B4D3DBAA14B5E77EFE75928FE1DC127A2FFA8DE3348B3C1856A429BF97E7E31C2E5BD66011839296A789A3BC0045C8A5FB42C7D1BD998F54449579B446817AFBD17273E662C97EE72995EF42640C550B9013FAD0761353C7086A272C24088BE94769FD16650");
				return new X9ECParameters(eccurve, g, eccurve.Order, eccurve.Cofactor, seed);
			}

			// Token: 0x040040B1 RID: 16561
			internal static readonly X9ECParametersHolder Instance = new CustomNamedCurves.SecP521R1Holder();
		}

		// Token: 0x02000DF5 RID: 3573
		internal class SecT113R1Holder : X9ECParametersHolder
		{
			// Token: 0x06008BD0 RID: 35792 RVA: 0x0029F468 File Offset: 0x0029F468
			private SecT113R1Holder()
			{
			}

			// Token: 0x06008BD1 RID: 35793 RVA: 0x0029F470 File Offset: 0x0029F470
			protected override X9ECParameters CreateParameters()
			{
				byte[] seed = Hex.DecodeStrict("10E723AB14D696E6768756151756FEBF8FCB49A9");
				ECCurve eccurve = CustomNamedCurves.ConfigureCurve(new SecT113R1Curve());
				X9ECPoint g = CustomNamedCurves.ConfigureBasepoint(eccurve, "04009D73616F35F4AB1407D73562C10F00A52830277958EE84D1315ED31886");
				return new X9ECParameters(eccurve, g, eccurve.Order, eccurve.Cofactor, seed);
			}

			// Token: 0x040040B2 RID: 16562
			internal static readonly X9ECParametersHolder Instance = new CustomNamedCurves.SecT113R1Holder();
		}

		// Token: 0x02000DF6 RID: 3574
		internal class SecT113R2Holder : X9ECParametersHolder
		{
			// Token: 0x06008BD3 RID: 35795 RVA: 0x0029F4C4 File Offset: 0x0029F4C4
			private SecT113R2Holder()
			{
			}

			// Token: 0x06008BD4 RID: 35796 RVA: 0x0029F4CC File Offset: 0x0029F4CC
			protected override X9ECParameters CreateParameters()
			{
				byte[] seed = Hex.DecodeStrict("10C0FB15760860DEF1EEF4D696E676875615175D");
				ECCurve eccurve = CustomNamedCurves.ConfigureCurve(new SecT113R2Curve());
				X9ECPoint g = CustomNamedCurves.ConfigureBasepoint(eccurve, "0401A57A6A7B26CA5EF52FCDB816479700B3ADC94ED1FE674C06E695BABA1D");
				return new X9ECParameters(eccurve, g, eccurve.Order, eccurve.Cofactor, seed);
			}

			// Token: 0x040040B3 RID: 16563
			internal static readonly X9ECParametersHolder Instance = new CustomNamedCurves.SecT113R2Holder();
		}

		// Token: 0x02000DF7 RID: 3575
		internal class SecT131R1Holder : X9ECParametersHolder
		{
			// Token: 0x06008BD6 RID: 35798 RVA: 0x0029F520 File Offset: 0x0029F520
			private SecT131R1Holder()
			{
			}

			// Token: 0x06008BD7 RID: 35799 RVA: 0x0029F528 File Offset: 0x0029F528
			protected override X9ECParameters CreateParameters()
			{
				byte[] seed = Hex.DecodeStrict("4D696E676875615175985BD3ADBADA21B43A97E2");
				ECCurve eccurve = CustomNamedCurves.ConfigureCurve(new SecT131R1Curve());
				X9ECPoint g = CustomNamedCurves.ConfigureBasepoint(eccurve, "040081BAF91FDF9833C40F9C181343638399078C6E7EA38C001F73C8134B1B4EF9E150");
				return new X9ECParameters(eccurve, g, eccurve.Order, eccurve.Cofactor, seed);
			}

			// Token: 0x040040B4 RID: 16564
			internal static readonly X9ECParametersHolder Instance = new CustomNamedCurves.SecT131R1Holder();
		}

		// Token: 0x02000DF8 RID: 3576
		internal class SecT131R2Holder : X9ECParametersHolder
		{
			// Token: 0x06008BD9 RID: 35801 RVA: 0x0029F57C File Offset: 0x0029F57C
			private SecT131R2Holder()
			{
			}

			// Token: 0x06008BDA RID: 35802 RVA: 0x0029F584 File Offset: 0x0029F584
			protected override X9ECParameters CreateParameters()
			{
				byte[] seed = Hex.DecodeStrict("985BD3ADBAD4D696E676875615175A21B43A97E3");
				ECCurve eccurve = CustomNamedCurves.ConfigureCurve(new SecT131R2Curve());
				X9ECPoint g = CustomNamedCurves.ConfigureBasepoint(eccurve, "040356DCD8F2F95031AD652D23951BB366A80648F06D867940A5366D9E265DE9EB240F");
				return new X9ECParameters(eccurve, g, eccurve.Order, eccurve.Cofactor, seed);
			}

			// Token: 0x040040B5 RID: 16565
			internal static readonly X9ECParametersHolder Instance = new CustomNamedCurves.SecT131R2Holder();
		}

		// Token: 0x02000DF9 RID: 3577
		internal class SecT163K1Holder : X9ECParametersHolder
		{
			// Token: 0x06008BDC RID: 35804 RVA: 0x0029F5D8 File Offset: 0x0029F5D8
			private SecT163K1Holder()
			{
			}

			// Token: 0x06008BDD RID: 35805 RVA: 0x0029F5E0 File Offset: 0x0029F5E0
			protected override X9ECParameters CreateParameters()
			{
				byte[] seed = null;
				ECCurve eccurve = CustomNamedCurves.ConfigureCurve(new SecT163K1Curve());
				X9ECPoint g = CustomNamedCurves.ConfigureBasepoint(eccurve, "0402FE13C0537BBC11ACAA07D793DE4E6D5E5C94EEE80289070FB05D38FF58321F2E800536D538CCDAA3D9");
				return new X9ECParameters(eccurve, g, eccurve.Order, eccurve.Cofactor, seed);
			}

			// Token: 0x040040B6 RID: 16566
			internal static readonly X9ECParametersHolder Instance = new CustomNamedCurves.SecT163K1Holder();
		}

		// Token: 0x02000DFA RID: 3578
		internal class SecT163R1Holder : X9ECParametersHolder
		{
			// Token: 0x06008BDF RID: 35807 RVA: 0x0029F62C File Offset: 0x0029F62C
			private SecT163R1Holder()
			{
			}

			// Token: 0x06008BE0 RID: 35808 RVA: 0x0029F634 File Offset: 0x0029F634
			protected override X9ECParameters CreateParameters()
			{
				byte[] seed = Hex.DecodeStrict("24B7B137C8A14D696E6768756151756FD0DA2E5C");
				ECCurve eccurve = CustomNamedCurves.ConfigureCurve(new SecT163R1Curve());
				X9ECPoint g = CustomNamedCurves.ConfigureBasepoint(eccurve, "040369979697AB43897789566789567F787A7876A65400435EDB42EFAFB2989D51FEFCE3C80988F41FF883");
				return new X9ECParameters(eccurve, g, eccurve.Order, eccurve.Cofactor, seed);
			}

			// Token: 0x040040B7 RID: 16567
			internal static readonly X9ECParametersHolder Instance = new CustomNamedCurves.SecT163R1Holder();
		}

		// Token: 0x02000DFB RID: 3579
		internal class SecT163R2Holder : X9ECParametersHolder
		{
			// Token: 0x06008BE2 RID: 35810 RVA: 0x0029F688 File Offset: 0x0029F688
			private SecT163R2Holder()
			{
			}

			// Token: 0x06008BE3 RID: 35811 RVA: 0x0029F690 File Offset: 0x0029F690
			protected override X9ECParameters CreateParameters()
			{
				byte[] seed = Hex.DecodeStrict("85E25BFE5C86226CDB12016F7553F9D0E693A268");
				ECCurve eccurve = CustomNamedCurves.ConfigureCurve(new SecT163R2Curve());
				X9ECPoint g = CustomNamedCurves.ConfigureBasepoint(eccurve, "0403F0EBA16286A2D57EA0991168D4994637E8343E3600D51FBC6C71A0094FA2CDD545B11C5C0C797324F1");
				return new X9ECParameters(eccurve, g, eccurve.Order, eccurve.Cofactor, seed);
			}

			// Token: 0x040040B8 RID: 16568
			internal static readonly X9ECParametersHolder Instance = new CustomNamedCurves.SecT163R2Holder();
		}

		// Token: 0x02000DFC RID: 3580
		internal class SecT193R1Holder : X9ECParametersHolder
		{
			// Token: 0x06008BE5 RID: 35813 RVA: 0x0029F6E4 File Offset: 0x0029F6E4
			private SecT193R1Holder()
			{
			}

			// Token: 0x06008BE6 RID: 35814 RVA: 0x0029F6EC File Offset: 0x0029F6EC
			protected override X9ECParameters CreateParameters()
			{
				byte[] seed = Hex.DecodeStrict("103FAEC74D696E676875615175777FC5B191EF30");
				ECCurve eccurve = CustomNamedCurves.ConfigureCurve(new SecT193R1Curve());
				X9ECPoint g = CustomNamedCurves.ConfigureBasepoint(eccurve, "0401F481BC5F0FF84A74AD6CDF6FDEF4BF6179625372D8C0C5E10025E399F2903712CCF3EA9E3A1AD17FB0B3201B6AF7CE1B05");
				return new X9ECParameters(eccurve, g, eccurve.Order, eccurve.Cofactor, seed);
			}

			// Token: 0x040040B9 RID: 16569
			internal static readonly X9ECParametersHolder Instance = new CustomNamedCurves.SecT193R1Holder();
		}

		// Token: 0x02000DFD RID: 3581
		internal class SecT193R2Holder : X9ECParametersHolder
		{
			// Token: 0x06008BE8 RID: 35816 RVA: 0x0029F740 File Offset: 0x0029F740
			private SecT193R2Holder()
			{
			}

			// Token: 0x06008BE9 RID: 35817 RVA: 0x0029F748 File Offset: 0x0029F748
			protected override X9ECParameters CreateParameters()
			{
				byte[] seed = Hex.DecodeStrict("10B7B4D696E676875615175137C8A16FD0DA2211");
				ECCurve eccurve = CustomNamedCurves.ConfigureCurve(new SecT193R2Curve());
				X9ECPoint g = CustomNamedCurves.ConfigureBasepoint(eccurve, "0400D9B67D192E0367C803F39E1A7E82CA14A651350AAE617E8F01CE94335607C304AC29E7DEFBD9CA01F596F927224CDECF6C");
				return new X9ECParameters(eccurve, g, eccurve.Order, eccurve.Cofactor, seed);
			}

			// Token: 0x040040BA RID: 16570
			internal static readonly X9ECParametersHolder Instance = new CustomNamedCurves.SecT193R2Holder();
		}

		// Token: 0x02000DFE RID: 3582
		internal class SecT233K1Holder : X9ECParametersHolder
		{
			// Token: 0x06008BEB RID: 35819 RVA: 0x0029F79C File Offset: 0x0029F79C
			private SecT233K1Holder()
			{
			}

			// Token: 0x06008BEC RID: 35820 RVA: 0x0029F7A4 File Offset: 0x0029F7A4
			protected override X9ECParameters CreateParameters()
			{
				byte[] seed = null;
				ECCurve eccurve = CustomNamedCurves.ConfigureCurve(new SecT233K1Curve());
				X9ECPoint g = CustomNamedCurves.ConfigureBasepoint(eccurve, "04017232BA853A7E731AF129F22FF4149563A419C26BF50A4C9D6EEFAD612601DB537DECE819B7F70F555A67C427A8CD9BF18AEB9B56E0C11056FAE6A3");
				return new X9ECParameters(eccurve, g, eccurve.Order, eccurve.Cofactor, seed);
			}

			// Token: 0x040040BB RID: 16571
			internal static readonly X9ECParametersHolder Instance = new CustomNamedCurves.SecT233K1Holder();
		}

		// Token: 0x02000DFF RID: 3583
		internal class SecT233R1Holder : X9ECParametersHolder
		{
			// Token: 0x06008BEE RID: 35822 RVA: 0x0029F7F0 File Offset: 0x0029F7F0
			private SecT233R1Holder()
			{
			}

			// Token: 0x06008BEF RID: 35823 RVA: 0x0029F7F8 File Offset: 0x0029F7F8
			protected override X9ECParameters CreateParameters()
			{
				byte[] seed = Hex.DecodeStrict("74D59FF07F6B413D0EA14B344B20A2DB049B50C3");
				ECCurve eccurve = CustomNamedCurves.ConfigureCurve(new SecT233R1Curve());
				X9ECPoint g = CustomNamedCurves.ConfigureBasepoint(eccurve, "0400FAC9DFCBAC8313BB2139F1BB755FEF65BC391F8B36F8F8EB7371FD558B01006A08A41903350678E58528BEBF8A0BEFF867A7CA36716F7E01F81052");
				return new X9ECParameters(eccurve, g, eccurve.Order, eccurve.Cofactor, seed);
			}

			// Token: 0x040040BC RID: 16572
			internal static readonly X9ECParametersHolder Instance = new CustomNamedCurves.SecT233R1Holder();
		}

		// Token: 0x02000E00 RID: 3584
		internal class SecT239K1Holder : X9ECParametersHolder
		{
			// Token: 0x06008BF1 RID: 35825 RVA: 0x0029F84C File Offset: 0x0029F84C
			private SecT239K1Holder()
			{
			}

			// Token: 0x06008BF2 RID: 35826 RVA: 0x0029F854 File Offset: 0x0029F854
			protected override X9ECParameters CreateParameters()
			{
				byte[] seed = null;
				ECCurve eccurve = CustomNamedCurves.ConfigureCurve(new SecT239K1Curve());
				X9ECPoint g = CustomNamedCurves.ConfigureBasepoint(eccurve, "0429A0B6A887A983E9730988A68727A8B2D126C44CC2CC7B2A6555193035DC76310804F12E549BDB011C103089E73510ACB275FC312A5DC6B76553F0CA");
				return new X9ECParameters(eccurve, g, eccurve.Order, eccurve.Cofactor, seed);
			}

			// Token: 0x040040BD RID: 16573
			internal static readonly X9ECParametersHolder Instance = new CustomNamedCurves.SecT239K1Holder();
		}

		// Token: 0x02000E01 RID: 3585
		internal class SecT283K1Holder : X9ECParametersHolder
		{
			// Token: 0x06008BF4 RID: 35828 RVA: 0x0029F8A0 File Offset: 0x0029F8A0
			private SecT283K1Holder()
			{
			}

			// Token: 0x06008BF5 RID: 35829 RVA: 0x0029F8A8 File Offset: 0x0029F8A8
			protected override X9ECParameters CreateParameters()
			{
				byte[] seed = null;
				ECCurve eccurve = CustomNamedCurves.ConfigureCurve(new SecT283K1Curve());
				X9ECPoint g = CustomNamedCurves.ConfigureBasepoint(eccurve, "040503213F78CA44883F1A3B8162F188E553CD265F23C1567A16876913B0C2AC245849283601CCDA380F1C9E318D90F95D07E5426FE87E45C0E8184698E45962364E34116177DD2259");
				return new X9ECParameters(eccurve, g, eccurve.Order, eccurve.Cofactor, seed);
			}

			// Token: 0x040040BE RID: 16574
			internal static readonly X9ECParametersHolder Instance = new CustomNamedCurves.SecT283K1Holder();
		}

		// Token: 0x02000E02 RID: 3586
		internal class SecT283R1Holder : X9ECParametersHolder
		{
			// Token: 0x06008BF7 RID: 35831 RVA: 0x0029F8F4 File Offset: 0x0029F8F4
			private SecT283R1Holder()
			{
			}

			// Token: 0x06008BF8 RID: 35832 RVA: 0x0029F8FC File Offset: 0x0029F8FC
			protected override X9ECParameters CreateParameters()
			{
				byte[] seed = Hex.DecodeStrict("77E2B07370EB0F832A6DD5B62DFC88CD06BB84BE");
				ECCurve eccurve = CustomNamedCurves.ConfigureCurve(new SecT283R1Curve());
				X9ECPoint g = CustomNamedCurves.ConfigureBasepoint(eccurve, "0405F939258DB7DD90E1934F8C70B0DFEC2EED25B8557EAC9C80E2E198F8CDBECD86B1205303676854FE24141CB98FE6D4B20D02B4516FF702350EDDB0826779C813F0DF45BE8112F4");
				return new X9ECParameters(eccurve, g, eccurve.Order, eccurve.Cofactor, seed);
			}

			// Token: 0x040040BF RID: 16575
			internal static readonly X9ECParametersHolder Instance = new CustomNamedCurves.SecT283R1Holder();
		}

		// Token: 0x02000E03 RID: 3587
		internal class SecT409K1Holder : X9ECParametersHolder
		{
			// Token: 0x06008BFA RID: 35834 RVA: 0x0029F950 File Offset: 0x0029F950
			private SecT409K1Holder()
			{
			}

			// Token: 0x06008BFB RID: 35835 RVA: 0x0029F958 File Offset: 0x0029F958
			protected override X9ECParameters CreateParameters()
			{
				byte[] seed = null;
				ECCurve eccurve = CustomNamedCurves.ConfigureCurve(new SecT409K1Curve());
				X9ECPoint g = CustomNamedCurves.ConfigureBasepoint(eccurve, "040060F05F658F49C1AD3AB1890F7184210EFD0987E307C84C27ACCFB8F9F67CC2C460189EB5AAAA62EE222EB1B35540CFE902374601E369050B7C4E42ACBA1DACBF04299C3460782F918EA427E6325165E9EA10E3DA5F6C42E9C55215AA9CA27A5863EC48D8E0286B");
				return new X9ECParameters(eccurve, g, eccurve.Order, eccurve.Cofactor, seed);
			}

			// Token: 0x040040C0 RID: 16576
			internal static readonly X9ECParametersHolder Instance = new CustomNamedCurves.SecT409K1Holder();
		}

		// Token: 0x02000E04 RID: 3588
		internal class SecT409R1Holder : X9ECParametersHolder
		{
			// Token: 0x06008BFD RID: 35837 RVA: 0x0029F9A4 File Offset: 0x0029F9A4
			private SecT409R1Holder()
			{
			}

			// Token: 0x06008BFE RID: 35838 RVA: 0x0029F9AC File Offset: 0x0029F9AC
			protected override X9ECParameters CreateParameters()
			{
				byte[] seed = Hex.DecodeStrict("4099B5A457F9D69F79213D094C4BCD4D4262210B");
				ECCurve eccurve = CustomNamedCurves.ConfigureCurve(new SecT409R1Curve());
				X9ECPoint g = CustomNamedCurves.ConfigureBasepoint(eccurve, "04015D4860D088DDB3496B0C6064756260441CDE4AF1771D4DB01FFE5B34E59703DC255A868A1180515603AEAB60794E54BB7996A70061B1CFAB6BE5F32BBFA78324ED106A7636B9C5A7BD198D0158AA4F5488D08F38514F1FDF4B4F40D2181B3681C364BA0273C706");
				return new X9ECParameters(eccurve, g, eccurve.Order, eccurve.Cofactor, seed);
			}

			// Token: 0x040040C1 RID: 16577
			internal static readonly X9ECParametersHolder Instance = new CustomNamedCurves.SecT409R1Holder();
		}

		// Token: 0x02000E05 RID: 3589
		internal class SecT571K1Holder : X9ECParametersHolder
		{
			// Token: 0x06008C00 RID: 35840 RVA: 0x0029FA00 File Offset: 0x0029FA00
			private SecT571K1Holder()
			{
			}

			// Token: 0x06008C01 RID: 35841 RVA: 0x0029FA08 File Offset: 0x0029FA08
			protected override X9ECParameters CreateParameters()
			{
				byte[] seed = null;
				ECCurve eccurve = CustomNamedCurves.ConfigureCurve(new SecT571K1Curve());
				X9ECPoint g = CustomNamedCurves.ConfigureBasepoint(eccurve, "04026EB7A859923FBC82189631F8103FE4AC9CA2970012D5D46024804801841CA44370958493B205E647DA304DB4CEB08CBBD1BA39494776FB988B47174DCA88C7E2945283A01C89720349DC807F4FBF374F4AEADE3BCA95314DD58CEC9F307A54FFC61EFC006D8A2C9D4979C0AC44AEA74FBEBBB9F772AEDCB620B01A7BA7AF1B320430C8591984F601CD4C143EF1C7A3");
				return new X9ECParameters(eccurve, g, eccurve.Order, eccurve.Cofactor, seed);
			}

			// Token: 0x040040C2 RID: 16578
			internal static readonly X9ECParametersHolder Instance = new CustomNamedCurves.SecT571K1Holder();
		}

		// Token: 0x02000E06 RID: 3590
		internal class SecT571R1Holder : X9ECParametersHolder
		{
			// Token: 0x06008C03 RID: 35843 RVA: 0x0029FA54 File Offset: 0x0029FA54
			private SecT571R1Holder()
			{
			}

			// Token: 0x06008C04 RID: 35844 RVA: 0x0029FA5C File Offset: 0x0029FA5C
			protected override X9ECParameters CreateParameters()
			{
				byte[] seed = Hex.DecodeStrict("2AA058F73A0E33AB486B0F610410C53A7F132310");
				ECCurve eccurve = CustomNamedCurves.ConfigureCurve(new SecT571R1Curve());
				X9ECPoint g = CustomNamedCurves.ConfigureBasepoint(eccurve, "040303001D34B856296C16C0D40D3CD7750A93D1D2955FA80AA5F40FC8DB7B2ABDBDE53950F4C0D293CDD711A35B67FB1499AE60038614F1394ABFA3B4C850D927E1E7769C8EEC2D19037BF27342DA639B6DCCFFFEB73D69D78C6C27A6009CBBCA1980F8533921E8A684423E43BAB08A576291AF8F461BB2A8B3531D2F0485C19B16E2F1516E23DD3C1A4827AF1B8AC15B");
				return new X9ECParameters(eccurve, g, eccurve.Order, eccurve.Cofactor, seed);
			}

			// Token: 0x040040C3 RID: 16579
			internal static readonly X9ECParametersHolder Instance = new CustomNamedCurves.SecT571R1Holder();
		}

		// Token: 0x02000E07 RID: 3591
		internal class SM2P256V1Holder : X9ECParametersHolder
		{
			// Token: 0x06008C06 RID: 35846 RVA: 0x0029FAB0 File Offset: 0x0029FAB0
			private SM2P256V1Holder()
			{
			}

			// Token: 0x06008C07 RID: 35847 RVA: 0x0029FAB8 File Offset: 0x0029FAB8
			protected override X9ECParameters CreateParameters()
			{
				byte[] seed = null;
				ECCurve eccurve = CustomNamedCurves.ConfigureCurve(new SM2P256V1Curve());
				X9ECPoint g = CustomNamedCurves.ConfigureBasepoint(eccurve, "0432C4AE2C1F1981195F9904466A39C9948FE30BBFF2660BE1715A4589334C74C7BC3736A2F4F6779C59BDCEE36B692153D0A9877CC62A474002DF32E52139F0A0");
				return new X9ECParameters(eccurve, g, eccurve.Order, eccurve.Cofactor, seed);
			}

			// Token: 0x040040C4 RID: 16580
			internal static readonly X9ECParametersHolder Instance = new CustomNamedCurves.SM2P256V1Holder();
		}
	}
}
