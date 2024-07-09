using System;
using System.Collections;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Smime
{
	// Token: 0x020001C2 RID: 450
	public class SmimeCapabilities : Asn1Encodable
	{
		// Token: 0x06000EA1 RID: 3745 RVA: 0x000589E4 File Offset: 0x000589E4
		public static SmimeCapabilities GetInstance(object obj)
		{
			if (obj == null || obj is SmimeCapabilities)
			{
				return (SmimeCapabilities)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new SmimeCapabilities((Asn1Sequence)obj);
			}
			if (obj is AttributeX509)
			{
				return new SmimeCapabilities((Asn1Sequence)((AttributeX509)obj).AttrValues[0]);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x00058A68 File Offset: 0x00058A68
		public SmimeCapabilities(Asn1Sequence seq)
		{
			this.capabilities = seq;
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x00058A78 File Offset: 0x00058A78
		[Obsolete("Use 'GetCapabilitiesForOid' instead")]
		public ArrayList GetCapabilities(DerObjectIdentifier capability)
		{
			ArrayList arrayList = new ArrayList();
			this.DoGetCapabilitiesForOid(capability, arrayList);
			return arrayList;
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x00058A98 File Offset: 0x00058A98
		public IList GetCapabilitiesForOid(DerObjectIdentifier capability)
		{
			IList list = Platform.CreateArrayList();
			this.DoGetCapabilitiesForOid(capability, list);
			return list;
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x00058AB8 File Offset: 0x00058AB8
		private void DoGetCapabilitiesForOid(DerObjectIdentifier capability, IList list)
		{
			if (capability == null)
			{
				using (IEnumerator enumerator = this.capabilities.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						SmimeCapability instance = SmimeCapability.GetInstance(obj);
						list.Add(instance);
					}
					return;
				}
			}
			foreach (object obj2 in this.capabilities)
			{
				SmimeCapability instance2 = SmimeCapability.GetInstance(obj2);
				if (capability.Equals(instance2.CapabilityID))
				{
					list.Add(instance2);
				}
			}
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x00058B90 File Offset: 0x00058B90
		public override Asn1Object ToAsn1Object()
		{
			return this.capabilities;
		}

		// Token: 0x04000AEE RID: 2798
		public static readonly DerObjectIdentifier PreferSignedData = PkcsObjectIdentifiers.PreferSignedData;

		// Token: 0x04000AEF RID: 2799
		public static readonly DerObjectIdentifier CannotDecryptAny = PkcsObjectIdentifiers.CannotDecryptAny;

		// Token: 0x04000AF0 RID: 2800
		public static readonly DerObjectIdentifier SmimeCapabilitesVersions = PkcsObjectIdentifiers.SmimeCapabilitiesVersions;

		// Token: 0x04000AF1 RID: 2801
		public static readonly DerObjectIdentifier Aes256Cbc = NistObjectIdentifiers.IdAes256Cbc;

		// Token: 0x04000AF2 RID: 2802
		public static readonly DerObjectIdentifier Aes192Cbc = NistObjectIdentifiers.IdAes192Cbc;

		// Token: 0x04000AF3 RID: 2803
		public static readonly DerObjectIdentifier Aes128Cbc = NistObjectIdentifiers.IdAes128Cbc;

		// Token: 0x04000AF4 RID: 2804
		public static readonly DerObjectIdentifier IdeaCbc = new DerObjectIdentifier("1.3.6.1.4.1.188.7.1.1.2");

		// Token: 0x04000AF5 RID: 2805
		public static readonly DerObjectIdentifier Cast5Cbc = new DerObjectIdentifier("1.2.840.113533.7.66.10");

		// Token: 0x04000AF6 RID: 2806
		public static readonly DerObjectIdentifier DesCbc = new DerObjectIdentifier("1.3.14.3.2.7");

		// Token: 0x04000AF7 RID: 2807
		public static readonly DerObjectIdentifier DesEde3Cbc = PkcsObjectIdentifiers.DesEde3Cbc;

		// Token: 0x04000AF8 RID: 2808
		public static readonly DerObjectIdentifier RC2Cbc = PkcsObjectIdentifiers.RC2Cbc;

		// Token: 0x04000AF9 RID: 2809
		private Asn1Sequence capabilities;
	}
}
