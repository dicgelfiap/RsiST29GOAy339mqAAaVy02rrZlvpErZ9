using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004E5 RID: 1253
	public interface DatagramTransport : TlsCloseable
	{
		// Token: 0x06002673 RID: 9843
		int GetReceiveLimit();

		// Token: 0x06002674 RID: 9844
		int GetSendLimit();

		// Token: 0x06002675 RID: 9845
		int Receive(byte[] buf, int off, int len, int waitMillis);

		// Token: 0x06002676 RID: 9846
		void Send(byte[] buf, int off, int len);
	}
}
