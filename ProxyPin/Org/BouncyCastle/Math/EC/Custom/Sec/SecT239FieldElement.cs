using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x020005D2 RID: 1490
	internal class SecT239FieldElement : AbstractF2mFieldElement
	{
		// Token: 0x0600315C RID: 12636 RVA: 0x00101AE8 File Offset: 0x00101AE8
		public SecT239FieldElement(BigInteger x)
		{
			if (x == null || x.SignValue < 0 || x.BitLength > 239)
			{
				throw new ArgumentException("value invalid for SecT239FieldElement", "x");
			}
			this.x = SecT239Field.FromBigInteger(x);
		}

		// Token: 0x0600315D RID: 12637 RVA: 0x00101B40 File Offset: 0x00101B40
		public SecT239FieldElement()
		{
			this.x = Nat256.Create64();
		}

		// Token: 0x0600315E RID: 12638 RVA: 0x00101B54 File Offset: 0x00101B54
		protected internal SecT239FieldElement(ulong[] x)
		{
			this.x = x;
		}

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x0600315F RID: 12639 RVA: 0x00101B64 File Offset: 0x00101B64
		public override bool IsOne
		{
			get
			{
				return Nat256.IsOne64(this.x);
			}
		}

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06003160 RID: 12640 RVA: 0x00101B74 File Offset: 0x00101B74
		public override bool IsZero
		{
			get
			{
				return Nat256.IsZero64(this.x);
			}
		}

		// Token: 0x06003161 RID: 12641 RVA: 0x00101B84 File Offset: 0x00101B84
		public override bool TestBitZero()
		{
			return (this.x[0] & 1UL) != 0UL;
		}

		// Token: 0x06003162 RID: 12642 RVA: 0x00101B98 File Offset: 0x00101B98
		public override BigInteger ToBigInteger()
		{
			return Nat256.ToBigInteger64(this.x);
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06003163 RID: 12643 RVA: 0x00101BA8 File Offset: 0x00101BA8
		public override string FieldName
		{
			get
			{
				return "SecT239Field";
			}
		}

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06003164 RID: 12644 RVA: 0x00101BB0 File Offset: 0x00101BB0
		public override int FieldSize
		{
			get
			{
				return 239;
			}
		}

		// Token: 0x06003165 RID: 12645 RVA: 0x00101BB8 File Offset: 0x00101BB8
		public override ECFieldElement Add(ECFieldElement b)
		{
			ulong[] z = Nat256.Create64();
			SecT239Field.Add(this.x, ((SecT239FieldElement)b).x, z);
			return new SecT239FieldElement(z);
		}

		// Token: 0x06003166 RID: 12646 RVA: 0x00101BEC File Offset: 0x00101BEC
		public override ECFieldElement AddOne()
		{
			ulong[] z = Nat256.Create64();
			SecT239Field.AddOne(this.x, z);
			return new SecT239FieldElement(z);
		}

		// Token: 0x06003167 RID: 12647 RVA: 0x00101C18 File Offset: 0x00101C18
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			return this.Add(b);
		}

		// Token: 0x06003168 RID: 12648 RVA: 0x00101C24 File Offset: 0x00101C24
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			ulong[] z = Nat256.Create64();
			SecT239Field.Multiply(this.x, ((SecT239FieldElement)b).x, z);
			return new SecT239FieldElement(z);
		}

		// Token: 0x06003169 RID: 12649 RVA: 0x00101C58 File Offset: 0x00101C58
		public override ECFieldElement MultiplyMinusProduct(ECFieldElement b, ECFieldElement x, ECFieldElement y)
		{
			return this.MultiplyPlusProduct(b, x, y);
		}

		// Token: 0x0600316A RID: 12650 RVA: 0x00101C64 File Offset: 0x00101C64
		public override ECFieldElement MultiplyPlusProduct(ECFieldElement b, ECFieldElement x, ECFieldElement y)
		{
			ulong[] array = this.x;
			ulong[] y2 = ((SecT239FieldElement)b).x;
			ulong[] array2 = ((SecT239FieldElement)x).x;
			ulong[] y3 = ((SecT239FieldElement)y).x;
			ulong[] array3 = Nat256.CreateExt64();
			SecT239Field.MultiplyAddToExt(array, y2, array3);
			SecT239Field.MultiplyAddToExt(array2, y3, array3);
			ulong[] z = Nat256.Create64();
			SecT239Field.Reduce(array3, z);
			return new SecT239FieldElement(z);
		}

		// Token: 0x0600316B RID: 12651 RVA: 0x00101CD0 File Offset: 0x00101CD0
		public override ECFieldElement Divide(ECFieldElement b)
		{
			return this.Multiply(b.Invert());
		}

		// Token: 0x0600316C RID: 12652 RVA: 0x00101CE0 File Offset: 0x00101CE0
		public override ECFieldElement Negate()
		{
			return this;
		}

		// Token: 0x0600316D RID: 12653 RVA: 0x00101CE4 File Offset: 0x00101CE4
		public override ECFieldElement Square()
		{
			ulong[] z = Nat256.Create64();
			SecT239Field.Square(this.x, z);
			return new SecT239FieldElement(z);
		}

		// Token: 0x0600316E RID: 12654 RVA: 0x00101D10 File Offset: 0x00101D10
		public override ECFieldElement SquareMinusProduct(ECFieldElement x, ECFieldElement y)
		{
			return this.SquarePlusProduct(x, y);
		}

		// Token: 0x0600316F RID: 12655 RVA: 0x00101D1C File Offset: 0x00101D1C
		public override ECFieldElement SquarePlusProduct(ECFieldElement x, ECFieldElement y)
		{
			ulong[] array = this.x;
			ulong[] array2 = ((SecT239FieldElement)x).x;
			ulong[] y2 = ((SecT239FieldElement)y).x;
			ulong[] array3 = Nat256.CreateExt64();
			SecT239Field.SquareAddToExt(array, array3);
			SecT239Field.MultiplyAddToExt(array2, y2, array3);
			ulong[] z = Nat256.Create64();
			SecT239Field.Reduce(array3, z);
			return new SecT239FieldElement(z);
		}

		// Token: 0x06003170 RID: 12656 RVA: 0x00101D78 File Offset: 0x00101D78
		public override ECFieldElement SquarePow(int pow)
		{
			if (pow < 1)
			{
				return this;
			}
			ulong[] z = Nat256.Create64();
			SecT239Field.SquareN(this.x, pow, z);
			return new SecT239FieldElement(z);
		}

		// Token: 0x06003171 RID: 12657 RVA: 0x00101DAC File Offset: 0x00101DAC
		public override ECFieldElement HalfTrace()
		{
			ulong[] z = Nat256.Create64();
			SecT239Field.HalfTrace(this.x, z);
			return new SecT239FieldElement(z);
		}

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06003172 RID: 12658 RVA: 0x00101DD8 File Offset: 0x00101DD8
		public override bool HasFastTrace
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003173 RID: 12659 RVA: 0x00101DDC File Offset: 0x00101DDC
		public override int Trace()
		{
			return (int)SecT239Field.Trace(this.x);
		}

		// Token: 0x06003174 RID: 12660 RVA: 0x00101DEC File Offset: 0x00101DEC
		public override ECFieldElement Invert()
		{
			ulong[] z = Nat256.Create64();
			SecT239Field.Invert(this.x, z);
			return new SecT239FieldElement(z);
		}

		// Token: 0x06003175 RID: 12661 RVA: 0x00101E18 File Offset: 0x00101E18
		public override ECFieldElement Sqrt()
		{
			ulong[] z = Nat256.Create64();
			SecT239Field.Sqrt(this.x, z);
			return new SecT239FieldElement(z);
		}

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06003176 RID: 12662 RVA: 0x00101E44 File Offset: 0x00101E44
		public virtual int Representation
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06003177 RID: 12663 RVA: 0x00101E48 File Offset: 0x00101E48
		public virtual int M
		{
			get
			{
				return 239;
			}
		}

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x06003178 RID: 12664 RVA: 0x00101E50 File Offset: 0x00101E50
		public virtual int K1
		{
			get
			{
				return 158;
			}
		}

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x06003179 RID: 12665 RVA: 0x00101E58 File Offset: 0x00101E58
		public virtual int K2
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x0600317A RID: 12666 RVA: 0x00101E5C File Offset: 0x00101E5C
		public virtual int K3
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600317B RID: 12667 RVA: 0x00101E60 File Offset: 0x00101E60
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SecT239FieldElement);
		}

		// Token: 0x0600317C RID: 12668 RVA: 0x00101E70 File Offset: 0x00101E70
		public override bool Equals(ECFieldElement other)
		{
			return this.Equals(other as SecT239FieldElement);
		}

		// Token: 0x0600317D RID: 12669 RVA: 0x00101E80 File Offset: 0x00101E80
		public virtual bool Equals(SecT239FieldElement other)
		{
			return this == other || (other != null && Nat256.Eq64(this.x, other.x));
		}

		// Token: 0x0600317E RID: 12670 RVA: 0x00101EA4 File Offset: 0x00101EA4
		public override int GetHashCode()
		{
			return 23900158 ^ Arrays.GetHashCode(this.x, 0, 4);
		}

		// Token: 0x04001C49 RID: 7241
		protected internal readonly ulong[] x;
	}
}
