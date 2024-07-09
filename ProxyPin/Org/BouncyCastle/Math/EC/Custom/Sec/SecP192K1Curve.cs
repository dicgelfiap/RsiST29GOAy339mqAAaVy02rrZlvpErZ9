using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x0200058E RID: 1422
	internal class SecP192K1Curve : AbstractFpCurve
	{
		// Token: 0x06002D08 RID: 11528 RVA: 0x000EDB18 File Offset: 0x000EDB18
		public SecP192K1Curve() : base(SecP192K1Curve.q)
		{
			this.m_infinity = new SecP192K1Point(this, null, null);
			this.m_a = this.FromBigInteger(BigInteger.Zero);
			this.m_b = this.FromBigInteger(BigInteger.ValueOf(3L));
			this.m_order = new BigInteger(1, Hex.DecodeStrict("FFFFFFFFFFFFFFFFFFFFFFFE26F2FC170F69466A74DEFD8D"));
			this.m_cofactor = BigInteger.One;
			this.m_coord = 2;
		}

		// Token: 0x06002D09 RID: 11529 RVA: 0x000EDB90 File Offset: 0x000EDB90
		protected override ECCurve CloneCurve()
		{
			return new SecP192K1Curve();
		}

		// Token: 0x06002D0A RID: 11530 RVA: 0x000EDB98 File Offset: 0x000EDB98
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 2;
		}

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x06002D0B RID: 11531 RVA: 0x000EDBB8 File Offset: 0x000EDBB8
		public virtual BigInteger Q
		{
			get
			{
				return SecP192K1Curve.q;
			}
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06002D0C RID: 11532 RVA: 0x000EDBC0 File Offset: 0x000EDBC0
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06002D0D RID: 11533 RVA: 0x000EDBC8 File Offset: 0x000EDBC8
		public override int FieldSize
		{
			get
			{
				return SecP192K1Curve.q.BitLength;
			}
		}

		// Token: 0x06002D0E RID: 11534 RVA: 0x000EDBD4 File Offset: 0x000EDBD4
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecP192K1FieldElement(x);
		}

		// Token: 0x06002D0F RID: 11535 RVA: 0x000EDBDC File Offset: 0x000EDBDC
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecP192K1Point(this, x, y, withCompression);
		}

		// Token: 0x06002D10 RID: 11536 RVA: 0x000EDBE8 File Offset: 0x000EDBE8
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecP192K1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x06002D11 RID: 11537 RVA: 0x000EDBF8 File Offset: 0x000EDBF8
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			uint[] array = new uint[len * 6 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat192.Copy(((SecP192K1FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 6;
				Nat192.Copy(((SecP192K1FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 6;
			}
			return new SecP192K1Curve.SecP192K1LookupTable(this, array, len);
		}

		// Token: 0x04001BA9 RID: 7081
		private const int SECP192K1_DEFAULT_COORDS = 2;

		// Token: 0x04001BAA RID: 7082
		private const int SECP192K1_FE_INTS = 6;

		// Token: 0x04001BAB RID: 7083
		public static readonly BigInteger q = SecP192K1FieldElement.Q;

		// Token: 0x04001BAC RID: 7084
		private static readonly ECFieldElement[] SECP192K1_AFFINE_ZS = new ECFieldElement[]
		{
			new SecP192K1FieldElement(BigInteger.One)
		};

		// Token: 0x04001BAD RID: 7085
		protected readonly SecP192K1Point m_infinity;

		// Token: 0x02000E30 RID: 3632
		private class SecP192K1LookupTable : AbstractECLookupTable
		{
			// Token: 0x06008C96 RID: 35990 RVA: 0x002A31BC File Offset: 0x002A31BC
			internal SecP192K1LookupTable(SecP192K1Curve outer, uint[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17001D87 RID: 7559
			// (get) Token: 0x06008C97 RID: 35991 RVA: 0x002A31DC File Offset: 0x002A31DC
			public override int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06008C98 RID: 35992 RVA: 0x002A31E4 File Offset: 0x002A31E4
			public override ECPoint Lookup(int index)
			{
				uint[] array = Nat192.Create();
				uint[] array2 = Nat192.Create();
				int num = 0;
				for (int i = 0; i < this.m_size; i++)
				{
					uint num2 = (uint)((i ^ index) - 1 >> 31);
					for (int j = 0; j < 6; j++)
					{
						uint[] array3;
						IntPtr intPtr;
						(array3 = array)[(int)(intPtr = (IntPtr)j)] = (array3[(int)intPtr] ^ (this.m_table[num + j] & num2));
						(array3 = array2)[(int)(intPtr = (IntPtr)j)] = (array3[(int)intPtr] ^ (this.m_table[num + 6 + j] & num2));
					}
					num += 12;
				}
				return this.CreatePoint(array, array2);
			}

			// Token: 0x06008C99 RID: 35993 RVA: 0x002A3280 File Offset: 0x002A3280
			public override ECPoint LookupVar(int index)
			{
				uint[] array = Nat192.Create();
				uint[] array2 = Nat192.Create();
				int num = index * 6 * 2;
				for (int i = 0; i < 6; i++)
				{
					array[i] = this.m_table[num + i];
					array2[i] = this.m_table[num + 6 + i];
				}
				return this.CreatePoint(array, array2);
			}

			// Token: 0x06008C9A RID: 35994 RVA: 0x002A32D8 File Offset: 0x002A32D8
			private ECPoint CreatePoint(uint[] x, uint[] y)
			{
				return this.m_outer.CreateRawPoint(new SecP192K1FieldElement(x), new SecP192K1FieldElement(y), SecP192K1Curve.SECP192K1_AFFINE_ZS, false);
			}

			// Token: 0x040041AE RID: 16814
			private readonly SecP192K1Curve m_outer;

			// Token: 0x040041AF RID: 16815
			private readonly uint[] m_table;

			// Token: 0x040041B0 RID: 16816
			private readonly int m_size;
		}
	}
}
