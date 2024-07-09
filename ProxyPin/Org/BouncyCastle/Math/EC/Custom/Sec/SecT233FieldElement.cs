using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x020005CC RID: 1484
	internal class SecT233FieldElement : AbstractF2mFieldElement
	{
		// Token: 0x060030EE RID: 12526 RVA: 0x000FFC08 File Offset: 0x000FFC08
		public SecT233FieldElement(BigInteger x)
		{
			if (x == null || x.SignValue < 0 || x.BitLength > 233)
			{
				throw new ArgumentException("value invalid for SecT233FieldElement", "x");
			}
			this.x = SecT233Field.FromBigInteger(x);
		}

		// Token: 0x060030EF RID: 12527 RVA: 0x000FFC60 File Offset: 0x000FFC60
		public SecT233FieldElement()
		{
			this.x = Nat256.Create64();
		}

		// Token: 0x060030F0 RID: 12528 RVA: 0x000FFC74 File Offset: 0x000FFC74
		protected internal SecT233FieldElement(ulong[] x)
		{
			this.x = x;
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x060030F1 RID: 12529 RVA: 0x000FFC84 File Offset: 0x000FFC84
		public override bool IsOne
		{
			get
			{
				return Nat256.IsOne64(this.x);
			}
		}

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x060030F2 RID: 12530 RVA: 0x000FFC94 File Offset: 0x000FFC94
		public override bool IsZero
		{
			get
			{
				return Nat256.IsZero64(this.x);
			}
		}

		// Token: 0x060030F3 RID: 12531 RVA: 0x000FFCA4 File Offset: 0x000FFCA4
		public override bool TestBitZero()
		{
			return (this.x[0] & 1UL) != 0UL;
		}

		// Token: 0x060030F4 RID: 12532 RVA: 0x000FFCB8 File Offset: 0x000FFCB8
		public override BigInteger ToBigInteger()
		{
			return Nat256.ToBigInteger64(this.x);
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x060030F5 RID: 12533 RVA: 0x000FFCC8 File Offset: 0x000FFCC8
		public override string FieldName
		{
			get
			{
				return "SecT233Field";
			}
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x060030F6 RID: 12534 RVA: 0x000FFCD0 File Offset: 0x000FFCD0
		public override int FieldSize
		{
			get
			{
				return 233;
			}
		}

		// Token: 0x060030F7 RID: 12535 RVA: 0x000FFCD8 File Offset: 0x000FFCD8
		public override ECFieldElement Add(ECFieldElement b)
		{
			ulong[] z = Nat256.Create64();
			SecT233Field.Add(this.x, ((SecT233FieldElement)b).x, z);
			return new SecT233FieldElement(z);
		}

		// Token: 0x060030F8 RID: 12536 RVA: 0x000FFD0C File Offset: 0x000FFD0C
		public override ECFieldElement AddOne()
		{
			ulong[] z = Nat256.Create64();
			SecT233Field.AddOne(this.x, z);
			return new SecT233FieldElement(z);
		}

		// Token: 0x060030F9 RID: 12537 RVA: 0x000FFD38 File Offset: 0x000FFD38
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			return this.Add(b);
		}

		// Token: 0x060030FA RID: 12538 RVA: 0x000FFD44 File Offset: 0x000FFD44
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			ulong[] z = Nat256.Create64();
			SecT233Field.Multiply(this.x, ((SecT233FieldElement)b).x, z);
			return new SecT233FieldElement(z);
		}

		// Token: 0x060030FB RID: 12539 RVA: 0x000FFD78 File Offset: 0x000FFD78
		public override ECFieldElement MultiplyMinusProduct(ECFieldElement b, ECFieldElement x, ECFieldElement y)
		{
			return this.MultiplyPlusProduct(b, x, y);
		}

		// Token: 0x060030FC RID: 12540 RVA: 0x000FFD84 File Offset: 0x000FFD84
		public override ECFieldElement MultiplyPlusProduct(ECFieldElement b, ECFieldElement x, ECFieldElement y)
		{
			ulong[] array = this.x;
			ulong[] y2 = ((SecT233FieldElement)b).x;
			ulong[] array2 = ((SecT233FieldElement)x).x;
			ulong[] y3 = ((SecT233FieldElement)y).x;
			ulong[] array3 = Nat256.CreateExt64();
			SecT233Field.MultiplyAddToExt(array, y2, array3);
			SecT233Field.MultiplyAddToExt(array2, y3, array3);
			ulong[] z = Nat256.Create64();
			SecT233Field.Reduce(array3, z);
			return new SecT233FieldElement(z);
		}

		// Token: 0x060030FD RID: 12541 RVA: 0x000FFDF0 File Offset: 0x000FFDF0
		public override ECFieldElement Divide(ECFieldElement b)
		{
			return this.Multiply(b.Invert());
		}

		// Token: 0x060030FE RID: 12542 RVA: 0x000FFE00 File Offset: 0x000FFE00
		public override ECFieldElement Negate()
		{
			return this;
		}

		// Token: 0x060030FF RID: 12543 RVA: 0x000FFE04 File Offset: 0x000FFE04
		public override ECFieldElement Square()
		{
			ulong[] z = Nat256.Create64();
			SecT233Field.Square(this.x, z);
			return new SecT233FieldElement(z);
		}

		// Token: 0x06003100 RID: 12544 RVA: 0x000FFE30 File Offset: 0x000FFE30
		public override ECFieldElement SquareMinusProduct(ECFieldElement x, ECFieldElement y)
		{
			return this.SquarePlusProduct(x, y);
		}

		// Token: 0x06003101 RID: 12545 RVA: 0x000FFE3C File Offset: 0x000FFE3C
		public override ECFieldElement SquarePlusProduct(ECFieldElement x, ECFieldElement y)
		{
			ulong[] array = this.x;
			ulong[] array2 = ((SecT233FieldElement)x).x;
			ulong[] y2 = ((SecT233FieldElement)y).x;
			ulong[] array3 = Nat256.CreateExt64();
			SecT233Field.SquareAddToExt(array, array3);
			SecT233Field.MultiplyAddToExt(array2, y2, array3);
			ulong[] z = Nat256.Create64();
			SecT233Field.Reduce(array3, z);
			return new SecT233FieldElement(z);
		}

		// Token: 0x06003102 RID: 12546 RVA: 0x000FFE98 File Offset: 0x000FFE98
		public override ECFieldElement SquarePow(int pow)
		{
			if (pow < 1)
			{
				return this;
			}
			ulong[] z = Nat256.Create64();
			SecT233Field.SquareN(this.x, pow, z);
			return new SecT233FieldElement(z);
		}

		// Token: 0x06003103 RID: 12547 RVA: 0x000FFECC File Offset: 0x000FFECC
		public override ECFieldElement HalfTrace()
		{
			ulong[] z = Nat256.Create64();
			SecT233Field.HalfTrace(this.x, z);
			return new SecT233FieldElement(z);
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x06003104 RID: 12548 RVA: 0x000FFEF8 File Offset: 0x000FFEF8
		public override bool HasFastTrace
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003105 RID: 12549 RVA: 0x000FFEFC File Offset: 0x000FFEFC
		public override int Trace()
		{
			return (int)SecT233Field.Trace(this.x);
		}

		// Token: 0x06003106 RID: 12550 RVA: 0x000FFF0C File Offset: 0x000FFF0C
		public override ECFieldElement Invert()
		{
			ulong[] z = Nat256.Create64();
			SecT233Field.Invert(this.x, z);
			return new SecT233FieldElement(z);
		}

		// Token: 0x06003107 RID: 12551 RVA: 0x000FFF38 File Offset: 0x000FFF38
		public override ECFieldElement Sqrt()
		{
			ulong[] z = Nat256.Create64();
			SecT233Field.Sqrt(this.x, z);
			return new SecT233FieldElement(z);
		}

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x06003108 RID: 12552 RVA: 0x000FFF64 File Offset: 0x000FFF64
		public virtual int Representation
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x06003109 RID: 12553 RVA: 0x000FFF68 File Offset: 0x000FFF68
		public virtual int M
		{
			get
			{
				return 233;
			}
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x0600310A RID: 12554 RVA: 0x000FFF70 File Offset: 0x000FFF70
		public virtual int K1
		{
			get
			{
				return 74;
			}
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x0600310B RID: 12555 RVA: 0x000FFF74 File Offset: 0x000FFF74
		public virtual int K2
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x0600310C RID: 12556 RVA: 0x000FFF78 File Offset: 0x000FFF78
		public virtual int K3
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600310D RID: 12557 RVA: 0x000FFF7C File Offset: 0x000FFF7C
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SecT233FieldElement);
		}

		// Token: 0x0600310E RID: 12558 RVA: 0x000FFF8C File Offset: 0x000FFF8C
		public override bool Equals(ECFieldElement other)
		{
			return this.Equals(other as SecT233FieldElement);
		}

		// Token: 0x0600310F RID: 12559 RVA: 0x000FFF9C File Offset: 0x000FFF9C
		public virtual bool Equals(SecT233FieldElement other)
		{
			return this == other || (other != null && Nat256.Eq64(this.x, other.x));
		}

		// Token: 0x06003110 RID: 12560 RVA: 0x000FFFC0 File Offset: 0x000FFFC0
		public override int GetHashCode()
		{
			return 2330074 ^ Arrays.GetHashCode(this.x, 0, 4);
		}

		// Token: 0x04001C3E RID: 7230
		protected internal readonly ulong[] x;
	}
}
