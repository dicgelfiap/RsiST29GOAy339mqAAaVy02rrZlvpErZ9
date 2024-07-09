using System;

namespace Org.BouncyCastle.Math.EC.Multiplier
{
	// Token: 0x02000604 RID: 1540
	[Obsolete("Will be removed")]
	public class ZSignedDigitR2LMultiplier : AbstractECMultiplier
	{
		// Token: 0x0600336D RID: 13165 RVA: 0x00109C08 File Offset: 0x00109C08
		protected override ECPoint MultiplyPositive(ECPoint p, BigInteger k)
		{
			ECPoint ecpoint = p.Curve.Infinity;
			int bitLength = k.BitLength;
			int lowestSetBit = k.GetLowestSetBit();
			ECPoint ecpoint2 = p.TimesPow2(lowestSetBit);
			int num = lowestSetBit;
			while (++num < bitLength)
			{
				ecpoint = ecpoint.Add(k.TestBit(num) ? ecpoint2 : ecpoint2.Negate());
				ecpoint2 = ecpoint2.Twice();
			}
			return ecpoint.Add(ecpoint2);
		}
	}
}
