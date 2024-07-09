using System;
using System.Collections;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020001F8 RID: 504
	public class ExtendedKeyUsage : Asn1Encodable
	{
		// Token: 0x06001038 RID: 4152 RVA: 0x0005EE4C File Offset: 0x0005EE4C
		public static ExtendedKeyUsage GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return ExtendedKeyUsage.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x0005EE5C File Offset: 0x0005EE5C
		public static ExtendedKeyUsage GetInstance(object obj)
		{
			if (obj is ExtendedKeyUsage)
			{
				return (ExtendedKeyUsage)obj;
			}
			if (obj is X509Extension)
			{
				return ExtendedKeyUsage.GetInstance(X509Extension.ConvertValueToObject((X509Extension)obj));
			}
			if (obj == null)
			{
				return null;
			}
			return new ExtendedKeyUsage(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x0005EEB0 File Offset: 0x0005EEB0
		public static ExtendedKeyUsage FromExtensions(X509Extensions extensions)
		{
			return ExtendedKeyUsage.GetInstance(X509Extensions.GetExtensionParsedValue(extensions, X509Extensions.ExtendedKeyUsage));
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x0005EEC4 File Offset: 0x0005EEC4
		private ExtendedKeyUsage(Asn1Sequence seq)
		{
			this.seq = seq;
			foreach (object obj in seq)
			{
				Asn1Encodable obj2 = (Asn1Encodable)obj;
				DerObjectIdentifier instance = DerObjectIdentifier.GetInstance(obj2);
				this.usageTable[instance] = instance;
			}
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x0005EF48 File Offset: 0x0005EF48
		public ExtendedKeyUsage(params KeyPurposeID[] usages)
		{
			this.seq = new DerSequence(usages);
			foreach (KeyPurposeID keyPurposeID in usages)
			{
				this.usageTable[keyPurposeID] = keyPurposeID;
			}
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x0005EFA0 File Offset: 0x0005EFA0
		[Obsolete]
		public ExtendedKeyUsage(ArrayList usages) : this(usages)
		{
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x0005EFAC File Offset: 0x0005EFAC
		public ExtendedKeyUsage(IEnumerable usages)
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			foreach (object obj in usages)
			{
				DerObjectIdentifier instance = DerObjectIdentifier.GetInstance(obj);
				asn1EncodableVector.Add(instance);
				this.usageTable[instance] = instance;
			}
			this.seq = new DerSequence(asn1EncodableVector);
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x0005F040 File Offset: 0x0005F040
		public bool HasKeyPurposeId(KeyPurposeID keyPurposeId)
		{
			return this.usageTable.Contains(keyPurposeId);
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x0005F050 File Offset: 0x0005F050
		[Obsolete("Use 'GetAllUsages'")]
		public ArrayList GetUsages()
		{
			return new ArrayList(this.usageTable.Values);
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x0005F064 File Offset: 0x0005F064
		public IList GetAllUsages()
		{
			return Platform.CreateArrayList(this.usageTable.Values);
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06001042 RID: 4162 RVA: 0x0005F078 File Offset: 0x0005F078
		public int Count
		{
			get
			{
				return this.usageTable.Count;
			}
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x0005F088 File Offset: 0x0005F088
		public override Asn1Object ToAsn1Object()
		{
			return this.seq;
		}

		// Token: 0x04000BDC RID: 3036
		internal readonly IDictionary usageTable = Platform.CreateHashtable();

		// Token: 0x04000BDD RID: 3037
		internal readonly Asn1Sequence seq;
	}
}
