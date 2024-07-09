using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Crmf
{
	// Token: 0x02000130 RID: 304
	public class Controls : Asn1Encodable
	{
		// Token: 0x06000ACA RID: 2762 RVA: 0x000496F4 File Offset: 0x000496F4
		private Controls(Asn1Sequence seq)
		{
			this.content = seq;
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x00049704 File Offset: 0x00049704
		public static Controls GetInstance(object obj)
		{
			if (obj is Controls)
			{
				return (Controls)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new Controls((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x00049758 File Offset: 0x00049758
		public Controls(params AttributeTypeAndValue[] atvs)
		{
			this.content = new DerSequence(atvs);
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x0004976C File Offset: 0x0004976C
		public virtual AttributeTypeAndValue[] ToAttributeTypeAndValueArray()
		{
			AttributeTypeAndValue[] array = new AttributeTypeAndValue[this.content.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = AttributeTypeAndValue.GetInstance(this.content[num]);
			}
			return array;
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x000497B8 File Offset: 0x000497B8
		public override Asn1Object ToAsn1Object()
		{
			return this.content;
		}

		// Token: 0x04000767 RID: 1895
		private readonly Asn1Sequence content;
	}
}
