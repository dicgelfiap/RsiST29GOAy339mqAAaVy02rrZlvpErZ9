using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Math.EC
{
	// Token: 0x02000574 RID: 1396
	public abstract class ECFieldElement
	{
		// Token: 0x06002B86 RID: 11142
		public abstract BigInteger ToBigInteger();

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06002B87 RID: 11143
		public abstract string FieldName { get; }

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06002B88 RID: 11144
		public abstract int FieldSize { get; }

		// Token: 0x06002B89 RID: 11145
		public abstract ECFieldElement Add(ECFieldElement b);

		// Token: 0x06002B8A RID: 11146
		public abstract ECFieldElement AddOne();

		// Token: 0x06002B8B RID: 11147
		public abstract ECFieldElement Subtract(ECFieldElement b);

		// Token: 0x06002B8C RID: 11148
		public abstract ECFieldElement Multiply(ECFieldElement b);

		// Token: 0x06002B8D RID: 11149
		public abstract ECFieldElement Divide(ECFieldElement b);

		// Token: 0x06002B8E RID: 11150
		public abstract ECFieldElement Negate();

		// Token: 0x06002B8F RID: 11151
		public abstract ECFieldElement Square();

		// Token: 0x06002B90 RID: 11152
		public abstract ECFieldElement Invert();

		// Token: 0x06002B91 RID: 11153
		public abstract ECFieldElement Sqrt();

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06002B92 RID: 11154 RVA: 0x000E75EC File Offset: 0x000E75EC
		public virtual int BitLength
		{
			get
			{
				return this.ToBigInteger().BitLength;
			}
		}

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06002B93 RID: 11155 RVA: 0x000E75FC File Offset: 0x000E75FC
		public virtual bool IsOne
		{
			get
			{
				return this.BitLength == 1;
			}
		}

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06002B94 RID: 11156 RVA: 0x000E7608 File Offset: 0x000E7608
		public virtual bool IsZero
		{
			get
			{
				return 0 == this.ToBigInteger().SignValue;
			}
		}

		// Token: 0x06002B95 RID: 11157 RVA: 0x000E7618 File Offset: 0x000E7618
		public virtual ECFieldElement MultiplyMinusProduct(ECFieldElement b, ECFieldElement x, ECFieldElement y)
		{
			return this.Multiply(b).Subtract(x.Multiply(y));
		}

		// Token: 0x06002B96 RID: 11158 RVA: 0x000E7630 File Offset: 0x000E7630
		public virtual ECFieldElement MultiplyPlusProduct(ECFieldElement b, ECFieldElement x, ECFieldElement y)
		{
			return this.Multiply(b).Add(x.Multiply(y));
		}

		// Token: 0x06002B97 RID: 11159 RVA: 0x000E7648 File Offset: 0x000E7648
		public virtual ECFieldElement SquareMinusProduct(ECFieldElement x, ECFieldElement y)
		{
			return this.Square().Subtract(x.Multiply(y));
		}

		// Token: 0x06002B98 RID: 11160 RVA: 0x000E765C File Offset: 0x000E765C
		public virtual ECFieldElement SquarePlusProduct(ECFieldElement x, ECFieldElement y)
		{
			return this.Square().Add(x.Multiply(y));
		}

		// Token: 0x06002B99 RID: 11161 RVA: 0x000E7670 File Offset: 0x000E7670
		public virtual ECFieldElement SquarePow(int pow)
		{
			ECFieldElement ecfieldElement = this;
			for (int i = 0; i < pow; i++)
			{
				ecfieldElement = ecfieldElement.Square();
			}
			return ecfieldElement;
		}

		// Token: 0x06002B9A RID: 11162 RVA: 0x000E769C File Offset: 0x000E769C
		public virtual bool TestBitZero()
		{
			return this.ToBigInteger().TestBit(0);
		}

		// Token: 0x06002B9B RID: 11163 RVA: 0x000E76AC File Offset: 0x000E76AC
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ECFieldElement);
		}

		// Token: 0x06002B9C RID: 11164 RVA: 0x000E76BC File Offset: 0x000E76BC
		public virtual bool Equals(ECFieldElement other)
		{
			return this == other || (other != null && this.ToBigInteger().Equals(other.ToBigInteger()));
		}

		// Token: 0x06002B9D RID: 11165 RVA: 0x000E76E0 File Offset: 0x000E76E0
		public override int GetHashCode()
		{
			return this.ToBigInteger().GetHashCode();
		}

		// Token: 0x06002B9E RID: 11166 RVA: 0x000E76F0 File Offset: 0x000E76F0
		public override string ToString()
		{
			return this.ToBigInteger().ToString(16);
		}

		// Token: 0x06002B9F RID: 11167 RVA: 0x000E7700 File Offset: 0x000E7700
		public virtual byte[] GetEncoded()
		{
			return BigIntegers.AsUnsignedByteArray((this.FieldSize + 7) / 8, this.ToBigInteger());
		}
	}
}
