using System;

namespace Org.BouncyCastle.Math.EC.Multiplier
{
	// Token: 0x02000603 RID: 1539
	[Obsolete("Will be removed")]
	public class ZSignedDigitL2RMultiplier : AbstractECMultiplier
	{
		// Token: 0x0600336B RID: 13163 RVA: 0x00109B98 File Offset: 0x00109B98
		protected override ECPoint MultiplyPositive(ECPoint p, BigInteger k)
		{
			ECPoint ecpoint = p.Normalize();
			ECPoint ecpoint2 = ecpoint.Negate();
			ECPoint ecpoint3 = ecpoint;
			int bitLength = k.BitLength;
			int lowestSetBit = k.GetLowestSetBit();
			int num = bitLength;
			while (--num > lowestSetBit)
			{
				ecpoint3 = ecpoint3.TwicePlus(k.TestBit(num) ? ecpoint : ecpoint2);
			}
			return ecpoint3.TimesPow2(lowestSetBit);
		}
	}
}
