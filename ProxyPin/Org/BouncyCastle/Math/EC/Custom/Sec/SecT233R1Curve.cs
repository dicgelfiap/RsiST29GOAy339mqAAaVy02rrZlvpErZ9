using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x020005CF RID: 1487
	internal class SecT233R1Curve : AbstractF2mCurve
	{
		// Token: 0x0600312C RID: 12588 RVA: 0x0010088C File Offset: 0x0010088C
		public SecT233R1Curve() : base(233, 74, 0, 0)
		{
			this.m_infinity = new SecT233R1Point(this, null, null);
			this.m_a = this.FromBigInteger(BigInteger.One);
			this.m_b = this.FromBigInteger(new BigInteger(1, Hex.DecodeStrict("0066647EDE6C332C7F8C0923BB58213B333B20E9CE4281FE115F7D8F90AD")));
			this.m_order = new BigInteger(1, Hex.DecodeStrict("01000000000000000000000000000013E974E72F8A6922031D2603CFE0D7"));
			this.m_cofactor = BigInteger.Two;
			this.m_coord = 6;
		}

		// Token: 0x0600312D RID: 12589 RVA: 0x00100910 File Offset: 0x00100910
		protected override ECCurve CloneCurve()
		{
			return new SecT233R1Curve();
		}

		// Token: 0x0600312E RID: 12590 RVA: 0x00100918 File Offset: 0x00100918
		public override bool SupportsCoordinateSystem(int coord)
		{
			return coord == 6;
		}

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x0600312F RID: 12591 RVA: 0x00100938 File Offset: 0x00100938
		public override ECPoint Infinity
		{
			get
			{
				return this.m_infinity;
			}
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06003130 RID: 12592 RVA: 0x00100940 File Offset: 0x00100940
		public override int FieldSize
		{
			get
			{
				return 233;
			}
		}

		// Token: 0x06003131 RID: 12593 RVA: 0x00100948 File Offset: 0x00100948
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new SecT233FieldElement(x);
		}

		// Token: 0x06003132 RID: 12594 RVA: 0x00100950 File Offset: 0x00100950
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			return new SecT233R1Point(this, x, y, withCompression);
		}

		// Token: 0x06003133 RID: 12595 RVA: 0x0010095C File Offset: 0x0010095C
		protected internal override ECPoint CreateRawPoint(ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression)
		{
			return new SecT233R1Point(this, x, y, zs, withCompression);
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06003134 RID: 12596 RVA: 0x0010096C File Offset: 0x0010096C
		public override bool IsKoblitz
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06003135 RID: 12597 RVA: 0x00100970 File Offset: 0x00100970
		public virtual int M
		{
			get
			{
				return 233;
			}
		}

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06003136 RID: 12598 RVA: 0x00100978 File Offset: 0x00100978
		public virtual bool IsTrinomial
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06003137 RID: 12599 RVA: 0x0010097C File Offset: 0x0010097C
		public virtual int K1
		{
			get
			{
				return 74;
			}
		}

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06003138 RID: 12600 RVA: 0x00100980 File Offset: 0x00100980
		public virtual int K2
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06003139 RID: 12601 RVA: 0x00100984 File Offset: 0x00100984
		public virtual int K3
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600313A RID: 12602 RVA: 0x00100988 File Offset: 0x00100988
		public override ECLookupTable CreateCacheSafeLookupTable(ECPoint[] points, int off, int len)
		{
			ulong[] array = new ulong[len * 4 * 2];
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				ECPoint ecpoint = points[off + i];
				Nat256.Copy64(((SecT233FieldElement)ecpoint.RawXCoord).x, 0, array, num);
				num += 4;
				Nat256.Copy64(((SecT233FieldElement)ecpoint.RawYCoord).x, 0, array, num);
				num += 4;
			}
			return new SecT233R1Curve.SecT233R1LookupTable(this, array, len);
		}

		// Token: 0x04001C43 RID: 7235
		private const int SECT233R1_DEFAULT_COORDS = 6;

		// Token: 0x04001C44 RID: 7236
		private const int SECT233R1_FE_LONGS = 4;

		// Token: 0x04001C45 RID: 7237
		private static readonly ECFieldElement[] SECT233R1_AFFINE_ZS = new ECFieldElement[]
		{
			new SecT233FieldElement(BigInteger.One)
		};

		// Token: 0x04001C46 RID: 7238
		protected readonly SecT233R1Point m_infinity;

		// Token: 0x02000E42 RID: 3650
		private class SecT233R1LookupTable : AbstractECLookupTable
		{
			// Token: 0x06008CF0 RID: 36080 RVA: 0x002A4814 File Offset: 0x002A4814
			internal SecT233R1LookupTable(SecT233R1Curve outer, ulong[] table, int size)
			{
				this.m_outer = outer;
				this.m_table = table;
				this.m_size = size;
			}

			// Token: 0x17001D99 RID: 7577
			// (get) Token: 0x06008CF1 RID: 36081 RVA: 0x002A4834 File Offset: 0x002A4834
			public override int Size
			{
				get
				{
					return this.m_size;
				}
			}

			// Token: 0x06008CF2 RID: 36082 RVA: 0x002A483C File Offset: 0x002A483C
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

			// Token: 0x06008CF3 RID: 36083 RVA: 0x002A48D8 File Offset: 0x002A48D8
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

			// Token: 0x06008CF4 RID: 36084 RVA: 0x002A4930 File Offset: 0x002A4930
			private ECPoint CreatePoint(ulong[] x, ulong[] y)
			{
				return this.m_outer.CreateRawPoint(new SecT233FieldElement(x), new SecT233FieldElement(y), SecT233R1Curve.SECT233R1_AFFINE_ZS, false);
			}

			// Token: 0x040041E4 RID: 16868
			private readonly SecT233R1Curve m_outer;

			// Token: 0x040041E5 RID: 16869
			private readonly ulong[] m_table;

			// Token: 0x040041E6 RID: 16870
			private readonly int m_size;
		}
	}
}
