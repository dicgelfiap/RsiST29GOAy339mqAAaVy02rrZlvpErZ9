using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x0200010C RID: 268
	public class Evidence : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x060009A0 RID: 2464 RVA: 0x0004619C File Offset: 0x0004619C
		public Evidence(TimeStampTokenEvidence tstEvidence)
		{
			this.tstEvidence = tstEvidence;
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x000461AC File Offset: 0x000461AC
		private Evidence(Asn1TaggedObject tagged)
		{
			if (tagged.TagNo == 0)
			{
				this.tstEvidence = TimeStampTokenEvidence.GetInstance(tagged, false);
				return;
			}
			if (tagged.TagNo == 2)
			{
				this.otherEvidence = Asn1Sequence.GetInstance(tagged, false);
				return;
			}
			throw new ArgumentException("unknown tag in Evidence", "tagged");
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x00046208 File Offset: 0x00046208
		public static Evidence GetInstance(object obj)
		{
			if (obj is Evidence)
			{
				return (Evidence)obj;
			}
			if (obj is Asn1TaggedObject)
			{
				return new Evidence(Asn1TaggedObject.GetInstance(obj));
			}
			throw new ArgumentException("Unknown object in GetInstance: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0004625C File Offset: 0x0004625C
		public static Evidence GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return Evidence.GetInstance(obj.GetObject());
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060009A4 RID: 2468 RVA: 0x0004626C File Offset: 0x0004626C
		public virtual TimeStampTokenEvidence TstEvidence
		{
			get
			{
				return this.tstEvidence;
			}
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x00046274 File Offset: 0x00046274
		public override Asn1Object ToAsn1Object()
		{
			if (this.tstEvidence != null)
			{
				return new DerTaggedObject(false, 0, this.tstEvidence);
			}
			return new DerTaggedObject(false, 2, this.otherEvidence);
		}

		// Token: 0x040006ED RID: 1773
		private TimeStampTokenEvidence tstEvidence;

		// Token: 0x040006EE RID: 1774
		private Asn1Sequence otherEvidence;
	}
}
