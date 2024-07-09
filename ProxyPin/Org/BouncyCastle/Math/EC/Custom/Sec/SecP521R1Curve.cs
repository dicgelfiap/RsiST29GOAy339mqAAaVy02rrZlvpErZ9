using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x020005AA RID: 1450
	internal class SecP521R1Curve : AbstractFpCurve
	{
		// Token: 0x06002EB8 RID: 11960 RVA: 0x000F5980 File Offset: 0x000F5980
		public SecP521R1Curve() : base(SecP521R1Curve.q)
		{
			this.m_infinity = new SecP521R1Point(this, null, null);
			this.m_a = this.FromBigInteger(new BigInteger(1, Hex.DecodeStrict("01FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFC")));
			this.m_b = this.FromBigInteger(new BigInteger(1, Hex.DecodeStrict("0051953EB9618E1C9A1F929A21A0B68540EEA2DA725B99B315F3B8B489918EF109E156193951EC7E937B1652C0BD3BB1BF073573DF883D2C34F1EF451FD46B503F00")));
			this.m_order = new BigInteger(1, Hex.DecodeStrict("01FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFA51868783BF2F966B7FCC0148F709A5D03BB5C9B8899C47AEBB6FB71E91386409"));
			this.m_cofactor = BigInteger.One;
			this.m_coord = 2;
		}

		// Token: 0x06002EB9 RID: 11961 RVA: 0x000F5A0C File Offset: 0x000F5A0C
		protected override ECCurve CloneCurve()
		{
			return new SecP521R1Curve();
		}

		// Token: 0x06002EBA RID: 11962 RVA: 0x000F5A14 File Offset: 0x000F5A14
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 2;
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06002EBB RID: 11963 RVA: 0x000F5A34 File Offset: 0x000F5A34
		public virtual BigInteger Q
		{
			get
			{
				return SecP521R1Curve.q;
			}
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06002EBC RID: 11964 RVA: 0x000F5A3C File Offset: 0x000F5A3C
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06002EBD RID: 11965 RVA: 0x000F5A44 File Offset: 0x000F5A44
		public override int FieldSize
		{
			get
			{
				return SecP521R1Curve.q.BitLength;
			}
		}

		// Token: 0x06002EBE RID: 11966 RVA: 0x000F5A50 File Offset: 0x000F5A50
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecP521R1FieldElement(x);
		}

		// Token: 0x06002EBF RID: 11967 RVA: 0x000F5A58 File Offset: 0x000F5A58
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecP521R1Point(this, x, y, withCompression);
		}

		// Token: 0x06002EC0 RID: 11968 RVA: 0x000F5A64 File Offset: 0x000F5A64
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecP521R1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x06002EC1 RID: 11969 RVA: 0x000F5A74 File Offset: 0x000F5A74
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			uint[] array = new uint[len * 17 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat.Copy(17, ((SecP521R1FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 17;
				Nat.Copy(17, ((SecP521R1FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 17;
			}
			return new SecP521R1Curve.SecP521R1LookupTable(this, array, len);
		}

		// Token: 0x04001C00 RID: 7168
		private const int SECP521R1_DEFAULT_COORDS = 2;

		// Token: 0x04001C01 RID: 7169
		private const int SECP521R1_FE_INTS = 17;

		// Token: 0x04001C02 RID: 7170
		public static readonly BigInteger q = SecP521R1FieldElement.Q;

		// Token: 0x04001C03 RID: 7171
		private static readonly ECFieldElement[] SECP521R1_AFFINE_ZS = new ECFieldElement[]
		{
			new SecP521R1FieldElement(BigInteger.One)
		};

		// Token: 0x04001C04 RID: 7172
		protected readonly SecP521R1Point m_infinity;

		// Token: 0x02000E37 RID: 3639
		private class SecP521R1LookupTable : AbstractECLookupTable
		{
			// Token: 0x06008CB9 RID: 36025 RVA: 0x002A3A70 File Offset: 0x002A3A70
			internal SecP521R1LookupTable(SecP521R1Curve outer, uint[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17001D8E RID: 7566
			// (get) Token: 0x06008CBA RID: 36026 RVA: 0x002A3A90 File Offset: 0x002A3A90
			public override int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06008CBB RID: 36027 RVA: 0x002A3A98 File Offset: 0x002A3A98
			public override ECPoint Lookup(int index)
			{
				uint[] array = Nat.Create(17);
				uint[] array2 = Nat.Create(17);
				int num = 0;
				for (int i = 0; i < this.m_size; i++)
				{
					uint num2 = (uint)((i ^ index) - 1 >> 31);
					for (int j = 0; j < 17; j++)
					{
						uint[] array3;
						IntPtr intPtr;
						(array3 = array)[(int)(intPtr = (IntPtr)j)] = (array3[(int)intPtr] ^ (this.m_table[num + j] & num2));
						(array3 = array2)[(int)(intPtr = (IntPtr)j)] = (array3[(int)intPtr] ^ (this.m_table[num + 17 + j] & num2));
					}
					num += 34;
				}
				return this.CreatePoint(array, array2);
			}

			// Token: 0x06008CBC RID: 36028 RVA: 0x002A3B3C File Offset: 0x002A3B3C
			public override ECPoint LookupVar(int index)
			{
				uint[] array = Nat.Create(17);
				uint[] array2 = Nat.Create(17);
				int num = index * 17 * 2;
				for (int i = 0; i < 17; i++)
				{
					array[i] = this.m_table[num + i];
					array2[i] = this.m_table[num + 17 + i];
				}
				return this.CreatePoint(array, array2);
			}

			// Token: 0x06008CBD RID: 36029 RVA: 0x002A3B9C File Offset: 0x002A3B9C
			private ECPoint CreatePoint(uint[] x, uint[] y)
			{
				return this.m_outer.CreateRawPoint(new SecP521R1FieldElement(x), new SecP521R1FieldElement(y), SecP521R1Curve.SECP521R1_AFFINE_ZS, false);
			}

			// Token: 0x040041C3 RID: 16835
			private readonly SecP521R1Curve m_outer;

			// Token: 0x040041C4 RID: 16836
			private readonly uint[] m_table;

			// Token: 0x040041C5 RID: 16837
			private readonly int m_size;
		}
	}
}
