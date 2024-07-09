using System;

namespace Org.BouncyCastle.Math.EC.Multiplier
{
	// Token: 0x020005F3 RID: 1523
	[Obsolete("Will be removed")]
	public class DoubleAddMultiplier : AbstractECMultiplier
	{
		// Token: 0x06003317 RID: 13079 RVA: 0x001088AC File Offset: 0x001088AC
		protected override ECPoint MultiplyPositive(ECPoint p, BigInteger k)
		{
			ECPoint[] array = new ECPoint[]
			{
				p.Curve.Infinity,
				p
			};
			int bitLength = k.BitLength;
			for (int i = 0; i < bitLength; i++)
			{
				int num = k.TestBit(i) ? 1 : 0;
				int num2 = 1 - num;
				array[num2] = array[num2].TwicePlus(array[num]);
			}
			return array[0];
		}
	}
}
