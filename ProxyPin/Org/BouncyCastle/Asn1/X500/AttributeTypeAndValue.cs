using System;

namespace Org.BouncyCastle.Asn1.X500
{
	// Token: 0x020001D4 RID: 468
	public class AttributeTypeAndValue : Asn1Encodable
	{
		// Token: 0x06000F1C RID: 3868 RVA: 0x0005B690 File Offset: 0x0005B690
		private AttributeTypeAndValue(Asn1Sequence seq)
		{
			this.type = (DerObjectIdentifier)seq[0];
			this.value = seq[1];
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x0005B6B8 File Offset: 0x0005B6B8
		public static AttributeTypeAndValue GetInstance(object obj)
		{
			if (obj is AttributeTypeAndValue)
			{
				return (AttributeTypeAndValue)obj;
			}
			if (obj != null)
			{
				return new AttributeTypeAndValue(Asn1Sequence.GetInstance(obj));
			}
			throw new ArgumentNullException("obj");
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x0005B6E8 File Offset: 0x0005B6E8
		public AttributeTypeAndValue(DerObjectIdentifier type, Asn1Encodable value)
		{
			this.type = type;
			this.value = value;
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000F1F RID: 3871 RVA: 0x0005B700 File Offset: 0x0005B700
		public virtual DerObjectIdentifier Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000F20 RID: 3872 RVA: 0x0005B708 File Offset: 0x0005B708
		public virtual Asn1Encodable Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x0005B710 File Offset: 0x0005B710
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.type,
				this.value
			});
		}

		// Token: 0x04000B6A RID: 2922
		private readonly DerObjectIdentifier type;

		// Token: 0x04000B6B RID: 2923
		private readonly Asn1Encodable value;
	}
}
