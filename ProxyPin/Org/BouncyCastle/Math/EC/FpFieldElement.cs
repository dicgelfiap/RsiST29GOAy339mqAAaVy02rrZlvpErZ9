using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Math.EC
{
	// Token: 0x0200060E RID: 1550
	public class FpFieldElement : AbstractFpFieldElement
	{
		// Token: 0x0600346E RID: 13422 RVA: 0x00112E60 File Offset: 0x00112E60
		internal static BigInteger CalculateResidue(BigInteger p)
		{
			int bitLength = p.BitLength;
			if (bitLength >= 96)
			{
				BigInteger bigInteger = p.ShiftRight(bitLength - 64);
				if (bigInteger.LongValue == -1L)
				{
					return BigInteger.One.ShiftLeft(bitLength).Subtract(p);
				}
				if ((bitLength & 7) == 0)
				{
					return BigInteger.One.ShiftLeft(bitLength << 1).Divide(p).Negate();
				}
			}
			return null;
		}

		// Token: 0x0600346F RID: 13423 RVA: 0x00112ECC File Offset: 0x00112ECC
		[Obsolete("Use ECCurve.FromBigInteger to construct field elements")]
		public FpFieldElement(BigInteger q, BigInteger x) : this(q, FpFieldElement.CalculateResidue(q), x)
		{
		}

		// Token: 0x06003470 RID: 13424 RVA: 0x00112EDC File Offset: 0x00112EDC
		internal FpFieldElement(BigInteger q, BigInteger r, BigInteger x)
		{
			if (x == null || x.SignValue < 0 || x.CompareTo(q) >= 0)
			{
				throw new ArgumentException("value invalid in Fp field element", "x");
			}
			this.q = q;
			this.r = r;
			this.x = x;
		}

		// Token: 0x06003471 RID: 13425 RVA: 0x00112F38 File Offset: 0x00112F38
		public override BigInteger ToBigInteger()
		{
			return this.x;
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x06003472 RID: 13426 RVA: 0x00112F40 File Offset: 0x00112F40
		public override string FieldName
		{
			get
			{
				return "Fp";
			}
		}

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x06003473 RID: 13427 RVA: 0x00112F48 File Offset: 0x00112F48
		public override int FieldSize
		{
			get
			{
				return this.q.BitLength;
			}
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x06003474 RID: 13428 RVA: 0x00112F58 File Offset: 0x00112F58
		public BigInteger Q
		{
			get
			{
				return this.q;
			}
		}

		// Token: 0x06003475 RID: 13429 RVA: 0x00112F60 File Offset: 0x00112F60
		public override ECFieldElement Add(ECFieldElement b)
		{
			return new FpFieldElement(this.q, this.r, this.ModAdd(this.x, b.ToBigInteger()));
		}

		// Token: 0x06003476 RID: 13430 RVA: 0x00112F88 File Offset: 0x00112F88
		public override ECFieldElement AddOne()
		{
			BigInteger bigInteger = this.x.Add(BigInteger.One);
			if (bigInteger.CompareTo(this.q) == 0)
			{
				bigInteger = BigInteger.Zero;
			}
			return new FpFieldElement(this.q, this.r, bigInteger);
		}

		// Token: 0x06003477 RID: 13431 RVA: 0x00112FD4 File Offset: 0x00112FD4
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			return new FpFieldElement(this.q, this.r, this.ModSubtract(this.x, b.ToBigInteger()));
		}

		// Token: 0x06003478 RID: 13432 RVA: 0x00112FFC File Offset: 0x00112FFC
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			return new FpFieldElement(this.q, this.r, this.ModMult(this.x, b.ToBigInteger()));
		}

		// Token: 0x06003479 RID: 13433 RVA: 0x00113024 File Offset: 0x00113024
		public override ECFieldElement MultiplyMinusProduct(ECFieldElement b, ECFieldElement x, ECFieldElement y)
		{
			BigInteger bigInteger = this.x;
			BigInteger val = b.ToBigInteger();
			BigInteger bigInteger2 = x.ToBigInteger();
			BigInteger val2 = y.ToBigInteger();
			BigInteger bigInteger3 = bigInteger.Multiply(val);
			BigInteger n = bigInteger2.Multiply(val2);
			return new FpFieldElement(this.q, this.r, this.ModReduce(bigInteger3.Subtract(n)));
		}

		// Token: 0x0600347A RID: 13434 RVA: 0x00113084 File Offset: 0x00113084
		public override ECFieldElement MultiplyPlusProduct(ECFieldElement b, ECFieldElement x, ECFieldElement y)
		{
			BigInteger bigInteger = this.x;
			BigInteger val = b.ToBigInteger();
			BigInteger bigInteger2 = x.ToBigInteger();
			BigInteger val2 = y.ToBigInteger();
			BigInteger bigInteger3 = bigInteger.Multiply(val);
			BigInteger value = bigInteger2.Multiply(val2);
			BigInteger bigInteger4 = bigInteger3.Add(value);
			if (this.r != null && this.r.SignValue < 0 && bigInteger4.BitLength > this.q.BitLength << 1)
			{
				bigInteger4 = bigInteger4.Subtract(this.q.ShiftLeft(this.q.BitLength));
			}
			return new FpFieldElement(this.q, this.r, this.ModReduce(bigInteger4));
		}

		// Token: 0x0600347B RID: 13435 RVA: 0x0011313C File Offset: 0x0011313C
		public override ECFieldElement Divide(ECFieldElement b)
		{
			return new FpFieldElement(this.q, this.r, this.ModMult(this.x, this.ModInverse(b.ToBigInteger())));
		}

		// Token: 0x0600347C RID: 13436 RVA: 0x00113178 File Offset: 0x00113178
		public override ECFieldElement Negate()
		{
			if (this.x.SignValue != 0)
			{
				return new FpFieldElement(this.q, this.r, this.q.Subtract(this.x));
			}
			return this;
		}

		// Token: 0x0600347D RID: 13437 RVA: 0x001131B0 File Offset: 0x001131B0
		public override ECFieldElement Square()
		{
			return new FpFieldElement(this.q, this.r, this.ModMult(this.x, this.x));
		}

		// Token: 0x0600347E RID: 13438 RVA: 0x001131D8 File Offset: 0x001131D8
		public override ECFieldElement SquareMinusProduct(ECFieldElement x, ECFieldElement y)
		{
			BigInteger bigInteger = this.x;
			BigInteger bigInteger2 = x.ToBigInteger();
			BigInteger val = y.ToBigInteger();
			BigInteger bigInteger3 = bigInteger.Multiply(bigInteger);
			BigInteger n = bigInteger2.Multiply(val);
			return new FpFieldElement(this.q, this.r, this.ModReduce(bigInteger3.Subtract(n)));
		}

		// Token: 0x0600347F RID: 13439 RVA: 0x00113230 File Offset: 0x00113230
		public override ECFieldElement SquarePlusProduct(ECFieldElement x, ECFieldElement y)
		{
			BigInteger bigInteger = this.x;
			BigInteger bigInteger2 = x.ToBigInteger();
			BigInteger val = y.ToBigInteger();
			BigInteger bigInteger3 = bigInteger.Multiply(bigInteger);
			BigInteger value = bigInteger2.Multiply(val);
			BigInteger bigInteger4 = bigInteger3.Add(value);
			if (this.r != null && this.r.SignValue < 0 && bigInteger4.BitLength > this.q.BitLength << 1)
			{
				bigInteger4 = bigInteger4.Subtract(this.q.ShiftLeft(this.q.BitLength));
			}
			return new FpFieldElement(this.q, this.r, this.ModReduce(bigInteger4));
		}

		// Token: 0x06003480 RID: 13440 RVA: 0x001132E0 File Offset: 0x001132E0
		public override ECFieldElement Invert()
		{
			return new FpFieldElement(this.q, this.r, this.ModInverse(this.x));
		}

		// Token: 0x06003481 RID: 13441 RVA: 0x00113300 File Offset: 0x00113300
		public override ECFieldElement Sqrt()
		{
			if (this.IsZero || this.IsOne)
			{
				return this;
			}
			if (!this.q.TestBit(0))
			{
				throw Platform.CreateNotImplementedException("even value of q");
			}
			if (this.q.TestBit(1))
			{
				BigInteger e = this.q.ShiftRight(2).Add(BigInteger.One);
				return this.CheckSqrt(new FpFieldElement(this.q, this.r, this.x.ModPow(e, this.q)));
			}
			if (this.q.TestBit(2))
			{
				BigInteger bigInteger = this.x.ModPow(this.q.ShiftRight(3), this.q);
				BigInteger x = this.ModMult(bigInteger, this.x);
				BigInteger bigInteger2 = this.ModMult(x, bigInteger);
				if (bigInteger2.Equals(BigInteger.One))
				{
					return this.CheckSqrt(new FpFieldElement(this.q, this.r, x));
				}
				BigInteger x2 = BigInteger.Two.ModPow(this.q.ShiftRight(2), this.q);
				BigInteger bigInteger3 = this.ModMult(x, x2);
				return this.CheckSqrt(new FpFieldElement(this.q, this.r, bigInteger3));
			}
			else
			{
				BigInteger bigInteger4 = this.q.ShiftRight(1);
				if (!this.x.ModPow(bigInteger4, this.q).Equals(BigInteger.One))
				{
					return null;
				}
				BigInteger bigInteger5 = this.x;
				BigInteger bigInteger6 = this.ModDouble(this.ModDouble(bigInteger5));
				BigInteger k = bigInteger4.Add(BigInteger.One);
				BigInteger obj = this.q.Subtract(BigInteger.One);
				BigInteger bigInteger9;
				for (;;)
				{
					BigInteger bigInteger7 = BigInteger.Arbitrary(this.q.BitLength);
					if (bigInteger7.CompareTo(this.q) < 0 && this.ModReduce(bigInteger7.Multiply(bigInteger7).Subtract(bigInteger6)).ModPow(bigInteger4, this.q).Equals(obj))
					{
						BigInteger[] array = this.LucasSequence(bigInteger7, bigInteger5, k);
						BigInteger bigInteger8 = array[0];
						bigInteger9 = array[1];
						if (this.ModMult(bigInteger9, bigInteger9).Equals(bigInteger6))
						{
							break;
						}
						if (!bigInteger8.Equals(BigInteger.One) && !bigInteger8.Equals(obj))
						{
							goto Block_11;
						}
					}
				}
				return new FpFieldElement(this.q, this.r, this.ModHalfAbs(bigInteger9));
				Block_11:
				return null;
			}
		}

		// Token: 0x06003482 RID: 13442 RVA: 0x00113574 File Offset: 0x00113574
		private ECFieldElement CheckSqrt(ECFieldElement z)
		{
			if (!z.Square().Equals(this))
			{
				return null;
			}
			return z;
		}

		// Token: 0x06003483 RID: 13443 RVA: 0x0011358C File Offset: 0x0011358C
		private BigInteger[] LucasSequence(BigInteger P, BigInteger Q, BigInteger k)
		{
			int bitLength = k.BitLength;
			int lowestSetBit = k.GetLowestSetBit();
			BigInteger bigInteger = BigInteger.One;
			BigInteger bigInteger2 = BigInteger.Two;
			BigInteger bigInteger3 = P;
			BigInteger bigInteger4 = BigInteger.One;
			BigInteger bigInteger5 = BigInteger.One;
			for (int i = bitLength - 1; i >= lowestSetBit + 1; i--)
			{
				bigInteger4 = this.ModMult(bigInteger4, bigInteger5);
				if (k.TestBit(i))
				{
					bigInteger5 = this.ModMult(bigInteger4, Q);
					bigInteger = this.ModMult(bigInteger, bigInteger3);
					bigInteger2 = this.ModReduce(bigInteger3.Multiply(bigInteger2).Subtract(P.Multiply(bigInteger4)));
					bigInteger3 = this.ModReduce(bigInteger3.Multiply(bigInteger3).Subtract(bigInteger5.ShiftLeft(1)));
				}
				else
				{
					bigInteger5 = bigInteger4;
					bigInteger = this.ModReduce(bigInteger.Multiply(bigInteger2).Subtract(bigInteger4));
					bigInteger3 = this.ModReduce(bigInteger3.Multiply(bigInteger2).Subtract(P.Multiply(bigInteger4)));
					bigInteger2 = this.ModReduce(bigInteger2.Multiply(bigInteger2).Subtract(bigInteger4.ShiftLeft(1)));
				}
			}
			bigInteger4 = this.ModMult(bigInteger4, bigInteger5);
			bigInteger5 = this.ModMult(bigInteger4, Q);
			bigInteger = this.ModReduce(bigInteger.Multiply(bigInteger2).Subtract(bigInteger4));
			bigInteger2 = this.ModReduce(bigInteger3.Multiply(bigInteger2).Subtract(P.Multiply(bigInteger4)));
			bigInteger4 = this.ModMult(bigInteger4, bigInteger5);
			for (int j = 1; j <= lowestSetBit; j++)
			{
				bigInteger = this.ModMult(bigInteger, bigInteger2);
				bigInteger2 = this.ModReduce(bigInteger2.Multiply(bigInteger2).Subtract(bigInteger4.ShiftLeft(1)));
				bigInteger4 = this.ModMult(bigInteger4, bigInteger4);
			}
			return new BigInteger[]
			{
				bigInteger,
				bigInteger2
			};
		}

		// Token: 0x06003484 RID: 13444 RVA: 0x00113754 File Offset: 0x00113754
		protected virtual BigInteger ModAdd(BigInteger x1, BigInteger x2)
		{
			BigInteger bigInteger = x1.Add(x2);
			if (bigInteger.CompareTo(this.q) >= 0)
			{
				bigInteger = bigInteger.Subtract(this.q);
			}
			return bigInteger;
		}

		// Token: 0x06003485 RID: 13445 RVA: 0x00113790 File Offset: 0x00113790
		protected virtual BigInteger ModDouble(BigInteger x)
		{
			BigInteger bigInteger = x.ShiftLeft(1);
			if (bigInteger.CompareTo(this.q) >= 0)
			{
				bigInteger = bigInteger.Subtract(this.q);
			}
			return bigInteger;
		}

		// Token: 0x06003486 RID: 13446 RVA: 0x001137CC File Offset: 0x001137CC
		protected virtual BigInteger ModHalf(BigInteger x)
		{
			if (x.TestBit(0))
			{
				x = this.q.Add(x);
			}
			return x.ShiftRight(1);
		}

		// Token: 0x06003487 RID: 13447 RVA: 0x001137F0 File Offset: 0x001137F0
		protected virtual BigInteger ModHalfAbs(BigInteger x)
		{
			if (x.TestBit(0))
			{
				x = this.q.Subtract(x);
			}
			return x.ShiftRight(1);
		}

		// Token: 0x06003488 RID: 13448 RVA: 0x00113814 File Offset: 0x00113814
		protected virtual BigInteger ModInverse(BigInteger x)
		{
			int fieldSize = this.FieldSize;
			int len = fieldSize + 31 >> 5;
			uint[] p = Nat.FromBigInteger(fieldSize, this.q);
			uint[] array = Nat.FromBigInteger(fieldSize, x);
			uint[] z = Nat.Create(len);
			Mod.Invert(p, array, z);
			return Nat.ToBigInteger(len, z);
		}

		// Token: 0x06003489 RID: 13449 RVA: 0x00113864 File Offset: 0x00113864
		protected virtual BigInteger ModMult(BigInteger x1, BigInteger x2)
		{
			return this.ModReduce(x1.Multiply(x2));
		}

		// Token: 0x0600348A RID: 13450 RVA: 0x00113874 File Offset: 0x00113874
		protected virtual BigInteger ModReduce(BigInteger x)
		{
			if (this.r == null)
			{
				x = x.Mod(this.q);
			}
			else
			{
				bool flag = x.SignValue < 0;
				if (flag)
				{
					x = x.Abs();
				}
				int bitLength = this.q.BitLength;
				if (this.r.SignValue > 0)
				{
					BigInteger n = BigInteger.One.ShiftLeft(bitLength);
					bool flag2 = this.r.Equals(BigInteger.One);
					while (x.BitLength > bitLength + 1)
					{
						BigInteger bigInteger = x.ShiftRight(bitLength);
						BigInteger value = x.Remainder(n);
						if (!flag2)
						{
							bigInteger = bigInteger.Multiply(this.r);
						}
						x = bigInteger.Add(value);
					}
				}
				else
				{
					int num = (bitLength - 1 & 31) + 1;
					BigInteger bigInteger2 = this.r.Negate();
					BigInteger bigInteger3 = bigInteger2.Multiply(x.ShiftRight(bitLength - num));
					BigInteger bigInteger4 = bigInteger3.ShiftRight(bitLength + num);
					BigInteger bigInteger5 = bigInteger4.Multiply(this.q);
					BigInteger bigInteger6 = BigInteger.One.ShiftLeft(bitLength + num);
					bigInteger5 = bigInteger5.Remainder(bigInteger6);
					x = x.Remainder(bigInteger6);
					x = x.Subtract(bigInteger5);
					if (x.SignValue < 0)
					{
						x = x.Add(bigInteger6);
					}
				}
				while (x.CompareTo(this.q) >= 0)
				{
					x = x.Subtract(this.q);
				}
				if (flag && x.SignValue != 0)
				{
					x = this.q.Subtract(x);
				}
			}
			return x;
		}

		// Token: 0x0600348B RID: 13451 RVA: 0x00113A0C File Offset: 0x00113A0C
		protected virtual BigInteger ModSubtract(BigInteger x1, BigInteger x2)
		{
			BigInteger bigInteger = x1.Subtract(x2);
			if (bigInteger.SignValue < 0)
			{
				bigInteger = bigInteger.Add(this.q);
			}
			return bigInteger;
		}

		// Token: 0x0600348C RID: 13452 RVA: 0x00113A40 File Offset: 0x00113A40
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			FpFieldElement fpFieldElement = obj as FpFieldElement;
			return fpFieldElement != null && this.Equals(fpFieldElement);
		}

		// Token: 0x0600348D RID: 13453 RVA: 0x00113A70 File Offset: 0x00113A70
		public virtual bool Equals(FpFieldElement other)
		{
			return this.q.Equals(other.q) && base.Equals(other);
		}

		// Token: 0x0600348E RID: 13454 RVA: 0x00113A94 File Offset: 0x00113A94
		public override int GetHashCode()
		{
			return this.q.GetHashCode() ^ base.GetHashCode();
		}

		// Token: 0x04001D00 RID: 7424
		private readonly BigInteger q;

		// Token: 0x04001D01 RID: 7425
		private readonly BigInteger r;

		// Token: 0x04001D02 RID: 7426
		private readonly BigInteger x;
	}
}
