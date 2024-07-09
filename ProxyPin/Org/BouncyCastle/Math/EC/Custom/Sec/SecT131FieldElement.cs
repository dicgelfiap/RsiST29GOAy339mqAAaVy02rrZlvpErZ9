using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x020005B8 RID: 1464
	internal class SecT131FieldElement : AbstractF2mFieldElement
	{
		// Token: 0x06002F8C RID: 12172 RVA: 0x000F97F4 File Offset: 0x000F97F4
		public SecT131FieldElement(BigInteger x)
		{
			if (x == null || x.SignValue < 0 || x.BitLength > 131)
			{
				throw new ArgumentException("value invalid for SecT131FieldElement", "x");
			}
			this.x = SecT131Field.FromBigInteger(x);
		}

		// Token: 0x06002F8D RID: 12173 RVA: 0x000F984C File Offset: 0x000F984C
		public SecT131FieldElement()
		{
			this.x = Nat192.Create64();
		}

		// Token: 0x06002F8E RID: 12174 RVA: 0x000F9860 File Offset: 0x000F9860
		protected internal SecT131FieldElement(ulong[] x)
		{
			this.x = x;
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06002F8F RID: 12175 RVA: 0x000F9870 File Offset: 0x000F9870
		public override bool IsOne
		{
			get
			{
				return Nat192.IsOne64(this.x);
			}
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06002F90 RID: 12176 RVA: 0x000F9880 File Offset: 0x000F9880
		public override bool IsZero
		{
			get
			{
				return Nat192.IsZero64(this.x);
			}
		}

		// Token: 0x06002F91 RID: 12177 RVA: 0x000F9890 File Offset: 0x000F9890
		public override bool TestBitZero()
		{
			return (this.x[0] & 1UL) != 0UL;
		}

		// Token: 0x06002F92 RID: 12178 RVA: 0x000F98A4 File Offset: 0x000F98A4
		public override BigInteger ToBigInteger()
		{
			return Nat192.ToBigInteger64(this.x);
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06002F93 RID: 12179 RVA: 0x000F98B4 File Offset: 0x000F98B4
		public override string FieldName
		{
			get
			{
				return "SecT131Field";
			}
		}

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06002F94 RID: 12180 RVA: 0x000F98BC File Offset: 0x000F98BC
		public override int FieldSize
		{
			get
			{
				return 131;
			}
		}

		// Token: 0x06002F95 RID: 12181 RVA: 0x000F98C4 File Offset: 0x000F98C4
		public override ECFieldElement Add(ECFieldElement b)
		{
			ulong[] z = Nat192.Create64();
			SecT131Field.Add(this.x, ((SecT131FieldElement)b).x, z);
			return new SecT131FieldElement(z);
		}

		// Token: 0x06002F96 RID: 12182 RVA: 0x000F98F8 File Offset: 0x000F98F8
		public override ECFieldElement AddOne()
		{
			ulong[] z = Nat192.Create64();
			SecT131Field.AddOne(this.x, z);
			return new SecT131FieldElement(z);
		}

		// Token: 0x06002F97 RID: 12183 RVA: 0x000F9924 File Offset: 0x000F9924
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			return this.Add(b);
		}

		// Token: 0x06002F98 RID: 12184 RVA: 0x000F9930 File Offset: 0x000F9930
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			ulong[] z = Nat192.Create64();
			SecT131Field.Multiply(this.x, ((SecT131FieldElement)b).x, z);
			return new SecT131FieldElement(z);
		}

		// Token: 0x06002F99 RID: 12185 RVA: 0x000F9964 File Offset: 0x000F9964
		public override ECFieldElement MultiplyMinusProduct(ECFieldElement b, ECFieldElement x, ECFieldElement y)
		{
			return this.MultiplyPlusProduct(b, x, y);
		}

		// Token: 0x06002F9A RID: 12186 RVA: 0x000F9970 File Offset: 0x000F9970
		public override ECFieldElement MultiplyPlusProduct(ECFieldElement b, ECFieldElement x, ECFieldElement y)
		{
			ulong[] array = this.x;
			ulong[] y2 = ((SecT131FieldElement)b).x;
			ulong[] array2 = ((SecT131FieldElement)x).x;
			ulong[] y3 = ((SecT131FieldElement)y).x;
			ulong[] array3 = Nat.Create64(5);
			SecT131Field.MultiplyAddToExt(array, y2, array3);
			SecT131Field.MultiplyAddToExt(array2, y3, array3);
			ulong[] z = Nat192.Create64();
			SecT131Field.Reduce(array3, z);
			return new SecT131FieldElement(z);
		}

		// Token: 0x06002F9B RID: 12187 RVA: 0x000F99E0 File Offset: 0x000F99E0
		public override ECFieldElement Divide(ECFieldElement b)
		{
			return this.Multiply(b.Invert());
		}

		// Token: 0x06002F9C RID: 12188 RVA: 0x000F99F0 File Offset: 0x000F99F0
		public override ECFieldElement Negate()
		{
			return this;
		}

		// Token: 0x06002F9D RID: 12189 RVA: 0x000F99F4 File Offset: 0x000F99F4
		public override ECFieldElement Square()
		{
			ulong[] z = Nat192.Create64();
			SecT131Field.Square(this.x, z);
			return new SecT131FieldElement(z);
		}

		// Token: 0x06002F9E RID: 12190 RVA: 0x000F9A20 File Offset: 0x000F9A20
		public override ECFieldElement SquareMinusProduct(ECFieldElement x, ECFieldElement y)
		{
			return this.SquarePlusProduct(x, y);
		}

		// Token: 0x06002F9F RID: 12191 RVA: 0x000F9A2C File Offset: 0x000F9A2C
		public override ECFieldElement SquarePlusProduct(ECFieldElement x, ECFieldElement y)
		{
			ulong[] array = this.x;
			ulong[] array2 = ((SecT131FieldElement)x).x;
			ulong[] y2 = ((SecT131FieldElement)y).x;
			ulong[] array3 = Nat.Create64(5);
			SecT131Field.SquareAddToExt(array, array3);
			SecT131Field.MultiplyAddToExt(array2, y2, array3);
			ulong[] z = Nat192.Create64();
			SecT131Field.Reduce(array3, z);
			return new SecT131FieldElement(z);
		}

		// Token: 0x06002FA0 RID: 12192 RVA: 0x000F9A88 File Offset: 0x000F9A88
		public override ECFieldElement SquarePow(int pow)
		{
			if (pow < 1)
			{
				return this;
			}
			ulong[] z = Nat192.Create64();
			SecT131Field.SquareN(this.x, pow, z);
			return new SecT131FieldElement(z);
		}

		// Token: 0x06002FA1 RID: 12193 RVA: 0x000F9ABC File Offset: 0x000F9ABC
		public override ECFieldElement HalfTrace()
		{
			ulong[] z = Nat192.Create64();
			SecT131Field.HalfTrace(this.x, z);
			return new SecT131FieldElement(z);
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06002FA2 RID: 12194 RVA: 0x000F9AE8 File Offset: 0x000F9AE8
		public override bool HasFastTrace
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002FA3 RID: 12195 RVA: 0x000F9AEC File Offset: 0x000F9AEC
		public override int Trace()
		{
			return (int)SecT131Field.Trace(this.x);
		}

		// Token: 0x06002FA4 RID: 12196 RVA: 0x000F9AFC File Offset: 0x000F9AFC
		public override ECFieldElement Invert()
		{
			ulong[] z = Nat192.Create64();
			SecT131Field.Invert(this.x, z);
			return new SecT131FieldElement(z);
		}

		// Token: 0x06002FA5 RID: 12197 RVA: 0x000F9B28 File Offset: 0x000F9B28
		public override ECFieldElement Sqrt()
		{
			ulong[] z = Nat192.Create64();
			SecT131Field.Sqrt(this.x, z);
			return new SecT131FieldElement(z);
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x06002FA6 RID: 12198 RVA: 0x000F9B54 File Offset: 0x000F9B54
		public virtual int Representation
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06002FA7 RID: 12199 RVA: 0x000F9B58 File Offset: 0x000F9B58
		public virtual int M
		{
			get
			{
				return 131;
			}
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06002FA8 RID: 12200 RVA: 0x000F9B60 File Offset: 0x000F9B60
		public virtual int K1
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06002FA9 RID: 12201 RVA: 0x000F9B64 File Offset: 0x000F9B64
		public virtual int K2
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06002FAA RID: 12202 RVA: 0x000F9B68 File Offset: 0x000F9B68
		public virtual int K3
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x06002FAB RID: 12203 RVA: 0x000F9B6C File Offset: 0x000F9B6C
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SecT131FieldElement);
		}

		// Token: 0x06002FAC RID: 12204 RVA: 0x000F9B7C File Offset: 0x000F9B7C
		public override bool Equals(ECFieldElement other)
		{
			return this.Equals(other as SecT131FieldElement);
		}

		// Token: 0x06002FAD RID: 12205 RVA: 0x000F9B8C File Offset: 0x000F9B8C
		public virtual bool Equals(SecT131FieldElement other)
		{
			return this == other || (other != null && Nat192.Eq64(this.x, other.x));
		}

		// Token: 0x06002FAE RID: 12206 RVA: 0x000F9BB0 File Offset: 0x000F9BB0
		public override int GetHashCode()
		{
			return 131832 ^ Arrays.GetHashCode(this.x, 0, 3);
		}

		// Token: 0x04001C18 RID: 7192
		protected internal readonly ulong[] x;
	}
}
