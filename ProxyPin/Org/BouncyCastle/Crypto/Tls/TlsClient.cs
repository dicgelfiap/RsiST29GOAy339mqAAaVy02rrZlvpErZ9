using System;
using System.Collections;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004BB RID: 1211
	public interface TlsClient : TlsPeer
	{
		// Token: 0x06002539 RID: 9529
		void Init(TlsClientContext context);

		// Token: 0x0600253A RID: 9530
		TlsSession GetSessionToResume();

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x0600253B RID: 9531
		ProtocolVersion ClientHelloRecordLayerVersion { get; }

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x0600253C RID: 9532
		ProtocolVersion ClientVersion { get; }

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x0600253D RID: 9533
		bool IsFallback { get; }

		// Token: 0x0600253E RID: 9534
		int[] GetCipherSuites();

		// Token: 0x0600253F RID: 9535
		byte[] GetCompressionMethods();

		// Token: 0x06002540 RID: 9536
		IDictionary GetClientExtensions();

		// Token: 0x06002541 RID: 9537
		void NotifyServerVersion(ProtocolVersion selectedVersion);

		// Token: 0x06002542 RID: 9538
		void NotifySessionID(byte[] sessionID);

		// Token: 0x06002543 RID: 9539
		void NotifySelectedCipherSuite(int selectedCipherSuite);

		// Token: 0x06002544 RID: 9540
		void NotifySelectedCompressionMethod(byte selectedCompressionMethod);

		// Token: 0x06002545 RID: 9541
		void ProcessServerExtensions(IDictionary serverExtensions);

		// Token: 0x06002546 RID: 9542
		void ProcessServerSupplementalData(IList serverSupplementalData);

		// Token: 0x06002547 RID: 9543
		TlsKeyExchange GetKeyExchange();

		// Token: 0x06002548 RID: 9544
		TlsAuthentication GetAuthentication();

		// Token: 0x06002549 RID: 9545
		IList GetClientSupplementalData();

		// Token: 0x0600254A RID: 9546
		void NotifyNewSessionTicket(NewSessionTicket newSessionTicket);
	}
}
