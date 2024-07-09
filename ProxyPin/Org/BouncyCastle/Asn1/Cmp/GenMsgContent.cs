using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020000DA RID: 218
	public class GenMsgContent : Asn1Encodable
	{
		// Token: 0x06000837 RID: 2103 RVA: 0x000419FC File Offset: 0x000419FC
		private GenMsgContent(Asn1Sequence seq)
		{
			this.content = seq;
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00041A0C File Offset: 0x00041A0C
		public static GenMsgContent GetInstance(object obj)
		{
			if (obj is GenMsgContent)
			{
				return (GenMsgContent)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new GenMsgContent((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00041A60 File Offset: 0x00041A60
		public GenMsgContent(params InfoTypeAndValue[] itv)
		{
			this.content = new DerSequence(itv);
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x00041A74 File Offset: 0x00041A74
		public virtual InfoTypeAndValue[] ToInfoTypeAndValueArray()
		{
			InfoTypeAndValue[] array = new InfoTypeAndValue[this.content.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = InfoTypeAndValue.GetInstance(this.content[num]);
			}
			return array;
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x00041AC0 File Offset: 0x00041AC0
		public override Asn1Object ToAsn1Object()
		{
			return this.content;
		}

		// Token: 0x0400060D RID: 1549
		private readonly Asn1Sequence content;
	}
}
