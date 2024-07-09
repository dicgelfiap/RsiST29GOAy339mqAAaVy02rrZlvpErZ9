using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Math.EC.Custom.GM
{
	// Token: 0x0200057E RID: 1406
	internal class SM2P256V1FieldElement : AbstractFpFieldElement
	{
		// Token: 0x06002C1E RID: 11294 RVA: 0x000E9618 File Offset: 0x000E9618
		public SM2P256V1FieldElement(BigInteger x)
		{
			if (x == null || x.SignValue < 0 || x.CompareTo(SM2P256V1FieldElement.Q) >= 0)
			{
				throw new ArgumentException("value invalid for SM2P256V1FieldElement", "x");
			}
			this.x = SM2P256V1Field.FromBigInteger(x);
		}

		// Token: 0x06002C1F RID: 11295 RVA: 0x000E9670 File Offset: 0x000E9670
		public SM2P256V1FieldElement()
		{
			this.x = Nat256.Create();
		}

		// Token: 0x06002C20 RID: 11296 RVA: 0x000E9684 File Offset: 0x000E9684
		protected internal SM2P256V1FieldElement(uint[] x)
		{
			this.x = x;
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06002C21 RID: 11297 RVA: 0x000E9694 File Offset: 0x000E9694
		public override bool IsZero
		{
			get
			{
				return Nat256.IsZero(this.x);
			}
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06002C22 RID: 11298 RVA: 0x000E96A4 File Offset: 0x000E96A4
		public override bool IsOne
		{
			get
			{
				return Nat256.IsOne(this.x);
			}
		}

		// Token: 0x06002C23 RID: 11299 RVA: 0x000E96B4 File Offset: 0x000E96B4
		public override bool TestBitZero()
		{
			return Nat256.GetBit(this.x, 0) == 1U;
		}

		// Token: 0x06002C24 RID: 11300 RVA: 0x000E96C8 File Offset: 0x000E96C8
		public override BigInteger ToBigInteger()
		{
			return Nat256.ToBigInteger(this.x);
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06002C25 RID: 11301 RVA: 0x000E96D8 File Offset: 0x000E96D8
		public override string FieldName
		{
			get
			{
				return "SM2P256V1Field";
			}
		}

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06002C26 RID: 11302 RVA: 0x000E96E0 File Offset: 0x000E96E0
		public override int FieldSize
		{
			get
			{
				return SM2P256V1FieldElement.Q.BitLength;
			}
		}

		// Token: 0x06002C27 RID: 11303 RVA: 0x000E96EC File Offset: 0x000E96EC
		public override ECFieldElement Add(ECFieldElement b)
		{
			uint[] z = Nat256.Create();
			SM2P256V1Field.Add(this.x, ((SM2P256V1FieldElement)b).x, z);
			return new SM2P256V1FieldElement(z);
		}

		// Token: 0x06002C28 RID: 11304 RVA: 0x000E9720 File Offset: 0x000E9720
		public override ECFieldElement AddOne()
		{
			uint[] z = Nat256.Create();
			SM2P256V1Field.AddOne(this.x, z);
			return new SM2P256V1FieldElement(z);
		}

		// Token: 0x06002C29 RID: 11305 RVA: 0x000E974C File Offset: 0x000E974C
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			uint[] z = Nat256.Create();
			SM2P256V1Field.Subtract(this.x, ((SM2P256V1FieldElement)b).x, z);
			return new SM2P256V1FieldElement(z);
		}

		// Token: 0x06002C2A RID: 11306 RVA: 0x000E9780 File Offset: 0x000E9780
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			uint[] z = Nat256.Create();
			SM2P256V1Field.Multiply(this.x, ((SM2P256V1FieldElement)b).x, z);
			return new SM2P256V1FieldElement(z);
		}

		// Token: 0x06002C2B RID: 11307 RVA: 0x000E97B4 File Offset: 0x000E97B4
		public override ECFieldElement Divide(ECFieldElement b)
		{
			uint[] z = Nat256.Create();
			Mod.Invert(SM2P256V1Field.P, ((SM2P256V1FieldElement)b).x, z);
			SM2P256V1Field.Multiply(z, this.x, z);
			return new SM2P256V1FieldElement(z);
		}

		// Token: 0x06002C2C RID: 11308 RVA: 0x000E97F4 File Offset: 0x000E97F4
		public override ECFieldElement Negate()
		{
			uint[] z = Nat256.Create();
			SM2P256V1Field.Negate(this.x, z);
			return new SM2P256V1FieldElement(z);
		}

		// Token: 0x06002C2D RID: 11309 RVA: 0x000E9820 File Offset: 0x000E9820
		public override ECFieldElement Square()
		{
			uint[] z = Nat256.Create();
			SM2P256V1Field.Square(this.x, z);
			return new SM2P256V1FieldElement(z);
		}

		// Token: 0x06002C2E RID: 11310 RVA: 0x000E984C File Offset: 0x000E984C
		public override ECFieldElement Invert()
		{
			uint[] z = Nat256.Create();
			Mod.Invert(SM2P256V1Field.P, this.x, z);
			return new SM2P256V1FieldElement(z);
		}

		// Token: 0x06002C2F RID: 11311 RVA: 0x000E987C File Offset: 0x000E987C
		public override ECFieldElement Sqrt()
		{
			uint[] y = this.x;
			if (Nat256.IsZero(y) || Nat256.IsOne(y))
			{
				return this;
			}
			uint[] array = Nat256.Create();
			SM2P256V1Field.Square(y, array);
			SM2P256V1Field.Multiply(array, y, array);
			uint[] array2 = Nat256.Create();
			SM2P256V1Field.SquareN(array, 2, array2);
			SM2P256V1Field.Multiply(array2, array, array2);
			uint[] array3 = Nat256.Create();
			SM2P256V1Field.SquareN(array2, 2, array3);
			SM2P256V1Field.Multiply(array3, array, array3);
			uint[] array4 = array;
			SM2P256V1Field.SquareN(array3, 6, array4);
			SM2P256V1Field.Multiply(array4, array3, array4);
			uint[] array5 = Nat256.Create();
			SM2P256V1Field.SquareN(array4, 12, array5);
			SM2P256V1Field.Multiply(array5, array4, array5);
			uint[] array6 = array4;
			SM2P256V1Field.SquareN(array5, 6, array6);
			SM2P256V1Field.Multiply(array6, array3, array6);
			uint[] array7 = array3;
			SM2P256V1Field.Square(array6, array7);
			SM2P256V1Field.Multiply(array7, y, array7);
			uint[] z = array5;
			SM2P256V1Field.SquareN(array7, 31, z);
			uint[] array8 = array6;
			SM2P256V1Field.Multiply(z, array7, array8);
			SM2P256V1Field.SquareN(z, 32, z);
			SM2P256V1Field.Multiply(z, array8, z);
			SM2P256V1Field.SquareN(z, 62, z);
			SM2P256V1Field.Multiply(z, array8, z);
			SM2P256V1Field.SquareN(z, 4, z);
			SM2P256V1Field.Multiply(z, array2, z);
			SM2P256V1Field.SquareN(z, 32, z);
			SM2P256V1Field.Multiply(z, y, z);
			SM2P256V1Field.SquareN(z, 62, z);
			uint[] array9 = array2;
			SM2P256V1Field.Square(z, array9);
			if (!Nat256.Eq(y, array9))
			{
				return null;
			}
			return new SM2P256V1FieldElement(z);
		}

		// Token: 0x06002C30 RID: 11312 RVA: 0x000E99F0 File Offset: 0x000E99F0
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SM2P256V1FieldElement);
		}

		// Token: 0x06002C31 RID: 11313 RVA: 0x000E9A00 File Offset: 0x000E9A00
		public override bool Equals(ECFieldElement other)
		{
			return this.Equals(other as SM2P256V1FieldElement);
		}

		// Token: 0x06002C32 RID: 11314 RVA: 0x000E9A10 File Offset: 0x000E9A10
		public virtual bool Equals(SM2P256V1FieldElement other)
		{
			return this == other || (other != null && Nat256.Eq(this.x, other.x));
		}

		// Token: 0x06002C33 RID: 11315 RVA: 0x000E9A34 File Offset: 0x000E9A34
		public override int GetHashCode()
		{
			return SM2P256V1FieldElement.Q.GetHashCode() ^ Arrays.GetHashCode(this.x, 0, 8);
		}

		// Token: 0x04001B7C RID: 7036
		public static readonly BigInteger Q = new BigInteger(1, Hex.DecodeStrict("FFFFFFFEFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF00000000FFFFFFFFFFFFFFFF"));

		// Token: 0x04001B7D RID: 7037
		protected internal readonly uint[] x;
	}
}
