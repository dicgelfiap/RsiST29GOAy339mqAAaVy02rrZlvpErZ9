using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x020005B5 RID: 1461
	internal class SecT113R2Curve : AbstractF2mCurve
	{
		// Token: 0x06002F5C RID: 12124 RVA: 0x000F862C File Offset: 0x000F862C
		public SecT113R2Curve() : base(113, 9, 0, 0)
		{
			this.m_infinity = new SecT113R2Point(this, null, null);
			this.m_a = this.FromBigInteger(new BigInteger(1, Hex.DecodeStrict("00689918DBEC7E5A0DD6DFC0AA55C7")));
			this.m_b = this.FromBigInteger(new BigInteger(1, Hex.DecodeStrict("0095E9A9EC9B297BD4BF36E059184F")));
			this.m_order = new BigInteger(1, Hex.DecodeStrict("010000000000000108789B2496AF93"));
			this.m_cofactor = BigInteger.Two;
			this.m_coord = 6;
		}

		// Token: 0x06002F5D RID: 12125 RVA: 0x000F86B8 File Offset: 0x000F86B8
		protected override ECCurve CloneCurve()
		{
			return new SecT113R2Curve();
		}

		// Token: 0x06002F5E RID: 12126 RVA: 0x000F86C0 File Offset: 0x000F86C0
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 6;
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06002F5F RID: 12127 RVA: 0x000F86E0 File Offset: 0x000F86E0
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06002F60 RID: 12128 RVA: 0x000F86E8 File Offset: 0x000F86E8
		public override int FieldSize
		{
			get
			{
				return 113;
			}
		}

		// Token: 0x06002F61 RID: 12129 RVA: 0x000F86EC File Offset: 0x000F86EC
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecT113FieldElement(x);
		}

		// Token: 0x06002F62 RID: 12130 RVA: 0x000F86F4 File Offset: 0x000F86F4
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecT113R2Point(this, x, y, withCompression);
		}

		// Token: 0x06002F63 RID: 12131 RVA: 0x000F8700 File Offset: 0x000F8700
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecT113R2Point(this, x, y, zs, withCompression);
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06002F64 RID: 12132 RVA: 0x000F8710 File Offset: 0x000F8710
		public override bool IsKoblitz
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06002F65 RID: 12133 RVA: 0x000F8714 File Offset: 0x000F8714
		public virtual int M
		{
			get
			{
				return 113;
			}
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x06002F66 RID: 12134 RVA: 0x000F8718 File Offset: 0x000F8718
		public virtual bool IsTrinomial
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06002F67 RID: 12135 RVA: 0x000F871C File Offset: 0x000F871C
		public virtual int K1
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x06002F68 RID: 12136 RVA: 0x000F8720 File Offset: 0x000F8720
		public virtual int K2
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x06002F69 RID: 12137 RVA: 0x000F8724 File Offset: 0x000F8724
		public virtual int K3
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06002F6A RID: 12138 RVA: 0x000F8728 File Offset: 0x000F8728
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
			return new SecT113R2Curve.SecT113R2LookupTable(this, array, len);
		}

		// Token: 0x04001C11 RID: 7185
		private const int SECT113R2_DEFAULT_COORDS = 6;

		// Token: 0x04001C12 RID: 7186
		private const int SECT113R2_FE_LONGS = 2;

		// Token: 0x04001C13 RID: 7187
		private static readonly ECFieldElement[] SECT113R2_AFFINE_ZS = new ECFieldElement[]
		{
			new SecT113FieldElement(BigInteger.One)
		};

		// Token: 0x04001C14 RID: 7188
		protected readonly SecT113R2Point m_infinity;

		// Token: 0x02000E39 RID: 3641
		private class SecT113R2LookupTable : AbstractECLookupTable
		{
			// Token: 0x06008CC3 RID: 36035 RVA: 0x002A3CF8 File Offset: 0x002A3CF8
			internal SecT113R2LookupTable(SecT113R2Curve outer, ulong[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17001D90 RID: 7568
			// (get) Token: 0x06008CC4 RID: 36036 RVA: 0x002A3D18 File Offset: 0x002A3D18
			public override int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06008CC5 RID: 36037 RVA: 0x002A3D20 File Offset: 0x002A3D20
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

			// Token: 0x06008CC6 RID: 36038 RVA: 0x002A3DBC File Offset: 0x002A3DBC
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

			// Token: 0x06008CC7 RID: 36039 RVA: 0x002A3E14 File Offset: 0x002A3E14
			private ECPoint CreatePoint(ulong[] x, ulong[] y)
			{
				return this.m_outer.CreateRawPoint(new SecT113FieldElement(x), new SecT113FieldElement(y), SecT113R2Curve.SECT113R2_AFFINE_ZS, false);
			}

			// Token: 0x040041C9 RID: 16841
			private readonly SecT113R2Curve m_outer;

			// Token: 0x040041CA RID: 16842
			private readonly ulong[] m_table;

			// Token: 0x040041CB RID: 16843
			private readonly int m_size;
		}
	}
}
