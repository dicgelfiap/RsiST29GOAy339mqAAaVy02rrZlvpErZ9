using System;

namespace Org.BouncyCastle.Math.EC.Multiplier
{
	// Token: 0x020005F2 RID: 1522
	public abstract class AbstractECMultiplier : ECMultiplier
	{
		// Token: 0x06003313 RID: 13075 RVA: 0x0010883C File Offset: 0x0010883C
		public virtual ECPoint Multiply(ECPoint p, BigInteger k)
		{
			int signValue = k.SignValue;
			if (signValue == 0 || p.IsInfinity)
			{
				return p.Curve.Infinity;
			}
			ECPoint ecpoint = this.MultiplyPositive(p, k.Abs());
			ECPoint p2 = (signValue > 0) ? ecpoint : ecpoint.Negate();
			return this.CheckResult(p2);
		}

		// Token: 0x06003314 RID: 13076
		protected abstract ECPoint MultiplyPositive(ECPoint p, BigInteger k);

		// Token: 0x06003315 RID: 13077 RVA: 0x0010889C File Offset: 0x0010889C
		protected virtual ECPoint CheckResult(ECPoint p)
		{
			return ECAlgorithms.ImplCheckResult(p);
		}
	}
}
