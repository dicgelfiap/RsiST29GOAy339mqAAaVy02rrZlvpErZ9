using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000590 RID: 1424
	internal class SecP192K1FieldElement : AbstractFpFieldElement
	{
		// Token: 0x06002D24 RID: 11556 RVA: 0x000EE0F8 File Offset: 0x000EE0F8
		public SecP192K1FieldElement(BigInteger x)
		{
			if (x == null || x.SignValue < 0 || x.CompareTo(SecP192K1FieldElement.Q) >= 0)
			{
				throw new ArgumentException("value invalid for SecP192K1FieldElement", "x");
			}
			this.x = SecP192K1Field.FromBigInteger(x);
		}

		// Token: 0x06002D25 RID: 11557 RVA: 0x000EE150 File Offset: 0x000EE150
		public SecP192K1FieldElement()
		{
			this.x = Nat192.Create();
		}

		// Token: 0x06002D26 RID: 11558 RVA: 0x000EE164 File Offset: 0x000EE164
		protected internal SecP192K1FieldElement(uint[] x)
		{
			this.x = x;
		}

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x06002D27 RID: 11559 RVA: 0x000EE174 File Offset: 0x000EE174
		public override bool IsZero
		{
			get
			{
				return Nat192.IsZero(this.x);
			}
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x06002D28 RID: 11560 RVA: 0x000EE184 File Offset: 0x000EE184
		public override bool IsOne
		{
			get
			{
				return Nat192.IsOne(this.x);
			}
		}

		// Token: 0x06002D29 RID: 11561 RVA: 0x000EE194 File Offset: 0x000EE194
		public override bool TestBitZero()
		{
			return Nat192.GetBit(this.x, 0) == 1U;
		}

		// Token: 0x06002D2A RID: 11562 RVA: 0x000EE1A8 File Offset: 0x000EE1A8
		public override BigInteger ToBigInteger()
		{
			return Nat192.ToBigInteger(this.x);
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x06002D2B RID: 11563 RVA: 0x000EE1B8 File Offset: 0x000EE1B8
		public override string FieldName
		{
			get
			{
				return "SecP192K1Field";
			}
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06002D2C RID: 11564 RVA: 0x000EE1C0 File Offset: 0x000EE1C0
		public override int FieldSize
		{
			get
			{
				return SecP192K1FieldElement.Q.BitLength;
			}
		}

		// Token: 0x06002D2D RID: 11565 RVA: 0x000EE1CC File Offset: 0x000EE1CC
		public override ECFieldElement Add(ECFieldElement b)
		{
			uint[] z = Nat192.Create();
			SecP192K1Field.Add(this.x, ((SecP192K1FieldElement)b).x, z);
			return new SecP192K1FieldElement(z);
		}

		// Token: 0x06002D2E RID: 11566 RVA: 0x000EE200 File Offset: 0x000EE200
		public override ECFieldElement AddOne()
		{
			uint[] z = Nat192.Create();
			SecP192K1Field.AddOne(this.x, z);
			return new SecP192K1FieldElement(z);
		}

		// Token: 0x06002D2F RID: 11567 RVA: 0x000EE22C File Offset: 0x000EE22C
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			uint[] z = Nat192.Create();
			SecP192K1Field.Subtract(this.x, ((SecP192K1FieldElement)b).x, z);
			return new SecP192K1FieldElement(z);
		}

		// Token: 0x06002D30 RID: 11568 RVA: 0x000EE260 File Offset: 0x000EE260
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			uint[] z = Nat192.Create();
			SecP192K1Field.Multiply(this.x, ((SecP192K1FieldElement)b).x, z);
			return new SecP192K1FieldElement(z);
		}

		// Token: 0x06002D31 RID: 11569 RVA: 0x000EE294 File Offset: 0x000EE294
		public override ECFieldElement Divide(ECFieldElement b)
		{
			uint[] z = Nat192.Create();
			Mod.Invert(SecP192K1Field.P, ((SecP192K1FieldElement)b).x, z);
			SecP192K1Field.Multiply(z, this.x, z);
			return new SecP192K1FieldElement(z);
		}

		// Token: 0x06002D32 RID: 11570 RVA: 0x000EE2D4 File Offset: 0x000EE2D4
		public override ECFieldElement Negate()
		{
			uint[] z = Nat192.Create();
			SecP192K1Field.Negate(this.x, z);
			return new SecP192K1FieldElement(z);
		}

		// Token: 0x06002D33 RID: 11571 RVA: 0x000EE300 File Offset: 0x000EE300
		public override ECFieldElement Square()
		{
			uint[] z = Nat192.Create();
			SecP192K1Field.Square(this.x, z);
			return new SecP192K1FieldElement(z);
		}

		// Token: 0x06002D34 RID: 11572 RVA: 0x000EE32C File Offset: 0x000EE32C
		public override ECFieldElement Invert()
		{
			uint[] z = Nat192.Create();
			Mod.Invert(SecP192K1Field.P, this.x, z);
			return new SecP192K1FieldElement(z);
		}

		// Token: 0x06002D35 RID: 11573 RVA: 0x000EE35C File Offset: 0x000EE35C
		public override ECFieldElement Sqrt()
		{
			uint[] y = this.x;
			if (Nat192.IsZero(y) || Nat192.IsOne(y))
			{
				return this;
			}
			uint[] array = Nat192.Create();
			SecP192K1Field.Square(y, array);
			SecP192K1Field.Multiply(array, y, array);
			uint[] array2 = Nat192.Create();
			SecP192K1Field.Square(array, array2);
			SecP192K1Field.Multiply(array2, y, array2);
			uint[] array3 = Nat192.Create();
			SecP192K1Field.SquareN(array2, 3, array3);
			SecP192K1Field.Multiply(array3, array2, array3);
			uint[] array4 = array3;
			SecP192K1Field.SquareN(array3, 2, array4);
			SecP192K1Field.Multiply(array4, array, array4);
			uint[] array5 = array;
			SecP192K1Field.SquareN(array4, 8, array5);
			SecP192K1Field.Multiply(array5, array4, array5);
			uint[] array6 = array4;
			SecP192K1Field.SquareN(array5, 3, array6);
			SecP192K1Field.Multiply(array6, array2, array6);
			uint[] array7 = Nat192.Create();
			SecP192K1Field.SquareN(array6, 16, array7);
			SecP192K1Field.Multiply(array7, array5, array7);
			uint[] array8 = array5;
			SecP192K1Field.SquareN(array7, 35, array8);
			SecP192K1Field.Multiply(array8, array7, array8);
			uint[] z = array7;
			SecP192K1Field.SquareN(array8, 70, z);
			SecP192K1Field.Multiply(z, array8, z);
			uint[] array9 = array8;
			SecP192K1Field.SquareN(z, 19, array9);
			SecP192K1Field.Multiply(array9, array6, array9);
			uint[] z2 = array9;
			SecP192K1Field.SquareN(z2, 20, z2);
			SecP192K1Field.Multiply(z2, array6, z2);
			SecP192K1Field.SquareN(z2, 4, z2);
			SecP192K1Field.Multiply(z2, array2, z2);
			SecP192K1Field.SquareN(z2, 6, z2);
			SecP192K1Field.Multiply(z2, array2, z2);
			SecP192K1Field.Square(z2, z2);
			uint[] array10 = array2;
			SecP192K1Field.Square(z2, array10);
			if (!Nat192.Eq(y, array10))
			{
				return null;
			}
			return new SecP192K1FieldElement(z2);
		}

		// Token: 0x06002D36 RID: 11574 RVA: 0x000EE4EC File Offset: 0x000EE4EC
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SecP192K1FieldElement);
		}

		// Token: 0x06002D37 RID: 11575 RVA: 0x000EE4FC File Offset: 0x000EE4FC
		public override bool Equals(ECFieldElement other)
		{
			return this.Equals(other as SecP192K1FieldElement);
		}

		// Token: 0x06002D38 RID: 11576 RVA: 0x000EE50C File Offset: 0x000EE50C
		public virtual bool Equals(SecP192K1FieldElement other)
		{
			return this == other || (other != null && Nat192.Eq(this.x, other.x));
		}

		// Token: 0x06002D39 RID: 11577 RVA: 0x000EE530 File Offset: 0x000EE530
		public override int GetHashCode()
		{
			return SecP192K1FieldElement.Q.GetHashCode() ^ Arrays.GetHashCode(this.x, 0, 6);
		}

		// Token: 0x04001BB4 RID: 7092
		public static readonly BigInteger Q = new BigInteger(1, Hex.DecodeStrict("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFEE37"));

		// Token: 0x04001BB5 RID: 7093
		protected internal readonly uint[] x;
	}
}
