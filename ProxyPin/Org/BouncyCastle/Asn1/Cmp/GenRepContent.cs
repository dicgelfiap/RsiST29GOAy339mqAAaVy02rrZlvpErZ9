using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020000DB RID: 219
	public class GenRepContent : Asn1Encodable
	{
		// Token: 0x0600083C RID: 2108 RVA: 0x00041AC8 File Offset: 0x00041AC8
		private GenRepContent(Asn1Sequence seq)
		{
			this.content = seq;
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x00041AD8 File Offset: 0x00041AD8
		public static GenRepContent GetInstance(object obj)
		{
			if (obj is GenRepContent)
			{
				return (GenRepContent)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new GenRepContent((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00041B2C File Offset: 0x00041B2C
		public GenRepContent(params InfoTypeAndValue[] itv)
		{
			this.content = new DerSequence(itv);
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00041B40 File Offset: 0x00041B40
		public virtual InfoTypeAndValue[] ToInfoTypeAndValueArray()
		{
			InfoTypeAndValue[] array = new InfoTypeAndValue[this.content.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = InfoTypeAndValue.GetInstance(this.content[num]);
			}
			return array;
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00041B8C File Offset: 0x00041B8C
		public override Asn1Object ToAsn1Object()
		{
			return this.content;
		}

		// Token: 0x0400060E RID: 1550
		private readonly Asn1Sequence content;
	}
}
