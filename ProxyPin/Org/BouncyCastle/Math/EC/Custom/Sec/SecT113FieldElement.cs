using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x020005B0 RID: 1456
	internal class SecT113FieldElement : AbstractF2mFieldElement
	{
		// Token: 0x06002F0B RID: 12043 RVA: 0x000F6F74 File Offset: 0x000F6F74
		public SecT113FieldElement(BigInteger x)
		{
			if (x == null || x.SignValue < 0 || x.BitLength > 113)
			{
				throw new ArgumentException("value invalid for SecT113FieldElement", "x");
			}
			this.x = SecT113Field.FromBigInteger(x);
		}

		// Token: 0x06002F0C RID: 12044 RVA: 0x000F6FC8 File Offset: 0x000F6FC8
		public SecT113FieldElement()
		{
			this.x = Nat128.Create64();
		}

		// Token: 0x06002F0D RID: 12045 RVA: 0x000F6FDC File Offset: 0x000F6FDC
		protected internal SecT113FieldElement(ulong[] x)
		{
			this.x = x;
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x06002F0E RID: 12046 RVA: 0x000F6FEC File Offset: 0x000F6FEC
		public override bool IsOne
		{
			get
			{
				return Nat128.IsOne64(this.x);
			}
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06002F0F RID: 12047 RVA: 0x000F6FFC File Offset: 0x000F6FFC
		public override bool IsZero
		{
			get
			{
				return Nat128.IsZero64(this.x);
			}
		}

		// Token: 0x06002F10 RID: 12048 RVA: 0x000F700C File Offset: 0x000F700C
		public override bool TestBitZero()
		{
			return (this.x[0] & 1UL) != 0UL;
		}

		// Token: 0x06002F11 RID: 12049 RVA: 0x000F7020 File Offset: 0x000F7020
		public override BigInteger ToBigInteger()
		{
			return Nat128.ToBigInteger64(this.x);
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x06002F12 RID: 12050 RVA: 0x000F7030 File Offset: 0x000F7030
		public override string FieldName
		{
			get
			{
				return "SecT113Field";
			}
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06002F13 RID: 12051 RVA: 0x000F7038 File Offset: 0x000F7038
		public override int FieldSize
		{
			get
			{
				return 113;
			}
		}

		// Token: 0x06002F14 RID: 12052 RVA: 0x000F703C File Offset: 0x000F703C
		public override ECFieldElement Add(ECFieldElement b)
		{
			ulong[] z = Nat128.Create64();
			SecT113Field.Add(this.x, ((SecT113FieldElement)b).x, z);
			return new SecT113FieldElement(z);
		}

		// Token: 0x06002F15 RID: 12053 RVA: 0x000F7070 File Offset: 0x000F7070
		public override ECFieldElement AddOne()
		{
			ulong[] z = Nat128.Create64();
			SecT113Field.AddOne(this.x, z);
			return new SecT113FieldElement(z);
		}

		// Token: 0x06002F16 RID: 12054 RVA: 0x000F709C File Offset: 0x000F709C
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			return this.Add(b);
		}

		// Token: 0x06002F17 RID: 12055 RVA: 0x000F70A8 File Offset: 0x000F70A8
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			ulong[] z = Nat128.Create64();
			SecT113Field.Multiply(this.x, ((SecT113FieldElement)b).x, z);
			return new SecT113FieldElement(z);
		}

		// Token: 0x06002F18 RID: 12056 RVA: 0x000F70DC File Offset: 0x000F70DC
		public override ECFieldElement MultiplyMinusProduct(ECFieldElement b, ECFieldElement x, ECFieldElement y)
		{
			return this.MultiplyPlusProduct(b, x, y);
		}

		// Token: 0x06002F19 RID: 12057 RVA: 0x000F70E8 File Offset: 0x000F70E8
		public override ECFieldElement MultiplyPlusProduct(ECFieldElement b, ECFieldElement x, ECFieldElement y)
		{
			ulong[] array = this.x;
			ulong[] y2 = ((SecT113FieldElement)b).x;
			ulong[] array2 = ((SecT113FieldElement)x).x;
			ulong[] y3 = ((SecT113FieldElement)y).x;
			ulong[] array3 = Nat128.CreateExt64();
			SecT113Field.MultiplyAddToExt(array, y2, array3);
			SecT113Field.MultiplyAddToExt(array2, y3, array3);
			ulong[] z = Nat128.Create64();
			SecT113Field.Reduce(array3, z);
			return new SecT113FieldElement(z);
		}

		// Token: 0x06002F1A RID: 12058 RVA: 0x000F7154 File Offset: 0x000F7154
		public override ECFieldElement Divide(ECFieldElement b)
		{
			return this.Multiply(b.Invert());
		}

		// Token: 0x06002F1B RID: 12059 RVA: 0x000F7164 File Offset: 0x000F7164
		public override ECFieldElement Negate()
		{
			return this;
		}

		// Token: 0x06002F1C RID: 12060 RVA: 0x000F7168 File Offset: 0x000F7168
		public override ECFieldElement Square()
		{
			ulong[] z = Nat128.Create64();
			SecT113Field.Square(this.x, z);
			return new SecT113FieldElement(z);
		}

		// Token: 0x06002F1D RID: 12061 RVA: 0x000F7194 File Offset: 0x000F7194
		public override ECFieldElement SquareMinusProduct(ECFieldElement x, ECFieldElement y)
		{
			return this.SquarePlusProduct(x, y);
		}

		// Token: 0x06002F1E RID: 12062 RVA: 0x000F71A0 File Offset: 0x000F71A0
		public override ECFieldElement SquarePlusProduct(ECFieldElement x, ECFieldElement y)
		{
			ulong[] array = this.x;
			ulong[] array2 = ((SecT113FieldElement)x).x;
			ulong[] y2 = ((SecT113FieldElement)y).x;
			ulong[] array3 = Nat128.CreateExt64();
			SecT113Field.SquareAddToExt(array, array3);
			SecT113Field.MultiplyAddToExt(array2, y2, array3);
			ulong[] z = Nat128.Create64();
			SecT113Field.Reduce(array3, z);
			return new SecT113FieldElement(z);
		}

		// Token: 0x06002F1F RID: 12063 RVA: 0x000F71FC File Offset: 0x000F71FC
		public override ECFieldElement SquarePow(int pow)
		{
			if (pow < 1)
			{
				return this;
			}
			ulong[] z = Nat128.Create64();
			SecT113Field.SquareN(this.x, pow, z);
			return new SecT113FieldElement(z);
		}

		// Token: 0x06002F20 RID: 12064 RVA: 0x000F7230 File Offset: 0x000F7230
		public override ECFieldElement HalfTrace()
		{
			ulong[] z = Nat128.Create64();
			SecT113Field.HalfTrace(this.x, z);
			return new SecT113FieldElement(z);
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x06002F21 RID: 12065 RVA: 0x000F725C File Offset: 0x000F725C
		public override bool HasFastTrace
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002F22 RID: 12066 RVA: 0x000F7260 File Offset: 0x000F7260
		public override int Trace()
		{
			return (int)SecT113Field.Trace(this.x);
		}

		// Token: 0x06002F23 RID: 12067 RVA: 0x000F7270 File Offset: 0x000F7270
		public override ECFieldElement Invert()
		{
			ulong[] z = Nat128.Create64();
			SecT113Field.Invert(this.x, z);
			return new SecT113FieldElement(z);
		}

		// Token: 0x06002F24 RID: 12068 RVA: 0x000F729C File Offset: 0x000F729C
		public override ECFieldElement Sqrt()
		{
			ulong[] z = Nat128.Create64();
			SecT113Field.Sqrt(this.x, z);
			return new SecT113FieldElement(z);
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x06002F25 RID: 12069 RVA: 0x000F72C8 File Offset: 0x000F72C8
		public virtual int Representation
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x06002F26 RID: 12070 RVA: 0x000F72CC File Offset: 0x000F72CC
		public virtual int M
		{
			get
			{
				return 113;
			}
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x06002F27 RID: 12071 RVA: 0x000F72D0 File Offset: 0x000F72D0
		public virtual int K1
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06002F28 RID: 12072 RVA: 0x000F72D4 File Offset: 0x000F72D4
		public virtual int K2
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x06002F29 RID: 12073 RVA: 0x000F72D8 File Offset: 0x000F72D8
		public virtual int K3
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06002F2A RID: 12074 RVA: 0x000F72DC File Offset: 0x000F72DC
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SecT113FieldElement);
		}

		// Token: 0x06002F2B RID: 12075 RVA: 0x000F72EC File Offset: 0x000F72EC
		public override bool Equals(ECFieldElement other)
		{
			return this.Equals(other as SecT113FieldElement);
		}

		// Token: 0x06002F2C RID: 12076 RVA: 0x000F72FC File Offset: 0x000F72FC
		public virtual bool Equals(SecT113FieldElement other)
		{
			return this == other || (other != null && Nat128.Eq64(this.x, other.x));
		}

		// Token: 0x06002F2D RID: 12077 RVA: 0x000F7320 File Offset: 0x000F7320
		public override int GetHashCode()
		{
			return 113009 ^ Arrays.GetHashCode(this.x, 0, 2);
		}

		// Token: 0x04001C0B RID: 7179
		protected internal readonly ulong[] x;
	}
}
