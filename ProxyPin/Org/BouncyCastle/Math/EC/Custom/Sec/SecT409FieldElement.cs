using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x020005DC RID: 1500
	internal class SecT409FieldElement : AbstractF2mFieldElement
	{
		// Token: 0x0600321F RID: 12831 RVA: 0x00105120 File Offset: 0x00105120
		public SecT409FieldElement(BigInteger x)
		{
			if (x == null || x.SignValue < 0 || x.BitLength > 409)
			{
				throw new ArgumentException("value invalid for SecT409FieldElement", "x");
			}
			this.x = SecT409Field.FromBigInteger(x);
		}

		// Token: 0x06003220 RID: 12832 RVA: 0x00105178 File Offset: 0x00105178
		public SecT409FieldElement()
		{
			this.x = Nat448.Create64();
		}

		// Token: 0x06003221 RID: 12833 RVA: 0x0010518C File Offset: 0x0010518C
		protected internal SecT409FieldElement(ulong[] x)
		{
			this.x = x;
		}

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x06003222 RID: 12834 RVA: 0x0010519C File Offset: 0x0010519C
		public override bool IsOne
		{
			get
			{
				return Nat448.IsOne64(this.x);
			}
		}

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x06003223 RID: 12835 RVA: 0x001051AC File Offset: 0x001051AC
		public override bool IsZero
		{
			get
			{
				return Nat448.IsZero64(this.x);
			}
		}

		// Token: 0x06003224 RID: 12836 RVA: 0x001051BC File Offset: 0x001051BC
		public override bool TestBitZero()
		{
			return (this.x[0] & 1UL) != 0UL;
		}

		// Token: 0x06003225 RID: 12837 RVA: 0x001051D0 File Offset: 0x001051D0
		public override BigInteger ToBigInteger()
		{
			return Nat448.ToBigInteger64(this.x);
		}

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x06003226 RID: 12838 RVA: 0x001051E0 File Offset: 0x001051E0
		public override string FieldName
		{
			get
			{
				return "SecT409Field";
			}
		}

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x06003227 RID: 12839 RVA: 0x001051E8 File Offset: 0x001051E8
		public override int FieldSize
		{
			get
			{
				return 409;
			}
		}

		// Token: 0x06003228 RID: 12840 RVA: 0x001051F0 File Offset: 0x001051F0
		public override ECFieldElement Add(ECFieldElement b)
		{
			ulong[] z = Nat448.Create64();
			SecT409Field.Add(this.x, ((SecT409FieldElement)b).x, z);
			return new SecT409FieldElement(z);
		}

		// Token: 0x06003229 RID: 12841 RVA: 0x00105224 File Offset: 0x00105224
		public override ECFieldElement AddOne()
		{
			ulong[] z = Nat448.Create64();
			SecT409Field.AddOne(this.x, z);
			return new SecT409FieldElement(z);
		}

		// Token: 0x0600322A RID: 12842 RVA: 0x00105250 File Offset: 0x00105250
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			return this.Add(b);
		}

		// Token: 0x0600322B RID: 12843 RVA: 0x0010525C File Offset: 0x0010525C
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			ulong[] z = Nat448.Create64();
			SecT409Field.Multiply(this.x, ((SecT409FieldElement)b).x, z);
			return new SecT409FieldElement(z);
		}

		// Token: 0x0600322C RID: 12844 RVA: 0x00105290 File Offset: 0x00105290
		public override ECFieldElement MultiplyMinusProduct(ECFieldElement b, ECFieldElement x, ECFieldElement y)
		{
			return this.MultiplyPlusProduct(b, x, y);
		}

		// Token: 0x0600322D RID: 12845 RVA: 0x0010529C File Offset: 0x0010529C
		public override ECFieldElement MultiplyPlusProduct(ECFieldElement b, ECFieldElement x, ECFieldElement y)
		{
			ulong[] array = this.x;
			ulong[] y2 = ((SecT409FieldElement)b).x;
			ulong[] array2 = ((SecT409FieldElement)x).x;
			ulong[] y3 = ((SecT409FieldElement)y).x;
			ulong[] array3 = Nat.Create64(13);
			SecT409Field.MultiplyAddToExt(array, y2, array3);
			SecT409Field.MultiplyAddToExt(array2, y3, array3);
			ulong[] z = Nat448.Create64();
			SecT409Field.Reduce(array3, z);
			return new SecT409FieldElement(z);
		}

		// Token: 0x0600322E RID: 12846 RVA: 0x0010530C File Offset: 0x0010530C
		public override ECFieldElement Divide(ECFieldElement b)
		{
			return this.Multiply(b.Invert());
		}

		// Token: 0x0600322F RID: 12847 RVA: 0x0010531C File Offset: 0x0010531C
		public override ECFieldElement Negate()
		{
			return this;
		}

		// Token: 0x06003230 RID: 12848 RVA: 0x00105320 File Offset: 0x00105320
		public override ECFieldElement Square()
		{
			ulong[] z = Nat448.Create64();
			SecT409Field.Square(this.x, z);
			return new SecT409FieldElement(z);
		}

		// Token: 0x06003231 RID: 12849 RVA: 0x0010534C File Offset: 0x0010534C
		public override ECFieldElement SquareMinusProduct(ECFieldElement x, ECFieldElement y)
		{
			return this.SquarePlusProduct(x, y);
		}

		// Token: 0x06003232 RID: 12850 RVA: 0x00105358 File Offset: 0x00105358
		public override ECFieldElement SquarePlusProduct(ECFieldElement x, ECFieldElement y)
		{
			ulong[] array = this.x;
			ulong[] array2 = ((SecT409FieldElement)x).x;
			ulong[] y2 = ((SecT409FieldElement)y).x;
			ulong[] array3 = Nat.Create64(13);
			SecT409Field.SquareAddToExt(array, array3);
			SecT409Field.MultiplyAddToExt(array2, y2, array3);
			ulong[] z = Nat448.Create64();
			SecT409Field.Reduce(array3, z);
			return new SecT409FieldElement(z);
		}

		// Token: 0x06003233 RID: 12851 RVA: 0x001053B8 File Offset: 0x001053B8
		public override ECFieldElement SquarePow(int pow)
		{
			if (pow < 1)
			{
				return this;
			}
			ulong[] z = Nat448.Create64();
			SecT409Field.SquareN(this.x, pow, z);
			return new SecT409FieldElement(z);
		}

		// Token: 0x06003234 RID: 12852 RVA: 0x001053EC File Offset: 0x001053EC
		public override ECFieldElement HalfTrace()
		{
			ulong[] z = Nat448.Create64();
			SecT409Field.HalfTrace(this.x, z);
			return new SecT409FieldElement(z);
		}

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x06003235 RID: 12853 RVA: 0x00105418 File Offset: 0x00105418
		public override bool HasFastTrace
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003236 RID: 12854 RVA: 0x0010541C File Offset: 0x0010541C
		public override int Trace()
		{
			return (int)SecT409Field.Trace(this.x);
		}

		// Token: 0x06003237 RID: 12855 RVA: 0x0010542C File Offset: 0x0010542C
		public override ECFieldElement Invert()
		{
			ulong[] z = Nat448.Create64();
			SecT409Field.Invert(this.x, z);
			return new SecT409FieldElement(z);
		}

		// Token: 0x06003238 RID: 12856 RVA: 0x00105458 File Offset: 0x00105458
		public override ECFieldElement Sqrt()
		{
			ulong[] z = Nat448.Create64();
			SecT409Field.Sqrt(this.x, z);
			return new SecT409FieldElement(z);
		}

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x06003239 RID: 12857 RVA: 0x00105484 File Offset: 0x00105484
		public virtual int Representation
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x0600323A RID: 12858 RVA: 0x00105488 File Offset: 0x00105488
		public virtual int M
		{
			get
			{
				return 409;
			}
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x0600323B RID: 12859 RVA: 0x00105490 File Offset: 0x00105490
		public virtual int K1
		{
			get
			{
				return 87;
			}
		}

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x0600323C RID: 12860 RVA: 0x00105494 File Offset: 0x00105494
		public virtual int K2
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x0600323D RID: 12861 RVA: 0x00105498 File Offset: 0x00105498
		public virtual int K3
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600323E RID: 12862 RVA: 0x0010549C File Offset: 0x0010549C
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SecT409FieldElement);
		}

		// Token: 0x0600323F RID: 12863 RVA: 0x001054AC File Offset: 0x001054AC
		public override bool Equals(ECFieldElement other)
		{
			return this.Equals(other as SecT409FieldElement);
		}

		// Token: 0x06003240 RID: 12864 RVA: 0x001054BC File Offset: 0x001054BC
		public virtual bool Equals(SecT409FieldElement other)
		{
			return this == other || (other != null && Nat448.Eq64(this.x, other.x));
		}

		// Token: 0x06003241 RID: 12865 RVA: 0x001054E0 File Offset: 0x001054E0
		public override int GetHashCode()
		{
			return 4090087 ^ Arrays.GetHashCode(this.x, 0, 7);
		}

		// Token: 0x04001C5C RID: 7260
		protected internal readonly ulong[] x;
	}
}
