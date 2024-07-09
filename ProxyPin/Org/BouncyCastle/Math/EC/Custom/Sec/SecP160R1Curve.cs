using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000586 RID: 1414
	internal class SecP160R1Curve : AbstractFpCurve
	{
		// Token: 0x06002C90 RID: 11408 RVA: 0x000EB98C File Offset: 0x000EB98C
		public SecP160R1Curve() : base(SecP160R1Curve.q)
		{
			this.m_infinity = new SecP160R1Point(this, null, null);
			this.m_a = this.FromBigInteger(new BigInteger(1, Hex.DecodeStrict("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF7FFFFFFC")));
			this.m_b = this.FromBigInteger(new BigInteger(1, Hex.DecodeStrict("1C97BEFC54BD7A8B65ACF89F81D4D4ADC565FA45")));
			this.m_order = new BigInteger(1, Hex.DecodeStrict("0100000000000000000001F4C8F927AED3CA752257"));
			this.m_cofactor = BigInteger.One;
			this.m_coord = 2;
		}

		// Token: 0x06002C91 RID: 11409 RVA: 0x000EBA18 File Offset: 0x000EBA18
		protected override ECCurve CloneCurve()
		{
			return new SecP160R1Curve();
		}

		// Token: 0x06002C92 RID: 11410 RVA: 0x000EBA20 File Offset: 0x000EBA20
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 2;
		}

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06002C93 RID: 11411 RVA: 0x000EBA40 File Offset: 0x000EBA40
		public virtual BigInteger Q
		{
			get
			{
				return SecP160R1Curve.q;
			}
		}

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x06002C94 RID: 11412 RVA: 0x000EBA48 File Offset: 0x000EBA48
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06002C95 RID: 11413 RVA: 0x000EBA50 File Offset: 0x000EBA50
		public override int FieldSize
		{
			get
			{
				return SecP160R1Curve.q.BitLength;
			}
		}

		// Token: 0x06002C96 RID: 11414 RVA: 0x000EBA5C File Offset: 0x000EBA5C
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecP160R1FieldElement(x);
		}

		// Token: 0x06002C97 RID: 11415 RVA: 0x000EBA64 File Offset: 0x000EBA64
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecP160R1Point(this, x, y, withCompression);
		}

		// Token: 0x06002C98 RID: 11416 RVA: 0x000EBA70 File Offset: 0x000EBA70
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecP160R1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x06002C99 RID: 11417 RVA: 0x000EBA80 File Offset: 0x000EBA80
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			uint[] array = new uint[len * 5 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat160.Copy(((SecP160R1FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 5;
				Nat160.Copy(((SecP160R1FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 5;
			}
			return new SecP160R1Curve.SecP160R1LookupTable(this, array, len);
		}

		// Token: 0x04001B8F RID: 7055
		private const int SECP160R1_DEFAULT_COORDS = 2;

		// Token: 0x04001B90 RID: 7056
		private const int SECP160R1_FE_INTS = 5;

		// Token: 0x04001B91 RID: 7057
		public static readonly BigInteger q = SecP160R1FieldElement.Q;

		// Token: 0x04001B92 RID: 7058
		private static readonly ECFieldElement[] SECP160R1_AFFINE_ZS = new ECFieldElement[]
		{
			new SecP160R1FieldElement(BigInteger.One)
		};

		// Token: 0x04001B93 RID: 7059
		protected readonly SecP160R1Point m_infinity;

		// Token: 0x02000E2E RID: 3630
		private class SecP160R1LookupTable : AbstractECLookupTable
		{
			// Token: 0x06008C8C RID: 35980 RVA: 0x002A2F44 File Offset: 0x002A2F44
			internal SecP160R1LookupTable(SecP160R1Curve outer, uint[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17001D85 RID: 7557
			// (get) Token: 0x06008C8D RID: 35981 RVA: 0x002A2F64 File Offset: 0x002A2F64
			public override int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06008C8E RID: 35982 RVA: 0x002A2F6C File Offset: 0x002A2F6C
			public override ECPoint Lookup(int index)
			{
				uint[] array = Nat160.Create();
				uint[] array2 = Nat160.Create();
				int num = 0;
				for (int i = 0; i < this.m_size; i++)
				{
					uint num2 = (uint)((i ^ index) - 1 >> 31);
					for (int j = 0; j < 5; j++)
					{
						uint[] array3;
						IntPtr intPtr;
						(array3 = array)[(int)(intPtr = (IntPtr)j)] = (array3[(int)intPtr] ^ (this.m_table[num + j] & num2));
						(array3 = array2)[(int)(intPtr = (IntPtr)j)] = (array3[(int)intPtr] ^ (this.m_table[num + 5 + j] & num2));
					}
					num += 10;
				}
				return this.CreatePoint(array, array2);
			}

			// Token: 0x06008C8F RID: 35983 RVA: 0x002A3008 File Offset: 0x002A3008
			public override ECPoint LookupVar(int index)
			{
				uint[] array = Nat160.Create();
				uint[] array2 = Nat160.Create();
				int num = index * 5 * 2;
				for (int i = 0; i < 5; i++)
				{
					array[i] = this.m_table[num + i];
					array2[i] = this.m_table[num + 5 + i];
				}
				return this.CreatePoint(array, array2);
			}

			// Token: 0x06008C90 RID: 35984 RVA: 0x002A3060 File Offset: 0x002A3060
			private ECPoint CreatePoint(uint[] x, uint[] y)
			{
				return this.m_outer.CreateRawPoint(new SecP160R1FieldElement(x), new SecP160R1FieldElement(y), SecP160R1Curve.SECP160R1_AFFINE_ZS, false);
			}

			// Token: 0x040041A8 RID: 16808
			private readonly SecP160R1Curve m_outer;

			// Token: 0x040041A9 RID: 16809
			private readonly uint[] m_table;

			// Token: 0x040041AA RID: 16810
			private readonly int m_size;
		}
	}
}
