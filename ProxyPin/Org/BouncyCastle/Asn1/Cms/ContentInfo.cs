using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000105 RID: 261
	public class ContentInfo : Asn1Encodable
	{
		// Token: 0x06000971 RID: 2417 RVA: 0x00045808 File Offset: 0x00045808
		public static ContentInfo GetInstance(object obj)
		{
			if (obj == null || obj is ContentInfo)
			{
				return (ContentInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new ContentInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj));
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x00045860 File Offset: 0x00045860
		public static ContentInfo GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return ContentInfo.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x00045870 File Offset: 0x00045870
		private ContentInfo(Asn1Sequence seq)
		{
			if (seq.Count < 1 || seq.Count > 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.contentType = (DerObjectIdentifier)seq[0];
			if (seq.Count > 1)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)seq[1];
				if (!asn1TaggedObject.IsExplicit() || asn1TaggedObject.TagNo != 0)
				{
					throw new ArgumentException("Bad tag for 'content'", "seq");
				}
				this.content = asn1TaggedObject.GetObject();
			}
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x0004591C File Offset: 0x0004591C
		public ContentInfo(DerObjectIdentifier contentType, Asn1Encodable content)
		{
			this.contentType = contentType;
			this.content = content;
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x00045934 File Offset: 0x00045934
		public DerObjectIdentifier ContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000976 RID: 2422 RVA: 0x0004593C File Offset: 0x0004593C
		public Asn1Encodable Content
		{
			get
			{
				return this.content;
			}
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x00045944 File Offset: 0x00045944
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

		// Token: 0x040006D7 RID: 1751
		private readonly DerObjectIdentifier contentType;

		// Token: 0x040006D8 RID: 1752
		private readonly Asn1Encodable content;
	}
}
