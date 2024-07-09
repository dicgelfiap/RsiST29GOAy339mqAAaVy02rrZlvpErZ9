using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Math.EC.Custom.GM
{
	// Token: 0x0200057C RID: 1404
	internal class SM2P256V1Curve : AbstractFpCurve
	{
		// Token: 0x06002C00 RID: 11264 RVA: 0x000E8D8C File Offset: 0x000E8D8C
		public SM2P256V1Curve() : base(SM2P256V1Curve.q)
		{
			this.m_infinity = new SM2P256V1Point(this, null, null);
			this.m_a = this.FromBigInteger(new BigInteger(1, Hex.DecodeStrict("FFFFFFFEFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF00000000FFFFFFFFFFFFFFFC")));
			this.m_b = this.FromBigInteger(new BigInteger(1, Hex.DecodeStrict("28E9FA9E9D9F5E344D5A9E4BCF6509A7F39789F515AB8F92DDBCBD414D940E93")));
			this.m_order = new BigInteger(1, Hex.DecodeStrict("FFFFFFFEFFFFFFFFFFFFFFFFFFFFFFFF7203DF6B21C6052B53BBF40939D54123"));
			this.m_cofactor = BigInteger.One;
			this.m_coord = 2;
		}

		// Token: 0x06002C01 RID: 11265 RVA: 0x000E8E18 File Offset: 0x000E8E18
		protected override ECCurve CloneCurve()
		{
			return new SM2P256V1Curve();
		}

		// Token: 0x06002C02 RID: 11266 RVA: 0x000E8E20 File Offset: 0x000E8E20
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 2;
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06002C03 RID: 11267 RVA: 0x000E8E40 File Offset: 0x000E8E40
		public virtual BigInteger Q
		{
			get
			{
				return SM2P256V1Curve.q;
			}
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06002C04 RID: 11268 RVA: 0x000E8E48 File Offset: 0x000E8E48
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06002C05 RID: 11269 RVA: 0x000E8E50 File Offset: 0x000E8E50
		public override int FieldSize
		{
			get
			{
				return SM2P256V1Curve.q.BitLength;
			}
		}

		// Token: 0x06002C06 RID: 11270 RVA: 0x000E8E5C File Offset: 0x000E8E5C
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SM2P256V1FieldElement(x);
		}

		// Token: 0x06002C07 RID: 11271 RVA: 0x000E8E64 File Offset: 0x000E8E64
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SM2P256V1Point(this, x, y, withCompression);
		}

		// Token: 0x06002C08 RID: 11272 RVA: 0x000E8E70 File Offset: 0x000E8E70
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SM2P256V1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x06002C09 RID: 11273 RVA: 0x000E8E80 File Offset: 0x000E8E80
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			uint[] array = new uint[len * 8 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat256.Copy(((SM2P256V1FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 8;
				Nat256.Copy(((SM2P256V1FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 8;
			}
			return new SM2P256V1Curve.SM2P256V1LookupTable(this, array, len);
		}

		// Token: 0x04001B73 RID: 7027
		private const int SM2P256V1_DEFAULT_COORDS = 2;

		// Token: 0x04001B74 RID: 7028
		private const int SM2P256V1_FE_INTS = 8;

		// Token: 0x04001B75 RID: 7029
		public static readonly BigInteger q = SM2P256V1FieldElement.Q;

		// Token: 0x04001B76 RID: 7030
		private static readonly ECFieldElement[] SM2P256V1_AFFINE_ZS = new ECFieldElement[]
		{
			new SM2P256V1FieldElement(BigInteger.One)
		};

		// Token: 0x04001B77 RID: 7031
		protected readonly SM2P256V1Point m_infinity;

		// Token: 0x02000E2B RID: 3627
		private class SM2P256V1LookupTable : AbstractECLookupTable
		{
			// Token: 0x06008C7D RID: 35965 RVA: 0x002A2B90 File Offset: 0x002A2B90
			internal SM2P256V1LookupTable(SM2P256V1Curve outer, uint[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17001D82 RID: 7554
			// (get) Token: 0x06008C7E RID: 35966 RVA: 0x002A2BB0 File Offset: 0x002A2BB0
			public override int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06008C7F RID: 35967 RVA: 0x002A2BB8 File Offset: 0x002A2BB8
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

			// Token: 0x06008C80 RID: 35968 RVA: 0x002A2C54 File Offset: 0x002A2C54
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

			// Token: 0x06008C81 RID: 35969 RVA: 0x002A2CAC File Offset: 0x002A2CAC
			private ECPoint CreatePoint(uint[] x, uint[] y)
			{
				return this.m_outer.CreateRawPoint(new SM2P256V1FieldElement(x), new SM2P256V1FieldElement(y), SM2P256V1Curve.SM2P256V1_AFFINE_ZS, false);
			}

			// Token: 0x0400419F RID: 16799
			private readonly SM2P256V1Curve m_outer;

			// Token: 0x040041A0 RID: 16800
			private readonly uint[] m_table;

			// Token: 0x040041A1 RID: 16801
			private readonly int m_size;
		}
	}
}
