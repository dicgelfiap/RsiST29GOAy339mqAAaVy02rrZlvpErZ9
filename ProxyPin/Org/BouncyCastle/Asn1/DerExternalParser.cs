using System;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000269 RID: 617
	public class DerExternalParser : Asn1Encodable
	{
		// Token: 0x060013A3 RID: 5027 RVA: 0x0006B590 File Offset: 0x0006B590
		public DerExternalParser(Asn1StreamParser parser)
		{
			this._parser = parser;
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x0006B5A0 File Offset: 0x0006B5A0
		public IAsn1Convertible ReadObject()
		{
			return this._parser.ReadObject();
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x0006B5B0 File Offset: 0x0006B5B0
		public override Asn1Object ToAsn1Object()
		{
			return new DerExternal(this._parser.ReadVector());
		}

		// Token: 0x04000DAF RID: 3503
		private readonly Asn1StreamParser _parser;
	}
}
