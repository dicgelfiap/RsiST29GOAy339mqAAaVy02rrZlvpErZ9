using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x020005C1 RID: 1473
	internal class SecT163R1Curve : AbstractF2mCurve
	{
		// Token: 0x06003037 RID: 12343 RVA: 0x000FC31C File Offset: 0x000FC31C
		public SecT163R1Curve() : base(163, 3, 6, 7)
		{
			this.m_infinity = new SecT163R1Point(this, null, null);
			this.m_a = this.FromBigInteger(new BigInteger(1, Hex.DecodeStrict("07B6882CAAEFA84F9554FF8428BD88E246D2782AE2")));
			this.m_b = this.FromBigInteger(new BigInteger(1, Hex.DecodeStrict("0713612DCDDCB40AAB946BDA29CA91F73AF958AFD9")));
			this.m_order = new BigInteger(1, Hex.DecodeStrict("03FFFFFFFFFFFFFFFFFFFF48AAB689C29CA710279B"));
			this.m_cofactor = BigInteger.Two;
			this.m_coord = 6;
		}

		// Token: 0x06003038 RID: 12344 RVA: 0x000FC3AC File Offset: 0x000FC3AC
		protected override ECCurve CloneCurve()
		{
			return new SecT163R1Curve();
		}

		// Token: 0x06003039 RID: 12345 RVA: 0x000FC3B4 File Offset: 0x000FC3B4
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 6;
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x0600303A RID: 12346 RVA: 0x000FC3D4 File Offset: 0x000FC3D4
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x0600303B RID: 12347 RVA: 0x000FC3DC File Offset: 0x000FC3DC
		public override int FieldSize
		{
			get
			{
				return 163;
			}
		}

		// Token: 0x0600303C RID: 12348 RVA: 0x000FC3E4 File Offset: 0x000FC3E4
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecT163FieldElement(x);
		}

		// Token: 0x0600303D RID: 12349 RVA: 0x000FC3EC File Offset: 0x000FC3EC
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecT163R1Point(this, x, y, withCompression);
		}

		// Token: 0x0600303E RID: 12350 RVA: 0x000FC3F8 File Offset: 0x000FC3F8
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecT163R1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x0600303F RID: 12351 RVA: 0x000FC408 File Offset: 0x000FC408
		public override bool IsKoblitz
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06003040 RID: 12352 RVA: 0x000FC40C File Offset: 0x000FC40C
		public virtual int M
		{
			get
			{
				return 163;
			}
		}

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06003041 RID: 12353 RVA: 0x000FC414 File Offset: 0x000FC414
		public virtual bool IsTrinomial
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06003042 RID: 12354 RVA: 0x000FC418 File Offset: 0x000FC418
		public virtual int K1
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06003043 RID: 12355 RVA: 0x000FC41C File Offset: 0x000FC41C
		public virtual int K2
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06003044 RID: 12356 RVA: 0x000FC420 File Offset: 0x000FC420
		public virtual int K3
		{
			get
			{
				return 7;
			}
		}

		// Token: 0x06003045 RID: 12357 RVA: 0x000FC424 File Offset: 0x000FC424
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			ulong[] array = new ulong[len * 3 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat192.Copy64(((SecT163FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 3;
				Nat192.Copy64(((SecT163FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 3;
			}
			return new SecT163R1Curve.SecT163R1LookupTable(this, array, len);
		}

		// Token: 0x04001C29 RID: 7209
		private const int SECT163R1_DEFAULT_COORDS = 6;

		// Token: 0x04001C2A RID: 7210
		private const int SECT163R1_FE_LONGS = 3;

		// Token: 0x04001C2B RID: 7211
		private static readonly ECFieldElement[] SECT163R1_AFFINE_ZS = new ECFieldElement[]
		{
			new SecT163FieldElement(BigInteger.One)
		};

		// Token: 0x04001C2C RID: 7212
		protected readonly SecT163R1Point m_infinity;

		// Token: 0x02000E3D RID: 3645
		private class SecT163R1LookupTable : AbstractECLookupTable
		{
			// Token: 0x06008CD7 RID: 36055 RVA: 0x002A41E8 File Offset: 0x002A41E8
			internal SecT163R1LookupTable(SecT163R1Curve outer, ulong[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17001D94 RID: 7572
			// (get) Token: 0x06008CD8 RID: 36056 RVA: 0x002A4208 File Offset: 0x002A4208
			public override int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06008CD9 RID: 36057 RVA: 0x002A4210 File Offset: 0x002A4210
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

			// Token: 0x06008CDA RID: 36058 RVA: 0x002A42AC File Offset: 0x002A42AC
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

			// Token: 0x06008CDB RID: 36059 RVA: 0x002A4304 File Offset: 0x002A4304
			private ECPoint CreatePoint(ulong[] x, ulong[] y)
			{
				return this.m_outer.CreateRawPoint(new SecT163FieldElement(x), new SecT163FieldElement(y), SecT163R1Curve.SECT163R1_AFFINE_ZS, false);
			}

			// Token: 0x040041D5 RID: 16853
			private readonly SecT163R1Curve m_outer;

			// Token: 0x040041D6 RID: 16854
			private readonly ulong[] m_table;

			// Token: 0x040041D7 RID: 16855
			private readonly int m_size;
		}
	}
}
