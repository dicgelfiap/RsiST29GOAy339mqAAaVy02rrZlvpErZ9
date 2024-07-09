using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000117 RID: 279
	public class OtherKeyAttribute : Asn1Encodable
	{
		// Token: 0x060009FB RID: 2555 RVA: 0x000470F8 File Offset: 0x000470F8
		public static OtherKeyAttribute GetInstance(object obj)
		{
			if (obj == null || obj is OtherKeyAttribute)
			{
				return (OtherKeyAttribute)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new OtherKeyAttribute((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x00047154 File Offset: 0x00047154
		public OtherKeyAttribute(Asn1Sequence seq)
		{
			this.keyAttrId = (DerObjectIdentifier)seq[0];
			this.keyAttr = seq[1];
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x0004717C File Offset: 0x0004717C
		public OtherKeyAttribute(DerObjectIdentifier keyAttrId, Asn1Encodable keyAttr)
		{
			this.keyAttrId = keyAttrId;
			this.keyAttr = keyAttr;
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060009FE RID: 2558 RVA: 0x00047194 File Offset: 0x00047194
		public DerObjectIdentifier KeyAttrId
		{
			get
			{
				return this.keyAttrId;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x0004719C File Offset: 0x0004719C
		public Asn1Encodable KeyAttr
		{
			get
			{
				return this.keyAttr;
			}
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x000471A4 File Offset: 0x000471A4
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.keyAttrId,
				this.keyAttr
			});
		}

		// Token: 0x0400070C RID: 1804
		private DerObjectIdentifier keyAttrId;

		// Token: 0x0400070D RID: 1805
		private Asn1Encodable keyAttr;
	}
}
