using System;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x0200024C RID: 588
	public class BerApplicationSpecificParser : IAsn1ApplicationSpecificParser, IAsn1Convertible
	{
		// Token: 0x060012E4 RID: 4836 RVA: 0x00069934 File Offset: 0x00069934
		internal BerApplicationSpecificParser(int tag, Asn1StreamParser parser)
		{
			this.tag = tag;
			this.parser = parser;
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x0006994C File Offset: 0x0006994C
		public IAsn1Convertible ReadObject()
		{
			return this.parser.ReadObject();
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x0006995C File Offset: 0x0006995C
		public Asn1Object ToAsn1Object()
		{
			return new BerApplicationSpecific(this.tag, this.parser.ReadVector());
		}

		// Token: 0x04000D87 RID: 3463
		private readonly int tag;

		// Token: 0x04000D88 RID: 3464
		private readonly Asn1StreamParser parser;
	}
}
