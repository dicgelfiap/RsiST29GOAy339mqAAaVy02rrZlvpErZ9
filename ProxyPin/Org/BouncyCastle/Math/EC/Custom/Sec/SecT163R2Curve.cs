using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x020005C3 RID: 1475
	internal class SecT163R2Curve : AbstractF2mCurve
	{
		// Token: 0x06003051 RID: 12369 RVA: 0x000FCC20 File Offset: 0x000FCC20
		public SecT163R2Curve() : base(163, 3, 6, 7)
		{
			this.m_infinity = new SecT163R2Point(this, null, null);
			this.m_a = this.FromBigInteger(BigInteger.One);
			this.m_b = this.FromBigInteger(new BigInteger(1, Hex.DecodeStrict("020A601907B8C953CA1481EB10512F78744A3205FD")));
			this.m_order = new BigInteger(1, Hex.DecodeStrict("040000000000000000000292FE77E70C12A4234C33"));
			this.m_cofactor = BigInteger.Two;
			this.m_coord = 6;
		}

		// Token: 0x06003052 RID: 12370 RVA: 0x000FCCA4 File Offset: 0x000FCCA4
		protected override ECCurve CloneCurve()
		{
			return new SecT163R2Curve();
		}

		// Token: 0x06003053 RID: 12371 RVA: 0x000FCCAC File Offset: 0x000FCCAC
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 6;
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06003054 RID: 12372 RVA: 0x000FCCCC File Offset: 0x000FCCCC
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06003055 RID: 12373 RVA: 0x000FCCD4 File Offset: 0x000FCCD4
		public override int FieldSize
		{
			get
			{
				return 163;
			}
		}

		// Token: 0x06003056 RID: 12374 RVA: 0x000FCCDC File Offset: 0x000FCCDC
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecT163FieldElement(x);
		}

		// Token: 0x06003057 RID: 12375 RVA: 0x000FCCE4 File Offset: 0x000FCCE4
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecT163R2Point(this, x, y, withCompression);
		}

		// Token: 0x06003058 RID: 12376 RVA: 0x000FCCF0 File Offset: 0x000FCCF0
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecT163R2Point(this, x, y, zs, withCompression);
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06003059 RID: 12377 RVA: 0x000FCD00 File Offset: 0x000FCD00
		public override bool IsKoblitz
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x0600305A RID: 12378 RVA: 0x000FCD04 File Offset: 0x000FCD04
		public virtual int M
		{
			get
			{
				return 163;
			}
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x0600305B RID: 12379 RVA: 0x000FCD0C File Offset: 0x000FCD0C
		public virtual bool IsTrinomial
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x0600305C RID: 12380 RVA: 0x000FCD10 File Offset: 0x000FCD10
		public virtual int K1
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x0600305D RID: 12381 RVA: 0x000FCD14 File Offset: 0x000FCD14
		public virtual int K2
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x0600305E RID: 12382 RVA: 0x000FCD18 File Offset: 0x000FCD18
		public virtual int K3
		{
			get
			{
				return 7;
			}
		}

		// Token: 0x0600305F RID: 12383 RVA: 0x000FCD1C File Offset: 0x000FCD1C
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
			return new SecT163R2Curve.SecT163R2LookupTable(this, array, len);
		}

		// Token: 0x04001C2D RID: 7213
		private const int SECT163R2_DEFAULT_COORDS = 6;

		// Token: 0x04001C2E RID: 7214
		private const int SECT163R2_FE_LONGS = 3;

		// Token: 0x04001C2F RID: 7215
		private static readonly ECFieldElement[] SECT163R2_AFFINE_ZS = new ECFieldElement[]
		{
			new SecT163FieldElement(BigInteger.One)
		};

		// Token: 0x04001C30 RID: 7216
		protected readonly SecT163R2Point m_infinity;

		// Token: 0x02000E3E RID: 3646
		private class SecT163R2LookupTable : AbstractECLookupTable
		{
			// Token: 0x06008CDC RID: 36060 RVA: 0x002A4324 File Offset: 0x002A4324
			internal SecT163R2LookupTable(SecT163R2Curve outer, ulong[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17001D95 RID: 7573
			// (get) Token: 0x06008CDD RID: 36061 RVA: 0x002A4344 File Offset: 0x002A4344
			public override int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06008CDE RID: 36062 RVA: 0x002A434C File Offset: 0x002A434C
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

			// Token: 0x06008CDF RID: 36063 RVA: 0x002A43E8 File Offset: 0x002A43E8
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

			// Token: 0x06008CE0 RID: 36064 RVA: 0x002A4440 File Offset: 0x002A4440
			private ECPoint CreatePoint(ulong[] x, ulong[] y)
			{
				return this.m_outer.CreateRawPoint(new SecT163FieldElement(x), new SecT163FieldElement(y), SecT163R2Curve.SECT163R2_AFFINE_ZS, false);
			}

			// Token: 0x040041D8 RID: 16856
			private readonly SecT163R2Curve m_outer;

			// Token: 0x040041D9 RID: 16857
			private readonly ulong[] m_table;

			// Token: 0x040041DA RID: 16858
			private readonly int m_size;
		}
	}
}
