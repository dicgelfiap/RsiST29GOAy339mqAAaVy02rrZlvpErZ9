using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x020005A2 RID: 1442
	internal class SecP256R1Curve : AbstractFpCurve
	{
		// Token: 0x06002E3D RID: 11837 RVA: 0x000F330C File Offset: 0x000F330C
		public SecP256R1Curve() : base(SecP256R1Curve.q)
		{
			this.m_infinity = new SecP256R1Point(this, null, null);
			this.m_a = this.FromBigInteger(new BigInteger(1, Hex.DecodeStrict("FFFFFFFF00000001000000000000000000000000FFFFFFFFFFFFFFFFFFFFFFFC")));
			this.m_b = this.FromBigInteger(new BigInteger(1, Hex.DecodeStrict("5AC635D8AA3A93E7B3EBBD55769886BC651D06B0CC53B0F63BCE3C3E27D2604B")));
			this.m_order = new BigInteger(1, Hex.DecodeStrict("FFFFFFFF00000000FFFFFFFFFFFFFFFFBCE6FAADA7179E84F3B9CAC2FC632551"));
			this.m_cofactor = BigInteger.One;
			this.m_coord = 2;
		}

		// Token: 0x06002E3E RID: 11838 RVA: 0x000F3398 File Offset: 0x000F3398
		protected override ECCurve CloneCurve()
		{
			return new SecP256R1Curve();
		}

		// Token: 0x06002E3F RID: 11839 RVA: 0x000F33A0 File Offset: 0x000F33A0
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 2;
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06002E40 RID: 11840 RVA: 0x000F33C0 File Offset: 0x000F33C0
		public virtual BigInteger Q
		{
			get
			{
				return SecP256R1Curve.q;
			}
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06002E41 RID: 11841 RVA: 0x000F33C8 File Offset: 0x000F33C8
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06002E42 RID: 11842 RVA: 0x000F33D0 File Offset: 0x000F33D0
		public override int FieldSize
		{
			get
			{
				return SecP256R1Curve.q.BitLength;
			}
		}

		// Token: 0x06002E43 RID: 11843 RVA: 0x000F33DC File Offset: 0x000F33DC
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecP256R1FieldElement(x);
		}

		// Token: 0x06002E44 RID: 11844 RVA: 0x000F33E4 File Offset: 0x000F33E4
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecP256R1Point(this, x, y, withCompression);
		}

		// Token: 0x06002E45 RID: 11845 RVA: 0x000F33F0 File Offset: 0x000F33F0
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecP256R1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x06002E46 RID: 11846 RVA: 0x000F3400 File Offset: 0x000F3400
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			uint[] array = new uint[len * 8 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat256.Copy(((SecP256R1FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 8;
				Nat256.Copy(((SecP256R1FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 8;
			}
			return new SecP256R1Curve.SecP256R1LookupTable(this, array, len);
		}

		// Token: 0x04001BE9 RID: 7145
		private const int SECP256R1_DEFAULT_COORDS = 2;

		// Token: 0x04001BEA RID: 7146
		private const int SECP256R1_FE_INTS = 8;

		// Token: 0x04001BEB RID: 7147
		public static readonly BigInteger q = SecP256R1FieldElement.Q;

		// Token: 0x04001BEC RID: 7148
		private static readonly ECFieldElement[] SECP256R1_AFFINE_ZS = new ECFieldElement[]
		{
			new SecP256R1FieldElement(BigInteger.One)
		};

		// Token: 0x04001BED RID: 7149
		protected readonly SecP256R1Point m_infinity;

		// Token: 0x02000E35 RID: 3637
		private class SecP256R1LookupTable : AbstractECLookupTable
		{
			// Token: 0x06008CAF RID: 36015 RVA: 0x002A37E8 File Offset: 0x002A37E8
			internal SecP256R1LookupTable(SecP256R1Curve outer, uint[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17001D8C RID: 7564
			// (get) Token: 0x06008CB0 RID: 36016 RVA: 0x002A3808 File Offset: 0x002A3808
			public override int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06008CB1 RID: 36017 RVA: 0x002A3810 File Offset: 0x002A3810
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

			// Token: 0x06008CB2 RID: 36018 RVA: 0x002A38AC File Offset: 0x002A38AC
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

			// Token: 0x06008CB3 RID: 36019 RVA: 0x002A3904 File Offset: 0x002A3904
			private ECPoint CreatePoint(uint[] x, uint[] y)
			{
				return this.m_outer.CreateRawPoint(new SecP256R1FieldElement(x), new SecP256R1FieldElement(y), SecP256R1Curve.SECP256R1_AFFINE_ZS, false);
			}

			// Token: 0x040041BD RID: 16829
			private readonly SecP256R1Curve m_outer;

			// Token: 0x040041BE RID: 16830
			private readonly uint[] m_table;

			// Token: 0x040041BF RID: 16831
			private readonly int m_size;
		}
	}
}
