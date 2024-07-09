using System;
using System.IO;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000542 RID: 1346
	[Obsolete("Use 'TlsClientProtocol' instead")]
	public class TlsProtocolHandler : TlsClientProtocol
	{
		// Token: 0x0600295A RID: 10586 RVA: 0x000DDCDC File Offset: 0x000DDCDC
		public TlsProtocolHandler(Stream stream, SecureRandom secureRandom) : base(stream, stream, secureRandom)
		{
		}

		// Token: 0x0600295B RID: 10587 RVA: 0x000DDCE8 File Offset: 0x000DDCE8
		public TlsProtocolHandler(Stream input, Stream output, SecureRandom secureRandom) : base(input, output, secureRandom)
		{
		}
	}
}
