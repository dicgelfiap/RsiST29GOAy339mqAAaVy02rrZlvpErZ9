using System;

namespace Org.BouncyCastle.Math.EC.Multiplier
{
	// Token: 0x020005FC RID: 1532
	[Obsolete("Will be removed")]
	public class ReferenceMultiplier : AbstractECMultiplier
	{
		// Token: 0x06003333 RID: 13107 RVA: 0x00108EBC File Offset: 0x00108EBC
		protected override ECPoint MultiplyPositive(ECPoint p, BigInteger k)
		{
			return ECAlgorithms.ReferenceMultiply(p, k);
		}
	}
}
