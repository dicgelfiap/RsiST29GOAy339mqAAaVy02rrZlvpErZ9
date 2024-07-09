using System;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000106 RID: 262
	public class ContentInfoParser
	{
		// Token: 0x06000978 RID: 2424 RVA: 0x00045994 File Offset: 0x00045994
		public ContentInfoParser(Asn1SequenceParser seq)
		{
			this.contentType = (DerObjectIdentifier)seq.ReadObject();
			this.content = (Asn1TaggedObjectParser)seq.ReadObject();
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x000459C0 File Offset: 0x000459C0
		public DerObjectIdentifier ContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x000459C8 File Offset: 0x000459C8
		public IAsn1Convertible GetContent(int tag)
		{
			if (this.content == null)
			{
				return null;
			}
			return this.content.GetObjectParser(tag, true);
		}

		// Token: 0x040006D9 RID: 1753
		private DerObjectIdentifier contentType;

		// Token: 0x040006DA RID: 1754
		private Asn1TaggedObjectParser content;
	}
}
