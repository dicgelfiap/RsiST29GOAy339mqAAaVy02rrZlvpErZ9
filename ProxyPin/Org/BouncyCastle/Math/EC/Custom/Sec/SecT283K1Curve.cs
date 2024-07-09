using System;
using Org.BouncyCastle.Math.EC.Multiplier;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x020005D7 RID: 1495
	internal class SecT283K1Curve : AbstractF2mCurve
	{
		// Token: 0x060031D4 RID: 12756 RVA: 0x00103634 File Offset: 0x00103634
		public SecT283K1Curve() : base(283, 5, 7, 12)
		{
			this.m_infinity = new SecT283K1Point(this, null, null);
			this.m_a = this.FromBigInteger(BigInteger.Zero);
			this.m_b = this.FromBigInteger(BigInteger.One);
			this.m_order = new BigInteger(1, Hex.DecodeStrict("01FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFE9AE2ED07577265DFF7F94451E061E163C61"));
			this.m_cofactor = BigInteger.ValueOf(4L);
			this.m_coord = 6;
		}

		// Token: 0x060031D5 RID: 12757 RVA: 0x001036B0 File Offset: 0x001036B0
		protected override ECCurve CloneCurve()
		{
			return new SecT283K1Curve();
		}

		// Token: 0x060031D6 RID: 12758 RVA: 0x001036B8 File Offset: 0x001036B8
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 6;
		}

		// Token: 0x060031D7 RID: 12759 RVA: 0x001036D8 File Offset: 0x001036D8
		protected override ECMultiplier CreateDefaultMultiplier()
		{
			return new WTauNafMultiplier();
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x060031D8 RID: 12760 RVA: 0x001036E0 File Offset: 0x001036E0
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x060031D9 RID: 12761 RVA: 0x001036E8 File Offset: 0x001036E8
		public override int FieldSize
		{
			get
			{
				return 283;
			}
		}

		// Token: 0x060031DA RID: 12762 RVA: 0x001036F0 File Offset: 0x001036F0
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecT283FieldElement(x);
		}

		// Token: 0x060031DB RID: 12763 RVA: 0x001036F8 File Offset: 0x001036F8
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecT283K1Point(this, x, y, withCompression);
		}

		// Token: 0x060031DC RID: 12764 RVA: 0x00103704 File Offset: 0x00103704
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecT283K1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x060031DD RID: 12765 RVA: 0x00103714 File Offset: 0x00103714
		public override bool IsKoblitz
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x060031DE RID: 12766 RVA: 0x00103718 File Offset: 0x00103718
		public virtual int M
		{
			get
			{
				return 283;
			}
		}

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x060031DF RID: 12767 RVA: 0x00103720 File Offset: 0x00103720
		public virtual bool IsTrinomial
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x060031E0 RID: 12768 RVA: 0x00103724 File Offset: 0x00103724
		public virtual int K1
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x060031E1 RID: 12769 RVA: 0x00103728 File Offset: 0x00103728
		public virtual int K2
		{
			get
			{
				return 7;
			}
		}

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x060031E2 RID: 12770 RVA: 0x0010372C File Offset: 0x0010372C
		public virtual int K3
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x060031E3 RID: 12771 RVA: 0x00103730 File Offset: 0x00103730
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			ulong[] array = new ulong[len * 5 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat320.Copy64(((SecT283FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 5;
				Nat320.Copy64(((SecT283FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 5;
			}
			return new SecT283K1Curve.SecT283K1LookupTable(this, array, len);
		}

		// Token: 0x04001C52 RID: 7250
		private const int SECT283K1_DEFAULT_COORDS = 6;

		// Token: 0x04001C53 RID: 7251
		private const int SECT283K1_FE_LONGS = 5;

		// Token: 0x04001C54 RID: 7252
		private static readonly ECFieldElement[] SECT283K1_AFFINE_ZS = new ECFieldElement[]
		{
			new SecT283FieldElement(BigInteger.One)
		};

		// Token: 0x04001C55 RID: 7253
		protected readonly SecT283K1Point m_infinity;

		// Token: 0x02000E44 RID: 3652
		private class SecT283K1LookupTable : AbstractECLookupTable
		{
			// Token: 0x06008CFA RID: 36090 RVA: 0x002A4A8C File Offset: 0x002A4A8C
			internal SecT283K1LookupTable(SecT283K1Curve outer, ulong[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17001D9B RID: 7579
			// (get) Token: 0x06008CFB RID: 36091 RVA: 0x002A4AAC File Offset: 0x002A4AAC
			public override int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06008CFC RID: 36092 RVA: 0x002A4AB4 File Offset: 0x002A4AB4
			public override ECPoint Lookup(int index)
			{
				ulong[] array = Nat320.Create64();
				ulong[] array2 = Nat320.Create64();
				int num = 0;
				for (int i = 0; i < this.m_size; i++)
				{
					ulong num2 = (ulong)((long)((i ^ index) - 1 >> 31));
					for (int j = 0; j < 5; j++)
					{
						ulong[] array3;
						IntPtr intPtr;
						(array3 = array)[(int)(intPtr = (IntPtr)j)] = (array3[(int)intPtr] ^ (this.m_table[num + j] & num2));
						(array3 = array2)[(int)(intPtr = (IntPtr)j)] = (array3[(int)intPtr] ^ (this.m_table[num + 5 + j] & num2));
					}
					num += 10;
				}
				return this.CreatePoint(array, array2);
			}

			// Token: 0x06008CFD RID: 36093 RVA: 0x002A4B50 File Offset: 0x002A4B50
			public override ECPoint LookupVar(int index)
			{
				ulong[] array = Nat320.Create64();
				ulong[] array2 = Nat320.Create64();
				int num = index * 5 * 2;
				for (int i = 0; i < 5; i++)
				{
					array[i] = this.m_table[num + i];
					array2[i] = this.m_table[num + 5 + i];
				}
				return this.CreatePoint(array, array2);
			}

			// Token: 0x06008CFE RID: 36094 RVA: 0x002A4BA8 File Offset: 0x002A4BA8
			private ECPoint CreatePoint(ulong[] x, ulong[] y)
			{
				return this.m_outer.CreateRawPoint(new SecT283FieldElement(x), new SecT283FieldElement(y), SecT283K1Curve.SECT283K1_AFFINE_ZS, false);
			}

			// Token: 0x040041EA RID: 16874
			private readonly SecT283K1Curve m_outer;

			// Token: 0x040041EB RID: 16875
			private readonly ulong[] m_table;

			// Token: 0x040041EC RID: 16876
			private readonly int m_size;
		}
	}
}
