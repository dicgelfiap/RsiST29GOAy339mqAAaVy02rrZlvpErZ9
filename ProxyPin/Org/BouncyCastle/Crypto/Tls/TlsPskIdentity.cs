using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004CB RID: 1227
	public interface TlsPskIdentity
	{
		// Token: 0x060025F1 RID: 9713
		void SkipIdentityHint();

		// Token: 0x060025F2 RID: 9714
		void NotifyIdentityHint(byte[] psk_identity_hint);

		// Token: 0x060025F3 RID: 9715
		byte[] GetPskIdentity();

		// Token: 0x060025F4 RID: 9716
		byte[] GetPsk();
	}
}
