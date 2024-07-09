using System;
using Org.BouncyCastle.Math.Raw;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x020005C6 RID: 1478
	internal class SecT193FieldElement : AbstractF2mFieldElement
	{
		// Token: 0x06003081 RID: 12417 RVA: 0x000FDCF8 File Offset: 0x000FDCF8
		public SecT193FieldElement(BigInteger x)
		{
			if (x == null || x.SignValue < 0 || x.BitLength > 193)
			{
				throw new ArgumentException("value invalid for SecT193FieldElement", "x");
			}
			this.x = SecT193Field.FromBigInteger(x);
		}

		// Token: 0x06003082 RID: 12418 RVA: 0x000FDD50 File Offset: 0x000FDD50
		public SecT193FieldElement()
		{
			this.x = Nat256.Create64();
		}

		// Token: 0x06003083 RID: 12419 RVA: 0x000FDD64 File Offset: 0x000FDD64
		protected internal SecT193FieldElement(ulong[] x)
		{
			this.x = x;
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x06003084 RID: 12420 RVA: 0x000FDD74 File Offset: 0x000FDD74
		public override bool IsOne
		{
			get
			{
				return Nat256.IsOne64(this.x);
			}
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06003085 RID: 12421 RVA: 0x000FDD84 File Offset: 0x000FDD84
		public override bool IsZero
		{
			get
			{
				return Nat256.IsZero64(this.x);
			}
		}

		// Token: 0x06003086 RID: 12422 RVA: 0x000FDD94 File Offset: 0x000FDD94
		public override bool TestBitZero()
		{
			return (this.x[0] & 1UL) != 0UL;
		}

		// Token: 0x06003087 RID: 12423 RVA: 0x000FDDA8 File Offset: 0x000FDDA8
		public override BigInteger ToBigInteger()
		{
			return Nat256.ToBigInteger64(this.x);
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06003088 RID: 12424 RVA: 0x000FDDB8 File Offset: 0x000FDDB8
		public override string FieldName
		{
			get
			{
				return "SecT193Field";
			}
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06003089 RID: 12425 RVA: 0x000FDDC0 File Offset: 0x000FDDC0
		public override int FieldSize
		{
			get
			{
				return 193;
			}
		}

		// Token: 0x0600308A RID: 12426 RVA: 0x000FDDC8 File Offset: 0x000FDDC8
		public override ECFieldElement Add(ECFieldElement b)
		{
			ulong[] z = Nat256.Create64();
			SecT193Field.Add(this.x, ((SecT193FieldElement)b).x, z);
			return new SecT193FieldElement(z);
		}

		// Token: 0x0600308B RID: 12427 RVA: 0x000FDDFC File Offset: 0x000FDDFC
		public override ECFieldElement AddOne()
		{
			ulong[] z = Nat256.Create64();
			SecT193Field.AddOne(this.x, z);
			return new SecT193FieldElement(z);
		}

		// Token: 0x0600308C RID: 12428 RVA: 0x000FDE28 File Offset: 0x000FDE28
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			return this.Add(b);
		}

		// Token: 0x0600308D RID: 12429 RVA: 0x000FDE34 File Offset: 0x000FDE34
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			ulong[] z = Nat256.Create64();
			SecT193Field.Multiply(this.x, ((SecT193FieldElement)b).x, z);
			return new SecT193FieldElement(z);
		}

		// Token: 0x0600308E RID: 12430 RVA: 0x000FDE68 File Offset: 0x000FDE68
		public override ECFieldElement MultiplyMinusProduct(ECFieldElement b, ECFieldElement x, ECFieldElement y)
		{
			return this.MultiplyPlusProduct(b, x, y);
		}

		// Token: 0x0600308F RID: 12431 RVA: 0x000FDE74 File Offset: 0x000FDE74
		public override ECFieldElement MultiplyPlusProduct(ECFieldElement b, ECFieldElement x, ECFieldElement y)
		{
			ulong[] array = this.x;
			ulong[] y2 = ((SecT193FieldElement)b).x;
			ulong[] array2 = ((SecT193FieldElement)x).x;
			ulong[] y3 = ((SecT193FieldElement)y).x;
			ulong[] array3 = Nat256.CreateExt64();
			SecT193Field.MultiplyAddToExt(array, y2, array3);
			SecT193Field.MultiplyAddToExt(array2, y3, array3);
			ulong[] z = Nat256.Create64();
			SecT193Field.Reduce(array3, z);
			return new SecT193FieldElement(z);
		}

		// Token: 0x06003090 RID: 12432 RVA: 0x000FDEE0 File Offset: 0x000FDEE0
		public override ECFieldElement Divide(ECFieldElement b)
		{
			return this.Multiply(b.Invert());
		}

		// Token: 0x06003091 RID: 12433 RVA: 0x000FDEF0 File Offset: 0x000FDEF0
		public override ECFieldElement Negate()
		{
			return this;
		}

		// Token: 0x06003092 RID: 12434 RVA: 0x000FDEF4 File Offset: 0x000FDEF4
		public override ECFieldElement Square()
		{
			ulong[] z = Nat256.Create64();
			SecT193Field.Square(this.x, z);
			return new SecT193FieldElement(z);
		}

		// Token: 0x06003093 RID: 12435 RVA: 0x000FDF20 File Offset: 0x000FDF20
		public override ECFieldElement SquareMinusProduct(ECFieldElement x, ECFieldElement y)
		{
			return this.SquarePlusProduct(x, y);
		}

		// Token: 0x06003094 RID: 12436 RVA: 0x000FDF2C File Offset: 0x000FDF2C
		public override ECFieldElement SquarePlusProduct(ECFieldElement x, ECFieldElement y)
		{
			ulong[] array = this.x;
			ulong[] array2 = ((SecT193FieldElement)x).x;
			ulong[] y2 = ((SecT193FieldElement)y).x;
			ulong[] array3 = Nat256.CreateExt64();
			SecT193Field.SquareAddToExt(array, array3);
			SecT193Field.MultiplyAddToExt(array2, y2, array3);
			ulong[] z = Nat256.Create64();
			SecT193Field.Reduce(array3, z);
			return new SecT193FieldElement(z);
		}

		// Token: 0x06003095 RID: 12437 RVA: 0x000FDF88 File Offset: 0x000FDF88
		public override ECFieldElement SquarePow(int pow)
		{
			if (pow < 1)
			{
				return this;
			}
			ulong[] z = Nat256.Create64();
			SecT193Field.SquareN(this.x, pow, z);
			return new SecT193FieldElement(z);
		}

		// Token: 0x06003096 RID: 12438 RVA: 0x000FDFBC File Offset: 0x000FDFBC
		public override ECFieldElement HalfTrace()
		{
			ulong[] z = Nat256.Create64();
			SecT193Field.HalfTrace(this.x, z);
			return new SecT193FieldElement(z);
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06003097 RID: 12439 RVA: 0x000FDFE8 File Offset: 0x000FDFE8
		public override bool HasFastTrace
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003098 RID: 12440 RVA: 0x000FDFEC File Offset: 0x000FDFEC
		public override int Trace()
		{
			return (int)SecT193Field.Trace(this.x);
		}

		// Token: 0x06003099 RID: 12441 RVA: 0x000FDFFC File Offset: 0x000FDFFC
		public override ECFieldElement Invert()
		{
			ulong[] z = Nat256.Create64();
			SecT193Field.Invert(this.x, z);
			return new SecT193FieldElement(z);
		}

		// Token: 0x0600309A RID: 12442 RVA: 0x000FE028 File Offset: 0x000FE028
		public override ECFieldElement Sqrt()
		{
			ulong[] z = Nat256.Create64();
			SecT193Field.Sqrt(this.x, z);
			return new SecT193FieldElement(z);
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x0600309B RID: 12443 RVA: 0x000FE054 File Offset: 0x000FE054
		public virtual int Representation
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x0600309C RID: 12444 RVA: 0x000FE058 File Offset: 0x000FE058
		public virtual int M
		{
			get
			{
				return 193;
			}
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x0600309D RID: 12445 RVA: 0x000FE060 File Offset: 0x000FE060
		public virtual int K1
		{
			get
			{
				return 15;
			}
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x0600309E RID: 12446 RVA: 0x000FE064 File Offset: 0x000FE064
		public virtual int K2
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x0600309F RID: 12447 RVA: 0x000FE068 File Offset: 0x000FE068
		public virtual int K3
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060030A0 RID: 12448 RVA: 0x000FE06C File Offset: 0x000FE06C
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SecT193FieldElement);
		}

		// Token: 0x060030A1 RID: 12449 RVA: 0x000FE07C File Offset: 0x000FE07C
		public override bool Equals(ECFieldElement other)
		{
			return this.Equals(other as SecT193FieldElement);
		}

		// Token: 0x060030A2 RID: 12450 RVA: 0x000FE08C File Offset: 0x000FE08C
		public virtual bool Equals(SecT193FieldElement other)
		{
			return this == other || (other != null && Nat256.Eq64(this.x, other.x));
		}

		// Token: 0x060030A3 RID: 12451 RVA: 0x000FE0B0 File Offset: 0x000FE0B0
		public override int GetHashCode()
		{
			return 1930015 ^ Arrays.GetHashCode(this.x, 0, 4);
		}

		// Token: 0x04001C33 RID: 7219
		protected internal readonly ulong[] x;
	}
}
