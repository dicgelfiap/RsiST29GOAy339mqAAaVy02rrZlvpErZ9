using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x020005A8 RID: 1448
	internal class SecP384R1FieldElement : AbstractFpFieldElement
	{
		// Token: 0x06002E98 RID: 11928 RVA: 0x000F4E9C File Offset: 0x000F4E9C
		public SecP384R1FieldElement(BigInteger x)
		{
			if (x == null || x.SignValue < 0 || x.CompareTo(SecP384R1FieldElement.Q) >= 0)
			{
				throw new ArgumentException("value invalid for SecP384R1FieldElement", "x");
			}
			this.x = SecP384R1Field.FromBigInteger(x);
		}

		// Token: 0x06002E99 RID: 11929 RVA: 0x000F4EF4 File Offset: 0x000F4EF4
		public SecP384R1FieldElement()
		{
			this.x = Nat.Create(12);
		}

		// Token: 0x06002E9A RID: 11930 RVA: 0x000F4F0C File Offset: 0x000F4F0C
		protected internal SecP384R1FieldElement(uint[] x)
		{
			this.x = x;
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06002E9B RID: 11931 RVA: 0x000F4F1C File Offset: 0x000F4F1C
		public override bool IsZero
		{
			get
			{
				return Nat.IsZero(12, this.x);
			}
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06002E9C RID: 11932 RVA: 0x000F4F2C File Offset: 0x000F4F2C
		public override bool IsOne
		{
			get
			{
				return Nat.IsOne(12, this.x);
			}
		}

		// Token: 0x06002E9D RID: 11933 RVA: 0x000F4F3C File Offset: 0x000F4F3C
		public override bool TestBitZero()
		{
			return Nat.GetBit(this.x, 0) == 1U;
		}

		// Token: 0x06002E9E RID: 11934 RVA: 0x000F4F50 File Offset: 0x000F4F50
		public override BigInteger ToBigInteger()
		{
			return Nat.ToBigInteger(12, this.x);
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06002E9F RID: 11935 RVA: 0x000F4F60 File Offset: 0x000F4F60
		public override string FieldName
		{
			get
			{
				return "SecP384R1Field";
			}
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06002EA0 RID: 11936 RVA: 0x000F4F68 File Offset: 0x000F4F68
		public override int FieldSize
		{
			get
			{
				return SecP384R1FieldElement.Q.BitLength;
			}
		}

		// Token: 0x06002EA1 RID: 11937 RVA: 0x000F4F74 File Offset: 0x000F4F74
		public override ECFieldElement Add(ECFieldElement b)
		{
			uint[] z = Nat.Create(12);
			SecP384R1Field.Add(this.x, ((SecP384R1FieldElement)b).x, z);
			return new SecP384R1FieldElement(z);
		}

		// Token: 0x06002EA2 RID: 11938 RVA: 0x000F4FAC File Offset: 0x000F4FAC
		public override ECFieldElement AddOne()
		{
			uint[] z = Nat.Create(12);
			SecP384R1Field.AddOne(this.x, z);
			return new SecP384R1FieldElement(z);
		}

		// Token: 0x06002EA3 RID: 11939 RVA: 0x000F4FD8 File Offset: 0x000F4FD8
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			uint[] z = Nat.Create(12);
			SecP384R1Field.Subtract(this.x, ((SecP384R1FieldElement)b).x, z);
			return new SecP384R1FieldElement(z);
		}

		// Token: 0x06002EA4 RID: 11940 RVA: 0x000F5010 File Offset: 0x000F5010
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			uint[] z = Nat.Create(12);
			SecP384R1Field.Multiply(this.x, ((SecP384R1FieldElement)b).x, z);
			return new SecP384R1FieldElement(z);
		}

		// Token: 0x06002EA5 RID: 11941 RVA: 0x000F5048 File Offset: 0x000F5048
		public override ECFieldElement Divide(ECFieldElement b)
		{
			uint[] z = Nat.Create(12);
			Mod.Invert(SecP384R1Field.P, ((SecP384R1FieldElement)b).x, z);
			SecP384R1Field.Multiply(z, this.x, z);
			return new SecP384R1FieldElement(z);
		}

		// Token: 0x06002EA6 RID: 11942 RVA: 0x000F508C File Offset: 0x000F508C
		public override ECFieldElement Negate()
		{
			uint[] z = Nat.Create(12);
			SecP384R1Field.Negate(this.x, z);
			return new SecP384R1FieldElement(z);
		}

		// Token: 0x06002EA7 RID: 11943 RVA: 0x000F50B8 File Offset: 0x000F50B8
		public override ECFieldElement Square()
		{
			uint[] z = Nat.Create(12);
			SecP384R1Field.Square(this.x, z);
			return new SecP384R1FieldElement(z);
		}

		// Token: 0x06002EA8 RID: 11944 RVA: 0x000F50E4 File Offset: 0x000F50E4
		public override ECFieldElement Invert()
		{
			uint[] z = Nat.Create(12);
			Mod.Invert(SecP384R1Field.P, this.x, z);
			return new SecP384R1FieldElement(z);
		}

		// Token: 0x06002EA9 RID: 11945 RVA: 0x000F5114 File Offset: 0x000F5114
		public override ECFieldElement Sqrt()
		{
			uint[] y = this.x;
			if (Nat.IsZero(12, y) || Nat.IsOne(12, y))
			{
				return this;
			}
			uint[] array = Nat.Create(12);
			uint[] array2 = Nat.Create(12);
			uint[] array3 = Nat.Create(12);
			uint[] array4 = Nat.Create(12);
			SecP384R1Field.Square(y, array);
			SecP384R1Field.Multiply(array, y, array);
			SecP384R1Field.SquareN(array, 2, array2);
			SecP384R1Field.Multiply(array2, array, array2);
			SecP384R1Field.Square(array2, array2);
			SecP384R1Field.Multiply(array2, y, array2);
			SecP384R1Field.SquareN(array2, 5, array3);
			SecP384R1Field.Multiply(array3, array2, array3);
			SecP384R1Field.SquareN(array3, 5, array4);
			SecP384R1Field.Multiply(array4, array2, array4);
			SecP384R1Field.SquareN(array4, 15, array2);
			SecP384R1Field.Multiply(array2, array4, array2);
			SecP384R1Field.SquareN(array2, 2, array3);
			SecP384R1Field.Multiply(array, array3, array);
			SecP384R1Field.SquareN(array3, 28, array3);
			SecP384R1Field.Multiply(array2, array3, array2);
			SecP384R1Field.SquareN(array2, 60, array3);
			SecP384R1Field.Multiply(array3, array2, array3);
			uint[] z = array2;
			SecP384R1Field.SquareN(array3, 120, z);
			SecP384R1Field.Multiply(z, array3, z);
			SecP384R1Field.SquareN(z, 15, z);
			SecP384R1Field.Multiply(z, array4, z);
			SecP384R1Field.SquareN(z, 33, z);
			SecP384R1Field.Multiply(z, array, z);
			SecP384R1Field.SquareN(z, 64, z);
			SecP384R1Field.Multiply(z, y, z);
			SecP384R1Field.SquareN(z, 30, array);
			SecP384R1Field.Square(array, array2);
			if (!Nat.Eq(12, y, array2))
			{
				return null;
			}
			return new SecP384R1FieldElement(array);
		}

		// Token: 0x06002EAA RID: 11946 RVA: 0x000F5280 File Offset: 0x000F5280
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SecP384R1FieldElement);
		}

		// Token: 0x06002EAB RID: 11947 RVA: 0x000F5290 File Offset: 0x000F5290
		public override bool Equals(ECFieldElement other)
		{
			return this.Equals(other as SecP384R1FieldElement);
		}

		// Token: 0x06002EAC RID: 11948 RVA: 0x000F52A0 File Offset: 0x000F52A0
		public virtual bool Equals(SecP384R1FieldElement other)
		{
			return this == other || (other != null && Nat.Eq(12, this.x, other.x));
		}

		// Token: 0x06002EAD RID: 11949 RVA: 0x000F52C8 File Offset: 0x000F52C8
		public override int GetHashCode()
		{
			return SecP384R1FieldElement.Q.GetHashCode() ^ Arrays.GetHashCode(this.x, 0, 12);
		}

		// Token: 0x04001BFE RID: 7166
		public static readonly BigInteger Q = new BigInteger(1, Hex.DecodeStrict("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFFFF0000000000000000FFFFFFFF"));

		// Token: 0x04001BFF RID: 7167
		protected internal readonly uint[] x;
	}
}
