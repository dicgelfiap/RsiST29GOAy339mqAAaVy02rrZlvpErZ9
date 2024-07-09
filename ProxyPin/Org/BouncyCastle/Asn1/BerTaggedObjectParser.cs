using System;
using System.IO;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000261 RID: 609
	public class BerTaggedObjectParser : Asn1TaggedObjectParser, IAsn1Convertible
	{
		// Token: 0x0600135C RID: 4956 RVA: 0x0006A7A0 File Offset: 0x0006A7A0
		[Obsolete]
		internal BerTaggedObjectParser(int baseTag, int tagNumber, Stream contentStream) : this((baseTag & 32) != 0, tagNumber, new Asn1StreamParser(contentStream))
		{
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x0006A7BC File Offset: 0x0006A7BC
		internal BerTaggedObjectParser(bool constructed, int tagNumber, Asn1StreamParser parser)
		{
			this._constructed = constructed;
			this._tagNumber = tagNumber;
			this._parser = parser;
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x0600135E RID: 4958 RVA: 0x0006A7DC File Offset: 0x0006A7DC
		public bool IsConstructed
		{
			get
			{
				return this._constructed;
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x0600135F RID: 4959 RVA: 0x0006A7E4 File Offset: 0x0006A7E4
		public int TagNo
		{
			get
			{
				return this._tagNumber;
			}
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x0006A7EC File Offset: 0x0006A7EC
		public IAsn1Convertible GetObjectParser(int tag, bool isExplicit)
		{
			if (!isExplicit)
			{
				return this._parser.ReadImplicit(this._constructed, tag);
			}
			if (!this._constructed)
			{
				throw new IOException("Explicit tags must be constructed (see X.690 8.14.2)");
			}
			return this._parser.ReadObject();
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x0006A828 File Offset: 0x0006A828
		public Asn1Object ToAsn1Object()
		{
			Asn1Object result;
			try
			{
				result = this._parser.ReadTaggedObject(this._constructed, this._tagNumber);
			}
			catch (IOException ex)
			{
				throw new Asn1ParsingException(ex.Message);
			}
			return result;
		}

		// Token: 0x04000D9A RID: 3482
		private bool _constructed;

		// Token: 0x04000D9B RID: 3483
		private int _tagNumber;

		// Token: 0x04000D9C RID: 3484
		private Asn1StreamParser _parser;
	}
}
