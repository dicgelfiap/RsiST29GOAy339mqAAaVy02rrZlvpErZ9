using System;
using Org.BouncyCastle.Crypto.Prng;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004BD RID: 1213
	public interface TlsContext
	{
		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06002564 RID: 9572
		IRandomGenerator NonceRandomGenerator { get; }

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06002565 RID: 9573
		SecureRandom SecureRandom { get; }

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06002566 RID: 9574
		SecurityParameters SecurityParameters { get; }

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06002567 RID: 9575
		bool IsServer { get; }

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06002568 RID: 9576
		ProtocolVersion ClientVersion { get; }

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06002569 RID: 9577
		ProtocolVersion ServerVersion { get; }

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x0600256A RID: 9578
		TlsSession ResumableSession { get; }

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x0600256B RID: 9579
		// (set) Token: 0x0600256C RID: 9580
		object UserObject { get; set; }

		// Token: 0x0600256D RID: 9581
		byte[] ExportKeyingMaterial(string asciiLabel, byte[] context_value, int length);
	}
}
