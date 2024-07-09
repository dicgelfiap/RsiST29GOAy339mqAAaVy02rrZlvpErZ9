using System;
using System.IO;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x0200027C RID: 636
	public class LazyAsn1InputStream : Asn1InputStream
	{
		// Token: 0x06001432 RID: 5170 RVA: 0x0006CEC4 File Offset: 0x0006CEC4
		public LazyAsn1InputStream(byte[] input) : base(input)
		{
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x0006CED0 File Offset: 0x0006CED0
		public LazyAsn1InputStream(Stream inputStream) : base(inputStream)
		{
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x0006CEDC File Offset: 0x0006CEDC
		internal override DerSequence CreateDerSequence(DefiniteLengthInputStream dIn)
		{
			return new LazyDerSequence(dIn.ToArray());
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x0006CEEC File Offset: 0x0006CEEC
		internal override DerSet CreateDerSet(DefiniteLengthInputStream dIn)
		{
			return new LazyDerSet(dIn.ToArray());
		}
	}
}
