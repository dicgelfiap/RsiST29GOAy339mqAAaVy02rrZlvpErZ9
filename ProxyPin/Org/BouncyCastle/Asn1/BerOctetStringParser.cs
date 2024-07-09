using System;
using System.IO;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000255 RID: 597
	public class BerOctetStringParser : Asn1OctetStringParser, IAsn1Convertible
	{
		// Token: 0x06001326 RID: 4902 RVA: 0x00069FE0 File Offset: 0x00069FE0
		internal BerOctetStringParser(Asn1StreamParser parser)
		{
			this._parser = parser;
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x00069FF0 File Offset: 0x00069FF0
		public Stream GetOctetStream()
		{
			return new ConstructedOctetStream(this._parser);
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x0006A000 File Offset: 0x0006A000
		public Asn1Object ToAsn1Object()
		{
			Asn1Object result;
			try
			{
				result = new BerOctetString(Streams.ReadAll(this.GetOctetStream()));
			}
			catch (IOException ex)
			{
				throw new Asn1ParsingException("IOException converting stream to byte array: " + ex.Message, ex);
			}
			return result;
		}

		// Token: 0x04000D93 RID: 3475
		private readonly Asn1StreamParser _parser;
	}
}
