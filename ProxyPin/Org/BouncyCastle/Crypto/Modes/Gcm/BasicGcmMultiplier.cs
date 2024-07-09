using System;

namespace Org.BouncyCastle.Crypto.Modes.Gcm
{
	// Token: 0x020003F3 RID: 1011
	public class BasicGcmMultiplier : IGcmMultiplier
	{
		// Token: 0x0600200C RID: 8204 RVA: 0x000BAB68 File Offset: 0x000BAB68
		public void Init(byte[] H)
		{
			this.H = GcmUtilities.AsUints(H);
		}

		// Token: 0x0600200D RID: 8205 RVA: 0x000BAB78 File Offset: 0x000BAB78
		public void MultiplyH(byte[] x)
		{
			uint[] x2 = GcmUtilities.AsUints(x);
			GcmUtilities.Multiply(x2, this.H);
			GcmUtilities.AsBytes(x2, x);
		}

		// Token: 0x04001509 RID: 5385
		private uint[] H;
	}
}
