using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Crmf
{
	// Token: 0x02000129 RID: 297
	public class AttributeTypeAndValue : Asn1Encodable
	{
		// Token: 0x06000A8E RID: 2702 RVA: 0x00048E20 File Offset: 0x00048E20
		private AttributeTypeAndValue(Asn1Sequence seq)
		{
			this.type = (DerObjectIdentifier)seq[0];
			this.value = seq[1];
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x00048E48 File Offset: 0x00048E48
		public static AttributeTypeAndValue GetInstance(object obj)
		{
			if (obj is AttributeTypeAndValue)
			{
				return (AttributeTypeAndValue)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new AttributeTypeAndValue((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x00048E9C File Offset: 0x00048E9C
		public AttributeTypeAndValue(string oid, Asn1Encodable value) : this(new DerObjectIdentifier(oid), value)
		{
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x00048EAC File Offset: 0x00048EAC
		public AttributeTypeAndValue(DerObjectIdentifier type, Asn1Encodable value)
		{
			this.type = type;
			this.value = value;
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000A92 RID: 2706 RVA: 0x00048EC4 File Offset: 0x00048EC4
		public virtual DerObjectIdentifier Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000A93 RID: 2707 RVA: 0x00048ECC File Offset: 0x00048ECC
		public virtual Asn1Encodable Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x00048ED4 File Offset: 0x00048ED4
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.type,
				this.value
			});
		}

		// Token: 0x04000747 RID: 1863
		private readonly DerObjectIdentifier type;

		// Token: 0x04000748 RID: 1864
		private readonly Asn1Encodable value;
	}
}
