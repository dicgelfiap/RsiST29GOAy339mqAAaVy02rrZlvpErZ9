using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020000F0 RID: 240
	public class PollReqContent : Asn1Encodable
	{
		// Token: 0x060008E0 RID: 2272 RVA: 0x0004385C File Offset: 0x0004385C
		private PollReqContent(Asn1Sequence seq)
		{
			this.content = seq;
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x0004386C File Offset: 0x0004386C
		public static PollReqContent GetInstance(object obj)
		{
			if (obj is PollReqContent)
			{
				return (PollReqContent)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new PollReqContent((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x000438C0 File Offset: 0x000438C0
		public virtual DerInteger[][] GetCertReqIDs()
		{
			DerInteger[][] array = new DerInteger[this.content.Count][];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = PollReqContent.SequenceToDerIntegerArray((Asn1Sequence)this.content[num]);
			}
			return array;
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x00043914 File Offset: 0x00043914
		private static DerInteger[] SequenceToDerIntegerArray(Asn1Sequence seq)
		{
			DerInteger[] array = new DerInteger[seq.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = DerInteger.GetInstance(seq[num]);
			}
			return array;
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x00043958 File Offset: 0x00043958
		public override Asn1Object ToAsn1Object()
		{
			return this.content;
		}

		// Token: 0x0400068E RID: 1678
		private readonly Asn1Sequence content;
	}
}
