using System;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000274 RID: 628
	public class DerSetParser : Asn1SetParser, IAsn1Convertible
	{
		// Token: 0x060013F1 RID: 5105 RVA: 0x0006C3B0 File Offset: 0x0006C3B0
		internal DerSetParser(Asn1StreamParser parser)
		{
			this._parser = parser;
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x0006C3C0 File Offset: 0x0006C3C0
		public IAsn1Convertible ReadObject()
		{
			return this._parser.ReadObject();
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x0006C3D0 File Offset: 0x0006C3D0
		public Asn1Object ToAsn1Object()
		{
			return new DerSet(this._parser.ReadVector(), false);
		}

		// Token: 0x04000DBC RID: 3516
		private readonly Asn1StreamParser _parser;
	}
}
