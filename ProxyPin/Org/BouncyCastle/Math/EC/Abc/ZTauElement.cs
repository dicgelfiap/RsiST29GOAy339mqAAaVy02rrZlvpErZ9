using System;

namespace Org.BouncyCastle.Math.EC.Abc
{
	// Token: 0x0200056D RID: 1389
	internal class ZTauElement
	{
		// Token: 0x06002B35 RID: 11061 RVA: 0x000E6610 File Offset: 0x000E6610
		public ZTauElement(BigInteger u, BigInteger v)
		{
			this.u = u;
			this.v = v;
		}

		// Token: 0x04001B4C RID: 6988
		public readonly BigInteger u;

		// Token: 0x04001B4D RID: 6989
		public readonly BigInteger v;
	}
}
