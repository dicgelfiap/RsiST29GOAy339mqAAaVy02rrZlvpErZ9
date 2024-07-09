using System;

namespace Org.BouncyCastle.Math.EC.Multiplier
{
	// Token: 0x020005F9 RID: 1529
	[Obsolete("Will be removed")]
	public class MontgomeryLadderMultiplier : AbstractECMultiplier
	{
		// Token: 0x0600332D RID: 13101 RVA: 0x00108D08 File Offset: 0x00108D08
		protected override ECPoint MultiplyPositive(ECPoint p, BigInteger k)
		{
			ECPoint[] array = new ECPoint[]
			{
				p.Curve.Infinity,
				p
			};
			int bitLength = k.BitLength;
			int num = bitLength;
			while (--num >= 0)
			{
				int num2 = k.TestBit(num) ? 1 : 0;
				int num3 = 1 - num2;
				array[num3] = array[num3].Add(array[num2]);
				array[num2] = array[num2].Twice();
			}
			return array[0];
		}
	}
}
