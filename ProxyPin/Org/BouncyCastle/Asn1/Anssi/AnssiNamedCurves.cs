using System;
using System.Collections;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Math.EC.Multiplier;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Asn1.Anssi
{
	// Token: 0x020000C5 RID: 197
	public class AnssiNamedCurves
	{
		// Token: 0x060007CA RID: 1994 RVA: 0x00040164 File Offset: 0x00040164
		private static X9ECPoint ConfigureBasepoint(ECCurve curve, string encoding)
		{
			X9ECPoint x9ECPoint = new X9ECPoint(curve, Hex.DecodeStrict(encoding));
			WNafUtilities.ConfigureBasepoint(x9ECPoint.Point);
			return x9ECPoint;
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00040190 File Offset: 0x00040190
		private static ECCurve ConfigureCurve(ECCurve curve)
		{
			return curve;
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00040194 File Offset: 0x00040194
		private static BigInteger FromHex(string hex)
		{
			return new BigInteger(1, Hex.DecodeStrict(hex));
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x000401A4 File Offset: 0x000401A4
		private static void DefineCurve(string name, DerObjectIdentifier oid, X9ECParametersHolder holder)
		{
			AnssiNamedCurves.objIds.Add(Platform.ToUpperInvariant(name), oid);
			AnssiNamedCurves.names.Add(oid, name);
			AnssiNamedCurves.curves.Add(oid, holder);
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x000401D0 File Offset: 0x000401D0
		static AnssiNamedCurves()
		{
			AnssiNamedCurves.DefineCurve("FRP256v1", AnssiObjectIdentifiers.FRP256v1, AnssiNamedCurves.Frp256v1Holder.Instance);
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x00040204 File Offset: 0x00040204
		public static X9ECParameters GetByName(string name)
		{
			DerObjectIdentifier oid = AnssiNamedCurves.GetOid(name);
			if (oid != null)
			{
				return AnssiNamedCurves.GetByOid(oid);
			}
			return null;
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0004022C File Offset: 0x0004022C
		public static X9ECParameters GetByOid(DerObjectIdentifier oid)
		{
			X9ECParametersHolder x9ECParametersHolder = (X9ECParametersHolder)AnssiNamedCurves.curves[oid];
			if (x9ECParametersHolder != null)
			{
				return x9ECParametersHolder.Parameters;
			}
			return null;
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x0004025C File Offset: 0x0004025C
		public static DerObjectIdentifier GetOid(string name)
		{
			return (DerObjectIdentifier)AnssiNamedCurves.objIds[Platform.ToUpperInvariant(name)];
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00040274 File Offset: 0x00040274
		public static string GetName(DerObjectIdentifier oid)
		{
			return (string)AnssiNamedCurves.names[oid];
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060007D3 RID: 2003 RVA: 0x00040288 File Offset: 0x00040288
		public static IEnumerable Names
		{
			get
			{
				return new EnumerableProxy(AnssiNamedCurves.names.Values);
			}
		}

		// Token: 0x04000592 RID: 1426
		private static readonly IDictionary objIds = Platform.CreateHashtable();

		// Token: 0x04000593 RID: 1427
		private static readonly IDictionary curves = Platform.CreateHashtable();

		// Token: 0x04000594 RID: 1428
		private static readonly IDictionary names = Platform.CreateHashtable();

		// Token: 0x02000D86 RID: 3462
		internal class Frp256v1Holder : X9ECParametersHolder
		{
			// Token: 0x06008A7B RID: 35451 RVA: 0x0029AC50 File Offset: 0x0029AC50
			private Frp256v1Holder()
			{
			}

			// Token: 0x06008A7C RID: 35452 RVA: 0x0029AC58 File Offset: 0x0029AC58
			protected override X9ECParameters CreateParameters()
			{
				BigInteger q = AnssiNamedCurves.FromHex("F1FD178C0B3AD58F10126DE8CE42435B3961ADBCABC8CA6DE8FCF353D86E9C03");
				BigInteger a = AnssiNamedCurves.FromHex("F1FD178C0B3AD58F10126DE8CE42435B3961ADBCABC8CA6DE8FCF353D86E9C00");
				BigInteger b = AnssiNamedCurves.FromHex("EE353FCA5428A9300D4ABA754A44C00FDFEC0C9AE4B1A1803075ED967B7BB73F");
				byte[] seed = null;
				BigInteger bigInteger = AnssiNamedCurves.FromHex("F1FD178C0B3AD58F10126DE8CE42435B53DC67E140D2BF941FFDD459C6D655E1");
				BigInteger one = BigInteger.One;
				ECCurve curve = AnssiNamedCurves.ConfigureCurve(new FpCurve(q, a, b, bigInteger, one));
				X9ECPoint g = AnssiNamedCurves.ConfigureBasepoint(curve, "04B6B3D4C356C139EB31183D4749D423958C27D2DCAF98B70164C97A2DD98F5CFF6142E0F7C8B204911F9271F0F3ECEF8C2701C307E8E4C9E183115A1554062CFB");
				return new X9ECParameters(curve, g, bigInteger, one, seed);
			}

			// Token: 0x04003FD1 RID: 16337
			internal static readonly X9ECParametersHolder Instance = new AnssiNamedCurves.Frp256v1Holder();
		}
	}
}
