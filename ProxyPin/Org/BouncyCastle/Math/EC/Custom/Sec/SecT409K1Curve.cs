using System;
using Org.BouncyCastle.Math.EC.Multiplier;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x020005DD RID: 1501
	internal class SecT409K1Curve : AbstractF2mCurve
	{
		// Token: 0x06003242 RID: 12866 RVA: 0x001054F8 File Offset: 0x001054F8
		public SecT409K1Curve() : base(409, 87, 0, 0)
		{
			this.m_infinity = new SecT409K1Point(this, null, null);
			this.m_a = this.FromBigInteger(BigInteger.Zero);
			this.m_b = this.FromBigInteger(BigInteger.One);
			this.m_order = new BigInteger(1, Hex.DecodeStrict("7FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFE5F83B2D4EA20400EC4557D5ED3E3E7CA5B4B5C83B8E01E5FCF"));
			this.m_cofactor = BigInteger.ValueOf(4L);
			this.m_coord = 6;
		}

		// Token: 0x06003243 RID: 12867 RVA: 0x00105574 File Offset: 0x00105574
		protected override ECCurve CloneCurve()
		{
			return new SecT409K1Curve();
		}

		// Token: 0x06003244 RID: 12868 RVA: 0x0010557C File Offset: 0x0010557C
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 6;
		}

		// Token: 0x06003245 RID: 12869 RVA: 0x0010559C File Offset: 0x0010559C
		protected override ECMultiplier CreateDefaultMultiplier()
		{
			return new WTauNafMultiplier();
		}

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x06003246 RID: 12870 RVA: 0x001055A4 File Offset: 0x001055A4
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06003247 RID: 12871 RVA: 0x001055AC File Offset: 0x001055AC
		public override int FieldSize
		{
			get
			{
				return 409;
			}
		}

		// Token: 0x06003248 RID: 12872 RVA: 0x001055B4 File Offset: 0x001055B4
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecT409FieldElement(x);
		}

		// Token: 0x06003249 RID: 12873 RVA: 0x001055BC File Offset: 0x001055BC
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecT409K1Point(this, x, y, withCompression);
		}

		// Token: 0x0600324A RID: 12874 RVA: 0x001055C8 File Offset: 0x001055C8
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecT409K1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x0600324B RID: 12875 RVA: 0x001055D8 File Offset: 0x001055D8
		public override bool IsKoblitz
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x0600324C RID: 12876 RVA: 0x001055DC File Offset: 0x001055DC
		public virtual int M
		{
			get
			{
				return 409;
			}
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x0600324D RID: 12877 RVA: 0x001055E4 File Offset: 0x001055E4
		public virtual bool IsTrinomial
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x0600324E RID: 12878 RVA: 0x001055E8 File Offset: 0x001055E8
		public virtual int K1
		{
			get
			{
				return 87;
			}
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x0600324F RID: 12879 RVA: 0x001055EC File Offset: 0x001055EC
		public virtual int K2
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x06003250 RID: 12880 RVA: 0x001055F0 File Offset: 0x001055F0
		public virtual int K3
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06003251 RID: 12881 RVA: 0x001055F4 File Offset: 0x001055F4
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			ulong[] array = new ulong[len * 7 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat448.Copy64(((SecT409FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 7;
				Nat448.Copy64(((SecT409FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 7;
			}
			return new SecT409K1Curve.SecT409K1LookupTable(this, array, len);
		}

		// Token: 0x04001C5D RID: 7261
		private const int SECT409K1_DEFAULT_COORDS = 6;

		// Token: 0x04001C5E RID: 7262
		private const int SECT409K1_FE_LONGS = 7;

		// Token: 0x04001C5F RID: 7263
		private static readonly ECFieldElement[] SECT409K1_AFFINE_ZS = new ECFieldElement[]
		{
			new SecT409FieldElement(BigInteger.One)
		};

		// Token: 0x04001C60 RID: 7264
		protected readonly SecT409K1Point m_infinity;

		// Token: 0x02000E46 RID: 3654
		private class SecT409K1LookupTable : AbstractECLookupTable
		{
			// Token: 0x06008D04 RID: 36100 RVA: 0x002A4D04 File Offset: 0x002A4D04
			internal SecT409K1LookupTable(SecT409K1Curve outer, ulong[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17001D9D RID: 7581
			// (get) Token: 0x06008D05 RID: 36101 RVA: 0x002A4D24 File Offset: 0x002A4D24
			public override int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06008D06 RID: 36102 RVA: 0x002A4D2C File Offset: 0x002A4D2C
			public override ECPoint Lookup(int index)
			{
				ulong[] array = Nat448.Create64();
				ulong[] array2 = Nat448.Create64();
				int num = 0;
				for (int i = 0; i < this.m_size; i++)
				{
					ulong num2 = (ulong)((long)((i ^ index) - 1 >> 31));
					for (int j = 0; j < 7; j++)
					{
						ulong[] array3;
						IntPtr intPtr;
						(array3 = array)[(int)(intPtr = (IntPtr)j)] = (array3[(int)intPtr] ^ (this.m_table[num + j] & num2));
						(array3 = array2)[(int)(intPtr = (IntPtr)j)] = (array3[(int)intPtr] ^ (this.m_table[num + 7 + j] & num2));
					}
					num += 14;
				}
				return this.CreatePoint(array, array2);
			}

			// Token: 0x06008D07 RID: 36103 RVA: 0x002A4DC8 File Offset: 0x002A4DC8
			public override ECPoint LookupVar(int index)
			{
				ulong[] array = Nat448.Create64();
				ulong[] array2 = Nat448.Create64();
				int num = index * 7 * 2;
				for (int i = 0; i < 7; i++)
				{
					array[i] = this.m_table[num + i];
					array2[i] = this.m_table[num + 7 + i];
				}
				return this.CreatePoint(array, array2);
			}

			// Token: 0x06008D08 RID: 36104 RVA: 0x002A4E20 File Offset: 0x002A4E20
			private ECPoint CreatePoint(ulong[] x, ulong[] y)
			{
				return this.m_outer.CreateRawPoint(new SecT409FieldElement(x), new SecT409FieldElement(y), SecT409K1Curve.SECT409K1_AFFINE_ZS, false);
			}

			// Token: 0x040041F0 RID: 16880
			private readonly SecT409K1Curve m_outer;

			// Token: 0x040041F1 RID: 16881
			private readonly ulong[] m_table;

			// Token: 0x040041F2 RID: 16882
			private readonly int m_size;
		}
	}
}
