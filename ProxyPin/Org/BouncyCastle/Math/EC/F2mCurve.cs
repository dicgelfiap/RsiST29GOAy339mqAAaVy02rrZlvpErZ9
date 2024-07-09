using System;
using Org.BouncyCastle.Math.EC.Multiplier;

namespace Org.BouncyCastle.Math.EC
{
	// Token: 0x0200060D RID: 1549
	public class F2mCurve : AbstractF2mCurve
	{
		// Token: 0x0600345B RID: 13403 RVA: 0x00112B30 File Offset: 0x00112B30
		[Obsolete("Use constructor taking order/cofactor")]
		public F2mCurve(int m, int k, BigInteger a, BigInteger b) : this(m, k, 0, 0, a, b, null, null)
		{
		}

		// Token: 0x0600345C RID: 13404 RVA: 0x00112B50 File Offset: 0x00112B50
		public F2mCurve(int m, int k, BigInteger a, BigInteger b, BigInteger order, BigInteger cofactor) : this(m, k, 0, 0, a, b, order, cofactor)
		{
		}

		// Token: 0x0600345D RID: 13405 RVA: 0x00112B74 File Offset: 0x00112B74
		[Obsolete("Use constructor taking order/cofactor")]
		public F2mCurve(int m, int k1, int k2, int k3, BigInteger a, BigInteger b) : this(m, k1, k2, k3, a, b, null, null)
		{
		}

		// Token: 0x0600345E RID: 13406 RVA: 0x00112B98 File Offset: 0x00112B98
		public F2mCurve(int m, int k1, int k2, int k3, BigInteger a, BigInteger b, BigInteger order, BigInteger cofactor) : base(m, k1, k2, k3)
		{
			this.m = m;
			this.k1 = k1;
			this.k2 = k2;
			this.k3 = k3;
			this.m_order = order;
			this.m_cofactor = cofactor;
			this.m_infinity = new F2mPoint(this, null, null, false);
			if (k1 == 0)
			{
				throw new ArgumentException("k1 must be > 0");
			}
			if (k2 == 0)
			{
				if (k3 != 0)
				{
					throw new ArgumentException("k3 must be 0 if k2 == 0");
				}
			}
			else
			{
				if (k2 <= k1)
				{
					throw new ArgumentException("k2 must be > k1");
				}
				if (k3 <= k2)
				{
					throw new ArgumentException("k3 must be > k2");
				}
			}
			this.m_a = this.FromBigInteger(a);
			this.m_b = this.FromBigInteger(b);
			this.m_coord = 6;
		}

		// Token: 0x0600345F RID: 13407 RVA: 0x00112C64 File Offset: 0x00112C64
		protected F2mCurve(int m, int k1, int k2, int k3, ECFieldElement a, ECFieldElement b, BigInteger order, BigInteger cofactor) : base(m, k1, k2, k3)
		{
			this.m = m;
			this.k1 = k1;
			this.k2 = k2;
			this.k3 = k3;
			this.m_order = order;
			this.m_cofactor = cofactor;
			this.m_infinity = new F2mPoint(this, null, null, false);
			this.m_a = a;
			this.m_b = b;
			this.m_coord = 6;
		}

		// Token: 0x06003460 RID: 13408 RVA: 0x00112CD4 File Offset: 0x00112CD4
		protected override ECCurve CloneCurve()
		{
			return new F2mCurve(this.m, this.k1, this.k2, this.k3, this.m_a, this.m_b, this.m_order, this.m_cofactor);
		}

