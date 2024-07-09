using System;

namespace Org.BouncyCastle.Math.Field
{
	// Token: 0x0200061A RID: 1562
	public interface IFiniteField
	{
		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x06003524 RID: 13604
		BigInteger Characteristic { get; }

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x06003525 RID: 13605
		int Dimension { get; }
	}
}
