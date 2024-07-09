using System;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000127 RID: 295
	public class TimeStampedDataParser
	{
		// Token: 0x06000A81 RID: 2689 RVA: 0x00048BDC File Offset: 0x00048BDC
		private TimeStampedDataParser(Asn1SequenceParser parser)
		{
			this.parser = parser;
			this.version = DerInteger.GetInstance(parser.ReadObject());
			Asn1Object asn1Object = parser.ReadObject().ToAsn1Object();
			if (asn1Object is DerIA5String)
			{
				this.dataUri = DerIA5String.GetInstance(asn1Object);
				asn1Object = parser.ReadObject().ToAsn1Object();
			}
			if (asn1Object is Asn1SequenceParser)
			{
				this.metaData = MetaData.GetInstance(asn1Object.ToAsn1Object());
				asn1Object = parser.ReadObject().ToAsn1Object();
			}
			if (asn1Object is Asn1OctetStringParser)
			{
				this.content = (Asn1OctetStringParser)asn1Object;
			}
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x00048C7C File Offset: 0x00048C7C
		public static TimeStampedDataParser GetInstance(object obj)
		{
			if (obj is Asn1Sequence)
			{
				return new TimeStampedDataParser(((Asn1Sequence)obj).Parser);
			}
			if (obj is Asn1SequenceParser)
			{
				return new TimeStampedDataParser((Asn1SequenceParser)obj);
			}
			return null;
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x00048CB4 File Offset: 0x00048CB4
		public virtual DerIA5String DataUri
		{
			get
			{
				return this.dataUri;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000A84 RID: 2692 RVA: 0x00048CBC File Offset: 0x00048CBC
		public virtual MetaData MetaData
		{
			get
			{
				return this.metaData;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000A85 RID: 2693 RVA: 0x00048CC4 File Offset: 0x00048CC4
		public virtual Asn1OctetStringParser Content
		{
			get
			{
				return this.content;
			}
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x00048CCC File Offset: 0x00048CCC
		public virtual Evidence GetTemporalEvidence()
		{
			if (this.temporalEvidence == null)
			{
				this.temporalEvidence = Evidence.GetInstance(this.parser.ReadObject().ToAsn1Object());
			}
			return this.temporalEvidence;
		}

		// Token: 0x04000740 RID: 1856
		private DerInteger version;

		// Token: 0x04000741 RID: 1857
		private DerIA5String dataUri;

		// Token: 0x04000742 RID: 1858
		private MetaData metaData;

		// Token: 0x04000743 RID: 1859
		private Asn1OctetStringParser content;

		// Token: 0x04000744 RID: 1860
		private Evidence temporalEvidence;

		// Token: 0x04000745 RID: 1861
		private Asn1SequenceParser parser;
	}
}
