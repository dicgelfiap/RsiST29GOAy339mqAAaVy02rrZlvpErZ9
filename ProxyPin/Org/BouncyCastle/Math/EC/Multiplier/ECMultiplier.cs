using System;

namespace Org.BouncyCastle.Math.EC.Multiplier
{
	// Token: 0x020005F1 RID: 1521
	public interface ECMultiplier
	{
		// Token: 0x06003312 RID: 13074
		ECPoint Multiply(ECPoint p, BigInteger k);
	}
}
