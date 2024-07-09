using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020000EB RID: 235
	public class PkiMessages : Asn1Encodable
	{
		// Token: 0x060008C3 RID: 2243 RVA: 0x0004336C File Offset: 0x0004336C
		private PkiMessages(Asn1Sequence seq)
		{
			this.content = seq;
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x0004337C File Offset: 0x0004337C
		public static PkiMessages GetInstance(object obj)
		{
			if (obj is PkiMessages)
			{
				return (PkiMessages)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new PkiMessages((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x000433D0 File Offset: 0x000433D0
		public PkiMessages(params PkiMessage[] msgs)
		{
			this.content = new DerSequence(msgs);
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x000433E4 File Offset: 0x000433E4
		public virtual PkiMessage[] ToPkiMessageArray()
		{
			PkiMessage[] array = new PkiMessage[this.content.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = PkiMessage.GetInstance(this.content[num]);
			}
			return array;
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x00043430 File Offset: 0x00043430
		public override Asn1Object ToAsn1Object()
		{
			return this.content;
		}

		// Token: 0x04000677 RID: 1655
		private Asn1Sequence content;
	}
}
