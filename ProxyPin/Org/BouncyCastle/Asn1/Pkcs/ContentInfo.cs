using System;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020001A6 RID: 422
	public class ContentInfo : Asn1Encodable
	{
		// Token: 0x06000DD0 RID: 3536 RVA: 0x00055348 File Offset: 0x00055348
		public static ContentInfo GetInstance(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			ContentInfo contentInfo = obj as ContentInfo;
			if (contentInfo != null)
			{
				return contentInfo;
			}
			return new ContentInfo(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x0005537C File Offset: 0x0005537C
		private ContentInfo(Asn1Sequence seq)
		{
			this.contentType = (DerObjectIdentifier)seq[0];
			if (seq.Count > 1)
			{
				this.content = ((Asn1TaggedObject)seq[1]).GetObject();
			}
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x000553C8 File Offset: 0x000553C8
		public ContentInfo(DerObjectIdentifier contentType, Asn1Encodable content)
		{
			this.contentType = contentType;
			this.content = content;
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000DD3 RID: 3539 RVA: 0x000553E0 File Offset: 0x000553E0
		public DerObjectIdentifier ContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000DD4 RID: 3540 RVA: 0x000553E8 File Offset: 0x000553E8
		public Asn1Encodable Content
		{
			get
			{
				return this.content;
			}
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x000553F0 File Offset: 0x000553F0
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.contentType
			});
			if (this.content != null)
			{
				asn1EncodableVector.Add(new BerTaggedObject(0, this.content));
			}
			return new BerSequence(asn1EncodableVector);
		}

		// Token: 0x040009D7 RID: 2519
		private readonly DerObjectIdentifier contentType;

		// Token: 0x040009D8 RID: 2520
		private readonly Asn1Encodable content;
	}
}
