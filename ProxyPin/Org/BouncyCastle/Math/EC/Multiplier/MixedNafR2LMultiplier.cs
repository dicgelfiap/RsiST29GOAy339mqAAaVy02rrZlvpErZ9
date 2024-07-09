using System;

namespace Org.BouncyCastle.Math.EC.Multiplier
{
	// Token: 0x020005F8 RID: 1528
	[Obsolete("Will be removed")]
	public class MixedNafR2LMultiplier : AbstractECMultiplier
	{
		// Token: 0x06003329 RID: 13097 RVA: 0x00108BCC File Offset: 0x00108BCC
		public MixedNafR2LMultiplier() : this(2, 4)
		{
		}

		// Token: 0x0600332A RID: 13098 RVA: 0x00108BD8 File Offset: 0x00108BD8
		public MixedNafR2LMultiplier(int additionCoord, int doublingCoord)
		{
			this.additionCoord = additionCoord;
			this.doublingCoord = doublingCoord;
		}

		// Token: 0x0600332B RID: 13099 RVA: 0x00108BF0 File Offset: 0x00108BF0
		protected override ECPoint MultiplyPositive(ECPoint p, BigInteger k)
		{
			ECCurve curve = p.Curve;
			ECCurve eccurve = this.ConfigureCurve(curve, this.additionCoord);
			ECCurve eccurve2 = this.ConfigureCurve(curve, this.doublingCoord);
			int[] array = WNafUtilities.GenerateCompactNaf(k);
			ECPoint ecpoint = eccurve.Infinity;
			ECPoint ecpoint2 = eccurve2.ImportPoint(p);
			int num = 0;
			foreach (int num2 in array)
			{
				int num3 = num2 >> 16;
				num += (num2 & 65535);
				ecpoint2 = ecpoint2.TimesPow2(num);
				ECPoint ecpoint3 = eccurve.ImportPoint(ecpoint2);
				if (num3 < 0)
				{
					ecpoint3 = ecpoint3.Negate();
				}
				ecpoint = ecpoint.Add(ecpoint3);
				num = 1;
			}
			return curve.ImportPoint(ecpoint);
		}

		// Token: 0x0600332C RID: 13100 RVA: 0x00108CAC File Offset: 0x00108CAC
		protected virtual ECCurve ConfigureCurve(ECCurve c, int coord)
		{
			if (c.CoordinateSystem == coord)
			{
				return c;
			}
			if (!c.SupportsCoordinateSystem(coord))
			{
				throw new ArgumentException("Coordinate system " + coord + " not supported by this curve", "coord");
			}
			return c.Configure().SetCoordinateSystem(coord).Create();
		}

		// Token: 0x04001C8D RID: 7309
		protected readonly int additionCoord;

		// Token: 0x04001C8E RID: 7310
		protected readonly int doublingCoord;
	}
}
