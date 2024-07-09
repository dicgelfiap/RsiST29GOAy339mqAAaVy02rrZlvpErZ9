using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000588 RID: 1416
	internal class SecP160R1FieldElement : AbstractFpFieldElement
	{
		// Token: 0x06002CAC RID: 11436 RVA: 0x000EBFF0 File Offset: 0x000EBFF0
		public SecP160R1FieldElement(BigInteger x)
		{
			if (x == null || x.SignValue < 0 || x.CompareTo(SecP160R1FieldElement.Q) >= 0)
			{
				throw new ArgumentException("value invalid for SecP160R1FieldElement", "x");
			}
			this.x = SecP160R1Field.FromBigInteger(x);
		}

		// Token: 0x06002CAD RID: 11437 RVA: 0x000EC048 File Offset: 0x000EC048
		public SecP160R1FieldElement()
		{
			this.x = Nat160.Create();
		}

		// Token: 0x06002CAE RID: 11438 RVA: 0x000EC05C File Offset: 0x000EC05C
		protected internal SecP160R1FieldElement(uint[] x)
		{
			this.x = x;
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06002CAF RID: 11439 RVA: 0x000EC06C File Offset: 0x000EC06C
		public override bool IsZero
		{
			get
			{
				return Nat160.IsZero(this.x);
			}
		}

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x06002CB0 RID: 11440 RVA: 0x000EC07C File Offset: 0x000EC07C
		public override bool IsOne
		{
			get
			{
				return Nat160.IsOne(this.x);
			}
		}

		// Token: 0x06002CB1 RID: 11441 RVA: 0x000EC08C File Offset: 0x000EC08C
		public override bool TestBitZero()
		{
			return Nat160.GetBit(this.x, 0) == 1U;
		}

		// Token: 0x06002CB2 RID: 11442 RVA: 0x000EC0A0 File Offset: 0x000EC0A0
		public override BigInteger ToBigInteger()
		{
			return Nat160.ToBigInteger(this.x);
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x06002CB3 RID: 11443 RVA: 0x000EC0B0 File Offset: 0x000EC0B0
		public override string FieldName
		{
			get
			{
				return "SecP160R1Field";
			}
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x06002CB4 RID: 11444 RVA: 0x000EC0B8 File Offset: 0x000EC0B8
		public override int FieldSize
		{
			get
			{
				return SecP160R1FieldElement.Q.BitLength;
			}
		}

		// Token: 0x06002CB5 RID: 11445 RVA: 0x000EC0C4 File Offset: 0x000EC0C4
		public override ECFieldElement Add(ECFieldElement b)
		{
			uint[] z = Nat160.Create();
			SecP160R1Field.Add(this.x, ((SecP160R1FieldElement)b).x, z);
			return new SecP160R1FieldElement(z);
		}

		// Token: 0x06002CB6 RID: 11446 RVA: 0x000EC0F8 File Offset: 0x000EC0F8
		public override ECFieldElement AddOne()
		{
			uint[] z = Nat160.Create();
			SecP160R1Field.AddOne(this.x, z);
			return new SecP160R1FieldElement(z);
		}

		// Token: 0x06002CB7 RID: 11447 RVA: 0x000EC124 File Offset: 0x000EC124
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			uint[] z = Nat160.Create();
			SecP160R1Field.Subtract(this.x, ((SecP160R1FieldElement)b).x, z);
			return new SecP160R1FieldElement(z);
		}

		// Token: 0x06002CB8 RID: 11448 RVA: 0x000EC158 File Offset: 0x000EC158
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			uint[] z = Nat160.Create();
			SecP160R1Field.Multiply(this.x, ((SecP160R1FieldElement)b).x, z);
			return new SecP160R1FieldElement(z);
		}

		// Token: 0x06002CB9 RID: 11449 RVA: 0x000EC18C File Offset: 0x000EC18C
		public override ECFieldElement Divide(ECFieldElement b)
		{
			uint[] z = Nat160.Create();
			Mod.Invert(SecP160R1Field.P, ((SecP160R1FieldElement)b).x, z);
			SecP160R1Field.Multiply(z, this.x, z);
			return new SecP160R1FieldElement(z);
		}

		// Token: 0x06002CBA RID: 11450 RVA: 0x000EC1CC File Offset: 0x000EC1CC
		public override ECFieldElement Negate()
		{
			uint[] z = Nat160.Create();
			SecP160R1Field.Negate(this.x, z);
			return new SecP160R1FieldElement(z);
		}

		// Token: 0x06002CBB RID: 11451 RVA: 0x000EC1F8 File Offset: 0x000EC1F8
		public override ECFieldElement Square()
		{
			uint[] z = Nat160.Create();
			SecP160R1Field.Square(this.x, z);
			return new SecP160R1FieldElement(z);
		}

		// Token: 0x06002CBC RID: 11452 RVA: 0x000EC224 File Offset: 0x000EC224
		public override ECFieldElement Invert()
		{
			uint[] z = Nat160.Create();
			Mod.Invert(SecP160R1Field.P, this.x, z);
			return new SecP160R1FieldElement(z);
		}

		// Token: 0x06002CBD RID: 11453 RVA: 0x000EC254 File Offset: 0x000EC254
		public override ECFieldElement Sqrt()
		{
			uint[] y = this.x;
			if (Nat160.IsZero(y) || Nat160.IsOne(y))
			{
				return this;
			}
			uint[] array = Nat160.Create();
			SecP160R1Field.Square(y, array);
			SecP160R1Field.Multiply(array, y, array);
			uint[] array2 = Nat160.Create();
			SecP160R1Field.SquareN(array, 2, array2);
			SecP160R1Field.Multiply(array2, array, array2);
			uint[] array3 = array;
			SecP160R1Field.SquareN(array2, 4, array3);
			SecP160R1Field.Multiply(array3, array2, array3);
			uint[] array4 = array2;
			SecP160R1Field.SquareN(array3, 8, array4);
			SecP160R1Field.Multiply(array4, array3, array4);
			uint[] array5 = array3;
			SecP160R1Field.SquareN(array4, 16, array5);
			SecP160R1Field.Multiply(array5, array4, array5);
			uint[] array6 = array4;
			SecP160R1Field.SquareN(array5, 32, array6);
			SecP160R1Field.Multiply(array6, array5, array6);
			uint[] array7 = array5;
			SecP160R1Field.SquareN(array6, 64, array7);
			SecP160R1Field.Multiply(array7, array6, array7);
			uint[] array8 = array6;
			SecP160R1Field.Square(array7, array8);
			SecP160R1Field.Multiply(array8, y, array8);
			uint[] z = array8;
			SecP160R1Field.SquareN(z, 29, z);
			uint[] array9 = array7;
			SecP160R1Field.Square(z, array9);
			if (!Nat160.Eq(y, array9))
			{
				return null;
			}
			return new SecP160R1FieldElement(z);
		}

		// Token: 0x06002CBE RID: 11454 RVA: 0x000EC370 File Offset: 0x000EC370
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SecP160R1FieldElement);
		}

		// Token: 0x06002CBF RID: 11455 RVA: 0x000EC380 File Offset: 0x000EC380
		public override bool Equals(ECFieldElement other)
		{
			return this.Equals(other as SecP160R1FieldElement);
		}

		// Token: 0x06002CC0 RID: 11456 RVA: 0x000EC390 File Offset: 0x000EC390
		public virtual bool Equals(SecP160R1FieldElement other)
		{
			return this == other || (other != null && Nat160.Eq(this.x, other.x));
		}

		// Token: 0x06002CC1 RID: 11457 RVA: 0x000EC3B4 File Offset: 0x000EC3B4
		public override int GetHashCode()
		{
			return SecP160R1FieldElement.Q.GetHashCode() ^ Arrays.GetHashCode(this.x, 0, 5);
		}

		// Token: 0x04001B9A RID: 7066
		public static readonly BigInteger Q = new BigInteger(1, Hex.DecodeStrict("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF7FFFFFFF"));

		// Token: 0x04001B9B RID: 7067
		protected internal readonly uint[] x;
	}
}
