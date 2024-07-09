using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x020005BE RID: 1470
	internal class SecT163FieldElement : AbstractF2mFieldElement
	{
		// Token: 0x06002FF9 RID: 12281 RVA: 0x000FB6B8 File Offset: 0x000FB6B8
		public SecT163FieldElement(BigInteger x)
		{
			if (x == null || x.SignValue < 0 || x.BitLength > 163)
			{
				throw new ArgumentException("value invalid for SecT163FieldElement", "x");
			}
			this.x = SecT163Field.FromBigInteger(x);
		}

		// Token: 0x06002FFA RID: 12282 RVA: 0x000FB710 File Offset: 0x000FB710
		public SecT163FieldElement()
		{
			this.x = Nat192.Create64();
		}

		// Token: 0x06002FFB RID: 12283 RVA: 0x000FB724 File Offset: 0x000FB724
		protected internal SecT163FieldElement(ulong[] x)
		{
			this.x = x;
		}

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x06002FFC RID: 12284 RVA: 0x000FB734 File Offset: 0x000FB734
		public override bool IsOne
		{
			get
			{
				return Nat192.IsOne64(this.x);
			}
		}

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x06002FFD RID: 12285 RVA: 0x000FB744 File Offset: 0x000FB744
		public override bool IsZero
		{
			get
			{
				return Nat192.IsZero64(this.x);
			}
		}

		// Token: 0x06002FFE RID: 12286 RVA: 0x000FB754 File Offset: 0x000FB754
		public override bool TestBitZero()
		{
			return (this.x[0] & 1UL) != 0UL;
		}

		// Token: 0x06002FFF RID: 12287 RVA: 0x000FB768 File Offset: 0x000FB768
		public override BigInteger ToBigInteger()
		{
			return Nat192.ToBigInteger64(this.x);
		}

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x06003000 RID: 12288 RVA: 0x000FB778 File Offset: 0x000FB778
		public override string FieldName
		{
			get
			{
				return "SecT163Field";
			}
		}

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x06003001 RID: 12289 RVA: 0x000FB780 File Offset: 0x000FB780
		public override int FieldSize
		{
			get
			{
				return 163;
			}
		}

		// Token: 0x06003002 RID: 12290 RVA: 0x000FB788 File Offset: 0x000FB788
		public override ECFieldElement Add(ECFieldElement b)
		{
			ulong[] z = Nat192.Create64();
			SecT163Field.Add(this.x, ((SecT163FieldElement)b).x, z);
			return new SecT163FieldElement(z);
		}

		// Token: 0x06003003 RID: 12291 RVA: 0x000FB7BC File Offset: 0x000FB7BC
		public override ECFieldElement AddOne()
		{
			ulong[] z = Nat192.Create64();
			SecT163Field.AddOne(this.x, z);
			return new SecT163FieldElement(z);
		}

		// Token: 0x06003004 RID: 12292 RVA: 0x000FB7E8 File Offset: 0x000FB7E8
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			return this.Add(b);
		}

		// Token: 0x06003005 RID: 12293 RVA: 0x000FB7F4 File Offset: 0x000FB7F4
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			ulong[] z = Nat192.Create64();
			SecT163Field.Multiply(this.x, ((SecT163FieldElement)b).x, z);
			return new SecT163FieldElement(z);
		}

		// Token: 0x06003006 RID: 12294 RVA: 0x000FB828 File Offset: 0x000FB828
		public override ECFieldElement MultiplyMinusProduct(ECFieldElement b, ECFieldElement x, ECFieldElement y)
		{
			return this.MultiplyPlusProduct(b, x, y);
		}

		// Token: 0x06003007 RID: 12295 RVA: 0x000FB834 File Offset: 0x000FB834
		public override ECFieldElement MultiplyPlusProduct(ECFieldElement b, ECFieldElement x, ECFieldElement y)
		{
			ulong[] array = this.x;
			ulong[] y2 = ((SecT163FieldElement)b).x;
			ulong[] array2 = ((SecT163FieldElement)x).x;
			ulong[] y3 = ((SecT163FieldElement)y).x;
			ulong[] array3 = Nat192.CreateExt64();
			SecT163Field.MultiplyAddToExt(array, y2, array3);
			SecT163Field.MultiplyAddToExt(array2, y3, array3);
			ulong[] z = Nat192.Create64();
			SecT163Field.Reduce(array3, z);
			return new SecT163FieldElement(z);
		}

		// Token: 0x06003008 RID: 12296 RVA: 0x000FB8A0 File Offset: 0x000FB8A0
		public override ECFieldElement Divide(ECFieldElement b)
		{
			return this.Multiply(b.Invert());
		}

		// Token: 0x06003009 RID: 12297 RVA: 0x000FB8B0 File Offset: 0x000FB8B0
		public override ECFieldElement Negate()
		{
			return this;
		}

		// Token: 0x0600300A RID: 12298 RVA: 0x000FB8B4 File Offset: 0x000FB8B4
		public override ECFieldElement Square()
		{
			ulong[] z = Nat192.Create64();
			SecT163Field.Square(this.x, z);
			return new SecT163FieldElement(z);
		}

		// Token: 0x0600300B RID: 12299 RVA: 0x000FB8E0 File Offset: 0x000FB8E0
		public override ECFieldElement SquareMinusProduct(ECFieldElement x, ECFieldElement y)
		{
			return this.SquarePlusProduct(x, y);
		}

		// Token: 0x0600300C RID: 12300 RVA: 0x000FB8EC File Offset: 0x000FB8EC
		public override ECFieldElement SquarePlusProduct(ECFieldElement x, ECFieldElement y)
		{
			ulong[] array = this.x;
			ulong[] array2 = ((SecT163FieldElement)x).x;
			ulong[] y2 = ((SecT163FieldElement)y).x;
			ulong[] array3 = Nat192.CreateExt64();
			SecT163Field.SquareAddToExt(array, array3);
			SecT163Field.MultiplyAddToExt(array2, y2, array3);
			ulong[] z = Nat192.Create64();
			SecT163Field.Reduce(array3, z);
			return new SecT163FieldElement(z);
		}

		// Token: 0x0600300D RID: 12301 RVA: 0x000FB948 File Offset: 0x000FB948
		public override ECFieldElement SquarePow(int pow)
		{
			if (pow < 1)
			{
				return this;
			}
			ulong[] z = Nat192.Create64();
			SecT163Field.SquareN(this.x, pow, z);
			return new SecT163FieldElement(z);
		}

		// Token: 0x0600300E RID: 12302 RVA: 0x000FB97C File Offset: 0x000FB97C
		public override ECFieldElement HalfTrace()
		{
			ulong[] z = Nat192.Create64();
			SecT163Field.HalfTrace(this.x, z);
			return new SecT163FieldElement(z);
		}

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x0600300F RID: 12303 RVA: 0x000FB9A8 File Offset: 0x000FB9A8
		public override bool HasFastTrace
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003010 RID: 12304 RVA: 0x000FB9AC File Offset: 0x000FB9AC
		public override int Trace()
		{
			return (int)SecT163Field.Trace(this.x);
		}

		// Token: 0x06003011 RID: 12305 RVA: 0x000FB9BC File Offset: 0x000FB9BC
		public override ECFieldElement Invert()
		{
			ulong[] z = Nat192.Create64();
			SecT163Field.Invert(this.x, z);
			return new SecT163FieldElement(z);
		}

		// Token: 0x06003012 RID: 12306 RVA: 0x000FB9E8 File Offset: 0x000FB9E8
		public override ECFieldElement Sqrt()
		{
			ulong[] z = Nat192.Create64();
			SecT163Field.Sqrt(this.x, z);
			return new SecT163FieldElement(z);
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06003013 RID: 12307 RVA: 0x000FBA14 File Offset: 0x000FBA14
		public virtual int Representation
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06003014 RID: 12308 RVA: 0x000FBA18 File Offset: 0x000FBA18
		public virtual int M
		{
			get
			{
				return 163;
			}
		}

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06003015 RID: 12309 RVA: 0x000FBA20 File Offset: 0x000FBA20
		public virtual int K1
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06003016 RID: 12310 RVA: 0x000FBA24 File Offset: 0x000FBA24
		public virtual int K2
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06003017 RID: 12311 RVA: 0x000FBA28 File Offset: 0x000FBA28
		public virtual int K3
		{
			get
			{
				return 7;
			}
		}

		// Token: 0x06003018 RID: 12312 RVA: 0x000FBA2C File Offset: 0x000FBA2C
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SecT163FieldElement);
		}

		// Token: 0x06003019 RID: 12313 RVA: 0x000FBA3C File Offset: 0x000FBA3C
		public override bool Equals(ECFieldElement other)
		{
			return this.Equals(other as SecT163FieldElement);
		}

		// Token: 0x0600301A RID: 12314 RVA: 0x000FBA4C File Offset: 0x000FBA4C
		public virtual bool Equals(SecT163FieldElement other)
		{
			return this == other || (other != null && Nat192.Eq64(this.x, other.x));
		}

		// Token: 0x0600301B RID: 12315 RVA: 0x000FBA70 File Offset: 0x000FBA70
		public override int GetHashCode()
		{
			return 163763 ^ Arrays.GetHashCode(this.x, 0, 3);
		}

		// Token: 0x04001C24 RID: 7204
		protected internal readonly ulong[] x;
	}
}
