using System;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000272 RID: 626
	public class DerSequenceParser : Asn1SequenceParser, IAsn1Convertible
	{
		// Token: 0x060013E9 RID: 5097 RVA: 0x0006C31C File Offset: 0x0006C31C
		internal DerSequenceParser(Asn1StreamParser parser)
		{
			this._parser = parser;
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x0006C32C File Offset: 0x0006C32C
		public IAsn1Convertible ReadObject()
		{
			return this._parser.ReadObject();
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x0006C33C File Offset: 0x0006C33C
		public Asn1Object ToAsn1Object()
		{
			return new DerSequence(this._parser.ReadVector());
		}

		// Token: 0x04000DBA RID: 3514
		private readonly Asn1StreamParser _parser;
	}
}
