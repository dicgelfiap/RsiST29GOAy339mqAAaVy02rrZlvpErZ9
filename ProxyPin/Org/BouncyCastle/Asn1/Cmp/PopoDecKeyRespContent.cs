using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020000F2 RID: 242
	public class PopoDecKeyRespContent : Asn1Encodable
	{
		// Token: 0x060008E9 RID: 2281 RVA: 0x00043A18 File Offset: 0x00043A18
		private PopoDecKeyRespContent(Asn1Sequence seq)
		{
			this.content = seq;
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x00043A28 File Offset: 0x00043A28
		public static PopoDecKeyRespContent GetInstance(object obj)
		{
			if (obj is PopoDecKeyRespContent)
			{
				return (PopoDecKeyRespContent)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new PopoDecKeyRespContent((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x00043A7C File Offset: 0x00043A7C
		public virtual DerInteger[] ToDerIntegerArray()
		{
			DerInteger[] array = new DerInteger[this.content.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = DerInteger.GetInstance(this.content[num]);
			}
			return array;
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x00043AC8 File Offset: 0x00043AC8
		public override Asn1Object ToAsn1Object()
		{
			return this.content;
		}

		// Token: 0x04000690 RID: 1680
		private readonly Asn1Sequence content;
	}
}
