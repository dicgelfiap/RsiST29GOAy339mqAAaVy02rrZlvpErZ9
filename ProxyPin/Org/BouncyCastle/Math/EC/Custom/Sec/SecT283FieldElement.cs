using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x020005D6 RID: 1494
	internal class SecT283FieldElement : AbstractF2mFieldElement
	{
		// Token: 0x060031B1 RID: 12721 RVA: 0x0010325C File Offset: 0x0010325C
		public SecT283FieldElement(BigInteger x)
		{
			if (x == null || x.SignValue < 0 || x.BitLength > 283)
			{
				throw new ArgumentException("value invalid for SecT283FieldElement", "x");
			}
			this.x = SecT283Field.FromBigInteger(x);
		}

		// Token: 0x060031B2 RID: 12722 RVA: 0x001032B4 File Offset: 0x001032B4
		public SecT283FieldElement()
		{
			this.x = Nat320.Create64();
		}

		// Token: 0x060031B3 RID: 12723 RVA: 0x001032C8 File Offset: 0x001032C8
		protected internal SecT283FieldElement(ulong[] x)
		{
			this.x = x;
		}

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x060031B4 RID: 12724 RVA: 0x001032D8 File Offset: 0x001032D8
		public override bool IsOne
		{
			get
			{
				return Nat320.IsOne64(this.x);
			}
		}

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x060031B5 RID: 12725 RVA: 0x001032E8 File Offset: 0x001032E8
		public override bool IsZero
		{
			get
			{
				return Nat320.IsZero64(this.x);
			}
		}

		// Token: 0x060031B6 RID: 12726 RVA: 0x001032F8 File Offset: 0x001032F8
		public override bool TestBitZero()
		{
			return (this.x[0] & 1UL) != 0UL;
		}

		// Token: 0x060031B7 RID: 12727 RVA: 0x0010330C File Offset: 0x0010330C
		public override BigInteger ToBigInteger()
		{
			return Nat320.ToBigInteger64(this.x);
		}

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x060031B8 RID: 12728 RVA: 0x0010331C File Offset: 0x0010331C
		public override string FieldName
		{
			get
			{
				return "SecT283Field";
			}
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x060031B9 RID: 12729 RVA: 0x00103324 File Offset: 0x00103324
		public override int FieldSize
		{
			get
			{
				return 283;
			}
		}

		// Token: 0x060031BA RID: 12730 RVA: 0x0010332C File Offset: 0x0010332C
		public override ECFieldElement Add(ECFieldElement b)
		{
			ulong[] z = Nat320.Create64();
			SecT283Field.Add(this.x, ((SecT283FieldElement)b).x, z);
			return new SecT283FieldElement(z);
		}

		// Token: 0x060031BB RID: 12731 RVA: 0x00103360 File Offset: 0x00103360
		public override ECFieldElement AddOne()
		{
			ulong[] z = Nat320.Create64();
			SecT283Field.AddOne(this.x, z);
			return new SecT283FieldElement(z);
		}

		// Token: 0x060031BC RID: 12732 RVA: 0x0010338C File Offset: 0x0010338C
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			return this.Add(b);
		}

		// Token: 0x060031BD RID: 12733 RVA: 0x00103398 File Offset: 0x00103398
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			ulong[] z = Nat320.Create64();
			SecT283Field.Multiply(this.x, ((SecT283FieldElement)b).x, z);
			return new SecT283FieldElement(z);
		}

		// Token: 0x060031BE RID: 12734 RVA: 0x001033CC File Offset: 0x001033CC
		public override ECFieldElement MultiplyMinusProduct(ECFieldElement b, ECFieldElement x, ECFieldElement y)
		{
			return this.MultiplyPlusProduct(b, x, y);
		}

		// Token: 0x060031BF RID: 12735 RVA: 0x001033D8 File Offset: 0x001033D8
		public override ECFieldElement MultiplyPlusProduct(ECFieldElement b, ECFieldElement x, ECFieldElement y)
		{
			ulong[] array = this.x;
			ulong[] y2 = ((SecT283FieldElement)b).x;
			ulong[] array2 = ((SecT283FieldElement)x).x;
			ulong[] y3 = ((SecT283FieldElement)y).x;
			ulong[] array3 = Nat.Create64(9);
			SecT283Field.MultiplyAddToExt(array, y2, array3);
			SecT283Field.MultiplyAddToExt(array2, y3, array3);
			ulong[] z = Nat320.Create64();
			SecT283Field.Reduce(array3, z);
			return new SecT283FieldElement(z);
		}

		// Token: 0x060031C0 RID: 12736 RVA: 0x00103448 File Offset: 0x00103448
		public override ECFieldElement Divide(ECFieldElement b)
		{
			return this.Multiply(b.Invert());
		}

		// Token: 0x060031C1 RID: 12737 RVA: 0x00103458 File Offset: 0x00103458
		public override ECFieldElement Negate()
		{
			return this;
		}

		// Token: 0x060031C2 RID: 12738 RVA: 0x0010345C File Offset: 0x0010345C
		public override ECFieldElement Square()
		{
			ulong[] z = Nat320.Create64();
			SecT283Field.Square(this.x, z);
			return new SecT283FieldElement(z);
		}

		// Token: 0x060031C3 RID: 12739 RVA: 0x00103488 File Offset: 0x00103488
		public override ECFieldElement SquareMinusProduct(ECFieldElement x, ECFieldElement y)
		{
			return this.SquarePlusProduct(x, y);
		}

		// Token: 0x060031C4 RID: 12740 RVA: 0x00103494 File Offset: 0x00103494
		public override ECFieldElement SquarePlusProduct(ECFieldElement x, ECFieldElement y)
		{
			ulong[] array = this.x;
			ulong[] array2 = ((SecT283FieldElement)x).x;
			ulong[] y2 = ((SecT283FieldElement)y).x;
			ulong[] array3 = Nat.Create64(9);
			SecT283Field.SquareAddToExt(array, array3);
			SecT283Field.MultiplyAddToExt(array2, y2, array3);
			ulong[] z = Nat320.Create64();
			SecT283Field.Reduce(array3, z);
			return new SecT283FieldElement(z);
		}

		// Token: 0x060031C5 RID: 12741 RVA: 0x001034F4 File Offset: 0x001034F4
		public override ECFieldElement SquarePow(int pow)
		{
			if (pow < 1)
			{
				return this;
			}
			ulong[] z = Nat320.Create64();
			SecT283Field.SquareN(this.x, pow, z);
			return new SecT283FieldElement(z);
		}

		// Token: 0x060031C6 RID: 12742 RVA: 0x00103528 File Offset: 0x00103528
		public override ECFieldElement HalfTrace()
		{
			ulong[] z = Nat320.Create64();
			SecT283Field.HalfTrace(this.x, z);
			return new SecT283FieldElement(z);
		}

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x060031C7 RID: 12743 RVA: 0x00103554 File Offset: 0x00103554
		public override bool HasFastTrace
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060031C8 RID: 12744 RVA: 0x00103558 File Offset: 0x00103558
		public override int Trace()
		{
			return (int)SecT283Field.Trace(this.x);
		}

		// Token: 0x060031C9 RID: 12745 RVA: 0x00103568 File Offset: 0x00103568
		public override ECFieldElement Invert()
		{
			ulong[] z = Nat320.Create64();
			SecT283Field.Invert(this.x, z);
			return new SecT283FieldElement(z);
		}

		// Token: 0x060031CA RID: 12746 RVA: 0x00103594 File Offset: 0x00103594
		public override ECFieldElement Sqrt()
		{
			ulong[] z = Nat320.Create64();
			SecT283Field.Sqrt(this.x, z);
			return new SecT283FieldElement(z);
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x060031CB RID: 12747 RVA: 0x001035C0 File Offset: 0x001035C0
		public virtual int Representation
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x060031CC RID: 12748 RVA: 0x001035C4 File Offset: 0x001035C4
		public virtual int M
		{
			get
			{
				return 283;
			}
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x060031CD RID: 12749 RVA: 0x001035CC File Offset: 0x001035CC
		public virtual int K1
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x060031CE RID: 12750 RVA: 0x001035D0 File Offset: 0x001035D0
		public virtual int K2
		{
			get
			{
				return 7;
			}
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x060031CF RID: 12751 RVA: 0x001035D4 File Offset: 0x001035D4
		public virtual int K3
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x060031D0 RID: 12752 RVA: 0x001035D8 File Offset: 0x001035D8
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SecT283FieldElement);
		}

		// Token: 0x060031D1 RID: 12753 RVA: 0x001035E8 File Offset: 0x001035E8
		public override bool Equals(ECFieldElement other)
		{
			return this.Equals(other as SecT283FieldElement);
		}

		// Token: 0x060031D2 RID: 12754 RVA: 0x001035F8 File Offset: 0x001035F8
		public virtual bool Equals(SecT283FieldElement other)
		{
			return this == other || (other != null && Nat320.Eq64(this.x, other.x));
		}

		// Token: 0x060031D3 RID: 12755 RVA: 0x0010361C File Offset: 0x0010361C
		public override int GetHashCode()
		{
			return 2831275 ^ Arrays.GetHashCode(this.x, 0, 5);
		}

		// Token: 0x04001C51 RID: 7249
		protected internal readonly ulong[] x;
	}
}
