using System;

namespace Org.BouncyCastle.Math.EC.Multiplier
{
	// Token: 0x020005FA RID: 1530
	[Obsolete("Will be removed")]
	public class NafL2RMultiplier : AbstractECMultiplier
	{
		// Token: 0x0600332F RID: 13103 RVA: 0x00108DB0 File Offset: 0x00108DB0
		protected override ECPoint MultiplyPositive(ECPoint p, BigInteger k)
		{
			int[] array = WNafUtilities.GenerateCompactNaf(k);
			ECPoint ecpoint = p.Normalize();
			ECPoint ecpoint2 = ecpoint.Negate();
			ECPoint ecpoint3 = p.Curve.Infinity;
			int num = array.Length;
			while (--num >= 0)
			{
				int num2 = array[num];
				int num3 = num2 >> 16;
				int e = num2 & 65535;
				ecpoint3 = ecpoint3.TwicePlus((num3 < 0) ? ecpoint2 : ecpoint);
				ecpoint3 = ecpoint3.TimesPow2(e);
			}
			return ecpoint3;
		}
	}
}
