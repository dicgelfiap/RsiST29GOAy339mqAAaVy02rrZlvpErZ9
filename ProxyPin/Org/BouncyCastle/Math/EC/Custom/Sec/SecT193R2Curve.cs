using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x020005C9 RID: 1481
	internal class SecT193R2Curve : AbstractF2mCurve
	{
		// Token: 0x060030BE RID: 12478 RVA: 0x000FE9CC File Offset: 0x000FE9CC
		public SecT193R2Curve() : base(193, 15, 0, 0)
		{
			this.m_infinity = new SecT193R2Point(this, null, null);
			this.m_a = this.FromBigInteger(new BigInteger(1, Hex.DecodeStrict("0163F35A5137C2CE3EA6ED8667190B0BC43ECD69977702709B")));
			this.m_b = this.FromBigInteger(new BigInteger(1, Hex.DecodeStrict("00C9BB9E8927D4D64C377E2AB2856A5B16E3EFB7F61D4316AE")));
			this.m_order = new BigInteger(1, Hex.DecodeStrict("010000000000000000000000015AAB561B005413CCD4EE99D5"));
			this.m_cofactor = BigInteger.Two;
			this.m_coord = 6;
		}

		// Token: 0x060030BF RID: 12479 RVA: 0x000FEA5C File Offset: 0x000FEA5C
		protected override ECCurve CloneCurve()
		{
			return new SecT193R2Curve();
		}

		// Token: 0x060030C0 RID: 12480 RVA: 0x000FEA64 File Offset: 0x000FEA64
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 6;
		}

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x060030C1 RID: 12481 RVA: 0x000FEA84 File Offset: 0x000FEA84
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x060030C2 RID: 12482 RVA: 0x000FEA8C File Offset: 0x000FEA8C
		public override int FieldSize
		{
			get
			{
				return 193;
			}
		}

		// Token: 0x060030C3 RID: 12483 RVA: 0x000FEA94 File Offset: 0x000FEA94
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecT193FieldElement(x);
		}

		// Token: 0x060030C4 RID: 12484 RVA: 0x000FEA9C File Offset: 0x000FEA9C
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecT193R2Point(this, x, y, withCompression);
		}

		// Token: 0x060030C5 RID: 12485 RVA: 0x000FEAA8 File Offset: 0x000FEAA8
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecT193R2Point(this, x, y, zs, withCompression);
		}

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x060030C6 RID: 12486 RVA: 0x000FEAB8 File Offset: 0x000FEAB8
		public override bool IsKoblitz
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x060030C7 RID: 12487 RVA: 0x000FEABC File Offset: 0x000FEABC
		public virtual int M
		{
			get
			{
				return 193;
			}
		}

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x060030C8 RID: 12488 RVA: 0x000FEAC4 File Offset: 0x000FEAC4
		public virtual bool IsTrinomial
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x060030C9 RID: 12489 RVA: 0x000FEAC8 File Offset: 0x000FEAC8
		public virtual int K1
		{
			get
			{
				return 15;
			}
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x060030CA RID: 12490 RVA: 0x000FEACC File Offset: 0x000FEACC
		public virtual int K2
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x060030CB RID: 12491 RVA: 0x000FEAD0 File Offset: 0x000FEAD0
		public virtual int K3
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060030CC RID: 12492 RVA: 0x000FEAD4 File Offset: 0x000FEAD4
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
			return new SecT193R2Curve.SecT193R2LookupTable(this, array, len);
		}

		// Token: 0x04001C38 RID: 7224
		private const int SECT193R2_DEFAULT_COORDS = 6;

		// Token: 0x04001C39 RID: 7225
		private const int SECT193R2_FE_LONGS = 4;

		// Token: 0x04001C3A RID: 7226
		private static readonly ECFieldElement[] SECT193R2_AFFINE_ZS = new ECFieldElement[]
		{
			new SecT193FieldElement(BigInteger.One)
		};

		// Token: 0x04001C3B RID: 7227
		protected readonly SecT193R2Point m_infinity;

		// Token: 0x02000E40 RID: 3648
		private class SecT193R2LookupTable : AbstractECLookupTable
		{
			// Token: 0x06008CE6 RID: 36070 RVA: 0x002A459C File Offset: 0x002A459C
			internal SecT193R2LookupTable(SecT193R2Curve outer, ulong[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17001D97 RID: 7575
			// (get) Token: 0x06008CE7 RID: 36071 RVA: 0x002A45BC File Offset: 0x002A45BC
			public override int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06008CE8 RID: 36072 RVA: 0x002A45C4 File Offset: 0x002A45C4
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

			// Token: 0x06008CE9 RID: 36073 RVA: 0x002A4660 File Offset: 0x002A4660
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

			// Token: 0x06008CEA RID: 36074 RVA: 0x002A46B8 File Offset: 0x002A46B8
			private ECPoint CreatePoint(ulong[] x, ulong[] y)
			{
				return this.m_outer.CreateRawPoint(new SecT193FieldElement(x), new SecT193FieldElement(y), SecT193R2Curve.SECT193R2_AFFINE_ZS, false);
			}

			// Token: 0x040041DE RID: 16862
			private readonly SecT193R2Curve m_outer;

			// Token: 0x040041DF RID: 16863
			private readonly ulong[] m_table;

			// Token: 0x040041E0 RID: 16864
			private readonly int m_size;
		}
	}
}
