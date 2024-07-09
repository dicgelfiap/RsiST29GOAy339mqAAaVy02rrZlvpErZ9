using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x020005C7 RID: 1479
	internal class SecT193R1Curve : AbstractF2mCurve
	{
		// Token: 0x060030A4 RID: 12452 RVA: 0x000FE0C8 File Offset: 0x000FE0C8
		public SecT193R1Curve() : base(193, 15, 0, 0)
		{
			this.m_infinity = new SecT193R1Point(this, null, null);
			this.m_a = this.FromBigInteger(new BigInteger(1, Hex.DecodeStrict("0017858FEB7A98975169E171F77B4087DE098AC8A911DF7B01")));
			this.m_b = this.FromBigInteger(new BigInteger(1, Hex.DecodeStrict("00FDFB49BFE6C3A89FACADAA7A1E5BBC7CC1C2E5D831478814")));
			this.m_order = new BigInteger(1, Hex.DecodeStrict("01000000000000000000000000C7F34A778F443ACC920EBA49"));
			this.m_cofactor = BigInteger.Two;
			this.m_coord = 6;
		}

		// Token: 0x060030A5 RID: 12453 RVA: 0x000FE158 File Offset: 0x000FE158
		protected override ECCurve CloneCurve()
		{
			return new SecT193R1Curve();
		}

		// Token: 0x060030A6 RID: 12454 RVA: 0x000FE160 File Offset: 0x000FE160
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 6;
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x060030A7 RID: 12455 RVA: 0x000FE180 File Offset: 0x000FE180
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x060030A8 RID: 12456 RVA: 0x000FE188 File Offset: 0x000FE188
		public override int FieldSize
		{
			get
			{
				return 193;
			}
		}

		// Token: 0x060030A9 RID: 12457 RVA: 0x000FE190 File Offset: 0x000FE190
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecT193FieldElement(x);
		}

		// Token: 0x060030AA RID: 12458 RVA: 0x000FE198 File Offset: 0x000FE198
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecT193R1Point(this, x, y, withCompression);
		}

		// Token: 0x060030AB RID: 12459 RVA: 0x000FE1A4 File Offset: 0x000FE1A4
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecT193R1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x060030AC RID: 12460 RVA: 0x000FE1B4 File Offset: 0x000FE1B4
		public override bool IsKoblitz
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x060030AD RID: 12461 RVA: 0x000FE1B8 File Offset: 0x000FE1B8
		public virtual int M
		{
			get
			{
				return 193;
			}
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x060030AE RID: 12462 RVA: 0x000FE1C0 File Offset: 0x000FE1C0
		public virtual bool IsTrinomial
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x060030AF RID: 12463 RVA: 0x000FE1C4 File Offset: 0x000FE1C4
		public virtual int K1
		{
			get
			{
				return 15;
			}
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x060030B0 RID: 12464 RVA: 0x000FE1C8 File Offset: 0x000FE1C8
		public virtual int K2
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x060030B1 RID: 12465 RVA: 0x000FE1CC File Offset: 0x000FE1CC
		public virtual int K3
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060030B2 RID: 12466 RVA: 0x000FE1D0 File Offset: 0x000FE1D0
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			ulong[] array = new ulong[len * 4 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat256.Copy64(((SecT193FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 4;
				Nat256.Copy64(((SecT193FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 4;
			}
			return new SecT193R1Curve.SecT193R1LookupTable(this, array, len);
		}

		// Token: 0x04001C34 RID: 7220
		private const int SECT193R1_DEFAULT_COORDS = 6;

		// Token: 0x04001C35 RID: 7221
		private const int SECT193R1_FE_LONGS = 4;

		// Token: 0x04001C36 RID: 7222
		private static readonly ECFieldElement[] SECT193R1_AFFINE_ZS = new ECFieldElement[]
		{
			new SecT193FieldElement(BigInteger.One)
		};

		// Token: 0x04001C37 RID: 7223
		protected readonly SecT193R1Point m_infinity;

		// Token: 0x02000E3F RID: 3647
		private class SecT193R1LookupTable : AbstractECLookupTable
		{
			// Token: 0x06008CE1 RID: 36065 RVA: 0x002A4460 File Offset: 0x002A4460
			internal SecT193R1LookupTable(SecT193R1Curve outer, ulong[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17001D96 RID: 7574
			// (get) Token: 0x06008CE2 RID: 36066 RVA: 0x002A4480 File Offset: 0x002A4480
			public override int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06008CE3 RID: 36067 RVA: 0x002A4488 File Offset: 0x002A4488
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

			// Token: 0x06008CE4 RID: 36068 RVA: 0x002A4524 File Offset: 0x002A4524
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

			// Token: 0x06008CE5 RID: 36069 RVA: 0x002A457C File Offset: 0x002A457C
			private ECPoint CreatePoint(ulong[] x, ulong[] y)
			{
				return this.m_outer.CreateRawPoint(new SecT193FieldElement(x), new SecT193FieldElement(y), SecT193R1Curve.SECT193R1_AFFINE_ZS, false);
			}

			// Token: 0x040041DB RID: 16859
			private readonly SecT193R1Curve m_outer;

			// Token: 0x040041DC RID: 16860
			private readonly ulong[] m_table;

			// Token: 0x040041DD RID: 16861
			private readonly int m_size;
		}
	}
}
