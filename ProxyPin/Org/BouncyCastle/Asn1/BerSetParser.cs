using System;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x0200025E RID: 606
	public class BerSetParser : Asn1SetParser, IAsn1Convertible
	{
		// Token: 0x06001351 RID: 4945 RVA: 0x0006A528 File Offset: 0x0006A528
		internal BerSetParser(Asn1StreamParser parser)
		{
			this._parser = parser;
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x0006A538 File Offset: 0x0006A538
		public IAsn1Convertible ReadObject()
		{
			return this._parser.ReadObject();
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x0006A548 File Offset: 0x0006A548
		public Asn1Object ToAsn1Object()
		{
			return new BerSet(this._parser.ReadVector(), false);
		}

		// Token: 0x04000D99 RID: 3481
		private readonly Asn1StreamParser _parser;
	}
}
