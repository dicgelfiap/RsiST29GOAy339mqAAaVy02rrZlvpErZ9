using System;
using System.IO;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000541 RID: 1345
	public class TlsNullCompression : TlsCompression
	{
		// Token: 0x06002957 RID: 10583 RVA: 0x000DDCCC File Offset: 0x000DDCCC
		public virtual Stream Compress(Stream output)
		{
			return output;
		}

		// Token: 0x06002958 RID: 10584 RVA: 0x000DDCD0 File Offset: 0x000DDCD0
		public virtual Stream Decompress(Stream output)
		{
			return output;
		}
	}
}
