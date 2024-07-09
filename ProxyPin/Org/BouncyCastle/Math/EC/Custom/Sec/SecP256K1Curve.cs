using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x0200059E RID: 1438
	internal class SecP256K1Curve : AbstractFpCurve
	{
		// Token: 0x06002E01 RID: 11777 RVA: 0x000F229C File Offset: 0x000F229C
		public SecP256K1Curve() : base(SecP256K1Curve.q)
		{
			this.m_infinity = new SecP256K1Point(this, null, null);
			this.m_a = this.FromBigInteger(BigInteger.Zero);
			this.m_b = this.FromBigInteger(BigInteger.ValueOf(7L));
			this.m_order = new BigInteger(1, Hex.DecodeStrict("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEBAAEDCE6AF48A03BBFD25E8CD0364141"));
			this.m_cofactor = BigInteger.One;
			this.m_coord = 2;
		}

		// Token: 0x06002E02 RID: 11778 RVA: 0x000F2314 File Offset: 0x000F2314
		protected override ECCurve CloneCurve()
		{
			return new SecP256K1Curve();
		}

		// Token: 0x06002E03 RID: 11779 RVA: 0x000F231C File Offset: 0x000F231C
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 2;
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06002E04 RID: 11780 RVA: 0x000F233C File Offset: 0x000F233C
		public virtual BigInteger Q
		{
			get
			{
				return SecP256K1Curve.q;
			}
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x06002E05 RID: 11781 RVA: 0x000F2344 File Offset: 0x000F2344
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x06002E06 RID: 11782 RVA: 0x000F234C File Offset: 0x000F234C
		public override int FieldSize
		{
			get
			{
				return SecP256K1Curve.q.BitLength;
			}
		}

		// Token: 0x06002E07 RID: 11783 RVA: 0x000F2358 File Offset: 0x000F2358
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecP256K1FieldElement(x);
		}

		// Token: 0x06002E08 RID: 11784 RVA: 0x000F2360 File Offset: 0x000F2360
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecP256K1Point(this, x, y, withCompression);
		}

		// Token: 0x06002E09 RID: 11785 RVA: 0x000F236C File Offset: 0x000F236C
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecP256K1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x06002E0A RID: 11786 RVA: 0x000F237C File Offset: 0x000F237C
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			uint[] array = new uint[len * 8 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat256.Copy(((SecP256K1FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 8;
				Nat256.Copy(((SecP256K1FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 8;
			}
			return new SecP256K1Curve.SecP256K1LookupTable(this, array, len);
		}

		// Token: 0x04001BDC RID: 7132
		private const int SECP256K1_DEFAULT_COORDS = 2;

		// Token: 0x04001BDD RID: 7133
		private const int SECP256K1_FE_INTS = 8;

		// Token: 0x04001BDE RID: 7134
		public static readonly BigInteger q = SecP256K1FieldElement.Q;

		// Token: 0x04001BDF RID: 7135
		private static readonly ECFieldElement[] SECP256K1_AFFINE_ZS = new ECFieldElement[]
		{
			new SecP256K1FieldElement(BigInteger.One)
		};

		// Token: 0x04001BE0 RID: 7136
		protected readonly SecP256K1Point m_infinity;

		// Token: 0x02000E34 RID: 3636
		private class SecP256K1LookupTable : AbstractECLookupTable
		{
			// Token: 0x06008CAA RID: 36010 RVA: 0x002A36AC File Offset: 0x002A36AC
			internal SecP256K1LookupTable(SecP256K1Curve outer, uint[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17001D8B RID: 7563
			// (get) Token: 0x06008CAB RID: 36011 RVA: 0x002A36CC File Offset: 0x002A36CC
			public override int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06008CAC RID: 36012 RVA: 0x002A36D4 File Offset: 0x002A36D4
			public override ECPoint Lookup(int index)
			{
				uint[] array = Nat256.Create();
				uint[] array2 = Nat256.Create();
				int num = 0;
				for (int i = 0; i < this.m_size; i++)
				{
					uint num2 = (uint)((i ^ index) - 1 >> 31);
					for (int j = 0; j < 8; j++)
					{
						uint[] array3;
						IntPtr intPtr;
						(array3 = array)[(int)(intPtr = (IntPtr)j)] = (array3[(int)intPtr] ^ (this.m_table[num + j] & num2));
						(array3 = array2)[(int)(intPtr = (IntPtr)j)] = (array3[(int)intPtr] ^ (this.m_table[num + 8 + j] & num2));
					}
					num += 16;
				}
				return this.CreatePoint(array, array2);
			}

			// Token: 0x06008CAD RID: 36013 RVA: 0x002A3770 File Offset: 0x002A3770
			public override ECPoint LookupVar(int index)
			{
				uint[] array = Nat256.Create();
				uint[] array2 = Nat256.Create();
				int num = index * 8 * 2;
				for (int i = 0; i < 8; i++)
				{
					array[i] = this.m_table[num + i];
					array2[i] = this.m_table[num + 8 + i];
				}
				return this.CreatePoint(array, array2);
			}

			// Token: 0x06008CAE RID: 36014 RVA: 0x002A37C8 File Offset: 0x002A37C8
			private ECPoint CreatePoint(uint[] x, uint[] y)
			{
				return this.m_outer.CreateRawPoint(new SecP256K1FieldElement(x), new SecP256K1FieldElement(y), SecP256K1Curve.SECP256K1_AFFINE_ZS, false);
			}

			// Token: 0x040041BA RID: 16826
			private readonly SecP256K1Curve m_outer;

			// Token: 0x040041BB RID: 16827
			private readonly uint[] m_table;

			// Token: 0x040041BC RID: 16828
			private readonly int m_size;
		}
	}
}
