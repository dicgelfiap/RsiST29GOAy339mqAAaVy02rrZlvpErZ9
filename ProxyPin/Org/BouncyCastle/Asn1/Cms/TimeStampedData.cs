using System;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000126 RID: 294
	public class TimeStampedData : Asn1Encodable
	{
		// Token: 0x06000A79 RID: 2681 RVA: 0x00048A28 File Offset: 0x00048A28
		public TimeStampedData(DerIA5String dataUri, MetaData metaData, Asn1OctetString content, Evidence temporalEvidence)
		{
			this.version = new DerInteger(1);
			this.dataUri = dataUri;
			this.metaData = metaData;
			this.content = content;
			this.temporalEvidence = temporalEvidence;
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x00048A5C File Offset: 0x00048A5C
		private TimeStampedData(Asn1Sequence seq)
		{
			this.version = DerInteger.GetInstance(seq[0]);
			int index = 1;
			if (seq[index] is DerIA5String)
			{
				this.dataUri = DerIA5String.GetInstance(seq[index++]);
			}
			if (seq[index] is MetaData || seq[index] is Asn1Sequence)
			{
				this.metaData = MetaData.GetInstance(seq[index++]);
			}
			if (seq[index] is Asn1OctetString)
			{
				this.content = Asn1OctetString.GetInstance(seq[index++]);
			}
			this.temporalEvidence = Evidence.GetInstance(seq[index]);
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x00048B20 File Offset: 0x00048B20
		public static TimeStampedData GetInstance(object obj)
		{
			if (obj is TimeStampedData)
			{
				return (TimeStampedData)obj;
			}
			if (obj != null)
			{
				return new TimeStampedData(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000A7C RID: 2684 RVA: 0x00048B48 File Offset: 0x00048B48
		public virtual DerIA5String DataUri
		{
			get
			{
				return this.dataUri;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000A7D RID: 2685 RVA: 0x00048B50 File Offset: 0x00048B50
		public MetaData MetaData
		{
			get
			{
				return this.metaData;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000A7E RID: 2686 RVA: 0x00048B58 File Offset: 0x00048B58
		public Asn1OctetString Content
		{
			get
			{
				return this.content;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000A7F RID: 2687 RVA: 0x00048B60 File Offset: 0x00048B60
		public Evidence TemporalEvidence
		{
			get
			{
				return this.temporalEvidence;
			}
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x00048B68 File Offset: 0x00048B68
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.dataUri,
				this.metaData,
				this.content
			});
			asn1EncodableVector.Add(this.temporalEvidence);
			return new BerSequence(asn1EncodableVector);
		}

		// Token: 0x0400073B RID: 1851
		private DerInteger version;

		// Token: 0x0400073C RID: 1852
		private DerIA5String dataUri;

		// Token: 0x0400073D RID: 1853
		private MetaData metaData;

		// Token: 0x0400073E RID: 1854
		private Asn1OctetString content;

		// Token: 0x0400073F RID: 1855
		private Evidence temporalEvidence;
	}
}
