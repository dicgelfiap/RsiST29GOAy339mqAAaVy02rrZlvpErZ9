using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000584 RID: 1412
	internal class SecP160K1Curve : AbstractFpCurve
	{
		// Token: 0x06002C7C RID: 11388 RVA: 0x000EB1E4 File Offset: 0x000EB1E4
		public SecP160K1Curve() : base(SecP160K1Curve.q)
		{
			this.m_infinity = new SecP160K1Point(this, null, null);
			this.m_a = this.FromBigInteger(BigInteger.Zero);
			this.m_b = this.FromBigInteger(BigInteger.ValueOf(7L));
			this.m_order = new BigInteger(1, Hex.DecodeStrict("0100000000000000000001B8FA16DFAB9ACA16B6B3"));
			this.m_cofactor = BigInteger.One;
			this.m_coord = 2;
		}

		// Token: 0x06002C7D RID: 11389 RVA: 0x000EB25C File Offset: 0x000EB25C
		protected override ECCurve CloneCurve()
		{
			return new SecP160K1Curve();
		}

		// Token: 0x06002C7E RID: 11390 RVA: 0x000EB264 File Offset: 0x000EB264
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 2;
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x06002C7F RID: 11391 RVA: 0x000EB284 File Offset: 0x000EB284
		public virtual BigInteger Q
		{
			get
			{
				return SecP160K1Curve.q;
			}
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x06002C80 RID: 11392 RVA: 0x000EB28C File Offset: 0x000EB28C
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x06002C81 RID: 11393 RVA: 0x000EB294 File Offset: 0x000EB294
		public override int FieldSize
		{
			get
			{
				return SecP160K1Curve.q.BitLength;
			}
		}

		// Token: 0x06002C82 RID: 11394 RVA: 0x000EB2A0 File Offset: 0x000EB2A0
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecP160R2FieldElement(x);
		}

		// Token: 0x06002C83 RID: 11395 RVA: 0x000EB2A8 File Offset: 0x000EB2A8
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecP160K1Point(this, x, y, withCompression);
		}

		// Token: 0x06002C84 RID: 11396 RVA: 0x000EB2B4 File Offset: 0x000EB2B4
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecP160K1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x06002C85 RID: 11397 RVA: 0x000EB2C4 File Offset: 0x000EB2C4
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			uint[] array = new uint[len * 5 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat160.Copy(((SecP160R2FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 5;
				Nat160.Copy(((SecP160R2FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 5;
			}
			return new SecP160K1Curve.SecP160K1LookupTable(this, array, len);
		}

		// Token: 0x04001B8A RID: 7050
		private const int SECP160K1_DEFAULT_COORDS = 2;

		// Token: 0x04001B8B RID: 7051
		private const int SECP160K1_FE_INTS = 5;

		// Token: 0x04001B8C RID: 7052
		public static readonly BigInteger q = SecP160R2FieldElement.Q;

		// Token: 0x04001B8D RID: 7053
		private static readonly ECFieldElement[] SECP160K1_AFFINE_ZS = new ECFieldElement[]
		{
			new SecP160R2FieldElement(BigInteger.One)
		};

		// Token: 0x04001B8E RID: 7054
		protected readonly SecP160K1Point m_infinity;

		// Token: 0x02000E2D RID: 3629
		private class SecP160K1LookupTable : AbstractECLookupTable
		{
			// Token: 0x06008C87 RID: 35975 RVA: 0x002A2E08 File Offset: 0x002A2E08
			internal SecP160K1LookupTable(SecP160K1Curve outer, uint[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17001D84 RID: 7556
			// (get) Token: 0x06008C88 RID: 35976 RVA: 0x002A2E28 File Offset: 0x002A2E28
			public override int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06008C89 RID: 35977 RVA: 0x002A2E30 File Offset: 0x002A2E30
			public override ECPoint Lookup(int index)
			{
				uint[] array = Nat256.Create();
				uint[] array2 = Nat256.Create();
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

			// Token: 0x06008C8A RID: 35978 RVA: 0x002A2ECC File Offset: 0x002A2ECC
			public override ECPoint LookupVar(int index)
			{
				uint[] array = Nat256.Create();
				uint[] array2 = Nat256.Create();
				int num = index * 5 * 2;
				for (int i = 0; i < 5; i++)
				{
					array[i] = this.m_table[num + i];
					array2[i] = this.m_table[num + 5 + i];
				}
				return this.CreatePoint(array, array2);
			}

			// Token: 0x06008C8B RID: 35979 RVA: 0x002A2F24 File Offset: 0x002A2F24
			private ECPoint CreatePoint(uint[] x, uint[] y)
			{
				return this.m_outer.CreateRawPoint(new SecP160R2FieldElement(x), new SecP160R2FieldElement(y), SecP160K1Curve.SECP160K1_AFFINE_ZS, false);
			}

			// Token: 0x040041A5 RID: 16805
			private readonly SecP160K1Curve m_outer;

			// Token: 0x040041A6 RID: 16806
			private readonly uint[] m_table;

			// Token: 0x040041A7 RID: 16807
			private readonly int m_size;
		}
	}
}
