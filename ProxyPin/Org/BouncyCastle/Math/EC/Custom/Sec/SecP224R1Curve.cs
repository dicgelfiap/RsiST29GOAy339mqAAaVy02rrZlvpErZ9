using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x0200059A RID: 1434
	internal class SecP224R1Curve : AbstractFpCurve
	{
		// Token: 0x06002DBE RID: 11710 RVA: 0x000F0E60 File Offset: 0x000F0E60
		public SecP224R1Curve() : base(SecP224R1Curve.q)
		{
			this.m_infinity = new SecP224R1Point(this, null, null);
			this.m_a = this.FromBigInteger(new BigInteger(1, Hex.DecodeStrict("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFFFFFFFFFFFFFFFFFFFE")));
			this.m_b = this.FromBigInteger(new BigInteger(1, Hex.DecodeStrict("B4050A850C04B3ABF54132565044B0B7D7BFD8BA270B39432355FFB4")));
			this.m_order = new BigInteger(1, Hex.DecodeStrict("FFFFFFFFFFFFFFFFFFFFFFFFFFFF16A2E0B8F03E13DD29455C5C2A3D"));
			this.m_cofactor = BigInteger.One;
			this.m_coord = 2;
		}

		// Token: 0x06002DBF RID: 11711 RVA: 0x000F0EEC File Offset: 0x000F0EEC
		protected override ECCurve CloneCurve()
		{
			return new SecP224R1Curve();
		}

		// Token: 0x06002DC0 RID: 11712 RVA: 0x000F0EF4 File Offset: 0x000F0EF4
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 2;
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x06002DC1 RID: 11713 RVA: 0x000F0F14 File Offset: 0x000F0F14
		public virtual BigInteger Q
		{
			get
			{
				return SecP224R1Curve.q;
			}
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x06002DC2 RID: 11714 RVA: 0x000F0F1C File Offset: 0x000F0F1C
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06002DC3 RID: 11715 RVA: 0x000F0F24 File Offset: 0x000F0F24
		public override int FieldSize
		{
			get
			{
				return SecP224R1Curve.q.BitLength;
			}
		}

		// Token: 0x06002DC4 RID: 11716 RVA: 0x000F0F30 File Offset: 0x000F0F30
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecP224R1FieldElement(x);
		}

		// Token: 0x06002DC5 RID: 11717 RVA: 0x000F0F38 File Offset: 0x000F0F38
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecP224R1Point(this, x, y, withCompression);
		}

		// Token: 0x06002DC6 RID: 11718 RVA: 0x000F0F44 File Offset: 0x000F0F44
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecP224R1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x06002DC7 RID: 11719 RVA: 0x000F0F54 File Offset: 0x000F0F54
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			uint[] array = new uint[len * 7 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat224.Copy(((SecP224R1FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 7;
				Nat224.Copy(((SecP224R1FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 7;
			}
			return new SecP224R1Curve.SecP224R1LookupTable(this, array, len);
		}

		// Token: 0x04001BD0 RID: 7120
		private const int SECP224R1_DEFAULT_COORDS = 2;

		// Token: 0x04001BD1 RID: 7121
		private const int SECP224R1_FE_INTS = 7;

		// Token: 0x04001BD2 RID: 7122
		public static readonly BigInteger q = SecP224R1FieldElement.Q;

		// Token: 0x04001BD3 RID: 7123
		private static readonly ECFieldElement[] SECP224R1_AFFINE_ZS = new ECFieldElement[]
		{
			new SecP224R1FieldElement(BigInteger.One)
		};

		// Token: 0x04001BD4 RID: 7124
		protected readonly SecP224R1Point m_infinity;

		// Token: 0x02000E33 RID: 3635
		private class SecP224R1LookupTable : AbstractECLookupTable
		{
			// Token: 0x06008CA5 RID: 36005 RVA: 0x002A3570 File Offset: 0x002A3570
			internal SecP224R1LookupTable(SecP224R1Curve outer, uint[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17001D8A RID: 7562
			// (get) Token: 0x06008CA6 RID: 36006 RVA: 0x002A3590 File Offset: 0x002A3590
			public override int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06008CA7 RID: 36007 RVA: 0x002A3598 File Offset: 0x002A3598
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

			// Token: 0x06008CA8 RID: 36008 RVA: 0x002A3634 File Offset: 0x002A3634
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

			// Token: 0x06008CA9 RID: 36009 RVA: 0x002A368C File Offset: 0x002A368C
			private ECPoint CreatePoint(uint[] x, uint[] y)
			{
				return this.m_outer.CreateRawPoint(new SecP224R1FieldElement(x), new SecP224R1FieldElement(y), SecP224R1Curve.SECP224R1_AFFINE_ZS, false);
			}

			// Token: 0x040041B7 RID: 16823
			private readonly SecP224R1Curve m_outer;

			// Token: 0x040041B8 RID: 16824
			private readonly uint[] m_table;

			// Token: 0x040041B9 RID: 16825
			private readonly int m_size;
		}
	}
}
