using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x0200058A RID: 1418
	internal class SecP160R2Curve : AbstractFpCurve
	{
		// Token: 0x06002CCC RID: 11468 RVA: 0x000ECA40 File Offset: 0x000ECA40
		public SecP160R2Curve() : base(SecP160R2Curve.q)
		{
			this.m_infinity = new SecP160R2Point(this, null, null);
			this.m_a = this.FromBigInteger(new BigInteger(1, Hex.DecodeStrict("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFAC70")));
			this.m_b = this.FromBigInteger(new BigInteger(1, Hex.DecodeStrict("B4E134D3FB59EB8BAB57274904664D5AF50388BA")));
			this.m_order = new BigInteger(1, Hex.DecodeStrict("0100000000000000000000351EE786A818F3A1A16B"));
			this.m_cofactor = BigInteger.One;
			this.m_coord = 2;
		}

		// Token: 0x06002CCD RID: 11469 RVA: 0x000ECACC File Offset: 0x000ECACC
		protected override ECCurve CloneCurve()
		{
			return new SecP160R2Curve();
		}

		// Token: 0x06002CCE RID: 11470 RVA: 0x000ECAD4 File Offset: 0x000ECAD4
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 2;
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x06002CCF RID: 11471 RVA: 0x000ECAF4 File Offset: 0x000ECAF4
		public virtual BigInteger Q
		{
			get
			{
				return SecP160R2Curve.q;
			}
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06002CD0 RID: 11472 RVA: 0x000ECAFC File Offset: 0x000ECAFC
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06002CD1 RID: 11473 RVA: 0x000ECB04 File Offset: 0x000ECB04
		public override int FieldSize
		{
			get
			{
				return SecP160R2Curve.q.BitLength;
			}
		}

		// Token: 0x06002CD2 RID: 11474 RVA: 0x000ECB10 File Offset: 0x000ECB10
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecP160R2FieldElement(x);
		}

		// Token: 0x06002CD3 RID: 11475 RVA: 0x000ECB18 File Offset: 0x000ECB18
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecP160R2Point(this, x, y, withCompression);
		}

		// Token: 0x06002CD4 RID: 11476 RVA: 0x000ECB24 File Offset: 0x000ECB24
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecP160R2Point(this, x, y, zs, withCompression);
		}

		// Token: 0x06002CD5 RID: 11477 RVA: 0x000ECB34 File Offset: 0x000ECB34
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
			return new SecP160R2Curve.SecP160R2LookupTable(this, array, len);
		}

		// Token: 0x04001B9C RID: 7068
		private const int SECP160R2_DEFAULT_COORDS = 2;

		// Token: 0x04001B9D RID: 7069
		private const int SECP160R2_FE_INTS = 5;

		// Token: 0x04001B9E RID: 7070
		public static readonly BigInteger q = SecP160R2FieldElement.Q;

		// Token: 0x04001B9F RID: 7071
		private static readonly ECFieldElement[] SECP160R2_AFFINE_ZS = new ECFieldElement[]
		{
			new SecP160R2FieldElement(BigInteger.One)
		};

		// Token: 0x04001BA0 RID: 7072
		protected readonly SecP160R2Point m_infinity;

		// Token: 0x02000E2F RID: 3631
		private class SecP160R2LookupTable : AbstractECLookupTable
		{
			// Token: 0x06008C91 RID: 35985 RVA: 0x002A3080 File Offset: 0x002A3080
			internal SecP160R2LookupTable(SecP160R2Curve outer, uint[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17001D86 RID: 7558
			// (get) Token: 0x06008C92 RID: 35986 RVA: 0x002A30A0 File Offset: 0x002A30A0
			public override int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06008C93 RID: 35987 RVA: 0x002A30A8 File Offset: 0x002A30A8
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

			// Token: 0x06008C94 RID: 35988 RVA: 0x002A3144 File Offset: 0x002A3144
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

			// Token: 0x06008C95 RID: 35989 RVA: 0x002A319C File Offset: 0x002A319C
			private ECPoint CreatePoint(uint[] x, uint[] y)
			{
				return this.m_outer.CreateRawPoint(new SecP160R2FieldElement(x), new SecP160R2FieldElement(y), SecP160R2Curve.SECP160R2_AFFINE_ZS, false);
			}

			// Token: 0x040041AB RID: 16811
			private readonly SecP160R2Curve m_outer;

			// Token: 0x040041AC RID: 16812
			private readonly uint[] m_table;

			// Token: 0x040041AD RID: 16813
			private readonly int m_size;
		}
	}
}
