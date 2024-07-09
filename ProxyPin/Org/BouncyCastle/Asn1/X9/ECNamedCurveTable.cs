using System;
using System.Collections;
using Org.BouncyCastle.Asn1.Anssi;
using Org.BouncyCastle.Asn1.CryptoPro;
using Org.BouncyCastle.Asn1.GM;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.Sec;
using Org.BouncyCastle.Asn1.TeleTrust;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Asn1.X9
{
	// Token: 0x0200022A RID: 554
	public class ECNamedCurveTable
	{
		// Token: 0x060011F0 RID: 4592 RVA: 0x00065EF8 File Offset: 0x00065EF8
		public static X9ECParameters GetByName(string name)
		{
			X9ECParameters x9ECParameters = X962NamedCurves.GetByName(name);
			if (x9ECParameters == null)
			{
				x9ECParameters = SecNamedCurves.GetByName(name);
			}
			if (x9ECParameters == null)
			{
				x9ECParameters = NistNamedCurves.GetByName(name);
			}
			if (x9ECParameters == null)
			{
				x9ECParameters = TeleTrusTNamedCurves.GetByName(name);
			}
			if (x9ECParameters == null)
			{
				x9ECParameters = AnssiNamedCurves.GetByName(name);
			}
			if (x9ECParameters == null)
			{
				x9ECParameters = ECNamedCurveTable.FromDomainParameters(ECGost3410NamedCurves.GetByName(name));
			}
			if (x9ECParameters == null)
			{
				x9ECParameters = GMNamedCurves.GetByName(name);
			}
			return x9ECParameters;
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x00065F64 File Offset: 0x00065F64
		public static string GetName(DerObjectIdentifier oid)
		{
			string name = X962NamedCurves.GetName(oid);
			if (name == null)
			{
				name = SecNamedCurves.GetName(oid);
			}
			if (name == null)
			{
				name = NistNamedCurves.GetName(oid);
			}
			if (name == null)
			{
				name = TeleTrusTNamedCurves.GetName(oid);
			}
			if (name == null)
			{
				name = AnssiNamedCurves.GetName(oid);
			}
			if (name == null)
			{
				name = ECGost3410NamedCurves.GetName(oid);
			}
			if (name == null)
			{
				name = GMNamedCurves.GetName(oid);
			}
			return name;
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x00065FCC File Offset: 0x00065FCC
		public static DerObjectIdentifier GetOid(string name)
		{
			DerObjectIdentifier oid = X962NamedCurves.GetOid(name);
			if (oid == null)
			{
				oid = SecNamedCurves.GetOid(name);
			}
			if (oid == null)
			{
				oid = NistNamedCurves.GetOid(name);
			}
			if (oid == null)
			{
				oid = TeleTrusTNamedCurves.GetOid(name);
			}
			if (oid == null)
			{
				oid = AnssiNamedCurves.GetOid(name);
			}
			if (oid == null)
			{
				oid = ECGost3410NamedCurves.GetOid(name);
			}
			if (oid == null)
			{
				oid = GMNamedCurves.GetOid(name);
			}
			return oid;
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x00066034 File Offset: 0x00066034
		public static X9ECParameters GetByOid(DerObjectIdentifier oid)
		{
			X9ECParameters x9ECParameters = X962NamedCurves.GetByOid(oid);
			if (x9ECParameters == null)
			{
				x9ECParameters = SecNamedCurves.GetByOid(oid);
			}
			if (x9ECParameters == null)
			{
				x9ECParameters = TeleTrusTNamedCurves.GetByOid(oid);
			}
			if (x9ECParameters == null)
			{
				x9ECParameters = AnssiNamedCurves.GetByOid(oid);
			}
			if (x9ECParameters == null)
			{
				x9ECParameters = ECNamedCurveTable.FromDomainParameters(ECGost3410NamedCurves.GetByOid(oid));
			}
			if (x9ECParameters == null)
			{
				x9ECParameters = GMNamedCurves.GetByOid(oid);
			}
			return x9ECParameters;
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x060011F4 RID: 4596 RVA: 0x00066094 File Offset: 0x00066094
		public static IEnumerable Names
		{
			get
			{
				IList list = Platform.CreateArrayList();
				CollectionUtilities.AddRange(list, X962NamedCurves.Names);
				CollectionUtilities.AddRange(list, SecNamedCurves.Names);
				CollectionUtilities.AddRange(list, NistNamedCurves.Names);
				CollectionUtilities.AddRange(list, TeleTrusTNamedCurves.Names);
				CollectionUtilities.AddRange(list, AnssiNamedCurves.Names);
				CollectionUtilities.AddRange(list, ECGost3410NamedCurves.Names);
				CollectionUtilities.AddRange(list, GMNamedCurves.Names);
				return list;
			}
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x000660FC File Offset: 0x000660FC
		private static X9ECParameters FromDomainParameters(ECDomainParameters dp)
		{
			if (dp != null)
			{
				return new X9ECParameters(dp.Curve, dp.G, dp.N, dp.H, dp.GetSeed());
			}
			return null;
		}
	}
}
