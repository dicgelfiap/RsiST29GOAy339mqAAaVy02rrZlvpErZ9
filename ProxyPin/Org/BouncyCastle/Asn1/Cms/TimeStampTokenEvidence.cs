using System;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000128 RID: 296
	public class TimeStampTokenEvidence : Asn1Encodable
	{
		// Token: 0x06000A87 RID: 2695 RVA: 0x00048CFC File Offset: 0x00048CFC
		public TimeStampTokenEvidence(TimeStampAndCrl[] timeStampAndCrls)
		{
			this.timeStampAndCrls = timeStampAndCrls;
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x00048D0C File Offset: 0x00048D0C
		public TimeStampTokenEvidence(TimeStampAndCrl timeStampAndCrl)
		{
			this.timeStampAndCrls = new TimeStampAndCrl[]
			{
				timeStampAndCrl
			};
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x00048D3C File Offset: 0x00048D3C
		private TimeStampTokenEvidence(Asn1Sequence seq)
		{
			this.timeStampAndCrls = new TimeStampAndCrl[seq.Count];
			int num = 0;
			foreach (object obj in seq)
			{
				Asn1Encodable asn1Encodable = (Asn1Encodable)obj;
				this.timeStampAndCrls[num++] = TimeStampAndCrl.GetInstance(asn1Encodable.ToAsn1Object());
			}
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x00048DC4 File Offset: 0x00048DC4
		public static TimeStampTokenEvidence GetInstance(Asn1TaggedObject tagged, bool isExplicit)
		{
			return TimeStampTokenEvidence.GetInstance(Asn1Sequence.GetInstance(tagged, isExplicit));
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x00048DD4 File Offset: 0x00048DD4
		public static TimeStampTokenEvidence GetInstance(object obj)
		{
			if (obj is TimeStampTokenEvidence)
			{
				return (TimeStampTokenEvidence)obj;
			}
			if (obj != null)
			{
				return new TimeStampTokenEvidence(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x00048DFC File Offset: 0x00048DFC
		public virtual TimeStampAndCrl[] ToTimeStampAndCrlArray()
		{
			return (TimeStampAndCrl[])this.timeStampAndCrls.Clone();
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x00048E10 File Offset: 0x00048E10
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(this.timeStampAndCrls);
		}

		// Token: 0x04000746 RID: 1862
		private TimeStampAndCrl[] timeStampAndCrls;
	}
}
