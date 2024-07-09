using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x0200059C RID: 1436
	internal class SecP224R1FieldElement : AbstractFpFieldElement
	{
		// Token: 0x06002DDC RID: 11740 RVA: 0x000F16C4 File Offset: 0x000F16C4
		public SecP224R1FieldElement(BigInteger x)
		{
			if (x == null || x.SignValue < 0 || x.CompareTo(SecP224R1FieldElement.Q) >= 0)
			{
				throw new ArgumentException("value invalid for SecP224R1FieldElement", "x");
			}
			this.x = SecP224R1Field.FromBigInteger(x);
		}

		// Token: 0x06002DDD RID: 11741 RVA: 0x000F171C File Offset: 0x000F171C
		public SecP224R1FieldElement()
		{
			this.x = Nat224.Create();
		}

		// Token: 0x06002DDE RID: 11742 RVA: 0x000F1730 File Offset: 0x000F1730
		protected internal SecP224R1FieldElement(uint[] x)
		{
			this.x = x;
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06002DDF RID: 11743 RVA: 0x000F1740 File Offset: 0x000F1740
		public override bool IsZero
		{
			get
			{
				return Nat224.IsZero(this.x);
			}
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06002DE0 RID: 11744 RVA: 0x000F1750 File Offset: 0x000F1750
		public override bool IsOne
		{
			get
			{
				return Nat224.IsOne(this.x);
			}
		}

		// Token: 0x06002DE1 RID: 11745 RVA: 0x000F1760 File Offset: 0x000F1760
		public override bool TestBitZero()
		{
			return Nat224.GetBit(this.x, 0) == 1U;
		}

		// Token: 0x06002DE2 RID: 11746 RVA: 0x000F1774 File Offset: 0x000F1774
		public override BigInteger ToBigInteger()
		{
			return Nat224.ToBigInteger(this.x);
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x06002DE3 RID: 11747 RVA: 0x000F1784 File Offset: 0x000F1784
		public override string FieldName
		{
			get
			{
				return "SecP224R1Field";
			}
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x06002DE4 RID: 11748 RVA: 0x000F178C File Offset: 0x000F178C
		public override int FieldSize
		{
			get
			{
				return SecP224R1FieldElement.Q.BitLength;
			}
		}

		// Token: 0x06002DE5 RID: 11749 RVA: 0x000F1798 File Offset: 0x000F1798
		public override ECFieldElement Add(ECFieldElement b)
		{
			uint[] z = Nat224.Create();
			SecP224R1Field.Add(this.x, ((SecP224R1FieldElement)b).x, z);
			return new SecP224R1FieldElement(z);
		}

		// Token: 0x06002DE6 RID: 11750 RVA: 0x000F17CC File Offset: 0x000F17CC
		public override ECFieldElement AddOne()
		{
			uint[] z = Nat224.Create();
			SecP224R1Field.AddOne(this.x, z);
			return new SecP224R1FieldElement(z);
		}

		// Token: 0x06002DE7 RID: 11751 RVA: 0x000F17F8 File Offset: 0x000F17F8
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			uint[] z = Nat224.Create();
			SecP224R1Field.Subtract(this.x, ((SecP224R1FieldElement)b).x, z);
			return new SecP224R1FieldElement(z);
		}

		// Token: 0x06002DE8 RID: 11752 RVA: 0x000F182C File Offset: 0x000F182C
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			uint[] z = Nat224.Create();
			SecP224R1Field.Multiply(this.x, ((SecP224R1FieldElement)b).x, z);
			return new SecP224R1FieldElement(z);
		}

		// Token: 0x06002DE9 RID: 11753 RVA: 0x000F1860 File Offset: 0x000F1860
		public override ECFieldElement Divide(ECFieldElement b)
		{
			uint[] z = Nat224.Create();
			Mod.Invert(SecP224R1Field.P, ((SecP224R1FieldElement)b).x, z);
			SecP224R1Field.Multiply(z, this.x, z);
			return new SecP224R1FieldElement(z);
		}

		// Token: 0x06002DEA RID: 11754 RVA: 0x000F18A0 File Offset: 0x000F18A0
		public override ECFieldElement Negate()
		{
			uint[] z = Nat224.Create();
			SecP224R1Field.Negate(this.x, z);
			return new SecP224R1FieldElement(z);
		}

		// Token: 0x06002DEB RID: 11755 RVA: 0x000F18CC File Offset: 0x000F18CC
		public override ECFieldElement Square()
		{
			uint[] z = Nat224.Create();
			SecP224R1Field.Square(this.x, z);
			return new SecP224R1FieldElement(z);
		}

		// Token: 0x06002DEC RID: 11756 RVA: 0x000F18F8 File Offset: 0x000F18F8
		public override ECFieldElement Invert()
		{
			uint[] z = Nat224.Create();
			Mod.Invert(SecP224R1Field.P, this.x, z);
			return new SecP224R1FieldElement(z);
		}

		// Token: 0x06002DED RID: 11757 RVA: 0x000F1928 File Offset: 0x000F1928
		public override ECFieldElement Sqrt()
		{
			uint[] array = this.x;
			if (Nat224.IsZero(array) || Nat224.IsOne(array))
			{
				return this;
			}
			uint[] array2 = Nat224.Create();
			SecP224R1Field.Negate(array, array2);
			uint[] array3 = Mod.Random(SecP224R1Field.P);
			uint[] t = Nat224.Create();
			if (!SecP224R1FieldElement.IsSquare(array))
			{
				return null;
			}
			while (!SecP224R1FieldElement.TrySqrt(array2, array3, t))
			{
				SecP224R1Field.AddOne(array3, array3);
			}
			SecP224R1Field.Square(t, array3);
			if (!Nat224.Eq(array, array3))
			{
				return null;
			}
			return new SecP224R1FieldElement(t);
		}

		// Token: 0x06002DEE RID: 11758 RVA: 0x000F19B0 File Offset: 0x000F19B0
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SecP224R1FieldElement);
		}

		// Token: 0x06002DEF RID: 11759 RVA: 0x000F19C0 File Offset: 0x000F19C0
		public override bool Equals(ECFieldElement other)
		{
			return this.Equals(other as SecP224R1FieldElement);
		}

		// Token: 0x06002DF0 RID: 11760 RVA: 0x000F19D0 File Offset: 0x000F19D0
		public virtual bool Equals(SecP224R1FieldElement other)
		{
			return this == other || (other != null && Nat224.Eq(this.x, other.x));
		}

		// Token: 0x06002DF1 RID: 11761 RVA: 0x000F19F4 File Offset: 0x000F19F4
		public override int GetHashCode()
		{
			return SecP224R1FieldElement.Q.GetHashCode() ^ Arrays.GetHashCode(this.x, 0, 7);
		}

		// Token: 0x06002DF2 RID: 11762 RVA: 0x000F1A10 File Offset: 0x000F1A10
		private static bool IsSquare(uint[] x)
		{
			uint[] z = Nat224.Create();
			uint[] array = Nat224.Create();
			Nat224.Copy(x, z);
			for (int i = 0; i < 7; i++)
			{
				Nat224.Copy(z, array);
				SecP224R1Field.SquareN(z, 1 << i, z);
				SecP224R1Field.Multiply(z, array, z);
			}
			SecP224R1Field.SquareN(z, 95, z);
			return Nat224.IsOne(z);
		}

		// Token: 0x06002DF3 RID: 11763 RVA: 0x000F1A70 File Offset: 0x000F1A70
		private static void RM(uint[] nc, uint[] d0, uint[] e0, uint[] d1, uint[] e1, uint[] f1, uint[] t)
		{
			SecP224R1Field.Multiply(e1, e0, t);
			SecP224R1Field.Multiply(t, nc, t);
			SecP224R1Field.Multiply(d1, d0, f1);
			SecP224R1Field.Add(f1, t, f1);
			SecP224R1Field.Multiply(d1, e0, t);
			Nat224.Copy(f1, d1);
			SecP224R1Field.Multiply(e1, d0, e1);
			SecP224R1Field.Add(e1, t, e1);
			SecP224R1Field.Square(e1, f1);
			SecP224R1Field.Multiply(f1, nc, f1);
		}

		// Token: 0x06002DF4 RID: 11764 RVA: 0x000F1AE4 File Offset: 0x000F1AE4
		private static void RP(uint[] nc, uint[] d1, uint[] e1, uint[] f1, uint[] t)
		{
			Nat224.Copy(nc, f1);
			uint[] array = Nat224.Create();
			uint[] array2 = Nat224.Create();
			for (int i = 0; i < 7; i++)
			{
				Nat224.Copy(d1, array);
				Nat224.Copy(e1, array2);
				int num = 1 << i;
				while (--num >= 0)
				{
					SecP224R1FieldElement.RS(d1, e1, f1, t);
				}
				SecP224R1FieldElement.RM(nc, array, array2, d1, e1, f1, t);
			}
		}

		// Token: 0x06002DF5 RID: 11765 RVA: 0x000F1B50 File Offset: 0x000F1B50
		private static void RS(uint[] d, uint[] e, uint[] f, uint[] t)
		{
			SecP224R1Field.Multiply(e, d, e);
			SecP224R1Field.Twice(e, e);
			SecP224R1Field.Square(d, t);
			SecP224R1Field.Add(f, t, d);
			SecP224R1Field.Multiply(f, t, f);
			uint num = Nat.ShiftUpBits(7, f, 2, 0U);
			SecP224R1Field.Reduce32(num, f);
		}

		// Token: 0x06002DF6 RID: 11766 RVA: 0x000F1B98 File Offset: 0x000F1B98
		private static bool TrySqrt(uint[] nc, uint[] r, uint[] t)
		{
			uint[] array = Nat224.Create();
			Nat224.Copy(r, array);
			uint[] array2 = Nat224.Create();
			array2[0] = 1U;
			uint[] array3 = Nat224.Create();
			SecP224R1FieldElement.RP(nc, array, array2, array3, t);
			uint[] array4 = Nat224.Create();
			uint[] z = Nat224.Create();
			for (int i = 1; i < 96; i++)
			{
				Nat224.Copy(array, array4);
				Nat224.Copy(array2, z);
				SecP224R1FieldElement.RS(array, array2, array3, t);
				if (Nat224.IsZero(array))
				{
					Mod.Invert(SecP224R1Field.P, z, t);
					SecP224R1Field.Multiply(t, array4, t);
					return true;
				}
			}
			return false;
		}

		// Token: 0x04001BDA RID: 7130
		public static readonly BigInteger Q = new BigInteger(1, Hex.DecodeStrict("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF000000000000000000000001"));

		// Token: 0x04001BDB RID: 7131
		protected internal readonly uint[] x;
	}
}
