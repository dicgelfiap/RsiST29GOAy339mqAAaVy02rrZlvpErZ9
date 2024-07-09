using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x020005B9 RID: 1465
	internal class SecT131R1Curve : AbstractF2mCurve
	{
		// Token: 0x06002FAF RID: 12207 RVA: 0x000F9BC8 File Offset: 0x000F9BC8
		public SecT131R1Curve() : base(131, 2, 3, 8)
		{
			this.m_infinity = new SecT131R1Point(this, null, null);
			this.m_a = this.FromBigInteger(new BigInteger(1, Hex.DecodeStrict("07A11B09A76B562144418FF3FF8C2570B8")));
			this.m_b = this.FromBigInteger(new BigInteger(1, Hex.DecodeStrict("0217C05610884B63B9C6C7291678F9D341")));
			this.m_order = new BigInteger(1, Hex.DecodeStrict("0400000000000000023123953A9464B54D"));
			this.m_cofactor = BigInteger.Two;
			this.m_coord = 6;
		}

		// Token: 0x06002FB0 RID: 12208 RVA: 0x000F9C58 File Offset: 0x000F9C58
		protected override ECCurve CloneCurve()
		{
			return new SecT131R1Curve();
		}

		// Token: 0x06002FB1 RID: 12209 RVA: 0x000F9C60 File Offset: 0x000F9C60
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 6;
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x06002FB2 RID: 12210 RVA: 0x000F9C80 File Offset: 0x000F9C80
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x06002FB3 RID: 12211 RVA: 0x000F9C88 File Offset: 0x000F9C88
		public override int FieldSize
		{
			get
			{
				return 131;
			}
		}

		// Token: 0x06002FB4 RID: 12212 RVA: 0x000F9C90 File Offset: 0x000F9C90
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecT131FieldElement(x);
		}

		// Token: 0x06002FB5 RID: 12213 RVA: 0x000F9C98 File Offset: 0x000F9C98
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecT131R1Point(this, x, y, withCompression);
		}

		// Token: 0x06002FB6 RID: 12214 RVA: 0x000F9CA4 File Offset: 0x000F9CA4
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecT131R1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x06002FB7 RID: 12215 RVA: 0x000F9CB4 File Offset: 0x000F9CB4
		public override bool IsKoblitz
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x06002FB8 RID: 12216 RVA: 0x000F9CB8 File Offset: 0x000F9CB8
		public virtual int M
		{
			get
			{
				return 131;
			}
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x06002FB9 RID: 12217 RVA: 0x000F9CC0 File Offset: 0x000F9CC0
		public virtual bool IsTrinomial
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06002FBA RID: 12218 RVA: 0x000F9CC4 File Offset: 0x000F9CC4
		public virtual int K1
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06002FBB RID: 12219 RVA: 0x000F9CC8 File Offset: 0x000F9CC8
		public virtual int K2
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06002FBC RID: 12220 RVA: 0x000F9CCC File Offset: 0x000F9CCC
		public virtual int K3
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x06002FBD RID: 12221 RVA: 0x000F9CD0 File Offset: 0x000F9CD0
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
			return new SecT131R1Curve.SecT131R1LookupTable(this, array, len);
		}

		// Token: 0x04001C19 RID: 7193
		private const int SECT131R1_DEFAULT_COORDS = 6;

		// Token: 0x04001C1A RID: 7194
		private const int SECT131R1_FE_LONGS = 3;

		// Token: 0x04001C1B RID: 7195
		private static readonly ECFieldElement[] SECT131R1_AFFINE_ZS = new ECFieldElement[]
		{
			new SecT131FieldElement(BigInteger.One)
		};

		// Token: 0x04001C1C RID: 7196
		protected readonly SecT131R1Point m_infinity;

		// Token: 0x02000E3A RID: 3642
		private class SecT131R1LookupTable : AbstractECLookupTable
		{
			// Token: 0x06008CC8 RID: 36040 RVA: 0x002A3E34 File Offset: 0x002A3E34
			internal SecT131R1LookupTable(SecT131R1Curve outer, ulong[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17001D91 RID: 7569
			// (get) Token: 0x06008CC9 RID: 36041 RVA: 0x002A3E54 File Offset: 0x002A3E54
			public override int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06008CCA RID: 36042 RVA: 0x002A3E5C File Offset: 0x002A3E5C
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

			// Token: 0x06008CCB RID: 36043 RVA: 0x002A3EF8 File Offset: 0x002A3EF8
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

			// Token: 0x06008CCC RID: 36044 RVA: 0x002A3F50 File Offset: 0x002A3F50
			private ECPoint CreatePoint(ulong[] x, ulong[] y)
			{
				return this.m_outer.CreateRawPoint(new SecT131FieldElement(x), new SecT131FieldElement(y), SecT131R1Curve.SECT131R1_AFFINE_ZS, false);
			}

			// Token: 0x040041CC RID: 16844
			private readonly SecT131R1Curve m_outer;

			// Token: 0x040041CD RID: 16845
			private readonly ulong[] m_table;

			// Token: 0x040041CE RID: 16846
			private readonly int m_size;
		}
	}
}
