using System;
using Org.BouncyCastle.Math.EC.Multiplier;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x020005BF RID: 1471
	internal class SecT163K1Curve : AbstractF2mCurve
	{
		// Token: 0x0600301C RID: 12316 RVA: 0x000FBA88 File Offset: 0x000FBA88
		public SecT163K1Curve() : base(163, 3, 6, 7)
		{
			this.m_infinity = new SecT163K1Point(this, null, null);
			this.m_a = this.FromBigInteger(BigInteger.One);
			this.m_b = this.m_a;
			this.m_order = new BigInteger(1, Hex.DecodeStrict("04000000000000000000020108A2E0CC0D99F8A5EF"));
			this.m_cofactor = BigInteger.Two;
			this.m_coord = 6;
		}

		// Token: 0x0600301D RID: 12317 RVA: 0x000FBAFC File Offset: 0x000FBAFC
		protected override ECCurve CloneCurve()
		{
			return new SecT163K1Curve();
		}

		// Token: 0x0600301E RID: 12318 RVA: 0x000FBB04 File Offset: 0x000FBB04
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 6;
		}

		// Token: 0x0600301F RID: 12319 RVA: 0x000FBB24 File Offset: 0x000FBB24
		protected override ECMultiplier CreateDefaultMultiplier()
		{
			return new WTauNafMultiplier();
		}

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06003020 RID: 12320 RVA: 0x000FBB2C File Offset: 0x000FBB2C
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06003021 RID: 12321 RVA: 0x000FBB34 File Offset: 0x000FBB34
		public override int FieldSize
		{
			get
			{
				return 163;
			}
		}

		// Token: 0x06003022 RID: 12322 RVA: 0x000FBB3C File Offset: 0x000FBB3C
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecT163FieldElement(x);
		}

		// Token: 0x06003023 RID: 12323 RVA: 0x000FBB44 File Offset: 0x000FBB44
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecT163K1Point(this, x, y, withCompression);
		}

		// Token: 0x06003024 RID: 12324 RVA: 0x000FBB50 File Offset: 0x000FBB50
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecT163K1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06003025 RID: 12325 RVA: 0x000FBB60 File Offset: 0x000FBB60
		public override bool IsKoblitz
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06003026 RID: 12326 RVA: 0x000FBB64 File Offset: 0x000FBB64
		public virtual int M
		{
			get
			{
				return 163;
			}
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06003027 RID: 12327 RVA: 0x000FBB6C File Offset: 0x000FBB6C
		public virtual bool IsTrinomial
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x06003028 RID: 12328 RVA: 0x000FBB70 File Offset: 0x000FBB70
		public virtual int K1
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06003029 RID: 12329 RVA: 0x000FBB74 File Offset: 0x000FBB74
		public virtual int K2
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x0600302A RID: 12330 RVA: 0x000FBB78 File Offset: 0x000FBB78
		public virtual int K3
		{
			get
			{
				return 7;
			}
		}

		// Token: 0x0600302B RID: 12331 RVA: 0x000FBB7C File Offset: 0x000FBB7C
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			ulong[] array = new ulong[len * 3 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat192.Copy64(((SecT163FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 3;
				Nat192.Copy64(((SecT163FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 3;
			}
			return new SecT163K1Curve.SecT163K1LookupTable(this, array, len);
		}

		// Token: 0x04001C25 RID: 7205
		private const int SECT163K1_DEFAULT_COORDS = 6;

		// Token: 0x04001C26 RID: 7206
		private const int SECT163K1_FE_LONGS = 3;

		// Token: 0x04001C27 RID: 7207
		private static readonly ECFieldElement[] SECT163K1_AFFINE_ZS = new ECFieldElement[]
		{
			new SecT163FieldElement(BigInteger.One)
		};

		// Token: 0x04001C28 RID: 7208
		protected readonly SecT163K1Point m_infinity;

		// Token: 0x02000E3C RID: 3644
		private class SecT163K1LookupTable : AbstractECLookupTable
		{
			// Token: 0x06008CD2 RID: 36050 RVA: 0x002A40AC File Offset: 0x002A40AC
			internal SecT163K1LookupTable(SecT163K1Curve outer, ulong[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17001D93 RID: 7571
			// (get) Token: 0x06008CD3 RID: 36051 RVA: 0x002A40CC File Offset: 0x002A40CC
			public override int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06008CD4 RID: 36052 RVA: 0x002A40D4 File Offset: 0x002A40D4
			public override ECPoint Lookup(int index)
			{
				ulong[] array = Nat192.Create64();
				ulong[] array2 = Nat192.Create64();
				int num = 0;
				for (int i = 0; i < this.m_size; i++)
				{
					ulong num2 = (ulong)((long)((i ^ index) - 1 >> 31));
					for (int j = 0; j < 3; j++)
					{
						ulong[] array3;
						IntPtr intPtr;
						(array3 = array)[(int)(intPtr = (IntPtr)j)] = (array3[(int)intPtr] ^ (this.m_table[num + j] & num2));
						(array3 = array2)[(int)(intPtr = (IntPtr)j)] = (array3[(int)intPtr] ^ (this.m_table[num + 3 + j] & num2));
					}
					num += 6;
				}
				return this.CreatePoint(array, array2);
			}

			// Token: 0x06008CD5 RID: 36053 RVA: 0x002A4170 File Offset: 0x002A4170
			public override ECPoint LookupVar(int index)
			{
				ulong[] array = Nat192.Create64();
				ulong[] array2 = Nat192.Create64();
				int num = index * 3 * 2;
				for (int i = 0; i < 3; i++)
				{
					array[i] = this.m_table[num + i];
					array2[i] = this.m_table[num + 3 + i];
				}
				return this.CreatePoint(array, array2);
			}

			// Token: 0x06008CD6 RID: 36054 RVA: 0x002A41C8 File Offset: 0x002A41C8
			private ECPoint CreatePoint(ulong[] x, ulong[] y)
			{
				return this.m_outer.CreateRawPoint(new SecT163FieldElement(x), new SecT163FieldElement(y), SecT163K1Curve.SECT163K1_AFFINE_ZS, false);
			}

			// Token: 0x040041D2 RID: 16850
			private readonly SecT163K1Curve m_outer;

			// Token: 0x040041D3 RID: 16851
			private readonly ulong[] m_table;

			// Token: 0x040041D4 RID: 16852
			private readonly int m_size;
		}
	}
}
