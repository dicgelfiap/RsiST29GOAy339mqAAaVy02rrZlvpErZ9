using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000580 RID: 1408
	internal class SecP128R1Curve : AbstractFpCurve
	{
		// Token: 0x06002C3E RID: 11326 RVA: 0x000EA0C0 File Offset: 0x000EA0C0
		public SecP128R1Curve() : base(SecP128R1Curve.q)
		{
			this.m_infinity = new SecP128R1Point(this, null, null);
			this.m_a = this.FromBigInteger(new BigInteger(1, Hex.DecodeStrict("FFFFFFFDFFFFFFFFFFFFFFFFFFFFFFFC")));
			this.m_b = this.FromBigInteger(new BigInteger(1, Hex.DecodeStrict("E87579C11079F43DD824993C2CEE5ED3")));
			this.m_order = new BigInteger(1, Hex.DecodeStrict("FFFFFFFE0000000075A30D1B9038A115"));
			this.m_cofactor = BigInteger.One;
			this.m_coord = 2;
		}

		// Token: 0x06002C3F RID: 11327 RVA: 0x000EA14C File Offset: 0x000EA14C
		protected override ECCurve CloneCurve()
		{
			return new SecP128R1Curve();
		}

		// Token: 0x06002C40 RID: 11328 RVA: 0x000EA154 File Offset: 0x000EA154
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 2;
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06002C41 RID: 11329 RVA: 0x000EA174 File Offset: 0x000EA174
		public virtual BigInteger Q
		{
			get
			{
				return SecP128R1Curve.q;
			}
		}

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06002C42 RID: 11330 RVA: 0x000EA17C File Offset: 0x000EA17C
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06002C43 RID: 11331 RVA: 0x000EA184 File Offset: 0x000EA184
		public override int FieldSize
		{
			get
			{
				return SecP128R1Curve.q.BitLength;
			}
		}

		// Token: 0x06002C44 RID: 11332 RVA: 0x000EA190 File Offset: 0x000EA190
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecP128R1FieldElement(x);
		}

		// Token: 0x06002C45 RID: 11333 RVA: 0x000EA198 File Offset: 0x000EA198
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecP128R1Point(this, x, y, withCompression);
		}

		// Token: 0x06002C46 RID: 11334 RVA: 0x000EA1A4 File Offset: 0x000EA1A4
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecP128R1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x06002C47 RID: 11335 RVA: 0x000EA1B4 File Offset: 0x000EA1B4
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			uint[] array = new uint[len * 4 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat128.Copy(((SecP128R1FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 4;
				Nat128.Copy(((SecP128R1FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 4;
			}
			return new SecP128R1Curve.SecP128R1LookupTable(this, array, len);
		}

		// Token: 0x04001B7E RID: 7038
		private const int SECP128R1_DEFAULT_COORDS = 2;

		// Token: 0x04001B7F RID: 7039
		private const int SECP128R1_FE_INTS = 4;

		// Token: 0x04001B80 RID: 7040
		public static readonly BigInteger q = SecP128R1FieldElement.Q;

		// Token: 0x04001B81 RID: 7041
		private static readonly ECFieldElement[] SECP128R1_AFFINE_ZS = new ECFieldElement[]
		{
			new SecP128R1FieldElement(BigInteger.One)
		};

		// Token: 0x04001B82 RID: 7042
		protected readonly SecP128R1Point m_infinity;

		// Token: 0x02000E2C RID: 3628
		private class SecP128R1LookupTable : AbstractECLookupTable
		{
			// Token: 0x06008C82 RID: 35970 RVA: 0x002A2CCC File Offset: 0x002A2CCC
			internal SecP128R1LookupTable(SecP128R1Curve outer, uint[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17001D83 RID: 7555
			// (get) Token: 0x06008C83 RID: 35971 RVA: 0x002A2CEC File Offset: 0x002A2CEC
			public override int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06008C84 RID: 35972 RVA: 0x002A2CF4 File Offset: 0x002A2CF4
			public override ECPoint Lookup(int index)
			{
				uint[] array = Nat128.Create();
				uint[] array2 = Nat128.Create();
				int num = 0;
				for (int i = 0; i < this.m_size; i++)
				{
					uint num2 = (uint)((i ^ index) - 1 >> 31);
					for (int j = 0; j < 4; j++)
					{
						uint[] array3;
						IntPtr intPtr;
						(array3 = array)[(int)(intPtr = (IntPtr)j)] = (array3[(int)intPtr] ^ (this.m_table[num + j] & num2));
						(array3 = array2)[(int)(intPtr = (IntPtr)j)] = (array3[(int)intPtr] ^ (this.m_table[num + 4 + j] & num2));
					}
					num += 8;
				}
				return this.CreatePoint(array, array2);
			}

			// Token: 0x06008C85 RID: 35973 RVA: 0x002A2D90 File Offset: 0x002A2D90
			public override ECPoint LookupVar(int index)
			{
				uint[] array = Nat128.Create();
				uint[] array2 = Nat128.Create();
				int num = index * 4 * 2;
				for (int i = 0; i < 4; i++)
				{
					array[i] = this.m_table[num + i];
					array2[i] = this.m_table[num + 4 + i];
				}
				return this.CreatePoint(array, array2);
			}

			// Token: 0x06008C86 RID: 35974 RVA: 0x002A2DE8 File Offset: 0x002A2DE8
			private ECPoint CreatePoint(uint[] x, uint[] y)
			{
				return this.m_outer.CreateRawPoint(new SecP128R1FieldElement(x), new SecP128R1FieldElement(y), SecP128R1Curve.SECP128R1_AFFINE_ZS, false);
			}

			// Token: 0x040041A2 RID: 16802
			private readonly SecP128R1Curve m_outer;

			// Token: 0x040041A3 RID: 16803
			private readonly uint[] m_table;

			// Token: 0x040041A4 RID: 16804
			private readonly int m_size;
		}
	}
}
