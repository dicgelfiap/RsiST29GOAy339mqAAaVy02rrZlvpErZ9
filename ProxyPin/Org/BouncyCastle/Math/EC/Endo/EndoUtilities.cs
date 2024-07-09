using System;
using Org.BouncyCastle.Math.EC.Multiplier;

namespace Org.BouncyCastle.Math.EC.Endo
{
	// Token: 0x020005EA RID: 1514
	public abstract class EndoUtilities
	{
		// Token: 0x060032ED RID: 13037 RVA: 0x00108434 File Offset: 0x00108434
		public static BigInteger[] DecomposeScalar(ScalarSplitParameters p, BigInteger k)
		{
			int bits = p.Bits;
			BigInteger bigInteger = EndoUtilities.CalculateB(k, p.G1, bits);
			BigInteger bigInteger2 = EndoUtilities.CalculateB(k, p.G2, bits);
			BigInteger bigInteger3 = k.Subtract(bigInteger.Multiply(p.V1A).Add(bigInteger2.Multiply(p.V2A)));
			BigInteger bigInteger4 = bigInteger.Multiply(p.V1B).Add(bigInteger2.Multiply(p.V2B)).Negate();
			return new BigInteger[]
			{
				bigInteger3,
				bigInteger4
			};
		}

		// Token: 0x060032EE RID: 13038 RVA: 0x001084D0 File Offset: 0x001084D0
		public static ECPoint MapPoint(ECEndomorphism endomorphism, ECPoint p)
		{
			EndoPreCompInfo endoPreCompInfo = (EndoPreCompInfo)p.Curve.Precompute(p, EndoUtilities.PRECOMP_NAME, new EndoUtilities.MapPointCallback(endomorphism, p));
			return endoPreCompInfo.MappedPoint;
		}

		// Token: 0x060032EF RID: 13039 RVA: 0x00108508 File Offset: 0x00108508
		private static BigInteger CalculateB(BigInteger k, BigInteger g, int t)
		{
			bool flag = g.SignValue < 0;
			BigInteger bigInteger = k.Multiply(g.Abs());
			bool flag2 = bigInteger.TestBit(t - 1);
			bigInteger = bigInteger.ShiftRight(t);
			if (flag2)
			{
				bigInteger = bigInteger.Add(BigInteger.One);
			}
			if (!flag)
			{
				return bigInteger;
			}
			return bigInteger.Negate();
		}

		// Token: 0x04001C75 RID: 7285
		public static readonly string PRECOMP_NAME = "bc_endo";

		// Token: 0x02000E4A RID: 3658
		private class MapPointCallback : IPreCompCallback
		{
			// Token: 0x06008D18 RID: 36120 RVA: 0x002A5204 File Offset: 0x002A5204
			internal MapPointCallback(ECEndomorphism endomorphism, ECPoint point)
			{
				this.m_endomorphism = endomorphism;
				this.m_point = point;
			}

			// Token: 0x06008D19 RID: 36121 RVA: 0x002A521C File Offset: 0x002A521C
			public PreCompInfo Precompute(PreCompInfo existing)
			{
				EndoPreCompInfo endoPreCompInfo = existing as EndoPreCompInfo;
				if (this.CheckExisting(endoPreCompInfo, this.m_endomorphism))
				{
					return endoPreCompInfo;
				}
				ECPoint mappedPoint = this.m_endomorphism.PointMap.Map(this.m_point);
				return new EndoPreCompInfo
				{
					Endomorphism = this.m_endomorphism,
					MappedPoint = mappedPoint
				};
			}

			// Token: 0x06008D1A RID: 36122 RVA: 0x002A527C File Offset: 0x002A527C
			private bool CheckExisting(EndoPreCompInfo existingEndo, ECEndomorphism endomorphism)
			{
				return existingEndo != null && existingEndo.Endomorphism == endomorphism && existingEndo.MappedPoint != null;
			}

			// Token: 0x040041FC RID: 16892
			private readonly ECEndomorphism m_endomorphism;

			// Token: 0x040041FD RID: 16893
			private readonly ECPoint m_point;
		}
	}
}
