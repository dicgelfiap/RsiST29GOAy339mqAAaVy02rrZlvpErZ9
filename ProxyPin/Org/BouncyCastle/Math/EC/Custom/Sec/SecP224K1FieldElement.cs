using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000598 RID: 1432
	internal class SecP224K1FieldElement : AbstractFpFieldElement
	{
		// Token: 0x06002D9E RID: 11678 RVA: 0x000F036C File Offset: 0x000F036C
		public SecP224K1FieldElement(BigInteger x)
		{
			if (x == null || x.SignValue < 0 || x.CompareTo(SecP224K1FieldElement.Q) >= 0)
			{
				throw new ArgumentException("value invalid for SecP224K1FieldElement", "x");
			}
			this.x = SecP224K1Field.FromBigInteger(x);
		}

		// Token: 0x06002D9F RID: 11679 RVA: 0x000F03C4 File Offset: 0x000F03C4
		public SecP224K1FieldElement()
		{
			this.x = Nat224.Create();
		}

		// Token: 0x06002DA0 RID: 11680 RVA: 0x000F03D8 File Offset: 0x000F03D8
		protected internal SecP224K1FieldElement(uint[] x)
		{
			this.x = x;
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x06002DA1 RID: 11681 RVA: 0x000F03E8 File Offset: 0x000F03E8
		public override bool IsZero
		{
			get
			{
				return Nat224.IsZero(this.x);
			}
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x06002DA2 RID: 11682 RVA: 0x000F03F8 File Offset: 0x000F03F8
		public override bool IsOne
		{
			get
			{
				return Nat224.IsOne(this.x);
			}
		}

		// Token: 0x06002DA3 RID: 11683 RVA: 0x000F0408 File Offset: 0x000F0408
		public override bool TestBitZero()
		{
			return Nat224.GetBit(this.x, 0) == 1U;
		}

		// Token: 0x06002DA4 RID: 11684 RVA: 0x000F041C File Offset: 0x000F041C
		public override BigInteger ToBigInteger()
		{
			return Nat224.ToBigInteger(this.x);
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x06002DA5 RID: 11685 RVA: 0x000F042C File Offset: 0x000F042C
		public override string FieldName
		{
			get
			{
				return "SecP224K1Field";
			}
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x06002DA6 RID: 11686 RVA: 0x000F0434 File Offset: 0x000F0434
		public override int FieldSize
		{
			get
			{
				return SecP224K1FieldElement.Q.BitLength;
			}
		}

		// Token: 0x06002DA7 RID: 11687 RVA: 0x000F0440 File Offset: 0x000F0440
		public override ECFieldElement Add(ECFieldElement b)
		{
			uint[] z = Nat224.Create();
			SecP224K1Field.Add(this.x, ((SecP224K1FieldElement)b).x, z);
			return new SecP224K1FieldElement(z);
		}

		// Token: 0x06002DA8 RID: 11688 RVA: 0x000F0474 File Offset: 0x000F0474
		public override ECFieldElement AddOne()
		{
			uint[] z = Nat224.Create();
			SecP224K1Field.AddOne(this.x, z);
			return new SecP224K1FieldElement(z);
		}

		// Token: 0x06002DA9 RID: 11689 RVA: 0x000F04A0 File Offset: 0x000F04A0
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			uint[] z = Nat224.Create();
			SecP224K1Field.Subtract(this.x, ((SecP224K1FieldElement)b).x, z);
			return new SecP224K1FieldElement(z);
		}

		// Token: 0x06002DAA RID: 11690 RVA: 0x000F04D4 File Offset: 0x000F04D4
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			uint[] z = Nat224.Create();
			SecP224K1Field.Multiply(this.x, ((SecP224K1FieldElement)b).x, z);
			return new SecP224K1FieldElement(z);
		}

		// Token: 0x06002DAB RID: 11691 RVA: 0x000F0508 File Offset: 0x000F0508
		public override ECFieldElement Divide(ECFieldElement b)
		{
			uint[] z = Nat224.Create();
			Mod.Invert(SecP224K1Field.P, ((SecP224K1FieldElement)b).x, z);
			SecP224K1Field.Multiply(z, this.x, z);
			return new SecP224K1FieldElement(z);
		}

		// Token: 0x06002DAC RID: 11692 RVA: 0x000F0548 File Offset: 0x000F0548
		public override ECFieldElement Negate()
		{
			uint[] z = Nat224.Create();
			SecP224K1Field.Negate(this.x, z);
			return new SecP224K1FieldElement(z);
		}

		// Token: 0x06002DAD RID: 11693 RVA: 0x000F0574 File Offset: 0x000F0574
		public override ECFieldElement Square()
		{
			uint[] z = Nat224.Create();
			SecP224K1Field.Square(this.x, z);
			return new SecP224K1FieldElement(z);
		}

		// Token: 0x06002DAE RID: 11694 RVA: 0x000F05A0 File Offset: 0x000F05A0
		public override ECFieldElement Invert()
		{
			uint[] z = Nat224.Create();
			Mod.Invert(SecP224K1Field.P, this.x, z);
			return new SecP224K1FieldElement(z);
		}

		// Token: 0x06002DAF RID: 11695 RVA: 0x000F05D0 File Offset: 0x000F05D0
		public override ECFieldElement Sqrt()
		{
			uint[] y = this.x;
			if (Nat224.IsZero(y) || Nat224.IsOne(y))
			{
				return this;
			}
			uint[] array = Nat224.Create();
			SecP224K1Field.Square(y, array);
			SecP224K1Field.Multiply(array, y, array);
			uint[] array2 = array;
			SecP224K1Field.Square(array, array2);
			SecP224K1Field.Multiply(array2, y, array2);
			uint[] array3 = Nat224.Create();
			SecP224K1Field.Square(array2, array3);
			SecP224K1Field.Multiply(array3, y, array3);
			uint[] array4 = Nat224.Create();
			SecP224K1Field.SquareN(array3, 4, array4);
			SecP224K1Field.Multiply(array4, array3, array4);
			uint[] array5 = Nat224.Create();
			SecP224K1Field.SquareN(array4, 3, array5);
			SecP224K1Field.Multiply(array5, array2, array5);
			uint[] array6 = array5;
			SecP224K1Field.SquareN(array5, 8, array6);
			SecP224K1Field.Multiply(array6, array4, array6);
			uint[] array7 = array4;
			SecP224K1Field.SquareN(array6, 4, array7);
			SecP224K1Field.Multiply(array7, array3, array7);
			uint[] array8 = array3;
			SecP224K1Field.SquareN(array7, 19, array8);
			SecP224K1Field.Multiply(array8, array6, array8);
			uint[] array9 = Nat224.Create();
			SecP224K1Field.SquareN(array8, 42, array9);
			SecP224K1Field.Multiply(array9, array8, array9);
			uint[] z = array8;
			SecP224K1Field.SquareN(array9, 23, z);
			SecP224K1Field.Multiply(z, array7, z);
			uint[] array10 = array7;
			SecP224K1Field.SquareN(z, 84, array10);
			SecP224K1Field.Multiply(array10, array9, array10);
			uint[] z2 = array10;
			SecP224K1Field.SquareN(z2, 20, z2);
			SecP224K1Field.Multiply(z2, array6, z2);
			SecP224K1Field.SquareN(z2, 3, z2);
			SecP224K1Field.Multiply(z2, y, z2);
			SecP224K1Field.SquareN(z2, 2, z2);
			SecP224K1Field.Multiply(z2, y, z2);
			SecP224K1Field.SquareN(z2, 4, z2);
			SecP224K1Field.Multiply(z2, array2, z2);
			SecP224K1Field.Square(z2, z2);
			uint[] array11 = array9;
			SecP224K1Field.Square(z2, array11);
			if (Nat224.Eq(y, array11))
			{
				return new SecP224K1FieldElement(z2);
			}
			SecP224K1Field.Multiply(z2, SecP224K1FieldElement.PRECOMP_POW2, z2);
			SecP224K1Field.Square(z2, array11);
			if (Nat224.Eq(y, array11))
			{
				return new SecP224K1FieldElement(z2);
			}
			return null;
		}

		// Token: 0x06002DB0 RID: 11696 RVA: 0x000F07BC File Offset: 0x000F07BC
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SecP224K1FieldElement);
		}

		// Token: 0x06002DB1 RID: 11697 RVA: 0x000F07CC File Offset: 0x000F07CC
		public override bool Equals(ECFieldElement other)
		{
			return this.Equals(other as SecP224K1FieldElement);
		}

		// Token: 0x06002DB2 RID: 11698 RVA: 0x000F07DC File Offset: 0x000F07DC
		public virtual bool Equals(SecP224K1FieldElement other)
		{
			return this == other || (other != null && Nat224.Eq(this.x, other.x));
		}

		// Token: 0x06002DB3 RID: 11699 RVA: 0x000F0800 File Offset: 0x000F0800
		public override int GetHashCode()
		{
			return SecP224K1FieldElement.Q.GetHashCode() ^ Arrays.GetHashCode(this.x, 0, 7);
		}

		// Token: 0x04001BCD RID: 7117
		public static readonly BigInteger Q = new BigInteger(1, Hex.DecodeStrict("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFE56D"));

		// Token: 0x04001BCE RID: 7118
		private static readonly uint[] PRECOMP_POW2 = new uint[]
		{
			868209154U,
			3707425075U,
			579297866U,
			3280018344U,
			2824165628U,
			514782679U,
			2396984652U
		};

		// Token: 0x04001BCF RID: 7119
		protected internal readonly uint[] x;
	}
}