		// Token: 0x06003461 RID: 13409 RVA: 0x00112D0C File Offset: 0x00112D0C
		public override bool SupportsCoordinateSystem(int coord)
		{
			switch (coord)
			{
			case 0:
			case 1:
				break;
			default:
				if (coord != 6)
				{
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x06003462 RID: 13410 RVA: 0x00112D38 File Offset: 0x00112D38
		protected override ECMultiplier CreateDefaultMultiplier()
		{
			if (this.IsKoblitz)
			{
				return new WTauNafMultiplier();
			}
			return base.CreateDefaultMultiplier();
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06003463 RID: 13411 RVA: 0x00112D54 File Offset: 0x00112D54
		public override int FieldSize
		{
			get
			{
				return this.m;
			}
		}

		// Token: 0x06003464 RID: 13412 RVA: 0x00112D5C File Offset: 0x00112D5C
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new F2mFieldElement(this.m, this.k1, this.k2, this.k3, x);
		}

		// Token: 0x06003465 RID: 13413 RVA: 0x00112D7C File Offset: 0x00112D7C
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new F2mPoint(this, x, y, withCompression);
		}

		// Token: 0x06003466 RID: 13414 RVA: 0x00112D88 File Offset: 0x00112D88
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new F2mPoint(this, x, y, zs, withCompression);
		}

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06003467 RID: 13415 RVA: 0x00112D98 File Offset: 0x00112D98
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06003468 RID: 13416 RVA: 0x00112DA0 File Offset: 0x00112DA0
		public int M
		{
			get
			{
				return this.m;
			}
		}

		// Token: 0x06003469 RID: 13417 RVA: 0x00112DA8 File Offset: 0x00112DA8
		public bool IsTrinomial()
		{
			return this.k2 == 0 && this.k3 == 0;
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x0600346A RID: 13418 RVA: 0x00112DC0 File Offset: 0x00112DC0
		public int K1
		{
			get
			{
				return this.k1;
			}
		}

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x0600346B RID: 13419 RVA: 0x00112DC8 File Offset: 0x00112DC8
		public int K2
		{
			get
			{
				return this.k2;
			}
		}

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x0600346C RID: 13420 RVA: 0x00112DD0 File Offset: 0x00112DD0
		public int K3
		{
			get
			{
				return this.k3;
			}
		}

		// Token: 0x0600346D RID: 13421 RVA: 0x00112DD8 File Offset: 0x00112DD8
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			int num = (this.m + 63) / 64;
			long[] array = new long[len * num * 2];
			int num2 = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				((F2mFieldElement)ecpoint.RawXCoord).x.CopyTo(array, num2);
				num2 += num;
				((F2mFieldElement)ecpoint.RawYCoord).x.CopyTo(array, num2);
				num2 += num;
			}
			return new F2mCurve.DefaultF2mLookupTable(this, array, len);
		}

		// Token: 0x04001CFA RID: 7418
		private const int F2M_DEFAULT_COORDS = 6;

		// Token: 0x04001CFB RID: 7419
		private readonly int m;

		// Token: 0x04001CFC RID: 7420
		private readonly int k1;

		// Token: 0x04001CFD RID: 7421
		private readonly int k2;

		// Token: 0x04001CFE RID: 7422
		private readonly int k3;

		// Token: 0x04001CFF RID: 7423
		protected readonly F2mPoint m_infinity;

		// Token: 0x02000E58 RID: 3672
		private class DefaultF2mLookupTable : AbstractECLookupTable
		{
			// Token: 0x06008D32 RID: 36146 RVA: 0x002A5CA0 File Offset: 0x002A5CA0
			internal DefaultF2mLookupTable(F2mCurve outer, long[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17001DA1 RID: 7585
			// (get) Token: 0x06008D33 RID: 36147 RVA: 0x002A5CC0 File Offset: 0x002A5CC0
			public override int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06008D34 RID: 36148 RVA: 0x002A5CC8 File Offset: 0x002A5CC8
			public override ECPoint Lookup(int index)
			{
				int num = (this.m_outer.m + 63) / 64;
				long[] array = new long[num];
				long[] array2 = new long[num];
				int num2 = 0;
				for (int i = 0; i < this.m_size; i++)
				{
					long num3 = (long)((i ^ index) - 1 >> 31);
					for (int j = 0; j < num; j++)
					{
						long[] array3;
						IntPtr intPtr;
						(array3 = array)[(int)(intPtr = (IntPtr)j)] = (array3[(int)intPtr] ^ (this.m_table[num2 + j] & num3));
						(array3 = array2)[(int)(intPtr = (IntPtr)j)] = (array3[(int)intPtr] ^ (this.m_table[num2 + num + j] & num3));
					}
					num2 += num * 2;
				}
				return this.CreatePoint(array, array2);
			}

			// Token: 0x06008D35 RID: 36149 RVA: 0x002A5D80 File Offset: 0x002A5D80
			public override ECPoint LookupVar(int index)
			{
				int num = (this.m_outer.m + 63) / 64;
				long[] array = new long[num];
				long[] array2 = new long[num];
				int num2 = index * num * 2;
				for (int i = 0; i < num; i++)
				{
					array[i] = this.m_table[num2 + i];
					array2[i] = this.m_table[num2 + num + i];
				}
				return this.CreatePoint(array, array2);
			}

			// Token: 0x06008D36 RID: 36150 RVA: 0x002A5DF4 File Offset: 0x002A5DF4
			private ECPoint CreatePoint(long[] x, long[] y)
			{
				int m = this.m_outer.m;
				int[] ks = this.m_outer.IsTrinomial() ? new int[]
				{
					this.m_outer.k1
				} : new int[]
				{
					this.m_outer.k1,
					this.m_outer.k2,
					this.m_outer.k3
				};
				ECFieldElement x2 = new F2mFieldElement(m, ks, new LongArray(x));
				ECFieldElement y2 = new F2mFieldElement(m, ks, new LongArray(y));
				return this.m_outer.CreateRawPoint(x2, y2, false);
			}

			// Token: 0x04004225 RID: 16933
			private readonly F2mCurve m_outer;

			// Token: 0x04004226 RID: 16934
			private readonly long[] m_table;

			// Token: 0x04004227 RID: 16935
			private readonly int m_size;
		}
	}
}
