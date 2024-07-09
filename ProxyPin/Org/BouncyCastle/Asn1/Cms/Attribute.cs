using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x020000FA RID: 250
	public class Attribute : Asn1Encodable
	{
		// Token: 0x06000919 RID: 2329 RVA: 0x00044370 File Offset: 0x00044370
		public static Attribute GetInstance(object obj)
		{
			if (obj == null || obj is Attribute)
			{
				return (Attribute)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new Attribute((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x000443CC File Offset: 0x000443CC
		public Attribute(Asn1Sequence seq)
		{
			this.attrType = (DerObjectIdentifier)seq[0];
			this.attrValues = (Asn1Set)seq[1];
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x000443F8 File Offset: 0x000443F8
		public Attribute(DerObjectIdentifier attrType, Asn1Set attrValues)
		{
			this.attrType = attrType;
			this.attrValues = attrValues;
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x00044410 File Offset: 0x00044410
		public DerObjectIdentifier AttrType
		{
			get
			{
				return this.attrType;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x00044418 File Offset: 0x00044418
		public Asn1Set AttrValues
		{
			get
			{
				return this.attrValues;
			}
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x00044420 File Offset: 0x00044420
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.attrType,
				this.attrValues
			});
		}

		// Token: 0x040006A3 RID: 1699
		private DerObjectIdentifier attrType;

		// Token: 0x040006A4 RID: 1700
		private Asn1Set attrValues;
	}
}
