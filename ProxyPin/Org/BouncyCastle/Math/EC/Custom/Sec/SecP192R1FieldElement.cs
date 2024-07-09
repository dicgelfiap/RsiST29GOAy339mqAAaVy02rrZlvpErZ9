using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000594 RID: 1428
	internal class SecP192R1FieldElement : AbstractFpFieldElement
	{
		// Token: 0x06002D62 RID: 11618 RVA: 0x000EF380 File Offset: 0x000EF380
		public SecP192R1FieldElement(BigInteger x)
		{
			if (x == null || x.SignValue < 0 || x.CompareTo(SecP192R1FieldElement.Q) >= 0)
			{
				throw new ArgumentException("value invalid for SecP192R1FieldElement", "x");
			}
			this.x = SecP192R1Field.FromBigInteger(x);
		}

		// Token: 0x06002D63 RID: 11619 RVA: 0x000EF3D8 File Offset: 0x000EF3D8
		public SecP192R1FieldElement()
		{
			this.x = Nat192.Create();
		}

		// Token: 0x06002D64 RID: 11620 RVA: 0x000EF3EC File Offset: 0x000EF3EC
		protected internal SecP192R1FieldElement(uint[] x)
		{
			this.x = x;
		}

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x06002D65 RID: 11621 RVA: 0x000EF3FC File Offset: 0x000EF3FC
		public override bool IsZero
		{
			get
			{
				return Nat192.IsZero(this.x);
			}
		}

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x06002D66 RID: 11622 RVA: 0x000EF40C File Offset: 0x000EF40C
		public override bool IsOne
		{
			get
			{
				return Nat192.IsOne(this.x);
			}
		}

		// Token: 0x06002D67 RID: 11623 RVA: 0x000EF41C File Offset: 0x000EF41C
		public override bool TestBitZero()
		{
			return Nat192.GetBit(this.x, 0) == 1U;
		}

		// Token: 0x06002D68 RID: 11624 RVA: 0x000EF430 File Offset: 0x000EF430
		public override BigInteger ToBigInteger()
		{
			return Nat192.ToBigInteger(this.x);
		}

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x06002D69 RID: 11625 RVA: 0x000EF440 File Offset: 0x000EF440
		public override string FieldName
		{
			get
			{
				return "SecP192R1Field";
			}
		}

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x06002D6A RID: 11626 RVA: 0x000EF448 File Offset: 0x000EF448
		public override int FieldSize
		{
			get
			{
				return SecP192R1FieldElement.Q.BitLength;
			}
		}

		// Token: 0x06002D6B RID: 11627 RVA: 0x000EF454 File Offset: 0x000EF454
		public override ECFieldElement Add(ECFieldElement b)
		{
			uint[] z = Nat192.Create();
			SecP192R1Field.Add(this.x, ((SecP192R1FieldElement)b).x, z);
			return new SecP192R1FieldElement(z);
		}

		// Token: 0x06002D6C RID: 11628 RVA: 0x000EF488 File Offset: 0x000EF488
		public override ECFieldElement AddOne()
		{
			uint[] z = Nat192.Create();
			SecP192R1Field.AddOne(this.x, z);
			return new SecP192R1FieldElement(z);
		}

		// Token: 0x06002D6D RID: 11629 RVA: 0x000EF4B4 File Offset: 0x000EF4B4
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			uint[] z = Nat192.Create();
			SecP192R1Field.Subtract(this.x, ((SecP192R1FieldElement)b).x, z);
			return new SecP192R1FieldElement(z);
		}

		// Token: 0x06002D6E RID: 11630 RVA: 0x000EF4E8 File Offset: 0x000EF4E8
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			uint[] z = Nat192.Create();
			SecP192R1Field.Multiply(this.x, ((SecP192R1FieldElement)b).x, z);
			return new SecP192R1FieldElement(z);
		}

		// Token: 0x06002D6F RID: 11631 RVA: 0x000EF51C File Offset: 0x000EF51C
		public override ECFieldElement Divide(ECFieldElement b)
		{
			uint[] z = Nat192.Create();
			Mod.Invert(SecP192R1Field.P, ((SecP192R1FieldElement)b).x, z);
			SecP192R1Field.Multiply(z, this.x, z);
			return new SecP192R1FieldElement(z);
		}

		// Token: 0x06002D70 RID: 11632 RVA: 0x000EF55C File Offset: 0x000EF55C
		public override ECFieldElement Negate()
		{
			uint[] z = Nat192.Create();
			SecP192R1Field.Negate(this.x, z);
			return new SecP192R1FieldElement(z);
		}

		// Token: 0x06002D71 RID: 11633 RVA: 0x000EF588 File Offset: 0x000EF588
		public override ECFieldElement Square()
		{
			uint[] z = Nat192.Create();
			SecP192R1Field.Square(this.x, z);
			return new SecP192R1FieldElement(z);
		}

		// Token: 0x06002D72 RID: 11634 RVA: 0x000EF5B4 File Offset: 0x000EF5B4
		public override ECFieldElement Invert()
		{
			uint[] z = Nat192.Create();
			Mod.Invert(SecP192R1Field.P, this.x, z);
			return new SecP192R1FieldElement(z);
		}

		// Token: 0x06002D73 RID: 11635 RVA: 0x000EF5E4 File Offset: 0x000EF5E4
		public override ECFieldElement Sqrt()
		{
			uint[] y = this.x;
			if (Nat192.IsZero(y) || Nat192.IsOne(y))
			{
				return this;
			}
			uint[] array = Nat192.Create();
			uint[] array2 = Nat192.Create();
			SecP192R1Field.Square(y, array);
			SecP192R1Field.Multiply(array, y, array);
			SecP192R1Field.SquareN(array, 2, array2);
			SecP192R1Field.Multiply(array2, array, array2);
			SecP192R1Field.SquareN(array2, 4, array);
			SecP192R1Field.Multiply(array, array2, array);
			SecP192R1Field.SquareN(array, 8, array2);
			SecP192R1Field.Multiply(array2, array, array2);
			SecP192R1Field.SquareN(array2, 16, array);
			SecP192R1Field.Multiply(array, array2, array);
			SecP192R1Field.SquareN(array, 32, array2);
			SecP192R1Field.Multiply(array2, array, array2);
			SecP192R1Field.SquareN(array2, 64, array);
			SecP192R1Field.Multiply(array, array2, array);
			SecP192R1Field.SquareN(array, 62, array);
			SecP192R1Field.Square(array, array2);
			if (!Nat192.Eq(y, array2))
			{
				return null;
			}
			return new SecP192R1FieldElement(array);
		}

		// Token: 0x06002D74 RID: 11636 RVA: 0x000EF6B8 File Offset: 0x000EF6B8
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SecP192R1FieldElement);
		}

		// Token: 0x06002D75 RID: 11637 RVA: 0x000EF6C8 File Offset: 0x000EF6C8
		public override bool Equals(ECFieldElement other)
		{
			return this.Equals(other as SecP192R1FieldElement);
		}

		// Token: 0x06002D76 RID: 11638 RVA: 0x000EF6D8 File Offset: 0x000EF6D8
		public virtual bool Equals(SecP192R1FieldElement other)
		{
			return this == other || (other != null && Nat192.Eq(this.x, other.x));
		}

		// Token: 0x06002D77 RID: 11639 RVA: 0x000EF6FC File Offset: 0x000EF6FC
		public override int GetHashCode()
		{
			return SecP192R1FieldElement.Q.GetHashCode() ^ Arrays.GetHashCode(this.x, 0, 6);
		}

		// Token: 0x04001BC0 RID: 7104
		public static readonly BigInteger Q = new BigInteger(1, Hex.DecodeStrict("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFFFFFFFFFFFF"));

		// Token: 0x04001BC1 RID: 7105
		protected internal readonly uint[] x;
	}
}
