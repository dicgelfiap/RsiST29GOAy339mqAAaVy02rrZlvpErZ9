using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004F6 RID: 1270
	internal interface DtlsHandshakeRetransmit
	{
		// Token: 0x060026FF RID: 9983
		void ReceivedHandshakeRecord(int epoch, byte[] buf, int off, int len);
	}
}
