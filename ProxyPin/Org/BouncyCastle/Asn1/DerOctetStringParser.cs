using System;
using System.IO;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x0200026F RID: 623
	public class DerOctetStringParser : Asn1OctetStringParser, IAsn1Convertible
	{
		// Token: 0x060013D7 RID: 5079 RVA: 0x0006C05C File Offset: 0x0006C05C
		internal DerOctetStringParser(DefiniteLengthInputStream stream)
		{
			this.stream = stream;
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x0006C06C File Offset: 0x0006C06C
		public Stream GetOctetStream()
		{
			return this.stream;
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x0006C074 File Offset: 0x0006C074
		public Asn1Object ToAsn1Object()
		{
			Asn1Object result;
			try
			{
				result = new DerOctetString(this.stream.ToArray());
			}
			catch (IOException ex)
			{
				throw new InvalidOperationException("IOException converting stream to byte array: " + ex.Message, ex);
			}
			return result;
		}

		// Token: 0x04000DB7 RID: 3511
		private readonly DefiniteLengthInputStream stream;
	}
}
