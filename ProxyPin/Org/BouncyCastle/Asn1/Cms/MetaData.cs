using System;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000113 RID: 275
	public class MetaData : Asn1Encodable
	{
		// Token: 0x060009D7 RID: 2519 RVA: 0x00046AEC File Offset: 0x00046AEC
		public MetaData(DerBoolean hashProtected, DerUtf8String fileName, DerIA5String mediaType, Attributes otherMetaData)
		{
			this.hashProtected = hashProtected;
			this.fileName = fileName;
			this.mediaType = mediaType;
			this.otherMetaData = otherMetaData;
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x00046B14 File Offset: 0x00046B14
		private MetaData(Asn1Sequence seq)
		{
			this.hashProtected = DerBoolean.GetInstance(seq[0]);
			int num = 1;
			if (num < seq.Count && seq[num] is DerUtf8String)
			{
				this.fileName = DerUtf8String.GetInstance(seq[num++]);
			}
			if (num < seq.Count && seq[num] is DerIA5String)
			{
				this.mediaType = DerIA5String.GetInstance(seq[num++]);
			}
			if (num < seq.Count)
			{
				this.otherMetaData = Attributes.GetInstance(seq[num++]);
			}
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x00046BC8 File Offset: 0x00046BC8
		public static MetaData GetInstance(object obj)
		{
			if (obj is MetaData)
			{
				return (MetaData)obj;
			}
			if (obj != null)
			{
				return new MetaData(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x00046BF0 File Offset: 0x00046BF0
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.hashProtected
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.fileName,
				this.mediaType,
				this.otherMetaData
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060009DB RID: 2523 RVA: 0x00046C58 File Offset: 0x00046C58
		public virtual bool IsHashProtected
		{
			get
			{
				return this.hashProtected.IsTrue;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060009DC RID: 2524 RVA: 0x00046C68 File Offset: 0x00046C68
		public virtual DerUtf8String FileName
		{
			get
			{
				return this.fileName;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060009DD RID: 2525 RVA: 0x00046C70 File Offset: 0x00046C70
		public virtual DerIA5String MediaType
		{
			get
			{
				return this.mediaType;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x00046C78 File Offset: 0x00046C78
		public virtual Attributes OtherMetaData
		{
			get
			{
				return this.otherMetaData;
			}
		}

		// Token: 0x04000703 RID: 1795
		private DerBoolean hashProtected;

		// Token: 0x04000704 RID: 1796
		private DerUtf8String fileName;

		// Token: 0x04000705 RID: 1797
		private DerIA5String mediaType;

		// Token: 0x04000706 RID: 1798
		private Attributes otherMetaData;
	}
}
