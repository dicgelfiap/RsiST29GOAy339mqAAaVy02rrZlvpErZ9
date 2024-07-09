using System;
using Org.BouncyCastle.Math.EC.Multiplier;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x020005D3 RID: 1491
	internal class SecT239K1Curve : AbstractF2mCurve
	{
		// Token: 0x0600317F RID: 12671 RVA: 0x00101EBC File Offset: 0x00101EBC
		public SecT239K1Curve() : base(239, 158, 0, 0)
		{
			this.m_infinity = new SecT239K1Point(this, null, null);
			this.m_a = this.FromBigInteger(BigInteger.Zero);
			this.m_b = this.FromBigInteger(BigInteger.One);
			this.m_order = new BigInteger(1, Hex.DecodeStrict("2000000000000000000000000000005A79FEC67CB6E91F1C1DA800E478A5"));
			this.m_cofactor = BigInteger.ValueOf(4L);
			this.m_coord = 6;
		}

		// Token: 0x06003180 RID: 12672 RVA: 0x00101F3C File Offset: 0x00101F3C
		protected override ECCurve CloneCurve()
		{
			return new SecT239K1Curve();
		}

		// Token: 0x06003181 RID: 12673 RVA: 0x00101F44 File Offset: 0x00101F44
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 6;
		}

		// Token: 0x06003182 RID: 12674 RVA: 0x00101F64 File Offset: 0x00101F64
		protected override ECMultiplier CreateDefaultMultiplier()
		{
			return new WTauNafMultiplier();
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x06003183 RID: 12675 RVA: 0x00101F6C File Offset: 0x00101F6C
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06003184 RID: 12676 RVA: 0x00101F74 File Offset: 0x00101F74
		public override int FieldSize
		{
			get
			{
				return 239;
			}
		}

		// Token: 0x06003185 RID: 12677 RVA: 0x00101F7C File Offset: 0x00101F7C
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecT239FieldElement(x);
		}

		// Token: 0x06003186 RID: 12678 RVA: 0x00101F84 File Offset: 0x00101F84
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecT239K1Point(this, x, y, withCompression);
		}

		// Token: 0x06003187 RID: 12679 RVA: 0x00101F90 File Offset: 0x00101F90
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecT239K1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06003188 RID: 12680 RVA: 0x00101FA0 File Offset: 0x00101FA0
		public override bool IsKoblitz
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x06003189 RID: 12681 RVA: 0x00101FA4 File Offset: 0x00101FA4
		public virtual int M
		{
			get
			{
				return 239;
			}
		}

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x0600318A RID: 12682 RVA: 0x00101FAC File Offset: 0x00101FAC
		public virtual bool IsTrinomial
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x0600318B RID: 12683 RVA: 0x00101FB0 File Offset: 0x00101FB0
		public virtual int K1
		{
			get
			{
				return 158;
			}
		}

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x0600318C RID: 12684 RVA: 0x00101FB8 File Offset: 0x00101FB8
		public virtual int K2
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x0600318D RID: 12685 RVA: 0x00101FBC File Offset: 0x00101FBC
		public virtual int K3
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600318E RID: 12686 RVA: 0x00101FC0 File Offset: 0x00101FC0
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			ulong[] array = new ulong[len * 4 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat256.Copy64(((SecT239FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 4;
				Nat256.Copy64(((SecT239FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 4;
			}
			return new SecT239K1Curve.SecT239K1LookupTable(this, array, len);
		}

		// Token: 0x04001C4A RID: 7242
		private const int SECT239K1_DEFAULT_COORDS = 6;

		// Token: 0x04001C4B RID: 7243
		private const int SECT239K1_FE_LONGS = 4;

		// Token: 0x04001C4C RID: 7244
		private static readonly ECFieldElement[] SECT239K1_AFFINE_ZS = new ECFieldElement[]
		{
			new SecT239FieldElement(BigInteger.One)
		};

		// Token: 0x04001C4D RID: 7245
		protected readonly SecT239K1Point m_infinity;

		// Token: 0x02000E43 RID: 3651
		private class SecT239K1LookupTable : AbstractECLookupTable
		{
			// Token: 0x06008CF5 RID: 36085 RVA: 0x002A4950 File Offset: 0x002A4950
			internal SecT239K1LookupTable(SecT239K1Curve outer, ulong[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17001D9A RID: 7578
			// (get) Token: 0x06008CF6 RID: 36086 RVA: 0x002A4970 File Offset: 0x002A4970
			public override int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06008CF7 RID: 36087 RVA: 0x002A4978 File Offset: 0x002A4978
			public override ECPoint Lookup(int index)
			{
				ulong[] array = Nat256.Create64();
				ulong[] array2 = Nat256.Create64();
				int num = 0;
				for (int i = 0; i < this.m_size; i++)
				{
					ulong num2 = (ulong)((long)((i ^ index) - 1 >> 31));
					for (int j = 0; j < 4; j++)
					{
						ulong[] array3;
						IntPtr intPtr;
						(array3 = array)[(int)(intPtr = (IntPtr)j)] = (array3[(int)intPtr] ^ (this.m_table[num + j] & num2));
						(array3 = array2)[(int)(intPtr = (IntPtr)j)] = (array3[(int)intPtr] ^ (this.m_table[num + 4 + j] & num2));
					}
					num += 8;
				}
				return this.CreatePoint(array, array2);
			}

			// Token: 0x06008CF8 RID: 36088 RVA: 0x002A4A14 File Offset: 0x002A4A14
			public override ECPoint LookupVar(int index)
			{
				ulong[] array = Nat256.Create64();
				ulong[] array2 = Nat256.Create64();
				int num = index * 4 * 2;
				for (int i = 0; i < 4; i++)
				{
					array[i] = this.m_table[num + i];
					array2[i] = this.m_table[num + 4 + i];
				}
				return this.CreatePoint(array, array2);
			}

			// Token: 0x06008CF9 RID: 36089 RVA: 0x002A4A6C File Offset: 0x002A4A6C
			private ECPoint CreatePoint(ulong[] x, ulong[] y)
			{
				return this.m_outer.CreateRawPoint(new SecT239FieldElement(x), new SecT239FieldElement(y), SecT239K1Curve.SECT239K1_AFFINE_ZS, false);
			}

			// Token: 0x040041E7 RID: 16871
			private readonly SecT239K1Curve m_outer;

			// Token: 0x040041E8 RID: 16872
			private readonly ulong[] m_table;

			// Token: 0x040041E9 RID: 16873
			private readonly int m_size;
		}
	}
}
