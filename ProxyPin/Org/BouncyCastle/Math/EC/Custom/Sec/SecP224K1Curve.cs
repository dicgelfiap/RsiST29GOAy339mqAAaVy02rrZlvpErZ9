using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000596 RID: 1430
	internal class SecP224K1Curve : AbstractFpCurve
	{
		// Token: 0x06002D82 RID: 11650 RVA: 0x000EFD88 File Offset: 0x000EFD88
		public SecP224K1Curve() : base(SecP224K1Curve.q)
		{
			this.m_infinity = new SecP224K1Point(this, null, null);
			this.m_a = this.FromBigInteger(BigInteger.Zero);
			this.m_b = this.FromBigInteger(BigInteger.ValueOf(5L));
			this.m_order = new BigInteger(1, Hex.DecodeStrict("010000000000000000000000000001DCE8D2EC6184CAF0A971769FB1F7"));
			this.m_cofactor = BigInteger.One;
			this.m_coord = 2;
		}

		// Token: 0x06002D83 RID: 11651 RVA: 0x000EFE00 File Offset: 0x000EFE00
		protected override ECCurve CloneCurve()
		{
			return new SecP224K1Curve();
		}

		// Token: 0x06002D84 RID: 11652 RVA: 0x000EFE08 File Offset: 0x000EFE08
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 2;
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x06002D85 RID: 11653 RVA: 0x000EFE28 File Offset: 0x000EFE28
		public virtual BigInteger Q
		{
			get
			{
				return SecP224K1Curve.q;
			}
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x06002D86 RID: 11654 RVA: 0x000EFE30 File Offset: 0x000EFE30
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x06002D87 RID: 11655 RVA: 0x000EFE38 File Offset: 0x000EFE38
		public override int FieldSize
		{
			get
			{
				return SecP224K1Curve.q.BitLength;
			}
		}

		// Token: 0x06002D88 RID: 11656 RVA: 0x000EFE44 File Offset: 0x000EFE44
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecP224K1FieldElement(x);
		}

		// Token: 0x06002D89 RID: 11657 RVA: 0x000EFE4C File Offset: 0x000EFE4C
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecP224K1Point(this, x, y, withCompression);
		}

		// Token: 0x06002D8A RID: 11658 RVA: 0x000EFE58 File Offset: 0x000EFE58
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecP224K1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x06002D8B RID: 11659 RVA: 0x000EFE68 File Offset: 0x000EFE68
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			uint[] array = new uint[len * 7 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat224.Copy(((SecP224K1FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 7;
				Nat224.Copy(((SecP224K1FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 7;
			}
			return new SecP224K1Curve.SecP224K1LookupTable(this, array, len);
		}

		// Token: 0x04001BC2 RID: 7106
		private const int SECP224K1_DEFAULT_COORDS = 2;

		// Token: 0x04001BC3 RID: 7107
		private const int SECP224K1_FE_INTS = 7;

		// Token: 0x04001BC4 RID: 7108
		public static readonly BigInteger q = SecP224K1FieldElement.Q;

		// Token: 0x04001BC5 RID: 7109
		private static readonly ECFieldElement[] SECP224K1_AFFINE_ZS = new ECFieldElement[]
		{
			new SecP224K1FieldElement(BigInteger.One)
		};

		// Token: 0x04001BC6 RID: 7110
		protected readonly SecP224K1Point m_infinity;

		// Token: 0x02000E32 RID: 3634
		private class SecP224K1LookupTable : AbstractECLookupTable
		{
			// Token: 0x06008CA0 RID: 36000 RVA: 0x002A3434 File Offset: 0x002A3434
			internal SecP224K1LookupTable(SecP224K1Curve outer, uint[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17001D89 RID: 7561
			// (get) Token: 0x06008CA1 RID: 36001 RVA: 0x002A3454 File Offset: 0x002A3454
			public override int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06008CA2 RID: 36002 RVA: 0x002A345C File Offset: 0x002A345C
			public override ECPoint Lookup(int index)
			{
				uint[] array = Nat224.Create();
				uint[] array2 = Nat224.Create();
				int num = 0;
				for (int i = 0; i < this.m_size; i++)
				{
					uint num2 = (uint)((i ^ index) - 1 >> 31);
					for (int j = 0; j < 7; j++)
					{
						uint[] array3;
						IntPtr intPtr;
						(array3 = array)[(int)(intPtr = (IntPtr)j)] = (array3[(int)intPtr] ^ (this.m_table[num + j] & num2));
						(array3 = array2)[(int)(intPtr = (IntPtr)j)] = (array3[(int)intPtr] ^ (this.m_table[num + 7 + j] & num2));
					}
					num += 14;
				}
				return this.CreatePoint(array, array2);
			}

			// Token: 0x06008CA3 RID: 36003 RVA: 0x002A34F8 File Offset: 0x002A34F8
			public override ECPoint LookupVar(int index)
			{
				uint[] array = Nat224.Create();
				uint[] array2 = Nat224.Create();
				int num = index * 7 * 2;
				for (int i = 0; i < 7; i++)
				{
					array[i] = this.m_table[num + i];
					array2[i] = this.m_table[num + 7 + i];
				}
				return this.CreatePoint(array, array2);
			}

			// Token: 0x06008CA4 RID: 36004 RVA: 0x002A3550 File Offset: 0x002A3550
			private ECPoint CreatePoint(uint[] x, uint[] y)
			{
				return this.m_outer.CreateRawPoint(new SecP224K1FieldElement(x), new SecP224K1FieldElement(y), SecP224K1Curve.SECP224K1_AFFINE_ZS, false);
			}

			// Token: 0x040041B4 RID: 16820
			private readonly SecP224K1Curve m_outer;

			// Token: 0x040041B5 RID: 16821
			private readonly uint[] m_table;

			// Token: 0x040041B6 RID: 16822
			private readonly int m_size;
		}
	}
}
