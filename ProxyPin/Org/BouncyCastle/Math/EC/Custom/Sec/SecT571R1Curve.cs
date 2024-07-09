using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x020005E5 RID: 1509
	internal class SecT571R1Curve : AbstractF2mCurve
	{
		// Token: 0x060032CC RID: 13004 RVA: 0x00107B44 File Offset: 0x00107B44
		public SecT571R1Curve() : base(571, 2, 5, 10)
		{
			this.m_infinity = new SecT571R1Point(this, null, null);
			this.m_a = this.FromBigInteger(BigInteger.One);
			this.m_b = SecT571R1Curve.SecT571R1_B;
			this.m_order = new BigInteger(1, Hex.DecodeStrict("03FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFE661CE18FF55987308059B186823851EC7DD9CA1161DE93D5174D66E8382E9BB2FE84E47"));
			this.m_cofactor = BigInteger.Two;
			this.m_coord = 6;
		}

		// Token: 0x060032CD RID: 13005 RVA: 0x00107BB8 File Offset: 0x00107BB8
		protected override ECCurve CloneCurve()
		{
			return new SecT571R1Curve();
		}

		// Token: 0x060032CE RID: 13006 RVA: 0x00107BC0 File Offset: 0x00107BC0
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 6;
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x060032CF RID: 13007 RVA: 0x00107BE0 File Offset: 0x00107BE0
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x060032D0 RID: 13008 RVA: 0x00107BE8 File Offset: 0x00107BE8
		public override int FieldSize
		{
			get
			{
				return 571;
			}
		}

		// Token: 0x060032D1 RID: 13009 RVA: 0x00107BF0 File Offset: 0x00107BF0
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecT571FieldElement(x);
		}

		// Token: 0x060032D2 RID: 13010 RVA: 0x00107BF8 File Offset: 0x00107BF8
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecT571R1Point(this, x, y, withCompression);
		}

		// Token: 0x060032D3 RID: 13011 RVA: 0x00107C04 File Offset: 0x00107C04
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecT571R1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x060032D4 RID: 13012 RVA: 0x00107C14 File Offset: 0x00107C14
		public override bool IsKoblitz
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x060032D5 RID: 13013 RVA: 0x00107C18 File Offset: 0x00107C18
		public virtual int M
		{
			get
			{
				return 571;
			}
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x060032D6 RID: 13014 RVA: 0x00107C20 File Offset: 0x00107C20
		public virtual bool IsTrinomial
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x060032D7 RID: 13015 RVA: 0x00107C24 File Offset: 0x00107C24
		public virtual int K1
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x060032D8 RID: 13016 RVA: 0x00107C28 File Offset: 0x00107C28
		public virtual int K2
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x060032D9 RID: 13017 RVA: 0x00107C2C File Offset: 0x00107C2C
		public virtual int K3
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x060032DA RID: 13018 RVA: 0x00107C30 File Offset: 0x00107C30
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
			return new SecT571R1Curve.SecT571R1LookupTable(this, array, len);
		}

		// Token: 0x04001C6D RID: 7277
		private const int SECT571R1_DEFAULT_COORDS = 6;

		// Token: 0x04001C6E RID: 7278
		private const int SECT571R1_FE_LONGS = 9;

		// Token: 0x04001C6F RID: 7279
		private static readonly ECFieldElement[] SECT571R1_AFFINE_ZS = new ECFieldElement[]
		{
			new SecT571FieldElement(BigInteger.One)
		};

		// Token: 0x04001C70 RID: 7280
		protected readonly SecT571R1Point m_infinity;

		// Token: 0x04001C71 RID: 7281
		internal static readonly SecT571FieldElement SecT571R1_B = new SecT571FieldElement(new BigInteger(1, Hex.DecodeStrict("02F40E7E2221F295DE297117B7F3D62F5C6A97FFCB8CEFF1CD6BA8CE4A9A18AD84FFABBD8EFA59332BE7AD6756A66E294AFD185A78FF12AA520E4DE739BACA0C7FFEFF7F2955727A")));

		// Token: 0x04001C72 RID: 7282
		internal static readonly SecT571FieldElement SecT571R1_B_SQRT = (SecT571FieldElement)SecT571R1Curve.SecT571R1_B.Sqrt();

		// Token: 0x02000E49 RID: 3657
		private class SecT571R1LookupTable : AbstractECLookupTable
		{
			// Token: 0x06008D13 RID: 36115 RVA: 0x002A50C0 File Offset: 0x002A50C0
			internal SecT571R1LookupTable(SecT571R1Curve outer, ulong[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17001DA0 RID: 7584
			// (get) Token: 0x06008D14 RID: 36116 RVA: 0x002A50E0 File Offset: 0x002A50E0
			public override int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06008D15 RID: 36117 RVA: 0x002A50E8 File Offset: 0x002A50E8
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

			// Token: 0x06008D16 RID: 36118 RVA: 0x002A5188 File Offset: 0x002A5188
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

			// Token: 0x06008D17 RID: 36119 RVA: 0x002A51E4 File Offset: 0x002A51E4
			private ECPoint CreatePoint(ulong[] x, ulong[] y)
			{
				return this.m_outer.CreateRawPoint(new SecT571FieldElement(x), new SecT571FieldElement(y), SecT571R1Curve.SECT571R1_AFFINE_ZS, false);
			}

			// Token: 0x040041F9 RID: 16889
			private readonly SecT571R1Curve m_outer;

			// Token: 0x040041FA RID: 16890
			private readonly ulong[] m_table;

			// Token: 0x040041FB RID: 16891
			private readonly int m_size;
		}
	}
}
