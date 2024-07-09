using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000592 RID: 1426
	internal class SecP192R1Curve : AbstractFpCurve
	{
		// Token: 0x06002D44 RID: 11588 RVA: 0x000EEB78 File Offset: 0x000EEB78
		public SecP192R1Curve() : base(SecP192R1Curve.q)
		{
			this.m_infinity = new SecP192R1Point(this, null, null);
			this.m_a = this.FromBigInteger(new BigInteger(1, Hex.DecodeStrict("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFFFFFFFFFFFC")));
			this.m_b = this.FromBigInteger(new BigInteger(1, Hex.DecodeStrict("64210519E59C80E70FA7E9AB72243049FEB8DEECC146B9B1")));
			this.m_order = new BigInteger(1, Hex.DecodeStrict("FFFFFFFFFFFFFFFFFFFFFFFF99DEF836146BC9B1B4D22831"));
			this.m_cofactor = BigInteger.One;
			this.m_coord = 2;
		}

		// Token: 0x06002D45 RID: 11589 RVA: 0x000EEC04 File Offset: 0x000EEC04
		protected override ECCurve CloneCurve()
		{
			return new SecP192R1Curve();
		}

		// Token: 0x06002D46 RID: 11590 RVA: 0x000EEC0C File Offset: 0x000EEC0C
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 2;
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x06002D47 RID: 11591 RVA: 0x000EEC2C File Offset: 0x000EEC2C
		public virtual BigInteger Q
		{
			get
			{
				return SecP192R1Curve.q;
			}
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x06002D48 RID: 11592 RVA: 0x000EEC34 File Offset: 0x000EEC34
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x06002D49 RID: 11593 RVA: 0x000EEC3C File Offset: 0x000EEC3C
		public override int FieldSize
		{
			get
			{
				return SecP192R1Curve.q.BitLength;
			}
		}

		// Token: 0x06002D4A RID: 11594 RVA: 0x000EEC48 File Offset: 0x000EEC48
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecP192R1FieldElement(x);
		}

		// Token: 0x06002D4B RID: 11595 RVA: 0x000EEC50 File Offset: 0x000EEC50
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecP192R1Point(this, x, y, withCompression);
		}

		// Token: 0x06002D4C RID: 11596 RVA: 0x000EEC5C File Offset: 0x000EEC5C
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecP192R1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x06002D4D RID: 11597 RVA: 0x000EEC6C File Offset: 0x000EEC6C
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			uint[] array = new uint[len * 6 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat192.Copy(((SecP192R1FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 6;
				Nat192.Copy(((SecP192R1FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 6;
			}
			return new SecP192R1Curve.SecP192R1LookupTable(this, array, len);
		}

		// Token: 0x04001BB6 RID: 7094
		private const int SECP192R1_DEFAULT_COORDS = 2;

		// Token: 0x04001BB7 RID: 7095
		private const int SECP192R1_FE_INTS = 6;

		// Token: 0x04001BB8 RID: 7096
		public static readonly BigInteger q = SecP192R1FieldElement.Q;

		// Token: 0x04001BB9 RID: 7097
		private static readonly ECFieldElement[] SECP192R1_AFFINE_ZS = new ECFieldElement[]
		{
			new SecP192R1FieldElement(BigInteger.One)
		};

		// Token: 0x04001BBA RID: 7098
		protected readonly SecP192R1Point m_infinity;

		// Token: 0x02000E31 RID: 3633
		private class SecP192R1LookupTable : AbstractECLookupTable
		{
			// Token: 0x06008C9B RID: 35995 RVA: 0x002A32F8 File Offset: 0x002A32F8
			internal SecP192R1LookupTable(SecP192R1Curve outer, uint[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17001D88 RID: 7560
			// (get) Token: 0x06008C9C RID: 35996 RVA: 0x002A3318 File Offset: 0x002A3318
			public override int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06008C9D RID: 35997 RVA: 0x002A3320 File Offset: 0x002A3320
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

			// Token: 0x06008C9E RID: 35998 RVA: 0x002A33BC File Offset: 0x002A33BC
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

			// Token: 0x06008C9F RID: 35999 RVA: 0x002A3414 File Offset: 0x002A3414
			private ECPoint CreatePoint(uint[] x, uint[] y)
			{
				return this.m_outer.CreateRawPoint(new SecP192R1FieldElement(x), new SecP192R1FieldElement(y), SecP192R1Curve.SECP192R1_AFFINE_ZS, false);
			}

			// Token: 0x040041B1 RID: 16817
			private readonly SecP192R1Curve m_outer;

			// Token: 0x040041B2 RID: 16818
			private readonly uint[] m_table;

			// Token: 0x040041B3 RID: 16819
			private readonly int m_size;
		}
	}
}
