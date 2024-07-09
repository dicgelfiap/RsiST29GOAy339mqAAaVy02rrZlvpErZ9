using System;

namespace Org.BouncyCastle.Crypto.Modes.Gcm
{
	// Token: 0x020003F2 RID: 1010
	public interface IGcmMultiplier
	{
		// Token: 0x0600200A RID: 8202
		void Init(byte[] H);

		// Token: 0x0600200B RID: 8203
		void MultiplyH(byte[] x);
	}
}
