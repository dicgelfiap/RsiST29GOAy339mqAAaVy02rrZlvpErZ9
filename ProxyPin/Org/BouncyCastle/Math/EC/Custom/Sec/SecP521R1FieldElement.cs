using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x020005AC RID: 1452
	internal class SecP521R1FieldElement : AbstractFpFieldElement
	{
		// Token: 0x06002ED3 RID: 11987 RVA: 0x000F5F14 File Offset: 0x000F5F14
		public SecP521R1FieldElement(BigInteger x)
		{
			if (x == null || x.SignValue < 0 || x.CompareTo(SecP521R1FieldElement.Q) >= 0)
			{
				throw new ArgumentException("value invalid for SecP521R1FieldElement", "x");
			}
			this.x = SecP521R1Field.FromBigInteger(x);
		}

		// Token: 0x06002ED4 RID: 11988 RVA: 0x000F5F6C File Offset: 0x000F5F6C
		public SecP521R1FieldElement()
		{
			this.x = Nat.Create(17);
		}

		// Token: 0x06002ED5 RID: 11989 RVA: 0x000F5F84 File Offset: 0x000F5F84
		protected internal SecP521R1FieldElement(uint[] x)
		{
			this.x = x;
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06002ED6 RID: 11990 RVA: 0x000F5F94 File Offset: 0x000F5F94
		public override bool IsZero
		{
			get
			{
				return Nat.IsZero(17, this.x);
			}
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06002ED7 RID: 11991 RVA: 0x000F5FA4 File Offset: 0x000F5FA4
		public override bool IsOne
		{
			get
			{
				return Nat.IsOne(17, this.x);
			}
		}

		// Token: 0x06002ED8 RID: 11992 RVA: 0x000F5FB4 File Offset: 0x000F5FB4
		public override bool TestBitZero()
		{
			return Nat.GetBit(this.x, 0) == 1U;
		}

		// Token: 0x06002ED9 RID: 11993 RVA: 0x000F5FC8 File Offset: 0x000F5FC8
		public override BigInteger ToBigInteger()
		{
			return Nat.ToBigInteger(17, this.x);
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06002EDA RID: 11994 RVA: 0x000F5FD8 File Offset: 0x000F5FD8
		public override string FieldName
		{
			get
			{
				return "SecP521R1Field";
			}
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06002EDB RID: 11995 RVA: 0x000F5FE0 File Offset: 0x000F5FE0
		public override int FieldSize
		{
			get
			{
				return SecP521R1FieldElement.Q.BitLength;
			}
		}

		// Token: 0x06002EDC RID: 11996 RVA: 0x000F5FEC File Offset: 0x000F5FEC
		public override ECFieldElement Add(ECFieldElement b)
		{
			uint[] z = Nat.Create(17);
			SecP521R1Field.Add(this.x, ((SecP521R1FieldElement)b).x, z);
			return new SecP521R1FieldElement(z);
		}

		// Token: 0x06002EDD RID: 11997 RVA: 0x000F6024 File Offset: 0x000F6024
		public override ECFieldElement AddOne()
		{
			uint[] z = Nat.Create(17);
			SecP521R1Field.AddOne(this.x, z);
			return new SecP521R1FieldElement(z);
		}

		// Token: 0x06002EDE RID: 11998 RVA: 0x000F6050 File Offset: 0x000F6050
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			uint[] z = Nat.Create(17);
			SecP521R1Field.Subtract(this.x, ((SecP521R1FieldElement)b).x, z);
			return new SecP521R1FieldElement(z);
		}

		// Token: 0x06002EDF RID: 11999 RVA: 0x000F6088 File Offset: 0x000F6088
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			uint[] z = Nat.Create(17);
			SecP521R1Field.Multiply(this.x, ((SecP521R1FieldElement)b).x, z);
			return new SecP521R1FieldElement(z);
		}

		// Token: 0x06002EE0 RID: 12000 RVA: 0x000F60C0 File Offset: 0x000F60C0
		public override ECFieldElement Divide(ECFieldElement b)
		{
			uint[] z = Nat.Create(17);
			Mod.Invert(SecP521R1Field.P, ((SecP521R1FieldElement)b).x, z);
			SecP521R1Field.Multiply(z, this.x, z);
			return new SecP521R1FieldElement(z);
		}

		// Token: 0x06002EE1 RID: 12001 RVA: 0x000F6104 File Offset: 0x000F6104
		public override ECFieldElement Negate()
		{
			uint[] z = Nat.Create(17);
			SecP521R1Field.Negate(this.x, z);
			return new SecP521R1FieldElement(z);
		}

		// Token: 0x06002EE2 RID: 12002 RVA: 0x000F6130 File Offset: 0x000F6130
		public override ECFieldElement Square()
		{
			uint[] z = Nat.Create(17);
			SecP521R1Field.Square(this.x, z);
			return new SecP521R1FieldElement(z);
		}

		// Token: 0x06002EE3 RID: 12003 RVA: 0x000F615C File Offset: 0x000F615C
		public override ECFieldElement Invert()
		{
			uint[] z = Nat.Create(17);
			Mod.Invert(SecP521R1Field.P, this.x, z);
			return new SecP521R1FieldElement(z);
		}

		// Token: 0x06002EE4 RID: 12004 RVA: 0x000F618C File Offset: 0x000F618C
		public override ECFieldElement Sqrt()
		{
			uint[] array = this.x;
			if (Nat.IsZero(17, array) || Nat.IsOne(17, array))
			{
				return this;
			}
			uint[] z = Nat.Create(17);
			uint[] array2 = Nat.Create(17);
			SecP521R1Field.SquareN(array, 519, z);
			SecP521R1Field.Square(z, array2);
			if (!Nat.Eq(17, array, array2))
			{
				return null;
			}
			return new SecP521R1FieldElement(z);
		}

		// Token: 0x06002EE5 RID: 12005 RVA: 0x000F61FC File Offset: 0x000F61FC
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SecP521R1FieldElement);
		}

		// Token: 0x06002EE6 RID: 12006 RVA: 0x000F620C File Offset: 0x000F620C
		public override bool Equals(ECFieldElement other)
		{
			return this.Equals(other as SecP521R1FieldElement);
		}

		// Token: 0x06002EE7 RID: 12007 RVA: 0x000F621C File Offset: 0x000F621C
		public virtual bool Equals(SecP521R1FieldElement other)
		{
			return this == other || (other != null && Nat.Eq(17, this.x, other.x));
		}

		// Token: 0x06002EE8 RID: 12008 RVA: 0x000F6244 File Offset: 0x000F6244
		public override int GetHashCode()
		{
			return SecP521R1FieldElement.Q.GetHashCode() ^ Arrays.GetHashCode(this.x, 0, 17);
		}

		// Token: 0x04001C07 RID: 7175
		public static readonly BigInteger Q = new BigInteger(1, Hex.DecodeStrict("01FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF"));

		// Token: 0x04001C08 RID: 7176
		protected internal readonly uint[] x;
	}
}
