using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Math.EC.Multiplier
{
	// Token: 0x020005FE RID: 1534
	public class WNafL2RMultiplier : AbstractECMultiplier
	{
		// Token: 0x0600333D RID: 13117 RVA: 0x00108F38 File Offset: 0x00108F38
		protected override ECPoint MultiplyPositive(ECPoint p, BigInteger k)
		{
			int windowSize = WNafUtilities.GetWindowSize(k.BitLength);
			WNafPreCompInfo wnafPreCompInfo = WNafUtilities.Precompute(p, windowSize, true);
			ECPoint[] preComp = wnafPreCompInfo.PreComp;
			ECPoint[] preCompNeg = wnafPreCompInfo.PreCompNeg;
			int width = wnafPreCompInfo.Width;
			int[] array = WNafUtilities.GenerateCompactWindowNaf(width, k);
			ECPoint ecpoint = p.Curve.Infinity;
			int i = array.Length;
			if (i > 1)
			{
				int num = array[--i];
				int num2 = num >> 16;
				int num3 = num & 65535;
				int num4 = Math.Abs(num2);
				ECPoint[] array2 = (num2 < 0) ? preCompNeg : preComp;
				if (num4 << 2 < 1 << width)
				{
					int num5 = 32 - Integers.NumberOfLeadingZeros(num4);
					int num6 = width - num5;
					int num7 = num4 ^ 1 << num5 - 1;
					int num8 = (1 << width - 1) - 1;
					int num9 = (num7 << num6) + 1;
					ecpoint = array2[num8 >> 1].Add(array2[num9 >> 1]);
					num3 -= num6;
				}
				else
				{
					ecpoint = array2[num4 >> 1];
				}
				ecpoint = ecpoint.TimesPow2(num3);
			}
			while (i > 0)
			{
				int num10 = array[--i];
				int num11 = num10 >> 16;
				int e = num10 & 65535;
				int num12 = Math.Abs(num11);
				ECPoint[] array3 = (num11 < 0) ? preCompNeg : preComp;
				ECPoint b = array3[num12 >> 1];
				ecpoint = ecpoint.TwicePlus(b);
				ecpoint = ecpoint.TimesPow2(e);
			}
			return ecpoint;
		}
	}
}
