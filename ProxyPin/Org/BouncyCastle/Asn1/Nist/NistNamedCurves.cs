using System;
using System.Collections;
using Org.BouncyCastle.Asn1.Sec;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Asn1.Nist
{
	// Token: 0x0200018A RID: 394
	public sealed class NistNamedCurves
	{
		// Token: 0x06000D06 RID: 3334 RVA: 0x00052950 File Offset: 0x00052950
		private NistNamedCurves()
		{
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x00052958 File Offset: 0x00052958
		private static void DefineCurveAlias(string name, DerObjectIdentifier oid)
		{
			NistNamedCurves.objIds.Add(Platform.ToUpperInvariant(name), oid);
			NistNamedCurves.names.Add(oid, name);
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x00052978 File Offset: 0x00052978
		static NistNamedCurves()
		{
			NistNamedCurves.DefineCurveAlias("B-163", SecObjectIdentifiers.SecT163r2);
			NistNamedCurves.DefineCurveAlias("B-233", SecObjectIdentifiers.SecT233r1);
			NistNamedCurves.DefineCurveAlias("B-283", SecObjectIdentifiers.SecT283r1);
			NistNamedCurves.DefineCurveAlias("B-409", SecObjectIdentifiers.SecT409r1);
			NistNamedCurves.DefineCurveAlias("B-571", SecObjectIdentifiers.SecT571r1);
			NistNamedCurves.DefineCurveAlias("K-163", SecObjectIdentifiers.SecT163k1);
			NistNamedCurves.DefineCurveAlias("K-233", SecObjectIdentifiers.SecT233k1);
			NistNamedCurves.DefineCurveAlias("K-283", SecObjectIdentifiers.SecT283k1);
			NistNamedCurves.DefineCurveAlias("K-409", SecObjectIdentifiers.SecT409k1);
			NistNamedCurves.DefineCurveAlias("K-571", SecObjectIdentifiers.SecT571k1);
			NistNamedCurves.DefineCurveAlias("P-192", SecObjectIdentifiers.SecP192r1);
			NistNamedCurves.DefineCurveAlias("P-224", SecObjectIdentifiers.SecP224r1);
			NistNamedCurves.DefineCurveAlias("P-256", SecObjectIdentifiers.SecP256r1);
			NistNamedCurves.DefineCurveAlias("P-384", SecObjectIdentifiers.SecP384r1);
			NistNamedCurves.DefineCurveAlias("P-521", SecObjectIdentifiers.SecP521r1);
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x00052A80 File Offset: 0x00052A80
		public static X9ECParameters GetByName(string name)
		{
			DerObjectIdentifier oid = NistNamedCurves.GetOid(name);
			if (oid != null)
			{
				return NistNamedCurves.GetByOid(oid);
			}
			return null;
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x00052AA8 File Offset: 0x00052AA8
		public static X9ECParameters GetByOid(DerObjectIdentifier oid)
		{
			return SecNamedCurves.GetByOid(oid);
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x00052AB0 File Offset: 0x00052AB0
		public static DerObjectIdentifier GetOid(string name)
		{
			return (DerObjectIdentifier)NistNamedCurves.objIds[Platform.ToUpperInvariant(name)];
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x00052AC8 File Offset: 0x00052AC8
		public static string GetName(DerObjectIdentifier oid)
		{
			return (string)NistNamedCurves.names[oid];
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000D0D RID: 3341 RVA: 0x00052ADC File Offset: 0x00052ADC
		public static IEnumerable Names
		{
			get
			{
				return new EnumerableProxy(NistNamedCurves.names.Values);
			}
		}

		// Token: 0x04000936 RID: 2358
		private static readonly IDictionary objIds = Platform.CreateHashtable();

		// Token: 0x04000937 RID: 2359
		private static readonly IDictionary names = Platform.CreateHashtable();
	}
}
