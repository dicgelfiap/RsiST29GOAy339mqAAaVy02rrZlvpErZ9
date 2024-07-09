using System;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x0200025A RID: 602
	public class BerSequenceParser : Asn1SequenceParser, IAsn1Convertible
	{
		// Token: 0x0600133B RID: 4923 RVA: 0x0006A2AC File Offset: 0x0006A2AC
		internal BerSequenceParser(Asn1StreamParser parser)
		{
			this._parser = parser;
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x0006A2BC File Offset: 0x0006A2BC
		public IAsn1Convertible ReadObject()
		{
			return this._parser.ReadObject();
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x0006A2CC File Offset: 0x0006A2CC
		public Asn1Object ToAsn1Object()
		{
			return new BerSequence(this._parser.ReadVector());
		}

		// Token: 0x04000D96 RID: 3478
		private readonly Asn1StreamParser _parser;
	}
}
