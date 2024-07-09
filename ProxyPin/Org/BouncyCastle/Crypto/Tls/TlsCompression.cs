using System;
using System.IO;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200052E RID: 1326
	public interface TlsCompression
	{
		// Token: 0x06002879 RID: 10361
		Stream Compress(Stream output);

		// Token: 0x0600287A RID: 10362
		Stream Decompress(Stream output);
	}
}
