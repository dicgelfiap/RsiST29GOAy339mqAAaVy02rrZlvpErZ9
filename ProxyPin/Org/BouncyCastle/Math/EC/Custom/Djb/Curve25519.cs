using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Math.EC.Custom.Djb
{
	// Token: 0x02000572 RID: 1394
	internal class Curve25519 : AbstractFpCurve
	{
		// Token: 0x06002B66 RID: 11110 RVA: 0x000E6F34 File Offset: 0x000E6F34
		public Curve25519() : base(Curve25519.q)
		{
			this.m_infinity = new Curve25519Point(this, null, null);
			this.m_a = this.FromBigInteger(Curve25519.C_a);
			this.m_b = this.FromBigInteger(Curve25519.C_b);
			this.m_order = new BigInteger(1, Hex.DecodeStrict("1000000000000000000000000000000014DEF9DEA2F79CD65812631A5CF5D3ED"));
			this.m_cofactor = BigInteger.ValueOf(8L);
			this.m_coord = 4;
		}

		// Token: 0x06002B67 RID: 11111 RVA: 0x000E6FAC File Offset: 0x000E6FAC
		protected override ECCurve CloneCurve()
		{
			return new Curve25519();
		}

		// Token: 0x06002B68 RID: 11112 RVA: 0x000E6FB4 File Offset: 0x000E6FB4
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 4;
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06002B69 RID: 11113 RVA: 0x000E6FD4 File Offset: 0x000E6FD4
		public virtual BigInteger Q
		{
			get
			{
				return Curve25519.q;
			}
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06002B6A RID: 11114 RVA: 0x000E6FDC File Offset: 0x000E6FDC
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06002B6B RID: 11115 RVA: 0x000E6FE4 File Offset: 0x000E6FE4
		public override int FieldSize
		{
			get
			{
				return Curve25519.q.BitLength;
			}
		}

		// Token: 0x06002B6C RID: 11116 RVA: 0x000E6FF0 File Offset: 0x000E6FF0
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new Curve25519FieldElement(x);
		}

		// Token: 0x06002B6D RID: 11117 RVA: 0x000E6FF8 File Offset: 0x000E6FF8
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new Curve25519Point(this, x, y, withCompression);
		}

		// Token: 0x06002B6E RID: 11118 RVA: 0x000E7004 File Offset: 0x000E7004
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new Curve25519Point(this, x, y, zs, withCompression);
		}

		// Token: 0x06002B6F RID: 11119 RVA: 0x000E7014 File Offset: 0x000E7014
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			uint[] array = new uint[len * 8 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat256.Copy(((Curve25519FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 8;
				Nat256.Copy(((Curve25519FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 8;
			}
			return new Curve25519.Curve25519LookupTable(this, array, len);
		}

		// Token: 0x04001B5E RID: 7006
		private const int CURVE25519_DEFAULT_COORDS = 4;

		// Token: 0x04001B5F RID: 7007
		private const int CURVE25519_FE_INTS = 8;

		// Token: 0x04001B60 RID: 7008
		public static readonly BigInteger q = Curve25519FieldElement.Q;

		// Token: 0x04001B61 RID: 7009
		private static readonly BigInteger C_a = new BigInteger(1, Hex.DecodeStrict("2AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA984914A144"));

		// Token: 0x04001B62 RID: 7010
		private static readonly BigInteger C_b = new BigInteger(1, Hex.DecodeStrict("7B425ED097B425ED097B425ED097B425ED097B425ED097B4260B5E9C7710C864"));

		// Token: 0x04001B63 RID: 7011
		private static readonly ECFieldElement[] CURVE25519_AFFINE_ZS = new ECFieldElement[]
		{
			new Curve25519FieldElement(BigInteger.One),
			new Curve25519FieldElement(Curve25519.C_a)
		};

		// Token: 0x04001B64 RID: 7012
		protected readonly Curve25519Point m_infinity;

		// Token: 0x02000E29 RID: 3625
		private class Curve25519LookupTable : AbstractECLookupTable
		{
			// Token: 0x06008C76 RID: 35958 RVA: 0x002A2998 File Offset: 0x002A2998
			internal Curve25519LookupTable(Curve25519 outer, uint[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17001D81 RID: 7553
			// (get) Token: 0x06008C77 RID: 35959 RVA: 0x002A29B8 File Offset: 0x002A29B8
			public override int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06008C78 RID: 35960 RVA: 0x002A29C0 File Offset: 0x002A29C0
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

			// Token: 0x06008C79 RID: 35961 RVA: 0x002A2A5C File Offset: 0x002A2A5C
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

			// Token: 0x06008C7A RID: 35962 RVA: 0x002A2AB4 File Offset: 0x002A2AB4
			private ECPoint CreatePoint(uint[] x, uint[] y)
			{
				return this.m_outer.CreateRawPoint(new Curve25519FieldElement(x), new Curve25519FieldElement(y), Curve25519.CURVE25519_AFFINE_ZS, false);
			}

			// Token: 0x04004199 RID: 16793
			private readonly Curve25519 m_outer;

			// Token: 0x0400419A RID: 16794
			private readonly uint[] m_table;

			// Token: 0x0400419B RID: 16795
			private readonly int m_size;
		}
	}
}
