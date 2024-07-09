using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x0200058C RID: 1420
	internal class SecP160R2FieldElement : AbstractFpFieldElement
	{
		// Token: 0x06002CE8 RID: 11496 RVA: 0x000ED034 File Offset: 0x000ED034
		public SecP160R2FieldElement(BigInteger x)
		{
			if (x == null || x.SignValue < 0 || x.CompareTo(SecP160R2FieldElement.Q) >= 0)
			{
				throw new ArgumentException("value invalid for SecP160R2FieldElement", "x");
			}
			this.x = SecP160R2Field.FromBigInteger(x);
		}

		// Token: 0x06002CE9 RID: 11497 RVA: 0x000ED08C File Offset: 0x000ED08C
		public SecP160R2FieldElement()
		{
			this.x = Nat160.Create();
		}

		// Token: 0x06002CEA RID: 11498 RVA: 0x000ED0A0 File Offset: 0x000ED0A0
		protected internal SecP160R2FieldElement(uint[] x)
		{
			this.x = x;
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06002CEB RID: 11499 RVA: 0x000ED0B0 File Offset: 0x000ED0B0
		public override bool IsZero
		{
			get
			{
				return Nat160.IsZero(this.x);
			}
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06002CEC RID: 11500 RVA: 0x000ED0C0 File Offset: 0x000ED0C0
		public override bool IsOne
		{
			get
			{
				return Nat160.IsOne(this.x);
			}
		}

		// Token: 0x06002CED RID: 11501 RVA: 0x000ED0D0 File Offset: 0x000ED0D0
		public override bool TestBitZero()
		{
			return Nat160.GetBit(this.x, 0) == 1U;
		}

		// Token: 0x06002CEE RID: 11502 RVA: 0x000ED0E4 File Offset: 0x000ED0E4
		public override BigInteger ToBigInteger()
		{
			return Nat160.ToBigInteger(this.x);
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06002CEF RID: 11503 RVA: 0x000ED0F4 File Offset: 0x000ED0F4
		public override string FieldName
		{
			get
			{
				return "SecP160R2Field";
			}
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06002CF0 RID: 11504 RVA: 0x000ED0FC File Offset: 0x000ED0FC
		public override int FieldSize
		{
			get
			{
				return SecP160R2FieldElement.Q.BitLength;
			}
		}

		// Token: 0x06002CF1 RID: 11505 RVA: 0x000ED108 File Offset: 0x000ED108
		public override ECFieldElement Add(ECFieldElement b)
		{
			uint[] z = Nat160.Create();
			SecP160R2Field.Add(this.x, ((SecP160R2FieldElement)b).x, z);
			return new SecP160R2FieldElement(z);
		}

		// Token: 0x06002CF2 RID: 11506 RVA: 0x000ED13C File Offset: 0x000ED13C
		public override ECFieldElement AddOne()
		{
			uint[] z = Nat160.Create();
			SecP160R2Field.AddOne(this.x, z);
			return new SecP160R2FieldElement(z);
		}

		// Token: 0x06002CF3 RID: 11507 RVA: 0x000ED168 File Offset: 0x000ED168
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			uint[] z = Nat160.Create();
			SecP160R2Field.Subtract(this.x, ((SecP160R2FieldElement)b).x, z);
			return new SecP160R2FieldElement(z);
		}

		// Token: 0x06002CF4 RID: 11508 RVA: 0x000ED19C File Offset: 0x000ED19C
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			uint[] z = Nat160.Create();
			SecP160R2Field.Multiply(this.x, ((SecP160R2FieldElement)b).x, z);
			return new SecP160R2FieldElement(z);
		}

		// Token: 0x06002CF5 RID: 11509 RVA: 0x000ED1D0 File Offset: 0x000ED1D0
		public override ECFieldElement Divide(ECFieldElement b)
		{
			uint[] z = Nat160.Create();
			Mod.Invert(SecP160R2Field.P, ((SecP160R2FieldElement)b).x, z);
			SecP160R2Field.Multiply(z, this.x, z);
			return new SecP160R2FieldElement(z);
		}

		// Token: 0x06002CF6 RID: 11510 RVA: 0x000ED210 File Offset: 0x000ED210
		public override ECFieldElement Negate()
		{
			uint[] z = Nat160.Create();
			SecP160R2Field.Negate(this.x, z);
			return new SecP160R2FieldElement(z);
		}

		// Token: 0x06002CF7 RID: 11511 RVA: 0x000ED23C File Offset: 0x000ED23C
		public override ECFieldElement Square()
		{
			uint[] z = Nat160.Create();
			SecP160R2Field.Square(this.x, z);
			return new SecP160R2FieldElement(z);
		}

		// Token: 0x06002CF8 RID: 11512 RVA: 0x000ED268 File Offset: 0x000ED268
		public override ECFieldElement Invert()
		{
			uint[] z = Nat160.Create();
			Mod.Invert(SecP160R2Field.P, this.x, z);
			return new SecP160R2FieldElement(z);
		}

		// Token: 0x06002CF9 RID: 11513 RVA: 0x000ED298 File Offset: 0x000ED298
		public override ECFieldElement Sqrt()
		{
			uint[] y = this.x;
			if (Nat160.IsZero(y) || Nat160.IsOne(y))
			{
				return this;
			}
			uint[] array = Nat160.Create();
			SecP160R2Field.Square(y, array);
			SecP160R2Field.Multiply(array, y, array);
			uint[] array2 = Nat160.Create();
			SecP160R2Field.Square(array, array2);
			SecP160R2Field.Multiply(array2, y, array2);
			uint[] array3 = Nat160.Create();
			SecP160R2Field.Square(array2, array3);
			SecP160R2Field.Multiply(array3, y, array3);
			uint[] array4 = Nat160.Create();
			SecP160R2Field.SquareN(array3, 3, array4);
			SecP160R2Field.Multiply(array4, array2, array4);
			uint[] array5 = array3;
			SecP160R2Field.SquareN(array4, 7, array5);
			SecP160R2Field.Multiply(array5, array4, array5);
			uint[] array6 = array4;
			SecP160R2Field.SquareN(array5, 3, array6);
			SecP160R2Field.Multiply(array6, array2, array6);
			uint[] array7 = Nat160.Create();
			SecP160R2Field.SquareN(array6, 14, array7);
			SecP160R2Field.Multiply(array7, array5, array7);
			uint[] array8 = array5;
			SecP160R2Field.SquareN(array7, 31, array8);
			SecP160R2Field.Multiply(array8, array7, array8);
			uint[] z = array7;
			SecP160R2Field.SquareN(array8, 62, z);
			SecP160R2Field.Multiply(z, array8, z);
			uint[] array9 = array8;
			SecP160R2Field.SquareN(z, 3, array9);
			SecP160R2Field.Multiply(array9, array2, array9);
			uint[] z2 = array9;
			SecP160R2Field.SquareN(z2, 18, z2);
			SecP160R2Field.Multiply(z2, array6, z2);
			SecP160R2Field.SquareN(z2, 2, z2);
			SecP160R2Field.Multiply(z2, y, z2);
			SecP160R2Field.SquareN(z2, 3, z2);
			SecP160R2Field.Multiply(z2, array, z2);
			SecP160R2Field.SquareN(z2, 6, z2);
			SecP160R2Field.Multiply(z2, array2, z2);
			SecP160R2Field.SquareN(z2, 2, z2);
			SecP160R2Field.Multiply(z2, y, z2);
			uint[] array10 = array;
			SecP160R2Field.Square(z2, array10);
			if (!Nat160.Eq(y, array10))
			{
				return null;
			}
			return new SecP160R2FieldElement(z2);
		}

		// Token: 0x06002CFA RID: 11514 RVA: 0x000ED448 File Offset: 0x000ED448
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SecP160R2FieldElement);
		}

		// Token: 0x06002CFB RID: 11515 RVA: 0x000ED458 File Offset: 0x000ED458
		public override bool Equals(ECFieldElement other)
		{
			return this.Equals(other as SecP160R2FieldElement);
		}

		// Token: 0x06002CFC RID: 11516 RVA: 0x000ED468 File Offset: 0x000ED468
		public virtual bool Equals(SecP160R2FieldElement other)
		{
			return this == other || (other != null && Nat160.Eq(this.x, other.x));
		}

		// Token: 0x06002CFD RID: 11517 RVA: 0x000ED48C File Offset: 0x000ED48C
		public override int GetHashCode()
		{
			return SecP160R2FieldElement.Q.GetHashCode() ^ Arrays.GetHashCode(this.x, 0, 5);
		}

		// Token: 0x04001BA7 RID: 7079
		public static readonly BigInteger Q = new BigInteger(1, Hex.DecodeStrict("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFAC73"));

		// Token: 0x04001BA8 RID: 7080
		protected internal readonly uint[] x;
	}
}
