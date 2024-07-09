using System;
using Org.BouncyCastle.Math.EC.Multiplier;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x020005E3 RID: 1507
	internal class SecT571K1Curve : AbstractF2mCurve
	{
		// Token: 0x060032B1 RID: 12977 RVA: 0x0010728C File Offset: 0x0010728C
		public SecT571K1Curve() : base(571, 2, 5, 10)
		{
			this.m_infinity = new SecT571K1Point(this, null, null);
			this.m_a = this.FromBigInteger(BigInteger.Zero);
			this.m_b = this.FromBigInteger(BigInteger.One);
			this.m_order = new BigInteger(1, Hex.DecodeStrict("020000000000000000000000000000000000000000000000000000000000000000000000131850E1F19A63E4B391A8DB917F4138B630D84BE5D639381E91DEB45CFE778F637C1001"));
			this.m_cofactor = BigInteger.ValueOf(4L);
			this.m_coord = 6;
		}

		// Token: 0x060032B2 RID: 12978 RVA: 0x00107308 File Offset: 0x00107308
		protected override ECCurve CloneCurve()
		{
			return new SecT571K1Curve();
		}

		// Token: 0x060032B3 RID: 12979 RVA: 0x00107310 File Offset: 0x00107310
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 6;
		}

		// Token: 0x060032B4 RID: 12980 RVA: 0x00107330 File Offset: 0x00107330
		protected override ECMultiplier CreateDefaultMultiplier()
		{
			return new WTauNafMultiplier();
		}

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x060032B5 RID: 12981 RVA: 0x00107338 File Offset: 0x00107338
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x060032B6 RID: 12982 RVA: 0x00107340 File Offset: 0x00107340
		public override int FieldSize
		{
			get
			{
				return 571;
			}
		}

		// Token: 0x060032B7 RID: 12983 RVA: 0x00107348 File Offset: 0x00107348
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecT571FieldElement(x);
		}

		// Token: 0x060032B8 RID: 12984 RVA: 0x00107350 File Offset: 0x00107350
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecT571K1Point(this, x, y, withCompression);
		}

		// Token: 0x060032B9 RID: 12985 RVA: 0x0010735C File Offset: 0x0010735C
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecT571K1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x060032BA RID: 12986 RVA: 0x0010736C File Offset: 0x0010736C
		public override bool IsKoblitz
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x060032BB RID: 12987 RVA: 0x00107370 File Offset: 0x00107370
		public virtual int M
		{
			get
			{
				return 571;
			}
		}

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x060032BC RID: 12988 RVA: 0x00107378 File Offset: 0x00107378
		public virtual bool IsTrinomial
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x060032BD RID: 12989 RVA: 0x0010737C File Offset: 0x0010737C
		public virtual int K1
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x060032BE RID: 12990 RVA: 0x00107380 File Offset: 0x00107380
		public virtual int K2
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x060032BF RID: 12991 RVA: 0x00107384 File Offset: 0x00107384
		public virtual int K3
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x060032C0 RID: 12992 RVA: 0x00107388 File Offset: 0x00107388
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			ulong[] array = new ulong[len * 9 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat576.Copy64(((SecT571FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 9;
				Nat576.Copy64(((SecT571FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 9;
			}
			return new SecT571K1Curve.SecT571K1LookupTable(this, array, len);
		}

		// Token: 0x04001C69 RID: 7273
		private const int SECT571K1_DEFAULT_COORDS = 6;

		// Token: 0x04001C6A RID: 7274
		private const int SECT571K1_FE_LONGS = 9;

		// Token: 0x04001C6B RID: 7275
		private static readonly ECFieldElement[] SECT571K1_AFFINE_ZS = new ECFieldElement[]
		{
			new SecT571FieldElement(BigInteger.One)
		};

		// Token: 0x04001C6C RID: 7276
		protected readonly SecT571K1Point m_infinity;

		// Token: 0x02000E48 RID: 3656
		private class SecT571K1LookupTable : AbstractECLookupTable
		{
			// Token: 0x06008D0E RID: 36110 RVA: 0x002A4F7C File Offset: 0x002A4F7C
			internal SecT571K1LookupTable(SecT571K1Curve outer, ulong[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17001D9F RID: 7583
			// (get) Token: 0x06008D0F RID: 36111 RVA: 0x002A4F9C File Offset: 0x002A4F9C
			public override int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06008D10 RID: 36112 RVA: 0x002A4FA4 File Offset: 0x002A4FA4
			public override ECPoint Lookup(int index)
			{
				ulong[] array = Nat576.Create64();
				ulong[] array2 = Nat576.Create64();
				int num = 0;
				for (int i = 0; i < this.m_size; i++)
				{
					ulong num2 = (ulong)((long)((i ^ index) - 1 >> 31));
					for (int j = 0; j < 9; j++)
					{
						ulong[] array3;
						IntPtr intPtr;
						(array3 = array)[(int)(intPtr = (IntPtr)j)] = (array3[(int)intPtr] ^ (this.m_table[num + j] & num2));
						(array3 = array2)[(int)(intPtr = (IntPtr)j)] = (array3[(int)intPtr] ^ (this.m_table[num + 9 + j] & num2));
					}
					num += 18;
				}
				return this.CreatePoint(array, array2);
			}

			// Token: 0x06008D11 RID: 36113 RVA: 0x002A5044 File Offset: 0x002A5044
			public override ECPoint LookupVar(int index)
			{
				ulong[] array = Nat576.Create64();
				ulong[] array2 = Nat576.Create64();
				int num = index * 9 * 2;
				for (int i = 0; i < 9; i++)
				{
					array[i] = this.m_table[num + i];
					array2[i] = this.m_table[num + 9 + i];
				}
				return this.CreatePoint(array, array2);
			}

			// Token: 0x06008D12 RID: 36114 RVA: 0x002A50A0 File Offset: 0x002A50A0
			private ECPoint CreatePoint(ulong[] x, ulong[] y)
			{
				return this.m_outer.CreateRawPoint(new SecT571FieldElement(x), new SecT571FieldElement(y), SecT571K1Curve.SECT571K1_AFFINE_ZS, false);
			}

			// Token: 0x040041F6 RID: 16886
			private readonly SecT571K1Curve m_outer;

			// Token: 0x040041F7 RID: 16887
			private readonly ulong[] m_table;

			// Token: 0x040041F8 RID: 16888
			private readonly int m_size;
		}
	}
}
