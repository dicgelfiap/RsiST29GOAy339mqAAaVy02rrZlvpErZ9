using System;

namespace Org.BouncyCastle.Math.EC.Multiplier
{
	// Token: 0x020005F6 RID: 1526
	public class FixedPointUtilities
	{
		// Token: 0x06003322 RID: 13090 RVA: 0x00108A94 File Offset: 0x00108A94
		public static int GetCombSize(ECCurve c)
		{
			BigInteger order = c.Order;
			if (order != null)
			{
				return order.BitLength;
			}
			return c.FieldSize + 1;
		}

		// Token: 0x06003323 RID: 13091 RVA: 0x00108AC4 File Offset: 0x00108AC4
		public static FixedPointPreCompInfo GetFixedPointPreCompInfo(PreCompInfo preCompInfo)
		{
			return preCompInfo as FixedPointPreCompInfo;
		}

		// Token: 0x06003324 RID: 13092 RVA: 0x00108ACC File Offset: 0x00108ACC
		public static FixedPointPreCompInfo Precompute(ECPoint p)
		{
			return (FixedPointPreCompInfo)p.Curve.Precompute(p, FixedPointUtilities.PRECOMP_NAME, new FixedPointUtilities.FixedPointCallback(p));
		}

		// Token: 0x04001C8A RID: 7306
		public static readonly string PRECOMP_NAME = "bc_fixed_point";

		// Token: 0x02000E4B RID: 3659
		private class FixedPointCallback : IPreCompCallback
		{
			// Token: 0x06008D1B RID: 36123 RVA: 0x002A52A0 File Offset: 0x002A52A0
			internal FixedPointCallback(ECPoint p)
			{
				this.m_p = p;
			}

			// Token: 0x06008D1C RID: 36124 RVA: 0x002A52B0 File Offset: 0x002A52B0
			public PreCompInfo Precompute(PreCompInfo existing)
			{
				FixedPointPreCompInfo fixedPointPreCompInfo = (existing is FixedPointPreCompInfo) ? ((FixedPointPreCompInfo)existing) : null;
				ECCurve curve = this.m_p.Curve;
				int combSize = FixedPointUtilities.GetCombSize(curve);
				int num = (combSize > 250) ? 6 : 5;
				int num2 = 1 << num;
				if (this.CheckExisting(fixedPointPreCompInfo, num2))
				{
					return fixedPointPreCompInfo;
				}
				int e = (combSize + num - 1) / num;
				ECPoint[] array = new ECPoint[num + 1];
				array[0] = this.m_p;
				for (int i = 1; i < num; i++)
				{
					array[i] = array[i - 1].TimesPow2(e);
				}
				array[num] = array[0].Subtract(array[1]);
				curve.NormalizeAll(array);
				ECPoint[] array2 = new ECPoint[num2];
				array2[0] = array[0];
				for (int j = num - 1; j >= 0; j--)
				{
					ECPoint b = array[j];
					int num3 = 1 << j;
					for (int k = num3; k < num2; k += num3 << 1)
					{
						array2[k] = array2[k - num3].Add(b);
					}
				}
				curve.NormalizeAll(array2);
				return new FixedPointPreCompInfo
				{
					LookupTable = curve.CreateCacheSafeLookupTable(array2, 0, array2.Length),
					Offset = array[num],
					Width = num
				};
			}

			// Token: 0x06008D1D RID: 36125 RVA: 0x002A5440 File Offset: 0x002A5440
			private bool CheckExisting(FixedPointPreCompInfo existingFP, int n)
			{
				return existingFP != null && this.CheckTable(existingFP.LookupTable, n);
			}

			// Token: 0x06008D1E RID: 36126 RVA: 0x002A5458 File Offset: 0x002A5458
			private bool CheckTable(ECLookupTable table, int n)
			{
				return table != null && table.Size >= n;
			}

			// Token: 0x040041FE RID: 16894
			private readonly ECPoint m_p;
		}
	}
}
