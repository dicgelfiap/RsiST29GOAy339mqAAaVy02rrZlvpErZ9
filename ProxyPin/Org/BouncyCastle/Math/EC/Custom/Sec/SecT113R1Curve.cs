using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x020005B2 RID: 1458
	internal class SecT113R1Curve : AbstractF2mCurve
	{
		// Token: 0x06002F37 RID: 12087 RVA: 0x000F7724 File Offset: 0x000F7724
		public SecT113R1Curve() : base(113, 9, 0, 0)
		{
			this.m_infinity = new SecT113R1Point(this, null, null);
			this.m_a = this.FromBigInteger(new BigInteger(1, Hex.DecodeStrict("003088250CA6E7C7FE649CE85820F7")));
			this.m_b = this.FromBigInteger(new BigInteger(1, Hex.DecodeStrict("00E8BEE4D3E2260744188BE0E9C723")));
			this.m_order = new BigInteger(1, Hex.DecodeStrict("0100000000000000D9CCEC8A39E56F"));
			this.m_cofactor = BigInteger.Two;
			this.m_coord = 6;
		}

		// Token: 0x06002F38 RID: 12088 RVA: 0x000F77B0 File Offset: 0x000F77B0
		protected override ECCurve CloneCurve()
		{
			return new SecT113R1Curve();
		}

		// Token: 0x06002F39 RID: 12089 RVA: 0x000F77B8 File Offset: 0x000F77B8
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 6;
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x06002F3A RID: 12090 RVA: 0x000F77D8 File Offset: 0x000F77D8
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x06002F3B RID: 12091 RVA: 0x000F77E0 File Offset: 0x000F77E0
		public override int FieldSize
		{
			get
			{
				return 113;
			}
		}

		// Token: 0x06002F3C RID: 12092 RVA: 0x000F77E4 File Offset: 0x000F77E4
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecT113FieldElement(x);
		}

		// Token: 0x06002F3D RID: 12093 RVA: 0x000F77EC File Offset: 0x000F77EC
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecT113R1Point(this, x, y, withCompression);
		}

		// Token: 0x06002F3E RID: 12094 RVA: 0x000F77F8 File Offset: 0x000F77F8
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecT113R1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x06002F3F RID: 12095 RVA: 0x000F7808 File Offset: 0x000F7808
		public override bool IsKoblitz
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x06002F40 RID: 12096 RVA: 0x000F780C File Offset: 0x000F780C
		public virtual int M
		{
			get
			{
				return 113;
			}
		}

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x06002F41 RID: 12097 RVA: 0x000F7810 File Offset: 0x000F7810
		public virtual bool IsTrinomial
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06002F42 RID: 12098 RVA: 0x000F7814 File Offset: 0x000F7814
		public virtual int K1
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06002F43 RID: 12099 RVA: 0x000F7818 File Offset: 0x000F7818
		public virtual int K2
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06002F44 RID: 12100 RVA: 0x000F781C File Offset: 0x000F781C
		public virtual int K3
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06002F45 RID: 12101 RVA: 0x000F7820 File Offset: 0x000F7820
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			ulong[] array = new ulong[len * 2 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat128.Copy64(((SecT113FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 2;
				Nat128.Copy64(((SecT113FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 2;
			}
			return new SecT113R1Curve.SecT113R1LookupTable(this, array, len);
		}

		// Token: 0x04001C0D RID: 7181
		private const int SECT113R1_DEFAULT_COORDS = 6;

		// Token: 0x04001C0E RID: 7182
		private const int SECT113R1_FE_LONGS = 2;

		// Token: 0x04001C0F RID: 7183
		private static readonly ECFieldElement[] SECT113R1_AFFINE_ZS = new ECFieldElement[]
		{
			new SecT113FieldElement(BigInteger.One)
		};

		// Token: 0x04001C10 RID: 7184
		protected readonly SecT113R1Point m_infinity;

		// Token: 0x02000E38 RID: 3640
		private class SecT113R1LookupTable : AbstractECLookupTable
		{
			// Token: 0x06008CBE RID: 36030 RVA: 0x002A3BBC File Offset: 0x002A3BBC
			internal SecT113R1LookupTable(SecT113R1Curve outer, ulong[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17001D8F RID: 7567
			// (get) Token: 0x06008CBF RID: 36031 RVA: 0x002A3BDC File Offset: 0x002A3BDC
			public override int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06008CC0 RID: 36032 RVA: 0x002A3BE4 File Offset: 0x002A3BE4
			public override ECPoint Lookup(int index)
			{
				ulong[] array = Nat128.Create64();
				ulong[] array2 = Nat128.Create64();
				int num = 0;
				for (int i = 0; i < this.m_size; i++)
				{
					ulong num2 = (ulong)((long)((i ^ index) - 1 >> 31));
					for (int j = 0; j < 2; j++)
					{
						ulong[] array3;
						IntPtr intPtr;
						(array3 = array)[(int)(intPtr = (IntPtr)j)] = (array3[(int)intPtr] ^ (this.m_table[num + j] & num2));
						(array3 = array2)[(int)(intPtr = (IntPtr)j)] = (array3[(int)intPtr] ^ (this.m_table[num + 2 + j] & num2));
					}
					num += 4;
				}
				return this.CreatePoint(array, array2);
			}

			// Token: 0x06008CC1 RID: 36033 RVA: 0x002A3C80 File Offset: 0x002A3C80
			public override ECPoint LookupVar(int index)
			{
				ulong[] array = Nat128.Create64();
				ulong[] array2 = Nat128.Create64();
				int num = index * 2 * 2;
				for (int i = 0; i < 2; i++)
				{
					array[i] = this.m_table[num + i];
					array2[i] = this.m_table[num + 2 + i];
				}
				return this.CreatePoint(array, array2);
			}

			// Token: 0x06008CC2 RID: 36034 RVA: 0x002A3CD8 File Offset: 0x002A3CD8
			private ECPoint CreatePoint(ulong[] x, ulong[] y)
			{
				return this.m_outer.CreateRawPoint(new SecT113FieldElement(x), new SecT113FieldElement(y), SecT113R1Curve.SECT113R1_AFFINE_ZS, false);
			}

			// Token: 0x040041C6 RID: 16838
			private readonly SecT113R1Curve m_outer;

			// Token: 0x040041C7 RID: 16839
			private readonly ulong[] m_table;

			// Token: 0x040041C8 RID: 16840
			private readonly int m_size;
		}
	}
}
