using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x020005A4 RID: 1444
	internal class SecP256R1FieldElement : AbstractFpFieldElement
	{
		// Token: 0x06002E5B RID: 11867 RVA: 0x000F3BA4 File Offset: 0x000F3BA4
		public SecP256R1FieldElement(BigInteger x)
		{
			if (x == null || x.SignValue < 0 || x.CompareTo(SecP256R1FieldElement.Q) >= 0)
			{
				throw new ArgumentException("value invalid for SecP256R1FieldElement", "x");
			}
			this.x = SecP256R1Field.FromBigInteger(x);
		}

		// Token: 0x06002E5C RID: 11868 RVA: 0x000F3BFC File Offset: 0x000F3BFC
		public SecP256R1FieldElement()
		{
			this.x = Nat256.Create();
		}

		// Token: 0x06002E5D RID: 11869 RVA: 0x000F3C10 File Offset: 0x000F3C10
		protected internal SecP256R1FieldElement(uint[] x)
		{
			this.x = x;
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06002E5E RID: 11870 RVA: 0x000F3C20 File Offset: 0x000F3C20
		public override bool IsZero
		{
			get
			{
				return Nat256.IsZero(this.x);
			}
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x06002E5F RID: 11871 RVA: 0x000F3C30 File Offset: 0x000F3C30
		public override bool IsOne
		{
			get
			{
				return Nat256.IsOne(this.x);
			}
		}

		// Token: 0x06002E60 RID: 11872 RVA: 0x000F3C40 File Offset: 0x000F3C40
		public override bool TestBitZero()
		{
			return Nat256.GetBit(this.x, 0) == 1U;
		}

		// Token: 0x06002E61 RID: 11873 RVA: 0x000F3C54 File Offset: 0x000F3C54
		public override BigInteger ToBigInteger()
		{
			return Nat256.ToBigInteger(this.x);
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06002E62 RID: 11874 RVA: 0x000F3C64 File Offset: 0x000F3C64
		public override string FieldName
		{
			get
			{
				return "SecP256R1Field";
			}
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06002E63 RID: 11875 RVA: 0x000F3C6C File Offset: 0x000F3C6C
		public override int FieldSize
		{
			get
			{
				return SecP256R1FieldElement.Q.BitLength;
			}
		}

		// Token: 0x06002E64 RID: 11876 RVA: 0x000F3C78 File Offset: 0x000F3C78
		public override ECFieldElement Add(ECFieldElement b)
		{
			uint[] z = Nat256.Create();
			SecP256R1Field.Add(this.x, ((SecP256R1FieldElement)b).x, z);
			return new SecP256R1FieldElement(z);
		}

		// Token: 0x06002E65 RID: 11877 RVA: 0x000F3CAC File Offset: 0x000F3CAC
		public override ECFieldElement AddOne()
		{
			uint[] z = Nat256.Create();
			SecP256R1Field.AddOne(this.x, z);
			return new SecP256R1FieldElement(z);
		}

		// Token: 0x06002E66 RID: 11878 RVA: 0x000F3CD8 File Offset: 0x000F3CD8
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			uint[] z = Nat256.Create();
			SecP256R1Field.Subtract(this.x, ((SecP256R1FieldElement)b).x, z);
			return new SecP256R1FieldElement(z);
		}

		// Token: 0x06002E67 RID: 11879 RVA: 0x000F3D0C File Offset: 0x000F3D0C
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			uint[] z = Nat256.Create();
			SecP256R1Field.Multiply(this.x, ((SecP256R1FieldElement)b).x, z);
			return new SecP256R1FieldElement(z);
		}

		// Token: 0x06002E68 RID: 11880 RVA: 0x000F3D40 File Offset: 0x000F3D40
		public override ECFieldElement Divide(ECFieldElement b)
		{
			uint[] z = Nat256.Create();
			Mod.Invert(SecP256R1Field.P, ((SecP256R1FieldElement)b).x, z);
			SecP256R1Field.Multiply(z, this.x, z);
			return new SecP256R1FieldElement(z);
		}

		// Token: 0x06002E69 RID: 11881 RVA: 0x000F3D80 File Offset: 0x000F3D80
		public override ECFieldElement Negate()
		{
			uint[] z = Nat256.Create();
			SecP256R1Field.Negate(this.x, z);
			return new SecP256R1FieldElement(z);
		}

		// Token: 0x06002E6A RID: 11882 RVA: 0x000F3DAC File Offset: 0x000F3DAC
		public override ECFieldElement Square()
		{
			uint[] z = Nat256.Create();
			SecP256R1Field.Square(this.x, z);
			return new SecP256R1FieldElement(z);
		}

		// Token: 0x06002E6B RID: 11883 RVA: 0x000F3DD8 File Offset: 0x000F3DD8
		public override ECFieldElement Invert()
		{
			uint[] z = Nat256.Create();
			Mod.Invert(SecP256R1Field.P, this.x, z);
			return new SecP256R1FieldElement(z);
		}

		// Token: 0x06002E6C RID: 11884 RVA: 0x000F3E08 File Offset: 0x000F3E08
		public override ECFieldElement Sqrt()
		{
			uint[] y = this.x;
			if (Nat256.IsZero(y) || Nat256.IsOne(y))
			{
				return this;
			}
			uint[] array = Nat256.Create();
			uint[] array2 = Nat256.Create();
			SecP256R1Field.Square(y, array);
			SecP256R1Field.Multiply(array, y, array);
			SecP256R1Field.SquareN(array, 2, array2);
			SecP256R1Field.Multiply(array2, array, array2);
			SecP256R1Field.SquareN(array2, 4, array);
			SecP256R1Field.Multiply(array, array2, array);
			SecP256R1Field.SquareN(array, 8, array2);
			SecP256R1Field.Multiply(array2, array, array2);
			SecP256R1Field.SquareN(array2, 16, array);
			SecP256R1Field.Multiply(array, array2, array);
			SecP256R1Field.SquareN(array, 32, array);
			SecP256R1Field.Multiply(array, y, array);
			SecP256R1Field.SquareN(array, 96, array);
			SecP256R1Field.Multiply(array, y, array);
			SecP256R1Field.SquareN(array, 94, array);
			SecP256R1Field.Multiply(array, array, array2);
			if (!Nat256.Eq(y, array2))
			{
				return null;
			}
			return new SecP256R1FieldElement(array);
		}

		// Token: 0x06002E6D RID: 11885 RVA: 0x000F3EDC File Offset: 0x000F3EDC
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SecP256R1FieldElement);
		}

		// Token: 0x06002E6E RID: 11886 RVA: 0x000F3EEC File Offset: 0x000F3EEC
		public override bool Equals(ECFieldElement other)
		{
			return this.Equals(other as SecP256R1FieldElement);
		}

		// Token: 0x06002E6F RID: 11887 RVA: 0x000F3EFC File Offset: 0x000F3EFC
		public virtual bool Equals(SecP256R1FieldElement other)
		{
			return this == other || (other != null && Nat256.Eq(this.x, other.x));
		}

		// Token: 0x06002E70 RID: 11888 RVA: 0x000F3F20 File Offset: 0x000F3F20
		public override int GetHashCode()
		{
			return SecP256R1FieldElement.Q.GetHashCode() ^ Arrays.GetHashCode(this.x, 0, 8);
		}

		// Token: 0x04001BF2 RID: 7154
		public static readonly BigInteger Q = new BigInteger(1, Hex.DecodeStrict("FFFFFFFF00000001000000000000000000000000FFFFFFFFFFFFFFFFFFFFFFFF"));

		// Token: 0x04001BF3 RID: 7155
		protected internal readonly uint[] x;
	}
}
