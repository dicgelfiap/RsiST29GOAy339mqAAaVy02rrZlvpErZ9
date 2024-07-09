using System;

namespace Org.BouncyCastle.Crypto.Modes.Gcm
{
	// Token: 0x020003F0 RID: 1008
	public interface IGcmExponentiator
	{
		// Token: 0x06002005 RID: 8197
		void Init(byte[] x);

		// Token: 0x06002006 RID: 8198
		void ExponentiateX(long pow, byte[] output);
	}
}
