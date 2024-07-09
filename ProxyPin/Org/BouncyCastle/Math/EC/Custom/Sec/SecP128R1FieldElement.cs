using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000582 RID: 1410
	internal class SecP128R1FieldElement : AbstractFpFieldElement
	{
		// Token: 0x06002C5C RID: 11356 RVA: 0x000EA7AC File Offset: 0x000EA7AC
		public SecP128R1FieldElement(BigInteger x)
		{
			if (x == null || x.SignValue < 0 || x.CompareTo(SecP128R1FieldElement.Q) >= 0)
			{
				throw new ArgumentException("value invalid for SecP128R1FieldElement", "x");
			}
			this.x = SecP128R1Field.FromBigInteger(x);
		}

		// Token: 0x06002C5D RID: 11357 RVA: 0x000EA804 File Offset: 0x000EA804
		public SecP128R1FieldElement()
		{
			this.x = Nat128.Create();
		}

		// Token: 0x06002C5E RID: 11358 RVA: 0x000EA818 File Offset: 0x000EA818
		protected internal SecP128R1FieldElement(uint[] x)
		{
			this.x = x;
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06002C5F RID: 11359 RVA: 0x000EA828 File Offset: 0x000EA828
		public override bool IsZero
		{
			get
			{
				return Nat128.IsZero(this.x);
			}
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x06002C60 RID: 11360 RVA: 0x000EA838 File Offset: 0x000EA838
		public override bool IsOne
		{
			get
			{
				return Nat128.IsOne(this.x);
			}
		}

		// Token: 0x06002C61 RID: 11361 RVA: 0x000EA848 File Offset: 0x000EA848
		public override bool TestBitZero()
		{
			return Nat128.GetBit(this.x, 0) == 1U;
		}

		// Token: 0x06002C62 RID: 11362 RVA: 0x000EA85C File Offset: 0x000EA85C
		public override BigInteger ToBigInteger()
		{
			return Nat128.ToBigInteger(this.x);
		}

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06002C63 RID: 11363 RVA: 0x000EA86C File Offset: 0x000EA86C
		public override string FieldName
		{
			get
			{
				return "SecP128R1Field";
			}
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x06002C64 RID: 11364 RVA: 0x000EA874 File Offset: 0x000EA874
		public override int FieldSize
		{
			get
			{
				return SecP128R1FieldElement.Q.BitLength;
			}
		}

		// Token: 0x06002C65 RID: 11365 RVA: 0x000EA880 File Offset: 0x000EA880
		public override ECFieldElement Add(ECFieldElement b)
		{
			uint[] z = Nat128.Create();
			SecP128R1Field.Add(this.x, ((SecP128R1FieldElement)b).x, z);
			return new SecP128R1FieldElement(z);
		}

		// Token: 0x06002C66 RID: 11366 RVA: 0x000EA8B4 File Offset: 0x000EA8B4
		public override ECFieldElement AddOne()
		{
			uint[] z = Nat128.Create();
			SecP128R1Field.AddOne(this.x, z);
			return new SecP128R1FieldElement(z);
		}

		// Token: 0x06002C67 RID: 11367 RVA: 0x000EA8E0 File Offset: 0x000EA8E0
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			uint[] z = Nat128.Create();
			SecP128R1Field.Subtract(this.x, ((SecP128R1FieldElement)b).x, z);
			return new SecP128R1FieldElement(z);
		}

		// Token: 0x06002C68 RID: 11368 RVA: 0x000EA914 File Offset: 0x000EA914
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			uint[] z = Nat128.Create();
			SecP128R1Field.Multiply(this.x, ((SecP128R1FieldElement)b).x, z);
			return new SecP128R1FieldElement(z);
		}

		// Token: 0x06002C69 RID: 11369 RVA: 0x000EA948 File Offset: 0x000EA948
		public override ECFieldElement Divide(ECFieldElement b)
		{
			uint[] z = Nat128.Create();
			Mod.Invert(SecP128R1Field.P, ((SecP128R1FieldElement)b).x, z);
			SecP128R1Field.Multiply(z, this.x, z);
			return new SecP128R1FieldElement(z);
		}

		// Token: 0x06002C6A RID: 11370 RVA: 0x000EA988 File Offset: 0x000EA988
		public override ECFieldElement Negate()
		{
			uint[] z = Nat128.Create();
			SecP128R1Field.Negate(this.x, z);
			return new SecP128R1FieldElement(z);
		}

		// Token: 0x06002C6B RID: 11371 RVA: 0x000EA9B4 File Offset: 0x000EA9B4
		public override ECFieldElement Square()
		{
			uint[] z = Nat128.Create();
			SecP128R1Field.Square(this.x, z);
			return new SecP128R1FieldElement(z);
		}

		// Token: 0x06002C6C RID: 11372 RVA: 0x000EA9E0 File Offset: 0x000EA9E0
		public override ECFieldElement Invert()
		{
			uint[] z = Nat128.Create();
			Mod.Invert(SecP128R1Field.P, this.x, z);
			return new SecP128R1FieldElement(z);
		}

		// Token: 0x06002C6D RID: 11373 RVA: 0x000EAA10 File Offset: 0x000EAA10
		public override ECFieldElement Sqrt()
		{
			uint[] y = this.x;
			if (Nat128.IsZero(y) || Nat128.IsOne(y))
			{
				return this;
			}
			uint[] array = Nat128.Create();
			SecP128R1Field.Square(y, array);
			SecP128R1Field.Multiply(array, y, array);
			uint[] array2 = Nat128.Create();
			SecP128R1Field.SquareN(array, 2, array2);
			SecP128R1Field.Multiply(array2, array, array2);
			uint[] array3 = Nat128.Create();
			SecP128R1Field.SquareN(array2, 4, array3);
			SecP128R1Field.Multiply(array3, array2, array3);
			uint[] array4 = array2;
			SecP128R1Field.SquareN(array3, 2, array4);
			SecP128R1Field.Multiply(array4, array, array4);
			uint[] z = array;
			SecP128R1Field.SquareN(array4, 10, z);
			SecP128R1Field.Multiply(z, array4, z);
			uint[] array5 = array3;
			SecP128R1Field.SquareN(z, 10, array5);
			SecP128R1Field.Multiply(array5, array4, array5);
			uint[] array6 = array4;
			SecP128R1Field.Square(array5, array6);
			SecP128R1Field.Multiply(array6, y, array6);
			uint[] z2 = array6;
			SecP128R1Field.SquareN(z2, 95, z2);
			uint[] array7 = array5;
			SecP128R1Field.Square(z2, array7);
			if (!Nat128.Eq(y, array7))
			{
				return null;
			}
			return new SecP128R1FieldElement(z2);
		}

		// Token: 0x06002C6E RID: 11374 RVA: 0x000EAB14 File Offset: 0x000EAB14
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SecP128R1FieldElement);
		}

		// Token: 0x06002C6F RID: 11375 RVA: 0x000EAB24 File Offset: 0x000EAB24
		public override bool Equals(ECFieldElement other)
		{
			return this.Equals(other as SecP128R1FieldElement);
		}

		// Token: 0x06002C70 RID: 11376 RVA: 0x000EAB34 File Offset: 0x000EAB34
		public virtual bool Equals(SecP128R1FieldElement other)
		{
			return this == other || (other != null && Nat128.Eq(this.x, other.x));
		}

		// Token: 0x06002C71 RID: 11377 RVA: 0x000EAB58 File Offset: 0x000EAB58
		public override int GetHashCode()
		{
			return SecP128R1FieldElement.Q.GetHashCode() ^ Arrays.GetHashCode(this.x, 0, 4);
		}

		// Token: 0x04001B88 RID: 7048
		public static readonly BigInteger Q = new BigInteger(1, Hex.DecodeStrict("FFFFFFFDFFFFFFFFFFFFFFFFFFFFFFFF"));

		// Token: 0x04001B89 RID: 7049
		protected internal readonly uint[] x;
	}
}
