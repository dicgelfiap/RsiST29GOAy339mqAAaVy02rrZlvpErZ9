using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Crmf
{
	// Token: 0x02000135 RID: 309
	public class OptionalValidity : Asn1Encodable
	{
		// Token: 0x06000AEB RID: 2795 RVA: 0x00049C98 File Offset: 0x00049C98
		private OptionalValidity(Asn1Sequence seq)
		{
			foreach (object obj in seq)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)obj;
				if (asn1TaggedObject.TagNo == 0)
				{
					this.notBefore = Time.GetInstance(asn1TaggedObject, true);
				}
				else
				{
					this.notAfter = Time.GetInstance(asn1TaggedObject, true);
				}
			}
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x00049D20 File Offset: 0x00049D20
		public static OptionalValidity GetInstance(object obj)
		{
			if (obj == null || obj is OptionalValidity)
			{
				return (OptionalValidity)obj;
			}
			return new OptionalValidity(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x00049D48 File Offset: 0x00049D48
		public OptionalValidity(Time notBefore, Time notAfter)
		{
			this.notBefore = notBefore;
			this.notAfter = notAfter;
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000AEE RID: 2798 RVA: 0x00049D60 File Offset: 0x00049D60
		public virtual Time NotBefore
		{
			get
			{
				return this.notBefore;
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000AEF RID: 2799 RVA: 0x00049D68 File Offset: 0x00049D68
		public virtual Time NotAfter
		{
			get
			{
				return this.notAfter;
			}
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x00049D70 File Offset: 0x00049D70
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			asn1EncodableVector.AddOptionalTagged(true, 0, this.notBefore);
			asn1EncodableVector.AddOptionalTagged(true, 1, this.notAfter);
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x0400077A RID: 1914
		private readonly Time notBefore;

		// Token: 0x0400077B RID: 1915
		private readonly Time notAfter;
	}
}
