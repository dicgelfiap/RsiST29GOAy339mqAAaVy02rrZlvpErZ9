using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x020005A0 RID: 1440
	internal class SecP256K1FieldElement : AbstractFpFieldElement
	{
		// Token: 0x06002E1D RID: 11805 RVA: 0x000F2880 File Offset: 0x000F2880
		public SecP256K1FieldElement(BigInteger x)
		{
			if (x == null || x.SignValue < 0 || x.CompareTo(SecP256K1FieldElement.Q) >= 0)
			{
				throw new ArgumentException("value invalid for SecP256K1FieldElement", "x");
			}
			this.x = SecP256K1Field.FromBigInteger(x);
		}

		// Token: 0x06002E1E RID: 11806 RVA: 0x000F28D8 File Offset: 0x000F28D8
		public SecP256K1FieldElement()
		{
			this.x = Nat256.Create();
		}

		// Token: 0x06002E1F RID: 11807 RVA: 0x000F28EC File Offset: 0x000F28EC
		protected internal SecP256K1FieldElement(uint[] x)
		{
			this.x = x;
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x06002E20 RID: 11808 RVA: 0x000F28FC File Offset: 0x000F28FC
		public override bool IsZero
		{
			get
			{
				return Nat256.IsZero(this.x);
			}
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x06002E21 RID: 11809 RVA: 0x000F290C File Offset: 0x000F290C
		public override bool IsOne
		{
			get
			{
				return Nat256.IsOne(this.x);
			}
		}

		// Token: 0x06002E22 RID: 11810 RVA: 0x000F291C File Offset: 0x000F291C
		public override bool TestBitZero()
		{
			return Nat256.GetBit(this.x, 0) == 1U;
		}

		// Token: 0x06002E23 RID: 11811 RVA: 0x000F2930 File Offset: 0x000F2930
		public override BigInteger ToBigInteger()
		{
			return Nat256.ToBigInteger(this.x);
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06002E24 RID: 11812 RVA: 0x000F2940 File Offset: 0x000F2940
		public override string FieldName
		{
			get
			{
				return "SecP256K1Field";
			}
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06002E25 RID: 11813 RVA: 0x000F2948 File Offset: 0x000F2948
		public override int FieldSize
		{
			get
			{
				return SecP256K1FieldElement.Q.BitLength;
			}
		}

		// Token: 0x06002E26 RID: 11814 RVA: 0x000F2954 File Offset: 0x000F2954
		public override ECFieldElement Add(ECFieldElement b)
		{
			uint[] z = Nat256.Create();
			SecP256K1Field.Add(this.x, ((SecP256K1FieldElement)b).x, z);
			return new SecP256K1FieldElement(z);
		}

		// Token: 0x06002E27 RID: 11815 RVA: 0x000F2988 File Offset: 0x000F2988
		public override ECFieldElement AddOne()
		{
			uint[] z = Nat256.Create();
			SecP256K1Field.AddOne(this.x, z);
			return new SecP256K1FieldElement(z);
		}

		// Token: 0x06002E28 RID: 11816 RVA: 0x000F29B4 File Offset: 0x000F29B4
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			uint[] z = Nat256.Create();
			SecP256K1Field.Subtract(this.x, ((SecP256K1FieldElement)b).x, z);
			return new SecP256K1FieldElement(z);
		}

		// Token: 0x06002E29 RID: 11817 RVA: 0x000F29E8 File Offset: 0x000F29E8
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			uint[] z = Nat256.Create();
			SecP256K1Field.Multiply(this.x, ((SecP256K1FieldElement)b).x, z);
			return new SecP256K1FieldElement(z);
		}

		// Token: 0x06002E2A RID: 11818 RVA: 0x000F2A1C File Offset: 0x000F2A1C
		public override ECFieldElement Divide(ECFieldElement b)
		{
			uint[] z = Nat256.Create();
			Mod.Invert(SecP256K1Field.P, ((SecP256K1FieldElement)b).x, z);
			SecP256K1Field.Multiply(z, this.x, z);
			return new SecP256K1FieldElement(z);
		}

		// Token: 0x06002E2B RID: 11819 RVA: 0x000F2A5C File Offset: 0x000F2A5C
		public override ECFieldElement Negate()
		{
			uint[] z = Nat256.Create();
			SecP256K1Field.Negate(this.x, z);
			return new SecP256K1FieldElement(z);
		}

		// Token: 0x06002E2C RID: 11820 RVA: 0x000F2A88 File Offset: 0x000F2A88
		public override ECFieldElement Square()
		{
			uint[] z = Nat256.Create();
			SecP256K1Field.Square(this.x, z);
			return new SecP256K1FieldElement(z);
		}

		// Token: 0x06002E2D RID: 11821 RVA: 0x000F2AB4 File Offset: 0x000F2AB4
		public override ECFieldElement Invert()
		{
			uint[] z = Nat256.Create();
			Mod.Invert(SecP256K1Field.P, this.x, z);
			return new SecP256K1FieldElement(z);
		}

		// Token: 0x06002E2E RID: 11822 RVA: 0x000F2AE4 File Offset: 0x000F2AE4
		public override ECFieldElement Sqrt()
		{
			uint[] y = this.x;
			if (Nat256.IsZero(y) || Nat256.IsOne(y))
			{
				return this;
			}
			uint[] array = Nat256.Create();
			SecP256K1Field.Square(y, array);
			SecP256K1Field.Multiply(array, y, array);
			uint[] array2 = Nat256.Create();
			SecP256K1Field.Square(array, array2);
			SecP256K1Field.Multiply(array2, y, array2);
			uint[] array3 = Nat256.Create();
			SecP256K1Field.SquareN(array2, 3, array3);
			SecP256K1Field.Multiply(array3, array2, array3);
			uint[] array4 = array3;
			SecP256K1Field.SquareN(array3, 3, array4);
			SecP256K1Field.Multiply(array4, array2, array4);
			uint[] array5 = array4;
			SecP256K1Field.SquareN(array4, 2, array5);
			SecP256K1Field.Multiply(array5, array, array5);
			uint[] array6 = Nat256.Create();
			SecP256K1Field.SquareN(array5, 11, array6);
			SecP256K1Field.Multiply(array6, array5, array6);
			uint[] array7 = array5;
			SecP256K1Field.SquareN(array6, 22, array7);
			SecP256K1Field.Multiply(array7, array6, array7);
			uint[] array8 = Nat256.Create();
			SecP256K1Field.SquareN(array7, 44, array8);
			SecP256K1Field.Multiply(array8, array7, array8);
			uint[] z = Nat256.Create();
			SecP256K1Field.SquareN(array8, 88, z);
			SecP256K1Field.Multiply(z, array8, z);
			uint[] z2 = array8;
			SecP256K1Field.SquareN(z, 44, z2);
			SecP256K1Field.Multiply(z2, array7, z2);
			uint[] array9 = array7;
			SecP256K1Field.SquareN(z2, 3, array9);
			SecP256K1Field.Multiply(array9, array2, array9);
			uint[] z3 = array9;
			SecP256K1Field.SquareN(z3, 23, z3);
			SecP256K1Field.Multiply(z3, array6, z3);
			SecP256K1Field.SquareN(z3, 6, z3);
			SecP256K1Field.Multiply(z3, array, z3);
			SecP256K1Field.SquareN(z3, 2, z3);
			uint[] array10 = array;
			SecP256K1Field.Square(z3, array10);
			if (!Nat256.Eq(y, array10))
			{
				return null;
			}
			return new SecP256K1FieldElement(z3);
		}

		// Token: 0x06002E2F RID: 11823 RVA: 0x000F2C80 File Offset: 0x000F2C80
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SecP256K1FieldElement);
		}

		// Token: 0x06002E30 RID: 11824 RVA: 0x000F2C90 File Offset: 0x000F2C90
		public override bool Equals(ECFieldElement other)
		{
			return this.Equals(other as SecP256K1FieldElement);
		}

		// Token: 0x06002E31 RID: 11825 RVA: 0x000F2CA0 File Offset: 0x000F2CA0
		public virtual bool Equals(SecP256K1FieldElement other)
		{
			return this == other || (other != null && Nat256.Eq(this.x, other.x));
		}

		// Token: 0x06002E32 RID: 11826 RVA: 0x000F2CC4 File Offset: 0x000F2CC4
		public override int GetHashCode()
		{
			return SecP256K1FieldElement.Q.GetHashCode() ^ Arrays.GetHashCode(this.x, 0, 8);
		}

		// Token: 0x04001BE7 RID: 7143
		public static readonly BigInteger Q = new BigInteger(1, Hex.DecodeStrict("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFC2F"));

		// Token: 0x04001BE8 RID: 7144
		protected internal readonly uint[] x;
	}
}
