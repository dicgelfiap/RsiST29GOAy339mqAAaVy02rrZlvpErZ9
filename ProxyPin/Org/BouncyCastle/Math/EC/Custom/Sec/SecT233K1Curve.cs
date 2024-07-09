using System;
using Org.BouncyCastle.Math.EC.Multiplier;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x020005CD RID: 1485
	internal class SecT233K1Curve : AbstractF2mCurve
	{
		// Token: 0x06003111 RID: 12561 RVA: 0x000FFFD8 File Offset: 0x000FFFD8
		public SecT233K1Curve() : base(233, 74, 0, 0)
		{
			this.m_infinity = new SecT233K1Point(this, null, null);
			this.m_a = this.FromBigInteger(BigInteger.Zero);
			this.m_b = this.FromBigInteger(BigInteger.One);
			this.m_order = new BigInteger(1, Hex.DecodeStrict("8000000000000000000000000000069D5BB915BCD46EFB1AD5F173ABDF"));
			this.m_cofactor = BigInteger.ValueOf(4L);
			this.m_coord = 6;
		}

		// Token: 0x06003112 RID: 12562 RVA: 0x00100054 File Offset: 0x00100054
		protected override ECCurve CloneCurve()
		{
			return new SecT233K1Curve();
		}

		// Token: 0x06003113 RID: 12563 RVA: 0x0010005C File Offset: 0x0010005C
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 6;
		}

		// Token: 0x06003114 RID: 12564 RVA: 0x0010007C File Offset: 0x0010007C
		protected override ECMultiplier CreateDefaultMultiplier()
		{
			return new WTauNafMultiplier();
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06003115 RID: 12565 RVA: 0x00100084 File Offset: 0x00100084
		public override int FieldSize
		{
			get
			{
				return 233;
			}
		}

		// Token: 0x06003116 RID: 12566 RVA: 0x0010008C File Offset: 0x0010008C
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecT233FieldElement(x);
		}

		// Token: 0x06003117 RID: 12567 RVA: 0x00100094 File Offset: 0x00100094
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecT233K1Point(this, x, y, withCompression);
		}

		// Token: 0x06003118 RID: 12568 RVA: 0x001000A0 File Offset: 0x001000A0
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecT233K1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06003119 RID: 12569 RVA: 0x001000B0 File Offset: 0x001000B0
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x0600311A RID: 12570 RVA: 0x001000B8 File Offset: 0x001000B8
		public override bool IsKoblitz
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x0600311B RID: 12571 RVA: 0x001000BC File Offset: 0x001000BC
		public virtual int M
		{
			get
			{
				return 233;
			}
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x0600311C RID: 12572 RVA: 0x001000C4 File Offset: 0x001000C4
		public virtual bool IsTrinomial
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x0600311D RID: 12573 RVA: 0x001000C8 File Offset: 0x001000C8
		public virtual int K1
		{
			get
			{
				return 74;
			}
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x0600311E RID: 12574 RVA: 0x001000CC File Offset: 0x001000CC
		public virtual int K2
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x0600311F RID: 12575 RVA: 0x001000D0 File Offset: 0x001000D0
		public virtual int K3
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06003120 RID: 12576 RVA: 0x001000D4 File Offset: 0x001000D4
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			ulong[] array = new ulong[len * 4 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat256.Copy64(((SecT233FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 4;
				Nat256.Copy64(((SecT233FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 4;
			}
			return new SecT233K1Curve.SecT233K1LookupTable(this, array, len);
		}

		// Token: 0x04001C3F RID: 7231
		private const int SECT233K1_DEFAULT_COORDS = 6;

		// Token: 0x04001C40 RID: 7232
		private const int SECT233K1_FE_LONGS = 4;

		// Token: 0x04001C41 RID: 7233
		private static readonly ECFieldElement[] SECT233K1_AFFINE_ZS = new ECFieldElement[]
		{
			new SecT233FieldElement(BigInteger.One)
		};

		// Token: 0x04001C42 RID: 7234
		protected readonly SecT233K1Point m_infinity;

		// Token: 0x02000E41 RID: 3649
		private class SecT233K1LookupTable : AbstractECLookupTable
		{
			// Token: 0x06008CEB RID: 36075 RVA: 0x002A46D8 File Offset: 0x002A46D8
			internal SecT233K1LookupTable(SecT233K1Curve outer, ulong[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17001D98 RID: 7576
			// (get) Token: 0x06008CEC RID: 36076 RVA: 0x002A46F8 File Offset: 0x002A46F8
			public override int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06008CED RID: 36077 RVA: 0x002A4700 File Offset: 0x002A4700
			public override ECPoint Lookup(int index)
			{
				ulong[] array = Nat256.Create64();
				ulong[] array2 = Nat256.Create64();
				int num = 0;
				for (int i = 0; i < this.m_size; i++)
				{
					ulong num2 = (ulong)((long)((i ^ index) - 1 >> 31));
					for (int j = 0; j < 4; j++)
					{
						ulong[] array3;
						IntPtr intPtr;
						(array3 = array)[(int)(intPtr = (IntPtr)j)] = (array3[(int)intPtr] ^ (this.m_table[num + j] & num2));
						(array3 = array2)[(int)(intPtr = (IntPtr)j)] = (array3[(int)intPtr] ^ (this.m_table[num + 4 + j] & num2));
					}
					num += 8;
				}
				return this.CreatePoint(array, array2);
			}

			// Token: 0x06008CEE RID: 36078 RVA: 0x002A479C File Offset: 0x002A479C
			public override ECPoint LookupVar(int index)
			{
				ulong[] array = Nat256.Create64();
				ulong[] array2 = Nat256.Create64();
				int num = index * 4 * 2;
				for (int i = 0; i < 4; i++)
				{
					array[i] = this.m_table[num + i];
					array2[i] = this.m_table[num + 4 + i];
				}
				return this.CreatePoint(array, array2);
			}

			// Token: 0x06008CEF RID: 36079 RVA: 0x002A47F4 File Offset: 0x002A47F4
			private ECPoint CreatePoint(ulong[] x, ulong[] y)
			{
				return this.m_outer.CreateRawPoint(new SecT233FieldElement(x), new SecT233FieldElement(y), SecT233K1Curve.SECT233K1_AFFINE_ZS, false);
			}

			// Token: 0x040041E1 RID: 16865
			private readonly SecT233K1Curve m_outer;

			// Token: 0x040041E2 RID: 16866
			private readonly ulong[] m_table;

			// Token: 0x040041E3 RID: 16867
			private readonly int m_size;
		}
	}
}
