using System;

namespace Org.BouncyCastle.Asn1.X500
{
	// Token: 0x020001D6 RID: 470
	public class Rdn : Asn1Encodable
	{
		// Token: 0x06000F28 RID: 3880 RVA: 0x0005B840 File Offset: 0x0005B840
		private Rdn(Asn1Set values)
		{
			this.values = values;
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x0005B850 File Offset: 0x0005B850
		public static Rdn GetInstance(object obj)
		{
			if (obj is Rdn)
			{
				return (Rdn)obj;
			}
			if (obj != null)
			{
				return new Rdn(Asn1Set.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x0005B878 File Offset: 0x0005B878
		public Rdn(DerObjectIdentifier oid, Asn1Encodable value)
		{
			this.values = new DerSet(new DerSequence(new Asn1Encodable[]
			{
				oid,
				value
			}));
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x0005B8B8 File Offset: 0x0005B8B8
		public Rdn(AttributeTypeAndValue attrTAndV)
		{
			this.values = new DerSet(attrTAndV);
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x0005B8CC File Offset: 0x0005B8CC
		public Rdn(AttributeTypeAndValue[] aAndVs)
		{
			this.values = new DerSet(aAndVs);
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000F2D RID: 3885 RVA: 0x0005B8E0 File Offset: 0x0005B8E0
		public virtual bool IsMultiValued
		{
			get
			{
				return this.values.Count > 1;
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000F2E RID: 3886 RVA: 0x0005B8F0 File Offset: 0x0005B8F0
		public virtual int Count
		{
			get
			{
				return this.values.Count;
			}
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x0005B900 File Offset: 0x0005B900
		public virtual AttributeTypeAndValue GetFirst()
		{
			if (this.values.Count == 0)
			{
				return null;
			}
			return AttributeTypeAndValue.GetInstance(this.values[0]);
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x0005B928 File Offset: 0x0005B928
		public virtual AttributeTypeAndValue[] GetTypesAndValues()
		{
			AttributeTypeAndValue[] array = new AttributeTypeAndValue[this.values.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = AttributeTypeAndValue.GetInstance(this.values[i]);
			}
			return array;
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x0005B974 File Offset: 0x0005B974
		public override Asn1Object ToAsn1Object()
		{
			return this.values;
		}

		// Token: 0x04000B6D RID: 2925
		private readonly Asn1Set values;
	}
}
