using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Math.EC
{
	// Token: 0x0200060F RID: 1551
	public class F2mFieldElement : AbstractF2mFieldElement
	{
		// Token: 0x0600348F RID: 13455 RVA: 0x00113AA8 File Offset: 0x00113AA8
		[Obsolete("Use ECCurve.FromBigInteger to construct field elements")]
		public F2mFieldElement(int m, int k1, int k2, int k3, BigInteger x)
		{
			if (x == null || x.SignValue < 0 || x.BitLength > m)
			{
				throw new ArgumentException("value invalid in F2m field element", "x");
			}
			if (k2 == 0 && k3 == 0)
			{
				this.representation = 2;
				this.ks = new int[]
				{
					k1
				};
			}
			else
			{
				if (k2 >= k3)
				{
					throw new ArgumentException("k2 must be smaller than k3");
				}
				if (k2 <= 0)
				{
					throw new ArgumentException("k2 must be larger than 0");
				}
				this.representation = 3;
				this.ks = new int[]
				{
					k1,
					k2,
					k3
				};
			}
			this.m = m;
			this.x = new LongArray(x);
		}

		// Token: 0x06003490 RID: 13456 RVA: 0x00113B78 File Offset: 0x00113B78
		[Obsolete("Use ECCurve.FromBigInteger to construct field elements")]
		public F2mFieldElement(int m, int k, BigInteger x) : this(m, k, 0, 0, x)
		{
		}

		// Token: 0x06003491 RID: 13457 RVA: 0x00113B88 File Offset: 0x00113B88
		internal F2mFieldElement(int m, int[] ks, LongArray x)
		{
			this.m = m;
			this.representation = ((ks.Length == 1) ? 2 : 3);
			this.ks = ks;
			this.x = x;
		}

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x06003492 RID: 13458 RVA: 0x00113BBC File Offset: 0x00113BBC
		public override int BitLength
		{
			get
			{
				return this.x.Degree();
			}
		}

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x06003493 RID: 13459 RVA: 0x00113BCC File Offset: 0x00113BCC
		public override bool IsOne
		{
			get
			{
				return this.x.IsOne();
			}
		}

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06003494 RID: 13460 RVA: 0x00113BDC File Offset: 0x00113BDC
		public override bool IsZero
		{
			get
			{
				return this.x.IsZero();
			}
		}

		// Token: 0x06003495 RID: 13461 RVA: 0x00113BEC File Offset: 0x00113BEC
		public override bool TestBitZero()
		{
			return this.x.TestBitZero();
		}

		// Token: 0x06003496 RID: 13462 RVA: 0x00113BFC File Offset: 0x00113BFC
		public override BigInteger ToBigInteger()
		{
			return this.x.ToBigInteger();
		}

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x06003497 RID: 13463 RVA: 0x00113C0C File Offset: 0x00113C0C
		public override string FieldName
		{
			get
			{
				return "F2m";
			}
		}

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06003498 RID: 13464 RVA: 0x00113C14 File Offset: 0x00113C14
		public override int FieldSize
		{
			get
			{
				return this.m;
			}
		}

		// Token: 0x06003499 RID: 13465 RVA: 0x00113C1C File Offset: 0x00113C1C
		public static void CheckFieldElements(ECFieldElement a, ECFieldElement b)
		{
			if (!(a is F2mFieldElement) || !(b is F2mFieldElement))
			{
				throw new ArgumentException("Field elements are not both instances of F2mFieldElement");
			}
			F2mFieldElement f2mFieldElement = (F2mFieldElement)a;
			F2mFieldElement f2mFieldElement2 = (F2mFieldElement)b;
			if (f2mFieldElement.representation != f2mFieldElement2.representation)
			{
				throw new ArgumentException("One of the F2m field elements has incorrect representation");
			}
			if (f2mFieldElement.m != f2mFieldElement2.m || !Arrays.AreEqual(f2mFieldElement.ks, f2mFieldElement2.ks))
			{
				throw new ArgumentException("Field elements are not elements of the same field F2m");
			}
		}

		// Token: 0x0600349A RID: 13466 RVA: 0x00113CAC File Offset: 0x00113CAC
		public override ECFieldElement Add(ECFieldElement b)
		{
			LongArray longArray = this.x.Copy();
			F2mFieldElement f2mFieldElement = (F2mFieldElement)b;
			longArray.AddShiftedByWords(f2mFieldElement.x, 0);
			return new F2mFieldElement(this.m, this.ks, longArray);
		}

		// Token: 0x0600349B RID: 13467 RVA: 0x00113CF0 File Offset: 0x00113CF0
		public override ECFieldElement AddOne()
		{
			return new F2mFieldElement(this.m, this.ks, this.x.AddOne());
		}

		// Token: 0x0600349C RID: 13468 RVA: 0x00113D10 File Offset: 0x00113D10
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			return this.Add(b);
		}

		// Token: 0x0600349D RID: 13469 RVA: 0x00113D1C File Offset: 0x00113D1C
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			return new F2mFieldElement(this.m, this.ks, this.x.ModMultiply(((F2mFieldElement)b).x, this.m, this.ks));
		}

		// Token: 0x0600349E RID: 13470 RVA: 0x00113D54 File Offset: 0x00113D54
		public override ECFieldElement MultiplyMinusProduct(ECFieldElement b, ECFieldElement x, ECFieldElement y)
		{
			return this.MultiplyPlusProduct(b, x, y);
		}

		// Token: 0x0600349F RID: 13471 RVA: 0x00113D60 File Offset: 0x00113D60
		public override ECFieldElement MultiplyPlusProduct(ECFieldElement b, ECFieldElement x, ECFieldElement y)
		{
			LongArray longArray = this.x;
			LongArray longArray2 = ((F2mFieldElement)b).x;
			LongArray longArray3 = ((F2mFieldElement)x).x;
			LongArray other = ((F2mFieldElement)y).x;
			LongArray longArray4 = longArray.Multiply(longArray2, this.m, this.ks);
			LongArray other2 = longArray3.Multiply(other, this.m, this.ks);
			if (longArray4 == longArray || longArray4 == longArray2)
			{
				longArray4 = longArray4.Copy();
			}
			longArray4.AddShiftedByWords(other2, 0);
			longArray4.Reduce(this.m, this.ks);
			return new F2mFieldElement(this.m, this.ks, longArray4);
		}

		// Token: 0x060034A0 RID: 13472 RVA: 0x00113E10 File Offset: 0x00113E10
		public override ECFieldElement Divide(ECFieldElement b)
		{
			ECFieldElement b2 = b.Invert();
			return this.Multiply(b2);
		}

		// Token: 0x060034A1 RID: 13473 RVA: 0x00113E30 File Offset: 0x00113E30
		public override ECFieldElement Negate()
		{
			return this;
		}

		// Token: 0x060034A2 RID: 13474 RVA: 0x00113E34 File Offset: 0x00113E34
		public override ECFieldElement Square()
		{
			return new F2mFieldElement(this.m, this.ks, this.x.ModSquare(this.m, this.ks));
		}

		// Token: 0x060034A3 RID: 13475 RVA: 0x00113E60 File Offset: 0x00113E60
		public override ECFieldElement SquareMinusProduct(ECFieldElement x, ECFieldElement y)
		{
			return this.SquarePlusProduct(x, y);
		}

		// Token: 0x060034A4 RID: 13476 RVA: 0x00113E6C File Offset: 0x00113E6C
		public override ECFieldElement SquarePlusProduct(ECFieldElement x, ECFieldElement y)
		{
			LongArray longArray = this.x;
			LongArray longArray2 = ((F2mFieldElement)x).x;
			LongArray other = ((F2mFieldElement)y).x;
			LongArray longArray3 = longArray.Square(this.m, this.ks);
			LongArray other2 = longArray2.Multiply(other, this.m, this.ks);
			if (longArray3 == longArray)
			{
				longArray3 = longArray3.Copy();
			}
			longArray3.AddShiftedByWords(other2, 0);
			longArray3.Reduce(this.m, this.ks);
			return new F2mFieldElement(this.m, this.ks, longArray3);
		}

		// Token: 0x060034A5 RID: 13477 RVA: 0x00113F00 File Offset: 0x00113F00
		public override ECFieldElement SquarePow(int pow)
		{
			if (pow >= 1)
			{
				return new F2mFieldElement(this.m, this.ks, this.x.ModSquareN(pow, this.m, this.ks));
			}
			return this;
		}

		// Token: 0x060034A6 RID: 13478 RVA: 0x00113F34 File Offset: 0x00113F34
		public override ECFieldElement Invert()
		{
			return new F2mFieldElement(this.m, this.ks, this.x.ModInverse(this.m, this.ks));
		}

		// Token: 0x060034A7 RID: 13479 RVA: 0x00113F60 File Offset: 0x00113F60
		public override ECFieldElement Sqrt()
		{
			if (!this.x.IsZero() && !this.x.IsOne())
			{
				return this.SquarePow(this.m - 1);
			}
			return this;
		}

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x060034A8 RID: 13480 RVA: 0x00113F94 File Offset: 0x00113F94
		public int Representation
		{
			get
			{
				return this.representation;
			}
		}

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x060034A9 RID: 13481 RVA: 0x00113F9C File Offset: 0x00113F9C
		public int M
		{
			get
			{
				return this.m;
			}
		}

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x060034AA RID: 13482 RVA: 0x00113FA4 File Offset: 0x00113FA4
		public int K1
		{
			get
			{
				return this.ks[0];
			}
		}

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x060034AB RID: 13483 RVA: 0x00113FB0 File Offset: 0x00113FB0
		public int K2
		{
			get
			{
				if (this.ks.Length < 2)
				{
					return 0;
				}
				return this.ks[1];
			}
		}

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x060034AC RID: 13484 RVA: 0x00113FCC File Offset: 0x00113FCC
		public int K3
		{
			get
			{
				if (this.ks.Length < 3)
				{
					return 0;
				}
				return this.ks[2];
			}
		}

		// Token: 0x060034AD RID: 13485 RVA: 0x00113FE8 File Offset: 0x00113FE8
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			F2mFieldElement f2mFieldElement = obj as F2mFieldElement;
			return f2mFieldElement != null && this.Equals(f2mFieldElement);
		}

		// Token: 0x060034AE RID: 13486 RVA: 0x00114018 File Offset: 0x00114018
		public virtual bool Equals(F2mFieldElement other)
		{
			return this.m == other.m && this.representation == other.representation && Arrays.AreEqual(this.ks, other.ks) && this.x.Equals(other.x);
		}

		// Token: 0x060034AF RID: 13487 RVA: 0x00114074 File Offset: 0x00114074
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ this.m ^ Arrays.GetHashCode(this.ks);
		}

		// Token: 0x04001D03 RID: 7427
		public const int Gnb = 1;

		// Token: 0x04001D04 RID: 7428
		public const int Tpb = 2;

		// Token: 0x04001D05 RID: 7429
		public const int Ppb = 3;

		// Token: 0x04001D06 RID: 7430
		private int representation;

		// Token: 0x04001D07 RID: 7431
		private int m;

		// Token: 0x04001D08 RID: 7432
		private int[] ks;

		// Token: 0x04001D09 RID: 7433
		internal LongArray x;
	}
}
