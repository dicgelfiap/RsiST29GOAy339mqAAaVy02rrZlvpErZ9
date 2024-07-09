using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x020005E2 RID: 1506
	internal class SecT571FieldElement : AbstractF2mFieldElement
	{
		// Token: 0x0600328E RID: 12942 RVA: 0x00106EBC File Offset: 0x00106EBC
		public SecT571FieldElement(BigInteger x)
		{
			if (x == null || x.SignValue < 0 || x.BitLength > 571)
			{
				throw new ArgumentException("value invalid for SecT571FieldElement", "x");
			}
			this.x = SecT571Field.FromBigInteger(x);
		}

		// Token: 0x0600328F RID: 12943 RVA: 0x00106F14 File Offset: 0x00106F14
		public SecT571FieldElement()
		{
			this.x = Nat576.Create64();
		}

		// Token: 0x06003290 RID: 12944 RVA: 0x00106F28 File Offset: 0x00106F28
		protected internal SecT571FieldElement(ulong[] x)
		{
			this.x = x;
		}

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06003291 RID: 12945 RVA: 0x00106F38 File Offset: 0x00106F38
		public override bool IsOne
		{
			get
			{
				return Nat576.IsOne64(this.x);
			}
		}

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x06003292 RID: 12946 RVA: 0x00106F48 File Offset: 0x00106F48
		public override bool IsZero
		{
			get
			{
				return Nat576.IsZero64(this.x);
			}
		}

		// Token: 0x06003293 RID: 12947 RVA: 0x00106F58 File Offset: 0x00106F58
		public override bool TestBitZero()
		{
			return (this.x[0] & 1UL) != 0UL;
		}

		// Token: 0x06003294 RID: 12948 RVA: 0x00106F6C File Offset: 0x00106F6C
		public override BigInteger ToBigInteger()
		{
			return Nat576.ToBigInteger64(this.x);
		}

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x06003295 RID: 12949 RVA: 0x00106F7C File Offset: 0x00106F7C
		public override string FieldName
		{
			get
			{
				return "SecT571Field";
			}
		}

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x06003296 RID: 12950 RVA: 0x00106F84 File Offset: 0x00106F84
		public override int FieldSize
		{
			get
			{
				return 571;
			}
		}

		// Token: 0x06003297 RID: 12951 RVA: 0x00106F8C File Offset: 0x00106F8C
		public override ECFieldElement Add(ECFieldElement b)
		{
			ulong[] z = Nat576.Create64();
			SecT571Field.Add(this.x, ((SecT571FieldElement)b).x, z);
			return new SecT571FieldElement(z);
		}

		// Token: 0x06003298 RID: 12952 RVA: 0x00106FC0 File Offset: 0x00106FC0
		public override ECFieldElement AddOne()
		{
			ulong[] z = Nat576.Create64();
			SecT571Field.AddOne(this.x, z);
			return new SecT571FieldElement(z);
		}

		// Token: 0x06003299 RID: 12953 RVA: 0x00106FEC File Offset: 0x00106FEC
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			return this.Add(b);
		}

		// Token: 0x0600329A RID: 12954 RVA: 0x00106FF8 File Offset: 0x00106FF8
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			ulong[] z = Nat576.Create64();
			SecT571Field.Multiply(this.x, ((SecT571FieldElement)b).x, z);
			return new SecT571FieldElement(z);
		}

		// Token: 0x0600329B RID: 12955 RVA: 0x0010702C File Offset: 0x0010702C
		public override ECFieldElement MultiplyMinusProduct(ECFieldElement b, ECFieldElement x, ECFieldElement y)
		{
			return this.MultiplyPlusProduct(b, x, y);
		}

		// Token: 0x0600329C RID: 12956 RVA: 0x00107038 File Offset: 0x00107038
		public override ECFieldElement MultiplyPlusProduct(ECFieldElement b, ECFieldElement x, ECFieldElement y)
		{
			ulong[] array = this.x;
			ulong[] y2 = ((SecT571FieldElement)b).x;
			ulong[] array2 = ((SecT571FieldElement)x).x;
			ulong[] y3 = ((SecT571FieldElement)y).x;
			ulong[] array3 = Nat576.CreateExt64();
			SecT571Field.MultiplyAddToExt(array, y2, array3);
			SecT571Field.MultiplyAddToExt(array2, y3, array3);
			ulong[] z = Nat576.Create64();
			SecT571Field.Reduce(array3, z);
			return new SecT571FieldElement(z);
		}

		// Token: 0x0600329D RID: 12957 RVA: 0x001070A4 File Offset: 0x001070A4
		public override ECFieldElement Divide(ECFieldElement b)
		{
			return this.Multiply(b.Invert());
		}

		// Token: 0x0600329E RID: 12958 RVA: 0x001070B4 File Offset: 0x001070B4
		public override ECFieldElement Negate()
		{
			return this;
		}

		// Token: 0x0600329F RID: 12959 RVA: 0x001070B8 File Offset: 0x001070B8
		public override ECFieldElement Square()
		{
			ulong[] z = Nat576.Create64();
			SecT571Field.Square(this.x, z);
			return new SecT571FieldElement(z);
		}

		// Token: 0x060032A0 RID: 12960 RVA: 0x001070E4 File Offset: 0x001070E4
		public override ECFieldElement SquareMinusProduct(ECFieldElement x, ECFieldElement y)
		{
			return this.SquarePlusProduct(x, y);
		}

		// Token: 0x060032A1 RID: 12961 RVA: 0x001070F0 File Offset: 0x001070F0
		public override ECFieldElement SquarePlusProduct(ECFieldElement x, ECFieldElement y)
		{
			ulong[] array = this.x;
			ulong[] array2 = ((SecT571FieldElement)x).x;
			ulong[] y2 = ((SecT571FieldElement)y).x;
			ulong[] array3 = Nat576.CreateExt64();
			SecT571Field.SquareAddToExt(array, array3);
			SecT571Field.MultiplyAddToExt(array2, y2, array3);
			ulong[] z = Nat576.Create64();
			SecT571Field.Reduce(array3, z);
			return new SecT571FieldElement(z);
		}

		// Token: 0x060032A2 RID: 12962 RVA: 0x0010714C File Offset: 0x0010714C
		public override ECFieldElement SquarePow(int pow)
		{
			if (pow < 1)
			{
				return this;
			}
			ulong[] z = Nat576.Create64();
			SecT571Field.SquareN(this.x, pow, z);
			return new SecT571FieldElement(z);
		}

		// Token: 0x060032A3 RID: 12963 RVA: 0x00107180 File Offset: 0x00107180
		public override ECFieldElement HalfTrace()
		{
			ulong[] z = Nat576.Create64();
			SecT571Field.HalfTrace(this.x, z);
			return new SecT571FieldElement(z);
		}

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x060032A4 RID: 12964 RVA: 0x001071AC File Offset: 0x001071AC
		public override bool HasFastTrace
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060032A5 RID: 12965 RVA: 0x001071B0 File Offset: 0x001071B0
		public override int Trace()
		{
			return (int)SecT571Field.Trace(this.x);
		}

		// Token: 0x060032A6 RID: 12966 RVA: 0x001071C0 File Offset: 0x001071C0
		public override ECFieldElement Invert()
		{
			ulong[] z = Nat576.Create64();
			SecT571Field.Invert(this.x, z);
			return new SecT571FieldElement(z);
		}

		// Token: 0x060032A7 RID: 12967 RVA: 0x001071EC File Offset: 0x001071EC
		public override ECFieldElement Sqrt()
		{
			ulong[] z = Nat576.Create64();
			SecT571Field.Sqrt(this.x, z);
			return new SecT571FieldElement(z);
		}

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x060032A8 RID: 12968 RVA: 0x00107218 File Offset: 0x00107218
		public virtual int Representation
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x060032A9 RID: 12969 RVA: 0x0010721C File Offset: 0x0010721C
		public virtual int M
		{
			get
			{
				return 571;
			}
		}

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x060032AA RID: 12970 RVA: 0x00107224 File Offset: 0x00107224
		public virtual int K1
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x060032AB RID: 12971 RVA: 0x00107228 File Offset: 0x00107228
		public virtual int K2
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x060032AC RID: 12972 RVA: 0x0010722C File Offset: 0x0010722C
		public virtual int K3
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x060032AD RID: 12973 RVA: 0x00107230 File Offset: 0x00107230
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SecT571FieldElement);
		}

		// Token: 0x060032AE RID: 12974 RVA: 0x00107240 File Offset: 0x00107240
		public override bool Equals(ECFieldElement other)
		{
			return this.Equals(other as SecT571FieldElement);
		}

		// Token: 0x060032AF RID: 12975 RVA: 0x00107250 File Offset: 0x00107250
		public virtual bool Equals(SecT571FieldElement other)
		{
			return this == other || (other != null && Nat576.Eq64(this.x, other.x));
		}

		// Token: 0x060032B0 RID: 12976 RVA: 0x00107274 File Offset: 0x00107274
		public override int GetHashCode()
		{
			return 5711052 ^ Arrays.GetHashCode(this.x, 0, 9);
		}

		// Token: 0x04001C68 RID: 7272
		protected internal readonly ulong[] x;
	}
}
