using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x020005D9 RID: 1497
	internal class SecT283R1Curve : AbstractF2mCurve
	{
		// Token: 0x060031EF RID: 12783 RVA: 0x00103EE8 File Offset: 0x00103EE8
		public SecT283R1Curve() : base(283, 5, 7, 12)
		{
			this.m_infinity = new SecT283R1Point(this, null, null);
			this.m_a = this.FromBigInteger(BigInteger.One);
			this.m_b = this.FromBigInteger(new BigInteger(1, Hex.DecodeStrict("027B680AC8B8596DA5A4AF8A19A0303FCA97FD7645309FA2A581485AF6263E313B79A2F5")));
			this.m_order = new BigInteger(1, Hex.DecodeStrict("03FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEF90399660FC938A90165B042A7CEFADB307"));
			this.m_cofactor = BigInteger.Two;
			this.m_coord = 6;
		}

		// Token: 0x060031F0 RID: 12784 RVA: 0x00103F6C File Offset: 0x00103F6C
		protected override ECCurve CloneCurve()
		{
			return new SecT283R1Curve();
		}

		// Token: 0x060031F1 RID: 12785 RVA: 0x00103F74 File Offset: 0x00103F74
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 6;
		}

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x060031F2 RID: 12786 RVA: 0x00103F94 File Offset: 0x00103F94
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x060031F3 RID: 12787 RVA: 0x00103F9C File Offset: 0x00103F9C
		public override int FieldSize
		{
			get
			{
				return 283;
			}
		}

		// Token: 0x060031F4 RID: 12788 RVA: 0x00103FA4 File Offset: 0x00103FA4
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecT283FieldElement(x);
		}

		// Token: 0x060031F5 RID: 12789 RVA: 0x00103FAC File Offset: 0x00103FAC
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecT283R1Point(this, x, y, withCompression);
		}

		// Token: 0x060031F6 RID: 12790 RVA: 0x00103FB8 File Offset: 0x00103FB8
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecT283R1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x060031F7 RID: 12791 RVA: 0x00103FC8 File Offset: 0x00103FC8
		public override bool IsKoblitz
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x060031F8 RID: 12792 RVA: 0x00103FCC File Offset: 0x00103FCC
		public virtual int M
		{
			get
			{
				return 283;
			}
		}

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x060031F9 RID: 12793 RVA: 0x00103FD4 File Offset: 0x00103FD4
		public virtual bool IsTrinomial
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x060031FA RID: 12794 RVA: 0x00103FD8 File Offset: 0x00103FD8
		public virtual int K1
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x060031FB RID: 12795 RVA: 0x00103FDC File Offset: 0x00103FDC
		public virtual int K2
		{
			get
			{
				return 7;
			}
		}

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x060031FC RID: 12796 RVA: 0x00103FE0 File Offset: 0x00103FE0
		public virtual int K3
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x060031FD RID: 12797 RVA: 0x00103FE4 File Offset: 0x00103FE4
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			ulong[] array = new ulong[len * 5 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat320.Copy64(((SecT283FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 5;
				Nat320.Copy64(((SecT283FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 5;
			}
			return new SecT283R1Curve.SecT283R1LookupTable(this, array, len);
		}

		// Token: 0x04001C56 RID: 7254
		private const int SECT283R1_DEFAULT_COORDS = 6;

		// Token: 0x04001C57 RID: 7255
		private const int SECT283R1_FE_LONGS = 5;

		// Token: 0x04001C58 RID: 7256
		private static readonly ECFieldElement[] SECT283R1_AFFINE_ZS = new ECFieldElement[]
		{
			new SecT283FieldElement(BigInteger.One)
		};

		// Token: 0x04001C59 RID: 7257
		protected readonly SecT283R1Point m_infinity;

		// Token: 0x02000E45 RID: 3653
		private class SecT283R1LookupTable : AbstractECLookupTable
		{
			// Token: 0x06008CFF RID: 36095 RVA: 0x002A4BC8 File Offset: 0x002A4BC8
			internal SecT283R1LookupTable(SecT283R1Curve outer, ulong[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17001D9C RID: 7580
			// (get) Token: 0x06008D00 RID: 36096 RVA: 0x002A4BE8 File Offset: 0x002A4BE8
			public override int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06008D01 RID: 36097 RVA: 0x002A4BF0 File Offset: 0x002A4BF0
			public override ECPoint Lookup(int index)
			{
				ulong[] array = Nat320.Create64();
				ulong[] array2 = Nat320.Create64();
				int num = 0;
				for (int i = 0; i < this.m_size; i++)
				{
					ulong num2 = (ulong)((long)((i ^ index) - 1 >> 31));
					for (int j = 0; j < 5; j++)
					{
						ulong[] array3;
						IntPtr intPtr;
						(array3 = array)[(int)(intPtr = (IntPtr)j)] = (array3[(int)intPtr] ^ (this.m_table[num + j] & num2));
						(array3 = array2)[(int)(intPtr = (IntPtr)j)] = (array3[(int)intPtr] ^ (this.m_table[num + 5 + j] & num2));
					}
					num += 10;
				}
				return this.CreatePoint(array, array2);
			}

			// Token: 0x06008D02 RID: 36098 RVA: 0x002A4C8C File Offset: 0x002A4C8C
			public override ECPoint LookupVar(int index)
			{
				ulong[] array = Nat320.Create64();
				ulong[] array2 = Nat320.Create64();
				int num = index * 5 * 2;
				for (int i = 0; i < 5; i++)
				{
					array[i] = this.m_table[num + i];
					array2[i] = this.m_table[num + 5 + i];
				}
				return this.CreatePoint(array, array2);
			}

			// Token: 0x06008D03 RID: 36099 RVA: 0x002A4CE4 File Offset: 0x002A4CE4
			private ECPoint CreatePoint(ulong[] x, ulong[] y)
			{
				return this.m_outer.CreateRawPoint(new SecT283FieldElement(x), new SecT283FieldElement(y), SecT283R1Curve.SECT283R1_AFFINE_ZS, false);
			}

			// Token: 0x040041ED RID: 16877
			private readonly SecT283R1Curve m_outer;

			// Token: 0x040041EE RID: 16878
			private readonly ulong[] m_table;

			// Token: 0x040041EF RID: 16879
			private readonly int m_size;
		}
	}
}
