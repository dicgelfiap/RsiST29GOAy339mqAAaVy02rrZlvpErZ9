using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x020005DF RID: 1503
	internal class SecT409R1Curve : AbstractF2mCurve
	{
		// Token: 0x0600325D RID: 12893 RVA: 0x00105DAC File Offset: 0x00105DAC
		public SecT409R1Curve() : base(409, 87, 0, 0)
		{
			this.m_infinity = new SecT409R1Point(this, null, null);
			this.m_a = this.FromBigInteger(BigInteger.One);
			this.m_b = this.FromBigInteger(new BigInteger(1, Hex.DecodeStrict("0021A5C2C8EE9FEB5C4B9A753B7B476B7FD6422EF1F3DD674761FA99D6AC27C8A9A197B272822F6CD57A55AA4F50AE317B13545F")));
			this.m_order = new BigInteger(1, Hex.DecodeStrict("010000000000000000000000000000000000000000000000000001E2AAD6A612F33307BE5FA47C3C9E052F838164CD37D9A21173"));
			this.m_cofactor = BigInteger.Two;
			this.m_coord = 6;
		}

		// Token: 0x0600325E RID: 12894 RVA: 0x00105E30 File Offset: 0x00105E30
		protected override ECCurve CloneCurve()
		{
			return new SecT409R1Curve();
		}

		// Token: 0x0600325F RID: 12895 RVA: 0x00105E38 File Offset: 0x00105E38
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 6;
		}

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x06003260 RID: 12896 RVA: 0x00105E58 File Offset: 0x00105E58
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x06003261 RID: 12897 RVA: 0x00105E60 File Offset: 0x00105E60
		public override int FieldSize
		{
			get
			{
				return 409;
			}
		}

		// Token: 0x06003262 RID: 12898 RVA: 0x00105E68 File Offset: 0x00105E68
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecT409FieldElement(x);
		}

		// Token: 0x06003263 RID: 12899 RVA: 0x00105E70 File Offset: 0x00105E70
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecT409R1Point(this, x, y, withCompression);
		}

		// Token: 0x06003264 RID: 12900 RVA: 0x00105E7C File Offset: 0x00105E7C
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecT409R1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x06003265 RID: 12901 RVA: 0x00105E8C File Offset: 0x00105E8C
		public override bool IsKoblitz
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x06003266 RID: 12902 RVA: 0x00105E90 File Offset: 0x00105E90
		public virtual int M
		{
			get
			{
				return 409;
			}
		}

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x06003267 RID: 12903 RVA: 0x00105E98 File Offset: 0x00105E98
		public virtual bool IsTrinomial
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x06003268 RID: 12904 RVA: 0x00105E9C File Offset: 0x00105E9C
		public virtual int K1
		{
			get
			{
				return 87;
			}
		}

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x06003269 RID: 12905 RVA: 0x00105EA0 File Offset: 0x00105EA0
		public virtual int K2
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x0600326A RID: 12906 RVA: 0x00105EA4 File Offset: 0x00105EA4
		public virtual int K3
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600326B RID: 12907 RVA: 0x00105EA8 File Offset: 0x00105EA8
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
			return new SecT409R1Curve.SecT409R1LookupTable(this, array, len);
		}

		// Token: 0x04001C61 RID: 7265
		private const int SECT409R1_DEFAULT_COORDS = 6;

		// Token: 0x04001C62 RID: 7266
		private const int SECT409R1_FE_LONGS = 7;

		// Token: 0x04001C63 RID: 7267
		private static readonly ECFieldElement[] SECT409R1_AFFINE_ZS = new ECFieldElement[]
		{
			new SecT409FieldElement(BigInteger.One)
		};

		// Token: 0x04001C64 RID: 7268
		protected readonly SecT409R1Point m_infinity;

		// Token: 0x02000E47 RID: 3655
		private class SecT409R1LookupTable : AbstractECLookupTable
		{
			// Token: 0x06008D09 RID: 36105 RVA: 0x002A4E40 File Offset: 0x002A4E40
			internal SecT409R1LookupTable(SecT409R1Curve outer, ulong[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17001D9E RID: 7582
			// (get) Token: 0x06008D0A RID: 36106 RVA: 0x002A4E60 File Offset: 0x002A4E60
			public override int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06008D0B RID: 36107 RVA: 0x002A4E68 File Offset: 0x002A4E68
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

			// Token: 0x06008D0C RID: 36108 RVA: 0x002A4F04 File Offset: 0x002A4F04
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

			// Token: 0x06008D0D RID: 36109 RVA: 0x002A4F5C File Offset: 0x002A4F5C
			private ECPoint CreatePoint(ulong[] x, ulong[] y)
			{
				return this.m_outer.CreateRawPoint(new SecT409FieldElement(x), new SecT409FieldElement(y), SecT409R1Curve.SECT409R1_AFFINE_ZS, false);
			}

			// Token: 0x040041F3 RID: 16883
			private readonly SecT409R1Curve m_outer;

			// Token: 0x040041F4 RID: 16884
			private readonly ulong[] m_table;

			// Token: 0x040041F5 RID: 16885
			private readonly int m_size;
		}
	}
}
