using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x020005BB RID: 1467
	internal class SecT131R2Curve : AbstractF2mCurve
	{
		// Token: 0x06002FC9 RID: 12233 RVA: 0x000FA4CC File Offset: 0x000FA4CC
		public SecT131R2Curve() : base(131, 2, 3, 8)
		{
			this.m_infinity = new SecT131R2Point(this, null, null);
			this.m_a = this.FromBigInteger(new BigInteger(1, Hex.DecodeStrict("03E5A88919D7CAFCBF415F07C2176573B2")));
			this.m_b = this.FromBigInteger(new BigInteger(1, Hex.DecodeStrict("04B8266A46C55657AC734CE38F018F2192")));
			this.m_order = new BigInteger(1, Hex.DecodeStrict("0400000000000000016954A233049BA98F"));
			this.m_cofactor = BigInteger.Two;
			this.m_coord = 6;
		}

		// Token: 0x06002FCA RID: 12234 RVA: 0x000FA55C File Offset: 0x000FA55C
		protected override ECCurve CloneCurve()
		{
			return new SecT131R2Curve();
		}

		// Token: 0x06002FCB RID: 12235 RVA: 0x000FA564 File Offset: 0x000FA564
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 6;
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06002FCC RID: 12236 RVA: 0x000FA584 File Offset: 0x000FA584
		public override int FieldSize
		{
			get
			{
				return 131;
			}
		}

		// Token: 0x06002FCD RID: 12237 RVA: 0x000FA58C File Offset: 0x000FA58C
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecT131FieldElement(x);
		}

		// Token: 0x06002FCE RID: 12238 RVA: 0x000FA594 File Offset: 0x000FA594
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecT131R2Point(this, x, y, withCompression);
		}

		// Token: 0x06002FCF RID: 12239 RVA: 0x000FA5A0 File Offset: 0x000FA5A0
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecT131R2Point(this, x, y, zs, withCompression);
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06002FD0 RID: 12240 RVA: 0x000FA5B0 File Offset: 0x000FA5B0
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06002FD1 RID: 12241 RVA: 0x000FA5B8 File Offset: 0x000FA5B8
		public override bool IsKoblitz
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06002FD2 RID: 12242 RVA: 0x000FA5BC File Offset: 0x000FA5BC
		public virtual int M
		{
			get
			{
				return 131;
			}
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x06002FD3 RID: 12243 RVA: 0x000FA5C4 File Offset: 0x000FA5C4
		public virtual bool IsTrinomial
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06002FD4 RID: 12244 RVA: 0x000FA5C8 File Offset: 0x000FA5C8
		public virtual int K1
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06002FD5 RID: 12245 RVA: 0x000FA5CC File Offset: 0x000FA5CC
		public virtual int K2
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06002FD6 RID: 12246 RVA: 0x000FA5D0 File Offset: 0x000FA5D0
		public virtual int K3
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x06002FD7 RID: 12247 RVA: 0x000FA5D4 File Offset: 0x000FA5D4
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			ulong[] array = new ulong[len * 3 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat192.Copy64(((SecT131FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 3;
				Nat192.Copy64(((SecT131FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 3;
			}
			return new SecT131R2Curve.SecT131R2LookupTable(this, array, len);
		}

		// Token: 0x04001C1D RID: 7197
		private const int SECT131R2_DEFAULT_COORDS = 6;

		// Token: 0x04001C1E RID: 7198
		private const int SECT131R2_FE_LONGS = 3;

		// Token: 0x04001C1F RID: 7199
		private static readonly ECFieldElement[] SECT131R2_AFFINE_ZS = new ECFieldElement[]
		{
			new SecT131FieldElement(BigInteger.One)
		};

		// Token: 0x04001C20 RID: 7200
		protected readonly SecT131R2Point m_infinity;

		// Token: 0x02000E3B RID: 3643
		private class SecT131R2LookupTable : AbstractECLookupTable
		{
			// Token: 0x06008CCD RID: 36045 RVA: 0x002A3F70 File Offset: 0x002A3F70
			internal SecT131R2LookupTable(SecT131R2Curve outer, ulong[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17001D92 RID: 7570
			// (get) Token: 0x06008CCE RID: 36046 RVA: 0x002A3F90 File Offset: 0x002A3F90
			public override int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06008CCF RID: 36047 RVA: 0x002A3F98 File Offset: 0x002A3F98
			public override ECPoint Lookup(int index)
			{
				ulong[] array = Nat192.Create64();
				ulong[] array2 = Nat192.Create64();
				int num = 0;
				for (int i = 0; i < this.m_size; i++)
				{
					ulong num2 = (ulong)((long)((i ^ index) - 1 >> 31));
					for (int j = 0; j < 3; j++)
					{
						ulong[] array3;
						IntPtr intPtr;
						(array3 = array)[(int)(intPtr = (IntPtr)j)] = (array3[(int)intPtr] ^ (this.m_table[num + j] & num2));
						(array3 = array2)[(int)(intPtr = (IntPtr)j)] = (array3[(int)intPtr] ^ (this.m_table[num + 3 + j] & num2));
					}
					num += 6;
				}
				return this.CreatePoint(array, array2);
			}

			// Token: 0x06008CD0 RID: 36048 RVA: 0x002A4034 File Offset: 0x002A4034
			public override ECPoint LookupVar(int index)
			{
				ulong[] array = Nat192.Create64();
				ulong[] array2 = Nat192.Create64();
				int num = index * 3 * 2;
				for (int i = 0; i < 3; i++)
				{
					array[i] = this.m_table[num + i];
					array2[i] = this.m_table[num + 3 + i];
				}
				return this.CreatePoint(array, array2);
			}

			// Token: 0x06008CD1 RID: 36049 RVA: 0x002A408C File Offset: 0x002A408C
			private ECPoint CreatePoint(ulong[] x, ulong[] y)
			{
				return this.m_outer.CreateRawPoint(new SecT131FieldElement(x), new SecT131FieldElement(y), SecT131R2Curve.SECT131R2_AFFINE_ZS, false);
			}

			// Token: 0x040041CF RID: 16847
			private readonly SecT131R2Curve m_outer;

			// Token: 0x040041D0 RID: 16848
			private readonly ulong[] m_table;

			// Token: 0x040041D1 RID: 16849
			private readonly int m_size;
		}
	}
}
