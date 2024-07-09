using System;

namespace Org.BouncyCastle.Math.EC.Multiplier
{
	// Token: 0x020005FB RID: 1531
	[Obsolete("Will be removed")]
	public class NafR2LMultiplier : AbstractECMultiplier
	{
		// Token: 0x06003331 RID: 13105 RVA: 0x00108E38 File Offset: 0x00108E38
		protected override ECPoint MultiplyPositive(ECPoint p, BigInteger k)
		{
			int[] array = WNafUtilities.GenerateCompactNaf(k);
			ECPoint ecpoint = p.Curve.Infinity;
			ECPoint ecpoint2 = p;
			int num = 0;
			foreach (int num2 in array)
			{
				int num3 = num2 >> 16;
				num += (num2 & 65535);
				ecpoint2 = ecpoint2.TimesPow2(num);
				ecpoint = ecpoint.Add((num3 < 0) ? ecpoint2.Negate() : ecpoint2);
				num = 1;
			}
			return ecpoint;
		}
	}
}
