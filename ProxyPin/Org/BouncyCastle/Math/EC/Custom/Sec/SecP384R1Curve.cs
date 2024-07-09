using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x020005A6 RID: 1446
	internal class SecP384R1Curve : AbstractFpCurve
	{
		// Token: 0x06002E7B RID: 11899 RVA: 0x000F45AC File Offset: 0x000F45AC
		public SecP384R1Curve() : base(SecP384R1Curve.q)
		{
			this.m_infinity = new SecP384R1Point(this, null, null);
			this.m_a = this.FromBigInteger(new BigInteger(1, Hex.DecodeStrict("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFFFF0000000000000000FFFFFFFC")));
			this.m_b = this.FromBigInteger(new BigInteger(1, Hex.DecodeStrict("B3312FA7E23EE7E4988E056BE3F82D19181D9C6EFE8141120314088F5013875AC656398D8A2ED19D2A85C8EDD3EC2AEF")));
			this.m_order = new BigInteger(1, Hex.DecodeStrict("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFC7634D81F4372DDF581A0DB248B0A77AECEC196ACCC52973"));
			this.m_cofactor = BigInteger.One;
			this.m_coord = 2;
		}

		// Token: 0x06002E7C RID: 11900 RVA: 0x000F4638 File Offset: 0x000F4638
		protected override ECCurve CloneCurve()
		{
			return new SecP384R1Curve();
		}

		// Token: 0x06002E7D RID: 11901 RVA: 0x000F4640 File Offset: 0x000F4640
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 2;
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06002E7E RID: 11902 RVA: 0x000F4660 File Offset: 0x000F4660
		public virtual BigInteger Q
		{
			get
			{
				return SecP384R1Curve.q;
			}
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06002E7F RID: 11903 RVA: 0x000F4668 File Offset: 0x000F4668
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06002E80 RID: 11904 RVA: 0x000F4670 File Offset: 0x000F4670
		public override int FieldSize
		{
			get
			{
				return SecP384R1Curve.q.BitLength;
			}
		}

		// Token: 0x06002E81 RID: 11905 RVA: 0x000F467C File Offset: 0x000F467C
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecP384R1FieldElement(x);
		}

		// Token: 0x06002E82 RID: 11906 RVA: 0x000F4684 File Offset: 0x000F4684
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecP384R1Point(this, x, y, withCompression);
		}

		// Token: 0x06002E83 RID: 11907 RVA: 0x000F4690 File Offset: 0x000F4690
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecP384R1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x06002E84 RID: 11908 RVA: 0x000F46A0 File Offset: 0x000F46A0
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			uint[] array = new uint[len * 12 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat.Copy(12, ((SecP384R1FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 12;
				Nat.Copy(12, ((SecP384R1FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 12;
			}
			return new SecP384R1Curve.SecP384R1LookupTable(this, array, len);
		}

		// Token: 0x04001BF4 RID: 7156
		private const int SECP384R1_DEFAULT_COORDS = 2;

		// Token: 0x04001BF5 RID: 7157
		private const int SECP384R1_FE_INTS = 12;

		// Token: 0x04001BF6 RID: 7158
		public static readonly BigInteger q = SecP384R1FieldElement.Q;

		// Token: 0x04001BF7 RID: 7159
		private static readonly ECFieldElement[] SECP384R1_AFFINE_ZS = new ECFieldElement[]
		{
			new SecP384R1FieldElement(BigInteger.One)
		};

		// Token: 0x04001BF8 RID: 7160
		protected readonly SecP384R1Point m_infinity;

		// Token: 0x02000E36 RID: 3638
		private class SecP384R1LookupTable : AbstractECLookupTable
		{
			// Token: 0x06008CB4 RID: 36020 RVA: 0x002A3924 File Offset: 0x002A3924
			internal SecP384R1LookupTable(SecP384R1Curve outer, uint[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17001D8D RID: 7565
			// (get) Token: 0x06008CB5 RID: 36021 RVA: 0x002A3944 File Offset: 0x002A3944
			public override int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06008CB6 RID: 36022 RVA: 0x002A394C File Offset: 0x002A394C
			public override ECPoint Lookup(int index)
			{
				uint[] array = Nat.Create(12);
				uint[] array2 = Nat.Create(12);
				int num = 0;
				for (int i = 0; i < this.m_size; i++)
				{
					uint num2 = (uint)((i ^ index) - 1 >> 31);
					for (int j = 0; j < 12; j++)
					{
						uint[] array3;
						IntPtr intPtr;
						(array3 = array)[(int)(intPtr = (IntPtr)j)] = (array3[(int)intPtr] ^ (this.m_table[num + j] & num2));
						(array3 = array2)[(int)(intPtr = (IntPtr)j)] = (array3[(int)intPtr] ^ (this.m_table[num + 12 + j] & num2));
					}
					num += 24;
				}
				return this.CreatePoint(array, array2);
			}

			// Token: 0x06008CB7 RID: 36023 RVA: 0x002A39F0 File Offset: 0x002A39F0
			public override ECPoint LookupVar(int index)
			{
				uint[] array = Nat.Create(12);
				uint[] array2 = Nat.Create(12);
				int num = index * 12 * 2;
				for (int i = 0; i < 12; i++)
				{
					array[i] = this.m_table[num + i];
					array2[i] = this.m_table[num + 12 + i];
				}
				return this.CreatePoint(array, array2);
			}

			// Token: 0x06008CB8 RID: 36024 RVA: 0x002A3A50 File Offset: 0x002A3A50
			private ECPoint CreatePoint(uint[] x, uint[] y)
			{
				return this.m_outer.CreateRawPoint(new SecP384R1FieldElement(x), new SecP384R1FieldElement(y), SecP384R1Curve.SECP384R1_AFFINE_ZS, false);
			}

			// Token: 0x040041C0 RID: 16832
			private readonly SecP384R1Curve m_outer;

			// Token: 0x040041C1 RID: 16833
			private readonly uint[] m_table;

			// Token: 0x040041C2 RID: 16834
			private readonly int m_size;
		}
	}
}
