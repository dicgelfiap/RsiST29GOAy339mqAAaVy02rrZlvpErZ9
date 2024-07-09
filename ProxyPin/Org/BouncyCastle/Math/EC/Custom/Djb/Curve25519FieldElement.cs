using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Math.EC.Custom.Djb
{
	// Token: 0x02000576 RID: 1398
	internal class Curve25519FieldElement : AbstractFpFieldElement
	{
		// Token: 0x06002BA2 RID: 11170 RVA: 0x000E7728 File Offset: 0x000E7728
		public Curve25519FieldElement(BigInteger x)
		{
			if (x == null || x.SignValue < 0 || x.CompareTo(Curve25519FieldElement.Q) >= 0)
			{
				throw new ArgumentException("value invalid for Curve25519FieldElement", "x");
			}
			this.x = Curve25519Field.FromBigInteger(x);
		}

		// Token: 0x06002BA3 RID: 11171 RVA: 0x000E7780 File Offset: 0x000E7780
		public Curve25519FieldElement()
		{
			this.x = Nat256.Create();
		}

		// Token: 0x06002BA4 RID: 11172 RVA: 0x000E7794 File Offset: 0x000E7794
		protected internal Curve25519FieldElement(uint[] x)
		{
			this.x = x;
		}

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06002BA5 RID: 11173 RVA: 0x000E77A4 File Offset: 0x000E77A4
		public override bool IsZero
		{
			get
			{
				return Nat256.IsZero(this.x);
			}
		}

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06002BA6 RID: 11174 RVA: 0x000E77B4 File Offset: 0x000E77B4
		public override bool IsOne
		{
			get
			{
				return Nat256.IsOne(this.x);
			}
		}

		// Token: 0x06002BA7 RID: 11175 RVA: 0x000E77C4 File Offset: 0x000E77C4
		public override bool TestBitZero()
		{
			return Nat256.GetBit(this.x, 0) == 1U;
		}

		// Token: 0x06002BA8 RID: 11176 RVA: 0x000E77D8 File Offset: 0x000E77D8
		public override BigInteger ToBigInteger()
		{
			return Nat256.ToBigInteger(this.x);
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06002BA9 RID: 11177 RVA: 0x000E77E8 File Offset: 0x000E77E8
		public override string FieldName
		{
			get
			{
				return "Curve25519Field";
			}
		}

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06002BAA RID: 11178 RVA: 0x000E77F0 File Offset: 0x000E77F0
		public override int FieldSize
		{
			get
			{
				return Curve25519FieldElement.Q.BitLength;
			}
		}

		// Token: 0x06002BAB RID: 11179 RVA: 0x000E77FC File Offset: 0x000E77FC
		public override ECFieldElement Add(ECFieldElement b)
		{
			uint[] z = Nat256.Create();
			Curve25519Field.Add(this.x, ((Curve25519FieldElement)b).x, z);
			return new Curve25519FieldElement(z);
		}

		// Token: 0x06002BAC RID: 11180 RVA: 0x000E7830 File Offset: 0x000E7830
		public override ECFieldElement AddOne()
		{
			uint[] z = Nat256.Create();
			Curve25519Field.AddOne(this.x, z);
			return new Curve25519FieldElement(z);
		}

		// Token: 0x06002BAD RID: 11181 RVA: 0x000E785C File Offset: 0x000E785C
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			uint[] z = Nat256.Create();
			Curve25519Field.Subtract(this.x, ((Curve25519FieldElement)b).x, z);
			return new Curve25519FieldElement(z);
		}

		// Token: 0x06002BAE RID: 11182 RVA: 0x000E7890 File Offset: 0x000E7890
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			uint[] z = Nat256.Create();
			Curve25519Field.Multiply(this.x, ((Curve25519FieldElement)b).x, z);
			return new Curve25519FieldElement(z);
		}

		// Token: 0x06002BAF RID: 11183 RVA: 0x000E78C4 File Offset: 0x000E78C4
		public override ECFieldElement Divide(ECFieldElement b)
		{
			uint[] z = Nat256.Create();
			Mod.Invert(Curve25519Field.P, ((Curve25519FieldElement)b).x, z);
			Curve25519Field.Multiply(z, this.x, z);
			return new Curve25519FieldElement(z);
		}

		// Token: 0x06002BB0 RID: 11184 RVA: 0x000E7904 File Offset: 0x000E7904
		public override ECFieldElement Negate()
		{
			uint[] z = Nat256.Create();
			Curve25519Field.Negate(this.x, z);
			return new Curve25519FieldElement(z);
		}

		// Token: 0x06002BB1 RID: 11185 RVA: 0x000E7930 File Offset: 0x000E7930
		public override ECFieldElement Square()
		{
			uint[] z = Nat256.Create();
			Curve25519Field.Square(this.x, z);
			return new Curve25519FieldElement(z);
		}

		// Token: 0x06002BB2 RID: 11186 RVA: 0x000E795C File Offset: 0x000E795C
		public override ECFieldElement Invert()
		{
			uint[] z = Nat256.Create();
			Mod.Invert(Curve25519Field.P, this.x, z);
			return new Curve25519FieldElement(z);
		}

		// Token: 0x06002BB3 RID: 11187 RVA: 0x000E798C File Offset: 0x000E798C
		public override ECFieldElement Sqrt()
		{
			uint[] y = this.x;
			if (Nat256.IsZero(y) || Nat256.IsOne(y))
			{
				return this;
			}
			uint[] array = Nat256.Create();
			Curve25519Field.Square(y, array);
			Curve25519Field.Multiply(array, y, array);
			uint[] array2 = array;
			Curve25519Field.Square(array, array2);
			Curve25519Field.Multiply(array2, y, array2);
			uint[] array3 = Nat256.Create();
			Curve25519Field.Square(array2, array3);
			Curve25519Field.Multiply(array3, y, array3);
			uint[] array4 = Nat256.Create();
			Curve25519Field.SquareN(array3, 3, array4);
			Curve25519Field.Multiply(array4, array2, array4);
			uint[] array5 = array2;
			Curve25519Field.SquareN(array4, 4, array5);
			Curve25519Field.Multiply(array5, array3, array5);
			uint[] array6 = array4;
			Curve25519Field.SquareN(array5, 4, array6);
			Curve25519Field.Multiply(array6, array3, array6);
			uint[] array7 = array3;
			Curve25519Field.SquareN(array6, 15, array7);
			Curve25519Field.Multiply(array7, array6, array7);
			uint[] array8 = array6;
			Curve25519Field.SquareN(array7, 30, array8);
			Curve25519Field.Multiply(array8, array7, array8);
			uint[] array9 = array7;
			Curve25519Field.SquareN(array8, 60, array9);
			Curve25519Field.Multiply(array9, array8, array9);
			uint[] z = array8;
			Curve25519Field.SquareN(array9, 11, z);
			Curve25519Field.Multiply(z, array5, z);
			uint[] array10 = array5;
			Curve25519Field.SquareN(z, 120, array10);
			Curve25519Field.Multiply(array10, array9, array10);
			uint[] z2 = array10;
			Curve25519Field.Square(z2, z2);
			uint[] array11 = array9;
			Curve25519Field.Square(z2, array11);
			if (Nat256.Eq(y, array11))
			{
				return new Curve25519FieldElement(z2);
			}
			Curve25519Field.Multiply(z2, Curve25519FieldElement.PRECOMP_POW2, z2);
			Curve25519Field.Square(z2, array11);
			if (Nat256.Eq(y, array11))
			{
				return new Curve25519FieldElement(z2);
			}
			return null;
		}

		// Token: 0x06002BB4 RID: 11188 RVA: 0x000E7B20 File Offset: 0x000E7B20
		public override bool Equals(object obj)
		{
			return this.Equals(obj as Curve25519FieldElement);
		}

		// Token: 0x06002BB5 RID: 11189 RVA: 0x000E7B30 File Offset: 0x000E7B30
		public override bool Equals(ECFieldElement other)
		{
			return this.Equals(other as Curve25519FieldElement);
		}

		// Token: 0x06002BB6 RID: 11190 RVA: 0x000E7B40 File Offset: 0x000E7B40
		public virtual bool Equals(Curve25519FieldElement other)
		{
			return this == other || (other != null && Nat256.Eq(this.x, other.x));
		}

		// Token: 0x06002BB7 RID: 11191 RVA: 0x000E7B64 File Offset: 0x000E7B64
		public override int GetHashCode()
		{
			return Curve25519FieldElement.Q.GetHashCode() ^ Arrays.GetHashCode(this.x, 0, 8);
		}

		// Token: 0x04001B69 RID: 7017
		public static readonly BigInteger Q = Nat256.ToBigInteger(Curve25519Field.P);

		// Token: 0x04001B6A RID: 7018
		private static readonly uint[] PRECOMP_POW2 = new uint[]
		{
			1242472624U,
			3303938855U,
			2905597048U,
			792926214U,
			1039914919U,
			726466713U,
			1338105611U,
			730014848U
		};

		// Token: 0x04001B6B RID: 7019
		protected internal readonly uint[] x;
	}
}
