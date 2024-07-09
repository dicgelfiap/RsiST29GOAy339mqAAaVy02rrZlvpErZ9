using System;

namespace Org.BouncyCastle.Math.Field
{
	// Token: 0x0200061E RID: 1566
	public interface IPolynomial
	{
		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x06003531 RID: 13617
		int Degree { get; }

		// Token: 0x06003532 RID: 13618
		int[] GetExponentsPresent();
	}
}
